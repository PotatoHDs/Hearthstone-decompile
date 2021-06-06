using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C70 RID: 3184
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Get a material at index on a gameObject and store it in a variable")]
	public class GetMaterial : ComponentAction<Renderer>
	{
		// Token: 0x06009F7D RID: 40829 RVA: 0x0032E96E File Offset: 0x0032CB6E
		public override void Reset()
		{
			this.gameObject = null;
			this.material = null;
			this.materialIndex = 0;
			this.getSharedMaterial = false;
		}

		// Token: 0x06009F7E RID: 40830 RVA: 0x0032E991 File Offset: 0x0032CB91
		public override void OnEnter()
		{
			this.DoGetMaterial();
			base.Finish();
		}

		// Token: 0x06009F7F RID: 40831 RVA: 0x0032E9A0 File Offset: 0x0032CBA0
		private void DoGetMaterial()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			List<Material> materials = base.renderer.GetMaterials();
			if (this.materialIndex.Value == 0 && !this.getSharedMaterial)
			{
				this.material.Value = base.renderer.GetMaterial();
				return;
			}
			if (this.materialIndex.Value == 0 && this.getSharedMaterial)
			{
				this.material.Value = base.renderer.GetSharedMaterial();
				return;
			}
			if (materials.Count > this.materialIndex.Value && !this.getSharedMaterial)
			{
				this.material.Value = materials[this.materialIndex.Value];
				return;
			}
			if (materials.Count > this.materialIndex.Value && this.getSharedMaterial)
			{
				this.material.Value = materials[this.materialIndex.Value];
			}
		}

		// Token: 0x04008517 RID: 34071
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		[Tooltip("The GameObject the Material is applied to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008518 RID: 34072
		[Tooltip("The index of the Material in the Materials array.")]
		public FsmInt materialIndex;

		// Token: 0x04008519 RID: 34073
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the material in a variable.")]
		public FsmMaterial material;

		// Token: 0x0400851A RID: 34074
		[Tooltip("Get the shared material of this object. NOTE: Modifying the shared material will change the appearance of all objects using this material, and change material settings that are stored in the project too.")]
		public bool getSharedMaterial;
	}
}
