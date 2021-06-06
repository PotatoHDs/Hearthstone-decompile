using System;
using PegasusShared;
using UnityEngine;

// Token: 0x02000640 RID: 1600
public class RAFRecruitBar : PegUIElement
{
	// Token: 0x06005A2F RID: 23087 RVA: 0x001D71CF File Offset: 0x001D53CF
	private void Start()
	{
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnBarOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnBarOut));
	}

	// Token: 0x06005A30 RID: 23088 RVA: 0x001D71F9 File Offset: 0x001D53F9
	public void SetGameAccountId(BnetId gameAccountId)
	{
		this.m_gameAccountId = gameAccountId;
	}

	// Token: 0x06005A31 RID: 23089 RVA: 0x001D7202 File Offset: 0x001D5402
	public BnetId GetGameAccountId()
	{
		return this.m_gameAccountId;
	}

	// Token: 0x06005A32 RID: 23090 RVA: 0x001D720A File Offset: 0x001D540A
	public void SetBattleTag(string battleTag)
	{
		this.m_battleTag.Text = battleTag;
	}

	// Token: 0x06005A33 RID: 23091 RVA: 0x001D7218 File Offset: 0x001D5418
	public void SetLevel(int level)
	{
		this.m_level.Text = level.ToString();
		if (level >= 20)
		{
			this.m_progressBarRenderer.SetMaterial(this.m_progressCompleteMaterialContainer.m_material);
			this.m_progressBar.SetMaterial(this.m_progressBarRenderer.GetMaterial());
		}
		this.m_progressBar.SetProgressBar((float)level / 20f);
	}

	// Token: 0x06005A34 RID: 23092 RVA: 0x001D727C File Offset: 0x001D547C
	public void SetLocked(bool isLocked)
	{
		this.m_isLocked = isLocked;
		if (this.m_isLocked)
		{
			this.m_progressBar.gameObject.SetActive(false);
			this.SetBattleTag(GameStrings.Format("GLUE_RAF_PROGRESS_FRAME_LOCKED_BAR_NAME", Array.Empty<object>()));
			return;
		}
		this.m_progressBar.gameObject.SetActive(true);
	}

	// Token: 0x06005A35 RID: 23093 RVA: 0x001D72D0 File Offset: 0x001D54D0
	private void OnBarOver(UIEvent e)
	{
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component == null)
		{
			return;
		}
		component.ShowLayerTooltip(GameStrings.Get("GLUE_RAF_RECRUIT_BAR_TOOLTIP_HEADLINE"), GameStrings.Get("GLUE_RAF_RECRUIT_BAR_TOOLTIP_DESC"), 0);
	}

	// Token: 0x06005A36 RID: 23094 RVA: 0x001D730C File Offset: 0x001D550C
	private void OnBarOut(UIEvent e)
	{
		TooltipZone component = base.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	// Token: 0x04004D28 RID: 19752
	public UberText m_battleTag;

	// Token: 0x04004D29 RID: 19753
	public UberText m_level;

	// Token: 0x04004D2A RID: 19754
	public ProgressBar m_progressBar;

	// Token: 0x04004D2B RID: 19755
	public MeshRenderer m_progressBarRenderer;

	// Token: 0x04004D2C RID: 19756
	public MaterialContainer m_progressCompleteMaterialContainer;

	// Token: 0x04004D2D RID: 19757
	private BnetId m_gameAccountId;

	// Token: 0x04004D2E RID: 19758
	private bool m_isLocked = true;
}
