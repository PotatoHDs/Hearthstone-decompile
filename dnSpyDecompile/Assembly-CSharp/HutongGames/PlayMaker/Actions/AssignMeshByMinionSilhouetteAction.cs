using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BCF RID: 3023
	[ActionCategory("Pegasus")]
	[Tooltip("Assigns a mesh to a card object based on its rarity and golden status.")]
	public class AssignMeshByMinionSilhouetteAction : SilhouetteAction<FsmGameObject>
	{
		// Token: 0x06009CAA RID: 40106 RVA: 0x0032628D File Offset: 0x0032448D
		protected override void AssignAsset(FsmGameObject asset)
		{
			this.m_Minion.Value.GetComponent<MeshFilter>().sharedMesh = asset.Value.GetComponent<MeshFilter>().sharedMesh;
		}
	}
}
