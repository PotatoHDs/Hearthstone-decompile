using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the Scale of a named texture in a Game Object's Material. Useful for special effects.")]
	public class SetTextureScale : ComponentAction<Renderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		public FsmInt materialIndex;

		[UIHint(UIHint.NamedColor)]
		public FsmString namedTexture;

		[RequiredField]
		public FsmFloat scaleX;

		[RequiredField]
		public FsmFloat scaleY;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			namedTexture = "_MainTex";
			scaleX = 1f;
			scaleY = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetTextureScale();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetTextureScale();
		}

		private void DoSetTextureScale()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				Material material = base.renderer.GetMaterial();
				if (material == null)
				{
					LogError("Missing Material!");
				}
				else if (materialIndex.Value == 0)
				{
					material.SetTextureScale(namedTexture.Value, new Vector2(scaleX.Value, scaleY.Value));
				}
				else if (base.renderer.GetMaterials().Count > materialIndex.Value)
				{
					base.renderer.GetMaterial(materialIndex.Value).SetTextureScale(namedTexture.Value, new Vector2(scaleX.Value, scaleY.Value));
				}
			}
		}
	}
}
