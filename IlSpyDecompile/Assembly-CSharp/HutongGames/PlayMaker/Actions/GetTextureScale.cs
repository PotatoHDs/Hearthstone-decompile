using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the Scale of a named texture in a Game Object's Material.")]
	public class GetTextureScale : ComponentAction<Renderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		public FsmInt materialIndex;

		[UIHint(UIHint.NamedColor)]
		public FsmString namedTexture;

		private Vector2 scale;

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
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetTextureScale();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetTextureScale();
		}

		private void DoGetTextureScale()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Material material = base.renderer.GetMaterial();
			if (material == null)
			{
				LogError("Missing Material!");
				return;
			}
			if (materialIndex.Value == 0)
			{
				scale = material.GetTextureScale(namedTexture.Value);
			}
			else if (base.renderer.GetMaterials().Count > materialIndex.Value)
			{
				scale = base.renderer.GetMaterial(materialIndex.Value).GetTextureScale(namedTexture.Value);
			}
			scaleX.Value = scale.x;
			scaleY.Value = scale.y;
		}
	}
}
