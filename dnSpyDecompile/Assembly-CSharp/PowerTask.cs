using System;
using System.Collections.Generic;
using PegasusGame;

// Token: 0x0200033C RID: 828
public class PowerTask
{
	// Token: 0x06002F7E RID: 12158 RVA: 0x000F30DE File Offset: 0x000F12DE
	public Network.PowerHistory GetPower()
	{
		return this.m_power;
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x000F30E6 File Offset: 0x000F12E6
	public void SetPower(Network.PowerHistory power)
	{
		this.m_power = power;
	}

	// Token: 0x06002F80 RID: 12160 RVA: 0x000F30EF File Offset: 0x000F12EF
	public bool IsCompleted()
	{
		return this.m_completed;
	}

	// Token: 0x06002F81 RID: 12161 RVA: 0x000F30F7 File Offset: 0x000F12F7
	public void SetCompleted(bool complete)
	{
		this.m_completed = complete;
		if (this.m_completed && this.m_onCompleted != null)
		{
			this.m_onCompleted();
		}
	}

	// Token: 0x06002F82 RID: 12162 RVA: 0x000F311B File Offset: 0x000F131B
	public void SetTaskCompleteCallback(PowerTask.TaskCompleteCallback onComplete)
	{
		this.m_onCompleted = onComplete;
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x000F3124 File Offset: 0x000F1324
	private bool IsZoneTransition(TAG_ZONE fromZone, TAG_ZONE toZone)
	{
		if (this.IsCompleted())
		{
			return false;
		}
		Network.PowerHistory power = this.GetPower();
		if (power.Type == Network.PowerType.SHOW_ENTITY)
		{
			Network.HistShowEntity histShowEntity = (Network.HistShowEntity)power;
			global::Entity entity = GameState.Get().GetEntity(histShowEntity.Entity.ID);
			Network.Entity.Tag tag = histShowEntity.Entity.Tags.Find((Network.Entity.Tag currTag) => currTag.Name == 49);
			if (entity != null && tag != null && entity.GetZone() == fromZone && tag.Value == (int)toZone)
			{
				return true;
			}
		}
		if (power.Type == Network.PowerType.TAG_CHANGE)
		{
			Network.HistTagChange histTagChange = (Network.HistTagChange)power;
			global::Entity entity2 = GameState.Get().GetEntity(histTagChange.Entity);
			if (entity2 != null && histTagChange.Tag == 49 && entity2.GetZone() == fromZone && histTagChange.Value == (int)toZone)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x000F31FC File Offset: 0x000F13FC
	public bool IsCardDraw()
	{
		return this.IsZoneTransition(TAG_ZONE.DECK, TAG_ZONE.HAND);
	}

	// Token: 0x06002F85 RID: 12165 RVA: 0x000F3206 File Offset: 0x000F1406
	public bool IsCardMill()
	{
		return this.IsZoneTransition(TAG_ZONE.DECK, TAG_ZONE.GRAVEYARD);
	}

	// Token: 0x06002F86 RID: 12166 RVA: 0x000F3210 File Offset: 0x000F1410
	public bool IsFatigue()
	{
		if (this.IsCompleted())
		{
			return false;
		}
		Network.PowerHistory power = this.GetPower();
		return power.Type == Network.PowerType.BLOCK_START && ((Network.HistBlockStart)power).BlockType == HistoryBlock.Type.FATIGUE;
	}

	// Token: 0x06002F87 RID: 12167 RVA: 0x000F3248 File Offset: 0x000F1448
	public void DoRealTimeTask(List<Network.PowerHistory> powerList, int index)
	{
		GameState gameState = GameState.Get();
		switch (this.m_power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
		{
			Network.HistFullEntity fullEntity = (Network.HistFullEntity)this.m_power;
			gameState.OnRealTimeFullEntity(fullEntity);
			return;
		}
		case Network.PowerType.SHOW_ENTITY:
		{
			Network.HistShowEntity showEntity = (Network.HistShowEntity)this.m_power;
			gameState.OnRealTimeShowEntity(showEntity);
			return;
		}
		case Network.PowerType.HIDE_ENTITY:
		case Network.PowerType.BLOCK_START:
		case Network.PowerType.BLOCK_END:
		case Network.PowerType.META_DATA:
		case Network.PowerType.SUB_SPELL_START:
		case Network.PowerType.SUB_SPELL_END:
			break;
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange change = (Network.HistTagChange)this.m_power;
			gameState.OnRealTimeTagChange(change);
			return;
		}
		case Network.PowerType.CREATE_GAME:
		{
			Network.HistCreateGame createGame = (Network.HistCreateGame)this.m_power;
			gameState.OnRealTimeCreateGame(powerList, index, createGame);
			return;
		}
		case Network.PowerType.CHANGE_ENTITY:
		{
			Network.HistChangeEntity changeEntity = (Network.HistChangeEntity)this.m_power;
			gameState.OnRealTimeChangeEntity(powerList, index, changeEntity);
			return;
		}
		case Network.PowerType.RESET_GAME:
		{
			Network.HistResetGame resetGame = (Network.HistResetGame)this.m_power;
			gameState.OnRealTimeResetGame(resetGame);
			return;
		}
		case Network.PowerType.VO_SPELL:
		{
			Network.HistVoSpell voSpell = (Network.HistVoSpell)this.m_power;
			gameState.OnRealTimeVoSpell(voSpell);
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x06002F88 RID: 12168 RVA: 0x000F3344 File Offset: 0x000F1544
	public void DoTask()
	{
		if (this.m_completed)
		{
			return;
		}
		GameState gameState = GameState.Get();
		switch (this.m_power.Type)
		{
		case Network.PowerType.FULL_ENTITY:
		{
			Network.HistFullEntity fullEntity = (Network.HistFullEntity)this.m_power;
			gameState.OnFullEntity(fullEntity);
			HistoryManager.Get().OnEntityRevealed();
			break;
		}
		case Network.PowerType.SHOW_ENTITY:
		{
			Network.HistShowEntity showEntity = (Network.HistShowEntity)this.m_power;
			gameState.OnShowEntity(showEntity);
			HistoryManager.Get().OnEntityRevealed();
			break;
		}
		case Network.PowerType.HIDE_ENTITY:
		{
			Network.HistHideEntity hideEntity = (Network.HistHideEntity)this.m_power;
			gameState.OnHideEntity(hideEntity);
			break;
		}
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange netChange = (Network.HistTagChange)this.m_power;
			gameState.OnTagChange(netChange);
			break;
		}
		case Network.PowerType.META_DATA:
		{
			Network.HistMetaData metaData = (Network.HistMetaData)this.m_power;
			gameState.OnMetaData(metaData);
			break;
		}
		case Network.PowerType.CHANGE_ENTITY:
		{
			Network.HistChangeEntity changeEntity = (Network.HistChangeEntity)this.m_power;
			gameState.OnChangeEntity(changeEntity);
			break;
		}
		case Network.PowerType.RESET_GAME:
		{
			Network.HistResetGame resetGame = (Network.HistResetGame)this.m_power;
			gameState.OnResetGame(resetGame);
			break;
		}
		case Network.PowerType.VO_SPELL:
		{
			Network.HistVoSpell voSpell = (Network.HistVoSpell)this.m_power;
			gameState.OnVoSpell(voSpell);
			break;
		}
		case Network.PowerType.CACHED_TAG_FOR_DORMANT_CHANGE:
		{
			Network.HistCachedTagForDormantChange netChange2 = (Network.HistCachedTagForDormantChange)this.m_power;
			gameState.OnCachedTagForDormantChange(netChange2);
			break;
		}
		case Network.PowerType.SHUFFLE_DECK:
		{
			Network.HistShuffleDeck shuffleDeck = (Network.HistShuffleDeck)this.m_power;
			gameState.OnShuffleDeck(shuffleDeck);
			break;
		}
		}
		this.SetCompleted(true);
	}

	// Token: 0x06002F89 RID: 12169 RVA: 0x000F34C8 File Offset: 0x000F16C8
	public void DoEarlyConcedeTask()
	{
		if (this.m_completed)
		{
			return;
		}
		GameState gameState = GameState.Get();
		Network.PowerType type = this.m_power.Type;
		switch (type)
		{
		case Network.PowerType.SHOW_ENTITY:
		{
			Network.HistShowEntity showEntity = (Network.HistShowEntity)this.m_power;
			gameState.OnEarlyConcedeShowEntity(showEntity);
			break;
		}
		case Network.PowerType.HIDE_ENTITY:
		{
			Network.HistHideEntity hideEntity = (Network.HistHideEntity)this.m_power;
			gameState.OnEarlyConcedeHideEntity(hideEntity);
			break;
		}
		case Network.PowerType.TAG_CHANGE:
		{
			Network.HistTagChange netChange = (Network.HistTagChange)this.m_power;
			gameState.OnEarlyConcedeTagChange(netChange);
			break;
		}
		default:
			if (type == Network.PowerType.CHANGE_ENTITY)
			{
				Network.HistChangeEntity changeEntity = (Network.HistChangeEntity)this.m_power;
				gameState.OnEarlyConcedeChangeEntity(changeEntity);
			}
			break;
		}
		this.m_completed = true;
	}

	// Token: 0x06002F8A RID: 12170 RVA: 0x000F356C File Offset: 0x000F176C
	public override string ToString()
	{
		string arg = "null";
		if (this.m_power != null)
		{
			switch (this.m_power.Type)
			{
			case Network.PowerType.FULL_ENTITY:
			{
				Network.HistFullEntity histFullEntity = (Network.HistFullEntity)this.m_power;
				arg = string.Format("type={0} entity={1} tags={2}", this.m_power.Type, this.GetPrintableEntity(histFullEntity.Entity), histFullEntity.Entity.Tags);
				break;
			}
			case Network.PowerType.SHOW_ENTITY:
			{
				Network.HistShowEntity histShowEntity = (Network.HistShowEntity)this.m_power;
				arg = string.Format("type={0} entity={1} tags={2}", this.m_power.Type, this.GetPrintableEntity(histShowEntity.Entity), histShowEntity.Entity.Tags);
				break;
			}
			case Network.PowerType.HIDE_ENTITY:
			{
				Network.HistHideEntity histHideEntity = (Network.HistHideEntity)this.m_power;
				arg = string.Format("type={0} entity={1} zone={2}", this.m_power.Type, this.GetPrintableEntity(histHideEntity.Entity), histHideEntity.Zone);
				break;
			}
			case Network.PowerType.TAG_CHANGE:
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)this.m_power;
				arg = string.Format("type={0} entity={1} {2} {3}", new object[]
				{
					this.m_power.Type,
					this.GetPrintableEntity(histTagChange.Entity),
					Tags.DebugTag(histTagChange.Tag, histTagChange.Value),
					histTagChange.ChangeDef ? "DEF CHANGE" : ""
				});
				break;
			}
			case Network.PowerType.CREATE_GAME:
				arg = ((Network.HistCreateGame)this.m_power).ToString();
				break;
			case Network.PowerType.META_DATA:
				arg = ((Network.HistMetaData)this.m_power).ToString();
				break;
			case Network.PowerType.CHANGE_ENTITY:
			{
				Network.HistChangeEntity histChangeEntity = (Network.HistChangeEntity)this.m_power;
				arg = string.Format("type={0} entity={1} tags={2}", this.m_power.Type, this.GetPrintableEntity(histChangeEntity.Entity), histChangeEntity.Entity.Tags);
				break;
			}
			}
		}
		return string.Format("power=[{0}] complete={1}", arg, this.m_completed);
	}

	// Token: 0x06002F8B RID: 12171 RVA: 0x000F3784 File Offset: 0x000F1984
	private string GetEntityLogName(global::Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		string name = entity.GetName();
		if (entity.IsPlayer())
		{
			BnetPlayer bnetPlayer = (entity as global::Player).GetBnetPlayer();
			if (bnetPlayer != null && bnetPlayer.GetBattleTag() != null)
			{
				name = bnetPlayer.GetBattleTag().GetName();
			}
		}
		return name;
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x000F37D0 File Offset: 0x000F19D0
	private string GetPrintableEntity(int entityId)
	{
		global::Entity entity = GameState.Get().GetEntity(entityId);
		if (entity == null)
		{
			return entityId.ToString();
		}
		string entityLogName = this.GetEntityLogName(entity);
		if (entityLogName == null)
		{
			return string.Format("[id={0} cardId={1}]", entityId, entity.GetCardId());
		}
		return string.Format("[id={0} cardId={1} name={2}]", entityId, entity.GetCardId(), entityLogName);
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x000F3830 File Offset: 0x000F1A30
	private string GetPrintableEntity(Network.Entity netEntity)
	{
		global::Entity entity = GameState.Get().GetEntity(netEntity.ID);
		string entityLogName = this.GetEntityLogName(entity);
		if (entityLogName == null)
		{
			return string.Format("[id={0} cardId={1}]", netEntity.ID, netEntity.CardID);
		}
		return string.Format("[id={0} cardId={1} name={2}]", netEntity.ID, netEntity.CardID, entityLogName);
	}

	// Token: 0x04001A95 RID: 6805
	private Network.PowerHistory m_power;

	// Token: 0x04001A96 RID: 6806
	private bool m_completed;

	// Token: 0x04001A97 RID: 6807
	private PowerTask.TaskCompleteCallback m_onCompleted;

	// Token: 0x020016DA RID: 5850
	// (Invoke) Token: 0x0600E5D8 RID: 58840
	public delegate void TaskCompleteCallback();
}
