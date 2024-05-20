using System;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
namespace PCMonitor
{
	internal static class JsonParser
	{
		static void _SkipWS(IEnumerator<FAMatch> cursor)
		{
			while ((cursor.Current.SymbolId == JsonTextReaderRunner.WhiteSpace ||
				cursor.Current.SymbolId==JsonTextReaderRunner.CommentLine ||
				cursor.Current.SymbolId == JsonTextReaderRunner.CommentBlock)
				&& cursor.MoveNext()) ;
		}
		static JsonArray _ParseArray(IEnumerator<FAMatch> cursor)
		{
			_SkipWS(cursor);
			var position = cursor.Current.Position;
			var line = cursor.Current.Line;
			var column = cursor.Current.Column;
			var result = new JsonArray();
			if (cursor.Current.SymbolId != JsonTextReaderRunner.Array) 
				throw new JsonException("Expected an array",position,line,column);
			if (!cursor.MoveNext()) 
				throw new JsonException("Unterminated array", position, line, column);
			while (cursor.Current.SymbolId != JsonTextReaderRunner.ArrayEnd)
			{
				result.Add(_ParseValue(cursor));
				_SkipWS(cursor);
				if (cursor.Current.SymbolId == 
					JsonTextReaderRunner.Comma)
				{
					cursor.MoveNext();
					_SkipWS(cursor);
				} else if(cursor.Current.SymbolId==JsonTextReaderRunner.ArrayEnd)
				{
					break;
				}
			}
			return result;
		}
		static KeyValuePair<string,object> _ParseField(IEnumerator<FAMatch> cursor)
		{
			_SkipWS(cursor);
			var position = cursor.Current.Position;
			var line = cursor.Current.Line;
			var column = cursor.Current.Column;
			if (cursor.Current.SymbolId != JsonTextReaderRunner.String) 
				throw new JsonException("Expecting a field name", position, line, column);
			var name = JsonUtility.DeescapeString(
				cursor.Current.Value.Substring(1, cursor.Current.Value.Length - 2));
			_SkipWS(cursor);
			if (!cursor.MoveNext()) 
				throw new JsonException("Unterminated JSON field", position, line, column);
			if (cursor.Current.SymbolId != JsonTextReaderRunner.FieldSeparator) 
				throw new JsonException("Expecting a field separator", position, line, column);
			_SkipWS(cursor);
			if (!cursor.MoveNext()) 
				throw new JsonException("JSON field missing value", position, line, column);
			var value = _ParseValue(cursor);
			return new KeyValuePair<string, object>(name, value);
		}
		static JsonObject _ParseObject(IEnumerator<FAMatch> cursor)
		{
			_SkipWS(cursor);
			var position = cursor.Current.Position;
			var line = cursor.Current.Line;
			var column = cursor.Current.Column;
			var result = new JsonObject();
			if (cursor.Current.SymbolId != JsonTextReaderRunner.Object) 
				throw new JsonException("Expecting a JSON object", position, line, column);
			if (!cursor.MoveNext()) 
				throw new JsonException("Unterminated JSON object", position, line, column);
			while (cursor.Current.SymbolId != JsonTextReaderRunner.ObjectEnd)
			{
				_SkipWS(cursor);
				var kvp = _ParseField(cursor);
				result.Add(kvp.Key, kvp.Value);
				_SkipWS(cursor);
				if (cursor.Current.SymbolId == JsonTextReaderRunner.Comma)
				{
					cursor.MoveNext();
				} else if(cursor.Current.SymbolId == JsonTextReaderRunner.ObjectEnd)
				{
					break;
				}
			}
			return result;
		}
		static object _ParseValue(IEnumerator<FAMatch> cursor)
		{
			var position = cursor.Current.Position;
			var line = cursor.Current.Line;
			var column = cursor.Current.Column;

			object result = null;
			_SkipWS(cursor);
			switch (cursor.Current.SymbolId)
			{
				case JsonTextReaderRunner.Object:
					result = _ParseObject(cursor);
					break;
				case JsonTextReaderRunner.Array:
					result = _ParseArray(cursor);
					break;
				case JsonTextReaderRunner.Number:
					result = double.Parse(
						cursor.Current.Value, 
						CultureInfo.InvariantCulture.NumberFormat);
					break;
				case JsonTextReaderRunner.Boolean:
					result = cursor.Current.Value[0] == 't';
					break;
				case JsonTextReaderRunner.Null:
					break;
				case JsonTextReaderRunner.String:
					result = JsonUtility.DeescapeString(
						cursor.Current.Value.Substring(1, 
							cursor.Current.Value.Length - 2));
					break;
				default:
					throw new JsonException("Expecting a value", 
						position, 
						line, 
						column);
			}
			cursor.MoveNext();
			return result;
		}
		static object _Parse(FARunner runner)
		{
			var e = runner.GetEnumerator();
			if (e.MoveNext())
			{
				// _ParseObject() would be more compliant
				// but some services will return arrays
				// and this can handle that
				return _ParseValue(e);
			}
			throw new JsonException("No content", 0, 0, 0);
		}
		public static object ReadFrom(TextReader json)
		{
			var runner = new JsonTextReaderRunner();
			runner.Set(json);
			return _Parse(runner);
		}
	}
}
