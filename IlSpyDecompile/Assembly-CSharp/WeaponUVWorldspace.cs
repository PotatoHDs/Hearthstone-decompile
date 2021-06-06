using UnityEngine;

public class WeaponUVWorldspace : MonoBehaviour
{
	public float xOffset;

	public float yOffset;

	private Material m_material;

	private void Start()
	{
		m_material = base.gameObject.GetComponent<Renderer>().GetMaterial();
	}

	private void Update()
	{
		Vector3 vector = base.transform.position * 0.7f;
		m_material.SetFloat("_OffsetX", 0f - vector.z - xOffset);
		m_material.SetFloat("_OffsetY", 0f - vector.x - yOffset);
	}
}
