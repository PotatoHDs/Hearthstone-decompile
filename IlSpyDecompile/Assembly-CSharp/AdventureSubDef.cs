using UnityEngine;

[CustomEditClass]
public class AdventureSubDef : MonoBehaviour
{
	[CustomEditField(Sections = "Mission Display", T = EditType.TEXTURE)]
	public string m_WatermarkTexture;

	[CustomEditField(Sections = "Chooser", T = EditType.GAME_OBJECT)]
	public String_MobileOverride m_ChooserDescriptionPrefab;

	[CustomEditField(Sections = "Chooser", T = EditType.TEXTURE)]
	public string m_Texture;

	[CustomEditField(Sections = "Chooser")]
	public Vector2 m_TextureTiling = Vector2.one;

	[CustomEditField(Sections = "Chooser")]
	public Vector2 m_TextureOffset = Vector2.zero;

	private AdventureModeDbId m_AdventureModeId;

	private int m_SortOrder;

	private string m_ShortName;

	private string m_LockedShortName;

	private string m_Description;

	private string m_LockedDescription;

	private string m_RequirementsDescription;

	private string m_CompleteBannerText;

	public void Init(AdventureDataDbfRecord advDataRecord)
	{
		m_AdventureModeId = (AdventureModeDbId)advDataRecord.ModeId;
		m_SortOrder = advDataRecord.SortOrder;
		m_ShortName = advDataRecord.ShortName;
		m_LockedShortName = advDataRecord.LockedShortName;
		m_Description = (UniversalInputManager.UsePhoneUI ? advDataRecord.ShortDescription : advDataRecord.Description);
		m_LockedDescription = (UniversalInputManager.UsePhoneUI ? advDataRecord.LockedShortDescription : advDataRecord.LockedDescription);
		m_RequirementsDescription = advDataRecord.RequirementsDescription;
		m_CompleteBannerText = advDataRecord.CompleteBannerText;
	}

	public AdventureModeDbId GetAdventureModeId()
	{
		return m_AdventureModeId;
	}

	public int GetSortOrder()
	{
		return m_SortOrder;
	}

	public string GetShortName()
	{
		return m_ShortName;
	}

	public string GetLockedShortName()
	{
		return m_LockedShortName;
	}

	public string GetDescription()
	{
		return m_Description;
	}

	public string GetLockedDescription()
	{
		return m_LockedDescription;
	}

	public string GetRequirementsDescription()
	{
		return m_RequirementsDescription;
	}

	public string GetCompleteBannerText()
	{
		return m_CompleteBannerText;
	}
}
