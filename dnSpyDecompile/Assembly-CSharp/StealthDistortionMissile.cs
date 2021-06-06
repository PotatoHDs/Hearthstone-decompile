using System;
using UnityEngine;

// Token: 0x0200075E RID: 1886
public class StealthDistortionMissile : MonoBehaviour
{
	// Token: 0x060069E1 RID: 27105 RVA: 0x00228538 File Offset: 0x00226738
	private void Start()
	{
		this.Yoffset = StealthDistortionMissile.s_Yoffset;
		StealthDistortionMissile.s_Yoffset += 10f;
		this.PositionObjectToRender();
		this.CreateCameras();
		this.CreateRenderTextures();
		this.SetupCameras();
		this.SetupMaterial();
		base.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x060069E2 RID: 27106 RVA: 0x0022858A File Offset: 0x0022678A
	private void OnDestroy()
	{
		RenderTexture.ReleaseTemporary(this.particleRenderBuffer);
		RenderTexture.ReleaseTemporary(this.boardRenderBuffer);
	}

	// Token: 0x060069E3 RID: 27107 RVA: 0x002285A4 File Offset: 0x002267A4
	private void CreateCameras()
	{
		if (this.particleCameraGO == null)
		{
			if (this.particleCamera != null)
			{
				UnityEngine.Object.Destroy(this.particleCamera);
			}
			this.particleCameraGO = new GameObject();
			this.particleCamera = this.particleCameraGO.AddComponent<Camera>();
			this.particleCameraGO.name = base.name + "_DistortionParticleFXCamera";
		}
		if (this.boardCameraGO == null)
		{
			if (this.boardCamera != null)
			{
				UnityEngine.Object.Destroy(this.boardCamera);
			}
			this.boardCameraGO = new GameObject();
			this.boardCamera = this.boardCameraGO.AddComponent<Camera>();
			this.boardCameraGO.name = base.name + "_DistortionBoardFXCamera";
		}
	}

	// Token: 0x060069E4 RID: 27108 RVA: 0x00228670 File Offset: 0x00226870
	private void SetupCameras()
	{
		this.particleCamera.orthographic = true;
		this.particleCamera.orthographicSize = base.transform.localScale.x / 2f;
		this.particleCameraGO.transform.position = this.ObjectToRender.transform.position;
		this.particleCameraGO.transform.Translate(this.RenderOffsetX, this.RenderOffsetY, this.RenderOffsetZ);
		this.particleCameraGO.transform.rotation = this.ObjectToRender.transform.rotation;
		this.particleCameraGO.transform.Rotate(90f, 180f, 0f);
		this.particleCamera.transform.parent = base.transform;
		this.particleCamera.nearClipPlane = -this.ParticleAboveClip;
		this.particleCamera.farClipPlane = this.ParticleBelowClip;
		this.particleCamera.targetTexture = this.particleRenderBuffer;
		this.particleCamera.depth = Camera.main.depth - 1f;
		this.particleCamera.backgroundColor = Color.black;
		this.particleCamera.clearFlags = CameraClearFlags.Color;
		this.particleCamera.cullingMask &= -2097153;
		this.particleCamera.enabled = true;
		this.particleCamera.allowHDR = false;
		this.boardCamera.orthographic = true;
		this.boardCamera.orthographicSize = base.transform.localScale.x / 2f;
		this.boardCameraGO.transform.position = base.transform.position;
		this.boardCameraGO.transform.rotation = base.transform.rotation;
		this.boardCameraGO.transform.Rotate(90f, 180f, 0f);
		this.boardCamera.transform.parent = base.transform;
		this.boardCamera.nearClipPlane = -this.DistortionAboveClip;
		this.boardCamera.farClipPlane = this.DistortionBelowClip;
		this.boardCamera.targetTexture = this.boardRenderBuffer;
		this.boardCamera.depth = Camera.main.depth - 1f;
		this.boardCamera.backgroundColor = Color.black;
		this.boardCamera.clearFlags = CameraClearFlags.Color;
		this.boardCamera.cullingMask &= -2097153;
		this.boardCamera.enabled = true;
		this.boardCamera.allowHDR = false;
	}

	// Token: 0x060069E5 RID: 27109 RVA: 0x0022890D File Offset: 0x00226B0D
	private void CreateRenderTextures()
	{
		this.particleRenderBuffer = RenderTexture.GetTemporary(this.ParticleResolutionX, this.ParticleResolutionY);
		this.boardRenderBuffer = RenderTexture.GetTemporary(this.DistortionResolutionX, this.DistortionResolutionY);
	}

	// Token: 0x060069E6 RID: 27110 RVA: 0x0022893D File Offset: 0x00226B3D
	private void SetupMaterial()
	{
		Material material = base.gameObject.GetComponent<Renderer>().GetMaterial();
		material.mainTexture = this.boardRenderBuffer;
		material.SetTexture("_ParticleTex", this.particleRenderBuffer);
	}

	// Token: 0x060069E7 RID: 27111 RVA: 0x0022896C File Offset: 0x00226B6C
	private void PositionObjectToRender()
	{
		Vector3 b = Vector3.up * this.Yoffset;
		this.ObjectToRender.transform.position += b;
	}

	// Token: 0x040056AB RID: 22187
	public GameObject ObjectToRender;

	// Token: 0x040056AC RID: 22188
	public int ParticleResolutionX = 256;

	// Token: 0x040056AD RID: 22189
	public int ParticleResolutionY = 256;

	// Token: 0x040056AE RID: 22190
	public float ParticleAboveClip = 1f;

	// Token: 0x040056AF RID: 22191
	public float ParticleBelowClip = 1f;

	// Token: 0x040056B0 RID: 22192
	public float RenderOffsetX;

	// Token: 0x040056B1 RID: 22193
	public float RenderOffsetY;

	// Token: 0x040056B2 RID: 22194
	public float RenderOffsetZ;

	// Token: 0x040056B3 RID: 22195
	public int DistortionResolutionX = 256;

	// Token: 0x040056B4 RID: 22196
	public int DistortionResolutionY = 256;

	// Token: 0x040056B5 RID: 22197
	public float DistortionAboveClip = -0.1f;

	// Token: 0x040056B6 RID: 22198
	public float DistortionBelowClip = 10f;

	// Token: 0x040056B7 RID: 22199
	private Camera particleCamera;

	// Token: 0x040056B8 RID: 22200
	private GameObject particleCameraGO;

	// Token: 0x040056B9 RID: 22201
	private RenderTexture particleRenderBuffer;

	// Token: 0x040056BA RID: 22202
	private Camera boardCamera;

	// Token: 0x040056BB RID: 22203
	private GameObject boardCameraGO;

	// Token: 0x040056BC RID: 22204
	private RenderTexture boardRenderBuffer;

	// Token: 0x040056BD RID: 22205
	private const int IgnoreLayer = 21;

	// Token: 0x040056BE RID: 22206
	private float Yoffset;

	// Token: 0x040056BF RID: 22207
	private static float s_Yoffset = 50f;
}
