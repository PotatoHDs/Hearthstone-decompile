using System.Collections.Generic;
using UnityEngine;

public class AdventureWingAccent : MonoBehaviour
{
	[SerializeField]
	public AdventureWing AssociatedWing;

	[SerializeField]
	public List<WingAccentMapping> WingAccentMappingList;

	private void Start()
	{
		if (!(AssociatedWing == null))
		{
			WingDbId wingId = AssociatedWing.GetWingId();
			GameObject accentObjectFromWingId = GetAccentObjectFromWingId(wingId);
			if (!(accentObjectFromWingId == null))
			{
				accentObjectFromWingId.SetActive(value: true);
			}
		}
	}

	private GameObject GetAccentObjectFromWingId(WingDbId wingId)
	{
		foreach (WingAccentMapping wingAccentMapping in WingAccentMappingList)
		{
			if (wingAccentMapping.WingId == wingId)
			{
				return wingAccentMapping.AccentObject;
			}
		}
		return null;
	}
}
