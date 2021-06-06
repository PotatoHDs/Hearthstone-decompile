using System.Collections.Generic;

[CustomEditClass]
public class AdventureWingEventTable : StateEventTable
{
	private const string s_EventPlateActivate = "PlateActivate";

	private const string s_EventPlateDeactivate = "PlateDeactivate";

	private const string s_EventPlateInitialText = "PlateInitialText";

	private const string s_EventPlateBuy = "PlateBuy";

	private const string s_EventPlateInitialBuy = "PlateInitialBuy";

	private const string s_EventPlateKey = "PlateKey";

	private const string s_EventPlateKeyNotRecommended = "PlateKeyNotRecommended";

	private const string s_EventPlateInitialKey = "PlateInitialKey";

	private const string s_EventPlateInitialKeyNotRecommended = "PlateInitialKeyNotRecommended";

	private const string s_EventPlateOpen = "PlateOpen";

	private const string s_EventBigChestShow = "BigChestShow";

	private const string s_EventBigChestStayOpen = "BigChestStayOpen";

	private const string s_EventBigChestOpen = "BigChestOpen";

	private const string s_EventBigChestCover = "BigChestCover";

	private const string s_EventPlateCoverPreviewChest = "PlateCoverPreviewChest";

	private const string s_EventPlateReset = "PlateReset";

	public List<string> m_PlateOpenEvents;

	public List<string> m_PlateAlreadyOpenEvents;

	public bool IsPlatePartiallyOpen()
	{
		string lastState = GetLastState();
		if (m_PlateAlreadyOpenEvents != null && m_PlateAlreadyOpenEvents.Count > 0)
		{
			return m_PlateAlreadyOpenEvents.Contains(lastState);
		}
		return false;
	}

	public bool IsPlateKey()
	{
		string lastState = GetLastState();
		switch (lastState)
		{
		default:
			return lastState == "PlateInitialKeyNotRecommended";
		case "PlateKey":
		case "PlateInitialKey":
		case "PlateKeyNotRecommended":
			return true;
		}
	}

	public bool IsPlateBuy()
	{
		string lastState = GetLastState();
		if (!(lastState == "PlateBuy"))
		{
			return lastState == "PlateInitialBuy";
		}
		return true;
	}

	public bool IsPlateInitialText()
	{
		return GetLastState() == "PlateInitialText";
	}

	public bool IsPlateInOrGoingToAnActiveState()
	{
		switch (GetLastState())
		{
		case "PlateActivate":
		case "PlateInitialText":
		case "PlateBuy":
		case "PlateInitialBuy":
		case "PlateKey":
		case "PlateKeyNotRecommended":
		case "PlateInitialKey":
		case "PlateInitialKeyNotRecommended":
			return true;
		default:
			return false;
		}
	}

	public void DoStatePlateActivate()
	{
		TriggerState("PlateActivate");
	}

	public void DoStatePlateDeactivate()
	{
		TriggerState("PlateDeactivate");
	}

	public void DoStatePlateBuy(bool initial = false)
	{
		if (!IsPlateBuy())
		{
			TriggerState(initial ? "PlateInitialBuy" : "PlateBuy");
		}
	}

	public void DoStatePlateInitialText()
	{
		TriggerState("PlateInitialText");
	}

	public void DoStatePlateKey(bool isRecommended, bool initial)
	{
		if (!isRecommended)
		{
			string eventName = (initial ? "PlateInitialKeyNotRecommended" : "PlateKeyNotRecommended");
			if (GetStateEvent(eventName) != null)
			{
				TriggerState(eventName);
				return;
			}
		}
		TriggerState(initial ? "PlateInitialKey" : "PlateKey");
	}

	public void DoStatePlateOpen(int plateOpenEventIndex, float delay = 0f)
	{
		SetFloatVar("PlateOpen", "PostAnimationDelay", delay);
		if (m_PlateOpenEvents == null || m_PlateOpenEvents.Count == 0)
		{
			TriggerState("PlateOpen");
		}
		else if (m_PlateOpenEvents.Count > plateOpenEventIndex)
		{
			TriggerState(m_PlateOpenEvents[plateOpenEventIndex]);
		}
	}

	public void DoStatePlateAlreadyOpen(int plateAlreadyOpenEventIndex)
	{
		if (plateAlreadyOpenEventIndex >= m_PlateAlreadyOpenEvents.Count)
		{
			TriggerState("PlateDeactivate");
		}
		else
		{
			TriggerState(m_PlateAlreadyOpenEvents[plateAlreadyOpenEventIndex]);
		}
	}

	public bool SupportsIncrementalOpening()
	{
		if (m_PlateOpenEvents != null)
		{
			return m_PlateOpenEvents.Count > 0;
		}
		return false;
	}

	public void DoStatePlateCoverPreviewChest()
	{
		TriggerState("PlateCoverPreviewChest", saveLastState: false);
	}

	public void DoStatePlateReset()
	{
		TriggerState("PlateReset", saveLastState: false);
	}

	public void DoStateBigChestShow()
	{
		TriggerState("BigChestShow");
	}

	public void DoStateBigChestStayOpen()
	{
		TriggerState("BigChestStayOpen");
	}

	public void DoStateBigChestOpen()
	{
		TriggerState("BigChestOpen");
	}

	public void DoStateBigChestCover()
	{
		TriggerState("BigChestCover");
	}

	public void AddOpenPlateStartEventListener(StateEventTrigger dlg, bool once = false)
	{
		AddStateEventStartListener("PlateOpen", dlg, once);
	}

	public void RemoveOpenPlateStartEventListener(StateEventTrigger dlg)
	{
		RemoveStateEventStartListener("PlateOpen", dlg);
	}

	public void AddOpenPlateEndEventListener(StateEventTrigger dlg, bool once = false)
	{
		AddStateEventEndListener("PlateOpen", dlg, once);
		foreach (string plateOpenEvent in m_PlateOpenEvents)
		{
			AddStateEventEndListener(plateOpenEvent, dlg, once);
		}
		foreach (string plateAlreadyOpenEvent in m_PlateAlreadyOpenEvents)
		{
			AddStateEventEndListener(plateAlreadyOpenEvent, dlg, once);
		}
	}

	public void RemoveOpenPlateEndEventListener(StateEventTrigger dlg)
	{
		RemoveStateEventEndListener("PlateOpen", dlg);
		foreach (string plateOpenEvent in m_PlateOpenEvents)
		{
			RemoveStateEventEndListener(plateOpenEvent, dlg);
		}
		foreach (string plateAlreadyOpenEvent in m_PlateAlreadyOpenEvents)
		{
			RemoveStateEventEndListener(plateAlreadyOpenEvent, dlg);
		}
	}

	public void AddOpenChestStartEventListener(StateEventTrigger dlg, bool once = false)
	{
		AddStateEventStartListener("BigChestOpen", dlg, once);
	}

	public void RemoveOpenChestStartEventListener(StateEventTrigger dlg)
	{
		RemoveStateEventStartListener("BigChestOpen", dlg);
	}

	public void AddOpenChestEndEventListener(StateEventTrigger dlg, bool once = false)
	{
		AddStateEventEndListener("BigChestOpen", dlg, once);
	}

	public void RemoveOpenChestEndEventListener(StateEventTrigger dlg)
	{
		RemoveStateEventEndListener("BigChestOpen", dlg);
	}
}
