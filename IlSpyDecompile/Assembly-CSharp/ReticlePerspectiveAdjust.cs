using UnityEngine;

public class ReticlePerspectiveAdjust : MonoBehaviour
{
	public float m_HorizontalAdjustment = 20f;

	public float m_VertialAdjustment = 20f;

	private void Start()
	{
	}

	private void Update()
	{
		Camera main = Camera.main;
		if (!(main == null))
		{
			Vector3 vector = main.WorldToScreenPoint(base.transform.position);
			float num = vector.x / (float)main.pixelWidth - 0.5f;
			float num2 = 0f - (vector.y / (float)main.pixelHeight - 0.5f);
			base.transform.rotation = Quaternion.identity;
			base.transform.Rotate(new Vector3(m_VertialAdjustment * num2, 0f, m_HorizontalAdjustment * num), Space.World);
		}
	}
}
