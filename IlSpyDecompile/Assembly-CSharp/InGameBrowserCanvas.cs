using System;
using UnityEngine;

public class InGameBrowserCanvas : MonoBehaviour
{
	private const int LOADING_TIME_FADE_SEC = 1;

	private const float LOADING_MIN_ALPHA = 0.3f;

	public PegUIElement m_closeButton;

	public PegUIElement m_backButton;

	public PegUIElement m_backSymbolButton;

	public UberText m_loadingText;

	public GameObject m_topLeftBone;

	public GameObject m_bottomRightBone;

	public GameObject m_inputBlockerRenderer;

	private bool m_hideAllButtons;

	private bool m_canGoBack;

	private PegUIElement m_inputBlockerPegUIElement;

	private DateTime m_loadingLastTimeFade = DateTime.Now;

	private bool m_shouldLoadingFadeIn;

	private void Awake()
	{
		OverlayUI.Get().AddGameObject(base.gameObject);
		m_closeButton.AddEventListener(UIEventType.RELEASE, OnClosePressed);
		m_backButton.AddEventListener(UIEventType.RELEASE, OnBackPressed);
		m_backSymbolButton.AddEventListener(UIEventType.RELEASE, OnBackPressed);
		Navigation.Push(OnNavigateBack);
	}

	private void Update()
	{
		if (m_loadingText.gameObject.activeSelf)
		{
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now - m_loadingLastTimeFade;
			bool shouldLoadingFadeIn = m_shouldLoadingFadeIn;
			double num;
			if (timeSpan.TotalSeconds >= 1.0)
			{
				m_loadingLastTimeFade = now;
				m_shouldLoadingFadeIn = !m_shouldLoadingFadeIn;
				num = 1.0;
			}
			else
			{
				num = timeSpan.TotalSeconds / 1.0;
			}
			num = (shouldLoadingFadeIn ? num : (1.0 - num));
			float textAlpha = (float)(num * 0.699999988079071 + 0.30000001192092896);
			m_loadingText.TextAlpha = textAlpha;
		}
	}

	private void OnDestroy()
	{
		Navigation.RemoveHandler(OnNavigateBack);
		if (m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(m_inputBlockerPegUIElement.gameObject);
			m_inputBlockerPegUIElement = null;
		}
	}

	public void WebViewDidFinishLoad(string pageState)
	{
		Log.InGameBrowser.Print("Web View Page State: " + pageState);
		if (pageState == null)
		{
			return;
		}
		string[] array = pageState.Split(new string[1] { "|" }, StringSplitOptions.None);
		if (array.Length < 2)
		{
			Log.InGameBrowser.PrintWarning($"WebViewDidFinishLoad() - Invalid parsed pageState ({pageState})");
			return;
		}
		m_canGoBack = array[array.Length - 1].Equals("canGoBack");
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < array.Length - 1; i++)
		{
			string obj = array[i];
			obj.Equals("STATE_ACCOUNT_CREATION", StringComparison.InvariantCultureIgnoreCase);
			if (obj.Equals("STATE_ACCOUNT_CREATED", StringComparison.InvariantCultureIgnoreCase))
			{
				flag = true;
			}
			if (obj.Equals("STATE_NO_BACK", StringComparison.InvariantCultureIgnoreCase))
			{
				flag2 = true;
			}
		}
		flag2 = flag2 || flag;
		if (flag)
		{
			Options.Get().SetBool(Option.CREATED_ACCOUNT, val: true);
		}
		(UniversalInputManager.UsePhoneUI ? m_backSymbolButton.gameObject : m_backButton.gameObject).SetActive(!m_hideAllButtons && !flag2 && m_canGoBack);
	}

	public void WebViewBackButtonPressed(string dummyState)
	{
		Navigation.GoBack();
	}

	public void ShowDarkenInputBlocker()
	{
		if (m_inputBlockerPegUIElement != null)
		{
			UnityEngine.Object.Destroy(m_inputBlockerPegUIElement.gameObject);
			m_inputBlockerPegUIElement = null;
		}
		GameObject gameObject = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(base.gameObject.layer), "InGameBrowserCanvasInputBlocker");
		SceneUtils.SetLayer(gameObject, base.gameObject.layer);
		m_inputBlockerPegUIElement = gameObject.AddComponent<PegUIElement>();
		TransformUtil.SetPosY(m_inputBlockerPegUIElement, base.gameObject.transform.position.y - 5f);
		DarkenInputBlocker(gameObject, 0.5f);
	}

	public void ShowLoading()
	{
		m_loadingText.gameObject.SetActive(value: true);
	}

	public void HideLoading()
	{
		m_loadingText.gameObject.SetActive(value: false);
	}

	public void HideAllButtons()
	{
		m_hideAllButtons = true;
		m_closeButton.gameObject.SetActive(value: false);
		(UniversalInputManager.UsePhoneUI ? m_backSymbolButton.gameObject : m_backButton.gameObject).SetActive(value: false);
	}

	private void OnBackPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	private void OnClosePressed(UIEvent e)
	{
		InGameBrowserManager.Get().Hide();
	}

	private bool OnNavigateBack()
	{
		if (!m_hideAllButtons)
		{
			if (!m_canGoBack)
			{
				InGameBrowserManager.Get().Hide();
				return true;
			}
			WebAuth.GoBackWebPage();
		}
		return false;
	}

	private void DarkenInputBlocker(GameObject inputBlockerObject, float alpha)
	{
		inputBlockerObject.AddComponent<MeshRenderer>().SetMaterial(m_inputBlockerRenderer.GetComponent<MeshRenderer>().GetMaterial());
		inputBlockerObject.AddComponent<MeshFilter>().SetMesh(m_inputBlockerRenderer.GetComponent<MeshFilter>().GetMesh());
		BoxCollider component = inputBlockerObject.GetComponent<BoxCollider>();
		TransformUtil.SetLocalScaleXY(inputBlockerObject, component.size.x, component.size.y);
		component.size = new Vector3(1f, 1f, 0f);
		TransformUtil.SetLocalEulerAngleX(inputBlockerObject, 90f);
		RenderUtils.SetAlpha(inputBlockerObject, alpha);
	}
}
