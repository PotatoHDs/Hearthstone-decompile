public class AdventureMission
{
	public class WingProgress
	{
		private int m_wing;

		private int m_progress;

		private ulong m_flags;

		public int Wing => m_wing;

		public int Progress => m_progress;

		public ulong Flags => m_flags;

		public WingProgress(int wing, int progress, ulong flags)
		{
			m_wing = wing;
			m_progress = progress;
			m_flags = flags;
		}

		public bool IsEmpty()
		{
			if (Wing == 0)
			{
				return true;
			}
			if (Progress > 0)
			{
				return false;
			}
			return Flags == 0;
		}

		public bool IsOwned()
		{
			return MeetsFlagsRequirement(1uL);
		}

		public bool MeetsProgressRequirement(int requiredProgress)
		{
			return Progress >= requiredProgress;
		}

		public bool MeetsFlagsRequirement(ulong requiredFlags)
		{
			return (Flags & requiredFlags) == requiredFlags;
		}

		public bool MeetsProgressAndFlagsRequirements(int requiredProgress, ulong requiredFlags)
		{
			if (MeetsProgressRequirement(requiredProgress))
			{
				return MeetsFlagsRequirement(requiredFlags);
			}
			return false;
		}

		public bool MeetsProgressAndFlagsRequirements(WingProgress requiredProgress)
		{
			if (requiredProgress == null)
			{
				return true;
			}
			if (requiredProgress.Wing != Wing)
			{
				return false;
			}
			return MeetsProgressAndFlagsRequirements(requiredProgress.Progress, requiredProgress.Flags);
		}

		public void SetProgress(int progress)
		{
			if (m_progress <= progress)
			{
				m_progress = progress;
			}
		}

		public void SetFlags(ulong flags)
		{
			m_flags = flags;
		}

		public override string ToString()
		{
			return $"[AdventureMission.WingProgress: Wing={Wing}, Progress={Progress} Flags={Flags}]";
		}

		public WingProgress Clone()
		{
			return new WingProgress(Wing, Progress, Flags);
		}
	}

	private int m_scenarioID;

	private string m_description;

	private WingProgress m_requiredProgress;

	private WingProgress m_grantedProgress;

	public int ScenarioID => m_scenarioID;

	public string Description => m_description;

	public WingProgress RequiredProgress => m_requiredProgress;

	public WingProgress GrantedProgress => m_grantedProgress;

	public AdventureMission(int scenarioID, string description, WingProgress requiredProgress, WingProgress grantedProgress)
	{
		m_scenarioID = scenarioID;
		m_description = description;
		m_requiredProgress = (requiredProgress.IsEmpty() ? null : requiredProgress);
		m_grantedProgress = (grantedProgress.IsEmpty() ? null : grantedProgress);
	}

	public bool HasRequiredProgress()
	{
		return m_requiredProgress != null;
	}

	public bool HasGrantedProgress()
	{
		return m_grantedProgress != null;
	}

	public override string ToString()
	{
		return $"[AdventureMission: ScenarioID={ScenarioID}, Description={Description} RequiredProgress={RequiredProgress} GrantedProgress={GrantedProgress}]";
	}
}
