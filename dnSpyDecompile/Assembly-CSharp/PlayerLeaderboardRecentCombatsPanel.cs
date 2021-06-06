using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000339 RID: 825
public class PlayerLeaderboardRecentCombatsPanel : PlayerLeaderboardInformationPanel
{
	// Token: 0x06002F5C RID: 12124 RVA: 0x000F1C1C File Offset: 0x000EFE1C
	public void Awake()
	{
		for (int i = 0; i < this.m_recentActionPlaceholders.Count; i++)
		{
			PlayerLeaderboardRecentCombatEntry component = this.m_recentActionPlaceholders[i].GetComponent<PlayerLeaderboardRecentCombatEntry>();
			component.m_iconOpponentSwords.SetActive(false);
			component.m_iconOwnerSwords.SetActive(false);
			component.m_iconOpponentSplat.SetActive(false);
			component.m_iconOwnerSplat.SetActive(false);
			component.m_opponentTileActor.gameObject.SetActive(false);
			component.m_background.gameObject.SetActive(true);
		}
		this.m_techLevel.ClearText();
		this.m_winStreak.ClearText();
		this.m_triples.ClearText();
	}

	// Token: 0x06002F5D RID: 12125 RVA: 0x000F1CC2 File Offset: 0x000EFEC2
	public bool HasRecentCombats()
	{
		return this.m_recentCombatEntries.Count > 0;
	}

	// Token: 0x06002F5E RID: 12126 RVA: 0x000F1CD2 File Offset: 0x000EFED2
	public void ClearRecentCombats()
	{
		while (this.m_recentCombatEntries.Count > 0)
		{
			UnityEngine.Object.Destroy(this.m_recentCombatEntries.Dequeue().gameObject);
		}
	}

