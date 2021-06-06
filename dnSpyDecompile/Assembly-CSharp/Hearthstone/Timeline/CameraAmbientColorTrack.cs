using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Hearthstone.Timeline
{
	// Token: 0x020010E7 RID: 4327
	[Obsolete("Use CameraOverlay instead.", false)]
	[TrackClipType(typeof(CameraAmbientColorAsset))]
	public class CameraAmbientColorTrack : TrackAsset
	{
		// Token: 0x0600BE13 RID: 48659 RVA: 0x0039EF55 File Offset: 0x0039D155
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<CameraAmbientColorBehaviour>.Create(graph, inputCount);
		}
	}
}
