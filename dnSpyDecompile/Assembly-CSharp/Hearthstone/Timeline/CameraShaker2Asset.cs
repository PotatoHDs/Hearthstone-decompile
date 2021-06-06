using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	// Token: 0x020010EF RID: 4335
	public class CameraShaker2Asset : PlayableAsset
	{
		// Token: 0x0600BE37 RID: 48695 RVA: 0x003A041F File Offset: 0x0039E61F
		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CameraShake2Behaviour>.Create(graph, this.template, 0);
		}

		// Token: 0x04009AF2 RID: 39666
		public CameraShake2Behaviour template;
	}
}
