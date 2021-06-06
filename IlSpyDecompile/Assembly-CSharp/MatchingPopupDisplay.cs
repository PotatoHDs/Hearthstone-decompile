using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using PegasusShared;
using UnityEngine;

public class MatchingPopupDisplay : TransitionPopup
{
	public UberText m_tipOfTheDay;

	public GameObject m_nameContainer;

	public GameObject m_wildVines;

	public GameObject m_classicPewter;

	private List<GameObject> m_spinnerTexts = new List<GameObject>();

	private SceneMgr.Mode m_gameMode;

	private const int NUM_SPINNER_ENTRIES = 10;

	protected override void Awake()
	{
		base.Awake();
		m_nameContainer.SetActive(value: false);
		m_title.gameObject.SetActive(value: false);
		m_tipOfTheDay.gameObject.SetActive(value: false);
		m_wildVines.SetActive(value: false);
		m_classicPewter.SetActive(value: false);
		SoundManager.Get().Load("FindOpponent_mechanism_start.prefab:effa04f444ca08840b677d98fc8abf39");
	}

	public override void Hide()
	{
		if (m_shown)
		{
			Navigation.RemoveHandler(OnNavigateBack);
			base.Hide();
		}
	}

	public override void Show()
	{
		SetupSpinnerText();
		UpdateTipOfTheDay();
		GenerateRandomSpinnerTexts(IsMultiOpponentGame());
		m_title.Text = GetTitleTextBasedOnScenario();
		base.Show();
	}

	protected override void OnGameConnecting(FindGameEventData eventData)
	{
		base.OnGameConnecting(eventData);
		IncreaseTooltipProgress();
	}

	protected override void OnGameEntered(FindGameEventData eventData)
	{
		EnableCancelButtonIfPossible();
	}

	protected override void OnGameDelayed(FindGameEventData eventData)
	{
		EnableCancelButtonIfPossible();
	}

	protected override void OnAnimateShowFinished()
	{
		base.OnAnimateShowFinished();
		EnableCancelButtonIfPossible();
	}

	private void SetupSpinnerText()
	{
		for (int i = 1; i <= 10; i++)
		{
			GameObject item = SceneUtils.FindChild(base.gameObject, "NAME_" + i).gameObject;
			m_spinnerTexts.Add(item);
		}
	}

	private void GenerateRandomSpinnerTexts(bool isPlural)
	{
		string text = (isPlural ? "GLUE_SPINNER_PLURAL_" : "GLUE_SPINNER_");
		int num = 1;
		List<string> list = new List<string>();
		while (true)
		{
			string text2 = GameStrings.Get(text + num);
			if (text2 == text + num)
			{
				break;
			}
			list.Add(text2);
			num++;
		}
		SceneUtils.FindChild(base.gameObject, "NAME_PerfectOpponent").gameObject.GetComponent<UberText>().Text = GetWorthyOpponentTextBasedOnScenario();
		for (num = 0; num < 10; num++)
		{
			int index = Mathf.FloorToInt(Random.value * (float)list.Count);
			m_spinnerTexts[num].GetComponent<UberText>().Text = list[index];
			list.RemoveAt(index);
		}
	}

	private IEnumerator StopSpinnerDelay()
	{
		yield return new WaitForSeconds(3.5f);
		Hide();
	}

	private bool OnNavigateBack()
	{
		if (!m_cancelButton.gameObject.activeSelf)
		{
			return false;
		}
		GetComponent<PlayMakerFSM>().SendEvent("Cancel");
		FireMatchCanceledEvent();
		if (FriendChallengeMgr.Get() != null)
		{
			FriendChallengeMgr.Get().CancelChallenge();
		}
		if (PartyManager.Get().IsInParty() && PartyManager.Get().IsPartyLeader())
		{
			PartyManager.Get().CancelQueue();
		}
		return true;
	}

	protected override void OnCancelButtonReleased(UIEvent e)
	{
		base.OnCancelButtonReleased(e);
		if (PartyManager.Get().IsInParty() && !PartyManager.Get().IsPartyLeader())
		{
			PartyManager.Get().CancelQueue();
		}
		else
		{
			Navigation.GoBack();
		}
	}

