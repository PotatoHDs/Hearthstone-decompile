using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001051 RID: 4177
	[Serializable]
	public struct ScriptString
	{
		// Token: 0x0600B4FD RID: 46333 RVA: 0x0037A6E4 File Offset: 0x003788E4
		public HashSet<int> GetDataModelIDs()
		{
			MatchCollection matchCollection = Regex.Matches(this.Script, "(?:\\(|[\\s]|^)\\$([\\d]+)");
			HashSet<int> hashSet = new HashSet<int>();
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				hashSet.Add(int.Parse(match.Groups[1].Value));
			}
			return hashSet;
		}

		// Token: 0x04009708 RID: 38664
		public string Script;

		// Token: 0x04009709 RID: 38665
		public string HumanReadableScript;
	}
}
