using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Gets a named float value from a game object's material.")]
	public class GetMaterialFloatAction : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[UIHint(UIHint.FsmFloat)]
		[Tooltip("The named float parameter in the shader.")]
		public FsmString namedFloat;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the parameter value.")]
		public FsmFloat floatValue;

		public FsmEvent fail;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			namedFloat = "_Intensity";
			floatValue = null;
			fail = null;
		}

		public override void OnEnter()
		{
			DoGetMaterialfloatValue();
			Finish();
		}

		private void DoGetMaterialfloatValue()
		{
			if (floatValue.IsNone)
			{
				return;
			}
			string text = namedFloat.Value;
			if (text == "")
			{
				text = "_Intensity";
			}
			if (this.material.Value != null)
			{
				if (!this.material.Value.HasProperty(text))
				{
					base.Fsm.Event(fail);
				}
				else
				{
					floatValue.Value = this.material.Value.GetFloat(text);
				}
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Renderer component = ownerDefaultTarget.GetComponent<Renderer>();
			if (component == null)
			{
				LogError("Missing Renderer!");
				return;
			}
			Material material = component.GetMaterial();
			if (material == null)
			{
				LogError("Missing Material!");
			}
			else if (materialIndex.Value == 0)
			{
				if (!material.HasProperty(text))
				{
					base.Fsm.Event(fail);
				}
				else
				{
					floatValue.Value = material.GetFloat(text);
				}
			}
			else if (component.GetMaterials().Count > materialIndex.Value)
			{
				material = component.GetMaterial(materialIndex.Value);
				if (!material.HasProperty(text))
				{
					base.Fsm.Event(fail);
				}
				else
				{
					floatValue.Value = material.GetFloat(text);
				}
			}
		}
	}
}
