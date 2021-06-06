using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008DC RID: 2268
public class HeroLabel : MonoBehaviour
{
	// Token: 0x06007DAA RID: 32170 RVA: 0x0028C33A File Offset: 0x0028A53A
	public void UpdateText(string nameText, string classText)
	{
		this.m_nameText.Text = nameText;
		this.m_classText.Text = classText;
	}

	// Token: 0x06007DAB RID: 32171 RVA: 0x0028C354 File Offset: 0x0028A554
	public void SetFade(float fade)
	{
		this.m_nameText.TextAlpha = fade;
		this.m_classText.TextAlpha = fade;
	}

	// Token: 0x06007DAC RID: 32172 RVA: 0x0028C36E File Offset: 0x0028A56E
	public void SetColor(Color color)
	{
		this.m_nameText.TextColor = color;
		this.m_classText.TextColor = color;
	}

	// Token: 0x06007DAD RID: 32173 RVA: 0x0028C388 File Offset: 0x0028A588
	public void FadeIn()
	{
		if (this.m_nameText == null)
		{
			return;
		}
		if (this.m_classText == null)
		{
			return;
		}
		iTween.Stop(this.m_nameText.gameObject);
		iTween.Stop(this.m_classText.gameObject);
		iTween.FadeTo(this.m_nameText.gameObject, 1f, 0.5f);
		iTween.FadeTo(this.m_classText.gameObject, 1f, 0.5f);
	}

	// Token: 0x06007DAE RID: 32174 RVA: 0x0028C408 File Offset: 0x0028A608
	public void FadeOut()
	{
		if (this.m_nameText == null)
		{
			return;
		}
		if (this.m_classText == null)
		{
			return;
		}
		iTween.Stop(this.m_nameText.gameObject);
		iTween.Stop(this.m_classText.gameObject);
		iTween.FadeTo(this.m_nameText.gameObject, 0f, 0.5f);
		iTween.FadeTo(this.m_classText.gameObject, 0f, 0.5f);
		base.StartCoroutine(this.FinishFade());
	}

	// Token: 0x06007DAF RID: 32175 RVA: 0x0028C494 File Offset: 0x0028A694
	private IEnumerator FinishFade()
	{
		yield return new WaitForSeconds(1f);
		UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x040065D2 RID: 26066
	public UberText m_nameText;

	// Token: 0x040065D3 RID: 26067
	public UberText m_classText;
}
