using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets the material on a game object.")]
	public class SetMaterial : ComponentAction<Renderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		public FsmInt materialIndex;

		[RequiredField]
		public FsmMaterial material;

		public override void Reset()
		{
			gameObject = null;
			material = null;
			materialIndex = 0;
		}

		public override void OnEnter()
		{
			DoSetMaterial();
			Finish();
		}

		private void DoSetMaterial()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				if (materialIndex.Value == 0)
				{
					base.renderer.SetMaterial(material.Value);
				}
				else if (base.renderer.GetMaterials().Count > materialIndex.Value)
				{
					base.renderer.SetMaterial(materialIndex.Value, material.Value);
				}
			}
		}
	}
}
