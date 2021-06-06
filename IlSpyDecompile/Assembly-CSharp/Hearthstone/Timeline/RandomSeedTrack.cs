using UnityEngine;
using UnityEngine.Timeline;

namespace Hearthstone.Timeline
{
	[TrackClipType(typeof(RandomSeedAsset))]
	[TrackBindingType(typeof(ParticleSystem))]
	public class RandomSeedTrack : TrackAsset
	{
	}
}
