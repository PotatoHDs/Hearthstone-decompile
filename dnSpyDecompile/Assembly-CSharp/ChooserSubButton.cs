using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000B03 RID: 2819
[CustomEditClass]
public abstract class ChooserSubButton : AdventureGenericButton
{
	// Token: 0x06009622 RID: 38434 RVA: 0x00309EEC File Offset: 0x003080EC
	public void SetHighlight(bool enable)
	{
		UIBHighlightStateControl component = base.GetComponent<UIBHighlightStateControl>();
		if (component != null)
		{
			if (this.m_Glow)
			{
				component.Select(true, true);
			}
			else
			{
				component.Select(enable, false);
			}
		}
		UIBHighlight component2 = base.GetComponent<UIBHighlight>();
		if (component2 != null)
		{
			if (enable)
			{
				component2.Select();
				return;
			}
			component2.Reset();
		}
	}

	// Token: 0x06009623 RID: 38435 RVA: 0x00309F44 File Offset: 0x00308144
	public void SetNewGlow(bool enable)
	{
		this.m_Glow = enable;
		UIBHighlightStateControl component = base.GetComponent<UIBHighlightStateControl>();
		if (component != null)
		{
			component.Select(enable, true);
		}
	}

	// Token: 0x06009624 RID: 38436 RVA: 0x00309F70 File Offset: 0x00308170
	public void ShowNewModePopup(string message)
	{
		if (this.m_NewModePopupBone == null)
		{
			return;
		}
		this.m_NewModePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.m_NewModePopupBone.transform.position, this.m_NewModePopupBone.transform.localScale, message, true, NotificationManager.PopupTextType.BASIC);
		this.m_NewModePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
	}

	// Token: 0x06009625 RID: 38437 RVA: 0x00309FCC File Offset: 0x003081CC
	public void HideNewModePopupAfterDelay()
	{
		base.StartCoroutine(this.HideNewModePopupAfterDelayCoroutine());
	}

	// Token: 0x06009626 RID: 38438 RVA: 0x00309FDB File Offset: 0x003081DB
	public void Flash()
	{
		this.m_StateTable.TriggerState("Flash", true, null);
	}

	// Token: 0x06009627 RID: 38439 RVA: 0x00309FF0 File Offset: 0x003081F0
	public bool IsReady()
	{
		UIBHighlightStateControl component = base.GetComponent<UIBHighlightStateControl>();
		return component != null && component.IsReady();
	}

	// Token: 0x06009628 RID: 38440 RVA: 0x0030A015 File Offset: 0x00308215
	protected override void OnDestroy()
	{
		if (this.m_NewModePopup != null)
		{
			this.m_NewModePopup.Shrink(-1f);
		}
		base.OnDestroy();
	}

	// Token: 0x06009629 RID: 38441 RVA: 0x0030A03B File Offset: 0x0030823B
	public void OnDisable()
	{
		if (this.m_NewModePopup != null)
		{
			this.m_NewModePopup.Shrink(-1f);
		}
	}

	// Token: 0x0600962A RID: 38442 RVA: 0x0030A05B File Offset: 0x0030825B
	private IEnumerator HideNewModePopupAfterDelayCoroutine()
	{
		float timer = this.m_NewModePopupAutomaticHideTime;
		while (timer > 0f)
		{
			timer -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		if (this.m_NewModePopup != null)
		{
			this.m_NewModePopup.Shrink(-1f);
		}
		yield break;
	}

	// Token: 0x04007DDD RID: 32221
	protected const string s_EventFlash = "Flash";

	// Token: 0x04007DDE RID: 32222
	public GameObject m_NewModePopupBone;

	// Token: 0x04007DDF RID: 32223
	[CustomEditField(Sections = "Event Table")]
	public StateEventTable m_StateTable;

	// Token: 0x04007DE0 RID: 32224
	public float m_NewModePopupAutomaticHideTime = 1f;

	// Token: 0x04007DE1 RID: 32225
	protected bool m_Glow;

	// Token: 0x04007DE2 RID: 32226
	private Notification m_NewModePopup;
}
