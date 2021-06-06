using UnityEngine;

[CustomEditClass]
public class CollectionHeroDef : MonoBehaviour
{
	public MaterialReference m_previewMaterial;

	[CustomEditField(T = EditType.UBERANIMATION)]
	public string m_materialAnimationPath;

	public Texture m_fauxPlateTexture;

	public MusicPlaylistType m_heroPlaylist;

	public EmoteType m_storePreviewEmote;

	public EmoteType m_storePurchaseEmote;

	public EmoteType m_collectionManagerPreviewEmote;

	public string GetHeroUberShaderAnimationPath()
	{
		return m_materialAnimationPath;
	}
}
