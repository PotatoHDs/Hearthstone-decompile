using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MoveMinionHoverTarget : MonoBehaviour
{
	private Actor m_actor;

	private Card m_lastDroppedCard;

	private readonly List<PlayErrors.ErrorType> PlayErrorsToSuppressWhenDisplaying = new List<PlayErrors.ErrorType>
	{
		PlayErrors.ErrorType.INVALID,
		PlayErrors.ErrorType.REQ_TARGET_TO_PLAY
	};

	private Entity m_entity => m_actor.GetEntity();

	private void Start()
	{
		GameState.Get().RegisterOptionRejectedListener(OnOptionRejected);
		m_actor = GetComponent<Actor>();
	}

	private void OnDestroy()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterOptionRejectedListener(OnOptionRejected);
		}
	}

	public void DropCardOnHoverTarget(Card heldCard)
	{
		Entity entity = heldCard.GetEntity();
		m_lastDroppedCard = heldCard;
		if (ThinkEmoteManager.Get() != null)
		{
			ThinkEmoteManager.Get().NotifyOfActivity();
		}
		GameState gameState = GameState.Get();
		if (SetSelectedOption(entity, out var mainOptionPlayErrorType, out var mainOptionPlayErrorParam, out var targetPlayErrorType, out var targetPlayErrorParam))
		{
			gameState.SetSelectedOptionTarget(entity.GetEntityId());
			gameState.SendOption();
			return;
		}
		if (!PlayErrorsToSuppressWhenDisplaying.Contains(mainOptionPlayErrorType))
		{
			PlayErrors.DisplayPlayError(mainOptionPlayErrorType, mainOptionPlayErrorParam, m_entity);
		}
		else if (!PlayErrorsToSuppressWhenDisplaying.Contains(targetPlayErrorType))
		{
			PlayErrors.DisplayPlayError(targetPlayErrorType, targetPlayErrorParam, m_entity);
		}
		InputManager.Get().AddHeldCardBackToPlayZone(m_lastDroppedCard);
	}

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
			if (option.Type != Network.Options.Option.OptionType.POWER || option.Main.ID != m_entity.GetEntityId())
			{
				continue;
			}
			if (!option.Main.IsValidTarget(heldEntity.GetEntityId()))
			{
				targetPlayErrorType = option.Main.GetErrorForTarget(heldEntity.GetEntityId());
				targetPlayErrorParam = option.Main.GetErrorParamForTarget(heldEntity.GetEntityId());
				continue;
			}
			if (!option.Main.PlayErrorInfo.IsValid())
			{
				mainOptionPlayErrorType = option.Main.PlayErrorInfo.PlayError;
				mainOptionPlayErrorParam = option.Main.PlayErrorInfo.PlayErrorParam;
				continue;
			}
			gameState.SetSelectedOption(i);
			return true;
		}
		return false;
	}

	private void OnOptionRejected(Network.Options.Option option, object userData)
	{
		if (option.Main.ID == m_entity.GetEntityId())
		{
			InputManager.Get().AddHeldCardBackToPlayZone(m_lastDroppedCard);
		}
	}
}
