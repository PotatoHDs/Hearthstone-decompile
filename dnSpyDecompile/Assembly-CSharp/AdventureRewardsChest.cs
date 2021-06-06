using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
[CustomEditClass]
public class AdventureRewardsChest : MonoBehaviour
{
	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000404 RID: 1028 RVA: 0x000186EF File Offset: 0x000168EF
	// (set) Token: 0x06000405 RID: 1029 RVA: 0x000186F7 File Offset: 0x000168F7
	public bool m_fadedOut { get; private set; }

	// Token: 0x06000406 RID: 1030 RVA: 0x00018700 File Offset: 0x00016900
	public void AddChestEventListener(UIEventType type, UIEvent.Handler handler)
	{
		this.m_ChestClickArea.AddEventListener(type, handler);
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00018710 File Offset: 0x00016910
	public void RemoveChestEventListener(UIEventType type, UIEvent.Handler handler)
	{
		this.m_ChestClickArea.RemoveEventListener(type, handler);
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00018720 File Offset: 0x00016920
	public void SlamInCheckmark()
	{
		this.ShowCheckmark();
		this.m_EventTable.TriggerState("SlamInCheckmark", true, null);
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0001873A File Offset: 0x0001693A
	public void ShowCheckmark()
	{
		this.m_CheckmarkContainer.SetActive(true);
		this.m_ChestContainer.SetActive(false);
		this.m_GameSaveDataProgressContainer.SetActive(false);
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00018760 File Offset: 0x00016960
	public void BurstCheckmark()
	{
		this.ShowCheckmark();
		this.m_EventTable.TriggerState("BurstCheckmark", true, null);
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0001877A File Offset: 0x0001697A
	public void BlinkChest()
	{
		if (this.m_fadedOut)
		{
			return;
		}
		this.ShowCheckmark();
		this.m_EventTable.TriggerState("BlinkChest", true, null);
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0001879D File Offset: 0x0001699D
	public void ShowChest()
	{
		this.m_CheckmarkContainer.SetActive(false);
		this.m_ChestContainer.SetActive(true);
		this.m_GameSaveDataProgressContainer.SetActive(false);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x000187C4 File Offset: 0x000169C4
	public void ShowGameSaveDataProgress(int progress, int maxProgress)
	{
		this.m_CheckmarkContainer.SetActive(false);
		this.m_ChestContainer.SetActive(false);
		if (progress > 0)
		{
			this.m_GameSaveDataProgressContainer.SetActive(true);
		}
		this.m_GameSaveDataProgressContainer.GetComponentInChildren<UberText>().Text = string.Format("{0}/{1}", progress, maxProgress);
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0001881F File Offset: 0x00016A1F
	public void HideAll()
	{
		this.m_CheckmarkContainer.SetActive(false);
		this.m_ChestContainer.SetActive(false);
		this.m_GameSaveDataProgressContainer.SetActive(false);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00018845 File Offset: 0x00016A45
	public void Enable(bool enable)
	{
		if (this.m_ChestClickArea != null)
		{
			this.m_ChestClickArea.gameObject.SetActive(enable);
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00018866 File Offset: 0x00016A66
	public void FadeInChest()
	{
		this.m_EventTable.TriggerState("FadeChestIn", true, null);
		this.m_fadedOut = false;
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00018881 File Offset: 0x00016A81
	public void FadeOutChest()
	{
		this.m_EventTable.TriggerState("FadeChestOut", true, null);
		this.m_fadedOut = true;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0001889C File Offset: 0x00016A9C
	public void FadeOutChestImmediate()
	{
		Color white = Color.white;
		white.a = 0f;
		this.m_ChestQuad.GetMaterial().SetColor("_Color", white);
		this.m_fadedOut = true;
	}

	// Token: 0x040002C4 RID: 708
	private const string s_EventBlinkChest = "BlinkChest";

	// Token: 0x040002C5 RID: 709
	private const string s_EventOpenChest = "OpenChest";

	// Token: 0x040002C6 RID: 710
	private const string s_EventSlamInCheckmark = "SlamInCheckmark";

	// Token: 0x040002C7 RID: 711
	private const string s_EventBurstCheckmark = "BurstCheckmark";

	// Token: 0x040002C8 RID: 712
	private const string s_EventFadeInChest = "FadeChestIn";

	// Token: 0x040002C9 RID: 713
	private const string s_EventFadeOutChest = "FadeChestOut";

	// Token: 0x040002CA RID: 714
	[CustomEditField(Sections = "Event Table")]
	public StateEventTable m_EventTable;

	// Token: 0x040002CB RID: 715
	[CustomEditField(Sections = "UI")]
	public PegUIElement m_ChestClickArea;

	// Token: 0x040002CC RID: 716
	[CustomEditField(Sections = "UI")]
	public GameObject m_CheckmarkContainer;

	// Token: 0x040002CD RID: 717
	[CustomEditField(Sections = "UI")]
	public GameObject m_ChestContainer;

	// Token: 0x040002CE RID: 718
	[CustomEditField(Sections = "UI")]
	public GameObject m_GameSaveDataProgressContainer;

	// Token: 0x040002CF RID: 719
	[CustomEditField(Sections = "UI")]
	public MeshRenderer m_ChestQuad;
}
