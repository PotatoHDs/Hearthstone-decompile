using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
[CustomEditClass]
public class CollectionHeroDef : MonoBehaviour
{
	// Token: 0x06001060 RID: 4192 RVA: 0x0005B733 File Offset: 0x00059933
	public string GetHeroUberShaderAnimationPath()
	{
		return this.m_materialAnimationPath;
	}

	// Token: 0x04000AEF RID: 2799
	public MaterialReference m_previewMaterial;

	// Token: 0x04000AF0 RID: 2800
	[CustomEditField(T = EditType.UBERANIMATION)]
	public string m_materialAnimationPath;

	// Token: 0x04000AF1 RID: 2801
	public Texture m_fauxPlateTexture;

	// Token: 0x04000AF2 RID: 2802
	public MusicPlaylistType m_heroPlaylist;

	// Token: 0x04000AF3 RID: 2803
	public EmoteType m_storePreviewEmote;

	// Token: 0x04000AF4 RID: 2804
	public EmoteType m_storePurchaseEmote;

	// Token: 0x04000AF5 RID: 2805
	public EmoteType m_collectionManagerPreviewEmote;
}
