using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CF RID: 207
[CustomEditClass]
[CreateAssetMenu(fileName = "SpecialEventsList", menuName = "ScriptableObjects/BoxSpecialEventScriptableObj", order = 1)]
public class BoxSpecialEventScriptableObj : ScriptableObject
{
	// Token: 0x040008AD RID: 2221
	public List<BoxSpecialEvent> m_specialEvents;
}
