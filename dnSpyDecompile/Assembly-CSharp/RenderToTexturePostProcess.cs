using System;
using UnityEngine;

// Token: 0x02000A77 RID: 2679
public interface RenderToTexturePostProcess
{
	// Token: 0x06008FD7 RID: 36823
	void Init(int outputTextureWidth, int outputTextureHeight);

	// Token: 0x06008FD8 RID: 36824
	void End();

	// Token: 0x06008FD9 RID: 36825
	void AddCommandBuffers(Camera camera);

	// Token: 0x06008FDA RID: 36826
	bool IsUsedBy(DiamondRenderToTexture r2t);
}
