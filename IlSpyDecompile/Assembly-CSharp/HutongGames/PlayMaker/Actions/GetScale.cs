using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the Scale of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable")]
	public class GetScale : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;

		[UIHint(UIHint.Variable)]
		public FsmFloat xScale;

		[UIHint(UIHint.Variable)]
		public FsmFloat yScale;

		[UIHint(UIHint.Variable)]
		public FsmFloat zScale;

		public Space space;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			xScale = null;
			yScale = null;
			zScale = null;
			space = Space.World;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetScale();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetScale();
		}

		private void DoGetScale()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				Vector3 value = ((space == Space.World) ? ownerDefaultTarget.transform.lossyScale : ownerDefaultTarget.transform.localScale);
				vector.Value = value;
				xScale.Value = value.x;
				yScale.Value = value.y;
				zScale.Value = value.z;
			}
		}
	}
}
