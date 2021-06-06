using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Hearthstone.Timeline
{
	[Obsolete("Use CameraOverlay instead.", false)]
	[TrackClipType(typeof(CameraAmbientColorAsset))]
	public class CameraAmbientColorTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<CameraAmbientColorBehaviour>.Create(graph, inputCount);
		}
	}
}
