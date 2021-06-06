using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace Hearthstone.Timeline
{
	[Obsolete("Use CameraShaker2 instead.", false)]
	[TrackClipType(typeof(CameraShakerAsset))]
	[TrackBindingType(typeof(Camera))]
	public class CameraShakerTrack : TrackAsset
	{
	}
}
