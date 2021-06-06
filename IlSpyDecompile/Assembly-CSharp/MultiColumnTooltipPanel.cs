using System.Collections.Generic;

public class MultiColumnTooltipPanel : ResizableTooltipPanel
{
	public List<UberText> m_textColumns = new List<UberText>();

	public override void Initialize(string keywordName, string keywordText)
	{
		base.Initialize(keywordName, keywordText);
		float num = 0f;
		foreach (UberText textColumn in m_textColumns)
		{
			if (textColumn != null && textColumn.Height > num)
			{
				num = textColumn.Height;
			}
		}
		float backgroundSize = (m_name.Height + m_bodyTextHeight + num) * m_heightPadding;
		SetBackgroundSize(backgroundSize);
	}
}
