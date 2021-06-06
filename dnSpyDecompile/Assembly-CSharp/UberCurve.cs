using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AA3 RID: 2723
public class UberCurve : MonoBehaviour
{
	// Token: 0x06009122 RID: 37154 RVA: 0x002F1196 File Offset: 0x002EF396
	private void Awake()
	{
		this.CatmullRomInit();
	}

	// Token: 0x06009123 RID: 37155 RVA: 0x002F119E File Offset: 0x002EF39E
	private void OnDrawGizmos()
	{
		this.CatmullRomGizmo();
	}

	// Token: 0x06009124 RID: 37156 RVA: 0x002F11A8 File Offset: 0x002EF3A8
	public Vector3 CatmullRomEvaluateWorldPosition(float position)
	{
		Vector3 position2 = this.CatmullRomEvaluateLocalPosition(position);
		return base.transform.TransformPoint(position2);
	}

	// Token: 0x06009125 RID: 37157 RVA: 0x002F11CC File Offset: 0x002EF3CC
	public Vector3 CatmullRomEvaluateLocalPosition(float position)
	{
		if (this.m_controlPoints == null)
		{
			return Vector3.zero;
		}
		int num = this.m_controlPoints.Count;
		if (!this.m_Looping)
		{
			num = this.m_controlPoints.Count - 1;
		}
		if (this.m_Reverse && !this.m_renderingGizmo)
		{
			position = 1f - position;
		}
		position = Mathf.Clamp01(position);
		int num2 = Mathf.FloorToInt(position * (float)num);
		float i = position * (float)num - (float)num2;
		Vector3 position2 = this.m_controlPoints[this.ClampIndexCatmullRom(num2 - 1)].position;
		Vector3 position3 = this.m_controlPoints[num2].position;
		Vector3 position4 = this.m_controlPoints[this.ClampIndexCatmullRom(num2 + 1)].position;
		Vector3 position5 = this.m_controlPoints[this.ClampIndexCatmullRom(num2 + 2)].position;
		return this.CatmullRomCalc(i, position2, position3, position4, position5);
	}

	// Token: 0x06009126 RID: 37158 RVA: 0x002F12B0 File Offset: 0x002EF4B0
	public Vector3 CatmullRomEvaluateDirection(float position)
	{
		if (this.m_controlPoints == null)
		{
			return Vector3.zero;
		}
		Vector3 b = this.CatmullRomEvaluateLocalPosition(position);
		return Vector3.Normalize(this.CatmullRomEvaluateLocalPosition(position + 0.01f) - b);
	}

	// Token: 0x06009127 RID: 37159 RVA: 0x002F12EC File Offset: 0x002EF4EC
	private void CatmullRomInit()
	{
		if (this.m_controlPoints != null)
		{
			return;
		}
		this.m_controlPoints = new List<UberCurve.UberCurveControlPoint>();
		for (int i = 0; i < 4; i++)
		{
			UberCurve.UberCurveControlPoint uberCurveControlPoint = new UberCurve.UberCurveControlPoint();
			uberCurveControlPoint.position = new Vector3(0f, 0f, (float)i * 4f);
			this.m_controlPoints.Add(uberCurveControlPoint);
		}
	}

	// Token: 0x06009128 RID: 37160 RVA: 0x002F1348 File Offset: 0x002EF548
	[ContextMenu("Show Curve Steps")]
	private void ShowRenderSteps()
	{
		this.m_renderStepPoints = !this.m_renderStepPoints;
	}

	// Token: 0x06009129 RID: 37161 RVA: 0x002F135C File Offset: 0x002EF55C
	private void CatmullRomGizmo()
	{
		if (this.m_gizmoSteps < 1)
		{
			this.m_gizmoSteps = 1;
		}
		if (this.m_controlPoints == null)
		{
			this.CatmullRomInit();
		}
		if (this.m_controlPoints.Count < 4)
		{
			return;
		}
		this.m_renderingGizmo = true;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		float num;
		if (this.m_Looping)
		{
			num = 1f / (float)(this.m_gizmoSteps * this.m_controlPoints.Count);
		}
		else
		{
			num = 1f / (float)(this.m_gizmoSteps * (this.m_controlPoints.Count - 1));
		}
		Gizmos.color = Color.cyan;
		Vector3 from = this.m_controlPoints[0].position;
		for (float num2 = 0f; num2 <= 1f; num2 += num)
		{
			Vector3 vector = this.CatmullRomEvaluateLocalPosition(num2);
			Gizmos.DrawLine(from, vector);
			from = vector;
		}
		if (this.m_renderStepPoints)
		{
			Gizmos.color = new Color(0.2f, 0.2f, 0.9f, 0.75f);
			for (float num3 = 0f; num3 <= 1f; num3 += num)
			{
				Gizmos.DrawSphere(this.CatmullRomEvaluateLocalPosition(num3), this.m_HandleSize * 0.15f);
			}
		}
		if (this.m_renderControlPoints)
		{
			Gizmos.color = new Color(0.3f, 0.3f, 1f, 1f);
			for (int i = 0; i < this.m_controlPoints.Count; i++)
			{
				Gizmos.DrawSphere(this.m_controlPoints[i].position, 0.25f);
			}
		}
		this.m_renderingGizmo = false;
	}

	// Token: 0x0600912A RID: 37162 RVA: 0x002F14F0 File Offset: 0x002EF6F0
	private Vector3 CatmullRomCalc(float i, Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 pointD)
	{
		Vector3 a = 0.5f * (2f * pointB);
		Vector3 a2 = 0.5f * (pointC - pointA);
		Vector3 a3 = 0.5f * (2f * pointA - 5f * pointB + 4f * pointC - pointD);
		Vector3 a4 = 0.5f * (-pointA + 3f * pointB - 3f * pointC + pointD);
		return a + a2 * i + a3 * i * i + a4 * i * i * i;
	}

	// Token: 0x0600912B RID: 37163 RVA: 0x002F15D4 File Offset: 0x002EF7D4
	private int ClampIndexCatmullRom(int pos)
	{
		if (this.m_Looping)
		{
			if (pos < 0)
			{
				pos = this.m_controlPoints.Count - 1;
			}
			if (pos > this.m_controlPoints.Count)
			{
				pos = 1;
			}
			else if (pos > this.m_controlPoints.Count - 1)
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
			if (pos > this.m_controlPoints.Count - 1)
			{
				pos = this.m_controlPoints.Count - 1;
			}
		}
		return pos;
	}

	// Token: 0x040079F7 RID: 31223
	public bool m_Looping;

	// Token: 0x040079F8 RID: 31224
	public bool m_Reverse;

	// Token: 0x040079F9 RID: 31225
	public float m_HandleSize = 0.3f;

	// Token: 0x040079FA RID: 31226
	public List<UberCurve.UberCurveControlPoint> m_controlPoints;

	// Token: 0x040079FB RID: 31227
	private int m_gizmoSteps = 10;

	// Token: 0x040079FC RID: 31228
	private bool m_renderControlPoints;

	// Token: 0x040079FD RID: 31229
	private bool m_renderStepPoints;

	// Token: 0x040079FE RID: 31230
	private bool m_renderingGizmo;

	// Token: 0x020026E1 RID: 9953
	[Serializable]
	public class UberCurveControlPoint
	{
		// Token: 0x0400F26D RID: 62061
		public Vector3 position = Vector3.zero;
	}
}
