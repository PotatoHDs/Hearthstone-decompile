using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BCE RID: 3022
	[ActionCategory("Pegasus")]
	[Tooltip("Assigns a material to a card object based on its rarity and golden status.")]
	public class AssignMaterialByMinionSilhouetteAction : SilhouetteAction<FsmMaterial>
	{
		// Token: 0x06009CA8 RID: 40104 RVA: 0x00326268 File Offset: 0x00324468
		protected override void AssignAsset(FsmMaterial asset)
		{
			this.m_Minion.Value.GetComponent<Renderer>().SetMaterial(asset.Value);
		}
	}
}
