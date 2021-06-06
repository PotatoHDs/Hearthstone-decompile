using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	[Obsolete("Use CameraShaker2 instead.", false)]
	public class CameraShakerAsset : PlayableAsset
	{
		public CameraShakeBehaviour template;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CameraShakeBehaviour>.Create(graph, template);
		}
	}
}
