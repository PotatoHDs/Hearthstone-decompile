using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Forces a Game Object's Rigid Body to wake up.")]
	public class WakeUp : ComponentAction<Rigidbody>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
			DoWakeUp();
			Finish();
		}

		private void DoWakeUp()
		{
			GameObject go = ((gameObject.OwnerOption == OwnerDefaultOption.UseOwner) ? base.Owner : gameObject.GameObject.Value);
			if (UpdateCache(go))
			{
				base.rigidbody.WakeUp();
			}
		}
	}
}
