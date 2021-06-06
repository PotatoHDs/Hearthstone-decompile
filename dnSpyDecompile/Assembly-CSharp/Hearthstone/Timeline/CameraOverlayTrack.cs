using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Hearthstone.Timeline
{
	// Token: 0x020010EB RID: 4331
	[TrackClipType(typeof(CameraOverlayAsset))]
	public class CameraOverlayTrack : TrackAsset
	{
		// Token: 0x0600BE2F RID: 48687 RVA: 0x003A02C3 File Offset: 0x0039E4C3
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<CameraOverlayBehaviour>.Create(graph, inputCount);
		}
	}
}
