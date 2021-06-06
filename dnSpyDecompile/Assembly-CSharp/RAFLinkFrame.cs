using System;
using UnityEngine;

// Token: 0x0200063E RID: 1598
public class RAFLinkFrame : MonoBehaviour
{
	// Token: 0x06005A0D RID: 23053 RVA: 0x001D678C File Offset: 0x001D498C
	private void Awake()
	{
		this.m_copyButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCopyButtonReleased));
		this.m_copyButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnCopyButtonOver));
		this.m_copyButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnCopyButtonOut));
	}

	// Token: 0x06005A0E RID: 23054 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnDestroy()
	{
	}

	// Token: 0x06005A0F RID: 23055 RVA: 0x001D67E4 File Offset: 0x001D49E4
	public void Show()
	{
		if (this.m_isShown)
		{
			return;
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		this.m_isShown = true;
		base.gameObject.SetActive(true);
		if (this.m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "RAFLinkInputBlocker");
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
		this.m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputBlockerRelease));
		TransformUtil.SetPosY(this.m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
		RAFManager.Get().GetRAFFrame().DarkenInputBlocker(gameObject, 0.5f);
	}

	// Token: 0x06005A10 RID: 23056 RVA: 0x001D68D8 File Offset: 0x001D4AD8
	public void Hide()
	{
		if (!this.m_isShown)
		{
			return;
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		if (this.m_inputBlockerPegUIElement != null && this.m_inputBlockerPegUIElement.gameObject != null)
		{
			UnityEngine.Object.Destroy(this.m_inputBlockerPegUIElement.gameObject);
			this.m_inputBlockerPegUIElement = null;
		}
		this.m_isShown = false;
		if (base.gameObject != null)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06005A11 RID: 23057 RVA: 0x001D6959 File Offset: 0x001D4B59
	public void SetURL(string displayUrl, string fullUrl)
	{
		this.m_url.Text = displayUrl;
		this.m_fullUrl = fullUrl;
	}

	// Token: 0x06005A12 RID: 23058 RVA: 0x001D696E File Offset: 0x001D4B6E
	private bool OnNavigateBack()
	{
		this.Hide();
		return true;
	}

	// Token: 0x06005A13 RID: 23059 RVA: 0x001D6977 File Offset: 0x001D4B77
	private void OnInputBlockerRelease(UIEvent e)
	{
		this.Hide();
	}

	// Token: 0x06005A14 RID: 23060 RVA: 0x001D697F File Offset: 0x001D4B7F
	private void OnCopyButtonReleased(UIEvent e)
	{
		ClipboardUtils.CopyToClipboard(this.m_fullUrl);
		UIStatus.Get().AddInfo(GameStrings.Get("GLUE_RAF_COPY_COMPLETE"));
		this.Hide();
	}

	// Token: 0x06005A15 RID: 23061 RVA: 0x001D69A6 File Offset: 0x001D4BA6
	private void OnCopyButtonOver(UIEvent e)
	{
		this.m_copyButtonHighlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_MOUSE_OVER);
	}

	// Token: 0x06005A16 RID: 23062 RVA: 0x001D69B6 File Offset: 0x001D4BB6
	private void OnCopyButtonOut(UIEvent e)
	{
		this.m_copyButtonHighlight.ChangeState(ActorStateType.NONE);
	}

	// Token: 0x04004D18 RID: 19736
	public UIBButton m_copyButton;

	// Token: 0x04004D19 RID: 19737
	public UberText m_url;

	// Token: 0x04004D1A RID: 19738
	public HighlightState m_copyButtonHighlight;

	// Token: 0x04004D1B RID: 19739
	private PegUIElement m_inputBlockerPegUIElement;

	// Token: 0x04004D1C RID: 19740
	private bool m_isShown;

	// Token: 0x04004D1D RID: 19741
	private string m_fullUrl;
}
