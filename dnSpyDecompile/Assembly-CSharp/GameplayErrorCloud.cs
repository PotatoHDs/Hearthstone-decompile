using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200030D RID: 781
public class GameplayErrorCloud : MonoBehaviour
{
	// Token: 0x06002AC7 RID: 10951 RVA: 0x000D7994 File Offset: 0x000D5B94
	private void Start()
	{
		RenderUtils.SetAlpha(base.gameObject, 0f);
		this.Hide();
	}

	// Token: 0x06002AC8 RID: 10952 RVA: 0x000D79AC File Offset: 0x000D5BAC
	public void Show()
	{
		this.m_errorText.gameObject.SetActive(true);
		this.m_psystem.gameObject.SetActive(true);
		this.SetParticleEmitterLocalPosition();
	}

	// Token: 0x06002AC9 RID: 10953 RVA: 0x000D79D8 File Offset: 0x000D5BD8
	public void Hide()
	{
		iTween.Stop(base.gameObject);
		base.StopCoroutine(this.START_COROUTINE_NAME);
		this.m_coroutine = null;
		this.m_errorText.gameObject.SetActive(false);
		this.m_psystem.gameObject.SetActive(false);
	}

	// Token: 0x06002ACA RID: 10954 RVA: 0x000D7A28 File Offset: 0x000D5C28
	public void ShowMessage(string message, float timeToDisplay)
	{
		if (this.m_coroutine != null)
		{
			this.Hide();
		}
		iTween.Stop(base.gameObject);
		this.m_holdDuration = Mathf.Max(2f, timeToDisplay);
		this.m_psystem.main.startLifetime = 0.15f + this.m_holdDuration * 1.4f + 0.5f;
		this.Show();
		this.m_errorText.Text = message;
		iTween.FadeTo(base.gameObject, iTween.Hash(new object[]
		{
			"alpha",
			1f,
			"time",
			0.15f
		}));
		this.m_coroutine = base.StartCoroutine(this.START_COROUTINE_NAME);
	}

	// Token: 0x06002ACB RID: 10955 RVA: 0x000D7AF4 File Offset: 0x000D5CF4
	public void HideMessage()
	{
		iTween.FadeTo(base.gameObject, iTween.Hash(new object[]
		{
			"alpha",
			0f,
			"time",
			0.5f,
			"oncomplete",
			"Hide"
		}));
	}

	// Token: 0x06002ACC RID: 10956 RVA: 0x000D7B51 File Offset: 0x000D5D51
	public IEnumerator StartHideMessageDelay()
	{
		yield return new WaitForSeconds(0.15f + this.m_holdDuration);
		this.HideMessage();
		yield break;
	}

	// Token: 0x06002ACD RID: 10957 RVA: 0x000D7B60 File Offset: 0x000D5D60
	private void SetParticleEmitterLocalPosition()
	{
		if (CollectionManager.Get().IsInEditMode())
		{
			this.m_psystem.transform.localPosition = this.m_psystemLocalPositionInCollectionManager;
			return;
		}
		this.m_psystem.transform.localPosition = this.m_psystemLocalPositionInGame;
	}

	// Token: 0x040017F3 RID: 6131
	public UberText m_errorText;

	// Token: 0x040017F4 RID: 6132
	public float initTime;

	// Token: 0x040017F5 RID: 6133
	public ParticleSystem m_psystem;

	// Token: 0x040017F6 RID: 6134
	public Vector3_MobileOverride m_psystemLocalPositionInCollectionManager;

	// Token: 0x040017F7 RID: 6135
	public Vector3_MobileOverride m_psystemLocalPositionInGame;

	// Token: 0x040017F8 RID: 6136
	private const float ERROR_MESSAGE_DURATION = 2f;

	// Token: 0x040017F9 RID: 6137
	private const float ERROR_MESSAGE_FADEIN = 0.15f;

	// Token: 0x040017FA RID: 6138
	private const float ERROR_MESSAGE_FADEOUT = 0.5f;

	// Token: 0x040017FB RID: 6139
	private readonly string START_COROUTINE_NAME = "StartHideMessageDelay";

	// Token: 0x040017FC RID: 6140
	private float m_holdDuration;

	// Token: 0x040017FD RID: 6141
	private Coroutine m_coroutine;
}
