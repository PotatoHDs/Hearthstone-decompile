using UnityEngine;

public class MeshUVScaler : MonoBehaviour
{
	public float UVScaleX;

	public float UVScaleY;

	private Vector2[] uvcache;

	private Vector2[] uvs;

	private MeshFilter meshFilter;

	private SkinnedMeshRenderer skinnedMeshRenderer;

	private Mesh mesh;

	private void OnEnable()
	{
		meshFilter = GetComponent<MeshFilter>();
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
		if ((bool)meshFilter)
		{
			mesh = meshFilter.mesh;
		}
		else if ((bool)skinnedMeshRenderer)
		{
			mesh = skinnedMeshRenderer.sharedMesh;
		}
		if (!mesh)
		{
			base.enabled = false;
		}
		uvcache = mesh.uv;
		uvs = mesh.uv;
		UVScaleX = 1f;
		UVScaleY = 1f;
	}

	private void Update()
	{
		if ((bool)mesh)
		{
			for (int i = 0; i < uvcache.Length; i++)
			{
				uvs[i] = new Vector2(uvcache[i].x * UVScaleX, uvcache[i].y * UVScaleY);
			}
			mesh.uv = uvs;
		}
	}

	private void OnDisable()
	{
		mesh.uv = uvcache;
	}
}