	// Token: 0x06002F5F RID: 12127 RVA: 0x000F1CF9 File Offset: 0x000EFEF9
	public void SetTriples(int triples)
	{
		this.m_triplesCount = triples;
		this.UpdateLayout();
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x000F1D08 File Offset: 0x000EFF08
	public void SetTechLevel(int techLevel)
	{
		this.m_techLevelCount = techLevel;
		this.UpdateLayout();
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x000F1D18 File Offset: 0x000EFF18
	public void AddRecentCombat(PlayerLeaderboardCard source, PlayerLeaderboardRecentCombatsPanel.RecentCombatInfo recentCombatInfo)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Recent_Combat_Entry.prefab:74bf698d81967c9498554a64c9db51fc", AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardRecentCombatsPanel.AddRecentCombat() - FAILED to load GameObject \"{0}\"", new object[]
			{
				"Recent_Combat_Entry.prefab:74bf698d81967c9498554a64c9db51fc"
			});
			return;
		}
		PlayerLeaderboardRecentCombatEntry component = gameObject.GetComponent<PlayerLeaderboardRecentCombatEntry>();
		if (component == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardRecentCombatsPanel.AddRecentCombat() - ERROR GameObject \"{0}\" has no PlayerLeaderboardRecentCombatEntry component", new object[]
			{
				"Recent_Combat_Entry.prefab:74bf698d81967c9498554a64c9db51fc"
			});
			return;
		}
		TransformUtil.Identity(component.transform);
		component.Load(source, recentCombatInfo);
		if ((long)this.m_recentCombatEntries.Count == (long)((ulong)this.m_maxDisplayItems))
		{
			UnityEngine.Object.Destroy(this.m_recentCombatEntries.Dequeue().gameObject);
		}
		this.m_recentCombatEntries.Enqueue(component);
		this.m_winStreakCount = recentCombatInfo.winStreak;
		this.UpdateLayout();
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x000F1DE4 File Offset: 0x000EFFE4
	private void UpdateTechLevelPlaymaker()
	{
		PlayMakerFSM component = this.m_techLevel.GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			Log.Gameplay.PrintError("No playmaker attached to tech level icon.", Array.Empty<object>());
			return;
		}
		component.FsmVariables.GetFsmInt("TechLevel").Value = this.m_techLevelCount;
		component.SendEvent("Action");
	}

	// Token: 0x06002F63 RID: 12131 RVA: 0x000F1E44 File Offset: 0x000F0044
	private void UpdateLayout()
	{
		if (this.m_triples != null)
		{
			this.m_triples.SetText(this.m_triplesCount.ToString());
		}
		if (this.m_winStreak != null)
		{
			this.m_winStreak.SetText(this.m_winStreakCount.ToString());
		}
		if (this.m_techLevel != null)
		{
			this.UpdateTechLevelPlaymaker();
		}
		if (this.m_recentActionPlaceholders == null)
		{
			return;
		}
		for (int i = 0; i < this.m_recentActionPlaceholders.Count; i++)
		{
			if (this.m_recentCombatEntries.Count > i)
			{
				int index = Math.Min(this.m_recentActionPlaceholders.Count, this.m_recentCombatEntries.Count) - (1 + i);
				GameObject gameObject = this.m_recentActionPlaceholders[index];
				GameObject gameObject2 = this.m_recentCombatEntries[i].gameObject;
				gameObject2.transform.parent = gameObject.transform.parent;
				TransformUtil.CopyLocal(gameObject2, gameObject);
				gameObject.SetActive(false);
			}
			else
			{
				this.m_recentActionPlaceholders[i].SetActive(true);
			}
		}
	}

	// Token: 0x06002F64 RID: 12132 RVA: 0x000F1F58 File Offset: 0x000F0158
	internal bool SetRaces(Map<TAG_RACE, int> raceCounts)
	{
		this.InitRaces(raceCounts);
		int num = 0;
		if (raceCounts.ContainsKey(TAG_RACE.ALL))
		{
			num = raceCounts[TAG_RACE.ALL];
		}
		TAG_RACE tag_RACE = TAG_RACE.ALL;
		int num2 = 0;
		int num3 = 0;
		foreach (KeyValuePair<TAG_RACE, int> keyValuePair in raceCounts)
		{
			if (keyValuePair.Key != TAG_RACE.ALL)
			{
				int num4 = keyValuePair.Value + num;
				if (num4 >= num2 && num4 > 0)
				{
					num3 = num2;
					num2 = num4;
					tag_RACE = keyValuePair.Key;
				}
				else if (num4 >= num3 && num4 > 0)
				{
					num3 = num4;
					TAG_RACE key = keyValuePair.Key;
				}
			}
		}
		if (tag_RACE == TAG_RACE.ALL || num2 == num3)
		{
			if (num2 == 0)
			{
				this.m_singleTribeWithoutCountWrapper.SetActive(false);
				this.m_singleTribeWithCountWrapper.SetActive(false);
			}
			else
			{
				this.m_singleTribeWithoutCountWrapper.SetActive(true);
				this.m_singleTribeWithCountWrapper.SetActive(false);
			}
		}
		else
		{
			this.m_singleTribeWithoutCountWrapper.SetActive(false);
			this.m_singleTribeWithCountWrapper.SetActive(true);
			this.m_singleTribeWithCountNumber.Text = num2.ToString();
			this.m_singleTribeWithCountName.Text = GameStrings.GetRaceNameBattlegrounds(tag_RACE);
		}
		return this.m_racesInitialized;
	}

	// Token: 0x06002F65 RID: 12133 RVA: 0x000F208C File Offset: 0x000F028C
	private void InitRaces(Map<TAG_RACE, int> raceCounts)
	{
		if (this.m_racesInitialized)
		{
			return;
		}
		if (raceCounts.Count == 0)
		{
			return;
		}
		this.m_racesInitialized = true;
	}

	// Token: 0x04001A6C RID: 6764
	public uint m_maxDisplayItems = 2U;

	// Token: 0x04001A6D RID: 6765
	public List<GameObject> m_recentActionPlaceholders;

	// Token: 0x04001A6E RID: 6766
	public GameObject m_recentActionsParent;

	// Token: 0x04001A6F RID: 6767
	public static int NO_DAMAGE_TARGET = 100000;

	// Token: 0x04001A70 RID: 6768
	private QueueList<PlayerLeaderboardRecentCombatEntry> m_recentCombatEntries = new QueueList<PlayerLeaderboardRecentCombatEntry>();

	// Token: 0x04001A71 RID: 6769
	private const string RECENT_COMBAT_ENTRY_PREFAB = "Recent_Combat_Entry.prefab:74bf698d81967c9498554a64c9db51fc";

	// Token: 0x04001A72 RID: 6770
	private int m_triplesCount;

	// Token: 0x04001A73 RID: 6771
	private int m_winStreakCount;

	// Token: 0x04001A74 RID: 6772
	private int m_techLevelCount = 1;

	// Token: 0x04001A75 RID: 6773
	private bool m_racesInitialized;

	// Token: 0x04001A76 RID: 6774
	public PlayerLeaderboardIcon m_techLevel;

	// Token: 0x04001A77 RID: 6775
	public PlayerLeaderboardIcon m_winStreak;

	// Token: 0x04001A78 RID: 6776
	public PlayerLeaderboardIcon m_triples;

	// Token: 0x04001A79 RID: 6777
	public List<GameObject> m_raceWrappers;

	// Token: 0x04001A7A RID: 6778
	public GameObject m_singleTribeWithCountWrapper;

	// Token: 0x04001A7B RID: 6779
	public UberText m_singleTribeWithCountName;

	// Token: 0x04001A7C RID: 6780
	public UberText m_singleTribeWithCountNumber;

	// Token: 0x04001A7D RID: 6781
	public GameObject m_singleTribeWithoutCountWrapper;

	// Token: 0x04001A7E RID: 6782
	public UberText m_singleTribeWithoutCountName;

	// Token: 0x020016D8 RID: 5848
	public struct RecentCombatInfo
	{
		// Token: 0x0400B22C RID: 45612
		public int ownerId;

		// Token: 0x0400B22D RID: 45613
		public int opponentId;

		// Token: 0x0400B22E RID: 45614
		public int damageTarget;

		// Token: 0x0400B22F RID: 45615
		public int damage;

		// Token: 0x0400B230 RID: 45616
		public int winStreak;

		// Token: 0x0400B231 RID: 45617
		public bool isDefeated;
	}
}
