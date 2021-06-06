using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F76 RID: 3958
	[ActionCategory("Pegasus")]
	[Tooltip("Sets a named float in a game object's material.")]
	public class SetMaterialFloatAction : FsmStateAction
	{
		// Token: 0x0600AD61 RID: 44385 RVA: 0x00360CB8 File Offset: 0x0035EEB8
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.material = null;
			this.namedFloat = "";
			this.floatValue = 0f;
			this.everyFrame = false;
		}

		// Token: 0x0600AD62 RID: 44386 RVA: 0x00360D06 File Offset: 0x0035EF06
		public override void OnEnter()
		{
			this.DoSetMaterialFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AD63 RID: 44387 RVA: 0x00360D1C File Offset: 0x0035EF1C
		public override void OnUpdate()
		{
			this.DoSetMaterialFloat();
		}

		// Token: 0x0600AD64 RID: 44388 RVA: 0x00360D24 File Offset: 0x0035EF24
		private void DoSetMaterialFloat()
		{
			if (this.material.Value != null)
			{
				this.material.Value.SetFloat(this.namedFloat.Value, this.floatValue.Value);
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Renderer component = ownerDefaultTarget.GetComponent<Renderer>();
			if (component == null)
			{
				component = ownerDefaultTarget.GetComponent<SkinnedMeshRenderer>();
				if (component == null)
				{
					base.LogError("Missing Renderer!");
					return;
				}
			}
			if (this.materialIndex.Value == 0)
			{
				component.GetMaterial().SetFloat(this.namedFloat.Value, this.floatValue.Value);
				return;
			}
			if (component.GetMaterials().Count > this.materialIndex.Value)
			{
				component.GetMaterial(this.materialIndex.Value).SetFloat(this.namedFloat.Value, this.floatValue.Value);
			}
		}

		// Token: 0x0400943E RID: 37950
		[Tooltip("The GameObject that the material is applied to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400943F RID: 37951
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		// Token: 0x04009440 RID: 37952
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		// Token: 0x04009441 RID: 37953
		[RequiredField]
		[Tooltip("A named float parameter in the shader.")]
		public FsmString namedFloat;

		// Token: 0x04009442 RID: 37954
		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmFloat floatValue;

		// Token: 0x04009443 RID: 37955
		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;
	}
}
