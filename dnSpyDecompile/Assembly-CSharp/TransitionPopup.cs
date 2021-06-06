using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x02000B31 RID: 2865
public abstract class TransitionPopup : MonoBehaviour
{
	// Token: 0x1400009B RID: 155
	// (add) Token: 0x06009826 RID: 38950 RVA: 0x00314C80 File Offset: 0x00312E80
	// (remove) Token: 0x06009827 RID: 38951 RVA: 0x00314CB8 File Offset: 0x00312EB8
	public event Action<TransitionPopup> OnHidden;

	// Token: 0x06009828 RID: 38952 RVA: 0x00314CED File Offset: 0x00312EED
	public void SetAdventureId(AdventureDbId adventureId)
	{
		this.m_adventureId = adventureId;
	}

	// Token: 0x06009829 RID: 38953 RVA: 0x00314CF6 File Offset: 0x00312EF6
	public void SetFormatType(FormatType formatType)
	{
		this.m_formatType = formatType;
	}

	// Token: 0x0600982A RID: 38954 RVA: 0x00314CFF File Offset: 0x00312EFF
	public void SetGameType(GameType gameType)
	{
		this.m_gameType = gameType;
	}

	// Token: 0x0600982B RID: 38955 RVA: 0x00314D08 File Offset: 0x00312F08
	public void SetDeckId(long? deckId)
	{
		this.m_deckId = deckId;
	}

	// Token: 0x0600982C RID: 38956 RVA: 0x00314D11 File Offset: 0x00312F11
	public void SetScenarioId(int scenarioId)
	{
		this.m_scenarioId = scenarioId;
	}

