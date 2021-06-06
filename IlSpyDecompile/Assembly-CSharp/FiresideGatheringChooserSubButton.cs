using System.Collections.Generic;
using Hearthstone;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class FiresideGatheringChooserSubButton : ChooserSubButton
{
	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public UIBButton m_officialRotationBrawlIcon;

	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public List<Material> m_alternateMaterials;

	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public Renderer m_buttonMesh;

	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public int m_buttonFaceMaterialIndex;

	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public GameObject m_TooltipBone;

	public FiresideGatheringManager.FiresideGatheringMode AssociatedMode { get; set; }

	public FormatType AssociatedFormatType { get; set; }

	public int AssociatedBrawlLibraryItemId { get; set; }

	protected override void Awake()
	{
		base.Awake();
		m_officialRotationBrawlIcon.AddEventListener(UIEventType.ROLLOVER, OnHearthstonIconOver);
		m_officialRotationBrawlIcon.AddEventListener(UIEventType.ROLLOUT, OnHearthstonIconOut);
	}

	protected override void OnDestroy()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.UnloadUnusedAssets();
		}
		m_officialRotationBrawlIcon.RemoveEventListener(UIEventType.ROLLOVER, OnHearthstonIconOver);
		m_officialRotationBrawlIcon.RemoveEventListener(UIEventType.ROLLOUT, OnHearthstonIconOut);
		base.OnDestroy();
	}

	public void SetOfficialBrawlRotationIcon(bool active)
	{
		m_officialRotationBrawlIcon.gameObject.SetActive(active);
	}

	public void SetMaterialFromButtonIndex(int index)
	{
		if (m_alternateMaterials != null && m_alternateMaterials.Count != 0)
		{
			int index2 = index % m_alternateMaterials.Count;
			m_buttonMesh.SetMaterial(m_buttonFaceMaterialIndex, m_alternateMaterials[index2]);
		}
	}

	private void OnHearthstonIconOver(UIEvent e)
	{
		TooltipZone component = m_TooltipBone.GetComponent<TooltipZone>();
		if (!(component == null))
		{
			component.ShowTooltip(GameStrings.Get("GLUE_FIRESIDE_GATHERING_BRAWL"), GameStrings.Get("GLUE_FIRESIDE_GATHERING_OFFICIAL_FIRESIDE_DESCRIPTION"), 4f);
		}
	}

	private void OnHearthstonIconOut(UIEvent e)
	{
		TooltipZone component = m_TooltipBone.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}
}
