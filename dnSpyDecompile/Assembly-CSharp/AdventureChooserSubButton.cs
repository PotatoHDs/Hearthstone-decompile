using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
[CustomEditClass]
public class AdventureChooserSubButton : ChooserSubButton
{
	// Token: 0x06000173 RID: 371 RVA: 0x00008A43 File Offset: 0x00006C43
	public void SetAdventure(AdventureDbId id, AdventureModeDbId mode)
	{
		this.m_TargetAdventure = id;
		this.m_TargetMode = mode;
		this.ShowRemainingProgressCount();
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00008A59 File Offset: 0x00006C59
	public AdventureDbId GetAdventure()
	{
		return this.m_TargetAdventure;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00008A61 File Offset: 0x00006C61
	public AdventureModeDbId GetMode()
	{
		return this.m_TargetMode;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00008A6C File Offset: 0x00006C6C
	public void ShowRemainingProgressCount()
	{
		int num = 0;
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)this.m_TargetAdventure, (int)this.m_TargetMode);
		if (adventureDataRecord != null && adventureDataRecord.ShowPlayableScenariosCount)
		{
			if (this.m_TargetMode == AdventureModeDbId.CLASS_CHALLENGE)
			{
				num = AdventureProgressMgr.Get().GetPlayableClassChallenges(this.m_TargetAdventure, this.m_TargetMode);
			}
			else
			{
				num = AdventureProgressMgr.Get().GetNumPlayableAdventureScenarios(this.m_TargetAdventure, this.m_TargetMode);
			}
		}
		if (GameUtils.IsModeHeroic(this.m_TargetMode))
		{
			if (this.m_heroicSkull != null)
			{
				if (num > 0)
				{
					this.m_heroicSkull.SetActive(true);
				}
				else
				{
					this.m_heroicSkull.SetActive(false);
				}
			}
			if (this.m_progressCounter != null)
			{
				this.m_progressCounter.SetActive(false);
			}
			return;
		}
		if (this.m_heroicSkull != null)
		{
			this.m_heroicSkull.SetActive(false);
		}
		if (this.m_progressCounter != null)
		{
			if (num > 0)
			{
				this.m_progressCounter.SetActive(true);
				this.m_progressCounterText.Text = num.ToString();
				return;
			}
			this.m_progressCounter.SetActive(false);
		}
	}

	// Token: 0x040000FC RID: 252
	[CustomEditField(Sections = "Progress UI")]
	public GameObject m_progressCounter;

	// Token: 0x040000FD RID: 253
	[CustomEditField(Sections = "Progress UI")]
	public UberText m_progressCounterText;

	// Token: 0x040000FE RID: 254
	[CustomEditField(Sections = "Progress UI")]
	public GameObject m_heroicSkull;

	// Token: 0x040000FF RID: 255
	public float m_ComingSoonBannerHeightOverride;

	// Token: 0x04000100 RID: 256
	private AdventureDbId m_TargetAdventure;

	// Token: 0x04000101 RID: 257
	private AdventureModeDbId m_TargetMode;
}
