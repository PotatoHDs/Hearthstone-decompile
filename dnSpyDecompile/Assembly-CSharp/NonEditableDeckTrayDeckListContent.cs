using System;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020008EF RID: 2287
public abstract class NonEditableDeckTrayDeckListContent : DeckTrayDeckListContent
{
	// Token: 0x06007EB8 RID: 32440 RVA: 0x00290F38 File Offset: 0x0028F138
	protected override void Awake()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset += this.WillReset;
		}
		base.Awake();
	}

	// Token: 0x06007EB9 RID: 32441 RVA: 0x00290F6C File Offset: 0x0028F16C
	protected override void OnDestroy()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= this.WillReset;
		}
		base.OnDestroy();
	}

	// Token: 0x06007EBA RID: 32442 RVA: 0x00290FA0 File Offset: 0x0028F1A0
	private void WillReset()
	{
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.BeginAnimation), null);
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.EndAnimation), null);
	}

	// Token: 0x06007EBB RID: 32443 RVA: 0x00290FC8 File Offset: 0x0028F1C8
	protected override void Initialize()
	{
		if (this.m_initialized)
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(this.m_deckInfoActorPrefab, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogError(string.Format("Unable to load actor {0}: null", this.m_deckInfoActorPrefab), base.gameObject);
			return;
		}
		this.m_deckInfoTooltip = gameObject.GetComponent<CollectionDeckInfo>();
		if (this.m_deckInfoTooltip == null)
		{
			Debug.LogError(string.Format("Actor {0} does not contain CollectionDeckInfo component.", this.m_deckInfoActorPrefab), base.gameObject);
			return;
		}
		GameUtils.SetParent(this.m_deckInfoTooltip, this.m_deckInfoTooltipBone, false);
		this.m_deckInfoTooltip.RegisterHideListener(new CollectionDeckInfo.HideListener(this.HideDeckInfoListener));
		gameObject = AssetLoader.Get().InstantiatePrefab(this.m_deckOptionsPrefab, AssetLoadingOptions.None);
		this.m_deckOptionsMenu = gameObject.GetComponent<DeckOptionsMenu>();
		GameUtils.SetParent(this.m_deckOptionsMenu.gameObject, this.m_deckOptionsBone, false);
		this.m_deckOptionsMenu.SetDeckInfo(this.m_deckInfoTooltip);
		base.HideDeckInfo();
		base.CreateTraySections();
		this.m_initialized = true;
	}

	// Token: 0x06007EBC RID: 32444 RVA: 0x002910D8 File Offset: 0x0028F2D8
	public override bool AnimateContentEntranceStart()
	{
		this.Initialize();
		long editDeckID = -1L;
		if (this.m_editingTraySection != null)
		{
			editDeckID = this.m_editingTraySection.m_deckBox.GetDeckID();
		}
		base.InitializeTraysFromDecks();
		base.SwapEditTrayIfNeeded(editDeckID);
		base.UpdateAllTrays(false, false);
		if (this.m_editingTraySection != null)
		{
			this.m_editingTraySection.MoveDeckBoxBackToOriginalPosition(0.25f, delegate(object o)
			{
				this.m_editingTraySection = null;
			});
		}
		base.FireBusyWithDeckEvent(true);
		base.FireDeckCountChangedEvent();
		CollectionManager.Get().DoneEditing();
		return true;
	}

	// Token: 0x06007EBD RID: 32445 RVA: 0x00291164 File Offset: 0x0028F364
	public override bool AnimateContentEntranceEnd()
	{
		if (this.m_editingTraySection != null)
		{
			return false;
		}
		base.FireBusyWithDeckEvent(false);
		return true;
	}

	// Token: 0x06007EBE RID: 32446 RVA: 0x0029117E File Offset: 0x0028F37E
	public override bool AnimateContentExitStart()
	{
		this.m_animatingExit = true;
		base.FireBusyWithDeckEvent(true);
		Processor.ScheduleCallback(0.5f, false, new Processor.ScheduledCallback(this.BeginAnimation), null);
		return true;
	}

	// Token: 0x06007EBF RID: 32447 RVA: 0x002911A8 File Offset: 0x0028F3A8
	private void BeginAnimation(object userData)
	{
		float secondsToWait = 0.5f;
		foreach (TraySection traySection in this.m_traySections)
		{
			if (this.m_editingTraySection != traySection)
			{
				traySection.HideDeckBox(false, null);
			}
		}
		if (this.m_editingTraySection != null)
		{
			this.m_editingTraySection.MoveDeckBoxToEditPosition(this.m_deckEditTopPos.position, 0.25f, null);
		}
		Processor.ScheduleCallback(secondsToWait, false, new Processor.ScheduledCallback(this.EndAnimation), null);
	}

	// Token: 0x06007EC0 RID: 32448 RVA: 0x002798FC File Offset: 0x00277AFC
	private void EndAnimation(object userData)
	{
		this.m_animatingExit = false;
		base.FireBusyWithDeckEvent(false);
	}

	// Token: 0x06007EC1 RID: 32449 RVA: 0x00291250 File Offset: 0x0028F450
	protected override void HideDeckInfoListener()
	{
		if (this.m_editingTraySection != null && !UniversalInputManager.UsePhoneUI)
		{
			SceneUtils.SetLayer(this.m_editingTraySection.m_deckBox.gameObject, GameLayer.Default);
			SceneUtils.SetLayer(this.m_deckOptionsMenu.gameObject, GameLayer.Default);
		}
		FullScreenFXMgr.Get().StopDesaturate(0.25f, iTween.EaseType.easeInOutQuad, null, null);
		if (UniversalInputManager.Get().IsTouchMode() && this.m_editingTraySection != null)
		{
			this.m_editingTraySection.m_deckBox.SetHighlightState(ActorStateType.NONE);
			this.m_editingTraySection.m_deckBox.ShowDeckName();
		}
		this.m_deckOptionsMenu.Hide(true);
	}
}
