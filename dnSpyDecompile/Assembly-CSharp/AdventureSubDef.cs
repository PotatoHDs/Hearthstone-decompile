using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
[CustomEditClass]
public class AdventureSubDef : MonoBehaviour
{
	// Token: 0x06000478 RID: 1144 RVA: 0x0001B190 File Offset: 0x00019390
	public void Init(AdventureDataDbfRecord advDataRecord)
	{
		this.m_AdventureModeId = (AdventureModeDbId)advDataRecord.ModeId;
		this.m_SortOrder = advDataRecord.SortOrder;
		this.m_ShortName = advDataRecord.ShortName;
		this.m_LockedShortName = advDataRecord.LockedShortName;
		this.m_Description = (UniversalInputManager.UsePhoneUI ? advDataRecord.ShortDescription : advDataRecord.Description);
		this.m_LockedDescription = (UniversalInputManager.UsePhoneUI ? advDataRecord.LockedShortDescription : advDataRecord.LockedDescription);
		this.m_RequirementsDescription = advDataRecord.RequirementsDescription;
		this.m_CompleteBannerText = advDataRecord.CompleteBannerText;
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0001B243 File Offset: 0x00019443
	public AdventureModeDbId GetAdventureModeId()
	{
		return this.m_AdventureModeId;
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0001B24B File Offset: 0x0001944B
	public int GetSortOrder()
	{
		return this.m_SortOrder;
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0001B253 File Offset: 0x00019453
	public string GetShortName()
	{
		return this.m_ShortName;
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001B25B File Offset: 0x0001945B
	public string GetLockedShortName()
	{
		return this.m_LockedShortName;
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0001B263 File Offset: 0x00019463
	public string GetDescription()
	{
		return this.m_Description;
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0001B26B File Offset: 0x0001946B
	public string GetLockedDescription()
	{
		return this.m_LockedDescription;
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0001B273 File Offset: 0x00019473
	public string GetRequirementsDescription()
	{
		return this.m_RequirementsDescription;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0001B27B File Offset: 0x0001947B
	public string GetCompleteBannerText()
	{
		return this.m_CompleteBannerText;
	}

	// Token: 0x0400031C RID: 796
	[CustomEditField(Sections = "Mission Display", T = EditType.TEXTURE)]
	public string m_WatermarkTexture;

	// Token: 0x0400031D RID: 797
	[CustomEditField(Sections = "Chooser", T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_ChooserDescriptionPrefab;

	// Token: 0x0400031E RID: 798
	[CustomEditField(Sections = "Chooser", T = EditType.TEXTURE)]
	public string m_Texture;

	// Token: 0x0400031F RID: 799
	[CustomEditField(Sections = "Chooser")]
	public Vector2 m_TextureTiling = Vector2.one;

	// Token: 0x04000320 RID: 800
	[CustomEditField(Sections = "Chooser")]
	public Vector2 m_TextureOffset = Vector2.zero;

	// Token: 0x04000321 RID: 801
	private AdventureModeDbId m_AdventureModeId;

	// Token: 0x04000322 RID: 802
	private int m_SortOrder;

	// Token: 0x04000323 RID: 803
	private string m_ShortName;

	// Token: 0x04000324 RID: 804
	private string m_LockedShortName;

	// Token: 0x04000325 RID: 805
	private string m_Description;

	// Token: 0x04000326 RID: 806
	private string m_LockedDescription;

	// Token: 0x04000327 RID: 807
	private string m_RequirementsDescription;

	// Token: 0x04000328 RID: 808
	private string m_CompleteBannerText;
}
