using System;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x0200081C RID: 2076
[CustomEditClass]
public class SelectSpell : Spell
{
	// Token: 0x06006FD7 RID: 28631 RVA: 0x002414EC File Offset: 0x0023F6EC
	private void LoadSelectedSpell(int selection)
	{
		if (this.m_selectionIndex == selection && this.m_selectedSpell)
		{
			return;
		}
		this.m_selectionIndex = selection;
		if (this.m_selectedSpell)
		{
			if (this.m_selectedSpell.HasUsableState(SpellStateType.CANCEL))
			{
				this.m_selectedSpell.ActivateState(SpellStateType.CANCEL);
			}
			else
			{
				this.m_selectedSpell.Deactivate();
			}
			this.m_selectedSpell = null;
		}
		SelectSpellTableEntry selectSpellTableEntry = null;
		foreach (SelectSpellTableEntry selectSpellTableEntry2 in this.m_Table)
		{
			if (selectSpellTableEntry2.m_Selection == selection)
			{
				selectSpellTableEntry = selectSpellTableEntry2;
				break;
			}
		}
		if (selectSpellTableEntry == null)
		{
			return;
		}
		if (selectSpellTableEntry.m_Spell != null)
		{
			this.m_selectedSpell = UnityEngine.Object.Instantiate<Spell>(selectSpellTableEntry.m_Spell);
			if (this.m_selectedSpell != null)
			{
				TransformUtil.AttachAndPreserveLocalTransform(this.m_selectedSpell.transform, base.gameObject.transform);
			}
		}
	}

	// Token: 0x06006FD8 RID: 28632 RVA: 0x002415EC File Offset: 0x0023F7EC
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		return this.SetSelectedSpell(taskList) && this.m_selectedSpell.AttachPowerTaskList(taskList) && base.AttachPowerTaskList(taskList);
	}

	// Token: 0x06006FD9 RID: 28633 RVA: 0x00241610 File Offset: 0x0023F810
	private bool SetSelectedSpell(PowerTaskList taskList)
	{
		foreach (PowerTask powerTask in taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = (Network.HistMetaData)power;
				if (histMetaData.MetaType == HistoryMeta.Type.EFFECT_SELECTION)
				{
					int data = histMetaData.Data;
					this.LoadSelectedSpell(data);
					return this.m_selectedSpell != null;
				}
			}
		}
		return false;
	}

	// Token: 0x06006FDA RID: 28634 RVA: 0x002416A0 File Offset: 0x0023F8A0
	protected override void OnBirth(SpellStateType prevStateType)
	{
		this.LoadSelectedSpell(0);
		if (this.m_selectedSpell)
		{
			this.m_selectedSpell.SetSource(base.GetSource());
			this.m_selectedSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSelectedSpellStateFinished));
			this.m_selectedSpell.ActivateState(SpellStateType.BIRTH);
		}
		base.OnBirth(prevStateType);
	}

	// Token: 0x06006FDB RID: 28635 RVA: 0x002416FC File Offset: 0x0023F8FC
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_selectedSpell.SetSource(base.GetSource());
		this.m_selectedSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSelectedSpellFinished));
		this.m_selectedSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSelectedSpellStateFinished));
		this.m_selectedSpell.ActivateState(SpellStateType.ACTION);
		base.OnAction(prevStateType);
	}

	// Token: 0x06006FDC RID: 28636 RVA: 0x0024175B File Offset: 0x0023F95B
	protected override void OnCancel(SpellStateType prevStateType)
	{
		if (this.m_selectedSpell != null && this.m_selectedSpell.GetActiveState() != SpellStateType.NONE && this.m_selectedSpell.GetActiveState() != SpellStateType.CANCEL)
		{
			this.m_selectedSpell.ActivateState(SpellStateType.CANCEL);
		}
		base.OnCancel(prevStateType);
	}

	// Token: 0x06006FDD RID: 28637 RVA: 0x0023AD22 File Offset: 0x00238F22
	private void OnSelectedSpellFinished(Spell spell, object userData)
	{
		this.OnSpellFinished();
	}

	// Token: 0x06006FDE RID: 28638 RVA: 0x00241799 File Offset: 0x0023F999
	private void OnSelectedSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE && spell == this.m_selectedSpell)
		{
			this.m_selectedSpell = null;
			this.m_selectionIndex = -1;
			base.Deactivate();
		}
	}

	// Token: 0x040059AD RID: 22957
	public List<SelectSpellTableEntry> m_Table = new List<SelectSpellTableEntry>();

	// Token: 0x040059AE RID: 22958
	private Spell m_selectedSpell;

	// Token: 0x040059AF RID: 22959
	private int m_selectionIndex = -1;
}
