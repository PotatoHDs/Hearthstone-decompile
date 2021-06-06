using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets a named float in multiple game object's material.")]
	public class SetMaterialFloatMultiAction : ComponentAction<Renderer>
	{
		[Tooltip("The GameObjects that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmGameObject[] gameObjectList;

		[RequiredField]
		[Tooltip("A named float parameter in the shader.")]
		public FsmString namedFloat;

		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmFloat floatValue;

		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObjectList = null;
			namedFloat = "";
			floatValue = 0f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetMaterialFloat();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetMaterialFloat();
		}

		private void DoSetMaterialFloat()
		{
			for (int i = 0; i < gameObjectList.Length; i++)
			{
				gameObjectList[i].Value.GetComponent<Renderer>().GetMaterial().SetFloat(namedFloat.Value, floatValue.Value);
			}
		}
	}
}
