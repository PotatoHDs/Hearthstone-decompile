using System;
using UnityEngine;

// Token: 0x02000A4A RID: 2634
public class LocalRenderEffect : MonoBehaviour
{
	// Token: 0x170007FA RID: 2042
	// (get) Token: 0x06008E50 RID: 36432 RVA: 0x002DE7AC File Offset: 0x002DC9AC
	protected Material BloomMaterial
	{
		get
		{
			if (this.m_BloomMaterial == null)
			{
				this.m_BloomMaterial = new Material(this.m_BloomShader);
				SceneUtils.SetHideFlags(this.m_BloomMaterial, HideFlags.DontSave);
			}
			return this.m_BloomMaterial;
		}
	}

	// Token: 0x170007FB RID: 2043
	// (get) Token: 0x06008E51 RID: 36433 RVA: 0x002DE7E0 File Offset: 0x002DC9E0
	protected Material BlurMaterial
	{
		get
		{
			if (this.m_BlurMaterial == null)
			{
				this.m_BlurMaterial = new Material(this.m_BlurShader);
				SceneUtils.SetHideFlags(this.m_BlurMaterial, HideFlags.DontSave);
			}
			return this.m_BlurMaterial;
		}
	}

	// Token: 0x170007FC RID: 2044
	// (get) Token: 0x06008E52 RID: 36434 RVA: 0x002DE814 File Offset: 0x002DCA14
	protected Material AdditiveMaterial
	{
		get
		{
			if (this.m_AdditiveMaterial == null)
			{
				this.m_AdditiveMaterial = new Material(this.m_AdditiveShader);
				SceneUtils.SetHideFlags(this.m_AdditiveMaterial, HideFlags.DontSave);
			}
			return this.m_AdditiveMaterial;
		}
	}

	// Token: 0x06008E53 RID: 36435 RVA: 0x002DE848 File Offset: 0x002DCA48
	protected void Start()
	{
		this.m_BloomShader = ShaderUtils.FindShader(this.BLOOM_SHADER_NAME);
		if (!this.m_BloomShader)
		{
			Debug.LogError("Failed to load Local Rendering Effect Shader: " + this.BLOOM_SHADER_NAME);
		}
		this.m_BlurShader = ShaderUtils.FindShader(this.BLUR_SHADER_NAME);
		if (!this.m_BlurShader)
		{
			Debug.LogError("Failed to load Local Rendering Effect Shader: " + this.BLUR_SHADER_NAME);
		}
		this.m_AdditiveShader = ShaderUtils.FindShader(this.ADDITIVE_SHADER_NAME);
		if (!this.m_AdditiveShader)
		{
			Debug.LogError("Failed to load Local Rendering Effect Shader: " + this.ADDITIVE_SHADER_NAME);
		}
		this.CreateTexture();
		this.CreateCamera();
		this.CreateRenderPlane();
	}

	// Token: 0x06008E54 RID: 36436 RVA: 0x002DE900 File Offset: 0x002DCB00
	protected void Update()
	{
		this.Render();
	}

