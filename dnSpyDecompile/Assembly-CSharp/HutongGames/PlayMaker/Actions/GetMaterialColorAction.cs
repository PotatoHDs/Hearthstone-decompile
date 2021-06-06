using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F43 RID: 3907
	[ActionCategory("Pegasus")]
	[Tooltip("Gets a named color value (or any vector4) from a game object's material.")]
	public class GetMaterialColorAction : FsmStateAction
	{
		// Token: 0x0600AC91 RID: 44177 RVA: 0x0035D8D4 File Offset: 0x0035BAD4
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.material = null;
			this.namedColor = "_Color";
			this.colorValue = null;
			this.fail = null;
		}

		// Token: 0x0600AC92 RID: 44178 RVA: 0x0035D90E File Offset: 0x0035BB0E
		public override void OnEnter()
		{
			this.DoGetMaterialcolorValue();
			base.Finish();
		}

		// Token: 0x0600AC93 RID: 44179 RVA: 0x0035D91C File Offset: 0x0035BB1C
		private void DoGetMaterialcolorValue()
		{
			if (this.colorValue.IsNone)
			{
				return;
			}
			string text = this.namedColor.Value;
			if (text == "")
			{
				text = "_Color";
			}
			if (this.material.Value != null)
			{
				if (!this.material.Value.HasProperty(text))
				{
					base.Fsm.Event(this.fail);
					return;
				}
				this.colorValue.Value = this.material.Value.GetVector(text);
				return;
			}
			else
			{
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
				if (ownerDefaultTarget == null)
				{
					return;
				}
				Renderer component = ownerDefaultTarget.GetComponent<Renderer>();
				if (component == null)
				{
					base.LogError("Missing Renderer!");
					return;
				}
				Material material = component.GetMaterial();
				if (material == null)
				{
					base.LogError("Missing Material!");
					return;
				}
				if (this.materialIndex.Value != 0)
				{
					if (component.GetMaterials().Count > this.materialIndex.Value)
					{
						material = component.GetMaterial(this.materialIndex.Value);
						if (!material.HasProperty(text))
						{
							base.Fsm.Event(this.fail);
							return;
						}
						this.colorValue.Value = material.GetVector(text);
					}
					return;
				}
				if (!material.HasProperty(text))
				{
					base.Fsm.Event(this.fail);
					return;
				}
				this.colorValue.Value = material.GetVector(text);
				return;
			}
		}

		// Token: 0x04009362 RID: 37730
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009363 RID: 37731
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		// Token: 0x04009364 RID: 37732
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		// Token: 0x04009365 RID: 37733
		[Tooltip("The named color parameter in the shader.")]
		public FsmString namedColor;

		// Token: 0x04009366 RID: 37734
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the parameter value.")]
		public FsmColor colorValue;

		// Token: 0x04009367 RID: 37735
		public FsmEvent fail;
	}
}
