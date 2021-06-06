using System;

// Token: 0x0200002A RID: 42
[CustomEditClass]
public class AdventureBossDefVariableIntroLines : AdventureBossDef
{
	// Token: 0x06000166 RID: 358 RVA: 0x00008738 File Offset: 0x00006938
	public override string GetIntroLine()
	{
		Achievement achievement = AchieveManager.Get().GetAchievement((int)this.m_IntroLineReferenceAchievement);
		if (achievement == null)
		{
			return this.m_IntroLine;
		}
		if (!string.IsNullOrEmpty(this.m_IntroLineAchievementComplete) && achievement.IsCompleted())
		{
			return this.m_IntroLineAchievementComplete;
		}
		if (!string.IsNullOrEmpty(this.m_IntroLineAchievementProgress) && achievement.Progress > 0 && achievement.Progress < achievement.MaxProgress)
		{
			return this.m_IntroLineAchievementProgress;
		}
		return this.m_IntroLine;
	}

	// Token: 0x040000F3 RID: 243
	[CustomEditField(Sections = "Intro Line")]
	public string m_IntroLineAchievementProgress;

	// Token: 0x040000F4 RID: 244
	[CustomEditField(Sections = "Intro Line")]
	public string m_IntroLineAchievementComplete;

	// Token: 0x040000F5 RID: 245
	[CustomEditField(Sections = "Intro Line")]
	public AchievementDbId m_IntroLineReferenceAchievement;
}
