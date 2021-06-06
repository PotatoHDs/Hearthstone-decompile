using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Assigns a material to a card object based on its rarity and golden status.")]
	public class AssignMaterialByMinionSilhouetteAction : SilhouetteAction<FsmMaterial>
	{
		protected override void AssignAsset(FsmMaterial asset)
		{
			m_Minion.Value.GetComponent<Renderer>().SetMaterial(asset.Value);
		}
	}
}
