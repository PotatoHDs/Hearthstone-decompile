using System;
using UnityEngine;

// Token: 0x0200075D RID: 1885
public class Stealth : MonoBehaviour
{
	// Token: 0x060069D8 RID: 27096 RVA: 0x002282DD File Offset: 0x002264DD
	private void Start()
	{
		this.CreateCamera();
		this.CreateRenderTexture();
		this.SetupCamera();
		this.SetupMaterial();
	}

	// Token: 0x060069D9 RID: 27097 RVA: 0x002282F7 File Offset: 0x002264F7
	private void OnDestroy()
	{
		RenderTexture.ReleaseTemporary(this.tempRenderBuffer);
	}

	// Token: 0x060069DA RID: 27098 RVA: 0x00228304 File Offset: 0x00226504
	private void CameraRender()
	{
		this.stealthCamera.Render();
	}

	// Token: 0x060069DB RID: 27099 RVA: 0x00228314 File Offset: 0x00226514
	private void CreateCamera()
	{
		if (this.stealthCameraGO == null)
		{
			if (this.stealthCamera != null)
			{
				UnityEngine.Object.Destroy(this.stealthCamera);
			}
			this.stealthCameraGO = new GameObject();
			this.stealthCamera = this.stealthCameraGO.AddComponent<Camera>();
			this.stealthCameraGO.name = base.name + "_StealthFXCamera";
			SceneUtils.SetHideFlags(this.stealthCameraGO, HideFlags.HideAndDontSave);
		}
	}

	// Token: 0x060069DC RID: 27100 RVA: 0x0022838C File Offset: 0x0022658C
	private void SetupCamera()
	{
		this.stealthCamera.orthographic = true;
		this.UpdateOffScreenCamera();
		this.stealthCamera.transform.parent = base.transform;
		this.stealthCamera.nearClipPlane = -this.AboveClip;
		this.stealthCamera.farClipPlane = this.BelowClip;
		this.stealthCamera.targetTexture = this.tempRenderBuffer;
		this.stealthCamera.depth = Camera.main.depth - 1f;
		this.stealthCamera.backgroundColor = Color.black;
		this.stealthCamera.clearFlags = CameraClearFlags.Color;
		this.stealthCamera.cullingMask &= -2097153;
		this.stealthCamera.enabled = false;
		this.stealthCamera.allowHDR = false;
	}

	// Token: 0x060069DD RID: 27101 RVA: 0x0022845C File Offset: 0x0022665C
	private void UpdateOffScreenCamera()
	{
		this.stealthCamera.orthographicSize = 1f;
		this.stealthCameraGO.transform.position = base.transform.position;
		this.stealthCameraGO.transform.rotation = base.transform.rotation;
		this.stealthCameraGO.transform.Rotate(90f, 180f, 0f);
	}

	// Token: 0x060069DE RID: 27102 RVA: 0x002284CE File Offset: 0x002266CE
	private void CreateRenderTexture()
	{
		this.tempRenderBuffer = RenderTexture.GetTemporary(this.RenderResolutionX, this.RenderResolutionY);
	}

	// Token: 0x060069DF RID: 27103 RVA: 0x002284E7 File Offset: 0x002266E7
	private void SetupMaterial()
	{
		base.gameObject.GetComponent<Renderer>().GetMaterial().mainTexture = this.tempRenderBuffer;
	}

	// Token: 0x040056A3 RID: 22179
	public int RenderResolutionX = 256;

	// Token: 0x040056A4 RID: 22180
	public int RenderResolutionY = 256;

	// Token: 0x040056A5 RID: 22181
	public float AboveClip = 0.4f;

	// Token: 0x040056A6 RID: 22182
	public float BelowClip = 0.4f;

	// Token: 0x040056A7 RID: 22183
	private Camera stealthCamera;

	// Token: 0x040056A8 RID: 22184
	private GameObject stealthCameraGO;

	// Token: 0x040056A9 RID: 22185
	private RenderTexture tempRenderBuffer;

	// Token: 0x040056AA RID: 22186
	private const int IgnoreLayer = 21;
}
