using System;

// Token: 0x02000055 RID: 85
[CustomEditClass]
public class AdventureWing_ICC : AdventureWing
{
	// Token: 0x06000528 RID: 1320 RVA: 0x0001E544 File Offset: 0x0001C744
	protected override void Awake()
	{
		base.Awake();
		if (this.m_bigChestContainer != null)
		{
			this.m_WingRewardsChest = this.m_bigChestContainer.PrefabGameObject(true).GetComponentInChildren<AdventureWingRewardsChest_ICC>();
			if (this.m_WingRewardsChest != null)
			{
				this.m_WingRewardsChest.ActivateChest(this.m_chestVariation);
				PegUIElement component = this.m_WingRewardsChest.GetComponent<PegUIElement>();
				if (component != null)
				{
					this.m_BigChest = component;
				}
			}
		}
	}

	// Token: 0x0400036B RID: 875
	[CustomEditField(Sections = "ICC")]
	public NestedPrefab m_bigChestContainer;

	// Token: 0x0400036C RID: 876
	[CustomEditField(Sections = "ICC")]
	public int m_chestVariation;

	// Token: 0x0400036D RID: 877
	private AdventureWingRewardsChest_ICC m_WingRewardsChest;
}
