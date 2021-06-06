using System;
using UnityEngine;

public class RotateByMovement : MonoBehaviour
{
	private Vector3 m_previousPos;

	public GameObject mParent;

	private void Update()
	{
		Transform transform = mParent.transform;
		if (!(m_previousPos == transform.localPosition))
		{
			if (m_previousPos == Vector3.zero)
			{
				m_previousPos = transform.localPosition;
				return;
			}
			Vector3 localPosition = transform.localPosition;
			float num = localPosition.z - m_previousPos.z;
			float num2 = Mathf.Sqrt(Mathf.Pow(localPosition.x - m_previousPos.x, 2f) + Mathf.Pow(num, 2f));
			float num3 = Mathf.Asin(num / num2) * 180f / (float)Math.PI;
			num3 -= 90f;
			base.transform.localEulerAngles = new Vector3(90f, num3, 0f);
			m_previousPos = localPosition;
		}
	}
}
