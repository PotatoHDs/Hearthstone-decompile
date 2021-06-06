using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeaderboardRecentCombatsPanel : PlayerLeaderboardInformationPanel
{
	public struct RecentCombatInfo
	{
		public int ownerId;

		public int opponentId;

		public int damageTarget;

		public int damage;

		public int winStreak;

		public bool isDefeated;
	}

	public uint m_maxDisplayItems = 2u;

	public List<GameObject> m_recentActionPlaceholders;

	public GameObject m_recentActionsParent;

	public static int NO_DAMAGE_TARGET = 100000;

	private QueueList<PlayerLeaderboardRecentCombatEntry> m_recentCombatEntries = new QueueList<PlayerLeaderboardRecentCombatEntry>();

	private const string RECENT_COMBAT_ENTRY_PREFAB = "Recent_Combat_Entry.prefab:74bf698d81967c9498554a64c9db51fc";

	private int m_triplesCount;

	private int m_winStreakCount;

	private int m_techLevelCount = 1;

	private bool m_racesInitialized;

	public PlayerLeaderboardIcon m_techLevel;

	public PlayerLeaderboardIcon m_winStreak;

	public PlayerLeaderboardIcon m_triples;

	public List<GameObject> m_raceWrappers;

	public GameObject m_singleTribeWithCountWrapper;

	public UberText m_singleTribeWithCountName;

	public UberText m_singleTribeWithCountNumber;

	public GameObject m_singleTribeWithoutCountWrapper;

	public UberText m_singleTribeWithoutCountName;

	public void Awake()
	{
		for (int i = 0; i < m_recentActionPlaceholders.Count; i++)
		{
			PlayerLeaderboardRecentCombatEntry component = m_recentActionPlaceholders[i].GetComponent<PlayerLeaderboardRecentCombatEntry>();
			component.m_iconOpponentSwords.SetActive(value: false);
			component.m_iconOwnerSwords.SetActive(value: false);
			component.m_iconOpponentSplat.SetActive(value: false);
			component.m_iconOwnerSplat.SetActive(value: false);
			component.m_opponentTileActor.gameObject.SetActive(value: false);
			component.m_background.gameObject.SetActive(value: true);
		}
		m_techLevel.ClearText();
		m_winStreak.ClearText();
		m_triples.ClearText();
	}

	public bool HasRecentCombats()
	{
		return m_recentCombatEntries.Count > 0;
	}

	public void ClearRecentCombats()
	{
		while (m_recentCombatEntries.Count > 0)
		{
			UnityEngine.Object.Destroy(m_recentCombatEntries.Dequeue().gameObject);
		}
	}

	public void SetTriples(int triples)
	{
		m_triplesCount = triples;
		UpdateLayout();
	}

	public void SetTechLevel(int techLevel)
	{
		m_techLevelCount = techLevel;
		UpdateLayout();
	}

	public void AddRecentCombat(PlayerLeaderboardCard source, RecentCombatInfo recentCombatInfo)
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Recent_Combat_Entry.prefab:74bf698d81967c9498554a64c9db51fc");
		if (gameObject == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardRecentCombatsPanel.AddRecentCombat() - FAILED to load GameObject \"{0}\"", "Recent_Combat_Entry.prefab:74bf698d81967c9498554a64c9db51fc");
			return;
		}
		PlayerLeaderboardRecentCombatEntry component = gameObject.GetComponent<PlayerLeaderboardRecentCombatEntry>();
		if (component == null)
		{
			Debug.LogWarningFormat("PlayerLeaderboardRecentCombatsPanel.AddRecentCombat() - ERROR GameObject \"{0}\" has no PlayerLeaderboardRecentCombatEntry component", "Recent_Combat_Entry.prefab:74bf698d81967c9498554a64c9db51fc");
			return;
		}
		TransformUtil.Identity(component.transform);
		component.Load(source, recentCombatInfo);
		if (m_recentCombatEntries.Count == m_maxDisplayItems)
		{
			UnityEngine.Object.Destroy(m_recentCombatEntries.Dequeue().gameObject);
		}
		m_recentCombatEntries.Enqueue(component);
		m_winStreakCount = recentCombatInfo.winStreak;
		UpdateLayout();
	}

	private void UpdateTechLevelPlaymaker()
	{
		PlayMakerFSM component = m_techLevel.GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			Log.Gameplay.PrintError("No playmaker attached to tech level icon.");
			return;
		}
		component.FsmVariables.GetFsmInt("TechLevel").Value = m_techLevelCount;
		component.SendEvent("Action");
	}

	private void UpdateLayout()
	{
		if (m_triples != null)
		{
			m_triples.SetText(m_triplesCount.ToString());
		}
		if (m_winStreak != null)
		{
			m_winStreak.SetText(m_winStreakCount.ToString());
		}
		if (m_techLevel != null)
		{
			UpdateTechLevelPlaymaker();
		}
		if (m_recentActionPlaceholders == null)
		{
			return;
		}
		for (int i = 0; i < m_recentActionPlaceholders.Count; i++)
		{
			if (m_recentCombatEntries.Count > i)
			{
				int index = Math.Min(m_recentActionPlaceholders.Count, m_recentCombatEntries.Count) - (1 + i);
				GameObject gameObject = m_recentActionPlaceholders[index];
				GameObject obj = m_recentCombatEntries[i].gameObject;
				obj.transform.parent = gameObject.transform.parent;
				TransformUtil.CopyLocal(obj, gameObject);
				gameObject.SetActive(value: false);
			}
			else
			{
				m_recentActionPlaceholders[i].SetActive(value: true);
			}
		}
	}

	internal bool SetRaces(Map<TAG_RACE, int> raceCounts)
	{
		InitRaces(raceCounts);
		int num = 0;
		if (raceCounts.ContainsKey(TAG_RACE.ALL))
		{
			num = raceCounts[TAG_RACE.ALL];
		}
		TAG_RACE tAG_RACE = TAG_RACE.ALL;
		int num2 = 0;
		int num3 = 0;
		foreach (KeyValuePair<TAG_RACE, int> raceCount in raceCounts)
		{
			if (raceCount.Key != TAG_RACE.ALL)
			{
				int num4 = raceCount.Value + num;
				if (num4 >= num2 && num4 > 0)
				{
					num3 = num2;
					num2 = num4;
					tAG_RACE = raceCount.Key;
				}
				else if (num4 >= num3 && num4 > 0)
				{
					num3 = num4;
					_ = raceCount.Key;
				}
			}
		}
		if (tAG_RACE == TAG_RACE.ALL || num2 == num3)
		{
			if (num2 == 0)
			{
				m_singleTribeWithoutCountWrapper.SetActive(value: false);
				m_singleTribeWithCountWrapper.SetActive(value: false);
			}
			else
			{
				m_singleTribeWithoutCountWrapper.SetActive(value: true);
				m_singleTribeWithCountWrapper.SetActive(value: false);
			}
		}
		else
		{
			m_singleTribeWithoutCountWrapper.SetActive(value: false);
			m_singleTribeWithCountWrapper.SetActive(value: true);
			m_singleTribeWithCountNumber.Text = num2.ToString();
			m_singleTribeWithCountName.Text = GameStrings.GetRaceNameBattlegrounds(tAG_RACE);
		}
		return m_racesInitialized;
	}

	private void InitRaces(Map<TAG_RACE, int> raceCounts)
	{
		if (!m_racesInitialized && raceCounts.Count != 0)
		{
			m_racesInitialized = true;
		}
	}
}
