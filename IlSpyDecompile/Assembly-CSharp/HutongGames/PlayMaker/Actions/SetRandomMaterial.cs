using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a Game Object's material randomly from an array of Materials.")]
	public class SetRandomMaterial : ComponentAction<Renderer>
	{
		[RequiredField]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		public FsmInt materialIndex;

		public FsmMaterial[] materials;

		public override void Reset()
		{
			gameObject = null;
			materialIndex = 0;
			materials = new FsmMaterial[3];
		}

		public override void OnEnter()
		{
			DoSetRandomMaterial();
			Finish();
		}

		private void DoSetRandomMaterial()
		{
			if (materials == null || materials.Length == 0)
			{
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				if (base.renderer.GetMaterial() == null)
				{
					LogError("Missing Material!");
				}
				else if (materialIndex.Value == 0)
				{
					base.renderer.SetMaterial(materials[Random.Range(0, materials.Length)].Value);
				}
				else if (base.renderer.GetMaterials().Count > materialIndex.Value)
				{
					base.renderer.SetMaterial(materialIndex.Value, materials[Random.Range(0, materials.Length)].Value);
				}
			}
		}
	}
}
