using UnityEngine;

public class RewardBanner : MonoBehaviour
{
	public UberText m_headlineText;

	public UberText m_detailsText;

	public UberText m_sourceText;

	public GameObject m_headlineCenterBone;

	private float m_headlineHeight;

	public string HeadlineText => m_headlineText.Text;

	public string DetailsText => m_detailsText.Text;

	public string SourceText => m_sourceText.Text;

	private void Awake()
	{
		if ((bool)UniversalInputManager.UsePhoneUI && m_sourceText != null)
		{
			m_sourceText.gameObject.SetActive(value: false);
		}
		m_headlineHeight = m_headlineText.Height;
	}

	public void SetText(string headline, string details, string source)
	{
		m_headlineText.Text = headline;
		m_detailsText.Text = details;
		m_sourceText.Text = source;
		if (details == "")
		{
			AlignHeadlineToCenterBone();
			m_headlineText.Height = m_headlineHeight * 1.5f;
		}
	}

	public void AlignHeadlineToCenterBone()
	{
		if (m_headlineCenterBone != null)
		{
			m_headlineText.transform.localPosition = m_headlineCenterBone.transform.localPosition;
		}
	}
}
