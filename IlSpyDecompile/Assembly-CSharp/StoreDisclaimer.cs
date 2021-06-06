using UnityEngine;

public class StoreDisclaimer : MonoBehaviour
{
	public UberText m_headlineText;

	public UberText m_warningText;

	public UberText m_detailsText;

	public GameObject m_root;

	private void Awake()
	{
		m_headlineText.Text = GameStrings.Get("GLUE_STORE_DISCLAIMER_HEADLINE");
		m_warningText.Text = GameStrings.Get("GLUE_STORE_DISCLAIMER_WARNING");
		m_detailsText.Text = "";
	}

	public void UpdateTextSize()
	{
		m_headlineText.UpdateNow();
		m_warningText.UpdateNow();
		m_detailsText.UpdateNow();
	}

	public void SetDetailsText(string detailsText)
	{
		m_detailsText.Text = detailsText;
	}
}
