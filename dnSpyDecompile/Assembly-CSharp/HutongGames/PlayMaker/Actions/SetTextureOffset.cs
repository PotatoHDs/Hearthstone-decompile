using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DFD RID: 3581
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the Offset of a named texture in a Game Object's Material. Useful for scrolling texture effects.")]
	public class SetTextureOffset : ComponentAction<Renderer>
	{
		// Token: 0x0600A6BC RID: 42684 RVA: 0x0034A148 File Offset: 0x00348348
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.namedTexture = "_MainTex";
			this.offsetX = 0f;
			this.offsetY = 0f;
			this.everyFrame = false;
		}

		// Token: 0x0600A6BD RID: 42685 RVA: 0x0034A19F File Offset: 0x0034839F
		public override void OnEnter()
		{
			this.DoSetTextureOffset();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A6BE RID: 42686 RVA: 0x0034A1B5 File Offset: 0x003483B5
		public override void OnUpdate()
		{
			this.DoSetTextureOffset();
		}

		// Token: 0x0600A6BF RID: 42687 RVA: 0x0034A1C0 File Offset: 0x003483C0
		private void DoSetTextureOffset()
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
				material.SetTextureOffset(this.namedTexture.Value, new Vector2(this.offsetX.Value, this.offsetY.Value));
				return;
			}
			if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				base.renderer.GetMaterial(this.materialIndex.Value).SetTextureOffset(this.namedTexture.Value, new Vector2(this.offsetX.Value, this.offsetY.Value));
			}
		}

		// Token: 0x04008D37 RID: 36151
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D38 RID: 36152
		public FsmInt materialIndex;

		// Token: 0x04008D39 RID: 36153
		[RequiredField]
		[UIHint(UIHint.NamedColor)]
		public FsmString namedTexture;

		// Token: 0x04008D3A RID: 36154
		[RequiredField]
		public FsmFloat offsetX;

		// Token: 0x04008D3B RID: 36155
		[RequiredField]
		public FsmFloat offsetY;

		// Token: 0x04008D3C RID: 36156
		public bool everyFrame;
	}
}