	// Token: 0x0600982D RID: 38957 RVA: 0x00314D1C File Offset: 0x00312F1C
	protected virtual void Awake()
	{
		this.m_fullScreenEffectsCamera = Camera.main;
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelButtonReleased));
		this.m_cancelButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnCancelButtonOver));
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		base.gameObject.transform.localPosition = this.m_startPosition;
	}

	// Token: 0x0600982E RID: 38958 RVA: 0x00314D9C File Offset: 0x00312F9C
	protected virtual void Start()
	{
		if (this.m_fullScreenEffectsCamera == null)
		{
			this.m_fullScreenEffectsCamera = Camera.main;
		}
		if (!this.m_shown)
		{
			iTween.FadeTo(base.gameObject, 0f, 0f);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600982F RID: 38959 RVA: 0x00314DEC File Offset: 0x00312FEC
	protected virtual void OnDestroy()
	{
		if (FullScreenFXMgr.Get() != null)
		{
			this.DisableFullScreenBlur();
		}
		this.StopBlockingTransition();
		if (GameMgr.Get() != null)
		{
			GameMgr.Get().UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		}
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		}
		if (this.m_shown && this.OnHidden != null)
		{
			this.OnHidden(this);
		}
	}

	// Token: 0x06009830 RID: 38960 RVA: 0x00314E65 File Offset: 0x00313065
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x06009831 RID: 38961 RVA: 0x00314E6D File Offset: 0x0031306D
	public virtual void Show()
	{
		if (this.m_shown)
		{
			return;
		}
		this.AnimateShow();
	}

	// Token: 0x06009832 RID: 38962 RVA: 0x00314E7E File Offset: 0x0031307E
	public virtual void Hide()
	{
		if (!this.m_shown)
		{
			return;
		}
		this.AnimateHide();
	}

	// Token: 0x06009833 RID: 38963 RVA: 0x00314E8F File Offset: 0x0031308F
	public void Cancel()
	{
		if (!this.m_shown)
		{
			return;
		}
		if (this.m_fullScreenEffectsCamera == null)
		{
			return;
		}
		this.DisableFullScreenBlur();
	}

	// Token: 0x06009834 RID: 38964 RVA: 0x00314EAF File Offset: 0x003130AF
	public void RegisterMatchCanceledEvent(TransitionPopup.MatchCanceledEvent callback)
	{
		this.m_matchCanceledListeners.Add(callback);
	}

	// Token: 0x06009835 RID: 38965 RVA: 0x00314EBD File Offset: 0x003130BD
	public bool UnregisterMatchCanceledEvent(TransitionPopup.MatchCanceledEvent callback)
	{
		return this.m_matchCanceledListeners.Remove(callback);
	}

	// Token: 0x06009836 RID: 38966 RVA: 0x00314ECC File Offset: 0x003130CC
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (!this.m_shown)
		{
			return false;
		}
		switch (eventData.m_state)
		{
		case FindGameState.BNET_QUEUE_ENTERED:
			this.OnGameEntered(eventData);
			break;
		case FindGameState.BNET_QUEUE_DELAYED:
			this.OnGameDelayed(eventData);
			break;
		case FindGameState.BNET_QUEUE_UPDATED:
			this.OnGameUpdated(eventData);
			break;
		case FindGameState.SERVER_GAME_CONNECTING:
			this.OnGameConnecting(eventData);
			break;
		case FindGameState.SERVER_GAME_STARTED:
			this.OnGameStarted(eventData);
			break;
		}
		return false;
	}

	// Token: 0x06009837 RID: 38967 RVA: 0x00314F3C File Offset: 0x0031313C
	protected virtual void OnGameEntered(FindGameEventData eventData)
	{
		this.m_queueTab.UpdateDisplay(eventData.m_queueMinSeconds, eventData.m_queueMaxSeconds);
	}

	// Token: 0x06009838 RID: 38968 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnGameDelayed(FindGameEventData eventData)
	{
	}

	// Token: 0x06009839 RID: 38969 RVA: 0x00314F3C File Offset: 0x0031313C
	protected virtual void OnGameUpdated(FindGameEventData eventData)
	{
		this.m_queueTab.UpdateDisplay(eventData.m_queueMinSeconds, eventData.m_queueMaxSeconds);
	}

	// Token: 0x0600983A RID: 38970 RVA: 0x00314F55 File Offset: 0x00313155
	protected virtual void OnGameConnecting(FindGameEventData eventData)
	{
		this.DisableCancelButton();
	}

	// Token: 0x0600983B RID: 38971 RVA: 0x00314F5D File Offset: 0x0031315D
	protected virtual void OnGameStarted(FindGameEventData eventData)
	{
		this.StartBlockingTransition();
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
	}

	// Token: 0x0600983C RID: 38972 RVA: 0x00314F7C File Offset: 0x0031317C
	protected virtual bool EnableCancelButtonIfPossible()
	{
		if (!this.m_showAnimationFinished)
		{
			return false;
		}
		if (GameMgr.Get().IsAboutToStopFindingGame())
		{
			return false;
		}
		if (this.m_cancelButton.IsEnabled())
		{
			return false;
		}
		this.EnableCancelButton();
		return true;
	}

	// Token: 0x0600983D RID: 38973 RVA: 0x00314FAC File Offset: 0x003131AC
	protected virtual void EnableCancelButton()
	{
		this.m_cancelButton.Flip(true, false);
		this.m_cancelButton.SetEnabled(true, false);
	}

	// Token: 0x0600983E RID: 38974 RVA: 0x00314FC8 File Offset: 0x003131C8
	protected virtual void DisableCancelButton()
	{
		this.m_cancelButton.Flip(false, false);
		this.m_cancelButton.SetEnabled(false, false);
	}

	// Token: 0x0600983F RID: 38975 RVA: 0x00314FE4 File Offset: 0x003131E4
	protected virtual void OnCancelButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Back_Click.prefab:f7df4bfeab7ccff4198e670ca516da2e");
		this.DisableCancelButton();
	}

	// Token: 0x06009840 RID: 38976 RVA: 0x00315000 File Offset: 0x00313200
	protected virtual void OnCancelButtonOver(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
	}

	// Token: 0x06009841 RID: 38977 RVA: 0x00315018 File Offset: 0x00313218
	protected void FireMatchCanceledEvent()
	{
		TransitionPopup.MatchCanceledEvent[] array = this.m_matchCanceledListeners.ToArray();
		if (array.Length == 0)
		{
			Debug.LogError("TransitionPopup.FireMatchCanceledEvent() - Cancel triggered, but nobody was listening!!");
		}
		TransitionPopup.MatchCanceledEvent[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i]();
		}
	}

	// Token: 0x06009842 RID: 38978 RVA: 0x00315058 File Offset: 0x00313258
	protected virtual void AnimateShow()
	{
		iTween.Stop(base.gameObject);
		this.m_shown = true;
		this.m_showAnimationFinished = false;
		base.gameObject.SetActive(true);
		SceneUtils.EnableRenderers(base.gameObject, false);
		this.DisableCancelButton();
		this.ShowPopup();
		this.AnimateBlurBlendOn();
	}

	// Token: 0x06009843 RID: 38979 RVA: 0x003150A8 File Offset: 0x003132A8
	protected virtual void ShowPopup()
	{
		SceneUtils.EnableRenderers(base.gameObject, true);
		iTween.FadeTo(base.gameObject, 1f, this.POPUP_TIME);
		base.gameObject.transform.localScale = new Vector3(this.START_SCALE_VAL, this.START_SCALE_VAL, this.START_SCALE_VAL);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(this.m_endScale, this.m_endScale, this.m_endScale),
			"time",
			this.POPUP_TIME,
			"oncomplete",
			"PunchPopup",
			"oncompletetarget",
			base.gameObject
		});
		iTween.ScaleTo(base.gameObject, args);
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			base.gameObject.transform.localPosition + new Vector3(0.02f, 0.02f, 0.02f),
			"time",
			1.5f,
			"islocal",
			true
		}));
		this.m_queueTab.ResetTimer();
	}

	// Token: 0x06009844 RID: 38980 RVA: 0x00315203 File Offset: 0x00313403
	private void PunchPopup()
	{
		iTween.ScaleTo(base.gameObject, new Vector3(this.m_scaleAfterPunch, this.m_scaleAfterPunch, this.m_scaleAfterPunch), 0.15f);
		this.OnAnimateShowFinished();
	}

	// Token: 0x06009845 RID: 38981 RVA: 0x00315241 File Offset: 0x00313441
	protected virtual void OnAnimateShowFinished()
	{
		this.m_showAnimationFinished = true;
	}

	// Token: 0x06009846 RID: 38982 RVA: 0x0031524C File Offset: 0x0031344C
	protected virtual void AnimateHide()
	{
		this.m_shown = false;
		this.DisableCancelButton();
		iTween.FadeTo(base.gameObject, 0f, this.POPUP_TIME);
		Hashtable hashtable = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(this.START_SCALE_VAL, this.START_SCALE_VAL, this.START_SCALE_VAL),
			"time",
			this.POPUP_TIME
		});
		if (this.OnHidden != null)
		{
			hashtable["oncomplete"] = new Action<object>(delegate(object data)
			{
				this.OnHidden(this);
			});
		}
		iTween.ScaleTo(base.gameObject, hashtable);
		this.AnimateBlurBlendOff();
	}

	// Token: 0x06009847 RID: 38983 RVA: 0x003152F6 File Offset: 0x003134F6
	private void AnimateBlurBlendOn()
	{
		this.EnableFullScreenBlur();
	}

	// Token: 0x06009848 RID: 38984 RVA: 0x003152FE File Offset: 0x003134FE
	protected void AnimateBlurBlendOff()
	{
		this.DisableFullScreenBlur();
		base.StartCoroutine(this.DelayDeactivatePopup(0.5f));
	}

	// Token: 0x06009849 RID: 38985 RVA: 0x00315318 File Offset: 0x00313518
	private IEnumerator DelayDeactivatePopup(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (this.m_shown)
		{
			yield break;
		}
		this.DeactivatePopup();
		yield break;
	}

	// Token: 0x0600984A RID: 38986 RVA: 0x0031532E File Offset: 0x0031352E
	protected void DeactivatePopup()
	{
		base.gameObject.SetActive(false);
		this.StopBlockingTransition();
	}

	// Token: 0x0600984B RID: 38987 RVA: 0x00315342 File Offset: 0x00313542
	protected void StartBlockingTransition()
	{
		this.m_blockingLoadingScreen = true;
		LoadingScreen.Get().AddTransitionBlocker();
		LoadingScreen.Get().AddTransitionObject(base.gameObject);
	}

	// Token: 0x0600984C RID: 38988 RVA: 0x00315365 File Offset: 0x00313565
	protected void StopBlockingTransition()
	{
		if (!this.m_blockingLoadingScreen)
		{
			return;
		}
		this.m_blockingLoadingScreen = false;
		if (LoadingScreen.Get())
		{
			LoadingScreen.Get().NotifyTransitionBlockerComplete();
		}
	}

	// Token: 0x0600984D RID: 38989 RVA: 0x0031538D File Offset: 0x0031358D
	protected virtual void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (!this.m_shown)
		{
			return;
		}
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		this.OnGameplaySceneLoaded();
	}

	// Token: 0x0600984E RID: 38990 RVA: 0x003153B8 File Offset: 0x003135B8
	private void EnableFullScreenBlur()
	{
		if (this.m_blurEnabled)
		{
			return;
		}
		this.m_blurEnabled = true;
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		fullScreenFXMgr.SetBlurAmount(0.3f);
		fullScreenFXMgr.SetBlurBrightness(0.4f);
		fullScreenFXMgr.SetBlurDesaturation(0.5f);
		fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc, null);
	}

	// Token: 0x0600984F RID: 38991 RVA: 0x0031540D File Offset: 0x0031360D
	private void DisableFullScreenBlur()
	{
		if (!this.m_blurEnabled)
		{
			return;
		}
		this.m_blurEnabled = false;
		FullScreenFXMgr.Get().StopBlur(0.5f, iTween.EaseType.easeOutCirc, null, true);
	}

	// Token: 0x06009850 RID: 38992
	protected abstract void OnGameplaySceneLoaded();

	// Token: 0x04007F47 RID: 32583
	public UberText m_title;

	// Token: 0x04007F48 RID: 32584
	public MatchingQueueTab m_queueTab;

	// Token: 0x04007F49 RID: 32585
	public UIBButton m_cancelButton;

	// Token: 0x04007F4A RID: 32586
	public Vector3_MobileOverride m_startPosition = new Vector3_MobileOverride(new Vector3(-0.05f, 8.2f, -1.8f));

	// Token: 0x04007F4C RID: 32588
	public Float_MobileOverride m_endScale;

	// Token: 0x04007F4D RID: 32589
	public Float_MobileOverride m_scaleAfterPunch;

	// Token: 0x04007F4E RID: 32590
	protected bool m_shown;

	// Token: 0x04007F4F RID: 32591
	protected bool m_blockingLoadingScreen;

	// Token: 0x04007F50 RID: 32592
	protected Camera m_fullScreenEffectsCamera;

	// Token: 0x04007F51 RID: 32593
	protected List<TransitionPopup.MatchCanceledEvent> m_matchCanceledListeners = new List<TransitionPopup.MatchCanceledEvent>();

	// Token: 0x04007F52 RID: 32594
	protected AdventureDbId m_adventureId;

	// Token: 0x04007F53 RID: 32595
	protected FormatType m_formatType;

	// Token: 0x04007F54 RID: 32596
	protected GameType m_gameType;

	// Token: 0x04007F55 RID: 32597
	protected long? m_deckId;

	// Token: 0x04007F56 RID: 32598
	protected int m_scenarioId;

	// Token: 0x04007F57 RID: 32599
	protected bool m_showAnimationFinished;

	// Token: 0x04007F58 RID: 32600
	private float POPUP_TIME = 0.3f;

	// Token: 0x04007F59 RID: 32601
	private float START_SCALE_VAL = 0.1f;

	// Token: 0x04007F5A RID: 32602
	private Vector3 END_POSITION;

	// Token: 0x04007F5B RID: 32603
	private bool m_blurEnabled;

	// Token: 0x0200277B RID: 10107
	// (Invoke) Token: 0x06013A1A RID: 80410
	public delegate void MatchCanceledEvent();
}
