using System;
using UnityEngine;

// Token: 0x02000B08 RID: 2824
[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class Tile9Mesh : MonoBehaviour
{
	// Token: 0x06009640 RID: 38464 RVA: 0x0030A55C File Offset: 0x0030875C
	private void Start()
	{
		this.vertices = new Vector3[16];
		this.uv = new Vector2[16];
		this.mesh = new Mesh
		{
			name = "Tile9Mesh"
		};
		this.FillGeometry();
		this.FillMesh();
		this.mesh.triangles = new int[]
		{
			0,
			1,
			12,
			0,
			12,
			11,
			1,
			2,
			13,
			1,
			13,
			12,
			2,
			3,
			4,
			2,
			4,
			13,
			13,
			4,
			5,
			13,
			5,
			14,
			14,
			5,
			6,
			14,
			6,
			7,
			15,
			14,
			7,
			15,
			7,
			8,
			10,
			15,
			8,
			10,
			8,
			9,
			11,
			12,
			15,
			11,
			15,
			10,
			12,
			13,
			14,
			12,
			14,
			15
		};
		this.RecalculateMesh();
		base.gameObject.GetComponent<MeshFilter>().mesh = this.mesh;
	}

	// Token: 0x06009641 RID: 38465 RVA: 0x0030A5DE File Offset: 0x003087DE
	public void UpdateMesh()
	{
		if (this.mesh != null)
		{
			this.FillGeometry();
			this.FillMesh();
			this.mesh.RecalculateBounds();
			this.mesh.RecalculateNormals();
		}
	}

	// Token: 0x06009642 RID: 38466 RVA: 0x0030A610 File Offset: 0x00308810
	private void FillGeometry()
	{
		float num = this.pivot.x * this.width;
		float num2 = this.pivot.y * this.height;
		float num3 = this.width;
		float num4 = this.height;
		float num5 = this.uvLeft * this.uvToWorldScaleX;
		float num6 = this.width - this.uvRight * this.uvToWorldScaleX;
		float num7 = this.height - this.uvTop * this.uvToWorldScaleY;
		float num8 = this.uvBottom * this.uvToWorldScaleY;
		this.vertices[0] = new Vector3(0f - num, 0f - num2, 0f);
		this.vertices[1] = new Vector3(0f - num, num8 - num2, 0f);
		this.vertices[2] = new Vector3(0f - num, num7 - num2, 0f);
		this.vertices[3] = new Vector3(0f - num, num4 - num2, 0f);
		this.vertices[4] = new Vector3(num5 - num, num4 - num2, 0f);
		this.vertices[5] = new Vector3(num6 - num, num4 - num2, 0f);
		this.vertices[6] = new Vector3(num3 - num, num4 - num2, 0f);
		this.vertices[7] = new Vector3(num3 - num, num7 - num2, 0f);
		this.vertices[8] = new Vector3(num3 - num, num8 - num2, 0f);
		this.vertices[9] = new Vector3(num3 - num, 0f - num2, 0f);
		this.vertices[10] = new Vector3(num6 - num, 0f - num2, 0f);
		this.vertices[11] = new Vector3(num5 - num, 0f - num2, 0f);
		this.vertices[12] = new Vector3(num5 - num, num8 - num2, 0f);
		this.vertices[13] = new Vector3(num5 - num, num7 - num2, 0f);
		this.vertices[14] = new Vector3(num6 - num, num7 - num2, 0f);
		this.vertices[15] = new Vector3(num6 - num, num8 - num2, 0f);
		float x = this.uvLeft;
		float x2 = 1f - this.uvRight;
		float y = 1f - this.uvTop;
		float y2 = this.uvBottom;
		this.uv[0] = new Vector2(0f, 0f);
		this.uv[1] = new Vector2(0f, y2);
		this.uv[2] = new Vector2(0f, y);
		this.uv[3] = new Vector2(0f, 1f);
		this.uv[4] = new Vector2(x, 1f);
		this.uv[5] = new Vector2(x2, 1f);
		this.uv[6] = new Vector2(1f, 1f);
		this.uv[7] = new Vector2(1f, y);
		this.uv[8] = new Vector2(1f, y2);
		this.uv[9] = new Vector2(1f, 0f);
		this.uv[10] = new Vector2(x2, 0f);
		this.uv[11] = new Vector2(x, 0f);
		this.uv[12] = new Vector2(x, y2);
		this.uv[13] = new Vector2(x, y);
		this.uv[14] = new Vector2(x2, y);
		this.uv[15] = new Vector2(x2, y2);
	}

	// Token: 0x06009643 RID: 38467 RVA: 0x0030AA45 File Offset: 0x00308C45
	private void FillMesh()
	{
		this.mesh.vertices = this.vertices;
		this.mesh.uv = this.uv;
	}

	// Token: 0x06009644 RID: 38468 RVA: 0x0030AA69 File Offset: 0x00308C69
	private void RecalculateMesh()
	{
		this.mesh.RecalculateBounds();
		this.mesh.RecalculateNormals();
	}

	// Token: 0x04007DED RID: 32237
	public float width = 1f;

	// Token: 0x04007DEE RID: 32238
	public float height = 1f;

	// Token: 0x04007DEF RID: 32239
	[Range(0f, 0.5f)]
	public float uvLeft = 0.2f;

	// Token: 0x04007DF0 RID: 32240
	[Range(0f, 0.5f)]
	public float uvRight = 0.2f;

	// Token: 0x04007DF1 RID: 32241
	[Range(0f, 0.5f)]
	public float uvTop = 0.2f;

	// Token: 0x04007DF2 RID: 32242
	[Range(0f, 0.5f)]
	public float uvBottom = 0.2f;

	// Token: 0x04007DF3 RID: 32243
	public float uvToWorldScaleX = 1f;

	// Token: 0x04007DF4 RID: 32244
	public float uvToWorldScaleY = 1f;

	// Token: 0x04007DF5 RID: 32245
	public Vector2 pivot = new Vector2(0.5f, 0.5f);

	// Token: 0x04007DF6 RID: 32246
	private Mesh mesh;

	// Token: 0x04007DF7 RID: 32247
	private Vector3[] vertices;

	// Token: 0x04007DF8 RID: 32248
	private Vector2[] uv;
}
