using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200032B RID: 811
[RequireComponent(typeof(Actor))]
public class MoveMinionHoverTarget : MonoBehaviour
{
	// Token: 0x17000507 RID: 1287
	// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000E940F File Offset: 0x000E760F
	private Entity m_entity
	{
		get
		{
			return this.m_actor.GetEntity();
		}
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x000E941C File Offset: 0x000E761C
	private void Start()
	{
		GameState.Get().RegisterOptionRejectedListener(new GameState.OptionRejectedCallback(this.OnOptionRejected), null);
		this.m_actor = base.GetComponent<Actor>();
	}

	// Token: 0x06002DE5 RID: 11749 RVA: 0x000E9442 File Offset: 0x000E7642
	private void OnDestroy()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterOptionRejectedListener(new GameState.OptionRejectedCallback(this.OnOptionRejected), null);
		}
	}

	// Token: 0x06002DE6 RID: 11750 RVA: 0x000E9464 File Offset: 0x000E7664
	public void DropCardOnHoverTarget(Card heldCard)
	{
		Entity entity = heldCard.GetEntity();
		this.m_lastDroppedCard = heldCard;
		if (ThinkEmoteManager.Get() != null)
		{
			ThinkEmoteManager.Get().NotifyOfActivity();
		}
		GameState gameState = GameState.Get();
		PlayErrors.ErrorType errorType;
		int? errorParam;
		PlayErrors.ErrorType errorType2;
		int? errorParam2;
		if (this.SetSelectedOption(entity, out errorType, out errorParam, out errorType2, out errorParam2))
		{
			gameState.SetSelectedOptionTarget(entity.GetEntityId());
			gameState.SendOption();
			return;
		}
		if (!this.PlayErrorsToSuppressWhenDisplaying.Contains(errorType))
		{
			PlayErrors.DisplayPlayError(errorType, errorParam, this.m_entity);
		}
		else if (!this.PlayErrorsToSuppressWhenDisplaying.Contains(errorType2))
		{
			PlayErrors.DisplayPlayError(errorType2, errorParam2, this.m_entity);
		}
		InputManager.Get().AddHeldCardBackToPlayZone(this.m_lastDroppedCard);
	}

	// Token: 0x06002DE7 RID: 11751 RVA: 0x000E950C File Offset: 0x000E770C
	private bool SetSelectedOption(Entity heldEntity, out PlayErrors.ErrorType mainOptionPlayErrorType, out int? mainOptionPlayErrorParam, out PlayErrors.ErrorType targetPlayErrorType, out int? targetPlayErrorParam)
	{
		GameState gameState = GameState.Get();
		mainOptionPlayErrorType = PlayErrors.ErrorType.INVALID;
		mainOptionPlayErrorParam = null;
		targetPlayErrorType = PlayErrors.ErrorType.INVALID;
		targetPlayErrorParam = null;
		Network.Options optionsPacket = gameState.GetOptionsPacket();
		if (optionsPacket == null || optionsPacket.List == null)
		{
			return false;
		}
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER && option.Main.ID == this.m_entity.GetEntityId())
			{
				if (!option.Main.IsValidTarget(heldEntity.GetEntityId()))
				{
					targetPlayErrorType = option.Main.GetErrorForTarget(heldEntity.GetEntityId());
					targetPlayErrorParam = option.Main.GetErrorParamForTarget(heldEntity.GetEntityId());
				}
				else
				{
					if (option.Main.PlayErrorInfo.IsValid())
					{
						gameState.SetSelectedOption(i);
						return true;
					}
					mainOptionPlayErrorType = option.Main.PlayErrorInfo.PlayError;
					mainOptionPlayErrorParam = option.Main.PlayErrorInfo.PlayErrorParam;
				}
			}
		}
		return false;
	}

	// Token: 0x06002DE8 RID: 11752 RVA: 0x000E9620 File Offset: 0x000E7820
	private void OnOptionRejected(Network.Options.Option option, object userData)
	{
		if (option.Main.ID != this.m_entity.GetEntityId())
		{
			return;
		}
		InputManager.Get().AddHeldCardBackToPlayZone(this.m_lastDroppedCard);
	}

	// Token: 0x04001950 RID: 6480
	private Actor m_actor;

	// Token: 0x04001951 RID: 6481
	private Card m_lastDroppedCard;

	// Token: 0x04001952 RID: 6482
	private readonly List<PlayErrors.ErrorType> PlayErrorsToSuppressWhenDisplaying = new List<PlayErrors.ErrorType>
	{
		PlayErrors.ErrorType.INVALID,
		PlayErrors.ErrorType.REQ_TARGET_TO_PLAY
	};
}
