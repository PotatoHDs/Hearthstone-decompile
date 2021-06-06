public class PearlOfTidesSpell : SuperSpell
{
	protected override void OnAction(SpellStateType prevStateType)
	{
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			Network.HistFullEntity histFullEntity = task.GetPower() as Network.HistFullEntity;
			if (histFullEntity != null)
			{
				GameState.Get().GetEntity(histFullEntity.Entity.ID).GetCard()
					.SuppressPlaySounds(suppress: true);
			}
		}
		base.OnAction(prevStateType);
	}
}
