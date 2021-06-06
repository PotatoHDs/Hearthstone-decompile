using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class AdventureWingAccent : MonoBehaviour
{
	// Token: 0x0600052B RID: 1323 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
	private void Start()
	{
		if (this.AssociatedWing == null)
		{
			return;
		}
		WingDbId wingId = this.AssociatedWing.GetWingId();
		GameObject accentObjectFromWingId = this.GetAccentObjectFromWingId(wingId);
		if (accentObjectFromWingId == null)
		{
			return;
		}
		accentObjectFromWingId.SetActive(true);
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0001E5FC File Offset: 0x0001C7FC
	private GameObject GetAccentObjectFromWingId(WingDbId wingId)
	{
		foreach (WingAccentMapping wingAccentMapping in this.WingAccentMappingList)
		{
			if (wingAccentMapping.WingId == wingId)
			{
				return wingAccentMapping.AccentObject;
			}
		}
		return null;
	}

	// Token: 0x04000370 RID: 880
	[SerializeField]
	public AdventureWing AssociatedWing;

	// Token: 0x04000371 RID: 881
	[SerializeField]
	public List<WingAccentMapping> WingAccentMappingList;
}
