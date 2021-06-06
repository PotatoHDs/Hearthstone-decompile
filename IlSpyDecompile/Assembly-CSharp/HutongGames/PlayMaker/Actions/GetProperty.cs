using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UnityObject)]
	[ActionTarget(typeof(Component), "targetProperty", false)]
	[ActionTarget(typeof(GameObject), "targetProperty", false)]
	[Tooltip("Gets the value of any public property or field on the targeted Unity Object and stores it in a variable. E.g., Drag and drop any component attached to a Game Object to access its properties.")]
	public class GetProperty : FsmStateAction
	{
		public FsmProperty targetProperty;

		public bool everyFrame;

		public override void Reset()
		{
			targetProperty = new FsmProperty
			{
				setProperty = false
			};
			everyFrame = false;
		}

		public override void OnEnter()
		{
			targetProperty.GetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			targetProperty.GetValue();
		}
	}
}
