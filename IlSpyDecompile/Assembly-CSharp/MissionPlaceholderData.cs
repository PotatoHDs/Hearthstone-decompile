using UnityEngine;

[CustomEditClass]
public class MissionPlaceholderData : ScriptableObject
{
	public string m_TexturesFolder = "Assets/Shared/Textures/Store/Adventures";

	public string m_MaterialsFolder = "Assets/Shared/Meshes/Store/Materials/Adventures";

	public string descriptionsFolder = "Assets/Game/GameObjects/AdventureDescriptions";

	public string defsFolder = "Assets/Game/GameObjects/AdventureDefs";

	public Texture m_FrameArt;

	public Texture m_FrameArtMask;

	public Texture m_FrameArtFX1;

	public Texture m_FrameArtFX2;

	public Texture m_SetIcon;

	public Texture m_Watermark;

	public Texture m_ChooserTemplate;

	public Mesh m_HeroicPlate;

	public Mesh m_HeroicPlatePhone;

	public Mesh m_PlateShadow;

	public Mesh m_PlateShadowPhone;

	public Material m_FrameArtMat;

	public Material m_FrameArtMatPhone;

	public Material m_SetIconMat;

	public GameObject m_AdventureDescriptionNormal;

	public GameObject m_AdventureDescriptionNormalPhone;

	public GameObject m_AdventureDescriptionHeroic;

	public GameObject m_AdventureDescriptionHeroicPhone;

	public GameObject m_HeroicBanner;
}
