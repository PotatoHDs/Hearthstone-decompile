using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Hearthstone.Timeline
{
	[TrackClipType(typeof(CameraOverlayAsset))]
	public class CameraOverlayTrack : TrackAsset
	{
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return ScriptPlayable<CameraOverlayBehaviour>.Create(graph, inputCount);
		}
	}
}
