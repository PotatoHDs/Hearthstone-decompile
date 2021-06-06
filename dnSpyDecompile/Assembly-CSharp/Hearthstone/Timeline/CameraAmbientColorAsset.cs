using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010E5 RID: 4325
	[Obsolete("Use CameraOverlay instead.", false)]
	public class CameraAmbientColorAsset : PlayableAsset
	{
		// Token: 0x0600BE0F RID: 48655 RVA: 0x0039EEBC File Offset: 0x0039D0BC
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CameraAmbientColorBehaviour>.Create(graph, this.template, 0);
		}

		// Token: 0x04009AB8 RID: 39608
		public CameraAmbientColorBehaviour template;
	}
}
