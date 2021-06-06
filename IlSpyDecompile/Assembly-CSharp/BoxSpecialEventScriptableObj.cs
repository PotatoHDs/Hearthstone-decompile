using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
[CreateAssetMenu(fileName = "SpecialEventsList", menuName = "ScriptableObjects/BoxSpecialEventScriptableObj", order = 1)]
public class BoxSpecialEventScriptableObj : ScriptableObject
{
	public List<BoxSpecialEvent> m_specialEvents;
}
