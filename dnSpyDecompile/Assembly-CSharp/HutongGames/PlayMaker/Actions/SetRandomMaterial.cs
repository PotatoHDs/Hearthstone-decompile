using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DF2 RID: 3570
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a Game Object's material randomly from an array of Materials.")]
	public class SetRandomMaterial : ComponentAction<Renderer>
	{
		// Token: 0x0600A687 RID: 42631 RVA: 0x00349786 File Offset: 0x00347986
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.materials = new FsmMaterial[3];
		}

		// Token: 0x0600A688 RID: 42632 RVA: 0x003497A7 File Offset: 0x003479A7
		public override void OnEnter()
		{
			this.DoSetRandomMaterial();
			base.Finish();
		}

		// Token: 0x0600A689 RID: 42633 RVA: 0x003497B8 File Offset: 0x003479B8
		private void DoSetRandomMaterial()
		{
			if (this.materials == null)
			{
				return;
			}
			if (this.materials.Length == 0)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			if (base.renderer.GetMaterial() == null)
			{
				base.LogError("Missing Material!");
				return;
			}
			if (this.materialIndex.Value == 0)
			{
				base.renderer.SetMaterial(this.materials[UnityEngine.Random.Range(0, this.materials.Length)].Value);
				return;
			}
			if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				base.renderer.SetMaterial(this.materialIndex.Value, this.materials[UnityEngine.Random.Range(0, this.materials.Length)].Value);
			}
		}

		// Token: 0x04008D09 RID: 36105
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D0A RID: 36106
		public FsmInt materialIndex;

		// Token: 0x04008D0B RID: 36107
		public FsmMaterial[] materials;
	}
}
