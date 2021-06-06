using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Get a texture from a material on a GameObject")]
	public class GetMaterialTexture : ComponentAction<Renderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		[Tooltip("The GameObject the Material is applied to.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The index of the Material in the Materials array.")]
		public FsmInt materialIndex;

		[UIHint(UIHint.NamedTexture)]
		[Tooltip("The texture to get. See Unity Shader docs for names.")]
		public FsmString namedTexture;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Title("StoreTexture")]
		[Tooltip("Store the texture in a variable.")]
		public FsmTexture storedTexture;

		[Tooltip("Get the shared version of the texture.")]
		public bool getFromSharedMaterial;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			namedTexture = "_MainTex";
			storedTexture = null;
			getFromSharedMaterial = false;
		}

		public override void OnEnter()
		{
			DoGetMaterialTexture();
			Finish();
		}

		private void DoGetMaterialTexture()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				string text = namedTexture.Value;
				if (text == "")
				{
					text = "_MainTex";
				}
				List<Material> materials = base.renderer.GetMaterials();
				if (materialIndex.Value == 0 && !getFromSharedMaterial)
				{
					storedTexture.Value = base.renderer.GetMaterial().GetTexture(text);
				}
				else if (materialIndex.Value == 0 && getFromSharedMaterial)
				{
					storedTexture.Value = base.renderer.GetSharedMaterial().GetTexture(text);
				}
				else if (materials.Count > materialIndex.Value && !getFromSharedMaterial)
				{
					storedTexture.Value = materials[materialIndex.Value].GetTexture(text);
				}
				else if (materials.Count > materialIndex.Value && getFromSharedMaterial)
				{
					storedTexture.Value = materials[materialIndex.Value].GetTexture(text);
				}
			}
		}
	}
}
