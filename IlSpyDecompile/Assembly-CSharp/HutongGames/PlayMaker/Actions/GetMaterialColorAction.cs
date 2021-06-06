using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Gets a named color value (or any vector4) from a game object's material.")]
	public class GetMaterialColorAction : FsmStateAction
	{
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		[Tooltip("The named color parameter in the shader.")]
		public FsmString namedColor;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the parameter value.")]
		public FsmColor colorValue;

		public FsmEvent fail;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			material = null;
			namedColor = "_Color";
			colorValue = null;
			fail = null;
		}

		public override void OnEnter()
		{
			DoGetMaterialcolorValue();
			Finish();
		}

		private void DoGetMaterialcolorValue()
		{
			if (colorValue.IsNone)
			{
				return;
			}
			string text = namedColor.Value;
			if (text == "")
			{
				text = "_Color";
			}
			if (this.material.Value != null)
			{
				if (!this.material.Value.HasProperty(text))
				{
					base.Fsm.Event(fail);
				}
				else
				{
					colorValue.Value = this.material.Value.GetVector(text);
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
					colorValue.Value = material.GetVector(text);
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
					colorValue.Value = material.GetVector(text);
				}
			}
		}
	}
}
