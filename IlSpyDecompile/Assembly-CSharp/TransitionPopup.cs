using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

public abstract class TransitionPopup : MonoBehaviour
{
	public delegate void MatchCanceledEvent();

	public UberText m_title;

	public MatchingQueueTab m_queueTab;

	public UIBButton m_cancelButton;

	public Vector3_MobileOverride m_startPosition = new Vector3_MobileOverride(new Vector3(-0.05f, 8.2f, -1.8f));

	public Float_MobileOverride m_endScale;

	public Float_MobileOverride m_scaleAfterPunch;

	protected bool m_shown;

	protected bool m_blockingLoadingScreen;

	protected Camera m_fullScreenEffectsCamera;

	protected List<MatchCanceledEvent> m_matchCanceledListeners = new List<MatchCanceledEvent>();

	protected AdventureDbId m_adventureId;

	protected FormatType m_formatType;

	protected GameType m_gameType;

	protected long? m_deckId;

	protected int m_scenarioId;

	protected bool m_showAnimationFinished;

	private float POPUP_TIME = 0.3f;

	private float START_SCALE_VAL = 0.1f;

	private Vector3 END_POSITION;

	private bool m_blurEnabled;

	public event Action<TransitionPopup> OnHidden;

	public void SetAdventureId(AdventureDbId adventureId)
	{
		m_adventureId = adventureId;
	}

	public void SetFormatType(FormatType formatType)
	{
		m_formatType = formatType;
	}

	public void SetGameType(GameType gameType)
	{
		m_gameType = gameType;
	}

	public void SetDeckId(long? deckId)
	{
		m_deckId = deckId;
	}

	public void SetScenarioId(int scenarioId)
	{
		m_scenarioId = scenarioId;
	}

