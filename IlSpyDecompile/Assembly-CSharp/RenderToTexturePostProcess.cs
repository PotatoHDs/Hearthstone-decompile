using UnityEngine;

public interface RenderToTexturePostProcess
{
	void Init(int outputTextureWidth, int outputTextureHeight);

	void End();

	void AddCommandBuffers(Camera camera);

	bool IsUsedBy(DiamondRenderToTexture r2t);
}
