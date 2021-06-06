using Hearthstone.UI;
using UnityEngine;

public class UIBScrollableItem : MonoBehaviour
{
	public delegate bool ActiveStateCallback();

	public enum ActiveState
	{
		Active,
		Inactive,
		UseHierarchy
	}

	public enum BoundsMode
	{
		Fixed,
		WidgetBoundsLocal,
		WidgetBoundsIncludeChildren
	}

	[Tooltip("Fixed: Use values for Size and Offset defined at edit-time.\n\nWidgetBoundsLocal: Match the size and position of this object's Widget bounds.\n\nWidgetBoundsIncludeChildren: Encapsulate the Widget bounds defined on this object and its children.")]
	public BoundsMode m_boundsMode;

	public Vector3 m_offset = Vector3.zero;

	public Vector3 m_size = Vector3.one;

	public ActiveState m_active;

	private ActiveStateCallback m_activeStateCallback;

	private Vector3[] m_boundsPointTempVector = new Vector3[8];

	public bool IsActive()
	{
		if (m_activeStateCallback != null)
		{
			return m_activeStateCallback();
		}
		if (m_active != 0)
		{
			if (m_active == ActiveState.UseHierarchy)
			{
				return base.gameObject.activeInHierarchy;
			}
			return false;
		}
		return true;
	}

	public void SetCustomActiveState(ActiveStateCallback callback)
	{
		m_activeStateCallback = callback;
	}

	public OrientedBounds GetOrientedBounds()
	{
		UpdateBounds();
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		OrientedBounds orientedBounds = new OrientedBounds();
		orientedBounds.Origin = base.transform.position + (Vector3)(localToWorldMatrix * m_offset);
		orientedBounds.Extents = new Vector3[3]
		{
			localToWorldMatrix * new Vector3(m_size.x * 0.5f, 0f, 0f),
			localToWorldMatrix * new Vector3(0f, m_size.y * 0.5f, 0f),
			localToWorldMatrix * new Vector3(0f, 0f, m_size.z * 0.5f)
		};
		return orientedBounds;
	}

	public void GetWorldBounds(out Vector3 min, out Vector3 max)
	{
		min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		Vector3[] boundsPoints = GetBoundsPoints();
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

	private Vector3[] GetBoundsPoints()
	{
		UpdateBounds();
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		Vector3 vector = localToWorldMatrix * new Vector3(m_size.x * 0.5f, 0f, 0f);
		Vector3 vector2 = localToWorldMatrix * new Vector3(0f, m_size.y * 0.5f, 0f);
		Vector3 vector3 = localToWorldMatrix * new Vector3(0f, 0f, m_size.z * 0.5f);
		Vector3 vector4 = base.transform.position + (Vector3)(localToWorldMatrix * m_offset);
		m_boundsPointTempVector[0] = vector4 + vector + vector2 + vector3;
		m_boundsPointTempVector[1] = vector4 + vector + vector2 - vector3;
		m_boundsPointTempVector[2] = vector4 + vector - vector2 + vector3;
		m_boundsPointTempVector[3] = vector4 + vector - vector2 - vector3;
		m_boundsPointTempVector[4] = vector4 - vector + vector2 + vector3;
		m_boundsPointTempVector[5] = vector4 - vector + vector2 - vector3;
		m_boundsPointTempVector[6] = vector4 - vector - vector2 + vector3;
		m_boundsPointTempVector[7] = vector4 - vector - vector2 - vector3;
		return m_boundsPointTempVector;
	}

	private void UpdateBounds()
	{
		if (m_boundsMode == BoundsMode.WidgetBoundsLocal)
		{
			if (GetComponent<WidgetTransform>() != null)
			{
				Bounds localBoundsOfWidgetTransform = WidgetTransform.GetLocalBoundsOfWidgetTransform(base.transform);
				m_size = localBoundsOfWidgetTransform.size;
				m_offset = localBoundsOfWidgetTransform.center;
			}
		}
		else if (m_boundsMode == BoundsMode.WidgetBoundsIncludeChildren)
		{
			Bounds boundsOfWidgetTransforms = WidgetTransform.GetBoundsOfWidgetTransforms(base.transform, base.transform.worldToLocalMatrix);
			m_size = boundsOfWidgetTransforms.size;
			m_offset = boundsOfWidgetTransforms.center;
		}
	}
}
