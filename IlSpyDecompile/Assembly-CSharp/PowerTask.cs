using System.Collections.Generic;
using PegasusGame;

public class PowerTask
{
	public delegate void TaskCompleteCallback();

	private Network.PowerHistory m_power;

	private bool m_completed;

	private TaskCompleteCallback m_onCompleted;

	public Network.PowerHistory GetPower()
	{
		return m_power;
	}

	public void SetPower(Network.PowerHistory power)
	{
		m_power = power;
	}

	public bool IsCompleted()
	{
		return m_completed;
	}

	public void SetCompleted(bool complete)
	{
		m_completed = complete;
		if (m_completed && m_onCompleted != null)
		{
			m_onCompleted();
		}
	}

	public void SetTaskCompleteCallback(TaskCompleteCallback onComplete)
	{
		m_onCompleted = onComplete;
	}

	private bool IsZoneTransition(TAG_ZONE fromZone, TAG_ZONE toZone)
	{
		if (IsCompleted())
		{
			return false;
		}
		Network.PowerHistory power = GetPower();
		if (power.Type == Network.PowerType.SHOW_ENTITY)
		{
			Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
			Entity entity = GameState.Get().GetEntity(histShowEntity.Entity.ID);
			Network.Entity.Tag tag = histShowEntity.Entity.Tags.Find((Network.Entity.Tag currTag) => currTag.Name == 49);
			if (entity != null && tag != null && entity.GetZone() == fromZone && tag.Value == (int)toZone)
			{
				return true;
			}
		}
		if (power.Type == Network.PowerType.TAG_CHANGE)
		{
			Network.HistTagChange histTagChange = (Network.HistTagChange)power;
			Entity entity2 = GameState.Get().GetEntity(histTagChange.Entity);
			if (entity2 != null && histTagChange.Tag == 49 && entity2.GetZone() == fromZone && histTagChange.Value == (int)toZone)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsCardDraw()
	{
		return IsZoneTransition(TAG_ZONE.DECK, TAG_ZONE.HAND);
	}

	public bool IsCardMill()
	{
		return IsZoneTransition(TAG_ZONE.DECK, TAG_ZONE.GRAVEYARD);
	}

	public bool IsFatigue()
	{
		if (IsCompleted())
		{
			return false;
		}
		Network.PowerHistory power = GetPower();
		if (power.Type == Network.PowerType.BLOCK_START)
		{
			return ((Network.HistBlockStart)power).BlockType == HistoryBlock.Type.FATIGUE;
		}
		return false;
	}

	public void DoRealTimeTask(List<Network.PowerHistory> powerList, int index)
	{
		GameState gameState = GameState.Get();
		switch (m_power.Type)
		{
		case Network.PowerType.CREATE_GAME:
		{
			Network.HistCreateGame createGame = (Network.HistCreateGame)m_power;
			gameState.OnRealTimeCreateGame(powerList, index, createGame);
			break;
		}
		case Network.PowerType.FULL_ENTITY:
		{
			Network.HistFullEntity fullEntity = (Network.HistFullEntity)m_power;
			gameState.OnRealTimeFullEntity(fullEntity);
			break;
		}
		case Network.PowerType.SHOW_ENTITY:
		{
			Network.HistShowEntity showEntity = (Network.HistShowEntity)m_power;
			gameState.OnRealTimeShowEntity(showEntity);
			break;
		}
		case Network.PowerType.CHANGE_ENTITY:
		{
			Network.HistChangeEntity changeEntity = (Network.HistChangeEntity)m_power;
			gameState.OnRealTimeChangeEntity(powerList, index, changeEntity);
			break;
		}
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange change = (Network.HistTagChange)m_power;
			gameState.OnRealTimeTagChange(change);
			break;
		}
		case Network.PowerType.RESET_GAME:
		{
			Network.HistResetGame resetGame = (Network.HistResetGame)m_power;
			gameState.OnRealTimeResetGame(resetGame);
			break;
		}
		case Network.PowerType.VO_SPELL:
		{
			Network.HistVoSpell voSpell = (Network.HistVoSpell)m_power;
			gameState.OnRealTimeVoSpell(voSpell);
			break;
		}
		case Network.PowerType.HIDE_ENTITY:
		case Network.PowerType.BLOCK_START:
		case Network.PowerType.BLOCK_END:
		case Network.PowerType.META_DATA:
		case Network.PowerType.SUB_SPELL_START:
		case Network.PowerType.SUB_SPELL_END:
			break;
		}
	}

	public void DoTask()
	{
		if (!m_completed)
		{
			GameState gameState = GameState.Get();
			switch (m_power.Type)
			{
			case Network.PowerType.FULL_ENTITY:
			{
				Network.HistFullEntity fullEntity = (Network.HistFullEntity)m_power;
				gameState.OnFullEntity(fullEntity);
				HistoryManager.Get().OnEntityRevealed();
				break;
			}
			case Network.PowerType.SHOW_ENTITY:
			{
				Network.HistShowEntity showEntity = (Network.HistShowEntity)m_power;
				gameState.OnShowEntity(showEntity);
				HistoryManager.Get().OnEntityRevealed();
				break;
			}
			case Network.PowerType.HIDE_ENTITY:
			{
				Network.HistHideEntity hideEntity = (Network.HistHideEntity)m_power;
				gameState.OnHideEntity(hideEntity);
				break;
			}
			case Network.PowerType.CHANGE_ENTITY:
			{
				Network.HistChangeEntity changeEntity = (Network.HistChangeEntity)m_power;
				gameState.OnChangeEntity(changeEntity);
				break;
			}
			case Network.PowerType.TAG_CHANGE:
			{
				Network.HistTagChange netChange2 = (Network.HistTagChange)m_power;
				gameState.OnTagChange(netChange2);
				break;
			}
			case Network.PowerType.META_DATA:
			{
				Network.HistMetaData metaData = (Network.HistMetaData)m_power;
				gameState.OnMetaData(metaData);
				break;
			}
			case Network.PowerType.RESET_GAME:
			{
				Network.HistResetGame resetGame = (Network.HistResetGame)m_power;
				gameState.OnResetGame(resetGame);
				break;
			}
			case Network.PowerType.VO_SPELL:
			{
				Network.HistVoSpell voSpell = (Network.HistVoSpell)m_power;
				gameState.OnVoSpell(voSpell);
				break;
			}
			case Network.PowerType.CACHED_TAG_FOR_DORMANT_CHANGE:
			{
				Network.HistCachedTagForDormantChange netChange = (Network.HistCachedTagForDormantChange)m_power;
				gameState.OnCachedTagForDormantChange(netChange);
				break;
			}
			case Network.PowerType.SHUFFLE_DECK:
			{
				Network.HistShuffleDeck shuffleDeck = (Network.HistShuffleDeck)m_power;
				gameState.OnShuffleDeck(shuffleDeck);
				break;
			}
			}
			SetCompleted(complete: true);
		}
	}

	public void DoEarlyConcedeTask()
	{
		if (!m_completed)
		{
			GameState gameState = GameState.Get();
			switch (m_power.Type)
			{
			case Network.PowerType.SHOW_ENTITY:
			{
				Network.HistShowEntity showEntity = (Network.HistShowEntity)m_power;
				gameState.OnEarlyConcedeShowEntity(showEntity);
				break;
			}
			case Network.PowerType.HIDE_ENTITY:
			{
				Network.HistHideEntity hideEntity = (Network.HistHideEntity)m_power;
				gameState.OnEarlyConcedeHideEntity(hideEntity);
				break;
			}
			case Network.PowerType.CHANGE_ENTITY:
			{
				Network.HistChangeEntity changeEntity = (Network.HistChangeEntity)m_power;
				gameState.OnEarlyConcedeChangeEntity(changeEntity);
				break;
			}
			case Network.PowerType.TAG_CHANGE:
			{
				Network.HistTagChange netChange = (Network.HistTagChange)m_power;
				gameState.OnEarlyConcedeTagChange(netChange);
				break;
			}
			}
			m_completed = true;
		}
	}

	public override string ToString()
	{
		string arg = "null";
		if (m_power != null)
		{
			switch (m_power.Type)
			{
			case Network.PowerType.CREATE_GAME:
				arg = ((Network.HistCreateGame)m_power).ToString();
				break;
			case Network.PowerType.TAG_CHANGE:
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)m_power;
				arg = string.Format("type={0} entity={1} {2} {3}", m_power.Type, GetPrintableEntity(histTagChange.Entity), Tags.DebugTag(histTagChange.Tag, histTagChange.Value), histTagChange.ChangeDef ? "DEF CHANGE" : "");
				break;
			}
			case Network.PowerType.FULL_ENTITY:
			{
				Network.HistFullEntity histFullEntity = (Network.HistFullEntity)m_power;
				arg = $"type={m_power.Type} entity={GetPrintableEntity(histFullEntity.Entity)} tags={histFullEntity.Entity.Tags}";
				break;
			}
			case Network.PowerType.SHOW_ENTITY:
			{
				Network.HistShowEntity histShowEntity = (Network.HistShowEntity)m_power;
				arg = $"type={m_power.Type} entity={GetPrintableEntity(histShowEntity.Entity)} tags={histShowEntity.Entity.Tags}";
				break;
			}
			case Network.PowerType.HIDE_ENTITY:
			{
				Network.HistHideEntity histHideEntity = (Network.HistHideEntity)m_power;
				arg = $"type={m_power.Type} entity={GetPrintableEntity(histHideEntity.Entity)} zone={histHideEntity.Zone}";
				break;
			}
			case Network.PowerType.CHANGE_ENTITY:
			{
				Network.HistChangeEntity histChangeEntity = (Network.HistChangeEntity)m_power;
				arg = $"type={m_power.Type} entity={GetPrintableEntity(histChangeEntity.Entity)} tags={histChangeEntity.Entity.Tags}";
				break;
			}
			case Network.PowerType.META_DATA:
				arg = ((Network.HistMetaData)m_power).ToString();
				break;
			}
		}
		return $"power=[{arg}] complete={m_completed}";
	}

	private string GetEntityLogName(Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		string name = entity.GetName();
		if (entity.IsPlayer())
		{
			BnetPlayer bnetPlayer = (entity as Player).GetBnetPlayer();
			if (bnetPlayer != null && bnetPlayer.GetBattleTag() != null)
			{
				name = bnetPlayer.GetBattleTag().GetName();
			}
		}
		return name;
	}

	private string GetPrintableEntity(int entityId)
	{
		Entity entity = GameState.Get().GetEntity(entityId);
		if (entity == null)
		{
			return entityId.ToString();
		}
		string entityLogName = GetEntityLogName(entity);
		if (entityLogName == null)
		{
			return $"[id={entityId} cardId={entity.GetCardId()}]";
		}
		return $"[id={entityId} cardId={entity.GetCardId()} name={entityLogName}]";
	}

	private string GetPrintableEntity(Network.Entity netEntity)
	{
		Entity entity = GameState.Get().GetEntity(netEntity.ID);
		string entityLogName = GetEntityLogName(entity);
		if (entityLogName == null)
		{
			return $"[id={netEntity.ID} cardId={netEntity.CardID}]";
		}
		return $"[id={netEntity.ID} cardId={netEntity.CardID} name={entityLogName}]";
	}
}
