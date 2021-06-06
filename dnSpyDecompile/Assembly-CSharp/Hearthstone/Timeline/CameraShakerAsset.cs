using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010EC RID: 4332
	[Obsolete("Use CameraShaker2 instead.", false)]
	public class CameraShakerAsset : PlayableAsset
	{
		// Token: 0x0600BE31 RID: 48689 RVA: 0x003A02D1 File Offset: 0x0039E4D1
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CameraShakeBehaviour>.Create(graph, this.template, 0);
		}

		// Token: 0x04009AEC RID: 39660
		public CameraShakeBehaviour template;
	}
}
