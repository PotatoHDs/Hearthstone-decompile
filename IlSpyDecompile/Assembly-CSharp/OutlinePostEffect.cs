using UnityEngine;

[ExecuteInEditMode]
public class OutlinePostEffect : MonoBehaviour
{
	private Camera m_AttachedCamera;

	public Shader m_RTTOutlineGlow;

	public Shader m_DrawSimple;

	public Camera m_TempCam;

	public Material m_PostMat;

	private void Start()
	{
		m_AttachedCamera = GetComponent<Camera>();
		if (!m_TempCam)
		{
			MakeTempCam();
		}
		m_PostMat = new Material(m_RTTOutlineGlow);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!m_AttachedCamera)
		{
			m_AttachedCamera = GetComponent<Camera>();
		}
		if (!m_TempCam)
		{
			MakeTempCam();
		}
		if (!m_PostMat)
		{
			m_PostMat = new Material(m_RTTOutlineGlow);
		}
		m_TempCam.CopyFrom(m_AttachedCamera);
		m_TempCam.clearFlags = CameraClearFlags.Color;
		m_TempCam.backgroundColor = Color.black;
		m_TempCam.cullingMask = 1 << LayerMask.NameToLayer("Unused16");
		RenderTexture renderTexture = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.ARGB32);
		renderTexture.Create();
		m_TempCam.targetTexture = renderTexture;
		m_PostMat.SetTexture("_SceneTex", source);
		m_TempCam.RenderWithShader(m_DrawSimple, "");
		Graphics.Blit(renderTexture, destination, m_PostMat);
		renderTexture.Release();
	}

	private void MakeTempCam()
	{
		m_TempCam = new GameObject().AddComponent<Camera>();
		m_TempCam.enabled = false;
		m_TempCam.name = "TempCam";
	}
}
