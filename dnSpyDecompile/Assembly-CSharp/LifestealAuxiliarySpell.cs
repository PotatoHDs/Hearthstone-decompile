using System;
using PegasusGame;

// Token: 0x02000802 RID: 2050
public class LifestealAuxiliarySpell : Spell
{
	// Token: 0x06006F2E RID: 28462 RVA: 0x0023CB2C File Offset: 0x0023AD2C
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (!base.AttachPowerTaskList(taskList))
		{
			return false;
		}
		if (taskList == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Tasklist is NULL. Can't check for healing and damage metadata.", new object[]
			{
				this
			});
			return false;
		}
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): No source card found.", new object[]
			{
				this
			});
			return false;
		}
		global::Entity entity = sourceCard.GetEntity();
		if (entity == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Current tasklist has no source entity.", new object[]
			{
				this
			});
			return false;
		}
		global::Player controller = entity.GetController();
		if (controller == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Source entity has no controller.", new object[]
			{
				this
			});
			return false;
		}
		global::Entity entity2 = null;
		if (controller.HasTag(GAME_TAG.LIFESTEAL_DAMAGES_OPPOSING_HERO))
		{
			global::Player firstOpponentPlayer = GameState.Get().GetFirstOpponentPlayer(controller);
			if (firstOpponentPlayer != null)
			{
				entity2 = firstOpponentPlayer.GetHero();
			}
			if (entity2 == null)
			{
				Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Opposing entity's controller has no hero.", new object[]
				{
					this
				});
				return false;
			}
		}
		else
		{
			entity2 = controller.GetHero();
			if (entity2 == null)
			{
				Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Source entity's controller has no hero.", new object[]
				{
					this
				});
				return false;
			}
		}
		foreach (PowerTask powerTask in taskList.GetTaskList())
		{
			Network.HistMetaData histMetaData = powerTask.GetPower() as Network.HistMetaData;
			if (histMetaData != null && (histMetaData.MetaType == HistoryMeta.Type.HEALING || histMetaData.MetaType == HistoryMeta.Type.DAMAGE))
			{
				global::Entity entity3 = GameState.Get().GetEntity(histMetaData.Info[0]);
				if (entity3 != null && entity3 == entity2 && !(entity3.GetCard() == null))
				{
					this.SetSource(entity3.GetCard().gameObject);
					return true;
				}
			}
		}
		return false;
	}
}
