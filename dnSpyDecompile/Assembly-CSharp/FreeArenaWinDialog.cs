using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BD RID: 701
public class FreeArenaWinDialog : DialogBase
{
	// Token: 0x060024D5 RID: 9429 RVA: 0x000B9609 File Offset: 0x000B7809
	protected override void Awake()
	{
		base.Awake();
		this.m_okayButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.PressOk();
		});
	}

	// Token: 0x060024D6 RID: 9430 RVA: 0x000B962A File Offset: 0x000B782A
	public void SetInfo(FreeArenaWinDialog.Info info)
	{
		this.m_info = info;
		if (this.m_info.m_callbackOnHide != null)
		{
			base.AddHideListener(this.m_info.m_callbackOnHide);
		}
	}

	// Token: 0x060024D7 RID: 9431 RVA: 0x000B9654 File Offset: 0x000B7854
	public override void Show()
	{
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		this.EnableFullScreenEffects(true);
		base.Show();
		this.m_winCount.Text = this.m_info.m_winCount.ToString();
		if (!string.IsNullOrEmpty(this.m_showAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_showAnimationSound);
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.3f,
			"easetype",
			iTween.EaseType.easeOutBack
		});
		iTween.ScaleTo(base.gameObject, args);
		UniversalInputManager.Get().SetSystemDialogActive(true);
	}

	// Token: 0x060024D8 RID: 9432 RVA: 0x000B9731 File Offset: 0x000B7931
	protected void EnableFullScreenEffects(bool enable)
	{
		if (FullScreenFXMgr.Get() == null)
		{
			return;
		}
		if (enable)
		{
			FullScreenFXMgr.Get().StartStandardBlurVignette(1f);
			return;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(1f, null);
	}

	// Token: 0x060024D9 RID: 9433 RVA: 0x000B975E File Offset: 0x000B795E
	protected override void DoHideAnimation()
	{
		if (!string.IsNullOrEmpty(this.m_hideAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_hideAnimationSound);
		}
		base.DoHideAnimation();
	}

	// Token: 0x060024DA RID: 9434 RVA: 0x000B9788 File Offset: 0x000B7988
	private void PressOk()
	{
		this.EnableFullScreenEffects(false);
		this.Hide();
	}

	// Token: 0x0400148A RID: 5258
	[CustomEditField(Sections = "Object Links")]
	public UIBButton m_okayButton;

	// Token: 0x0400148B RID: 5259
	public UberText m_okayButtonText;

	// Token: 0x0400148C RID: 5260
	public UberText m_winCount;

	// Token: 0x0400148D RID: 5261
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_showAnimationSound = "Expand_Up.prefab:775d97ea42498c044897f396362b9db3";

	// Token: 0x0400148E RID: 5262
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_hideAnimationSound = "Shrink_Down_Quicker.prefab:2fe963b171811ca4b8d544fa53e3330c";

	// Token: 0x0400148F RID: 5263
	private FreeArenaWinDialog.Info m_info;

	// Token: 0x020015CA RID: 5578
	public class Info
	{
		// Token: 0x0400AF00 RID: 44800
		public DialogBase.HideCallback m_callbackOnHide;

		// Token: 0x0400AF01 RID: 44801
		public int m_winCount;
	}
}
