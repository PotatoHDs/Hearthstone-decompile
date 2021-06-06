using System;

// Token: 0x02000018 RID: 24
public class AdventureMission
{
	// Token: 0x06000094 RID: 148 RVA: 0x00003AF6 File Offset: 0x00001CF6
	public AdventureMission(int scenarioID, string description, AdventureMission.WingProgress requiredProgress, AdventureMission.WingProgress grantedProgress)
	{
		this.m_scenarioID = scenarioID;
		this.m_description = description;
		this.m_requiredProgress = (requiredProgress.IsEmpty() ? null : requiredProgress);
		this.m_grantedProgress = (grantedProgress.IsEmpty() ? null : grantedProgress);
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x06000095 RID: 149 RVA: 0x00003B32 File Offset: 0x00001D32
	public int ScenarioID
	{
		get
		{
			return this.m_scenarioID;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000096 RID: 150 RVA: 0x00003B3A File Offset: 0x00001D3A
	public string Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000097 RID: 151 RVA: 0x00003B42 File Offset: 0x00001D42
	public AdventureMission.WingProgress RequiredProgress
	{
		get
		{
			return this.m_requiredProgress;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000098 RID: 152 RVA: 0x00003B4A File Offset: 0x00001D4A
	public AdventureMission.WingProgress GrantedProgress
	{
		get
		{
			return this.m_grantedProgress;
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00003B52 File Offset: 0x00001D52
	public bool HasRequiredProgress()
	{
		return this.m_requiredProgress != null;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00003B5D File Offset: 0x00001D5D
	public bool HasGrantedProgress()
	{
		return this.m_grantedProgress != null;
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00003B68 File Offset: 0x00001D68
	public override string ToString()
	{
		return string.Format("[AdventureMission: ScenarioID={0}, Description={1} RequiredProgress={2} GrantedProgress={3}]", new object[]
		{
			this.ScenarioID,
			this.Description,
			this.RequiredProgress,
			this.GrantedProgress
		});
	}

	// Token: 0x04000060 RID: 96
	private int m_scenarioID;

	// Token: 0x04000061 RID: 97
	private string m_description;

	// Token: 0x04000062 RID: 98
	private AdventureMission.WingProgress m_requiredProgress;

	// Token: 0x04000063 RID: 99
	private AdventureMission.WingProgress m_grantedProgress;

	// Token: 0x02001266 RID: 4710
	public class WingProgress
	{
		// Token: 0x0600D3F9 RID: 54265 RVA: 0x003E36B2 File Offset: 0x003E18B2
		public WingProgress(int wing, int progress, ulong flags)
		{
			this.m_wing = wing;
			this.m_progress = progress;
			this.m_flags = flags;
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x0600D3FA RID: 54266 RVA: 0x003E36CF File Offset: 0x003E18CF
		public int Wing
		{
			get
			{
				return this.m_wing;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x0600D3FB RID: 54267 RVA: 0x003E36D7 File Offset: 0x003E18D7
		public int Progress
		{
			get
			{
				return this.m_progress;
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x0600D3FC RID: 54268 RVA: 0x003E36DF File Offset: 0x003E18DF
		public ulong Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x0600D3FD RID: 54269 RVA: 0x003E36E7 File Offset: 0x003E18E7
		public bool IsEmpty()
		{
			return this.Wing == 0 || (this.Progress <= 0 && this.Flags == 0UL);
		}

		// Token: 0x0600D3FE RID: 54270 RVA: 0x003E3708 File Offset: 0x003E1908
		public bool IsOwned()
		{
			return this.MeetsFlagsRequirement(1UL);
		}

		// Token: 0x0600D3FF RID: 54271 RVA: 0x003E3712 File Offset: 0x003E1912
		public bool MeetsProgressRequirement(int requiredProgress)
		{
			return this.Progress >= requiredProgress;
		}

		// Token: 0x0600D400 RID: 54272 RVA: 0x003E3720 File Offset: 0x003E1920
		public bool MeetsFlagsRequirement(ulong requiredFlags)
		{
			return (this.Flags & requiredFlags) == requiredFlags;
		}

		// Token: 0x0600D401 RID: 54273 RVA: 0x003E372D File Offset: 0x003E192D
		public bool MeetsProgressAndFlagsRequirements(int requiredProgress, ulong requiredFlags)
		{
			return this.MeetsProgressRequirement(requiredProgress) && this.MeetsFlagsRequirement(requiredFlags);
		}

		// Token: 0x0600D402 RID: 54274 RVA: 0x003E3741 File Offset: 0x003E1941
		public bool MeetsProgressAndFlagsRequirements(AdventureMission.WingProgress requiredProgress)
		{
			return requiredProgress == null || (requiredProgress.Wing == this.Wing && this.MeetsProgressAndFlagsRequirements(requiredProgress.Progress, requiredProgress.Flags));
		}

		// Token: 0x0600D403 RID: 54275 RVA: 0x003E376A File Offset: 0x003E196A
		public void SetProgress(int progress)
		{
			if (this.m_progress > progress)
			{
				return;
			}
			this.m_progress = progress;
		}

		// Token: 0x0600D404 RID: 54276 RVA: 0x003E377D File Offset: 0x003E197D
		public void SetFlags(ulong flags)
		{
			this.m_flags = flags;
		}

		// Token: 0x0600D405 RID: 54277 RVA: 0x003E3786 File Offset: 0x003E1986
		public override string ToString()
		{
			return string.Format("[AdventureMission.WingProgress: Wing={0}, Progress={1} Flags={2}]", this.Wing, this.Progress, this.Flags);
		}

		// Token: 0x0600D406 RID: 54278 RVA: 0x003E37B3 File Offset: 0x003E19B3
		public AdventureMission.WingProgress Clone()
		{
			return new AdventureMission.WingProgress(this.Wing, this.Progress, this.Flags);
		}

		// Token: 0x0400A36D RID: 41837
		private int m_wing;

		// Token: 0x0400A36E RID: 41838
		private int m_progress;

		// Token: 0x0400A36F RID: 41839
		private ulong m_flags;
	}
}
