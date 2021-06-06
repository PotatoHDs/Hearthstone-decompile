public class UIHeader : ThreeSliceElement
{
	public UberText m_headerUberText;

	public void SetText(string t)
	{
		m_headerUberText.Text = t;
		SetMiddleWidth(m_headerUberText.GetTextWorldSpaceBounds().size.x);
	}
}
