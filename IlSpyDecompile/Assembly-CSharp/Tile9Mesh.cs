using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class Tile9Mesh : MonoBehaviour
{
	public float width = 1f;

	public float height = 1f;

	[Range(0f, 0.5f)]
	public float uvLeft = 0.2f;

	[Range(0f, 0.5f)]
	public float uvRight = 0.2f;

	[Range(0f, 0.5f)]
	public float uvTop = 0.2f;

	[Range(0f, 0.5f)]
	public float uvBottom = 0.2f;

	public float uvToWorldScaleX = 1f;

	public float uvToWorldScaleY = 1f;

	public Vector2 pivot = new Vector2(0.5f, 0.5f);

	private Mesh mesh;

	private Vector3[] vertices;

	private Vector2[] uv;

	private void Start()
	{
		vertices = new Vector3[16];
		uv = new Vector2[16];
		mesh = new Mesh
		{
			name = "Tile9Mesh"
		};
		FillGeometry();
		FillMesh();
		mesh.triangles = new int[54]
		{
			0, 1, 12, 0, 12, 11, 1, 2, 13, 1,
			13, 12, 2, 3, 4, 2, 4, 13, 13, 4,
			5, 13, 5, 14, 14, 5, 6, 14, 6, 7,
			15, 14, 7, 15, 7, 8, 10, 15, 8, 10,
			8, 9, 11, 12, 15, 11, 15, 10, 12, 13,
			14, 12, 14, 15
		};
		RecalculateMesh();
		base.gameObject.GetComponent<MeshFilter>().mesh = mesh;
	}

	public void UpdateMesh()
	{
		if (mesh != null)
		{
			FillGeometry();
			FillMesh();
			mesh.RecalculateBounds();
			mesh.RecalculateNormals();
		}
	}

	private void FillGeometry()
	{
		float num = pivot.x * width;
		float num2 = pivot.y * height;
		float num3 = width;
		float num4 = height;
		float num5 = uvLeft * uvToWorldScaleX;
		float num6 = width - uvRight * uvToWorldScaleX;
		float num7 = height - uvTop * uvToWorldScaleY;
		float num8 = uvBottom * uvToWorldScaleY;
		vertices[0] = new Vector3(0f - num, 0f - num2, 0f);
		vertices[1] = new Vector3(0f - num, num8 - num2, 0f);
		vertices[2] = new Vector3(0f - num, num7 - num2, 0f);
		vertices[3] = new Vector3(0f - num, num4 - num2, 0f);
		vertices[4] = new Vector3(num5 - num, num4 - num2, 0f);
		vertices[5] = new Vector3(num6 - num, num4 - num2, 0f);
		vertices[6] = new Vector3(num3 - num, num4 - num2, 0f);
		vertices[7] = new Vector3(num3 - num, num7 - num2, 0f);
		vertices[8] = new Vector3(num3 - num, num8 - num2, 0f);
		vertices[9] = new Vector3(num3 - num, 0f - num2, 0f);
		vertices[10] = new Vector3(num6 - num, 0f - num2, 0f);
		vertices[11] = new Vector3(num5 - num, 0f - num2, 0f);
		vertices[12] = new Vector3(num5 - num, num8 - num2, 0f);
		vertices[13] = new Vector3(num5 - num, num7 - num2, 0f);
		vertices[14] = new Vector3(num6 - num, num7 - num2, 0f);
		vertices[15] = new Vector3(num6 - num, num8 - num2, 0f);
		float x = uvLeft;
		float x2 = 1f - uvRight;
		float y = 1f - uvTop;
		float y2 = uvBottom;
		uv[0] = new Vector2(0f, 0f);
		uv[1] = new Vector2(0f, y2);
		uv[2] = new Vector2(0f, y);
		uv[3] = new Vector2(0f, 1f);
		uv[4] = new Vector2(x, 1f);
		uv[5] = new Vector2(x2, 1f);
		uv[6] = new Vector2(1f, 1f);
		uv[7] = new Vector2(1f, y);
		uv[8] = new Vector2(1f, y2);
		uv[9] = new Vector2(1f, 0f);
		uv[10] = new Vector2(x2, 0f);
		uv[11] = new Vector2(x, 0f);
		uv[12] = new Vector2(x, y2);
		uv[13] = new Vector2(x, y);
		uv[14] = new Vector2(x2, y);
		uv[15] = new Vector2(x2, y2);
	}

	private void FillMesh()
	{
		mesh.vertices = vertices;
		mesh.uv = uv;
	}

	private void RecalculateMesh()
	{
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
	}
}
