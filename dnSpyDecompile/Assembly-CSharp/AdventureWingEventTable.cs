using System;
using System.Collections.Generic;

// Token: 0x02000059 RID: 89
[CustomEditClass]
public class AdventureWingEventTable : StateEventTable
{
	// Token: 0x0600053D RID: 1341 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
	public bool IsPlatePartiallyOpen()
	{
		string lastState = base.GetLastState();
		return this.m_PlateAlreadyOpenEvents != null && this.m_PlateAlreadyOpenEvents.Count > 0 && this.m_PlateAlreadyOpenEvents.Contains(lastState);
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
	public bool IsPlateKey()
	{
		string lastState = base.GetLastState();
		return lastState == "PlateKey" || lastState == "PlateInitialKey" || lastState == "PlateKeyNotRecommended" || lastState == "PlateInitialKeyNotRecommended";
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0001E820 File Offset: 0x0001CA20
	public bool IsPlateBuy()
	{
		string lastState = base.GetLastState();
		return lastState == "PlateBuy" || lastState == "PlateInitialBuy";
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0001E84E File Offset: 0x0001CA4E
	public bool IsPlateInitialText()
	{
		return base.GetLastState() == "PlateInitialText";
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0001E860 File Offset: 0x0001CA60
	public bool IsPlateInOrGoingToAnActiveState()
	{
		string lastState = base.GetLastState();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(lastState);
		if (num <= 1233765393U)
		{
			if (num <= 322927799U)
			{
				if (num != 89592344U)
				{
					if (num != 322927799U)
					{
						return false;
					}
					if (!(lastState == "PlateInitialBuy"))
					{
						return false;
					}
				}
				else if (!(lastState == "PlateInitialKey"))
				{
					return false;
				}
			}
			else if (num != 400449792U)
			{
				if (num != 1233765393U)
				{
					return false;
				}
				if (!(lastState == "PlateBuy"))
				{
					return false;
				}
			}
			else if (!(lastState == "PlateInitialKeyNotRecommended"))
			{
				return false;
			}
		}
		else if (num <= 2553440546U)
		{
			if (num != 1275374650U)
			{
				if (num != 2553440546U)
				{
					return false;
				}
				if (!(lastState == "PlateKeyNotRecommended"))
				{
					return false;
				}
			}
			else if (!(lastState == "PlateKey"))
			{
				return false;
			}
		}
		else if (num != 3778501054U)
		{
			if (num != 4224115554U)
			{
				return false;
			}
			if (!(lastState == "PlateInitialText"))
			{
				return false;
			}
		}
		else if (!(lastState == "PlateActivate"))
		{
			return false;
		}
		return true;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0001E963 File Offset: 0x0001CB63
	public void DoStatePlateActivate()
	{
		base.TriggerState("PlateActivate", true, null);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0001E972 File Offset: 0x0001CB72
	public void DoStatePlateDeactivate()
	{
		base.TriggerState("PlateDeactivate", true, null);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x0001E981 File Offset: 0x0001CB81
	public void DoStatePlateBuy(bool initial = false)
	{
		if (this.IsPlateBuy())
		{
			return;
		}
		base.TriggerState(initial ? "PlateInitialBuy" : "PlateBuy", true, null);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0001E9A3 File Offset: 0x0001CBA3
	public void DoStatePlateInitialText()
	{
		base.TriggerState("PlateInitialText", true, null);
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
	public void DoStatePlateKey(bool isRecommended, bool initial)
	{
		if (!isRecommended)
		{
			string eventName = initial ? "PlateInitialKeyNotRecommended" : "PlateKeyNotRecommended";
			if (base.GetStateEvent(eventName) != null)
			{
				base.TriggerState(eventName, true, null);
				return;
			}
		}
		base.TriggerState(initial ? "PlateInitialKey" : "PlateKey", true, null);
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0001EA00 File Offset: 0x0001CC00
	public void DoStatePlateOpen(int plateOpenEventIndex, float delay = 0f)
	{
		base.SetFloatVar("PlateOpen", "PostAnimationDelay", delay);
		if (this.m_PlateOpenEvents == null || this.m_PlateOpenEvents.Count == 0)
		{
			base.TriggerState("PlateOpen", true, null);
			return;
		}
		if (this.m_PlateOpenEvents.Count > plateOpenEventIndex)
		{
			base.TriggerState(this.m_PlateOpenEvents[plateOpenEventIndex], true, null);
		}
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x0001EA63 File Offset: 0x0001CC63
	public void DoStatePlateAlreadyOpen(int plateAlreadyOpenEventIndex)
	{
		if (plateAlreadyOpenEventIndex >= this.m_PlateAlreadyOpenEvents.Count)
		{
			base.TriggerState("PlateDeactivate", true, null);
			return;
		}
		base.TriggerState(this.m_PlateAlreadyOpenEvents[plateAlreadyOpenEventIndex], true, null);
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0001EA95 File Offset: 0x0001CC95
	public bool SupportsIncrementalOpening()
	{
		return this.m_PlateOpenEvents != null && this.m_PlateOpenEvents.Count > 0;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0001EAAF File Offset: 0x0001CCAF
	public void DoStatePlateCoverPreviewChest()
	{
		base.TriggerState("PlateCoverPreviewChest", false, null);
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0001EABE File Offset: 0x0001CCBE
	public void DoStatePlateReset()
	{
		base.TriggerState("PlateReset", false, null);
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0001EACD File Offset: 0x0001CCCD
	public void DoStateBigChestShow()
	{
		base.TriggerState("BigChestShow", true, null);
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0001EADC File Offset: 0x0001CCDC
	public void DoStateBigChestStayOpen()
	{
		base.TriggerState("BigChestStayOpen", true, null);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0001EAEB File Offset: 0x0001CCEB
	public void DoStateBigChestOpen()
	{
		base.TriggerState("BigChestOpen", true, null);
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0001EAFA File Offset: 0x0001CCFA
	public void DoStateBigChestCover()
	{
		base.TriggerState("BigChestCover", true, null);
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0001EB09 File Offset: 0x0001CD09
	public void AddOpenPlateStartEventListener(StateEventTable.StateEventTrigger dlg, bool once = false)
	{
		base.AddStateEventStartListener("PlateOpen", dlg, once);
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0001EB18 File Offset: 0x0001CD18
	public void RemoveOpenPlateStartEventListener(StateEventTable.StateEventTrigger dlg)
	{
		base.RemoveStateEventStartListener("PlateOpen", dlg);
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0001EB28 File Offset: 0x0001CD28
	public void AddOpenPlateEndEventListener(StateEventTable.StateEventTrigger dlg, bool once = false)
	{
		base.AddStateEventEndListener("PlateOpen", dlg, once);
		foreach (string eventName in this.m_PlateOpenEvents)
		{
			base.AddStateEventEndListener(eventName, dlg, once);
		}
		foreach (string eventName2 in this.m_PlateAlreadyOpenEvents)
		{
			base.AddStateEventEndListener(eventName2, dlg, once);
		}
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0001EBD0 File Offset: 0x0001CDD0
	public void RemoveOpenPlateEndEventListener(StateEventTable.StateEventTrigger dlg)
	{
		base.RemoveStateEventEndListener("PlateOpen", dlg);
		foreach (string eventName in this.m_PlateOpenEvents)
		{
			base.RemoveStateEventEndListener(eventName, dlg);
		}
		foreach (string eventName2 in this.m_PlateAlreadyOpenEvents)
		{
			base.RemoveStateEventEndListener(eventName2, dlg);
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0001EC74 File Offset: 0x0001CE74
	public void AddOpenChestStartEventListener(StateEventTable.StateEventTrigger dlg, bool once = false)
	{
		base.AddStateEventStartListener("BigChestOpen", dlg, once);
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0001EC83 File Offset: 0x0001CE83
	public void RemoveOpenChestStartEventListener(StateEventTable.StateEventTrigger dlg)
	{
		base.RemoveStateEventStartListener("BigChestOpen", dlg);
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0001EC91 File Offset: 0x0001CE91
	public void AddOpenChestEndEventListener(StateEventTable.StateEventTrigger dlg, bool once = false)
	{
		base.AddStateEventEndListener("BigChestOpen", dlg, once);
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001ECA0 File Offset: 0x0001CEA0
	public void RemoveOpenChestEndEventListener(StateEventTable.StateEventTrigger dlg)
	{
		base.RemoveStateEventEndListener("BigChestOpen", dlg);
	}

	// Token: 0x04000395 RID: 917
	private const string s_EventPlateActivate = "PlateActivate";

	// Token: 0x04000396 RID: 918
	private const string s_EventPlateDeactivate = "PlateDeactivate";

	// Token: 0x04000397 RID: 919
	private const string s_EventPlateInitialText = "PlateInitialText";

	// Token: 0x04000398 RID: 920
	private const string s_EventPlateBuy = "PlateBuy";

	// Token: 0x04000399 RID: 921
	private const string s_EventPlateInitialBuy = "PlateInitialBuy";

	// Token: 0x0400039A RID: 922
	private const string s_EventPlateKey = "PlateKey";

	// Token: 0x0400039B RID: 923
	private const string s_EventPlateKeyNotRecommended = "PlateKeyNotRecommended";

	// Token: 0x0400039C RID: 924
	private const string s_EventPlateInitialKey = "PlateInitialKey";

	// Token: 0x0400039D RID: 925
	private const string s_EventPlateInitialKeyNotRecommended = "PlateInitialKeyNotRecommended";

	// Token: 0x0400039E RID: 926
	private const string s_EventPlateOpen = "PlateOpen";

	// Token: 0x0400039F RID: 927
	private const string s_EventBigChestShow = "BigChestShow";

	// Token: 0x040003A0 RID: 928
	private const string s_EventBigChestStayOpen = "BigChestStayOpen";

	// Token: 0x040003A1 RID: 929
	private const string s_EventBigChestOpen = "BigChestOpen";

	// Token: 0x040003A2 RID: 930
	private const string s_EventBigChestCover = "BigChestCover";

	// Token: 0x040003A3 RID: 931
	private const string s_EventPlateCoverPreviewChest = "PlateCoverPreviewChest";

	// Token: 0x040003A4 RID: 932
	private const string s_EventPlateReset = "PlateReset";

	// Token: 0x040003A5 RID: 933
	public List<string> m_PlateOpenEvents;

	// Token: 0x040003A6 RID: 934
	public List<string> m_PlateAlreadyOpenEvents;
}
