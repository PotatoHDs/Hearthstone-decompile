using System;

// Token: 0x0200059F RID: 1439
public class FB_TokiCoop : MissionEntity
{
	// Token: 0x06004FB2 RID: 20402 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}
}
