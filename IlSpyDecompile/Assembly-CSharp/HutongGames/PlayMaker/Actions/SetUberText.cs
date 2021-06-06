using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the text on an UberText object.")]
	public class SetUberText : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault uberTextObject;

		public FsmString text;

		[Tooltip("Set the UberText every frame. Useful if the text variable is expected to change/animate.")]
		public bool everyFrame;

		public override void Reset()
		{
			uberTextObject = null;
			text = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			UpdateText();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			UpdateText();
		}

		private void UpdateText()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(uberTextObject);
			if (ownerDefaultTarget != null)
			{
				UberText component = ownerDefaultTarget.GetComponent<UberText>();
				if (component != null)
				{
					component.Text = text.Value;
				}
			}
		}
	}
}
