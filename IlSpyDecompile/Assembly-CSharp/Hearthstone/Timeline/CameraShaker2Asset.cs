using UnityEngine;
using UnityEngine.Playables;

namespace Hearthstone.Timeline
{
	public class CameraShaker2Asset : PlayableAsset
	{
		public CameraShake2Behaviour template;

		public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
		{
			return ScriptPlayable<CameraShake2Behaviour>.Create(graph, template);
		}
	}
}
