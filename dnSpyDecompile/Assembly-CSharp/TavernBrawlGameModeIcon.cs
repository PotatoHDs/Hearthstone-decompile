using System;
using PegasusShared;
using PegasusUtil;

// Token: 0x02000B2E RID: 2862
public class TavernBrawlGameModeIcon : GameModeIcon
{
	// Token: 0x060097B2 RID: 38834 RVA: 0x00310B08 File Offset: 0x0030ED08
	protected override void Awake()
	{
		base.Awake();
		this.m_tooltipZone = base.gameObject.GetComponent<TooltipZone>();
		if (this.m_tooltipZone != null)
		{
			this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.MedalOver));
			this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.MedalOut));
			TavernBrawlManager.Get().CurrentBrawlType = (GameUtils.IsFiresideGatheringGameType(GameMgr.Get().GetGameType()) ? BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING : BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			if (TavernBrawlManager.Get().GetMission(TavernBrawlManager.Get().CurrentBrawlType) == null)
			{
				Network.Get().RegisterNetHandler(TavernBrawlInfo.PacketID.ID, new Network.NetHandler(this.EnsureTavernBrawlDataReady), null);
				Network.Get().RequestTavernBrawlInfo(TavernBrawlManager.Get().CurrentBrawlType);
				return;
			}
			this.EnsureTavernBrawlDataReady();
		}
	}

	// Token: 0x060097B3 RID: 38835 RVA: 0x00310BD7 File Offset: 0x0030EDD7
	private void EnsureTavernBrawlDataReady()
	{
		Network.Get().RemoveNetHandler(TavernBrawlInfo.PacketID.ID, new Network.NetHandler(this.EnsureTavernBrawlDataReady));
		TavernBrawlManager.Get().EnsureAllDataReady(delegate()
		{
			int missionId = GameMgr.Get().GetMissionId();
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
			if (record != null)
			{
				this.m_brawlName = record.Name;
				this.m_brawlDescription = record.Description;
			}
		});
	}

	// Token: 0x060097B4 RID: 38836 RVA: 0x00310C10 File Offset: 0x0030EE10
	public void MedalOver(UIEvent e)
	{
		if (!string.IsNullOrEmpty(this.m_brawlName))
		{
			this.m_tooltipZone.ShowLayerTooltip(this.m_brawlName, this.m_brawlDescription, this.m_tooltipScale, 0);
		}
	}

	// Token: 0x060097B5 RID: 38837 RVA: 0x00310C3E File Offset: 0x0030EE3E
	private void MedalOut(UIEvent e)
	{
		this.m_tooltipZone.HideTooltip();
	}

	// Token: 0x04007F09 RID: 32521
	public float m_tooltipScale = 1f;

	// Token: 0x04007F0A RID: 32522
	private string m_brawlName;

	// Token: 0x04007F0B RID: 32523
	private string m_brawlDescription;

	// Token: 0x04007F0C RID: 32524
	private TooltipZone m_tooltipZone;

	// Token: 0x04007F0D RID: 32525
	private BrawlType m_brawlType;
}
