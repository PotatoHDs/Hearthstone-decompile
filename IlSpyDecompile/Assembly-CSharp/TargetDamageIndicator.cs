using UnityEngine;

public class TargetDamageIndicator : MonoBehaviour
{
	public UberText m_indicatorText;

	public GameObject m_targetArrowBang;

	public void SetText(string newText)
	{
		m_indicatorText.Text = newText ?? string.Empty;
	}

	public void Show(bool active)
	{
		m_indicatorText.gameObject.SetActive(active);
		m_targetArrowBang.SetActive(active);
	}
}
