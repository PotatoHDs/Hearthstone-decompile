using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000962 RID: 2402
[CustomEditClass]
public class SpecialEventVisualMgr : MonoBehaviour
{
	// Token: 0x06008439 RID: 33849 RVA: 0x002AC9B8 File Offset: 0x002AABB8
	public bool LoadEvent(SpecialEventType eventType)
	{
		for (int i = 0; i < this.m_EventDefs.Count; i++)
		{
			SpecialEventVisualDef specialEventVisualDef = this.m_EventDefs[i];
			if (specialEventVisualDef.m_EventType == eventType)
			{
				AssetLoader.Get().InstantiatePrefab(specialEventVisualDef.m_Prefab, null, null, AssetLoadingOptions.None);
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600843A RID: 33850 RVA: 0x002ACA10 File Offset: 0x002AAC10
	public bool UnloadEvent(SpecialEventType eventType)
	{
		for (int i = 0; i < this.m_EventDefs.Count; i++)
		{
			if (this.m_EventDefs[i].m_EventType == eventType)
			{
				GameObject gameObject = GameObject.Find(base.name);
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}
		return false;
	}

	// Token: 0x0600843B RID: 33851 RVA: 0x001FACD3 File Offset: 0x001F8ED3
	private void OnEventFinished(Spell spell, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x04006F24 RID: 28452
	public List<SpecialEventVisualDef> m_EventDefs = new List<SpecialEventVisualDef>();
}
