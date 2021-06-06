using System.Collections.Generic;
using UnityEngine;

public class CollectionSearch : MonoBehaviour
{
	public delegate void ActivatedListener();

	public delegate void DeactivatedListener(string oldSearchText, string newSearchText);

	public delegate void ClearedListener(bool updateVisuals);

	public UberText m_searchText;

	public PegUIElement m_background;

	public PegUIElement m_clearButton;

	public GameObject m_xMesh;

	public GameObject m_backgroundWhenAtBottom;

	public GameObject m_backgroundWhenAtBottomTavernBrawl;

	public GameObject m_backgroundWhenAtTopNormal;

	public GameObject m_backgroundWhenAtTopWild;

	public Color m_altSearchColor;

	private const float ANIM_TIME = 0.1f;

	private const int MAX_SEARCH_LENGTH = 31;

	private Material m_origSearchMaterial;

	private Vector3 m_origSearchPos;

	private bool m_isActive;

	private string m_prevText;

	private string m_text;

	private bool m_wildModeActive;

	private List<ActivatedListener> m_activatedListeners = new List<ActivatedListener>();

	private List<DeactivatedListener> m_deactivatedListeners = new List<DeactivatedListener>();

	private List<ClearedListener> m_clearedListeners = new List<ClearedListener>();

	private GameLayer m_originalLayer;

	private GameLayer m_activeLayer;

	private bool m_isTouchKeyboardDisplayMode;

	private void Start()
	{
		m_background.AddEventListener(UIEventType.RELEASE, OnBackgroundReleased);
		m_clearButton.AddEventListener(UIEventType.RELEASE, OnClearReleased);
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		touchScreenService.AddOnVirtualKeyboardShowListener(OnKeyboardShown);
		touchScreenService.AddOnVirtualKeyboardHideListener(OnKeyboardHidden);
		m_origSearchPos = base.transform.localPosition;
		UpdateBackground();
		UpdateSearchText();
	}

