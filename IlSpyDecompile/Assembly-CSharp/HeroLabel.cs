using System.Collections;
using UnityEngine;

public class HeroLabel : MonoBehaviour
{
	public UberText m_nameText;

	public UberText m_classText;

	public void UpdateText(string nameText, string classText)
	{
		m_nameText.Text = nameText;
		m_classText.Text = classText;
	}

	public void SetFade(float fade)
	{
		m_nameText.TextAlpha = fade;
		m_classText.TextAlpha = fade;
	}

	public void SetColor(Color color)
	{
		m_nameText.TextColor = color;
		m_classText.TextColor = color;
	}

	public void FadeIn()
	{
		if (!(m_nameText == null) && !(m_classText == null))
		{
			iTween.Stop(m_nameText.gameObject);
			iTween.Stop(m_classText.gameObject);
			iTween.FadeTo(m_nameText.gameObject, 1f, 0.5f);
			iTween.FadeTo(m_classText.gameObject, 1f, 0.5f);
		}
	}

	public void FadeOut()
	{
		if (!(m_nameText == null) && !(m_classText == null))
		{
			iTween.Stop(m_nameText.gameObject);
			iTween.Stop(m_classText.gameObject);
			iTween.FadeTo(m_nameText.gameObject, 0f, 0.5f);
			iTween.FadeTo(m_classText.gameObject, 0f, 0.5f);
			StartCoroutine(FinishFade());
		}
	}

	private IEnumerator FinishFade()
	{
		yield return new WaitForSeconds(1f);
		Object.Destroy(base.gameObject);
	}
}
