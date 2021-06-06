using UnityEngine;

public class Banner : MonoBehaviour
{
	public UberText m_headline;

	public UberText m_caption;

	public GameObject m_glowObject;

	public void SetText(string headline)
	{
		m_headline.Text = headline;
		m_caption.gameObject.SetActive(value: false);
	}

	public void SetText(string headline, string caption)
	{
		m_headline.Text = headline;
		m_caption.gameObject.SetActive(value: true);
		m_caption.Text = caption;
	}

	public void MoveGlowForBottomPlacement()
	{
		m_glowObject.transform.localPosition = new Vector3(m_glowObject.transform.localPosition.x, m_glowObject.transform.localPosition.y, 0f);
	}
}
