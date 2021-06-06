public class CustomChoiceSpell : Spell
{
	protected ChoiceCardMgr.ChoiceState m_choiceState;

	public virtual void SetChoiceState(ChoiceCardMgr.ChoiceState choiceState)
	{
		m_choiceState = choiceState;
	}

	public ChoiceCardMgr.ChoiceState GetChoiceState()
	{
		return m_choiceState;
	}
}