	// Token: 0x06008E55 RID: 36437 RVA: 0x002DE908 File Offset: 0x002DCB08
	protected void OnDestroy()
	{
		if (this.m_Camera)
		{
			this.m_Camera.targetTexture = null;
			this.m_Camera.enabled = false;
			UnityEngine.Object.Destroy(this.m_Camera);
			UnityEngine.Object.Destroy(this.m_CameraGO);
		}
		RenderTexture.active = null;
		if (this.m_BloomMaterial == null)
		{
			UnityEngine.Object.Destroy(this.m_BloomMaterial);
		}
		if (this.m_BlurMaterial == null)
		{
			UnityEngine.Object.Destroy(this.m_BlurMaterial);
		}
		if (this.m_AdditiveMaterial == null)
		{
			UnityEngine.Object.Destroy(this.m_AdditiveMaterial);
		}
		if (this.m_RenderTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_RenderTexture);
		}
	}

	// Token: 0x06008E56 RID: 36438 RVA: 0x002DE9C0 File Offset: 0x002DCBC0
	private void OnDrawGizmos()
	{
		if (this.m_Depth < 0f)
		{
			this.m_Depth = 0f;
		}
		Gizmos.DrawWireCube(new Vector3(base.transform.position.x, base.transform.position.y - this.m_Depth * 0.5f, base.transform.position.z), new Vector3(this.m_Width, this.m_Depth, this.m_Height));
	}

	// Token: 0x06008E57 RID: 36439 RVA: 0x002DEA44 File Offset: 0x002DCC44
	public void Render()
	{
		if (this.m_Effect == localRenderEffects.Bloom || this.m_Effect == localRenderEffects.Blur)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(this.m_RenderTexture.width, this.m_RenderTexture.height, this.m_RenderTexture.depth, this.m_RenderTexture.format);
			this.m_Camera.targetTexture = temporary;
			this.m_Camera.Render();
			this.Sample(temporary, this.m_RenderTexture, this.BlurMaterial, this.m_BlurAmount);
			RenderTexture.ReleaseTemporary(temporary);
			Renderer component = this.m_PlaneGameObject.GetComponent<Renderer>();
			component.SetSharedMaterial(this.BloomMaterial);
			Material sharedMaterial = component.GetSharedMaterial();
			sharedMaterial.SetColor("_TintColor", this.m_Color);
			sharedMaterial.mainTexture = this.m_RenderTexture;
			return;
		}
		this.m_Camera.targetTexture = this.m_RenderTexture;
		this.m_Camera.Render();
	}

	// Token: 0x06008E58 RID: 36440 RVA: 0x002DEB24 File Offset: 0x002DCD24
	private void CreateTexture()
	{
		if (this.m_RenderTexture != null)
		{
			return;
		}
		Vector2 vector = this.CalcTextureSize();
		this.m_RenderTexture = RenderTextureTracker.Get().CreateNewTexture((int)vector.x, (int)vector.y, 32, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
	}

	// Token: 0x06008E59 RID: 36441 RVA: 0x002DEB6C File Offset: 0x002DCD6C
	private void CreateCamera()
	{
		if (this.m_Camera != null)
		{
			return;
		}
		this.m_CameraGO = new GameObject();
		this.m_Camera = this.m_CameraGO.AddComponent<Camera>();
		this.m_CameraGO.name = base.name + "_TextRenderCamera";
		SceneUtils.SetHideFlags(this.m_CameraGO, HideFlags.HideAndDontSave);
		this.m_Camera.orthographic = true;
		this.m_CameraGO.transform.parent = base.transform;
		this.m_CameraGO.transform.rotation = Quaternion.identity;
		this.m_CameraGO.transform.position = base.transform.position;
		this.m_CameraGO.transform.Rotate(90f, 0f, 0f);
		this.m_Camera.nearClipPlane = 0f;
		this.m_Camera.farClipPlane = this.m_Depth;
		this.m_Camera.depth = Camera.main.depth - 1f;
		this.m_Camera.backgroundColor = new Color(0f, 0f, 0f, 0f);
		this.m_Camera.clearFlags = CameraClearFlags.Color;
		this.m_Camera.depthTextureMode = DepthTextureMode.None;
		this.m_Camera.renderingPath = RenderingPath.Forward;
		this.m_Camera.cullingMask = this.m_LayerMask;
		this.m_Camera.allowHDR = false;
		this.m_Camera.targetTexture = this.m_RenderTexture;
		this.m_Camera.enabled = false;
		this.m_Camera.orthographicSize = Mathf.Min(this.m_Width * 0.5f, this.m_Height * 0.5f);
	}

	// Token: 0x06008E5A RID: 36442 RVA: 0x002DED28 File Offset: 0x002DCF28
	private Vector2 CalcTextureSize()
	{
		float num = this.m_Height / this.m_Width;
		return new Vector2((float)this.m_Resolution, (float)this.m_Resolution * num);
	}

	// Token: 0x06008E5B RID: 36443 RVA: 0x002DED58 File Offset: 0x002DCF58
	private void CreateRenderPlane()
	{
		if (this.m_Width == this.m_PreviousWidth && this.m_Height == this.m_PreviousHeight)
		{
			return;
		}
		if (this.m_PlaneGameObject != null)
		{
			UnityEngine.Object.Destroy(this.m_PlaneGameObject);
		}
		this.m_PlaneGameObject = base.gameObject;
		this.m_PlaneGameObject.AddComponent<MeshFilter>();
		this.m_PlaneGameObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();
		mesh.name = "TextMeshPlane";
		float num = this.m_Width * 0.5f;
		float num2 = this.m_Height * 0.5f;
		mesh.vertices = new Vector3[]
		{
			new Vector3(-num, this.RENDER_PLANE_OFFSET, -num2),
			new Vector3(num, this.RENDER_PLANE_OFFSET, -num2),
			new Vector3(-num, this.RENDER_PLANE_OFFSET, num2),
			new Vector3(num, this.RENDER_PLANE_OFFSET, num2)
		};
		mesh.uv = this.PLANE_UVS;
		mesh.normals = this.PLANE_NORMALS;
		mesh.triangles = this.PLANE_TRIANGLES;
		this.m_PlaneMesh = (this.m_PlaneGameObject.GetComponent<MeshFilter>().mesh = mesh);
		this.m_PlaneMesh.RecalculateBounds();
		this.BloomMaterial.mainTexture = this.m_RenderTexture;
		this.m_PlaneGameObject.GetComponent<Renderer>().SetSharedMaterial(this.AdditiveMaterial);
		this.m_PreviousWidth = this.m_Width;
		this.m_PreviousHeight = this.m_Height;
	}

	// Token: 0x06008E5C RID: 36444 RVA: 0x002DEED4 File Offset: 0x002DD0D4
	private void Sample(RenderTexture source, RenderTexture dest, Material sampleMat, float offset)
	{
		dest.DiscardContents();
		Graphics.BlitMultiTap(source, dest, sampleMat, new Vector2[]
		{
			new Vector2(-offset, -offset),
			new Vector2(-offset, offset),
			new Vector2(offset, offset),
			new Vector2(offset, -offset)
		});
	}

	// Token: 0x04007660 RID: 30304
	private readonly string ADDITIVE_SHADER_NAME = "Hero/Additive";

	// Token: 0x04007661 RID: 30305
	private readonly string BLOOM_SHADER_NAME = "Hidden/LocalRenderBloom";

	// Token: 0x04007662 RID: 30306
	private readonly string BLUR_SHADER_NAME = "Hidden/LocalRenderBlur";

	// Token: 0x04007663 RID: 30307
	private readonly float RENDER_PLANE_OFFSET = 0.05f;

	// Token: 0x04007664 RID: 30308
	public localRenderEffects m_Effect;

	// Token: 0x04007665 RID: 30309
	public int m_Resolution = 128;

	// Token: 0x04007666 RID: 30310
	public float m_Width = 1f;

	// Token: 0x04007667 RID: 30311
	public float m_Height = 1f;

	// Token: 0x04007668 RID: 30312
	public float m_Depth = 5f;

	// Token: 0x04007669 RID: 30313
	public LayerMask m_LayerMask = -1;

	// Token: 0x0400766A RID: 30314
	public Color m_Color = Color.gray;

	// Token: 0x0400766B RID: 30315
	public float m_BlurAmount = 0.6f;

	// Token: 0x0400766C RID: 30316
	private GameObject m_CameraGO;

	// Token: 0x0400766D RID: 30317
	private Camera m_Camera;

	// Token: 0x0400766E RID: 30318
	private RenderTexture m_RenderTexture;

	// Token: 0x0400766F RID: 30319
	private Mesh m_PlaneMesh;

	// Token: 0x04007670 RID: 30320
	private GameObject m_PlaneGameObject;

	// Token: 0x04007671 RID: 30321
	private float m_PreviousWidth;

	// Token: 0x04007672 RID: 30322
	private float m_PreviousHeight;

	// Token: 0x04007673 RID: 30323
	private Shader m_BloomShader;

	// Token: 0x04007674 RID: 30324
	private Material m_BloomMaterial;

	// Token: 0x04007675 RID: 30325
	private Shader m_BlurShader;

	// Token: 0x04007676 RID: 30326
	private Material m_BlurMaterial;

	// Token: 0x04007677 RID: 30327
	private Shader m_AdditiveShader;

	// Token: 0x04007678 RID: 30328
	private Material m_AdditiveMaterial;

	// Token: 0x04007679 RID: 30329
	private readonly Vector2[] PLANE_UVS = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	// Token: 0x0400767A RID: 30330
	private readonly Vector3[] PLANE_NORMALS = new Vector3[]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	// Token: 0x0400767B RID: 30331
	private readonly int[] PLANE_TRIANGLES = new int[]
	{
		3,
		1,
		2,
		2,
		1,
		0
	};
}
