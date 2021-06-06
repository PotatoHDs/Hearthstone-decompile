using Hearthstone.UI;
using UnityEngine;

public class ShopBrowserElement : MonoBehaviour
{
	public enum Side
	{
		TOP,
		BOTTOM,
		LEFT,
		RIGHT
	}

	public Rect Bounds;

	protected bool m_isElementEnabled = true;

	public bool m_previewBounds;

	public bool m_previewOutline;

	public Color m_boundsColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	public float Thickness = 0.1f;

	[Overridable]
	public float BoundsX
	{
		get
		{
			return Bounds.x;
		}
		set
		{
			Bounds.x = value;
			OnElementBoundsChanged();
		}
	}

	[Overridable]
	public float BoundsY
	{
		get
		{
			return Bounds.y;
		}
		set
		{
			Bounds.y = value;
			OnElementBoundsChanged();
		}
	}

	[Overridable]
	public float Width
	{
		get
		{
			return Bounds.width;
		}
		set
		{
			Bounds.width = value;
			OnElementBoundsChanged();
		}
	}

	[Overridable]
	public float Height
	{
		get
		{
			return Bounds.height;
		}
		set
		{
			Bounds.height = value;
			OnElementBoundsChanged();
		}
	}

	public float Top
	{
		get
		{
			return GetDisplayTransform().localPosition.z + Bounds.yMax;
		}
		set
		{
			Transform displayTransform = GetDisplayTransform();
			Vector3 localPosition = displayTransform.localPosition;
			displayTransform.localPosition = new Vector3(localPosition.x, localPosition.y, value - Bounds.yMax);
		}
	}

	public float Left
	{
		get
		{
			return GetDisplayTransform().localPosition.x + Bounds.xMin;
		}
		set
		{
			Transform displayTransform = GetDisplayTransform();
			Vector3 localPosition = displayTransform.localPosition;
			displayTransform.localPosition = new Vector3(value - Bounds.xMin, localPosition.y, localPosition.z);
		}
	}

	public float Right
	{
		get
		{
			return GetDisplayTransform().localPosition.x + Bounds.xMax;
		}
		set
		{
			Transform displayTransform = GetDisplayTransform();
			Vector3 localPosition = displayTransform.localPosition;
			displayTransform.localPosition = new Vector3(value - Bounds.xMax, localPosition.y, localPosition.z);
		}
	}

	public float Bottom
	{
		get
		{
			return GetDisplayTransform().localPosition.z + Bounds.yMin;
		}
		set
		{
			Transform displayTransform = GetDisplayTransform();
			Vector3 localPosition = displayTransform.localPosition;
			displayTransform.localPosition = new Vector3(localPosition.x, value - Bounds.yMin, localPosition.z);
		}
	}

	[Overridable]
	public bool IsElementEnabled
	{
		get
		{
			return m_isElementEnabled;
		}
		set
		{
			m_isElementEnabled = value;
			OnElementEnabled();
		}
	}

	protected virtual void OnElementBoundsChanged()
	{
	}

	protected virtual void OnElementEnabled()
	{
	}

	protected Transform GetDisplayTransform()
	{
		if (!(GetComponent<WidgetTemplate>() != null))
		{
			return base.transform;
		}
		return base.transform.parent;
	}

	public static int ComparePosition(ShopBrowserElement A, ShopBrowserElement B)
	{
		if (Mathf.Approximately(A.Top, B.Top))
		{
			if (Mathf.Approximately(A.Left, B.Left))
			{
				return 0;
			}
			if (!(A.Left < B.Left))
			{
				return 1;
			}
			return -1;
		}
		if (!(A.Top > B.Top))
		{
			return 1;
		}
		return -1;
	}

	private void OnDrawGizmosSelected()
	{
		if (base.isActiveAndEnabled)
		{
			DrawRegion(m_boundsColor);
		}
	}

	private void OnDrawGizmos()
	{
		if (base.isActiveAndEnabled || m_previewBounds)
		{
			DrawRegion(m_boundsColor);
		}
	}

	protected void DrawRegion(Color color, float padding = 0f)
	{
		Vector3 size = new Vector3(Width, Thickness, Height);
		size.x += padding;
		size.z += padding;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = color;
		if (m_previewOutline)
		{
			Gizmos.DrawWireCube(new Vector3(Bounds.center.x, 0f, Bounds.center.y), size);
		}
		else
		{
			Gizmos.DrawCube(new Vector3(Bounds.center.x, 0f, Bounds.center.y), size);
		}
	}
}
