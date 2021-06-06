using UnityEngine;

public class FiresideGatheringOpponentPickerFooter : MonoBehaviour
{
	public UberText m_TitleText;

	public UberText m_BodyText;

	private const string m_emptyListTitleText = "GLUE_FIRESIDE_GATHERING_FOOTER_EMPTY_FIRST_LINE";

	private const string m_emptyListBodyText = "GLUE_FIRESIDE_GATHERING_FOOTER_EMPTY_SECOND_LINE";

	private const string m_partialListTitleText = "GLUE_FIRESIDE_GATHERING_FOOTER_PARTIAL_FIRST_LINE";

	private const string m_partialListBodyText = "GLUE_FIRESIDE_GATHERING_FOOTER_PARTIAL_SECOND_LINE";

	public void SetTextOnFooter(bool listEmpty)
	{
		m_TitleText.Text = (listEmpty ? GameStrings.Get("GLUE_FIRESIDE_GATHERING_FOOTER_EMPTY_FIRST_LINE") : GameStrings.Get("GLUE_FIRESIDE_GATHERING_FOOTER_PARTIAL_FIRST_LINE"));
		m_BodyText.Text = (listEmpty ? GameStrings.Get("GLUE_FIRESIDE_GATHERING_FOOTER_EMPTY_SECOND_LINE") : GameStrings.Get("GLUE_FIRESIDE_GATHERING_FOOTER_PARTIAL_SECOND_LINE"));
	}
}
