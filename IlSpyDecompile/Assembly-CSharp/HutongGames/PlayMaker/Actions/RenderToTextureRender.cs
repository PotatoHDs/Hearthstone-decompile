using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Triggers a render to texture to render.")]
	public class RenderToTextureRender : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault r2tObject;

		public bool now;

		public override void Reset()
		{
			r2tObject = null;
			now = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(r2tObject);
			if (ownerDefaultTarget != null)
			{
				RenderToTexture component = ownerDefaultTarget.GetComponent<RenderToTexture>();
				if (component != null)
				{
					if (now)
					{
						component.RenderNow();
					}
					else
					{
						component.Render();
					}
				}
			}
			Finish();
		}
	}
}
