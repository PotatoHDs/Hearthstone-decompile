using PegasusShared;
using PegasusUtil;

public class TavernBrawlGameModeIcon : GameModeIcon
{
	public float m_tooltipScale = 1f;

	private string m_brawlName;

	private string m_brawlDescription;

	private TooltipZone m_tooltipZone;

	private BrawlType m_brawlType;

	protected override void Awake()
	{
		base.Awake();
		m_tooltipZone = base.gameObject.GetComponent<TooltipZone>();
		if (m_tooltipZone != null)
		{
			AddEventListener(UIEventType.ROLLOVER, MedalOver);
			AddEventListener(UIEventType.ROLLOUT, MedalOut);
			TavernBrawlManager.Get().CurrentBrawlType = ((!GameUtils.IsFiresideGatheringGameType(GameMgr.Get().GetGameType())) ? BrawlType.BRAWL_TYPE_TAVERN_BRAWL : BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
			if (TavernBrawlManager.Get().GetMission(TavernBrawlManager.Get().CurrentBrawlType) == null)
			{
				Network.Get().RegisterNetHandler(TavernBrawlInfo.PacketID.ID, EnsureTavernBrawlDataReady);
				Network.Get().RequestTavernBrawlInfo(TavernBrawlManager.Get().CurrentBrawlType);
			}
			else
			{
				EnsureTavernBrawlDataReady();
			}
		}
	}

	private void EnsureTavernBrawlDataReady()
	{
		Network.Get().RemoveNetHandler(TavernBrawlInfo.PacketID.ID, EnsureTavernBrawlDataReady);
		TavernBrawlManager.Get().EnsureAllDataReady(delegate
		{
			int missionId = GameMgr.Get().GetMissionId();
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
			if (record != null)
			{
				m_brawlName = record.Name;
				m_brawlDescription = record.Description;
			}
		});
	}

	public void MedalOver(UIEvent e)
	{
		if (!string.IsNullOrEmpty(m_brawlName))
		{
			m_tooltipZone.ShowLayerTooltip(m_brawlName, m_brawlDescription, m_tooltipScale);
		}
	}

	private void MedalOut(UIEvent e)
	{
		m_tooltipZone.HideTooltip();
	}
}
