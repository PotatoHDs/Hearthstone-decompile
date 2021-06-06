using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE8 RID: 3560
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named float in a game object's material.")]
	public class SetMaterialFloat : ComponentAction<Renderer>
	{
		// Token: 0x0600A65D RID: 42589 RVA: 0x00348F48 File Offset: 0x00347148
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.material = null;
			this.namedFloat = "";
			this.floatValue = 0f;
			this.everyFrame = false;
		}

		// Token: 0x0600A65E RID: 42590 RVA: 0x00348F96 File Offset: 0x00347196
		public override void OnEnter()
		{
			this.DoSetMaterialFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A65F RID: 42591 RVA: 0x00348FAC File Offset: 0x003471AC
		public override void OnUpdate()
		{
			this.DoSetMaterialFloat();
		}

		// Token: 0x0600A660 RID: 42592 RVA: 0x00348FB4 File Offset: 0x003471B4
		private void DoSetMaterialFloat()
		{
			if (this.material.Value != null)
			{
				this.material.Value.SetFloat(this.namedFloat.Value, this.floatValue.Value);
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Material material = base.renderer.GetMaterial();
			if (material == null)
			{
				base.LogError("Missing Material!");
				return;
			}
			if (this.materialIndex.Value == 0)
			{
				material.SetFloat(this.namedFloat.Value, this.floatValue.Value);
				return;
			}
			if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				base.renderer.GetMaterial(this.materialIndex.Value).SetFloat(this.namedFloat.Value, this.floatValue.Value);
			}
		}

		// Token: 0x04008CDF RID: 36063
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CE0 RID: 36064
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		// Token: 0x04008CE1 RID: 36065
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		// Token: 0x04008CE2 RID: 36066
		[RequiredField]
		[Tooltip("A named float parameter in the shader.")]
		public FsmString namedFloat;

		// Token: 0x04008CE3 RID: 36067
		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmFloat floatValue;

		// Token: 0x04008CE4 RID: 36068
		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;
	}
}
