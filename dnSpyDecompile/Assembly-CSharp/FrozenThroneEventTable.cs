using System;

// Token: 0x02000066 RID: 102
public class FrozenThroneEventTable : StateEventTable
{
	// Token: 0x060005C7 RID: 1479 RVA: 0x00021DEC File Offset: 0x0001FFEC
	public void AnimateRuneActivation(int rune)
	{
		if (rune >= FrozenThroneEventTable.s_AnimateRuneEvents.Length)
		{
			Log.All.PrintError("FrozenThroneEventTable.PlayRune: cannot play rune {0}, there are only {1} events!", new object[]
			{
				rune,
				FrozenThroneEventTable.s_AnimateRuneEvents.Length
			});
		}
		base.TriggerState(FrozenThroneEventTable.s_AnimateRuneEvents[rune], true, null);
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x00021E40 File Offset: 0x00020040
	public void SetRuneInitiallyActivated(int rune)
	{
		if (rune >= FrozenThroneEventTable.s_InitialRuneEvents.Length)
		{
			Log.All.PrintError("FrozenThroneEventTable.InitialRune: cannot set rune {0}, there are only {1} events!", new object[]
			{
				rune,
				FrozenThroneEventTable.s_InitialRuneEvents.Length
			});
		}
		base.TriggerState(FrozenThroneEventTable.s_InitialRuneEvents[rune], true, null);
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x00021E94 File Offset: 0x00020094
	public void AddAnimateRuneEventEndListener(StateEventTable.StateEventTrigger dlg)
	{
		foreach (string eventName in FrozenThroneEventTable.s_AnimateRuneEvents)
		{
			base.AddStateEventEndListener(eventName, dlg, false);
		}
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x00021EC2 File Offset: 0x000200C2
	public void BigChestSecondaryOpen()
	{
		base.TriggerState("BigChestSecondaryOpen", true, null);
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00021ED1 File Offset: 0x000200D1
	public void AddChestOpenEndEventListener(StateEventTable.StateEventTrigger dlg, bool once = false)
	{
		base.AddStateEventEndListener("BigChestSecondaryOpen", dlg, once);
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x00021EE0 File Offset: 0x000200E0
	public void BigChestSecondaryStayOpen()
	{
		base.TriggerState("BigChestSecondaryStayOpen", true, null);
	}

	// Token: 0x04000418 RID: 1048
	private static readonly string[] s_AnimateRuneEvents = new string[]
	{
		"ActivateRune1",
		"ActivateRune2",
		"ActivateRune3",
		"ActivateRune4",
		"ActivateRune5",
		"ActivateRune6",
		"ActivateRune7",
		"ActivateRune8",
		"ActivateRune9"
	};

	// Token: 0x04000419 RID: 1049
	private static readonly string[] s_InitialRuneEvents = new string[]
	{
		"InitialRune1",
		"InitialRune2",
		"InitialRune3",
		"InitialRune4",
		"InitialRune5",
		"InitialRune6",
		"InitialRune7",
		"InitialRune8",
		"InitialRune9"
	};

	// Token: 0x0400041A RID: 1050
	private const string s_EventBigChestSecondaryOpen = "BigChestSecondaryOpen";

	// Token: 0x0400041B RID: 1051
	private const string s_EventBigChestSecondaryStayOpen = "BigChestSecondaryStayOpen";
}
