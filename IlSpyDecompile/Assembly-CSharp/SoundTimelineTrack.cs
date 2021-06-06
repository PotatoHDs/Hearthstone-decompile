using UnityEngine;
using UnityEngine.Timeline;

[TrackClipType(typeof(SoundTimelineAsset))]
[TrackBindingType(typeof(AudioSource))]
public class SoundTimelineTrack : TrackAsset
{
}
