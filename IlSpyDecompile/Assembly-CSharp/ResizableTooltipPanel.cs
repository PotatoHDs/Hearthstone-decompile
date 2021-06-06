using UnityEngine;

public class ResizableTooltipPanel : TooltipPanel
{
	protected float m_heightPadding = 1f;

	protected float m_bodyTextHeight;

	protected float m_bodyPadding = 0.1f;

	public override void Initialize(string keywordName, string keywordText)
	{
		if (m_background.GetComponent<NewThreeSliceElement>() == null)
		{
			Error.AddDevFatal("Prefab expecting m_background to have a NewThreeSliceElement!");
		}
		base.Initialize(keywordName, keywordText);
		m_bodyTextHeight = m_body.GetTextBounds().size.y;
		if (keywordText == "")
		{
			m_bodyTextHeight = 0f;
		}
		if (m_initialBackgroundHeight == 0f || m_initialBackgroundScale == Vector3.zero)
		{
			m_initialBackgroundHeight = m_background.GetComponent<NewThreeSliceElement>().m_middle.GetComponent<Renderer>().bounds.size.z;
			m_initialBackgroundScale = m_background.GetComponent<NewThreeSliceElement>().m_middle.transform.localScale;
		}
		float backgroundSize = ((keywordName.Length != 0) ? ((m_name.Height + m_bodyTextHeight) * m_heightPadding) : ((m_bodyTextHeight + m_bodyPadding) * m_heightPadding));
		SetBackgroundSize(backgroundSize);
	}

	protected void SetBackgroundSize(float totalHeight)
	{
		m_background.GetComponent<NewThreeSliceElement>().SetSize(new Vector3(m_initialBackgroundScale.x, m_initialBackgroundScale.y * totalHeight / m_initialBackgroundHeight, m_initialBackgroundScale.z));
	}

	public override float GetHeight()
	{
		return m_background.GetComponent<NewThreeSliceElement>().m_leftOrTop.GetComponent<Renderer>().bounds.size.z + m_background.GetComponent<NewThreeSliceElement>().m_middle.GetComponent<Renderer>().bounds.size.z + m_background.GetComponent<NewThreeSliceElement>().m_rightOrBottom.GetComponent<Renderer>().bounds.size.z;
	}

	public override float GetWidth()
	{
		return m_background.GetComponent<NewThreeSliceElement>().m_leftOrTop.GetComponent<Renderer>().bounds.size.x;
	}
}
