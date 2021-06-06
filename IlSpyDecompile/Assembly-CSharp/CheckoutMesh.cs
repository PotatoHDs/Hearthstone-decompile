using UnityEngine;

public class CheckoutMesh : MonoBehaviour, IScreenSpace
{
	private static readonly Color kBlizzardBlue = new Color(0f, 38f / 255f, 16f / 51f, 1f);

	public MeshRenderer MeshRenderer { get; private set; }

	public Texture2D Texture { get; private set; }

	public GameObject CloseButton { get; private set; }

	public static CheckoutMesh GenerateCheckoutMesh(int browserWidth, int browserHeight, float meshWidth, float meshHeight)
	{
		CheckoutMesh checkoutMesh = new GameObject("CheckoutMesh").AddComponent<CheckoutMesh>();
		checkoutMesh.Initialize(browserWidth, browserHeight, meshWidth, meshHeight);
		return checkoutMesh;
	}

	private void Initialize(int browserWidth, int browserHeight, float meshWidth, float meshHeight)
	{
		MeshFilter filter = base.gameObject.AddComponent<MeshFilter>();
		MeshCollider collider = base.gameObject.AddComponent<MeshCollider>();
		MeshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		CreateBrowserMesh(filter, collider, meshWidth, meshHeight);
		CreateTexture(browserWidth, browserHeight);
	}

	public void UpdateTexture(byte[] bytes)
	{
		if (!(Texture == null))
		{
			Texture.LoadRawTextureData(bytes);
			Texture.Apply();
		}
	}

	public void ResizeTexture(int width, int height)
	{
		CreateTexture(width, height);
	}

	public Rect GetScreenRect()
	{
		float num = (float)Screen.height * base.transform.localScale.x;
		float num2 = num * 1.5f;
		return GetScreenRect((int)num2, (int)num);
	}

	public Rect GetScreenRect(int width, int height)
	{
		return new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height);
	}

	public float GetScreenSpaceScale()
	{
		float num = (float)Screen.height * base.transform.localScale.x;
		int height = Texture.height;
		return num / (float)height;
	}

	private void CreateBrowserMesh(MeshFilter filter, MeshCollider collider, float width, float height)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		Vector3[] vertices = new Vector3[4]
		{
			new Vector3(num, num2, num3),
			new Vector3((float)num + width, num2, num3),
			new Vector3(num, (float)num2 + height, num3),
			new Vector3((float)num + width, (float)num2 + height, num3)
		};
		Vector4[] tangents = new Vector4[4]
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
		Vector2[] array2 = new Vector2[4];
		array2[2] = new Vector2(0f, 0f);
		array2[3] = new Vector2(1f, 0f);
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

	private void CreateTexture(int width, int height)
	{
		Object.Destroy(Texture);
		Texture = null;
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.BGRA32, mipChain: false, linear: false);
		texture2D.filterMode = FilterMode.Point;
		texture2D.wrapMode = TextureWrapMode.Clamp;
		Texture = texture2D;
		Color color = kBlizzardBlue;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				Texture.SetPixel(i, j, color);
			}
		}
		Texture.Apply(updateMipmaps: false, makeNoLongerReadable: false);
		Material material = new Material(Shader.Find("Hero/Unlit_Texture"));
		material.SetTexture("_MainTex", Texture);
		MeshRenderer.SetMaterial(material);
	}

	private void OnDestroy()
	{
		Object.Destroy(Texture);
		Texture = null;
	}
}
