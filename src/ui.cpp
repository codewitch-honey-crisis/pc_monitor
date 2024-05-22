#include "lcd_panel.hpp"
#include "ui.hpp"
// our font for the UI. 
#define OPENSANS_REGULAR_IMPLEMENTATION
#include <assets/OpenSans_Regular.hpp>
// for easier modification
const gfx::open_font& text_font = OpenSans_Regular;
#define CHAIN_SLASH_IMPLEMENTATION
#include <assets/chain_slash.hpp>
using namespace gfx;
using namespace uix;
// implement std::move to limit dependencies on the STL, which may not be there
template< class T > struct ui_remove_reference      { typedef T type; };
template< class T > struct ui_remove_reference<T&>  { typedef T type; };
template< class T > struct ui_remove_reference<T&&> { typedef T type; };
template <typename T>
typename ui_remove_reference<T>::type&& ui_move(T&& arg) {
    return static_cast<typename ui_remove_reference<T>::type&&>(arg);
}
#ifdef LCD_DMA
// use two 32KB buffers (DMA)
uint8_t lcd_transfer_buffer1[lcd_transfer_buffer_size];
uint8_t lcd_transfer_buffer2[];
#else
uint8_t lcd_transfer_buffer1[lcd_transfer_buffer_size];
#endif

static canvas_t disconnected_ico(disconnected_screen);
screen_entry_t* ui_screen_entries = nullptr;
screen_entry::screen_entry(invalidation_tracker& parent) : value(parent), icon(parent), graph(parent), next(nullptr) {

}
screen_entry::screen_entry(screen_entry&& rhs) : value(main_screen), icon(main_screen), graph(main_screen) {
    do_move(rhs);
}
screen_entry& screen_entry::operator=(screen_entry&& rhs) {
    do_move(rhs);
    return *this;
}
void screen_entry::do_move(screen_entry& rhs) {
    memcpy(colors,rhs.colors,sizeof(colors));
    hsv_color=rhs.hsv_color;
    memcpy(icon_data,rhs.icon_data,sizeof(icon_data));
    icon = ui_move(rhs.icon);
    graph_data=ui_move(rhs.graph_data);
    graph = ui_move(rhs.graph);
    value_real = rhs.value_real;
    value_max = rhs.value_max;
    memcpy(value_data,rhs.value_data,sizeof(value_data));
    memcpy(value_format,rhs.value_format,sizeof(value_format));
    value = ui_move(rhs.value);
}
screen_entry_t* ui_screen_entry_tail() {
    screen_entry_t* current = ui_screen_entries;
    screen_entry_t* result = nullptr;
    while(current!=nullptr) {
        result = current;
        current = current->next;
    }
    return result;
}
screen_entry_t* ui_add_screen_entry() {
    screen_entry_t* result = new screen_entry_t(main_screen);
    if(result==nullptr) { // out of memory
        return nullptr;
    }
    if(ui_screen_entries==nullptr) {   
        ui_screen_entries = result;
    } else {
        screen_entry_t* tail = ui_screen_entry_tail();
        tail->next = result;
    }
    return result;
}
void ui_clear_screen_entries() {
    screen_entry_t* current = ui_screen_entries;
    ui_screen_entries = nullptr;
    while(current!=nullptr) {
        screen_entry_t* next = current->next;
        delete current;
        current = next;
    }
}
void ui_clear_graphs() {
    screen_entry_t* current = ui_screen_entries;
    ui_screen_entries = nullptr;
    while(current!=nullptr) {
        current->graph_data.clear();
        current = current->next;
    }
}
static void ui_on_paint_disconnected(canvas_t::control_surface_type& destination, 
                    const gfx::srect16& clip, 
                    void* state) {
    const const_bitmap<alpha_pixel<8>>& ico = (destination.dimensions().width<faChainSlash.dimensions().width)?faSmallChainSlash:faChainSlash;
    draw::icon(destination,point16::zero(),ico,color_t::black);
}

void ui_init() {
    disconnected_ico.on_paint_callback(ui_on_paint_disconnected);
    const const_bitmap<alpha_pixel<8>>& ico = (disconnected_screen.dimensions().width<faChainSlash.dimensions().width)?faSmallChainSlash:faChainSlash;
    disconnected_ico.bounds(((srect16)ico.bounds()).center(disconnected_screen.bounds()));
    disconnected_screen.register_control(disconnected_ico);
    main_screen.background_color(color_t::black);
    disconnected_screen.background_color(color_t::white);
}
static void ui_on_paint_icon(canvas_t::control_surface_type& destination, 
                    const gfx::srect16& clip, 
                    void* state) {
    const screen_entry_t& s = *(screen_entry_t*)state;
    const_bitmap<rgb_pixel<16>> bmp(size16(16,16),s.icon_data);
    draw::bitmap(destination,destination.bounds(),bmp,bmp.bounds());
}

