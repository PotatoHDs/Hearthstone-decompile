using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class CheckoutMesh : MonoBehaviour, IScreenSpace
{
	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000053 RID: 83 RVA: 0x00002DA5 File Offset: 0x00000FA5
	// (set) Token: 0x06000054 RID: 84 RVA: 0x00002DAD File Offset: 0x00000FAD
	public MeshRenderer MeshRenderer { get; private set; }

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000055 RID: 85 RVA: 0x00002DB6 File Offset: 0x00000FB6
	// (set) Token: 0x06000056 RID: 86 RVA: 0x00002DBE File Offset: 0x00000FBE
	public Texture2D Texture { get; private set; }

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000057 RID: 87 RVA: 0x00002DC7 File Offset: 0x00000FC7
	// (set) Token: 0x06000058 RID: 88 RVA: 0x00002DCF File Offset: 0x00000FCF
	public GameObject CloseButton { get; private set; }

	// Token: 0x06000059 RID: 89 RVA: 0x00002DD8 File Offset: 0x00000FD8
	public static CheckoutMesh GenerateCheckoutMesh(int browserWidth, int browserHeight, float meshWidth, float meshHeight)
	{
		CheckoutMesh checkoutMesh = new GameObject("CheckoutMesh").AddComponent<CheckoutMesh>();
		checkoutMesh.Initialize(browserWidth, browserHeight, meshWidth, meshHeight);
		return checkoutMesh;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002DF4 File Offset: 0x00000FF4
	private void Initialize(int browserWidth, int browserHeight, float meshWidth, float meshHeight)
	{
		MeshFilter filter = base.gameObject.AddComponent<MeshFilter>();
		MeshCollider collider = base.gameObject.AddComponent<MeshCollider>();
		this.MeshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		this.CreateBrowserMesh(filter, collider, meshWidth, meshHeight);
		this.CreateTexture(browserWidth, browserHeight);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00002E3D File Offset: 0x0000103D
	public void UpdateTexture(byte[] bytes)
	{
		if (this.Texture == null)
		{
			return;
		}
		this.Texture.LoadRawTextureData(bytes);
		this.Texture.Apply();
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002E65 File Offset: 0x00001065
	public void ResizeTexture(int width, int height)
	{
		this.CreateTexture(width, height);
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00002E70 File Offset: 0x00001070
	public Rect GetScreenRect()
	{
		float num = (float)Screen.height * base.transform.localScale.x;
		float num2 = num * 1.5f;
		return this.GetScreenRect((int)num2, (int)num);
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00002EA7 File Offset: 0x000010A7
	public Rect GetScreenRect(int width, int height)
	{
		return new Rect((float)((Screen.width - width) / 2), (float)((Screen.height - height) / 2), (float)width, (float)height);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00002EC8 File Offset: 0x000010C8
	public float GetScreenSpaceScale()
	{
		float num = (float)Screen.height * base.transform.localScale.x;
		int height = this.Texture.height;
		return num / (float)height;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00002EFC File Offset: 0x000010FC
	private void CreateBrowserMesh(MeshFilter filter, MeshCollider collider, float width, float height)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		Vector3[] vertices = new Vector3[]
		{
			new Vector3((float)num, (float)num2, (float)num3),
			new Vector3((float)num + width, (float)num2, (float)num3),
			new Vector3((float)num, (float)num2 + height, (float)num3),
			new Vector3((float)num + width, (float)num2 + height, (float)num3)
		};
		Vector4[] tangents = new Vector4[]
		{
			new Vector4(1f, 0f, 0f, -1f),
			new Vector4(1f, 0f, 0f, -1f),
			new Vector4(1f, 0f, 0f, -1f),
			new Vector4(1f, 0f, 0f, -1f)
		};
		int[] array = new int[6];
		array[0] = 0;
		array[3] = (array[2] = 1);
		array[4] = (array[1] = 2);
		array[5] = 3;
		Vector2[] array2 = new Vector2[]
		{
			default(Vector2),
			default(Vector2),
			new Vector2(0f, 0f),
			new Vector2(1f, 0f)
		};
		array2[0] = new Vector2(0f, 1f);
		array2[1] = new Vector2(1f, 1f);
		filter.mesh = new Mesh
		{
			name = "Blizzard Checkout",
			vertices = vertices,
			triangles = array,
			uv = array2,
			tangents = tangents
		};
		filter.mesh.RecalculateNormals();
		collider.sharedMesh = filter.mesh;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x000030D4 File Offset: 0x000012D4
	private void CreateTexture(int width, int height)
	{
		UnityEngine.Object.Destroy(this.Texture);
		this.Texture = null;
		this.Texture = new Texture2D(width, height, TextureFormat.BGRA32, false, false)
		{
			filterMode = FilterMode.Point,
			wrapMode = TextureWrapMode.Clamp
		};
		Color color = CheckoutMesh.kBlizzardBlue;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				this.Texture.SetPixel(i, j, color);
			}
		}
		this.Texture.Apply(false, false);
		Material material = new Material(Shader.Find("Hero/Unlit_Texture"));
		material.SetTexture("_MainTex", this.Texture);
		this.MeshRenderer.SetMaterial(material);
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0000317F File Offset: 0x0000137F
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.Texture);
		this.Texture = null;
	}

	// Token: 0x04000018 RID: 24
	private static readonly Color kBlizzardBlue = new Color(0f, 0.14901961f, 0.3137255f, 1f);
}
