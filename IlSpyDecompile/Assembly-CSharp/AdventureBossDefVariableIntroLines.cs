[CustomEditClass]
public class AdventureBossDefVariableIntroLines : AdventureBossDef
{
	[CustomEditField(Sections = "Intro Line")]
	public string m_IntroLineAchievementProgress;

	[CustomEditField(Sections = "Intro Line")]
	public string m_IntroLineAchievementComplete;

	[CustomEditField(Sections = "Intro Line")]
	public AchievementDbId m_IntroLineReferenceAchievement;

	public override string GetIntroLine()
	{
		Achievement achievement = AchieveManager.Get().GetAchievement((int)m_IntroLineReferenceAchievement);
		if (achievement == null)
		{
			return m_IntroLine;
		}
		if (!string.IsNullOrEmpty(m_IntroLineAchievementComplete) && achievement.IsCompleted())
		{
			return m_IntroLineAchievementComplete;
		}
		if (!string.IsNullOrEmpty(m_IntroLineAchievementProgress) && achievement.Progress > 0 && achievement.Progress < achievement.MaxProgress)
		{
			return m_IntroLineAchievementProgress;
		}
		return m_IntroLine;
	}
}
