using UnityEngine;

public class RenderTextureTracker
{
	private static RenderTextureTracker m_instance;

	private RenderTextureTracker()
	{
	}

	public static RenderTextureTracker Get()
	{
		if (m_instance == null)
		{
			m_instance = new RenderTextureTracker();
		}
		return m_instance;
	}

	public RenderTexture CreateNewTexture(int width, int height, int depth, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
	{
		return new RenderTexture(width, height, depth, format, readWrite);
	}

	public void DestroyRenderTexture(RenderTexture renderTexture)
	{
		Object.Destroy(renderTexture);
	}

	public void ReleaseRenderTexture(RenderTexture renderTexture)
	{
		renderTexture.Release();
	}
}
