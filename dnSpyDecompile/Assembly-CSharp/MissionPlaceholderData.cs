using System;
using UnityEngine;

// Token: 0x02000012 RID: 18
[CustomEditClass]
public class MissionPlaceholderData : ScriptableObject
{
	// Token: 0x04000019 RID: 25
	public string m_TexturesFolder = "Assets/Shared/Textures/Store/Adventures";

	// Token: 0x0400001A RID: 26
	public string m_MaterialsFolder = "Assets/Shared/Meshes/Store/Materials/Adventures";

	// Token: 0x0400001B RID: 27
	public string descriptionsFolder = "Assets/Game/GameObjects/AdventureDescriptions";

	// Token: 0x0400001C RID: 28
	public string defsFolder = "Assets/Game/GameObjects/AdventureDefs";

	// Token: 0x0400001D RID: 29
	public Texture m_FrameArt;

	// Token: 0x0400001E RID: 30
	public Texture m_FrameArtMask;

	// Token: 0x0400001F RID: 31
	public Texture m_FrameArtFX1;

	// Token: 0x04000020 RID: 32
	public Texture m_FrameArtFX2;

	// Token: 0x04000021 RID: 33
	public Texture m_SetIcon;

	// Token: 0x04000022 RID: 34
	public Texture m_Watermark;

	// Token: 0x04000023 RID: 35
	public Texture m_ChooserTemplate;

	// Token: 0x04000024 RID: 36
	public Mesh m_HeroicPlate;

	// Token: 0x04000025 RID: 37
	public Mesh m_HeroicPlatePhone;

	// Token: 0x04000026 RID: 38
	public Mesh m_PlateShadow;

	// Token: 0x04000027 RID: 39
	public Mesh m_PlateShadowPhone;

	// Token: 0x04000028 RID: 40
	public Material m_FrameArtMat;

	// Token: 0x04000029 RID: 41
	public Material m_FrameArtMatPhone;

	// Token: 0x0400002A RID: 42
	public Material m_SetIconMat;

	// Token: 0x0400002B RID: 43
	public GameObject m_AdventureDescriptionNormal;

	// Token: 0x0400002C RID: 44
	public GameObject m_AdventureDescriptionNormalPhone;

	// Token: 0x0400002D RID: 45
	public GameObject m_AdventureDescriptionHeroic;

	// Token: 0x0400002E RID: 46
	public GameObject m_AdventureDescriptionHeroicPhone;

	// Token: 0x0400002F RID: 47
	public GameObject m_HeroicBanner;
}
