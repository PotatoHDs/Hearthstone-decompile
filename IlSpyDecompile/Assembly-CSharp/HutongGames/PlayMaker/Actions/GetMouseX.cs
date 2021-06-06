using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the X Position of the mouse and stores it in a Float Variable.")]
	public class GetMouseX : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;

		public bool normalize;

		public override void Reset()
		{
			storeResult = null;
			normalize = true;
		}

		public override void OnEnter()
		{
			DoGetMouseX();
		}

		public override void OnUpdate()
		{
			DoGetMouseX();
		}

		private void DoGetMouseX()
		{
			if (storeResult != null)
			{
				float num = Input.mousePosition.x;
				if (normalize)
				{
					num /= (float)Screen.width;
				}
				storeResult.Value = num;
			}
		}
	}
}
