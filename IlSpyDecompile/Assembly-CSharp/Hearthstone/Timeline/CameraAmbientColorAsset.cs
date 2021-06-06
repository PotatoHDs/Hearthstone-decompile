using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	[Obsolete("Use CameraOverlay instead.", false)]
	public class CameraAmbientColorAsset : PlayableAsset
	{
		public CameraAmbientColorBehaviour template;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CameraAmbientColorBehaviour>.Create(graph, template);
		}
	}
}
