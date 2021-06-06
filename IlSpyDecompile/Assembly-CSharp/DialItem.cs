public class DialItem : PegUIElement
{
	public UberText m_Text;

	private object m_value;

	public object GetValue()
	{
		return m_value;
	}

	public void SetValue(object val, string text)
	{
		m_value = val;
		m_Text.Text = text;
	}
}
