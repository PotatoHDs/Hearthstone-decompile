using System;
using System.Collections.Generic;

// Token: 0x0200094E RID: 2382
[CustomEditClass]
public class ScenarioSpecificSoundDef : SoundDef, IMultipleRandomClipSoundDef
{
	// Token: 0x06008328 RID: 33576 RVA: 0x002A848C File Offset: 0x002A668C
	public List<RandomAudioClip> GetRandomAudioClips()
	{
		ScenarioDbId currentScenario = (ScenarioDbId)GameMgr.Get().GetMissionId();
		ScenarioSpecificSoundDef.ScenarioClipPair scenarioClipPair = this.m_ScenarioSpecificRandomClips.Find((ScenarioSpecificSoundDef.ScenarioClipPair pair) => pair.m_ScenarioID == currentScenario);
		if (scenarioClipPair == null)
		{
			return this.m_RandomClips;
		}
		return scenarioClipPair.m_RandomClips;
	}

	// Token: 0x04006E5E RID: 28254
	public List<ScenarioSpecificSoundDef.ScenarioClipPair> m_ScenarioSpecificRandomClips = new List<ScenarioSpecificSoundDef.ScenarioClipPair>();

	// Token: 0x02002611 RID: 9745
	[CustomEditClass]
	[Serializable]
	public class ScenarioClipPair
	{
		// Token: 0x0400EF86 RID: 61318
		public ScenarioDbId m_ScenarioID;

		// Token: 0x0400EF87 RID: 61319
		public List<RandomAudioClip> m_RandomClips;
	}
}
