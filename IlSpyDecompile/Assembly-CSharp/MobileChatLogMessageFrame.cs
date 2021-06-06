using System.Runtime.CompilerServices;
using UnityEngine;

public class MobileChatLogMessageFrame : MonoBehaviour, ITouchListItem
{
	public UberText text;

	public GameObject m_Background;

	private Bounds localBounds;

	public string Message
	{
		get
		{
			return text.Text;
		}
		set
		{
			text.Text = value;
			text.UpdateNow();
			UpdateLocalBounds();
		}
	}

	public bool IsHeader => false;

	public bool Visible
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	public Color Color
	{
		get
		{
			return text.TextColor;
		}
		set
		{
			text.TextColor = value;
		}
	}

	public float Width
	{
		get
		{
			return text.Width;
		}
		set
		{
			text.Width = value;
			if (m_Background != null)
			{
				float x = m_Background.GetComponent<MeshFilter>().mesh.bounds.size.x;
				m_Background.transform.localScale = new Vector3(value / x, m_Background.transform.localScale.y, 1f);
				m_Background.transform.localPosition = new Vector3((0f - value) / (0.5f * x), 0f, 0f);
			}
		}
	}

	public Bounds LocalBounds => localBounds;

	public new T GetComponent<T>() where T : Component
	{
		return ((Component)this).GetComponent<T>();
	}

	public void RebuildUberText()
	{
		text.UpdateNow(updateIfInactive: true);
		UpdateLocalBounds();
	}

	public void OnScrollOutOfView()
	{
	}

	private void UpdateLocalBounds()
	{
		Bounds textBounds = text.GetTextBounds();
		Vector3 size = textBounds.size;
		localBounds.center = base.transform.InverseTransformPoint(textBounds.center) + 10f * Vector3.up;
		localBounds.size = size;
	}

	[SpecialName]
	GameObject ITouchListItem.get_gameObject()
	{
		return base.gameObject;
	}

	[SpecialName]
	Transform ITouchListItem.get_transform()
	{
		return base.transform;
	}
}