	private void UpdateTipOfTheDay()
	{
		m_gameMode = SceneMgr.Get().GetMode();
		if (m_gameMode == SceneMgr.Mode.TOURNAMENT)
		{
			m_tipOfTheDay.Text = GameStrings.GetTip(TipCategory.PLAY, Options.Get().GetInt(Option.TIP_PLAY_PROGRESS, 0));
		}
		else if (m_gameMode == SceneMgr.Mode.DRAFT)
		{
			m_tipOfTheDay.Text = GameStrings.GetTip(TipCategory.FORGE, Options.Get().GetInt(Option.TIP_FORGE_PROGRESS, 0));
		}
		else if (m_gameMode == SceneMgr.Mode.BACON)
		{
			m_tipOfTheDay.Text = GameStrings.GetTip(TipCategory.BACON, int.MaxValue, TipCategory.BACON);
		}
		else if (m_gameMode == SceneMgr.Mode.TAVERN_BRAWL)
		{
			if (TavernBrawlManager.Get().IsCurrentSeasonSessionBased)
			{
				m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.HEROICBRAWL);
			}
			else
			{
				m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.TAVERNBRAWL);
			}
		}
		else if (m_gameMode == SceneMgr.Mode.PVP_DUNGEON_RUN)
		{
			m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.DUELS);
		}
		else
		{
			m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.DEFAULT);
		}
	}

	private void IncreaseTooltipProgress()
	{
		if (m_gameMode == SceneMgr.Mode.TOURNAMENT)
		{
			Options.Get().SetInt(Option.TIP_PLAY_PROGRESS, Options.Get().GetInt(Option.TIP_PLAY_PROGRESS, 0) + 1);
		}
		else if (m_gameMode == SceneMgr.Mode.DRAFT)
		{
			Options.Get().SetInt(Option.TIP_FORGE_PROGRESS, Options.Get().GetInt(Option.TIP_FORGE_PROGRESS, 0) + 1);
		}
	}

	protected override void ShowPopup()
	{
		SoundManager.Get().LoadAndPlay("FindOpponent_mechanism_start.prefab:effa04f444ca08840b677d98fc8abf39");
		base.ShowPopup();
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		FsmBool fsmBool = component.FsmVariables.FindFsmBool("PlaySpinningMusic");
		if (fsmBool != null)
		{
			fsmBool.Value = m_gameMode != SceneMgr.Mode.TAVERN_BRAWL;
		}
		component.SendEvent("Birth");
		SceneUtils.EnableRenderers(m_nameContainer, enable: false);
		m_title.gameObject.SetActive(value: true);
		m_tipOfTheDay.gameObject.SetActive(value: true);
		bool active = false;
		bool active2 = false;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
		{
			switch (m_gameType)
			{
			case GameType.GT_RANKED:
				active = m_formatType == FormatType.FT_WILD;
				active2 = m_formatType == FormatType.FT_CLASSIC;
				break;
			case GameType.GT_CASUAL:
			{
				if (!m_deckId.HasValue)
				{
					break;
				}
				CollectionManager collectionManager = CollectionManager.Get();
				if (collectionManager != null)
				{
					CollectionDeck deck = collectionManager.GetDeck(m_deckId.Value);
					if (deck != null)
					{
						active = deck.FormatType == FormatType.FT_WILD;
						active2 = deck.FormatType == FormatType.FT_CLASSIC;
					}
				}
				break;
			}
			}
		}
		m_wildVines.SetActive(active);
		m_classicPewter.SetActive(active2);
		Navigation.Push(OnNavigateBack);
	}

	protected override void OnGameplaySceneLoaded()
	{
		m_nameContainer.SetActive(value: true);
		GetComponent<PlayMakerFSM>().SendEvent("Death");
		StartCoroutine(StopSpinnerDelay());
		Navigation.Clear();
	}

	private string GetTitleTextBasedOnScenario()
	{
		if (!IsMultiOpponentGame())
		{
			return GameStrings.Get("GLUE_MATCHMAKER_FINDING_OPPONENT");
		}
		return GameStrings.Get("GLUE_MATCHMAKER_FINDING_OPPONENTS");
	}

	private string GetWorthyOpponentTextBasedOnScenario()
	{
		if (!IsMultiOpponentGame())
		{
			return GameStrings.Get("GLUE_MATCHMAKER_PERFECT_OPPONENT");
		}
		return GameStrings.Get("GLUE_MATCHMAKER_PERFECT_OPPONENTS");
	}

	private bool IsMultiOpponentGame()
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(m_scenarioId);
		if (record == null)
		{
			return false;
		}
		return record.Players > 2;
	}
}
