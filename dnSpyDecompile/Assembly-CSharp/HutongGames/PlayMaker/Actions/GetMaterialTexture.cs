using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C71 RID: 3185
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Get a texture from a material on a GameObject")]
	public class GetMaterialTexture : ComponentAction<Renderer>
	{
		// Token: 0x06009F81 RID: 40833 RVA: 0x0032EA9C File Offset: 0x0032CC9C
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.namedTexture = "_MainTex";
			this.storedTexture = null;
			this.getFromSharedMaterial = false;
		}

		// Token: 0x06009F82 RID: 40834 RVA: 0x0032EACF File Offset: 0x0032CCCF
		public override void OnEnter()
		{
			this.DoGetMaterialTexture();
			base.Finish();
		}

		// Token: 0x06009F83 RID: 40835 RVA: 0x0032EAE0 File Offset: 0x0032CCE0
		private void DoGetMaterialTexture()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			string text = this.namedTexture.Value;
			if (text == "")
			{
				text = "_MainTex";
			}
			List<Material> materials = base.renderer.GetMaterials();
			if (this.materialIndex.Value == 0 && !this.getFromSharedMaterial)
			{
				this.storedTexture.Value = base.renderer.GetMaterial().GetTexture(text);
				return;
			}
			if (this.materialIndex.Value == 0 && this.getFromSharedMaterial)
			{
				this.storedTexture.Value = base.renderer.GetSharedMaterial().GetTexture(text);
				return;
			}
			if (materials.Count > this.materialIndex.Value && !this.getFromSharedMaterial)
			{
				this.storedTexture.Value = materials[this.materialIndex.Value].GetTexture(text);
				return;
			}
			if (materials.Count > this.materialIndex.Value && this.getFromSharedMaterial)
			{
				this.storedTexture.Value = materials[this.materialIndex.Value].GetTexture(text);
			}
		}

		// Token: 0x0400851B RID: 34075
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		[Tooltip("The GameObject the Material is applied to.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400851C RID: 34076
		[Tooltip("The index of the Material in the Materials array.")]
		public FsmInt materialIndex;

		// Token: 0x0400851D RID: 34077
		[UIHint(UIHint.NamedTexture)]
		[Tooltip("The texture to get. See Unity Shader docs for names.")]
		public FsmString namedTexture;

		// Token: 0x0400851E RID: 34078
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Title("StoreTexture")]
		[Tooltip("Store the texture in a variable.")]
		public FsmTexture storedTexture;

		// Token: 0x0400851F RID: 34079
		[Tooltip("Get the shared version of the texture.")]
		public bool getFromSharedMaterial;
	}
}
