using UnityEngine;

public class RAFLinkFrame : MonoBehaviour
{
	public UIBButton m_copyButton;

	public UberText m_url;

	public HighlightState m_copyButtonHighlight;

	private PegUIElement m_inputBlockerPegUIElement;

	private bool m_isShown;

	private string m_fullUrl;

	private void Awake()
	{
		m_copyButton.AddEventListener(UIEventType.RELEASE, OnCopyButtonReleased);
		m_copyButton.AddEventListener(UIEventType.ROLLOVER, OnCopyButtonOver);
		m_copyButton.AddEventListener(UIEventType.ROLLOUT, OnCopyButtonOut);
	}

	private void OnDestroy()
	{
	}

	public void Show()
	{
		if (!m_isShown)
		{
			Navigation.Push(OnNavigateBack);
			m_isShown = true;
			base.gameObject.SetActive(value: true);
			if (m_inputBlockerPegUIElement != null)
			{
				Object.Destroy(m_inputBlockerPegUIElement.gameObject);
				m_inputBlockerPegUIElement = null;
			}
			GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "RAFLinkInputBlocker");
			SceneUtils.SetLayer(gameObject, base.gameObject.layer);
			m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
			m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, OnInputBlockerRelease);
			TransformUtil.SetPosY(m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
			RAFManager.Get().GetRAFFrame().DarkenInputBlocker(gameObject, 0.5f);
		}
	}

	public void Hide()
	{
		if (m_isShown)
		{
			Navigation.RemoveHandler(OnNavigateBack);
			if (m_inputBlockerPegUIElement != null && m_inputBlockerPegUIElement.gameObject != null)
			{
				Object.Destroy(m_inputBlockerPegUIElement.gameObject);
				m_inputBlockerPegUIElement = null;
			}
			m_isShown = false;
			if (base.gameObject != null)
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}

	public void SetURL(string displayUrl, string fullUrl)
	{
		m_url.Text = displayUrl;
		m_fullUrl = fullUrl;
	}

	private bool OnNavigateBack()
	{
		Hide();
		return true;
	}

	private void OnInputBlockerRelease(UIEvent e)
	{
		Hide();
	}

	private void OnCopyButtonReleased(UIEvent e)
	{
		ClipboardUtils.CopyToClipboard(m_fullUrl);
		UIStatus.Get().AddInfo(GameStrings.Get("GLUE_RAF_COPY_COMPLETE"));
		Hide();
	}

	private void OnCopyButtonOver(UIEvent e)
	{
		m_copyButtonHighlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_MOUSE_OVER);
	}

	private void OnCopyButtonOut(UIEvent e)
	{
		m_copyButtonHighlight.ChangeState(ActorStateType.NONE);
	}
}
