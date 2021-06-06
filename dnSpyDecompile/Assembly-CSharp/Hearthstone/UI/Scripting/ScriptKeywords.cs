using System;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200104E RID: 4174
	public static class ScriptKeywords
	{
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600B4F8 RID: 46328 RVA: 0x0037A669 File Offset: 0x00378869
		public static IEnumerable<KeyValuePair<string, object>> Keywords
		{
			get
			{
				return ScriptKeywords.s_keywords;
			}
		}

		// Token: 0x0600B4F9 RID: 46329 RVA: 0x0037A670 File Offset: 0x00378870
		public static bool EvaluateKeyword(ScriptToken token, out object value)
		{
			return ScriptKeywords.s_keywords.TryGetValue(token.Value, out value);
		}

		// Token: 0x040096FA RID: 38650
		private static Dictionary<string, object> s_keywords = new Dictionary<string, object>
		{
			{
				"true",
				true
			},
			{
				"false",
				false
			}
		};
	}
}
