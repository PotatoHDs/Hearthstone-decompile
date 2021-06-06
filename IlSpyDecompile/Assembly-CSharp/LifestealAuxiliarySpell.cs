using PegasusGame;

public class LifestealAuxiliarySpell : Spell
{
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (!base.AttachPowerTaskList(taskList))
		{
			return false;
		}
		if (taskList == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Tasklist is NULL. Can't check for healing and damage metadata.", this);
			return false;
		}
		Card sourceCard = GetSourceCard();
		if (sourceCard == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): No source card found.", this);
			return false;
		}
		Entity entity = sourceCard.GetEntity();
		if (entity == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Current tasklist has no source entity.", this);
			return false;
		}
		Player controller = entity.GetController();
		if (controller == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Source entity has no controller.", this);
			return false;
		}
		Entity entity2 = null;
		if (controller.HasTag(GAME_TAG.LIFESTEAL_DAMAGES_OPPOSING_HERO))
		{
			Player firstOpponentPlayer = GameState.Get().GetFirstOpponentPlayer(controller);
			if (firstOpponentPlayer != null)
			{
				entity2 = firstOpponentPlayer.GetHero();
			}
			if (entity2 == null)
			{
				Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Opposing entity's controller has no hero.", this);
				return false;
			}
		}
		else
		{
			entity2 = controller.GetHero();
			if (entity2 == null)
			{
				Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Source entity's controller has no hero.", this);
				return false;
			}
		}
		foreach (PowerTask task in taskList.GetTaskList())
		{
			Network.HistMetaData histMetaData = task.GetPower() as Network.HistMetaData;
			if (histMetaData != null && (histMetaData.MetaType == HistoryMeta.Type.HEALING || histMetaData.MetaType == HistoryMeta.Type.DAMAGE))
			{
				Entity entity3 = GameState.Get().GetEntity(histMetaData.Info[0]);
				if (entity3 != null && entity3 == entity2 && !(entity3.GetCard() == null))
				{
					SetSource(entity3.GetCard().gameObject);
					return true;
				}
			}
		}
		return false;
	}
}
