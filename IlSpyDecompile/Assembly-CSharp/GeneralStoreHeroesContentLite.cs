using UnityEngine;

[CustomEditClass]
public class GeneralStoreHeroesContentLite : MonoBehaviour
{
	public string m_keyArtFadeAnim = "HeroSkinArt_WipeAway";

	public string m_keyArtAppearAnim = "HeroSkinArtGlowIn";

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_keyArtFadeSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_keyArtAppearSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_previewButtonClick;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_backgroundFlipSound;

	public MeshRenderer m_renderQuad;

	public GameObject m_renderToTexture;
}