static rgba_pixel<32> get_rgb(uint16_t color) {
    rgb_pixel<16> px(color,true);
    rgba_pixel<32> result;
    convert(px,&result);
    return result;

}
static hsva_pixel<32> get_hsv(uint16_t color) {
    return convert<rgba_pixel<32>,hsva_pixel<32>>(get_rgb(color));
}
static void ui_on_paint_graph(canvas_t::control_surface_type& destination, 
                    const gfx::srect16& clip, 
                    void* state) {
    const screen_entry_t& s = *(screen_entry_t*)state;
    const bool hsv = s.hsv_color;
    const graph_buffer_t& buf = s.graph_data;
    const uint16_t width = destination.dimensions().width;
    const float x_scale = (float)width/100.0f;
    const uint16_t height = destination.dimensions().height;
    const float svalue = s.value_real/s.value_max;
    float blend_step = 1.0f/(float)width;
    const uint16_t x_end = width*svalue;
    int grad_step = 1;
    float blend_ratio = 0.0f;
    uint16_t x = 0;
    for(int i = 0;i<width;++i) {
        const srect16 r(i,0,i,height-1);
        draw::line(destination,r,color_t::black,&clip);
        rgba_pixel<32> col;
        if(s.hsv_color) {
            if(s.colors[0]!=s.colors[1]) {
                auto tmp = get_hsv(s.colors[1]).blend(get_hsv(s.colors[0]),blend_ratio);
                convert(tmp,&col);
            } else {
                convert(get_hsv(s.colors[0]),&col);
            }
        } else {
            col = get_rgb(s.colors[0]);
            if(s.colors[0]!=s.colors[1]) {
                col = get_rgb(s.colors[1]).blend(col,blend_ratio);
            }
        }
        if(i>=x_end) {
            col.template channel<channel_name::A>(50);
        }
        draw::line(destination,r,col);
        blend_ratio+=blend_step;
    }
    if(!buf.empty()) {    
        float ov = (*buf.peek(0))/255.0f;
        const float x_step = roundf(((float)destination.dimensions().width)/(float)(buf.capacity));
        int ox = 0;
        int x = x_step;
        int i=1;
        for(;i<buf.size();++i) {
            const float v = (((float)*buf.peek(i))/255.0f);
            const int oy = destination.bounds().y2 - (ov*destination.bounds().y2);
            const int y = destination.bounds().y2 - (v*destination.bounds().y2);
            srect16 sr(ox,oy,x,y);
            rgba_pixel<32> col = color32_t::black;
            if(x>x_end) {
                if(hsv) {
                    if(s.colors[0]!=s.colors[1]) {
                        convert(get_hsv(s.colors[1]).blend(get_hsv(s.colors[0]),v),&col);
                    }
                    else {
                        convert(get_hsv(s.colors[0]),&col);
                    }
                } else {
                    if(s.colors[0]!=s.colors[1]) {
                        col = get_rgb(s.colors[1]).blend(get_rgb(s.colors[0]),v);
                    } else {
                        col = get_rgb(s.colors[0]);
                    }
                }                
            }
            draw::line(destination,sr
                            ,col);
            ov =v;
            ox = x;
            x+=x_step;
        }
    }
}

