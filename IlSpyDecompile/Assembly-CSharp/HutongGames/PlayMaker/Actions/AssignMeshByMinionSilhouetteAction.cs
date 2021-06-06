using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Assigns a mesh to a card object based on its rarity and golden status.")]
	public class AssignMeshByMinionSilhouetteAction : SilhouetteAction<FsmGameObject>
	{
		protected override void AssignAsset(FsmGameObject asset)
		{
			m_Minion.Value.GetComponent<MeshFilter>().sharedMesh = asset.Value.GetComponent<MeshFilter>().sharedMesh;
		}
	}
}
