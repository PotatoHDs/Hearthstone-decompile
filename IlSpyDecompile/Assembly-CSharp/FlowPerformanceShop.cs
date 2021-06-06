using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

public class FlowPerformanceShop : FlowPerformance
{
	public class ShopSetupConfig : SetupConfig
	{
		public ShopType shopType;

		public ShopSetupConfig()
		{
			FlowType = Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.SHOP;
		}
	}

	public Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType m_shopType;

	public FlowPerformanceShop(ITimeProvider timeProvider, ITelemetryClient telemetryClient, ShopSetupConfig setupConfig)
		: base(timeProvider, telemetryClient, setupConfig)
	{
		SetShopType(setupConfig.shopType);
	}

	protected override void OnStop()
	{
		m_telemetryClient.SendFlowPerformanceShop(GetId(), m_shopType);
	}

	private void SetShopType(ShopType shopType)
	{
		switch (shopType)
		{
		case ShopType.ARENA_STORE:
			m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.ARENA_STORE;
			break;
		case ShopType.ADVENTURE_STORE:
			m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.ADVENTURE_STORE;
			break;
		case ShopType.TAVERN_BRAWL_STORE:
			m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.TAVERN_BRAWL_STORE;
			break;
		case ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET:
			m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET;
			break;
		case ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET:
			m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET;
			break;
		case ShopType.DUELS_STORE:
			m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.DUELS_STORE;
			break;
		default:
			m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.GENERAL_STORE;
			break;
		}
	}
}
