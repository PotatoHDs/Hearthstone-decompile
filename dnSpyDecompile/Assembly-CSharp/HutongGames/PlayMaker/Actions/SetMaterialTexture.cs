using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DEA RID: 3562
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named texture in a game object's material.")]
	public class SetMaterialTexture : ComponentAction<Renderer>
	{
		// Token: 0x0600A666 RID: 42598 RVA: 0x003491E4 File Offset: 0x003473E4
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.material = null;
			this.namedTexture = "_MainTex";
			this.texture = null;
		}

		// Token: 0x0600A667 RID: 42599 RVA: 0x00349217 File Offset: 0x00347417
		public override void OnEnter()
		{
			this.DoSetMaterialTexture();
			base.Finish();
		}

		// Token: 0x0600A668 RID: 42600 RVA: 0x00349228 File Offset: 0x00347428
		private void DoSetMaterialTexture()
		{
			string text = this.namedTexture.Value;
			if (text == "")
			{
				text = "_MainTex";
			}
			if (this.material.Value != null)
			{
				this.material.Value.SetTexture(text, this.texture.Value);
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
				material.SetTexture(text, this.texture.Value);
				return;
			}
			if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				base.renderer.GetMaterial(this.materialIndex.Value).SetTexture(text, this.texture.Value);
			}
		}

		// Token: 0x04008CEA RID: 36074
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CEB RID: 36075
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		// Token: 0x04008CEC RID: 36076
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		// Token: 0x04008CED RID: 36077
		[UIHint(UIHint.NamedTexture)]
		[Tooltip("A named parameter in the shader.")]
		public FsmString namedTexture;

		// Token: 0x04008CEE RID: 36078
		public FsmTexture texture;
	}
}
