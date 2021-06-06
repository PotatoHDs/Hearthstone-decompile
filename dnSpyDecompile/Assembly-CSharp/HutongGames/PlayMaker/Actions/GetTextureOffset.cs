using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F46 RID: 3910
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the Offset of a named texture in a Game Object's Material.")]
	public class GetTextureOffset : ComponentAction<Renderer>
	{
		// Token: 0x0600AC9D RID: 44189 RVA: 0x0035DCA3 File Offset: 0x0035BEA3
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.namedTexture = "_MainTex";
			this.everyFrame = false;
		}

		// Token: 0x0600AC9E RID: 44190 RVA: 0x0035DCCF File Offset: 0x0035BECF
		public override void OnEnter()
		{
			this.DoGetTextureOffset();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AC9F RID: 44191 RVA: 0x0035DCE5 File Offset: 0x0035BEE5
		public override void OnUpdate()
		{
			this.DoGetTextureOffset();
		}

		// Token: 0x0600ACA0 RID: 44192 RVA: 0x0035DCF0 File Offset: 0x0035BEF0
		private void DoGetTextureOffset()
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
				this.offset = material.GetTextureOffset(this.namedTexture.Value);
			}
			else if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				this.offset = base.renderer.GetMaterial(this.materialIndex.Value).GetTextureOffset(this.namedTexture.Value);
			}
			this.offsetX.Value = this.offset.x;
			this.offsetY.Value = this.offset.y;
		}

		// Token: 0x0400936F RID: 37743
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009370 RID: 37744
		public FsmInt materialIndex;

		// Token: 0x04009371 RID: 37745
		[UIHint(UIHint.NamedColor)]
		public FsmString namedTexture;

		// Token: 0x04009372 RID: 37746
		private Vector2 offset;

		// Token: 0x04009373 RID: 37747
		[RequiredField]
		public FsmFloat offsetX;

		// Token: 0x04009374 RID: 37748
		[RequiredField]
		public FsmFloat offsetY;

		// Token: 0x04009375 RID: 37749
		public bool everyFrame;
	}
}
