using System;
using UnityEngine;

// Token: 0x0200063D RID: 1597
public class RAFInfo : UIBPopup
{
	// Token: 0x06005A04 RID: 23044 RVA: 0x001D659C File Offset: 0x001D479C
	protected override void Awake()
	{
		base.Awake();
		this.m_okayButton.SetText(GameStrings.Get("GLUE_RAF_INFO_MORE_INFO_BUTTON"));
		this.m_cancelButton.SetText(GameStrings.Get("GLUE_RAF_INFO_GOT_IT_BUTTON"));
		this.m_okayButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnOkayPressed));
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelPressed));
	}

	// Token: 0x06005A05 RID: 23045 RVA: 0x001D660B File Offset: 0x001D480B
	private void OnDestroy()
	{
		this.Hide(false);
	}

	// Token: 0x06005A06 RID: 23046 RVA: 0x001D6614 File Offset: 0x001D4814
	public override void Show()
	{
		if (base.IsShown())
		{
			return;
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		base.Show(false);
		base.gameObject.SetActive(true);
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "RAFInfoInputBlocker");
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
		this.m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputBlockerRelease));
		TransformUtil.SetPosY(this.m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
		RAFManager.Get().GetRAFFrame().DarkenInputBlocker(gameObject, 0.5f);
	}

	// Token: 0x06005A07 RID: 23047 RVA: 0x001D6708 File Offset: 0x001D4908
	protected override void Hide(bool animate)
	{
		if (!base.IsShown())
		{
			return;
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		this.m_okayButton.SetEnabled(true, false);
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		base.gameObject.SetActive(false);
		base.Hide(animate);
	}

	// Token: 0x06005A08 RID: 23048 RVA: 0x001D6775 File Offset: 0x001D4975
	private void OnOkayPressed(UIEvent e)
	{
		RAFManager.Get().GotoRAFWebsite();
	}

	// Token: 0x06005A09 RID: 23049 RVA: 0x001D6080 File Offset: 0x001D4280
	private void OnCancelPressed(UIEvent e)
	{
		this.Hide(true);
	}

	// Token: 0x06005A0A RID: 23050 RVA: 0x001D6076 File Offset: 0x001D4276
	private bool OnNavigateBack()
	{
		this.Hide(true);
		return true;
	}

	// Token: 0x06005A0B RID: 23051 RVA: 0x001D6080 File Offset: 0x001D4280
	private void OnInputBlockerRelease(UIEvent e)
	{
		this.Hide(true);
	}

	// Token: 0x04004D11 RID: 19729
	public UIBButton m_okayButton;

	// Token: 0x04004D12 RID: 19730
	public UIBButton m_cancelButton;

	// Token: 0x04004D13 RID: 19731
	public UberText m_headlineText;

	// Token: 0x04004D14 RID: 19732
	public UberText m_messageText;

	// Token: 0x04004D15 RID: 19733
	public MultiSliceElement m_allSections;

	// Token: 0x04004D16 RID: 19734
	public GameObject m_midSection;

	// Token: 0x04004D17 RID: 19735
	private PegUIElement m_inputBlockerPegUIElement;
}
