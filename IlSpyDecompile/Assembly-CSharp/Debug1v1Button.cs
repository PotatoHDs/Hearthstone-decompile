using PegasusShared;
using UnityEngine;

public class Debug1v1Button : PegUIElement
{
	public int m_missionId;

	public GameObject m_heroImage;

	public UberText m_name;

	private GameObject m_heroPowerObject;

	public static bool HasUsedDebugMenu { get; set; }

	private void Start()
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(m_missionId);
		if (record != null)
		{
			string text = record.ShortName;
			if (m_name != null && !string.IsNullOrEmpty(text))
			{
				m_name.Text = text;
			}
		}
	}

	private void OnCardDefLoaded(string cardID, CardDef cardDef, object userData)
	{
		m_heroImage.GetComponent<Renderer>().GetMaterial().mainTexture = cardDef.GetPortraitTexture();
	}

	protected override void OnRelease()
	{
		base.OnRelease();
		long selectedDeckID = DeckPickerTrayDisplay.Get().GetSelectedDeckID();
		HasUsedDebugMenu = true;
		GameMgr.Get().FindGame(GameType.GT_TAVERNBRAWL, FormatType.FT_WILD, m_missionId, 0, selectedDeckID);
		Object.Destroy(base.transform.parent.gameObject);
	}
}
