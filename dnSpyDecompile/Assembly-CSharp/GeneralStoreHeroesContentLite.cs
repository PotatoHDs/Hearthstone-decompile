using System;
using UnityEngine;

// Token: 0x020006FA RID: 1786
[CustomEditClass]
public class GeneralStoreHeroesContentLite : MonoBehaviour
{
	// Token: 0x040052AF RID: 21167
	public string m_keyArtFadeAnim = "HeroSkinArt_WipeAway";

	// Token: 0x040052B0 RID: 21168
	public string m_keyArtAppearAnim = "HeroSkinArtGlowIn";

	// Token: 0x040052B1 RID: 21169
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_keyArtFadeSound;

	// Token: 0x040052B2 RID: 21170
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_keyArtAppearSound;

	// Token: 0x040052B3 RID: 21171
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_previewButtonClick;

	// Token: 0x040052B4 RID: 21172
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_backgroundFlipSound;

	// Token: 0x040052B5 RID: 21173
	public MeshRenderer m_renderQuad;

	// Token: 0x040052B6 RID: 21174
	public GameObject m_renderToTexture;
}
