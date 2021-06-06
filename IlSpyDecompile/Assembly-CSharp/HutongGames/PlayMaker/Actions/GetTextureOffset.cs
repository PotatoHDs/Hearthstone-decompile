using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Gets the Offset of a named texture in a Game Object's Material.")]
	public class GetTextureOffset : ComponentAction<Renderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		public FsmInt materialIndex;

		[UIHint(UIHint.NamedColor)]
		public FsmString namedTexture;

		private Vector2 offset;

		[RequiredField]
		public FsmFloat offsetX;

		[RequiredField]
		public FsmFloat offsetY;

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
			DoGetTextureOffset();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetTextureOffset();
		}

		private void DoGetTextureOffset()
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
				offset = material.GetTextureOffset(namedTexture.Value);
			}
			else if (base.renderer.GetMaterials().Count > materialIndex.Value)
			{
				offset = base.renderer.GetMaterial(materialIndex.Value).GetTextureOffset(namedTexture.Value);
			}
			offsetX.Value = offset.x;
			offsetY.Value = offset.y;
		}
	}
}
