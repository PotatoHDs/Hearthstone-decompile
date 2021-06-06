using System;

// Token: 0x020007DF RID: 2015
public class CustomChoiceSpell : Spell
{
	// Token: 0x06006E55 RID: 28245 RVA: 0x002397B0 File Offset: 0x002379B0
	public virtual void SetChoiceState(ChoiceCardMgr.ChoiceState choiceState)
	{
		this.m_choiceState = choiceState;
	}

	// Token: 0x06006E56 RID: 28246 RVA: 0x002397B9 File Offset: 0x002379B9
	public ChoiceCardMgr.ChoiceState GetChoiceState()
	{
		return this.m_choiceState;
	}

	// Token: 0x04005893 RID: 22675
	protected ChoiceCardMgr.ChoiceState m_choiceState;
}
