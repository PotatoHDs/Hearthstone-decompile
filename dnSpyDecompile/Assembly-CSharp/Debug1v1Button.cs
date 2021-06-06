using System;
using PegasusShared;
using UnityEngine;

// Token: 0x0200057D RID: 1405
public class Debug1v1Button : PegUIElement
{
	// Token: 0x17000510 RID: 1296
	// (get) Token: 0x06004E4B RID: 20043 RVA: 0x0019D6BE File Offset: 0x0019B8BE
	// (set) Token: 0x06004E4C RID: 20044 RVA: 0x0019D6C5 File Offset: 0x0019B8C5
	public static bool HasUsedDebugMenu { get; set; }

	// Token: 0x06004E4D RID: 20045 RVA: 0x0019D6D0 File Offset: 0x0019B8D0
	private void Start()
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.m_missionId);
		if (record != null)
		{
			string text = record.ShortName;
			if (this.m_name != null && !string.IsNullOrEmpty(text))
			{
				this.m_name.Text = text;
			}
		}
	}

	// Token: 0x06004E4E RID: 20046 RVA: 0x0019D71F File Offset: 0x0019B91F
	private void OnCardDefLoaded(string cardID, global::CardDef cardDef, object userData)
	{
		this.m_heroImage.GetComponent<Renderer>().GetMaterial().mainTexture = cardDef.GetPortraitTexture();
	}

	// Token: 0x06004E4F RID: 20047 RVA: 0x0019D73C File Offset: 0x0019B93C
	protected override void OnRelease()
	{
		base.OnRelease();
		long selectedDeckID = DeckPickerTrayDisplay.Get().GetSelectedDeckID();
		Debug1v1Button.HasUsedDebugMenu = true;
		GameMgr.Get().FindGame(GameType.GT_TAVERNBRAWL, FormatType.FT_WILD, this.m_missionId, 0, selectedDeckID, null, null, false, null, GameType.GT_UNKNOWN);
		UnityEngine.Object.Destroy(base.transform.parent.gameObject);
	}

	// Token: 0x04004517 RID: 17687
	public int m_missionId;

	// Token: 0x04004518 RID: 17688
	public GameObject m_heroImage;

	// Token: 0x04004519 RID: 17689
	public UberText m_name;

	// Token: 0x0400451A RID: 17690
	private GameObject m_heroPowerObject;
}
