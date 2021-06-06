using System;
using UnityEngine;

// Token: 0x02000B3B RID: 2875
[ExecuteInEditMode]
public class OutlinePostEffect : MonoBehaviour
{
	// Token: 0x0600987E RID: 39038 RVA: 0x00315DC3 File Offset: 0x00313FC3
	private void Start()
	{
		this.m_AttachedCamera = base.GetComponent<Camera>();
		if (!this.m_TempCam)
		{
			this.MakeTempCam();
		}
		this.m_PostMat = new Material(this.m_RTTOutlineGlow);
	}

	// Token: 0x0600987F RID: 39039 RVA: 0x00315DF8 File Offset: 0x00313FF8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.m_AttachedCamera)
		{
			this.m_AttachedCamera = base.GetComponent<Camera>();
		}
		if (!this.m_TempCam)
		{
			this.MakeTempCam();
		}
		if (!this.m_PostMat)
		{
			this.m_PostMat = new Material(this.m_RTTOutlineGlow);
		}
		this.m_TempCam.CopyFrom(this.m_AttachedCamera);
		this.m_TempCam.clearFlags = CameraClearFlags.Color;
		this.m_TempCam.backgroundColor = Color.black;
		this.m_TempCam.cullingMask = 1 << LayerMask.NameToLayer("Unused16");
		RenderTexture renderTexture = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.ARGB32);
		renderTexture.Create();
		this.m_TempCam.targetTexture = renderTexture;
		this.m_PostMat.SetTexture("_SceneTex", source);
		this.m_TempCam.RenderWithShader(this.m_DrawSimple, "");
		Graphics.Blit(renderTexture, destination, this.m_PostMat);
		renderTexture.Release();
	}

	// Token: 0x06009880 RID: 39040 RVA: 0x00315EF7 File Offset: 0x003140F7
	private void MakeTempCam()
	{
		this.m_TempCam = new GameObject().AddComponent<Camera>();
		this.m_TempCam.enabled = false;
		this.m_TempCam.name = "TempCam";
	}

	// Token: 0x04007F7B RID: 32635
	private Camera m_AttachedCamera;

	// Token: 0x04007F7C RID: 32636
	public Shader m_RTTOutlineGlow;

	// Token: 0x04007F7D RID: 32637
	public Shader m_DrawSimple;

	// Token: 0x04007F7E RID: 32638
	public Camera m_TempCam;

	// Token: 0x04007F7F RID: 32639
	public Material m_PostMat;
}
