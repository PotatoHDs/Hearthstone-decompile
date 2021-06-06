using System;
using UnityEngine;

// Token: 0x0200062F RID: 1583
public class ClassProgressBar : PegUIElement
{
	// Token: 0x060058F0 RID: 22768 RVA: 0x001D0329 File Offset: 0x001CE529
	protected override void Awake()
	{
		base.Awake();
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnProgressBarOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnProgressBarOut));
	}

	// Token: 0x060058F1 RID: 22769 RVA: 0x001D0359 File Offset: 0x001CE559
	public void Init()
	{
		if (this.m_classNameText != null)
		{
			this.m_classNameText.Text = GameStrings.GetClassName(this.m_class);
		}
	}

	// Token: 0x060058F2 RID: 22770 RVA: 0x001D0380 File Offset: 0x001CE580
	public void SetTooltipText(string title, string desc, string currentLevel)
	{
		if (this.m_tooltipRoot != null)
		{
			this.m_tooltipAvailable = true;
			this.m_tooltipRoot.GetComponent<TooltipPanel>().Initialize(title, desc);
			this.m_tooltipLevelText.Text = currentLevel.ToString();
			Transform[] componentsInChildren = this.m_tooltipRoot.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = base.gameObject.layer;
			}
			this.m_tooltipRoot.SetActive(false);
		}
	}

	// Token: 0x060058F3 RID: 22771 RVA: 0x001D0404 File Offset: 0x001CE604
	public void SetPremium(bool isPremium)
	{
		if (!isPremium)
		{
			base.GetComponent<Renderer>().GetMaterial().SetTexture("_MaskTex", null);
		}
	}

	// Token: 0x060058F4 RID: 22772 RVA: 0x001D041F File Offset: 0x001CE61F
	public void Lock()
	{
		this.m_locked = true;
		this.m_classLockedGO.SetActive(true);
		this.m_levelFrame.SetActive(false);
	}

	// Token: 0x060058F5 RID: 22773 RVA: 0x001D0440 File Offset: 0x001CE640
	private void OnProgressBarOver(UIEvent e)
	{
		if (this.m_locked)
		{
			return;
		}
		if (this.m_tooltipAvailable)
		{
			this.m_tooltipRoot.SetActive(true);
		}
	}

	// Token: 0x060058F6 RID: 22774 RVA: 0x001D045F File Offset: 0x001CE65F
	private void OnProgressBarOut(UIEvent e)
	{
		if (this.m_locked)
		{
			return;
		}
		this.m_tooltipRoot.SetActive(false);
	}

	// Token: 0x04004C1D RID: 19485
	public TAG_CLASS m_class;

	// Token: 0x04004C1E RID: 19486
	public UberText m_classNameText;

	// Token: 0x04004C1F RID: 19487
	public UberText m_levelText;

	// Token: 0x04004C20 RID: 19488
	public GameObject m_classLockedGO;

	// Token: 0x04004C21 RID: 19489
	public ProgressBar m_progressBar;

	// Token: 0x04004C22 RID: 19490
	public GameObject m_classIcon;

	// Token: 0x04004C23 RID: 19491
	public GameObject m_levelFrame;

	// Token: 0x04004C24 RID: 19492
	public GameObject m_tooltipRoot;

	// Token: 0x04004C25 RID: 19493
	public UberText m_tooltipTitle;

	// Token: 0x04004C26 RID: 19494
	public UberText m_tooltipDesc;

	// Token: 0x04004C27 RID: 19495
	public UberText m_tooltipLevelText;

	// Token: 0x04004C28 RID: 19496
	private string m_rewardText;

	// Token: 0x04004C29 RID: 19497
	private bool m_tooltipAvailable;

	// Token: 0x04004C2A RID: 19498
	private bool m_locked;
}
