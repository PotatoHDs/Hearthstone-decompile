using System;
using UnityEngine;

// Token: 0x02000B32 RID: 2866
public class TurnStartIndicator : MonoBehaviour
{
	// Token: 0x06009853 RID: 38995 RVA: 0x00315498 File Offset: 0x00313698
	private void Start()
	{
		iTween.FadeTo(base.gameObject, 0f, 0f);
		base.gameObject.transform.position = new Vector3(-7.8f, 8.2f, -5f);
		base.gameObject.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		base.gameObject.SetActive(false);
		this.SetReminderText("");
	}

	// Token: 0x06009854 RID: 38996 RVA: 0x000261C2 File Offset: 0x000243C2
	public bool IsShown()
	{
		return base.gameObject.activeSelf;
	}

	// Token: 0x06009855 RID: 38997 RVA: 0x00315519 File Offset: 0x00313719
	public float GetDesiredDelayDuration()
	{
		return this.m_desiredDelayDuration;
	}

	// Token: 0x06009856 RID: 38998 RVA: 0x00315524 File Offset: 0x00313724
	public virtual void Show()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			base.gameObject.transform.position = new Vector3(-7.8f, 8.2f, -4.2f);
		}
		else
		{
			base.gameObject.transform.position = new Vector3(-7.8f, 8.2f, -5f);
		}
		base.gameObject.SetActive(true);
		if (this.m_labelTop != null)
		{
			this.m_labelTop.Text = GameStrings.Get("GAMEPLAY_YOUR_TURN");
		}
		if (this.m_labelMiddle != null)
		{
			this.m_labelMiddle.Text = GameStrings.Get("GAMEPLAY_YOUR_TURN");
		}
		if (this.m_labelBottom != null)
		{
			this.m_labelBottom.Text = GameStrings.Get("GAMEPLAY_YOUR_TURN");
		}
		iTween.FadeTo(base.gameObject, 1f, 0.25f);
		base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(10f, 10f, 10f),
			"time",
			0.25f,
			"oncomplete",
			"PunchTurnStartInstance",
			"oncompletetarget",
			base.gameObject
		}));
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			base.gameObject.transform.position + new Vector3(0.02f, 0.02f, 0.02f),
			"time",
			this.m_desiredDisplayDuration,
			"oncomplete",
			"HideTurnStartInstance",
			"oncompletetarget",
			base.gameObject
		}));
		this.m_explosionFX.GetComponent<ParticleSystem>().Play();
	}

	// Token: 0x06009857 RID: 38999 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Hide()
	{
	}

	// Token: 0x06009858 RID: 39000 RVA: 0x0031573B File Offset: 0x0031393B
	private void PunchTurnStartInstance()
	{
		iTween.ScaleTo(base.gameObject, new Vector3(9.8f, 9.8f, 9.8f), 0.15f);
	}

	// Token: 0x06009859 RID: 39001 RVA: 0x00315764 File Offset: 0x00313964
	private void HideTurnStartInstance()
	{
		iTween.FadeTo(base.gameObject, 0f, 0.25f);
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"time",
			0.25f,
			"oncomplete",
			"DeactivateTurnStartInstance",
			"oncompletetarget",
			base.gameObject
		}));
	}

	// Token: 0x0600985A RID: 39002 RVA: 0x00028167 File Offset: 0x00026367
	private void DeactivateTurnStartInstance()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600985B RID: 39003 RVA: 0x003157F6 File Offset: 0x003139F6
	public void SetReminderText(string newText)
	{
		if (this.m_reminderText != null)
		{
			this.m_reminderText.Text = newText;
		}
	}

	// Token: 0x04007F5C RID: 32604
	public GameObject m_explosionFX;

	// Token: 0x04007F5D RID: 32605
	public GameObject m_godRays;

	// Token: 0x04007F5E RID: 32606
	public UberText m_labelTop;

	// Token: 0x04007F5F RID: 32607
	public UberText m_labelMiddle;

	// Token: 0x04007F60 RID: 32608
	public UberText m_labelBottom;

	// Token: 0x04007F61 RID: 32609
	public UberText m_reminderText;

	// Token: 0x04007F62 RID: 32610
	public float m_desiredDisplayDuration = 1.5f;

	// Token: 0x04007F63 RID: 32611
	public float m_desiredDelayDuration = 1f;

	// Token: 0x04007F64 RID: 32612
	private const float DISAPPEAR_SCALE_VAL = 0.01f;

	// Token: 0x04007F65 RID: 32613
	private const float START_SCALE_VAL = 1f;

	// Token: 0x04007F66 RID: 32614
	private const float AFTER_PUNCH_SCALE_VAL = 9.8f;

	// Token: 0x04007F67 RID: 32615
	private const float END_SCALE_VAL = 10f;
}
