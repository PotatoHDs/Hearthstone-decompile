public class BoxMenuButton : PegUIElement
{
	public UberText m_TextMesh;

	public Spell m_Spell;

	public HighlightState m_HighlightState;

	public string GetText()
	{
		return m_TextMesh.Text;
	}

	public void SetText(string text)
	{
		m_TextMesh.Text = text;
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (!(m_Spell == null))
		{
			m_Spell.ActivateState(SpellStateType.BIRTH);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		if (!(m_Spell == null))
		{
			m_Spell.ActivateState(SpellStateType.DEATH);
		}
	}

	protected override void OnPress()
	{
		if (!(m_Spell == null) && !DialogManager.Get().ShowingDialog())
		{
			m_Spell.ActivateState(SpellStateType.IDLE);
		}
	}

	protected override void OnRelease()
	{
		if (!(m_Spell == null))
		{
			m_Spell.ActivateState(SpellStateType.ACTION);
		}
	}
}
