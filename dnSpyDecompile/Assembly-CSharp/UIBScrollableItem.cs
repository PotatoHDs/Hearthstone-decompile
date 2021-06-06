using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000AF7 RID: 2807
public class UIBScrollableItem : MonoBehaviour
{
	// Token: 0x0600959C RID: 38300 RVA: 0x003073A4 File Offset: 0x003055A4
	public bool IsActive()
	{
		if (this.m_activeStateCallback != null)
		{
			return this.m_activeStateCallback();
		}
		return this.m_active == UIBScrollableItem.ActiveState.Active || (this.m_active == UIBScrollableItem.ActiveState.UseHierarchy && base.gameObject.activeInHierarchy);
	}

	// Token: 0x0600959D RID: 38301 RVA: 0x003073DA File Offset: 0x003055DA
	public void SetCustomActiveState(UIBScrollableItem.ActiveStateCallback callback)
	{
		this.m_activeStateCallback = callback;
	}

	// Token: 0x0600959E RID: 38302 RVA: 0x003073E4 File Offset: 0x003055E4
	public OrientedBounds GetOrientedBounds()
	{
		this.UpdateBounds();
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		return new OrientedBounds
		{
			Origin = base.transform.position + localToWorldMatrix * this.m_offset,
			Extents = new Vector3[]
			{
				localToWorldMatrix * new Vector3(this.m_size.x * 0.5f, 0f, 0f),
				localToWorldMatrix * new Vector3(0f, this.m_size.y * 0.5f, 0f),
				localToWorldMatrix * new Vector3(0f, 0f, this.m_size.z * 0.5f)
			}
		};
	}

	// Token: 0x0600959F RID: 38303 RVA: 0x003074E8 File Offset: 0x003056E8
	public void GetWorldBounds(out Vector3 min, out Vector3 max)
	{
		min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		Vector3[] boundsPoints = this.GetBoundsPoints();
		for (int i = 0; i < 8; i++)
		{
			min.x = Mathf.Min(boundsPoints[i].x, min.x);
			min.y = Mathf.Min(boundsPoints[i].y, min.y);
			min.z = Mathf.Min(boundsPoints[i].z, min.z);
			max.x = Mathf.Max(boundsPoints[i].x, max.x);
			max.y = Mathf.Max(boundsPoints[i].y, max.y);
			max.z = Mathf.Max(boundsPoints[i].z, max.z);
		}
	}

	// Token: 0x060095A0 RID: 38304 RVA: 0x003075F0 File Offset: 0x003057F0
	private Vector3[] GetBoundsPoints()
	{
		this.UpdateBounds();
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		Vector3 b = localToWorldMatrix * new Vector3(this.m_size.x * 0.5f, 0f, 0f);
		Vector3 b2 = localToWorldMatrix * new Vector3(0f, this.m_size.y * 0.5f, 0f);
		Vector3 b3 = localToWorldMatrix * new Vector3(0f, 0f, this.m_size.z * 0.5f);
		Vector3 a = base.transform.position + localToWorldMatrix * this.m_offset;
		this.m_boundsPointTempVector[0] = a + b + b2 + b3;
		this.m_boundsPointTempVector[1] = a + b + b2 - b3;
		this.m_boundsPointTempVector[2] = a + b - b2 + b3;
		this.m_boundsPointTempVector[3] = a + b - b2 - b3;
		this.m_boundsPointTempVector[4] = a - b + b2 + b3;
		this.m_boundsPointTempVector[5] = a - b + b2 - b3;
		this.m_boundsPointTempVector[6] = a - b - b2 + b3;
		this.m_boundsPointTempVector[7] = a - b - b2 - b3;
		return this.m_boundsPointTempVector;
	}

	// Token: 0x060095A1 RID: 38305 RVA: 0x003077D0 File Offset: 0x003059D0
	private void UpdateBounds()
	{
		if (this.m_boundsMode == UIBScrollableItem.BoundsMode.WidgetBoundsLocal)
		{
			if (base.GetComponent<WidgetTransform>() != null)
			{
				Bounds localBoundsOfWidgetTransform = WidgetTransform.GetLocalBoundsOfWidgetTransform(base.transform);
				this.m_size = localBoundsOfWidgetTransform.size;
				this.m_offset = localBoundsOfWidgetTransform.center;
				return;
			}
		}
		else if (this.m_boundsMode == UIBScrollableItem.BoundsMode.WidgetBoundsIncludeChildren)
		{
			Bounds boundsOfWidgetTransforms = WidgetTransform.GetBoundsOfWidgetTransforms(base.transform, base.transform.worldToLocalMatrix);
			this.m_size = boundsOfWidgetTransforms.size;
			this.m_offset = boundsOfWidgetTransforms.center;
		}
	}

	// Token: 0x04007D5E RID: 32094
	[Tooltip("Fixed: Use values for Size and Offset defined at edit-time.\n\nWidgetBoundsLocal: Match the size and position of this object's Widget bounds.\n\nWidgetBoundsIncludeChildren: Encapsulate the Widget bounds defined on this object and its children.")]
	public UIBScrollableItem.BoundsMode m_boundsMode;

	// Token: 0x04007D5F RID: 32095
	public Vector3 m_offset = Vector3.zero;

	// Token: 0x04007D60 RID: 32096
	public Vector3 m_size = Vector3.one;

	// Token: 0x04007D61 RID: 32097
	public UIBScrollableItem.ActiveState m_active;

	// Token: 0x04007D62 RID: 32098
	private UIBScrollableItem.ActiveStateCallback m_activeStateCallback;

	// Token: 0x04007D63 RID: 32099
	private Vector3[] m_boundsPointTempVector = new Vector3[8];

	// Token: 0x02002741 RID: 10049
	// (Invoke) Token: 0x06013955 RID: 80213
	public delegate bool ActiveStateCallback();

	// Token: 0x02002742 RID: 10050
	public enum ActiveState
	{
		// Token: 0x0400F3C1 RID: 62401
		Active,
		// Token: 0x0400F3C2 RID: 62402
		Inactive,
		// Token: 0x0400F3C3 RID: 62403
		UseHierarchy
	}

	// Token: 0x02002743 RID: 10051
	public enum BoundsMode
	{
		// Token: 0x0400F3C5 RID: 62405
		Fixed,
		// Token: 0x0400F3C6 RID: 62406
		WidgetBoundsLocal,
		// Token: 0x0400F3C7 RID: 62407
		WidgetBoundsIncludeChildren
	}
}
