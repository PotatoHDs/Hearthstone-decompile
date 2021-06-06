using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the Flare effect used by a Light.")]
	public class SetLightFlare : ComponentAction<Light>
	{
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		public Flare lightFlare;

		public override void Reset()
		{
			gameObject = null;
			lightFlare = null;
		}

		public override void OnEnter()
		{
			DoSetLightRange();
			Finish();
		}

		private void DoSetLightRange()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				base.light.flare = lightFlare;
			}
		}
	}
}
