using System;
using UnityEngine;
using UnityEngine.Playables;

// Token: 0x02000956 RID: 2390
public class SoundTimelineAsset : PlayableAsset
{
	// Token: 0x060083EB RID: 33771 RVA: 0x002AB710 File Offset: 0x002A9910
	public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
	{
		ScriptPlayable<SoundTimelineBehavior> playable = ScriptPlayable<SoundTimelineBehavior>.Create(graph, 0);
		SoundTimelineBehavior behaviour = playable.GetBehaviour();
		if (behaviour.m_AudioSource == null)
		{
			behaviour.m_AudioSource = this.m_AudioSource.Resolve(graph.GetResolver());
		}
		return playable;
	}

	// Token: 0x04006E9A RID: 28314
	public ExposedReference<AudioSource> m_AudioSource;
}
