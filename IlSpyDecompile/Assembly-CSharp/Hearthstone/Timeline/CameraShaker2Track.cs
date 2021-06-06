using UnityEngine;
using UnityEngine.Timeline;

namespace Hearthstone.Timeline
{
	[TrackClipType(typeof(CameraShaker2Asset))]
	[TrackBindingType(typeof(Camera))]
	public class CameraShaker2Track : TrackAsset
	{
	}
}
