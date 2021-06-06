[CustomEditClass]
public class AdventureWing_ICC : AdventureWing
{
	[CustomEditField(Sections = "ICC")]
	public NestedPrefab m_bigChestContainer;

	[CustomEditField(Sections = "ICC")]
	public int m_chestVariation;

	private AdventureWingRewardsChest_ICC m_WingRewardsChest;

	protected override void Awake()
	{
		base.Awake();
		if (!(m_bigChestContainer != null))
		{
			return;
		}
		m_WingRewardsChest = m_bigChestContainer.PrefabGameObject(instantiateIfNeeded: true).GetComponentInChildren<AdventureWingRewardsChest_ICC>();
		if (m_WingRewardsChest != null)
		{
			m_WingRewardsChest.ActivateChest(m_chestVariation);
			PegUIElement component = m_WingRewardsChest.GetComponent<PegUIElement>();
			if (component != null)
			{
				m_BigChest = component;
			}
		}
	}
}
