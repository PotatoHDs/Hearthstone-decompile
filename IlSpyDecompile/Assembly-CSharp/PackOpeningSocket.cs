public class PackOpeningSocket : PegUIElement
{
	private Spell m_alertSpell;

	protected override void Awake()
	{
		base.Awake();
		m_alertSpell = GetComponent<Spell>();
	}

	public void OnPackHeld()
	{
		m_alertSpell.ActivateState(SpellStateType.BIRTH);
	}

	public void OnPackReleased()
	{
		m_alertSpell.ActivateState(SpellStateType.DEATH);
	}
}