bool ui_status_screen_build(const screen_packet_t* packets, size_t packets_count) {
    main_screen.unregister_controls();
    ui_clear_screen_entries();
    constexpr static const rgba_pixel<32> transparent(0, 0, 0, 0);
    const uint16_t screen_width = main_screen.dimensions().width;
    const uint16_t screen_height = main_screen.dimensions().height;
    uint16_t packet_height = (screen_height / packets_count)-1;
    size_t size = packets_count;
    if(packet_height<16) {
        // TODO: there's a better way to do this
        // i'm just not thinking right now
        int h = 16;
        while(screen_height%h) {
            ++h;
        }
        packet_height = screen_height / h;
        size = screen_height / packet_height;
        --packet_height;
    }
    if(packet_height>(screen_height/2)) {
        int h = screen_height/2;
        while(screen_height%h) {
            ++h;
        }
        packet_height = screen_height / h;
        size = screen_height / packet_height;
        --packet_height;
    }
    const size_t total_height = (packet_height+1) * size;

    uint16_t text_line_height = packet_height-4;
    // leave space for the icon and padding
    const uint16_t max_text_width = (screen_width/2)-17-16;
    // compute the text width
    char sz[64];
    uint16_t min_text_width = 0;
    for(size_t i = 0;i<size;++i) {
        const screen_packet_t& pck = packets[i];       
        sprintf(sz,pck.format,pck.value_max);
        uint16_t tw= text_font.measure_text(ssize16::max(),spoint16::zero(),sz,text_font.scale(text_line_height)).width;
        while(tw>max_text_width && text_line_height>=10) {
            --text_line_height;
            tw= text_font.measure_text(ssize16::max(),spoint16::zero(),sz,text_font.scale(text_line_height)).width;
        }
        if(tw>min_text_width) {
            min_text_width = tw;
        }
    }
    const uint16_t packet_left_width=17+min_text_width;
    int y = (screen_height-total_height)/2;
    for(size_t i = 0;i<size;++i) {
        const screen_packet_t& pck = packets[i];
        screen_entry_t* entry_ptr = ui_add_screen_entry();
        if(entry_ptr==nullptr) {
            // out of memory
            return false;
        }
        screen_entry_t& entry = *entry_ptr;
        snprintf(entry.value_data,sizeof(entry.value_data),pck.format,pck.value,pck.value_max);
        memcpy(entry.icon_data,pck.icon,sizeof(entry.icon_data));
        entry.graph_data.put((pck.value/pck.value_max)*255);
        entry.value_real = pck.value;
        entry.value_max = pck.value_max;
        memcpy(entry.value_format,pck.format,sizeof(pck.format));
        const srect16 packet_bounds(0,y,screen_width-1,packet_height+y-1);
        entry.icon.bounds(srect16(0,0,15,15).center_vertical(packet_bounds).offset(packet_bounds.x1,0));        
        entry.value.bounds(srect16(17,0,min_text_width-1+17,0+text_line_height).center_vertical(packet_bounds).offset(packet_bounds.x1,0));
        entry.graph.bounds(srect16(packet_left_width+packet_bounds.x1,packet_bounds.y1,packet_bounds.x2,packet_bounds.y2));
        entry.icon.on_paint_callback(ui_on_paint_icon,entry_ptr);
        entry.value.text_color(color32_t::white);
        entry.value.text_open_font(&text_font);
        entry.value.text(entry.value_data);
        entry.value.text_line_height(text_line_height);
        entry.value.text_justify(uix_justify::bottom_right);
        entry.value.background_color(transparent);
        entry.value.border_color(transparent);
        entry.value.padding({0,0});
        entry.graph.on_paint_callback(ui_on_paint_graph,entry_ptr);;
        main_screen.register_control(entry.icon);
        main_screen.register_control(entry.value);
        main_screen.register_control(entry.graph);
        y+=packet_height;
    }
    return true;
}
bool ui_status_screen_update(const screen_packet_t* packets, size_t packets_count) {
    screen_entry_t* se = ui_screen_entries;
    int i = 0;
    while(se!=nullptr && i < packets_count) {
        const screen_packet_t& pck = packets[i];
        float v = pck.value;
        uint8_t vb = (pck.value/pck.value_max)*255;
        se->value_max = pck.value_max;
        se->value_real = pck.value;
        char sz[32];
        bool refresh_fmt = false;
        if(0!=memcmp(se->value_format,pck.format,sizeof(pck.format))) {
            refresh_fmt = true;
        }
        memcpy(se->value_format,pck.format,sizeof(pck.format));
        snprintf(sz,sizeof(sz),pck.format,pck.value,pck.value_max);
        if(refresh_fmt || 0!=strcmp(sz,se->value_data)) {
            memcpy(se->value_data,sz,sizeof(sz));
            se->value.invalidate();
        }
        uint8_t tmp;
        if(se->graph_data.full()) {
            se->graph_data.get(&tmp);
        }
        se->graph_data.put(vb);
        se->graph.invalidate();
        if(0!=memcmp(se->icon_data,pck.icon,sizeof(pck.icon))) {
            memcpy(se->icon_data,pck.icon,sizeof(pck.icon));
            se->icon.invalidate();
        }
        se->colors[0] = pck.colors[0];
        se->colors[1] = pck.colors[1];
        se->hsv_color = pck.hsv_color;
        se=se->next;
        ++i;
    }
    return i==0;
}