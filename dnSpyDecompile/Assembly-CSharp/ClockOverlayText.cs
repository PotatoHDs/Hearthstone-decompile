using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200069D RID: 1693
public class ClockOverlayText : MonoBehaviour
{
	// Token: 0x06005E7B RID: 24187 RVA: 0x001EB2E0 File Offset: 0x001E94E0
	public void Show()
	{
		Vector3 vector = this.m_maxScale;
		if (UniversalInputManager.UsePhoneUI)
		{
			vector = this.m_maxScale_phone;
		}
		iTween.Stop(base.gameObject);
		base.gameObject.SetActive(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			vector,
			"time",
			0.4f,
			"easetype",
			iTween.EaseType.easeOutQuad
		});
		iTween.ScaleTo(base.gameObject, args);
	}

	// Token: 0x06005E7C RID: 24188 RVA: 0x001EB370 File Offset: 0x001E9570
	public void Hide()
	{
		iTween.Stop(base.gameObject);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			this.m_minScale,
			"time",
			0.15f,
			"easetype",
			iTween.EaseType.easeInQuad,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				base.gameObject.SetActive(false);
			})
		});
		iTween.ScaleTo(base.gameObject, args);
	}

	// Token: 0x06005E7D RID: 24189 RVA: 0x001EB3F3 File Offset: 0x001E95F3
	public void HideImmediate()
	{
		base.gameObject.SetActive(false);
		base.transform.localScale = this.m_minScale;
	}

	// Token: 0x06005E7E RID: 24190 RVA: 0x001EB414 File Offset: 0x001E9614
	public void UpdateText(int step)
	{
		if (step == 0)
		{
			this.m_bannerYear.SetActive(false);
			this.m_detailsText.gameObject.SetActive(false);
			this.m_bannerStandard.SetActive(true);
			this.m_detailsTextStandard.gameObject.SetActive(true);
			return;
		}
		this.m_bannerStandard.SetActive(false);
		this.m_detailsTextStandard.gameObject.SetActive(false);
		this.m_bannerYear.SetActive(true);
		this.m_detailsText.gameObject.SetActive(true);
	}

	// Token: 0x04004F8F RID: 20367
	public GameObject m_bannerStandard;

	// Token: 0x04004F90 RID: 20368
	public UberText m_detailsTextStandard;

	// Token: 0x04004F91 RID: 20369
	public GameObject m_bannerYear;

	// Token: 0x04004F92 RID: 20370
	public UberText m_detailsText;

	// Token: 0x04004F93 RID: 20371
	public Vector3 m_maxScale;

	// Token: 0x04004F94 RID: 20372
	public Vector3 m_maxScale_phone;

	// Token: 0x04004F95 RID: 20373
	private Vector3 m_minScale = new Vector3(0.01f, 0.01f, 0.01f);
}
