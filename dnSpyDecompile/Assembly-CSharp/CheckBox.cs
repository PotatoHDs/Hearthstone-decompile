using System;
using UnityEngine;

// Token: 0x02000ACD RID: 2765
[CustomEditClass]
public class CheckBox : PegUIElement
{
	// Token: 0x06009378 RID: 37752 RVA: 0x002FD057 File Offset: 0x002FB257
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.SetState(PegUIElement.InteractionState.Over);
	}

	// Token: 0x06009379 RID: 37753 RVA: 0x002FD06E File Offset: 0x002FB26E
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.SetState(PegUIElement.InteractionState.Up);
	}

	// Token: 0x0600937A RID: 37754 RVA: 0x002FD085 File Offset: 0x002FB285
	protected override void OnPress()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.SetState(PegUIElement.InteractionState.Down);
	}

	// Token: 0x0600937B RID: 37755 RVA: 0x002FD09C File Offset: 0x002FB29C
	protected override void OnRelease()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.ToggleChecked();
		if (this.m_checked && !string.IsNullOrEmpty(this.m_checkOnSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_checkOnSound);
		}
		else if (!this.m_checked && !string.IsNullOrEmpty(this.m_checkOffSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_checkOffSound);
		}
		this.SetState(PegUIElement.InteractionState.Over);
	}

	// Token: 0x0600937C RID: 37756 RVA: 0x002FD11B File Offset: 0x002FB31B
	public void SetButtonText(string s)
	{
		if (this.m_text != null)
		{
			this.m_text.text = s;
		}
		if (this.m_uberText != null)
		{
			this.m_uberText.Text = s;
		}
	}

	// Token: 0x0600937D RID: 37757 RVA: 0x002FD151 File Offset: 0x002FB351
	public void SetButtonID(int id)
	{
		this.m_buttonID = id;
	}

	// Token: 0x0600937E RID: 37758 RVA: 0x002FD15A File Offset: 0x002FB35A
	public int GetButtonID()
	{
		return this.m_buttonID;
	}

	// Token: 0x0600937F RID: 37759 RVA: 0x002FD162 File Offset: 0x002FB362
	public void SetState(PegUIElement.InteractionState state)
	{
		this.SetEnabled(true, false);
		switch (state)
		{
		default:
			return;
		}
	}

	// Token: 0x06009380 RID: 37760 RVA: 0x002FD184 File Offset: 0x002FB384
	public virtual void SetChecked(bool isChecked)
	{
		this.m_checked = isChecked;
		if (this.m_check != null)
		{
			this.m_check.SetActive(this.m_checked);
		}
	}

	// Token: 0x06009381 RID: 37761 RVA: 0x002FD1AC File Offset: 0x002FB3AC
	public bool IsChecked()
	{
		return this.m_checked;
	}

	// Token: 0x06009382 RID: 37762 RVA: 0x002FD1B4 File Offset: 0x002FB3B4
	private bool ToggleChecked()
	{
		this.SetChecked(!this.m_checked);
		return this.m_checked;
	}

	// Token: 0x04007B8B RID: 31627
	public GameObject m_check;

	// Token: 0x04007B8C RID: 31628
	public TextMesh m_text;

	// Token: 0x04007B8D RID: 31629
	public UberText m_uberText;

	// Token: 0x04007B8E RID: 31630
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_checkOnSound;

	// Token: 0x04007B8F RID: 31631
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_checkOffSound;

	// Token: 0x04007B90 RID: 31632
	private bool m_checked;

	// Token: 0x04007B91 RID: 31633
	private int m_buttonID;
}
