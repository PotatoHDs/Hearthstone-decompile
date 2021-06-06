using System;
using System.Collections.Generic;
using UnityEngine;

public class FontTableData : ScriptableObject
{
	[Serializable]
	public class FontTableEntry
	{
		public string m_FontDefName;

		public string m_FontName;

		public string m_FontGuid;
	}

	public List<FontTableEntry> m_Entries;
}
