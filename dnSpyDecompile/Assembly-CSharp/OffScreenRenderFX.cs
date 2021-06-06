using System;
using UnityEngine;

// Token: 0x02000A57 RID: 2647
public class OffScreenRenderFX : MonoBehaviour
{
	// Token: 0x06008EA0 RID: 36512 RVA: 0x002E08C0 File Offset: 0x002DEAC0
	private void Start()
	{
		if (this.UseBounds)
		{
			Mesh mesh = base.GetComponent<MeshFilter>().mesh;
			this.RenderBounds = mesh.bounds;
		}
		this.Yoffset = OffScreenRenderFX.s_Yoffset;
		OffScreenRenderFX.s_Yoffset += 10f;
		this.PositionObjectToRender();
		this.CreateCamera();
		this.CreateRenderTexture();
		this.SetupCamera();
		this.SetupMaterial();
		base.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x06008EA1 RID: 36513 RVA: 0x002E0932 File Offset: 0x002DEB32
	private void OnDestroy()
	{
		if (this.tempRenderBuffer != null && this.tempRenderBuffer.IsCreated())
		{
			RenderTexture.ReleaseTemporary(this.tempRenderBuffer);
		}
	}

	// Token: 0x06008EA2 RID: 36514 RVA: 0x002E095C File Offset: 0x002DEB5C
	private void CreateCamera()
	{
		if (this.offscreenFXCameraGO == null)
		{
			if (this.offscreenFXCamera != null)
			{
				UnityEngine.Object.Destroy(this.offscreenFXCamera);
			}
			this.offscreenFXCameraGO = new GameObject();
			this.offscreenFXCamera = this.offscreenFXCameraGO.AddComponent<Camera>();
			this.offscreenFXCameraGO.name = base.name + "_OffScreenFXCamera";
			SceneUtils.SetHideFlags(this.offscreenFXCameraGO, HideFlags.HideAndDontSave);
			UniversalInputManager.Get().AddIgnoredCamera(this.offscreenFXCamera);
		}
	}

	// Token: 0x06008EA3 RID: 36515 RVA: 0x002E09E8 File Offset: 0x002DEBE8
	private void SetupCamera()
	{
		this.offscreenFXCamera.orthographic = true;
		this.UpdateOffScreenCamera();
		this.offscreenFXCamera.transform.parent = base.transform;
		this.offscreenFXCamera.nearClipPlane = -this.AboveClip;
		this.offscreenFXCamera.farClipPlane = this.BelowClip;
		this.offscreenFXCamera.targetTexture = this.tempRenderBuffer;
		this.offscreenFXCamera.depth = Camera.main.depth - 1f;
		this.offscreenFXCamera.backgroundColor = Color.black;
		this.offscreenFXCamera.clearFlags = CameraClearFlags.Color;
		this.offscreenFXCamera.rect = this.CameraRect;
		this.offscreenFXCamera.enabled = true;
		this.offscreenFXCamera.allowHDR = false;
	}

	// Token: 0x06008EA4 RID: 36516 RVA: 0x002E0AB4 File Offset: 0x002DECB4
	private void UpdateOffScreenCamera()
	{
		if (this.ObjectToRender == null)
		{
			return;
		}
		if (this.ForceSize == 0f)
		{
			float num = base.transform.localScale.x;
			if (this.UseBounds)
			{
				num *= this.RenderBounds.size.x;
			}
			this.offscreenFXCamera.orthographicSize = num / 2f;
		}
		else
		{
			this.offscreenFXCamera.orthographicSize = this.ForceSize;
		}
		this.offscreenFXCameraGO.transform.position = this.ObjectToRender.transform.position;
		this.offscreenFXCameraGO.transform.rotation = this.ObjectToRender.transform.rotation;
		this.offscreenFXCameraGO.transform.Rotate(90f, 180f, 0f);
	}

	// Token: 0x06008EA5 RID: 36517 RVA: 0x002E0B8D File Offset: 0x002DED8D
	private void CreateRenderTexture()
	{
		this.tempRenderBuffer = RenderTexture.GetTemporary(this.RenderResolutionX, this.RenderResolutionY);
	}

	// Token: 0x06008EA6 RID: 36518 RVA: 0x002E0BA6 File Offset: 0x002DEDA6
	private void SetupMaterial()
	{
		base.gameObject.GetComponent<Renderer>().GetMaterial().mainTexture = this.tempRenderBuffer;
	}

	// Token: 0x06008EA7 RID: 36519 RVA: 0x002E0BC4 File Offset: 0x002DEDC4
	private void PositionObjectToRender()
	{
		Vector3 b = Vector3.up * this.Yoffset;
		Vector3 b2 = Vector3.right * OffScreenRenderFX.s_Xoffset;
		if (this.ObjectToRender != null)
		{
			this.ObjectToRender.transform.position += b;
		}
		this.ObjectToRender.transform.position += b2;
	}

	// Token: 0x040076EA RID: 30442
	public GameObject ObjectToRender;

	// Token: 0x040076EB RID: 30443
	public bool UseBounds = true;

	// Token: 0x040076EC RID: 30444
	public int RenderResolutionX = 256;

	// Token: 0x040076ED RID: 30445
	public int RenderResolutionY = 256;

	// Token: 0x040076EE RID: 30446
	public float AboveClip = 1f;

	// Token: 0x040076EF RID: 30447
	public float BelowClip = 1f;

	// Token: 0x040076F0 RID: 30448
	public float ForceSize;

	// Token: 0x040076F1 RID: 30449
	public Rect CameraRect = new Rect(0f, 0f, 1f, 1f);

	// Token: 0x040076F2 RID: 30450
	private Camera offscreenFXCamera;

	// Token: 0x040076F3 RID: 30451
	private GameObject offscreenFXCameraGO;

	// Token: 0x040076F4 RID: 30452
	private RenderTexture tempRenderBuffer;

	// Token: 0x040076F5 RID: 30453
	private const int IgnoreLayer = 21;

	// Token: 0x040076F6 RID: 30454
	private float Yoffset;

	// Token: 0x040076F7 RID: 30455
	private Bounds RenderBounds;

	// Token: 0x040076F8 RID: 30456
	private static float s_Yoffset = 250f;

	// Token: 0x040076F9 RID: 30457
	private static float s_Xoffset = 250f;
}
