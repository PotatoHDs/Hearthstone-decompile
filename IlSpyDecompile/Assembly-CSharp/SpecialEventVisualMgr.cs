using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class SpecialEventVisualMgr : MonoBehaviour
{
	public List<SpecialEventVisualDef> m_EventDefs = new List<SpecialEventVisualDef>();

	public bool LoadEvent(SpecialEventType eventType)
	{
		for (int i = 0; i < m_EventDefs.Count; i++)
		{
			SpecialEventVisualDef specialEventVisualDef = m_EventDefs[i];
			if (specialEventVisualDef.m_EventType == eventType)
			{
				AssetLoader.Get().InstantiatePrefab(specialEventVisualDef.m_Prefab, null);
				return true;
			}
		}
		return false;
	}

	public bool UnloadEvent(SpecialEventType eventType)
	{
		for (int i = 0; i < m_EventDefs.Count; i++)
		{
			if (m_EventDefs[i].m_EventType == eventType)
			{
				GameObject gameObject = GameObject.Find(base.name);
				if (gameObject != null)
				{
					Object.Destroy(gameObject);
				}
			}
		}
		return false;
	}

	private void OnEventFinished(Spell spell, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell.gameObject);
		}
	}
}
