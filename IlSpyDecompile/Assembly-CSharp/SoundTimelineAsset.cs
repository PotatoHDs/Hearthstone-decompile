using UnityEngine;
using UnityEngine.Playables;

public class SoundTimelineAsset : PlayableAsset
{
	public ExposedReference<AudioSource> m_AudioSource;

	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		ScriptPlayable<SoundTimelineBehavior> scriptPlayable = ScriptPlayable<SoundTimelineBehavior>.Create(graph);
		SoundTimelineBehavior behaviour = scriptPlayable.GetBehaviour();
		if (behaviour.m_AudioSource == null)
		{
			behaviour.m_AudioSource = m_AudioSource.Resolve(graph.GetResolver());
		}
		return scriptPlayable;
	}
}
