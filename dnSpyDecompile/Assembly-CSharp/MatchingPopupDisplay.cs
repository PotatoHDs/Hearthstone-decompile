using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using PegasusShared;
using UnityEngine;

// Token: 0x02000373 RID: 883
public class MatchingPopupDisplay : TransitionPopup
{
	// Token: 0x060033EE RID: 13294 RVA: 0x0010A6D0 File Offset: 0x001088D0
	protected override void Awake()
	{
		base.Awake();
		this.m_nameContainer.SetActive(false);
		this.m_title.gameObject.SetActive(false);
		this.m_tipOfTheDay.gameObject.SetActive(false);
		this.m_wildVines.SetActive(false);
		this.m_classicPewter.SetActive(false);
		SoundManager.Get().Load("FindOpponent_mechanism_start.prefab:effa04f444ca08840b677d98fc8abf39");
	}

	// Token: 0x060033EF RID: 13295 RVA: 0x0010A73E File Offset: 0x0010893E
	public override void Hide()
	{
		if (!this.m_shown)
		{
			return;
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		base.Hide();
	}

	// Token: 0x060033F0 RID: 13296 RVA: 0x0010A761 File Offset: 0x00108961
	public override void Show()
	{
		this.SetupSpinnerText();
		this.UpdateTipOfTheDay();
		this.GenerateRandomSpinnerTexts(this.IsMultiOpponentGame());
		this.m_title.Text = this.GetTitleTextBasedOnScenario();
		base.Show();
	}

	// Token: 0x060033F1 RID: 13297 RVA: 0x0010A792 File Offset: 0x00108992
	protected override void OnGameConnecting(FindGameEventData eventData)
	{
		base.OnGameConnecting(eventData);
		this.IncreaseTooltipProgress();
	}

	// Token: 0x060033F2 RID: 13298 RVA: 0x00107BA3 File Offset: 0x00105DA3
	protected override void OnGameEntered(FindGameEventData eventData)
	{
		this.EnableCancelButtonIfPossible();
	}

	// Token: 0x060033F3 RID: 13299 RVA: 0x00107BA3 File Offset: 0x00105DA3
	protected override void OnGameDelayed(FindGameEventData eventData)
	{
		this.EnableCancelButtonIfPossible();
	}

	// Token: 0x060033F4 RID: 13300 RVA: 0x0010A7A1 File Offset: 0x001089A1
	protected override void OnAnimateShowFinished()
	{
		base.OnAnimateShowFinished();
		this.EnableCancelButtonIfPossible();
	}

	// Token: 0x060033F5 RID: 13301 RVA: 0x0010A7B0 File Offset: 0x001089B0
	private void SetupSpinnerText()
	{
		for (int i = 1; i <= 10; i++)
		{
			GameObject gameObject = SceneUtils.FindChild(base.gameObject, "NAME_" + i).gameObject;
			this.m_spinnerTexts.Add(gameObject);
		}
	}

	// Token: 0x060033F6 RID: 13302 RVA: 0x0010A7F8 File Offset: 0x001089F8
	private void GenerateRandomSpinnerTexts(bool isPlural)
	{
		string arg = isPlural ? "GLUE_SPINNER_PLURAL_" : "GLUE_SPINNER_";
		int i = 1;
		List<string> list = new List<string>();
		for (;;)
		{
			string text = GameStrings.Get(arg + i);
			if (text == arg + i)
			{
				break;
			}
			list.Add(text);
			i++;
		}
		SceneUtils.FindChild(base.gameObject, "NAME_PerfectOpponent").gameObject.GetComponent<UberText>().Text = this.GetWorthyOpponentTextBasedOnScenario();
		for (i = 0; i < 10; i++)
		{
			int index = Mathf.FloorToInt(UnityEngine.Random.value * (float)list.Count);
			this.m_spinnerTexts[i].GetComponent<UberText>().Text = list[index];
			list.RemoveAt(index);
		}
	}

	// Token: 0x060033F7 RID: 13303 RVA: 0x0010A8BC File Offset: 0x00108ABC
	private IEnumerator StopSpinnerDelay()
	{
		yield return new WaitForSeconds(3.5f);
		this.Hide();
		yield break;
	}

	// Token: 0x060033F8 RID: 13304 RVA: 0x0010A8CC File Offset: 0x00108ACC
	private bool OnNavigateBack()
	{
		if (!this.m_cancelButton.gameObject.activeSelf)
		{
			return false;
		}
		base.GetComponent<PlayMakerFSM>().SendEvent("Cancel");
		base.FireMatchCanceledEvent();
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

	// Token: 0x060033F9 RID: 13305 RVA: 0x0010A937 File Offset: 0x00108B37
	protected override void OnCancelButtonReleased(UIEvent e)
	{
		base.OnCancelButtonReleased(e);
		if (PartyManager.Get().IsInParty() && !PartyManager.Get().IsPartyLeader())
		{
			PartyManager.Get().CancelQueue();
			return;
		}
		Navigation.GoBack();
	}

	// Token: 0x060033FA RID: 13306 RVA: 0x0010A96C File Offset: 0x00108B6C
	private void UpdateTipOfTheDay()
	{
		this.m_gameMode = SceneMgr.Get().GetMode();
		if (this.m_gameMode == SceneMgr.Mode.TOURNAMENT)
		{
			this.m_tipOfTheDay.Text = GameStrings.GetTip(TipCategory.PLAY, Options.Get().GetInt(Option.TIP_PLAY_PROGRESS, 0), TipCategory.DEFAULT);
			return;
		}
		if (this.m_gameMode == SceneMgr.Mode.DRAFT)
		{
			this.m_tipOfTheDay.Text = GameStrings.GetTip(TipCategory.FORGE, Options.Get().GetInt(Option.TIP_FORGE_PROGRESS, 0), TipCategory.DEFAULT);
			return;
		}
		if (this.m_gameMode == SceneMgr.Mode.BACON)
		{
			this.m_tipOfTheDay.Text = GameStrings.GetTip(TipCategory.BACON, int.MaxValue, TipCategory.BACON);
			return;
		}
		if (this.m_gameMode == SceneMgr.Mode.TAVERN_BRAWL)
		{
			if (TavernBrawlManager.Get().IsCurrentSeasonSessionBased)
			{
				this.m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.HEROICBRAWL);
				return;
			}
			this.m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.TAVERNBRAWL);
			return;
		}
		else
		{
			if (this.m_gameMode == SceneMgr.Mode.PVP_DUNGEON_RUN)
			{
				this.m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.DUELS);
				return;
			}
			this.m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.DEFAULT);
			return;
		}
	}

	// Token: 0x060033FB RID: 13307 RVA: 0x0010AA6C File Offset: 0x00108C6C
	private void IncreaseTooltipProgress()
	{
		if (this.m_gameMode == SceneMgr.Mode.TOURNAMENT)
		{
			Options.Get().SetInt(Option.TIP_PLAY_PROGRESS, Options.Get().GetInt(Option.TIP_PLAY_PROGRESS, 0) + 1);
			return;
		}
		if (this.m_gameMode == SceneMgr.Mode.DRAFT)
		{
			Options.Get().SetInt(Option.TIP_FORGE_PROGRESS, Options.Get().GetInt(Option.TIP_FORGE_PROGRESS, 0) + 1);
		}
	}

	// Token: 0x060033FC RID: 13308 RVA: 0x0010AAC4 File Offset: 0x00108CC4
	protected override void ShowPopup()
	{
		SoundManager.Get().LoadAndPlay("FindOpponent_mechanism_start.prefab:effa04f444ca08840b677d98fc8abf39");
		base.ShowPopup();
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		FsmBool fsmBool = component.FsmVariables.FindFsmBool("PlaySpinningMusic");
		if (fsmBool != null)
		{
			fsmBool.Value = (this.m_gameMode != SceneMgr.Mode.TAVERN_BRAWL);
		}
		component.SendEvent("Birth");
		SceneUtils.EnableRenderers(this.m_nameContainer, false);
		this.m_title.gameObject.SetActive(true);
		this.m_tipOfTheDay.gameObject.SetActive(true);
		bool active = false;
		bool active2 = false;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
		{
			GameType gameType = this.m_gameType;
			if (gameType != GameType.GT_RANKED)
			{
				if (gameType == GameType.GT_CASUAL)
				{
					if (this.m_deckId != null)
					{
						CollectionManager collectionManager = CollectionManager.Get();
						if (collectionManager != null)
						{
							CollectionDeck deck = collectionManager.GetDeck(this.m_deckId.Value);
							if (deck != null)
							{
								active = (deck.FormatType == FormatType.FT_WILD);
								active2 = (deck.FormatType == FormatType.FT_CLASSIC);
							}
						}
					}
				}
			}
			else
			{
				active = (this.m_formatType == FormatType.FT_WILD);
				active2 = (this.m_formatType == FormatType.FT_CLASSIC);
			}
		}
		this.m_wildVines.SetActive(active);
		this.m_classicPewter.SetActive(active2);
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
	}

	// Token: 0x060033FD RID: 13309 RVA: 0x0010ABF7 File Offset: 0x00108DF7
	protected override void OnGameplaySceneLoaded()
	{
		this.m_nameContainer.SetActive(true);
		base.GetComponent<PlayMakerFSM>().SendEvent("Death");
		base.StartCoroutine(this.StopSpinnerDelay());
		Navigation.Clear();
	}

	// Token: 0x060033FE RID: 13310 RVA: 0x0010AC27 File Offset: 0x00108E27
	private string GetTitleTextBasedOnScenario()
	{
		if (!this.IsMultiOpponentGame())
		{
			return GameStrings.Get("GLUE_MATCHMAKER_FINDING_OPPONENT");
		}
		return GameStrings.Get("GLUE_MATCHMAKER_FINDING_OPPONENTS");
	}

	// Token: 0x060033FF RID: 13311 RVA: 0x0010AC46 File Offset: 0x00108E46
	private string GetWorthyOpponentTextBasedOnScenario()
	{
		if (!this.IsMultiOpponentGame())
		{
			return GameStrings.Get("GLUE_MATCHMAKER_PERFECT_OPPONENT");
		}
		return GameStrings.Get("GLUE_MATCHMAKER_PERFECT_OPPONENTS");
	}

	// Token: 0x06003400 RID: 13312 RVA: 0x0010AC68 File Offset: 0x00108E68
	private bool IsMultiOpponentGame()
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(this.m_scenarioId);
		return record != null && record.Players > 2;
	}

	// Token: 0x04001C70 RID: 7280
	public UberText m_tipOfTheDay;

	// Token: 0x04001C71 RID: 7281
	public GameObject m_nameContainer;

	// Token: 0x04001C72 RID: 7282
	public GameObject m_wildVines;

	// Token: 0x04001C73 RID: 7283
	public GameObject m_classicPewter;

	// Token: 0x04001C74 RID: 7284
	private List<GameObject> m_spinnerTexts = new List<GameObject>();

	// Token: 0x04001C75 RID: 7285
	private SceneMgr.Mode m_gameMode;

	// Token: 0x04001C76 RID: 7286
	private const int NUM_SPINNER_ENTRIES = 10;
}
