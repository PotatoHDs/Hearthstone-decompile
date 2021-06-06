using UnityEngine;

public class RAFInfo : UIBPopup
{
	public UIBButton m_okayButton;

	public UIBButton m_cancelButton;

	public UberText m_headlineText;

	public UberText m_messageText;

	public MultiSliceElement m_allSections;

	public GameObject m_midSection;

	private PegUIElement m_inputBlockerPegUIElement;

	protected override void Awake()
	{
		base.Awake();
		m_okayButton.SetText(GameStrings.Get("GLUE_RAF_INFO_MORE_INFO_BUTTON"));
		m_cancelButton.SetText(GameStrings.Get("GLUE_RAF_INFO_GOT_IT_BUTTON"));
		m_okayButton.AddEventListener(UIEventType.RELEASE, OnOkayPressed);
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelPressed);
	}

	private void OnDestroy()
	{
		Hide(animate: false);
	}

	public override void Show()
	{
		if (!base.IsShown())
		{
			Navigation.Push(OnNavigateBack);
			base.Show(useOverlayUI: false);
			base.gameObject.SetActive(value: true);
			if (m_inputBlockerPegUIElement != null)
			{
				Object.Destroy(m_inputBlockerPegUIElement.gameObject);
				m_inputBlockerPegUIElement = null;
			}
			GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "RAFInfoInputBlocker");
			SceneUtils.SetLayer(gameObject, base.gameObject.layer);
			m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
			m_inputBlockerPegUIElement.AddEventListener(UIEventType.RELEASE, OnInputBlockerRelease);
			TransformUtil.SetPosY(m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
			RAFManager.Get().GetRAFFrame().DarkenInputBlocker(gameObject, 0.5f);
		}
	}

	protected override void Hide(bool animate)
	{
		if (base.IsShown())
		{
			Navigation.RemoveHandler(OnNavigateBack);
			m_okayButton.SetEnabled(enabled: true);
			if (m_inputBlockerPegUIElement != null)
			{
				Object.Destroy(m_inputBlockerPegUIElement.gameObject);
				m_inputBlockerPegUIElement = null;
			}
			base.gameObject.SetActive(value: false);
			base.Hide(animate);
		}
	}

	private void OnOkayPressed(UIEvent e)
	{
		RAFManager.Get().GotoRAFWebsite();
	}

	private void OnCancelPressed(UIEvent e)
	{
		Hide(animate: true);
	}

	private bool OnNavigateBack()
	{
		Hide(animate: true);
		return true;
	}

	private void OnInputBlockerRelease(UIEvent e)
	{
		Hide(animate: true);
	}
}
