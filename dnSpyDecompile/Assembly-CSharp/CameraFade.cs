using System;
using UnityEngine;

// Token: 0x02000A13 RID: 2579
public class CameraFade : MonoBehaviour
{
	// Token: 0x06008B46 RID: 35654 RVA: 0x002C8278 File Offset: 0x002C6478
	private void Awake()
	{
		this.m_TempTexture = new Texture2D(1, 1);
		this.m_TempTexture.SetPixel(0, 0, Color.white);
		this.m_TempTexture.Apply();
		this.m_Camera = base.GetComponent<Camera>();
		this.m_Renderer = base.GetComponent<Renderer>();
		if (this.m_Camera == null)
		{
			Debug.LogError("CameraFade faild to find camera component!");
			base.enabled = false;
		}
		this.m_CameraDepth = this.m_Camera.depth;
		this.SetupCamera();
	}

	// Token: 0x06008B47 RID: 35655 RVA: 0x002C8300 File Offset: 0x002C6500
	private void Update()
	{
		if (this.m_Fade <= 0f)
		{
			if (this.m_Renderer != null && this.m_Renderer.enabled)
			{
				this.m_Renderer.enabled = false;
			}
			if (this.m_Camera.enabled)
			{
				this.m_Camera.enabled = false;
			}
			return;
		}
		if (this.m_Renderer == null)
		{
			this.CreateRenderPlane();
		}
		if (!this.m_Renderer.enabled)
		{
			this.m_Renderer.enabled = true;
		}
		if (!this.m_Camera.enabled)
		{
			this.m_Camera.enabled = true;
		}
		if (this.m_RenderOverAll)
		{
			if (this.m_Camera.depth < 100f)
			{
				this.m_Camera.depth = 100f;
			}
		}
		else if (this.m_Camera.depth != this.m_CameraDepth)
		{
			this.m_Camera.depth = this.m_CameraDepth;
		}
		Color color = new Color(this.m_Color.r, this.m_Color.g, this.m_Color.b, this.m_Fade);
		this.m_Material.color = color;
	}

	// Token: 0x06008B48 RID: 35656 RVA: 0x002C842C File Offset: 0x002C662C
	private void CreateRenderPlane()
	{
		base.gameObject.AddComponent<MeshFilter>();
		this.m_Renderer = base.gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[]
		{
			new Vector3(-10f, -10f, 0f),
			new Vector3(10f, -10f, 0f),
			new Vector3(-10f, 10f, 0f),
			new Vector3(10f, 10f, 0f)
		};
		mesh.colors = new Color[]
		{
			Color.white,
			Color.white,
			Color.white,
			Color.white
		};
		mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		mesh.normals = new Vector3[]
		{
			Vector3.up,
			Vector3.up,
			Vector3.up,
			Vector3.up
		};
		mesh.triangles = new int[]
		{
			3,
			1,
			2,
			2,
			1,
			0
		};
		this.m_Renderer.GetComponent<MeshFilter>().mesh = mesh;
		this.m_Material = new Material(ShaderUtils.FindShader("Hidden/CameraFade"));
		this.m_Renderer.SetSharedMaterial(this.m_Material);
	}

	// Token: 0x06008B49 RID: 35657 RVA: 0x002C85FE File Offset: 0x002C67FE
	private void SetupCamera()
	{
		this.m_Camera.farClipPlane = 1f;
		this.m_Camera.nearClipPlane = -1f;
		this.m_Camera.clearFlags = CameraClearFlags.Nothing;
		this.m_Camera.orthographicSize = 0.5f;
	}

	// Token: 0x040073C2 RID: 29634
	public Color m_Color = Color.black;

	// Token: 0x040073C3 RID: 29635
	public float m_Fade = 1f;

	// Token: 0x040073C4 RID: 29636
	public bool m_RenderOverAll;

	// Token: 0x040073C5 RID: 29637
	private Texture2D m_TempTexture;

	// Token: 0x040073C6 RID: 29638
	private GameObject m_PlaneGameObject;

	// Token: 0x040073C7 RID: 29639
	private Material m_Material;

	// Token: 0x040073C8 RID: 29640
	private Camera m_Camera;

	// Token: 0x040073C9 RID: 29641
	private float m_CameraDepth = 14f;

	// Token: 0x040073CA RID: 29642
	private Renderer m_Renderer;
}
