using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	public class CameraOverlayAsset : PlayableAsset
	{
		public CameraOverlayBehaviour template;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CameraOverlayBehaviour>.Create(graph, template);
		}
	}
}
