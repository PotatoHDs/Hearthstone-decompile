using UnityEngine;

[CustomEditClass]
public class NormalButton : PegUIElement
{
	[CustomEditField(Sections = "Button Properties")]
	public GameObject m_button;

	[CustomEditField(Sections = "Button Properties")]
	public TextMesh m_buttonText;

	[CustomEditField(Sections = "Button Properties")]
	public UberText m_buttonUberText;

	[CustomEditField(Sections = "Mouse Over Settings")]
	public GameObject m_mouseOverBone;

	[CustomEditField(Sections = "Mouse Over Settings")]
	public float m_userOverYOffset = -0.05f;

	private Vector3 m_originalButtonPosition;

	private int buttonID;

	protected override void Awake()
	{
		SetOriginalButtonPosition();
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (m_mouseOverBone != null)
		{
			m_button.transform.position = m_mouseOverBone.transform.position;
		}
		else
		{
			TransformUtil.SetLocalPosY(m_button.gameObject, m_originalButtonPosition.y + m_userOverYOffset);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		m_button.gameObject.transform.localPosition = m_originalButtonPosition;
	}

	public void SetUserOverYOffset(float userOverYOffset)
	{
		m_userOverYOffset = userOverYOffset;
	}

	public void SetButtonID(int newID)
	{
		buttonID = newID;
	}

	public int GetButtonID()
	{
		return buttonID;
	}

	public void SetText(string t)
	{
		if (m_buttonUberText == null)
		{
			m_buttonText.text = t;
		}
		else
		{
			m_buttonUberText.Text = t;
		}
	}

	public float GetTextWidth()
	{
		if (m_buttonUberText == null)
		{
			return m_buttonText.GetComponent<Renderer>().bounds.extents.x * 2f;
		}
		return m_buttonUberText.Width;
	}

	public float GetTextHeight()
	{
		if (m_buttonUberText == null)
		{
			return m_buttonText.GetComponent<Renderer>().bounds.extents.y * 2f;
		}
		return m_buttonUberText.Height;
	}

	public float GetRight()
	{
		return GetComponent<BoxCollider>().bounds.max.x;
	}

	public float GetLeft()
	{
		Bounds bounds = GetComponent<BoxCollider>().bounds;
		return bounds.center.x - bounds.extents.x;
	}

	public float GetTop()
	{
		Bounds bounds = GetComponent<BoxCollider>().bounds;
		return bounds.center.y + bounds.extents.y;
	}

	public float GetBottom()
	{
		Bounds bounds = GetComponent<BoxCollider>().bounds;
		return bounds.center.y - bounds.extents.y;
	}

	public void SetOriginalButtonPosition()
	{
		m_originalButtonPosition = m_button.transform.localPosition;
	}

	public GameObject GetButtonTextGO()
	{
		if (m_buttonUberText == null)
		{
			return m_buttonText.gameObject;
		}
		return m_buttonUberText.gameObject;
	}

	public UberText GetButtonUberText()
	{
		return m_buttonUberText;
	}

	public string GetText()
	{
		if (m_buttonUberText == null)
		{
			return m_buttonText.text;
		}
		return m_buttonUberText.Text;
	}
}
