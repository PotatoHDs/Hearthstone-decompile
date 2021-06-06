using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE6 RID: 3558
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the material on a game object.")]
	public class SetMaterial : ComponentAction<Renderer>
	{
		// Token: 0x0600A654 RID: 42580 RVA: 0x00348D1A File Offset: 0x00346F1A
		public override void Reset()
		{
			this.gameObject = null;
			this.material = null;
			this.materialIndex = 0;
		}

		// Token: 0x0600A655 RID: 42581 RVA: 0x00348D36 File Offset: 0x00346F36
		public override void OnEnter()
		{
			this.DoSetMaterial();
			base.Finish();
		}

		// Token: 0x0600A656 RID: 42582 RVA: 0x00348D44 File Offset: 0x00346F44
		private void DoSetMaterial()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			if (this.materialIndex.Value == 0)
			{
				base.renderer.SetMaterial(this.material.Value);
				return;
			}
			if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				base.renderer.SetMaterial(this.materialIndex.Value, this.material.Value);
			}
		}

		// Token: 0x04008CD6 RID: 36054
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CD7 RID: 36055
		public FsmInt materialIndex;

		// Token: 0x04008CD8 RID: 36056
		[RequiredField]
		public FsmMaterial material;
	}
}
