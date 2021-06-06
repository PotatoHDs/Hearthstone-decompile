using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hearthstone.UI.Scripting
{
	[Serializable]
	public struct ScriptString
	{
		public string Script;

		public string HumanReadableScript;

		public HashSet<int> GetDataModelIDs()
		{
			MatchCollection matchCollection = Regex.Matches(Script, "(?:\\(|[\\s]|^)\\$([\\d]+)");
			HashSet<int> hashSet = new HashSet<int>();
			foreach (Match item in matchCollection)
			{
				hashSet.Add(int.Parse(item.Groups[1].Value));
			}
			return hashSet;
		}
	}
}
