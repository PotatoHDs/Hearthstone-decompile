using System;
using System.Collections.Generic;
using UnityEngine;

public class UberCurve : MonoBehaviour
{
	[Serializable]
	public class UberCurveControlPoint
	{
		public Vector3 position = Vector3.zero;
	}

	public bool m_Looping;

	public bool m_Reverse;

	public float m_HandleSize = 0.3f;

	public List<UberCurveControlPoint> m_controlPoints;

	private int m_gizmoSteps = 10;

	private bool m_renderControlPoints;

	private bool m_renderStepPoints;

	private bool m_renderingGizmo;

	private void Awake()
	{
		CatmullRomInit();
	}

	private void OnDrawGizmos()
	{
		CatmullRomGizmo();
	}

	public Vector3 CatmullRomEvaluateWorldPosition(float position)
	{
		Vector3 position2 = CatmullRomEvaluateLocalPosition(position);
		return base.transform.TransformPoint(position2);
	}

	public Vector3 CatmullRomEvaluateLocalPosition(float position)
	{
		if (m_controlPoints == null)
		{
			return Vector3.zero;
		}
		int num = m_controlPoints.Count;
		if (!m_Looping)
		{
			num = m_controlPoints.Count - 1;
		}
		if (m_Reverse && !m_renderingGizmo)
		{
			position = 1f - position;
		}
		position = Mathf.Clamp01(position);
		int num2 = Mathf.FloorToInt(position * (float)num);
		float i = position * (float)num - (float)num2;
		Vector3 position2 = m_controlPoints[ClampIndexCatmullRom(num2 - 1)].position;
		Vector3 position3 = m_controlPoints[num2].position;
		Vector3 position4 = m_controlPoints[ClampIndexCatmullRom(num2 + 1)].position;
		Vector3 position5 = m_controlPoints[ClampIndexCatmullRom(num2 + 2)].position;
		return CatmullRomCalc(i, position2, position3, position4, position5);
	}

	public Vector3 CatmullRomEvaluateDirection(float position)
	{
		if (m_controlPoints == null)
		{
			return Vector3.zero;
		}
		Vector3 vector = CatmullRomEvaluateLocalPosition(position);
		return Vector3.Normalize(CatmullRomEvaluateLocalPosition(position + 0.01f) - vector);
	}

	private void CatmullRomInit()
	{
		if (m_controlPoints == null)
		{
			m_controlPoints = new List<UberCurveControlPoint>();
			for (int i = 0; i < 4; i++)
			{
				UberCurveControlPoint uberCurveControlPoint = new UberCurveControlPoint();
				uberCurveControlPoint.position = new Vector3(0f, 0f, (float)i * 4f);
				m_controlPoints.Add(uberCurveControlPoint);
			}
		}
	}

	[ContextMenu("Show Curve Steps")]
	private void ShowRenderSteps()
	{
		m_renderStepPoints = !m_renderStepPoints;
	}

	private void CatmullRomGizmo()
	{
		if (m_gizmoSteps < 1)
		{
			m_gizmoSteps = 1;
		}
		if (m_controlPoints == null)
		{
			CatmullRomInit();
		}
		if (m_controlPoints.Count < 4)
		{
			return;
		}
		m_renderingGizmo = true;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		float num = 0f;
		num = ((!m_Looping) ? (1f / (float)(m_gizmoSteps * (m_controlPoints.Count - 1))) : (1f / (float)(m_gizmoSteps * m_controlPoints.Count)));
		Gizmos.color = Color.cyan;
		Vector3 from = m_controlPoints[0].position;
		for (float num2 = 0f; num2 <= 1f; num2 += num)
		{
			Vector3 vector = CatmullRomEvaluateLocalPosition(num2);
			Gizmos.DrawLine(from, vector);
			from = vector;
		}
		if (m_renderStepPoints)
		{
			Gizmos.color = new Color(0.2f, 0.2f, 0.9f, 0.75f);
			for (float num3 = 0f; num3 <= 1f; num3 += num)
			{
				Gizmos.DrawSphere(CatmullRomEvaluateLocalPosition(num3), m_HandleSize * 0.15f);
			}
		}
		if (m_renderControlPoints)
		{
			Gizmos.color = new Color(0.3f, 0.3f, 1f, 1f);
			for (int i = 0; i < m_controlPoints.Count; i++)
			{
				Gizmos.DrawSphere(m_controlPoints[i].position, 0.25f);
			}
		}
		m_renderingGizmo = false;
	}

	private Vector3 CatmullRomCalc(float i, Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 pointD)
	{
		Vector3 vector = 0.5f * (2f * pointB);
		Vector3 vector2 = 0.5f * (pointC - pointA);
		Vector3 vector3 = 0.5f * (2f * pointA - 5f * pointB + 4f * pointC - pointD);
		Vector3 vector4 = 0.5f * (-pointA + 3f * pointB - 3f * pointC + pointD);
		return vector + vector2 * i + vector3 * i * i + vector4 * i * i * i;
	}

	private int ClampIndexCatmullRom(int pos)
	{
		if (m_Looping)
		{
			if (pos < 0)
			{
				pos = m_controlPoints.Count - 1;
			}
			if (pos > m_controlPoints.Count)
			{
				pos = 1;
			}
			else if (pos > m_controlPoints.Count - 1)
			{
				pos = 0;
			}
		}
		else
		{
			if (pos < 0)
			{
				pos = 0;
			}
			if (pos > m_controlPoints.Count - 1)
			{
				pos = m_controlPoints.Count - 1;
			}
		}
		return pos;
	}
}
