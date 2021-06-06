using UnityEngine;

public class TiledBackground : MonoBehaviour
{
	private Renderer m_renderer;

	private Material m_material;

	public float Depth;

	private Renderer TiledRenderer
	{
		get
		{
			if (m_renderer == null)
			{
				m_renderer = GetComponent<Renderer>();
			}
			return m_renderer;
		}
	}

	private Material TiledMaterial
	{
		get
		{
			if (m_material == null && TiledRenderer != null)
			{
				m_material = TiledRenderer.GetMaterial();
			}
			return m_material;
		}
	}

	public Vector2 Offset
	{
		get
		{
			if (TiledMaterial == null)
			{
				return Vector2.zero;
			}
			Vector3 vector = TiledMaterial.mainTextureOffset;
			Vector3 vector2 = TiledMaterial.mainTextureScale;
			return new Vector2(vector.x / vector2.x, vector.y / vector2.y);
		}
		set
		{
			if (!(TiledMaterial == null))
			{
				Vector3 vector = TiledMaterial.mainTextureScale;
				TiledMaterial.mainTextureOffset = new Vector2(vector.x * value.x, vector.y * value.y);
			}
		}
	}

	private void Awake()
	{
		if (TiledMaterial == null)
		{
			Debug.LogError("TiledBackground requires the mesh renderer and for it to have a material!");
			Object.Destroy(this);
		}
	}

	public void SetBounds(Bounds bounds)
	{
		if (TiledRenderer == null)
		{
			Debug.LogError("TiledBackground.SetBounds - no renderer was found on this game object!");
			return;
		}
		base.transform.localScale = Vector3.one;
		Bounds bounds2 = TiledRenderer.bounds;
		Vector3 vector = bounds2.min;
		Vector3 vector2 = bounds2.max;
		if (base.transform.parent != null)
		{
			vector = base.transform.parent.InverseTransformPoint(vector);
			vector2 = base.transform.parent.InverseTransformPoint(vector2);
		}
		Vector3 vector3 = VectorUtils.Abs(vector2 - vector);
		Vector3 vector4 = new Vector3((vector3.x > 0f) ? (bounds.size.x / vector3.x) : 0f, (vector3.y > 0f) ? (bounds.size.y / vector3.y) : 0f, (vector3.z > 0f) ? (bounds.size.z / vector3.z) : 0f);
		base.transform.localScale = vector4;
		base.transform.localPosition = bounds.center + new Vector3(0f, 0f, 0f - Depth);
		if (TiledMaterial == null)
		{
			Debug.LogError("TiledBackground.SetBounds - no material was found on this component!");
		}
		else
		{
			TiledMaterial.mainTextureScale = vector4;
		}
	}
}
