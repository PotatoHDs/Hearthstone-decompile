using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Lights)]
	[Tooltip("Sets the Texture projected by a Light.")]
	public class SetLightCookie : ComponentAction<Light>
	{
		[RequiredField]
		[CheckForComponent(typeof(Light))]
		public FsmOwnerDefault gameObject;

		public FsmTexture lightCookie;

		public override void Reset()
		{
			gameObject = null;
			lightCookie = null;
		}

		public override void OnEnter()
		{
			DoSetLightCookie();
			Finish();
		}

		private void DoSetLightCookie()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				base.light.cookie = lightCookie.Value;
			}
		}
	}
}
