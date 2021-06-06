using UnityEngine;

public class ThreeSliceTextElement : MonoBehaviour
{
	public UberText m_text;

	public ThreeSliceElement m_threeSlice;

	public void SetText(string text)
	{
		m_text.Text = text;
		m_text.UpdateNow();
		Resize();
	}

	public void Resize()
	{
		m_threeSlice.SetMiddleWidth(GetTextWidth());
	}

	private float GetTextWidth()
	{
		return m_text.GetTextBounds().size.x;
	}
}
