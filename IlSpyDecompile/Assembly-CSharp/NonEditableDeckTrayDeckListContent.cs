using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public abstract class NonEditableDeckTrayDeckListContent : DeckTrayDeckListContent
{
	protected override void Awake()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset += WillReset;
		}
		base.Awake();
	}

	protected override void OnDestroy()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= WillReset;
		}
		base.OnDestroy();
	}

	private void WillReset()
	{
		Processor.CancelScheduledCallback(BeginAnimation);
		Processor.CancelScheduledCallback(EndAnimation);
	}

	protected override void Initialize()
	{
		if (m_initialized)
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(m_deckInfoActorPrefab, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogError($"Unable to load actor {m_deckInfoActorPrefab}: null", base.gameObject);
			return;
		}
		m_deckInfoTooltip = gameObject.GetComponent<CollectionDeckInfo>();
		if (m_deckInfoTooltip == null)
		{
			Debug.LogError($"Actor {m_deckInfoActorPrefab} does not contain CollectionDeckInfo component.", base.gameObject);
			return;
		}
		GameUtils.SetParent(m_deckInfoTooltip, m_deckInfoTooltipBone);
		m_deckInfoTooltip.RegisterHideListener(HideDeckInfoListener);
		gameObject = AssetLoader.Get().InstantiatePrefab(m_deckOptionsPrefab);
		m_deckOptionsMenu = gameObject.GetComponent<DeckOptionsMenu>();
		GameUtils.SetParent(m_deckOptionsMenu.gameObject, m_deckOptionsBone);
		m_deckOptionsMenu.SetDeckInfo(m_deckInfoTooltip);
		HideDeckInfo();
		CreateTraySections();
		m_initialized = true;
	}

	public override bool AnimateContentEntranceStart()
	{
		Initialize();
		long editDeckID = -1L;
		if (m_editingTraySection != null)
		{
			editDeckID = m_editingTraySection.m_deckBox.GetDeckID();
		}
		InitializeTraysFromDecks();
		SwapEditTrayIfNeeded(editDeckID);
		UpdateAllTrays(immediate: false, initializeTrays: false);
		if (m_editingTraySection != null)
		{
			m_editingTraySection.MoveDeckBoxBackToOriginalPosition(0.25f, delegate
			{
				m_editingTraySection = null;
			});
		}
		FireBusyWithDeckEvent(busy: true);
		FireDeckCountChangedEvent();
		CollectionManager.Get().DoneEditing();
		return true;
	}

	public override bool AnimateContentEntranceEnd()
	{
		if (m_editingTraySection != null)
		{
			return false;
		}
		FireBusyWithDeckEvent(busy: false);
		return true;
	}

	public override bool AnimateContentExitStart()
	{
		m_animatingExit = true;
		FireBusyWithDeckEvent(busy: true);
		Processor.ScheduleCallback(0.5f, realTime: false, BeginAnimation);
		return true;
	}

	private void BeginAnimation(object userData)
	{
		float secondsToWait = 0.5f;
		foreach (TraySection traySection in m_traySections)
		{
			if (m_editingTraySection != traySection)
			{
				traySection.HideDeckBox();
			}
		}
		if (m_editingTraySection != null)
		{
			m_editingTraySection.MoveDeckBoxToEditPosition(m_deckEditTopPos.position, 0.25f);
		}
		Processor.ScheduleCallback(secondsToWait, realTime: false, EndAnimation);
	}

	private void EndAnimation(object userData)
	{
		m_animatingExit = false;
		FireBusyWithDeckEvent(busy: false);
	}

	protected override void HideDeckInfoListener()
	{
		if (m_editingTraySection != null && !UniversalInputManager.UsePhoneUI)
		{
			SceneUtils.SetLayer(m_editingTraySection.m_deckBox.gameObject, GameLayer.Default);
			SceneUtils.SetLayer(m_deckOptionsMenu.gameObject, GameLayer.Default);
		}
		FullScreenFXMgr.Get().StopDesaturate(0.25f, iTween.EaseType.easeInOutQuad);
		if (UniversalInputManager.Get().IsTouchMode() && m_editingTraySection != null)
		{
			m_editingTraySection.m_deckBox.SetHighlightState(ActorStateType.NONE);
			m_editingTraySection.m_deckBox.ShowDeckName();
		}
		m_deckOptionsMenu.Hide();
	}
}
