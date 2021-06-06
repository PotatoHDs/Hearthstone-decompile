public class FrozenThroneEventTable : StateEventTable
{
	private static readonly string[] s_AnimateRuneEvents = new string[9] { "ActivateRune1", "ActivateRune2", "ActivateRune3", "ActivateRune4", "ActivateRune5", "ActivateRune6", "ActivateRune7", "ActivateRune8", "ActivateRune9" };

	private static readonly string[] s_InitialRuneEvents = new string[9] { "InitialRune1", "InitialRune2", "InitialRune3", "InitialRune4", "InitialRune5", "InitialRune6", "InitialRune7", "InitialRune8", "InitialRune9" };

	private const string s_EventBigChestSecondaryOpen = "BigChestSecondaryOpen";

	private const string s_EventBigChestSecondaryStayOpen = "BigChestSecondaryStayOpen";

	public void AnimateRuneActivation(int rune)
	{
		if (rune >= s_AnimateRuneEvents.Length)
		{
			Log.All.PrintError("FrozenThroneEventTable.PlayRune: cannot play rune {0}, there are only {1} events!", rune, s_AnimateRuneEvents.Length);
		}
		TriggerState(s_AnimateRuneEvents[rune]);
	}

	public void SetRuneInitiallyActivated(int rune)
	{
		if (rune >= s_InitialRuneEvents.Length)
		{
			Log.All.PrintError("FrozenThroneEventTable.InitialRune: cannot set rune {0}, there are only {1} events!", rune, s_InitialRuneEvents.Length);
		}
		TriggerState(s_InitialRuneEvents[rune]);
	}

	public void AddAnimateRuneEventEndListener(StateEventTrigger dlg)
	{
		string[] array = s_AnimateRuneEvents;
		foreach (string eventName in array)
		{
			AddStateEventEndListener(eventName, dlg);
		}
	}

	public void BigChestSecondaryOpen()
	{
		TriggerState("BigChestSecondaryOpen");
	}

	public void AddChestOpenEndEventListener(StateEventTrigger dlg, bool once = false)
	{
		AddStateEventEndListener("BigChestSecondaryOpen", dlg, once);
	}

	public void BigChestSecondaryStayOpen()
	{
		TriggerState("BigChestSecondaryStayOpen");
	}
}