	protected virtual void Awake()
	{
		m_fullScreenEffectsCamera = Camera.main;
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelButtonReleased);
		m_cancelButton.AddEventListener(UIEventType.ROLLOVER, OnCancelButtonOver);
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		base.gameObject.transform.localPosition = m_startPosition;
	}

	protected virtual void Start()
	{
		if (m_fullScreenEffectsCamera == null)
		{
			m_fullScreenEffectsCamera = Camera.main;
		}
		if (!m_shown)
		{
			iTween.FadeTo(base.gameObject, 0f, 0f);
			base.gameObject.SetActive(value: false);
		}
	}

	protected virtual void OnDestroy()
	{
		if (FullScreenFXMgr.Get() != null)
		{
			DisableFullScreenBlur();
		}
		StopBlockingTransition();
		if (GameMgr.Get() != null)
		{
			GameMgr.Get().UnregisterFindGameEvent(OnFindGameEvent);
		}
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoaded);
		}
		if (m_shown && this.OnHidden != null)
		{
			this.OnHidden(this);
		}
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public virtual void Show()
	{
		if (!m_shown)
		{
			AnimateShow();
		}
	}

	public virtual void Hide()
	{
		if (m_shown)
		{
			AnimateHide();
		}
	}

	public void Cancel()
	{
		if (m_shown && !(m_fullScreenEffectsCamera == null))
		{
			DisableFullScreenBlur();
		}
	}

	public void RegisterMatchCanceledEvent(MatchCanceledEvent callback)
	{
		m_matchCanceledListeners.Add(callback);
	}

	public bool UnregisterMatchCanceledEvent(MatchCanceledEvent callback)
	{
		return m_matchCanceledListeners.Remove(callback);
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (!m_shown)
		{
			return false;
		}
		switch (eventData.m_state)
		{
		case FindGameState.BNET_QUEUE_ENTERED:
			OnGameEntered(eventData);
			break;
		case FindGameState.BNET_QUEUE_DELAYED:
			OnGameDelayed(eventData);
			break;
		case FindGameState.BNET_QUEUE_UPDATED:
			OnGameUpdated(eventData);
			break;
		case FindGameState.SERVER_GAME_CONNECTING:
			OnGameConnecting(eventData);
			break;
		case FindGameState.SERVER_GAME_STARTED:
			OnGameStarted(eventData);
			break;
		}
		return false;
	}

	protected virtual void OnGameEntered(FindGameEventData eventData)
	{
		m_queueTab.UpdateDisplay(eventData.m_queueMinSeconds, eventData.m_queueMaxSeconds);
	}

	protected virtual void OnGameDelayed(FindGameEventData eventData)
	{
	}

	protected virtual void OnGameUpdated(FindGameEventData eventData)
	{
		m_queueTab.UpdateDisplay(eventData.m_queueMinSeconds, eventData.m_queueMaxSeconds);
	}

	protected virtual void OnGameConnecting(FindGameEventData eventData)
	{
		DisableCancelButton();
	}

	protected virtual void OnGameStarted(FindGameEventData eventData)
	{
		StartBlockingTransition();
		SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
	}

	protected virtual bool EnableCancelButtonIfPossible()
	{
		if (!m_showAnimationFinished)
		{
			return false;
		}
		if (GameMgr.Get().IsAboutToStopFindingGame())
		{
			return false;
		}
		if (m_cancelButton.IsEnabled())
		{
			return false;
		}
		EnableCancelButton();
		return true;
	}

	protected virtual void EnableCancelButton()
	{
		m_cancelButton.Flip(faceUp: true);
		m_cancelButton.SetEnabled(enabled: true);
	}

	protected virtual void DisableCancelButton()
	{
		m_cancelButton.Flip(faceUp: false);
		m_cancelButton.SetEnabled(enabled: false);
	}

	protected virtual void OnCancelButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Back_Click.prefab:f7df4bfeab7ccff4198e670ca516da2e");
		DisableCancelButton();
	}

	protected virtual void OnCancelButtonOver(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
	}

	protected void FireMatchCanceledEvent()
	{
		MatchCanceledEvent[] array = m_matchCanceledListeners.ToArray();
		if (array.Length == 0)
		{
			Debug.LogError("TransitionPopup.FireMatchCanceledEvent() - Cancel triggered, but nobody was listening!!");
		}
		MatchCanceledEvent[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i]();
		}
	}

	protected virtual void AnimateShow()
	{
		iTween.Stop(base.gameObject);
		m_shown = true;
		m_showAnimationFinished = false;
		base.gameObject.SetActive(value: true);
		SceneUtils.EnableRenderers(base.gameObject, enable: false);
		DisableCancelButton();
		ShowPopup();
		AnimateBlurBlendOn();
	}

	protected virtual void ShowPopup()
	{
		SceneUtils.EnableRenderers(base.gameObject, enable: true);
		iTween.FadeTo(base.gameObject, 1f, POPUP_TIME);
		base.gameObject.transform.localScale = new Vector3(START_SCALE_VAL, START_SCALE_VAL, START_SCALE_VAL);
		Hashtable args = iTween.Hash("scale", new Vector3(m_endScale, m_endScale, m_endScale), "time", POPUP_TIME, "oncomplete", "PunchPopup", "oncompletetarget", base.gameObject);
		iTween.ScaleTo(base.gameObject, args);
		iTween.MoveTo(base.gameObject, iTween.Hash("position", base.gameObject.transform.localPosition + new Vector3(0.02f, 0.02f, 0.02f), "time", 1.5f, "islocal", true));
		m_queueTab.ResetTimer();
	}

	private void PunchPopup()
	{
		iTween.ScaleTo(base.gameObject, new Vector3(m_scaleAfterPunch, m_scaleAfterPunch, m_scaleAfterPunch), 0.15f);
		OnAnimateShowFinished();
	}

	protected virtual void OnAnimateShowFinished()
	{
		m_showAnimationFinished = true;
	}

	protected virtual void AnimateHide()
	{
		m_shown = false;
		DisableCancelButton();
		iTween.FadeTo(base.gameObject, 0f, POPUP_TIME);
		Hashtable hashtable = iTween.Hash("scale", new Vector3(START_SCALE_VAL, START_SCALE_VAL, START_SCALE_VAL), "time", POPUP_TIME);
		if (this.OnHidden != null)
		{
			hashtable["oncomplete"] = (Action<object>)delegate
			{
				this.OnHidden(this);
			};
		}
		iTween.ScaleTo(base.gameObject, hashtable);
		AnimateBlurBlendOff();
	}

	private void AnimateBlurBlendOn()
	{
		EnableFullScreenBlur();
	}

	protected void AnimateBlurBlendOff()
	{
		DisableFullScreenBlur();
		StartCoroutine(DelayDeactivatePopup(0.5f));
	}

	private IEnumerator DelayDeactivatePopup(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (!m_shown)
		{
			DeactivatePopup();
		}
	}

	protected void DeactivatePopup()
	{
		base.gameObject.SetActive(value: false);
		StopBlockingTransition();
	}

	protected void StartBlockingTransition()
	{
		m_blockingLoadingScreen = true;
		LoadingScreen.Get().AddTransitionBlocker();
		LoadingScreen.Get().AddTransitionObject(base.gameObject);
	}

	protected void StopBlockingTransition()
	{
		if (m_blockingLoadingScreen)
		{
			m_blockingLoadingScreen = false;
			if ((bool)LoadingScreen.Get())
			{
				LoadingScreen.Get().NotifyTransitionBlockerComplete();
			}
		}
	}

	protected virtual void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (m_shown)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoaded);
			OnGameplaySceneLoaded();
		}
	}

	private void EnableFullScreenBlur()
	{
		if (!m_blurEnabled)
		{
			m_blurEnabled = true;
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			fullScreenFXMgr.SetBlurAmount(0.3f);
			fullScreenFXMgr.SetBlurBrightness(0.4f);
			fullScreenFXMgr.SetBlurDesaturation(0.5f);
			fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc);
		}
	}

	private void DisableFullScreenBlur()
	{
		if (m_blurEnabled)
		{
			m_blurEnabled = false;
			FullScreenFXMgr.Get().StopBlur(0.5f, iTween.EaseType.easeOutCirc, null, stopAll: true);
		}
	}

	protected abstract void OnGameplaySceneLoaded();
}
