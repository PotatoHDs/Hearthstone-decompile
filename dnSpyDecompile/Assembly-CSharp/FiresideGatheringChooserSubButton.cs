using System;
using System.Collections.Generic;
using Hearthstone;
using PegasusShared;
using UnityEngine;

// Token: 0x020002E4 RID: 740
[CustomEditClass]
public class FiresideGatheringChooserSubButton : ChooserSubButton
{
	// Token: 0x170004D0 RID: 1232
	// (get) Token: 0x060026A4 RID: 9892 RVA: 0x000C1EC2 File Offset: 0x000C00C2
	// (set) Token: 0x060026A5 RID: 9893 RVA: 0x000C1ECA File Offset: 0x000C00CA
	public FiresideGatheringManager.FiresideGatheringMode AssociatedMode { get; set; }

	// Token: 0x170004D1 RID: 1233
	// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000C1ED3 File Offset: 0x000C00D3
	// (set) Token: 0x060026A7 RID: 9895 RVA: 0x000C1EDB File Offset: 0x000C00DB
	public FormatType AssociatedFormatType { get; set; }

	// Token: 0x170004D2 RID: 1234
	// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000C1EE4 File Offset: 0x000C00E4
	// (set) Token: 0x060026A9 RID: 9897 RVA: 0x000C1EEC File Offset: 0x000C00EC
	public int AssociatedBrawlLibraryItemId { get; set; }

	// Token: 0x060026AA RID: 9898 RVA: 0x000C1EF5 File Offset: 0x000C00F5
	protected override void Awake()
	{
		base.Awake();
		this.m_officialRotationBrawlIcon.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnHearthstonIconOver));
		this.m_officialRotationBrawlIcon.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnHearthstonIconOut));
	}

	// Token: 0x060026AB RID: 9899 RVA: 0x000C1F30 File Offset: 0x000C0130
	protected override void OnDestroy()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.UnloadUnusedAssets();
		}
		this.m_officialRotationBrawlIcon.RemoveEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnHearthstonIconOver));
		this.m_officialRotationBrawlIcon.RemoveEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnHearthstonIconOut));
		base.OnDestroy();
	}

	// Token: 0x060026AC RID: 9900 RVA: 0x000C1F8A File Offset: 0x000C018A
	public void SetOfficialBrawlRotationIcon(bool active)
	{
		this.m_officialRotationBrawlIcon.gameObject.SetActive(active);
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x000C1FA0 File Offset: 0x000C01A0
	public void SetMaterialFromButtonIndex(int index)
	{
		if (this.m_alternateMaterials == null || this.m_alternateMaterials.Count == 0)
		{
			return;
		}
		int index2 = index % this.m_alternateMaterials.Count;
		this.m_buttonMesh.SetMaterial(this.m_buttonFaceMaterialIndex, this.m_alternateMaterials[index2]);
	}

	// Token: 0x060026AE RID: 9902 RVA: 0x000C1FF0 File Offset: 0x000C01F0
	private void OnHearthstonIconOver(UIEvent e)
	{
		TooltipZone component = this.m_TooltipBone.GetComponent<TooltipZone>();
		if (component == null)
		{
			return;
		}
		component.ShowTooltip(GameStrings.Get("GLUE_FIRESIDE_GATHERING_BRAWL"), GameStrings.Get("GLUE_FIRESIDE_GATHERING_OFFICIAL_FIRESIDE_DESCRIPTION"), 4f, 0);
	}

	// Token: 0x060026AF RID: 9903 RVA: 0x000C2034 File Offset: 0x000C0234
	private void OnHearthstonIconOut(UIEvent e)
	{
		TooltipZone component = this.m_TooltipBone.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	// Token: 0x040015EE RID: 5614
	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public UIBButton m_officialRotationBrawlIcon;

	// Token: 0x040015EF RID: 5615
	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public List<Material> m_alternateMaterials;

	// Token: 0x040015F0 RID: 5616
	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public Renderer m_buttonMesh;

	// Token: 0x040015F1 RID: 5617
	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public int m_buttonFaceMaterialIndex;

	// Token: 0x040015F2 RID: 5618
	[CustomEditField(Sections = "Fireside Gathering Sub Buttons")]
	public GameObject m_TooltipBone;
}
