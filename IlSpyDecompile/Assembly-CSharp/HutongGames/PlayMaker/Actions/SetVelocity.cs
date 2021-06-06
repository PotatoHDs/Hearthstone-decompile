using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics)]
	[Tooltip("Sets the Velocity of a Game Object. To leave any axis unchanged, set variable to 'None'. NOTE: Game object must have a rigidbody.")]
	public class SetVelocity : ComponentAction<Rigidbody>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		public FsmVector3 vector;

		public FsmFloat x;

		public FsmFloat y;

		public FsmFloat z;

		public Space space;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			x = new FsmFloat
			{
				UseVariable = true
			};
			y = new FsmFloat
			{
				UseVariable = true
			};
			z = new FsmFloat
			{
				UseVariable = true
			};
			space = Space.Self;
			everyFrame = false;
		}

		public override void OnPreprocess()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			DoSetVelocity();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnFixedUpdate()
		{
			DoSetVelocity();
			if (!everyFrame)
			{
				Finish();
			}
		}

		private void DoSetVelocity()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				Vector3 vector = ((!this.vector.IsNone) ? this.vector.Value : ((space == Space.World) ? base.rigidbody.velocity : ownerDefaultTarget.transform.InverseTransformDirection(base.rigidbody.velocity)));
				if (!x.IsNone)
				{
					vector.x = x.Value;
				}
				if (!y.IsNone)
				{
					vector.y = y.Value;
				}
				if (!z.IsNone)
				{
					vector.z = z.Value;
				}
				base.rigidbody.velocity = ((space == Space.World) ? vector : ownerDefaultTarget.transform.TransformDirection(vector));
			}
		}
	}
}
