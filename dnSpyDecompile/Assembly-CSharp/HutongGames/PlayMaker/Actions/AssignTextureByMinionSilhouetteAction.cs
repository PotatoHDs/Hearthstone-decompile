using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD0 RID: 3024
	[ActionCategory("Pegasus")]
	[Tooltip("Assigns a texture to a card object based on its rarity and golden status.")]
	public class AssignTextureByMinionSilhouetteAction : SilhouetteAction<FsmTexture>
	{
		// Token: 0x06009CAC RID: 40108 RVA: 0x003262BC File Offset: 0x003244BC
		protected override void AssignAsset(FsmTexture texture)
		{
			this.m_Minion.Value.GetComponent<Renderer>().GetMaterial().mainTexture = texture.Value;
		}
	}
}
