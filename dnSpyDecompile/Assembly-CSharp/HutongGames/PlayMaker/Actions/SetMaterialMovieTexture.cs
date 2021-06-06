using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE9 RID: 3561
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named texture in a game object's material to a movie texture.")]
	public class SetMaterialMovieTexture : ComponentAction<Renderer>
	{
		// Token: 0x0600A662 RID: 42594 RVA: 0x003490B0 File Offset: 0x003472B0
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.material = null;
			this.namedTexture = "_MainTex";
			this.movieTexture = null;
		}

		// Token: 0x0600A663 RID: 42595 RVA: 0x003490E3 File Offset: 0x003472E3
		public override void OnEnter()
		{
			this.DoSetMaterialTexture();
			base.Finish();
		}

		// Token: 0x0600A664 RID: 42596 RVA: 0x003490F4 File Offset: 0x003472F4
		private void DoSetMaterialTexture()
		{
			MovieTexture value = this.movieTexture.Value as MovieTexture;
			string text = this.namedTexture.Value;
			if (text == "")
			{
				text = "_MainTex";
			}
			if (this.material.Value != null)
			{
				this.material.Value.SetTexture(text, value);
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
				material.SetTexture(text, value);
				return;
			}
			if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				base.renderer.GetMaterial(this.materialIndex.Value).SetTexture(text, value);
			}
		}

		// Token: 0x04008CE5 RID: 36069
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CE6 RID: 36070
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		// Token: 0x04008CE7 RID: 36071
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		// Token: 0x04008CE8 RID: 36072
		[UIHint(UIHint.NamedTexture)]
		[Tooltip("A named texture in the shader.")]
		public FsmString namedTexture;

		// Token: 0x04008CE9 RID: 36073
		[RequiredField]
		[ObjectType(typeof(MovieTexture))]
		public FsmObject movieTexture;
	}
}