	private void OnDestroy()
	{
		if (HearthstoneServices.TryGet<ITouchScreenService>(out var service))
		{
			service.RemoveOnVirtualKeyboardShowListener(OnKeyboardShown);
			service.RemoveOnVirtualKeyboardHideListener(OnKeyboardHidden);
		}
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(base.gameObject);
		}
	}

	public bool IsActive()
	{
		return m_isActive;
	}

	public void SetActiveLayer(GameLayer activeLayer)
	{
		if (activeLayer != m_activeLayer)
		{
			m_activeLayer = activeLayer;
			if (IsActive())
			{
				MoveToActiveLayer(saveOriginalLayer: false);
			}
		}
	}

	public void SetWildModeActive(bool active)
	{
		m_wildModeActive = active;
	}

	public void Activate(bool ignoreTouchMode = false)
	{
		if (!m_isActive)
		{
			m_background.SetEnabled(enabled: false);
			MoveToActiveLayer(saveOriginalLayer: true);
			m_isActive = true;
			m_prevText = m_text;
			ActivatedListener[] array = m_activatedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
			ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
			if (!ignoreTouchMode && UniversalInputManager.Get().UseWindowsTouch() && touchScreenService.IsTouchSupported() && touchScreenService.IsVirtualKeyboardVisible())
			{
				TouchKeyboardSearchDisplay(fromActivate: true);
			}
			else
			{
				ShowInput();
			}
		}
	}

	public void Deactivate()
	{
		if (m_isActive)
		{
			m_background.SetEnabled(enabled: true);
			MoveToOriginalLayer();
			m_isActive = false;
			HideInput();
			ResetSearchDisplay();
			DeactivatedListener[] array = m_deactivatedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](m_prevText, m_text);
			}
		}
	}

	public void Cancel()
	{
		if (m_isActive)
		{
			m_text = m_prevText;
			UpdateSearchText();
			Deactivate();
		}
	}

	public string GetText()
	{
		return m_text;
	}

	public void SetText(string text)
	{
		m_text = text;
		UpdateSearchText();
	}

	public void ClearFilter(bool updateVisuals = true)
	{
		m_text = "";
		UpdateSearchText();
		ClearInput();
		ClearedListener[] array = m_clearedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](updateVisuals);
		}
		ITouchScreenService touchScreenService = HearthstoneServices.Get<ITouchScreenService>();
		if ((UniversalInputManager.Get().IsTouchMode() && touchScreenService.IsTouchSupported()) || touchScreenService.IsVirtualKeyboardVisible())
		{
			Deactivate();
		}
	}

	public void RegisterActivatedListener(ActivatedListener listener)
	{
		if (!m_activatedListeners.Contains(listener))
		{
			m_activatedListeners.Add(listener);
		}
	}

	public void RemoveActivatedListener(ActivatedListener listener)
	{
		m_activatedListeners.Remove(listener);
	}

	public void RegisterDeactivatedListener(DeactivatedListener listener)
	{
		if (!m_deactivatedListeners.Contains(listener))
		{
			m_deactivatedListeners.Add(listener);
		}
	}

	public void RemoveDeactivatedListener(DeactivatedListener listener)
	{
		m_deactivatedListeners.Remove(listener);
	}

	public void RegisterClearedListener(ClearedListener listener)
	{
		if (!m_clearedListeners.Contains(listener))
		{
			m_clearedListeners.Add(listener);
		}
	}

	public void RemoveClearedListener(ClearedListener listener)
	{
		m_clearedListeners.Remove(listener);
	}

	public void SetEnabled(bool enabled)
	{
		m_background.SetEnabled(enabled);
		m_clearButton.SetEnabled(enabled);
	}

	private void OnBackgroundReleased(UIEvent e)
	{
		Activate();
	}

	private void OnClearReleased(UIEvent e)
	{
		ClearFilter();
	}

	private void OnActivateAnimComplete()
	{
		ShowInput();
	}

	private void OnDeactivateAnimComplete()
	{
		DeactivatedListener[] array = m_deactivatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](m_prevText, m_text);
		}
	}

	private void ShowInput(bool fromActivate = true)
	{
		Bounds bounds = m_searchText.GetBounds();
		m_searchText.gameObject.SetActive(value: false);
		Rect rect = CameraUtils.CreateGUIViewportRect(Box.Get().GetCamera(), bounds.min, bounds.max);
		Color? color = null;
		if (HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible())
		{
			color = m_altSearchColor;
		}
		UniversalInputManager.TextInputParams textInputParams = new UniversalInputManager.TextInputParams
		{
			m_owner = base.gameObject,
			m_rect = rect,
			m_updatedCallback = OnInputUpdated,
			m_completedCallback = OnInputComplete,
			m_canceledCallback = OnInputCanceled,
			m_unfocusedCallback = OnInputUnfocus,
			m_font = m_searchText.GetLocalizedFont(),
			m_text = m_text,
			m_color = color,
			m_touchScreenKeyboardHideInput = false
		};
		textInputParams.m_showVirtualKeyboard = fromActivate;
		UniversalInputManager.Get().UseTextInput(textInputParams);
	}

	private void HideInput()
	{
		UniversalInputManager.Get().CancelTextInput(base.gameObject);
		m_searchText.gameObject.SetActive(value: true);
	}

	private void ClearInput()
	{
		if (m_isActive)
		{
			SoundManager.Get().LoadAndPlay("text_box_delete_text.prefab:b4209934f760cc745b3dba5add912398");
			UniversalInputManager.Get().SetInputText("");
		}
	}

	private void OnInputUpdated(string input)
	{
		m_text = input;
		UpdateSearchText();
	}

	private void OnInputComplete(string input)
	{
		m_text = input;
		UpdateSearchText();
		SoundManager.Get().LoadAndPlay("text_commit.prefab:05a794ae046d3e842b87893629a826f1");
		Deactivate();
	}

	private void OnInputCanceled(bool userRequested, GameObject requester)
	{
		Cancel();
	}

	private void OnInputUnfocus()
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			Deactivate();
		}
	}

	private void UpdateSearchText()
	{
		if (string.IsNullOrEmpty(m_text))
		{
			m_searchText.Text = GameStrings.Get("GLUE_COLLECTION_SEARCH");
			m_clearButton.gameObject.SetActive(value: false);
		}
		else
		{
			m_searchText.Text = m_text;
			m_clearButton.gameObject.SetActive(value: true);
		}
	}

	private void MoveToActiveLayer(bool saveOriginalLayer)
	{
		if (saveOriginalLayer)
		{
			m_originalLayer = (GameLayer)base.gameObject.layer;
		}
		SceneUtils.SetLayer(base.gameObject, m_activeLayer);
	}

	private void MoveToOriginalLayer()
	{
		SceneUtils.SetLayer(base.gameObject, m_originalLayer);
	}

	private void TouchKeyboardSearchDisplay(bool fromActivate = false)
	{
		if (!m_isTouchKeyboardDisplayMode)
		{
			m_isTouchKeyboardDisplayMode = true;
			base.transform.localPosition = CollectionManager.Get().GetCollectibleDisplay().m_activeSearchBone_Win8.transform.localPosition;
			HideInput();
			ShowInput(fromActivate || HearthstoneServices.Get<ITouchScreenService>().IsVirtualKeyboardVisible());
			m_xMesh.GetComponent<Renderer>().GetMaterial().SetColor("_Color", m_altSearchColor);
			UpdateBackground();
		}
	}

	private void ResetSearchDisplay()
	{
		if (m_isTouchKeyboardDisplayMode)
		{
			m_isTouchKeyboardDisplayMode = false;
			base.transform.localPosition = m_origSearchPos;
			HideInput();
			ShowInput(fromActivate: false);
			m_xMesh.GetComponent<Renderer>().GetMaterial().SetColor("_Color", Color.white);
			UpdateBackground();
		}
	}

	private void OnKeyboardShown()
	{
		if (m_isActive && !m_isTouchKeyboardDisplayMode)
		{
			TouchKeyboardSearchDisplay();
		}
	}

	private void OnKeyboardHidden()
	{
		if (m_isActive && m_isTouchKeyboardDisplayMode)
		{
			ResetSearchDisplay();
		}
	}

	private void UpdateBackground()
	{
		bool flag = m_backgroundWhenAtTopNormal != null || m_backgroundWhenAtTopWild != null;
		GameObject gameObject = ((SceneMgr.Get().IsInTavernBrawlMode() && m_backgroundWhenAtBottomTavernBrawl != null) ? m_backgroundWhenAtBottomTavernBrawl : m_backgroundWhenAtBottom);
		if (gameObject != null)
		{
			gameObject.gameObject.SetActive(!m_isTouchKeyboardDisplayMode || !flag);
		}
		if (m_backgroundWhenAtTopNormal != null)
		{
			m_backgroundWhenAtTopNormal.gameObject.SetActive(m_isTouchKeyboardDisplayMode && (m_backgroundWhenAtTopWild == null || !m_wildModeActive));
		}
		if (m_backgroundWhenAtTopWild != null)
		{
			m_backgroundWhenAtTopWild.gameObject.SetActive(m_isTouchKeyboardDisplayMode && m_wildModeActive);
		}
	}
}
