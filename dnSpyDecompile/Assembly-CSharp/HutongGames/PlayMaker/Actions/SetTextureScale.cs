using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DFE RID: 3582
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the Scale of a named texture in a Game Object's Material. Useful for special effects.")]
	public class SetTextureScale : ComponentAction<Renderer>
	{
		// Token: 0x0600A6C1 RID: 42689 RVA: 0x0034A2A4 File Offset: 0x003484A4
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.namedTexture = "_MainTex";
			this.scaleX = 1f;
			this.scaleY = 1f;
			this.everyFrame = false;
		}

		// Token: 0x0600A6C2 RID: 42690 RVA: 0x0034A2FB File Offset: 0x003484FB
		public override void OnEnter()
		{
			this.DoSetTextureScale();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6C3 RID: 42691 RVA: 0x0034A311 File Offset: 0x00348511
		public override void OnUpdate()
		{
			this.DoSetTextureScale();
		}

		// Token: 0x0600A6C4 RID: 42692 RVA: 0x0034A31C File Offset: 0x0034851C
		private void DoSetTextureScale()
		{
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
				material.SetTextureScale(this.namedTexture.Value, new Vector2(this.scaleX.Value, this.scaleY.Value));
				return;
			}
			if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				base.renderer.GetMaterial(this.materialIndex.Value).SetTextureScale(this.namedTexture.Value, new Vector2(this.scaleX.Value, this.scaleY.Value));
			}
		}

		// Token: 0x04008D3D RID: 36157
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D3E RID: 36158
		public FsmInt materialIndex;

		// Token: 0x04008D3F RID: 36159
		[UIHint(UIHint.NamedColor)]
		public FsmString namedTexture;

		// Token: 0x04008D40 RID: 36160
		[RequiredField]
		public FsmFloat scaleX;

		// Token: 0x04008D41 RID: 36161
		[RequiredField]
		public FsmFloat scaleY;

		// Token: 0x04008D42 RID: 36162
		public bool everyFrame;
	}
}
