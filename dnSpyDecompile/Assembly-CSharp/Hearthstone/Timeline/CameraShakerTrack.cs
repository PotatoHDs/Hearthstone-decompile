using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace Hearthstone.Timeline
{
	// Token: 0x020010EE RID: 4334
	[Obsolete("Use CameraShaker2 instead.", false)]
	[TrackClipType(typeof(CameraShakerAsset))]
	[TrackBindingType(typeof(Camera))]
	public class CameraShakerTrack : TrackAsset
	{
	}
}
