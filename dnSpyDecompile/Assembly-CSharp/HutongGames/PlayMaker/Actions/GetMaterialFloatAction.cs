using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F44 RID: 3908
	[ActionCategory("Pegasus")]
	[Tooltip("Gets a named float value from a game object's material.")]
	public class GetMaterialFloatAction : FsmStateAction
	{
		// Token: 0x0600AC95 RID: 44181 RVA: 0x0035DAA0 File Offset: 0x0035BCA0
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.material = null;
			this.namedFloat = "_Intensity";
			this.floatValue = null;
			this.fail = null;
		}

		// Token: 0x0600AC96 RID: 44182 RVA: 0x0035DADA File Offset: 0x0035BCDA
		public override void OnEnter()
		{
			this.DoGetMaterialfloatValue();
			base.Finish();
		}

		// Token: 0x0600AC97 RID: 44183 RVA: 0x0035DAE8 File Offset: 0x0035BCE8
		private void DoGetMaterialfloatValue()
		{
			if (this.floatValue.IsNone)
			{
				return;
			}
			string text = this.namedFloat.Value;
			if (text == "")
			{
				text = "_Intensity";
			}
			if (this.material.Value != null)
			{
				if (!this.material.Value.HasProperty(text))
				{
					base.Fsm.Event(this.fail);
					return;
				}
				this.floatValue.Value = this.material.Value.GetFloat(text);
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
						this.floatValue.Value = material.GetFloat(text);
					}
					return;
				}
				if (!material.HasProperty(text))
				{
					base.Fsm.Event(this.fail);
					return;
				}
				this.floatValue.Value = material.GetFloat(text);
				return;
			}
		}

		// Token: 0x04009368 RID: 37736
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009369 RID: 37737
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		// Token: 0x0400936A RID: 37738
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		// Token: 0x0400936B RID: 37739
		[UIHint(UIHint.FsmFloat)]
		[Tooltip("The named float parameter in the shader.")]
		public FsmString namedFloat;

		// Token: 0x0400936C RID: 37740
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Get the parameter value.")]
		public FsmFloat floatValue;

		// Token: 0x0400936D RID: 37741
		public FsmEvent fail;
	}
}
