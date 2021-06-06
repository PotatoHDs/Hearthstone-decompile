public class UnearthedRaptorSpell : SuperSpell
{
	public override bool AddPowerTargets()
	{
		if (base.AddPowerTargets() && m_targets.Count > 0)
		{
			return true;
		}
		return false;
	}
}
