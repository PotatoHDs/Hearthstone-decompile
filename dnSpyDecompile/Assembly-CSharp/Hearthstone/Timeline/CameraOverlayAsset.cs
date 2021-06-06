using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010E8 RID: 4328
	public class CameraOverlayAsset : PlayableAsset
	{
		// Token: 0x0600BE15 RID: 48661 RVA: 0x0039EF63 File Offset: 0x0039D163
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CameraOverlayBehaviour>.Create(graph, this.template, 0);
		}

		// Token: 0x04009ABA RID: 39610
		public CameraOverlayBehaviour template;
	}
}
