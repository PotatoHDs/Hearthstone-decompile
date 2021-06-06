using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class AdventureWing_BOT_Boom : AdventureWing
{
	// Token: 0x0600051C RID: 1308 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
	protected override void Awake()
	{
		base.Awake();
		using (List<AdventureWing_BOT_Boom.PlateCoverData>.Enumerator enumerator = this.m_plateCoverData.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				AdventureWing_BOT_Boom.PlateCoverData data = enumerator.Current;
				data.PlateCoverHitbox.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
				{
					this.OnPlateCoverRollover(data);
				});
				data.PlateCoverHitbox.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
				{
					this.OnPlateCoverRollout(data);
				});
			}
		}
		if (!base.IsDevMode)
		{
			return;
		}
		this.CheatPlateOpenButtons[0].AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.m_WingEventTable.DoStatePlateOpen(0, 0f);
		});
		this.CheatPlateOpenButtons[1].AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.m_WingEventTable.DoStatePlateOpen(1, 0f);
		});
		this.CheatPlateOpenButtons[2].AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.m_WingEventTable.DoStatePlateOpen(2, 0f);
		});
		this.CheatPlateOpenButtons[3].AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.m_WingEventTable.DoStatePlateOpen(3, 0f);
		});
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x0001E2EC File Offset: 0x0001C4EC
	private void OnPlateCoverRollover(AdventureWing_BOT_Boom.PlateCoverData data)
	{
		data.PlateCoverHitbox.GetComponent<TooltipZone>().ShowTooltip(GameStrings.Get("GLUE_ADVENTURE_BOT_BOOM_TOOLTIP_HEADER"), GameStrings.Get(data.TooltipText), 5f, 0);
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0001E31A File Offset: 0x0001C51A
	private void OnPlateCoverRollout(AdventureWing_BOT_Boom.PlateCoverData data)
	{
		data.PlateCoverHitbox.GetComponent<TooltipZone>().HideTooltip();
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0001E32C File Offset: 0x0001C52C
	protected override void DoOpenPlate(float unlockDelay)
	{
		List<AdventureMissionDbfRecord> sortedAdventureMissionRecordsForThisWing = this.GetSortedAdventureMissionRecordsForThisWing();
		bool flag = false;
		foreach (AdventureMissionDbfRecord adventureMissionDbfRecord in sortedAdventureMissionRecordsForThisWing)
		{
			AdventureMission.WingProgress progress = AdventureProgressMgr.Get().GetProgress(adventureMissionDbfRecord.ReqWingId);
			int reqProgress = adventureMissionDbfRecord.ReqProgress;
			int progress2 = progress.Progress;
			int num;
			AdventureProgressMgr.Get().GetWingAck(adventureMissionDbfRecord.ReqWingId, out num);
			if (progress2 == reqProgress && (num < progress2 || base.HasDependentWingJustAckedRequiredProgress(adventureMissionDbfRecord)))
			{
				int plateOpenEventIndex = GameDbf.Scenario.GetRecord(adventureMissionDbfRecord.ScenarioId).SortOrder - 1;
				this.m_WingEventTable.DoStatePlateOpen(plateOpenEventIndex, unlockDelay);
				flag = true;
			}
		}
		if (!flag)
		{
			base.FireOpenPlateEndEvent(null);
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
	protected override bool InitializePlateOpenState()
	{
		List<AdventureMissionDbfRecord> sortedAdventureMissionRecordsForThisWing = this.GetSortedAdventureMissionRecordsForThisWing();
		bool result = false;
		foreach (AdventureMissionDbfRecord adventureMissionDbfRecord in sortedAdventureMissionRecordsForThisWing)
		{
			AdventureMission.WingProgress progress = AdventureProgressMgr.Get().GetProgress(adventureMissionDbfRecord.ReqWingId);
			int reqProgress = adventureMissionDbfRecord.ReqProgress;
			int progress2 = progress.Progress;
			int num;
			AdventureProgressMgr.Get().GetWingAck(adventureMissionDbfRecord.ReqWingId, out num);
			if (progress2 >= reqProgress && num >= progress2 && !base.HasDependentWingJustAckedRequiredProgress())
			{
				int plateAlreadyOpenEventIndex = GameDbf.Scenario.GetRecord(adventureMissionDbfRecord.ScenarioId).SortOrder - 1;
				this.m_WingEventTable.DoStatePlateAlreadyOpen(plateAlreadyOpenEventIndex);
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
	private List<AdventureMissionDbfRecord> GetSortedAdventureMissionRecordsForThisWing()
	{
		List<AdventureMissionDbfRecord> records = GameDbf.AdventureMission.GetRecords((AdventureMissionDbfRecord r) => r.GrantsWingId == (int)this.m_WingDef.GetWingId(), -1);
		from r in records
		orderby GameDbf.Scenario.GetRecord(r.ScenarioId).SortOrder
		select r;
		return records;
	}

	// Token: 0x04000369 RID: 873
	public List<UIBButton> CheatPlateOpenButtons;

	// Token: 0x0400036A RID: 874
	public List<AdventureWing_BOT_Boom.PlateCoverData> m_plateCoverData;

	// Token: 0x0200134B RID: 4939
	[Serializable]
	public class PlateCoverData
	{
		// Token: 0x0400A5F9 RID: 42489
		[SerializeField]
		public PegUIElement PlateCoverHitbox;

		// Token: 0x0400A5FA RID: 42490
		[SerializeField]
		public string TooltipText;
	}
}
