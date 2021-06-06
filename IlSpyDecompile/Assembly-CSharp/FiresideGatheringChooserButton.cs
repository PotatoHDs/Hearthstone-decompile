using UnityEngine;

[CustomEditClass]
public class FiresideGatheringChooserButton : ChooserButton
{
	public GameObject LanternMesh;

	public GameObject SwordMesh;

	public new FiresideGatheringChooserSubButton CreateSubButton(string subButtonPrefab, bool useAsLastSelected)
	{
		return (FiresideGatheringChooserSubButton)base.CreateSubButton(subButtonPrefab, useAsLastSelected);
	}

	public void ShowLantern()
	{
		LanternMesh.SetActive(value: true);
	}

	public void ShowSwords()
	{
		SwordMesh.SetActive(value: true);
	}
}
