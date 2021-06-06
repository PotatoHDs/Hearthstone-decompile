using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F47 RID: 3911
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the Scale of a named texture in a Game Object's Material.")]
	public class GetTextureScale : ComponentAction<Renderer>
	{
		// Token: 0x0600ACA2 RID: 44194 RVA: 0x0035DDD5 File Offset: 0x0035BFD5
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.namedTexture = "_MainTex";
			this.everyFrame = false;
		}

		// Token: 0x0600ACA3 RID: 44195 RVA: 0x0035DE01 File Offset: 0x0035C001
		public override void OnEnter()
		{
			this.DoGetTextureScale();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600ACA4 RID: 44196 RVA: 0x0035DE17 File Offset: 0x0035C017
		public override void OnUpdate()
		{
			this.DoGetTextureScale();
		}

		// Token: 0x0600ACA5 RID: 44197 RVA: 0x0035DE20 File Offset: 0x0035C020
		private void DoGetTextureScale()
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
				this.scale = material.GetTextureScale(this.namedTexture.Value);
			}
			else if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				this.scale = base.renderer.GetMaterial(this.materialIndex.Value).GetTextureScale(this.namedTexture.Value);
			}
			this.scaleX.Value = this.scale.x;
			this.scaleY.Value = this.scale.y;
		}

		// Token: 0x04009376 RID: 37750
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04009377 RID: 37751
		public FsmInt materialIndex;

		// Token: 0x04009378 RID: 37752
		[UIHint(UIHint.NamedColor)]
		public FsmString namedTexture;

		// Token: 0x04009379 RID: 37753
		private Vector2 scale;

		// Token: 0x0400937A RID: 37754
		[RequiredField]
		public FsmFloat scaleX;

		// Token: 0x0400937B RID: 37755
		[RequiredField]
		public FsmFloat scaleY;

		// Token: 0x0400937C RID: 37756
		public bool everyFrame;
	}
}
