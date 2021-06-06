using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Mass of a Game Object's Rigid Body.")]
	public class SetMass : ComponentAction<Rigidbody>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[HasFloatSlider(0.1f, 10f)]
		public FsmFloat mass;

		public override void Reset()
		{
			gameObject = null;
			mass = 1f;
		}

		public override void OnEnter()
		{
			DoSetMass();
			Finish();
		}

		private void DoSetMass()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				base.rigidbody.mass = mass.Value;
			}
		}
	}
}
