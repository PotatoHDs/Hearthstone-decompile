using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named float in a game object's material.")]
	public class SetMaterialFloat : ComponentAction<Renderer>
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

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
			gameObject = null;
			materialIndex = 0;
			material = null;
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
			if (this.material.Value != null)
			{
				this.material.Value.SetFloat(namedFloat.Value, floatValue.Value);
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				Material material = base.renderer.GetMaterial();
				if (material == null)
				{
					LogError("Missing Material!");
				}
				else if (materialIndex.Value == 0)
				{
					material.SetFloat(namedFloat.Value, floatValue.Value);
				}
				else if (base.renderer.GetMaterials().Count > materialIndex.Value)
				{
					base.renderer.GetMaterial(materialIndex.Value).SetFloat(namedFloat.Value, floatValue.Value);
				}
			}
		}
	}
}
