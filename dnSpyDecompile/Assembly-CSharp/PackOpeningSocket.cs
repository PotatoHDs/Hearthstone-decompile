using System;

// Token: 0x02000618 RID: 1560
public class PackOpeningSocket : PegUIElement
{
	// Token: 0x06005791 RID: 22417 RVA: 0x001CAB01 File Offset: 0x001C8D01
	protected override void Awake()
	{
		base.Awake();
		this.m_alertSpell = base.GetComponent<Spell>();
	}

	// Token: 0x06005792 RID: 22418 RVA: 0x001CAB15 File Offset: 0x001C8D15
	public void OnPackHeld()
	{
		this.m_alertSpell.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x06005793 RID: 22419 RVA: 0x001CAB23 File Offset: 0x001C8D23
	public void OnPackReleased()
	{
		this.m_alertSpell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x04004B25 RID: 19237
	private Spell m_alertSpell;
}
