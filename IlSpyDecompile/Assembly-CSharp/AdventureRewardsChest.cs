using UnityEngine;

[CustomEditClass]
public class AdventureRewardsChest : MonoBehaviour
{
	private const string s_EventBlinkChest = "BlinkChest";

	private const string s_EventOpenChest = "OpenChest";

	private const string s_EventSlamInCheckmark = "SlamInCheckmark";

	private const string s_EventBurstCheckmark = "BurstCheckmark";

	private const string s_EventFadeInChest = "FadeChestIn";

	private const string s_EventFadeOutChest = "FadeChestOut";

	[CustomEditField(Sections = "Event Table")]
	public StateEventTable m_EventTable;

	[CustomEditField(Sections = "UI")]
	public PegUIElement m_ChestClickArea;

	[CustomEditField(Sections = "UI")]
	public GameObject m_CheckmarkContainer;

	[CustomEditField(Sections = "UI")]
	public GameObject m_ChestContainer;

	[CustomEditField(Sections = "UI")]
	public GameObject m_GameSaveDataProgressContainer;

	[CustomEditField(Sections = "UI")]
	public MeshRenderer m_ChestQuad;

	public bool m_fadedOut { get; private set; }

	public void AddChestEventListener(UIEventType type, UIEvent.Handler handler)
	{
		m_ChestClickArea.AddEventListener(type, handler);
	}

	public void RemoveChestEventListener(UIEventType type, UIEvent.Handler handler)
	{
		m_ChestClickArea.RemoveEventListener(type, handler);
	}

	public void SlamInCheckmark()
	{
		ShowCheckmark();
		m_EventTable.TriggerState("SlamInCheckmark");
	}

	public void ShowCheckmark()
	{
		m_CheckmarkContainer.SetActive(value: true);
		m_ChestContainer.SetActive(value: false);
		m_GameSaveDataProgressContainer.SetActive(value: false);
	}

	public void BurstCheckmark()
	{
		ShowCheckmark();
		m_EventTable.TriggerState("BurstCheckmark");
	}

	public void BlinkChest()
	{
		if (!m_fadedOut)
		{
			ShowCheckmark();
			m_EventTable.TriggerState("BlinkChest");
		}
	}

	public void ShowChest()
	{
		m_CheckmarkContainer.SetActive(value: false);
		m_ChestContainer.SetActive(value: true);
		m_GameSaveDataProgressContainer.SetActive(value: false);
	}

	public void ShowGameSaveDataProgress(int progress, int maxProgress)
	{
		m_CheckmarkContainer.SetActive(value: false);
		m_ChestContainer.SetActive(value: false);
		if (progress > 0)
		{
			m_GameSaveDataProgressContainer.SetActive(value: true);
		}
		m_GameSaveDataProgressContainer.GetComponentInChildren<UberText>().Text = $"{progress}/{maxProgress}";
	}

	public void HideAll()
	{
		m_CheckmarkContainer.SetActive(value: false);
		m_ChestContainer.SetActive(value: false);
		m_GameSaveDataProgressContainer.SetActive(value: false);
	}

	public void Enable(bool enable)
	{
		if (m_ChestClickArea != null)
		{
			m_ChestClickArea.gameObject.SetActive(enable);
		}
	}

	public void FadeInChest()
	{
		m_EventTable.TriggerState("FadeChestIn");
		m_fadedOut = false;
	}

	public void FadeOutChest()
	{
		m_EventTable.TriggerState("FadeChestOut");
		m_fadedOut = true;
	}

	public void FadeOutChestImmediate()
	{
		Color white = Color.white;
		white.a = 0f;
		m_ChestQuad.GetMaterial().SetColor("_Color", white);
		m_fadedOut = true;
	}
}
