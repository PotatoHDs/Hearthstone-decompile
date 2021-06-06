using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public static class ScriptKeywords
	{
		private static Dictionary<string, object> s_keywords = new Dictionary<string, object>
		{
			{ "true", true },
			{ "false", false }
		};

		public static IEnumerable<KeyValuePair<string, object>> Keywords => s_keywords;

		public static bool EvaluateKeyword(ScriptToken token, out object value)
		{
			return s_keywords.TryGetValue(token.Value, out value);
		}
	}
}
