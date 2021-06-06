namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Gets info on the last Trigger event and store in variables.")]
	public class GetTriggerInfo : FsmStateAction
	{
		[UIHint(UIHint.Variable)]
		public FsmGameObject gameObjectHit;

		[UIHint(UIHint.Variable)]
		[Tooltip("Useful for triggering different effects. Audio, particles...")]
		public FsmString physicsMaterialName;

		public override void Reset()
		{
			gameObjectHit = null;
			physicsMaterialName = null;
		}

		private void StoreTriggerInfo()
		{
			if (!(base.Fsm.TriggerCollider == null))
			{
				gameObjectHit.Value = base.Fsm.TriggerCollider.gameObject;
				physicsMaterialName.Value = base.Fsm.TriggerCollider.material.name;
			}
		}

		public override void OnEnter()
		{
			StoreTriggerInfo();
			Finish();
		}
	}
}
