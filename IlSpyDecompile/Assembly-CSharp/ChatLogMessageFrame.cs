using UnityEngine;

public class ChatLogMessageFrame : MonoBehaviour
{
	public GameObject m_Background;

	public UberText m_Text;

	private float m_initialPadding;

	private float m_initialBackgroundHeight;

	private float m_initialBackgroundLocalScaleY;

	private void Awake()
	{
		Bounds bounds = m_Background.GetComponent<Collider>().bounds;
		Bounds textWorldSpaceBounds = m_Text.GetTextWorldSpaceBounds();
		m_initialPadding = bounds.size.y - textWorldSpaceBounds.size.y;
		m_initialBackgroundHeight = bounds.size.y;
		m_initialBackgroundLocalScaleY = m_Background.transform.localScale.y;
	}

	public string GetMessage()
	{
		return m_Text.Text;
	}

	public void SetMessage(string message)
	{
		m_Text.Text = message;
		UpdateText();
		UpdateBackground();
	}

	public Color GetColor()
	{
		return m_Text.TextColor;
	}

	public void SetColor(Color color)
	{
		m_Text.TextColor = color;
	}

	private void UpdateText()
	{
		m_Text.UpdateNow();
	}

	private void UpdateBackground()
	{
		float num = m_Text.GetTextWorldSpaceBounds().size.y + m_initialPadding;
		float num2 = m_initialBackgroundLocalScaleY;
		if (num > m_initialBackgroundHeight)
		{
			num2 *= num / m_initialBackgroundHeight;
		}
		TransformUtil.SetLocalScaleY(m_Background, num2);
	}
}
