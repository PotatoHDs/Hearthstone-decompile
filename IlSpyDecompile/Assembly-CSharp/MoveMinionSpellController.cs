public class MoveMinionSpellController : SpellController
{
	protected override void OnProcessTaskList()
	{
		OnFinishedTaskList();
		OnFinished();
	}
}
