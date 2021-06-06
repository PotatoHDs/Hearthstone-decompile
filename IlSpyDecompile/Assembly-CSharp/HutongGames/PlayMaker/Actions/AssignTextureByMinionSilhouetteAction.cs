using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Assigns a texture to a card object based on its rarity and golden status.")]
	public class AssignTextureByMinionSilhouetteAction : SilhouetteAction<FsmTexture>
	{
		protected override void AssignAsset(FsmTexture texture)
		{
			m_Minion.Value.GetComponent<Renderer>().GetMaterial().mainTexture = texture.Value;
		}
	}
}
