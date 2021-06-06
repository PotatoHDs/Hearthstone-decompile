using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010F3 RID: 4339
	public class RandomSeedAsset : PlayableAsset
	{
		// Token: 0x0600BE44 RID: 48708 RVA: 0x003A088D File Offset: 0x0039EA8D
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<RandomSeedBehaviour>.Create(graph, this.template, 0);
		}

		// Token: 0x04009AFB RID: 39675
		public RandomSeedBehaviour template;
	}
}
