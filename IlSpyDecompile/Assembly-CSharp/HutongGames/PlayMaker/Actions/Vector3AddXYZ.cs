using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Adds a XYZ values to Vector3 Variable.")]
	public class Vector3AddXYZ : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 vector3Variable;

		public FsmFloat addX;

		public FsmFloat addY;

		public FsmFloat addZ;

		public bool everyFrame;

		public bool perSecond;

		public override void Reset()
		{
			vector3Variable = null;
			addX = 0f;
			addY = 0f;
			addZ = 0f;
			everyFrame = false;
			perSecond = false;
		}

		public override void OnEnter()
		{
			DoVector3AddXYZ();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoVector3AddXYZ();
		}

		private void DoVector3AddXYZ()
		{
			Vector3 vector = new Vector3(addX.Value, addY.Value, addZ.Value);
			if (perSecond)
			{
				vector3Variable.Value += vector * Time.deltaTime;
			}
			else
			{
				vector3Variable.Value += vector;
			}
		}
	}
}
