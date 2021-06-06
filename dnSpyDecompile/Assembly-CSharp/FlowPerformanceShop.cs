using System;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

// Token: 0x02000697 RID: 1687
public class FlowPerformanceShop : global::FlowPerformance
{
	// Token: 0x06005E3D RID: 24125 RVA: 0x001EA539 File Offset: 0x001E8739
	public FlowPerformanceShop(ITimeProvider timeProvider, ITelemetryClient telemetryClient, global::FlowPerformanceShop.ShopSetupConfig setupConfig) : base(timeProvider, telemetryClient, setupConfig)
	{
		this.SetShopType(setupConfig.shopType);
	}

	// Token: 0x06005E3E RID: 24126 RVA: 0x001EA550 File Offset: 0x001E8750
	protected override void OnStop()
	{
		this.m_telemetryClient.SendFlowPerformanceShop(base.GetId(), this.m_shopType);
	}

	// Token: 0x06005E3F RID: 24127 RVA: 0x001EA56C File Offset: 0x001E876C
	private void SetShopType(ShopType shopType)
	{
		switch (shopType)
		{
		case ShopType.ARENA_STORE:
			this.m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.ARENA_STORE;
			return;
		case ShopType.ADVENTURE_STORE:
			this.m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.ADVENTURE_STORE;
			return;
		case ShopType.TAVERN_BRAWL_STORE:
			this.m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.TAVERN_BRAWL_STORE;
			return;
		case ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET:
			this.m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET;
			return;
		case ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET:
			this.m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET;
			return;
		case ShopType.DUELS_STORE:
			this.m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.DUELS_STORE;
			return;
		default:
			this.m_shopType = Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType.GENERAL_STORE;
			return;
		}
	}

	// Token: 0x04004F7C RID: 20348
	public Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType m_shopType;

	// Token: 0x020021C0 RID: 8640
	public class ShopSetupConfig : global::FlowPerformance.SetupConfig
	{
		// Token: 0x060124A7 RID: 74919 RVA: 0x00503D38 File Offset: 0x00501F38
		public ShopSetupConfig()
		{
			this.FlowType = Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.SHOP;
		}

		// Token: 0x0400E139 RID: 57657
		public ShopType shopType;
	}
}
