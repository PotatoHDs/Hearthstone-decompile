using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class LoadingPopupDisplay : TransitionPopup
{
	// Token: 0x06003350 RID: 13136 RVA: 0x0010798C File Offset: 0x00105B8C
	protected override void Awake()
	{
		base.Awake();
		this.GenerateStringNameMaps();
		this.m_title.Text = GameStrings.Get("GLUE_STARTING_GAME");
		base.gameObject.transform.localPosition = new Vector3(-0.05f, 9f, 3.908f);
		SoundManager.Get().Load("StartGame_window_expand_up.prefab:1989383da054858489f420f8e2ac43d4");
		SoundManager.Get().Load("StartGame_window_shrink_down.prefab:07b3273ed29d9df479442d93caa07799");
		SoundManager.Get().Load("StartGame_window_loading_bar_move_down_and_forward.prefab:0b04e30939289024dadd8292dbfd7fef");
		SoundManager.Get().Load("StartGame_window_loading_bar_flip.prefab:fa63e6a9075dba24fae4fddc2fd32a39");
		SoundManager.Get().Load("StartGame_window_bar_filling_loop.prefab:4e8350e37c1218a4cbbd9e93b394cd48");
		SoundManager.Get().Load("StartGame_window_loading_bar_drop.prefab:899774ec312ca8241a5a5e4e300c5d93");
		this.DisableCancelButton();
	}

	// Token: 0x06003351 RID: 13137 RVA: 0x00107A62 File Offset: 0x00105C62
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (SoundManager.Get() != null)
		{
			this.StopLoopingSound();
		}
	}

	// Token: 0x06003352 RID: 13138 RVA: 0x00107A77 File Offset: 0x00105C77
	public override void Hide()
	{
		if (!this.m_shown)
		{
			return;
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		base.Hide();
	}

	// Token: 0x06003353 RID: 13139 RVA: 0x00107A9A File Offset: 0x00105C9A
	protected override bool EnableCancelButtonIfPossible()
	{
		if (!base.EnableCancelButtonIfPossible())
		{
			return false;
		}
		TransformUtil.SetLocalPosX(this.m_queueTab, -0.3234057f);
		return true;
	}

	// Token: 0x06003354 RID: 13140 RVA: 0x00107AB7 File Offset: 0x00105CB7
	protected override void EnableCancelButton()
	{
		this.m_cancelButtonParent.SetActive(true);
		base.EnableCancelButton();
	}

	// Token: 0x06003355 RID: 13141 RVA: 0x00107ACB File Offset: 0x00105CCB
	protected override void DisableCancelButton()
	{
		base.DisableCancelButton();
		this.m_cancelButtonParent.SetActive(false);
	}

	// Token: 0x06003356 RID: 13142 RVA: 0x00107AE0 File Offset: 0x00105CE0
	protected override void AnimateShow()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"name",
			"ShowCancelButton",
			"time",
			30f,
			"ignoretimescale",
			true,
			"oncomplete",
			new Action<object>(this.OnCancelButtonShowTimerCompleted),
			"oncompletetarget",
			base.gameObject
		});
		iTween.Timer(base.gameObject, args);
		this.SetTipOfTheDay();
		this.SetLoadingBarTexture();
		SoundManager.Get().LoadAndPlay("StartGame_window_expand_up.prefab:1989383da054858489f420f8e2ac43d4");
		base.AnimateShow();
		this.m_stopAnimating = false;
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
	}

	// Token: 0x06003357 RID: 13143 RVA: 0x00107BA3 File Offset: 0x00105DA3
	protected void OnCancelButtonShowTimerCompleted(object userData)
	{
		this.EnableCancelButtonIfPossible();
	}

	// Token: 0x06003358 RID: 13144 RVA: 0x00107BAC File Offset: 0x00105DAC
	protected override void OnGameEntered(FindGameEventData eventData)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		base.OnGameEntered(eventData);
	}

	// Token: 0x06003359 RID: 13145 RVA: 0x00107BC2 File Offset: 0x00105DC2
	protected override void OnGameUpdated(FindGameEventData eventData)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		base.OnGameUpdated(eventData);
	}

	// Token: 0x0600335A RID: 13146 RVA: 0x00107BD8 File Offset: 0x00105DD8
	protected override void AnimateHide()
	{
		SoundManager.Get().LoadAndPlay("StartGame_window_shrink_down.prefab:07b3273ed29d9df479442d93caa07799");
		iTween.StopByName(base.gameObject, "ShowCancelButton");
		if (this.m_barAnimating)
		{
			base.StopCoroutine("AnimateBar");
			this.m_barAnimating = false;
			this.StopLoopingSound();
		}
		base.AnimateHide();
	}

	// Token: 0x0600335B RID: 13147 RVA: 0x00107C2F File Offset: 0x00105E2F
	protected override void OnAnimateShowFinished()
	{
		base.OnAnimateShowFinished();
		this.AnimateInLoadingTile();
	}

	// Token: 0x0600335C RID: 13148 RVA: 0x00107C40 File Offset: 0x00105E40
	private void AnimateInLoadingTile()
	{
		if (this.m_stopAnimating)
		{
			this.m_animationStopped = true;
			return;
		}
		this.m_loadingTile.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
		this.m_loadingTile.transform.localPosition = LoadingPopupDisplay.START_POS;
		this.m_progressBar.SetProgressBar(0f);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			LoadingPopupDisplay.MID_POS,
			"isLocal",
			true,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.MoveTo(this.m_loadingTile, args);
		SoundManager.Get().LoadAndPlay("StartGame_window_loading_bar_move_down_and_forward.prefab:0b04e30939289024dadd8292dbfd7fef");
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			LoadingPopupDisplay.END_POS,
			"isLocal",
			true,
			"time",
			0.5f,
			"delay",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutCubic
		});
		iTween.MoveTo(this.m_loadingTile, args2);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"amount",
			new Vector3(180f, 0f, 0f),
			"time",
			0.5f,
			"delay",
			0.8f,
			"easeType",
			iTween.EaseType.easeOutElastic,
			"space",
			Space.Self,
			"name",
			"flip"
		});
		iTween.RotateAdd(this.m_loadingTile, args3);
		this.m_progressBar.SetLabel(this.GetRandomTaskName());
		base.StartCoroutine("AnimateBar");
	}

	// Token: 0x0600335D RID: 13149 RVA: 0x00107E54 File Offset: 0x00106054
	private void AnimateOutLoadingTile()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			LoadingPopupDisplay.MID_POS,
			"isLocal",
			true,
			"time",
			0.25f,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.MoveTo(this.m_loadingTile, args);
		SoundManager.Get().LoadAndPlay("StartGame_window_loading_bar_drop.prefab:899774ec312ca8241a5a5e4e300c5d93");
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			LoadingPopupDisplay.OFFSCREEN_POS,
			"isLocal",
			true,
			"time",
			0.25f,
			"delay",
			0.25f,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"oncomplete",
			"AnimateInLoadingTile",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_loadingTile, args2);
	}

	// Token: 0x0600335E RID: 13150 RVA: 0x00107F7A File Offset: 0x0010617A
	private float GetRandomTaskDuration()
	{
		return 1f + UnityEngine.Random.value * 2f;
	}

	// Token: 0x0600335F RID: 13151 RVA: 0x00107F90 File Offset: 0x00106190
	private string GetRandomTaskName()
	{
		List<string> list;
		if (GameMgr.Get().IsSpectator())
		{
			list = this.m_spectatorTaskNameMap;
		}
		else if (!this.m_taskNameMap.TryGetValue(this.m_adventureId, out list))
		{
			list = this.m_taskNameMap[AdventureDbId.INVALID];
		}
		if (list.Count == 0)
		{
			return "ERROR - OUT OF TASK NAMES!!!";
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		return list[index];
	}

	// Token: 0x06003360 RID: 13152 RVA: 0x00107FF6 File Offset: 0x001061F6
	private IEnumerator AnimateBar()
	{
		this.m_barAnimating = true;
		yield return new WaitForSeconds(0.8f);
		SoundManager.Get().LoadAndPlay("StartGame_window_loading_bar_flip.prefab:fa63e6a9075dba24fae4fddc2fd32a39");
		yield return new WaitForSeconds(0.19999999f);
		float randomTaskDuration = this.GetRandomTaskDuration();
		this.m_progressBar.m_increaseAnimTime = randomTaskDuration;
		this.m_progressBar.AnimateProgress(0f, 1f, iTween.EaseType.easeOutQuad);
		SoundManager.Get().LoadAndPlay("StartGame_window_bar_filling_loop.prefab:4e8350e37c1218a4cbbd9e93b394cd48", null, 1f, new SoundManager.LoadedCallback(this.LoopingSoundLoadedCallback));
		yield return new WaitForSeconds(randomTaskDuration);
		this.StopLoopingSound();
		this.AnimateOutLoadingTile();
		this.m_barAnimating = false;
		yield break;
	}

	// Token: 0x06003361 RID: 13153 RVA: 0x00108005 File Offset: 0x00106205
	private void LoopingSoundLoadedCallback(AudioSource source, object userData)
	{
		this.StopLoopingSound();
		if (this.m_barAnimating)
		{
			this.m_loopSound = source;
			return;
		}
		SoundManager.Get().Stop(source);
	}

	// Token: 0x06003362 RID: 13154 RVA: 0x00108029 File Offset: 0x00106229
	protected override void OnGameplaySceneLoaded()
	{
		base.StartCoroutine(this.StopLoading());
		Navigation.Clear();
	}

	// Token: 0x06003363 RID: 13155 RVA: 0x0010803D File Offset: 0x0010623D
	private IEnumerator StopLoading()
	{
		this.m_stopAnimating = true;
		while (!this.m_animationStopped)
		{
			yield return null;
		}
		if (this.m_adventureId == AdventureDbId.PRACTICE)
		{
			int @int = Options.Get().GetInt(Option.TIP_PRACTICE_PROGRESS, 0);
			Options.Get().SetInt(Option.TIP_PRACTICE_PROGRESS, @int + 1);
		}
		this.Hide();
		yield break;
	}

	// Token: 0x06003364 RID: 13156 RVA: 0x0010804C File Offset: 0x0010624C
	private void StopLoopingSound()
	{
		SoundManager.Get().Stop(this.m_loopSound);
		this.m_loopSound = null;
	}

	// Token: 0x06003365 RID: 13157 RVA: 0x00108066 File Offset: 0x00106266
	private bool OnNavigateBack()
	{
		if (!this.m_cancelButtonParent.gameObject.activeSelf)
		{
			return false;
		}
		base.StartCoroutine(this.StopLoading());
		base.FireMatchCanceledEvent();
		return true;
	}

	// Token: 0x06003366 RID: 13158 RVA: 0x00108090 File Offset: 0x00106290
	protected override void OnCancelButtonReleased(UIEvent e)
	{
		base.OnCancelButtonReleased(e);
		Navigation.GoBack();
	}

	// Token: 0x06003367 RID: 13159 RVA: 0x001080A0 File Offset: 0x001062A0
	private void GenerateStringNameMaps()
	{
		this.GenerateTaskNamesForAdventure(AdventureDbId.INVALID, "GLUE_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.NAXXRAMAS, "GLUE_NAXX_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.BRM, "GLUE_BRM_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.LOE, "GLUE_LOE_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.KARA, "GLUE_KARA_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.ICC, "GLUE_ICC_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.LOOT, "GLUE_LOOT_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.GIL, "GLUE_GIL_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.BOT, "GLUE_BOT_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.TRL, "GLUE_TRL_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.DALARAN, "GLUE_DAL_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.ULDUM, "GLUE_ULD_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.DRAGONS, "GLUE_DRG_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.BTA, "GLUE_BTA_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.BOH, "GLUE_BOH_LOADING_BAR_TASK_");
		this.GenerateTaskNamesForAdventure(AdventureDbId.BOM, "GLUE_BOH_LOADING_BAR_TASK_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.LOOT, "GLUE_TIP_ADVENTURE_LOOT_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.GIL, "GLUE_TIP_ADVENTURE_GIL_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.BOT, "GLUE_TIP_ADVENTURE_BOT_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.TRL, "GLUE_TIP_ADVENTURE_TRL_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.DALARAN, "GLUE_TIP_ADVENTURE_DAL_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.ULDUM, "GLUE_TIP_ADVENTURE_ULD_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.DRAGONS, "GLUE_TIP_ADVENTURE_DRG_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.BTP, "GLUE_TIP_ADVENTURE_BTP_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.BTA, "GLUE_TIP_ADVENTURE_BTA_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.BOH, "GLUE_TIP_ADVENTURE_BOH_");
		this.GenerateTipOfTheDayNamesForAdventure(AdventureDbId.BOM, "GLUE_TIP_ADVENTURE_BOM_");
		this.GenerateTaskNamesForPrefix(this.m_spectatorTaskNameMap, "GLUE_SPECTATOR_LOADING_BAR_TASK_");
	}

	// Token: 0x06003368 RID: 13160 RVA: 0x0010825C File Offset: 0x0010645C
	private void GenerateTaskNamesForAdventure(AdventureDbId adventureId, string prefix)
	{
		List<string> list = new List<string>();
		this.GenerateTaskNamesForPrefix(list, prefix);
		this.m_taskNameMap[adventureId] = list;
	}

	// Token: 0x06003369 RID: 13161 RVA: 0x00108284 File Offset: 0x00106484
	private void GenerateTipOfTheDayNamesForAdventure(AdventureDbId adventureId, string prefix)
	{
		List<string> list = new List<string>();
		this.GenerateTaskNamesForPrefix(list, prefix);
		this.m_adventureTipOfTheDayNameMap[adventureId] = list;
	}

	// Token: 0x0600336A RID: 13162 RVA: 0x001082AC File Offset: 0x001064AC
	private void GenerateTaskNamesForPrefix(List<string> taskNames, string prefix)
	{
		taskNames.Clear();
		for (int i = 1; i < 100; i++)
		{
			string text = prefix + i;
			string text2 = GameStrings.Get(text);
			if (text2 == text)
			{
				break;
			}
			taskNames.Add(text2);
		}
	}

	// Token: 0x0600336B RID: 13163 RVA: 0x001082F0 File Offset: 0x001064F0
	private void SetTipOfTheDay()
	{
		if (this.m_adventureId == AdventureDbId.PRACTICE)
		{
			this.m_tipOfTheDay.Text = GameStrings.GetTip(TipCategory.PRACTICE, Options.Get().GetInt(Option.TIP_PRACTICE_PROGRESS, 0), TipCategory.DEFAULT);
			return;
		}
		if (GameUtils.IsExpansionAdventure(this.m_adventureId))
		{
			List<string> list;
			if (this.m_adventureTipOfTheDayNameMap.TryGetValue(this.m_adventureId, out list) && list != null && list.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list.Count);
				this.m_tipOfTheDay.Text = list[index];
				return;
			}
			this.m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.ADVENTURE);
			return;
		}
		else
		{
			if (this.m_scenarioId == 3539 || this.m_scenarioId == 3459)
			{
				this.m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.BACON);
				return;
			}
			this.m_tipOfTheDay.Text = GameStrings.GetRandomTip(TipCategory.DEFAULT);
			return;
		}
	}

	// Token: 0x0600336C RID: 13164 RVA: 0x001083C4 File Offset: 0x001065C4
	private void SetLoadingBarTexture()
	{
		Texture texture = this.m_barTextures[0].texture;
		foreach (LoadingPopupDisplay.LoadingbarTexture loadingbarTexture in this.m_barTextures)
		{
			if (loadingbarTexture.adventureID == this.m_adventureId || loadingbarTexture.scenarioId == (ScenarioDbId)this.m_scenarioId)
			{
				texture = loadingbarTexture.texture;
				this.m_progressBar.m_barIntensity = loadingbarTexture.m_barIntensity;
				this.m_progressBar.m_barIntensityIncreaseMax = loadingbarTexture.m_barIntensityIncreaseMax;
				break;
			}
		}
		this.m_progressBar.SetBarTexture(texture);
	}

	// Token: 0x04001C2A RID: 7210
	public UberText m_tipOfTheDay;

	// Token: 0x04001C2B RID: 7211
	public ProgressBar m_progressBar;

	// Token: 0x04001C2C RID: 7212
	public GameObject m_loadingTile;

	// Token: 0x04001C2D RID: 7213
	public GameObject m_cancelButtonParent;

	// Token: 0x04001C2E RID: 7214
	public List<LoadingPopupDisplay.LoadingbarTexture> m_barTextures = new List<LoadingPopupDisplay.LoadingbarTexture>();

	// Token: 0x04001C2F RID: 7215
	private Map<AdventureDbId, List<string>> m_taskNameMap = new Map<AdventureDbId, List<string>>();

	// Token: 0x04001C30 RID: 7216
	private Map<AdventureDbId, List<string>> m_adventureTipOfTheDayNameMap = new Map<AdventureDbId, List<string>>();

	// Token: 0x04001C31 RID: 7217
	private List<string> m_spectatorTaskNameMap = new List<string>();

	// Token: 0x04001C32 RID: 7218
	private bool m_stopAnimating;

	// Token: 0x04001C33 RID: 7219
	private bool m_animationStopped;

	// Token: 0x04001C34 RID: 7220
	private AudioSource m_loopSound;

	// Token: 0x04001C35 RID: 7221
	private bool m_barAnimating;

	// Token: 0x04001C36 RID: 7222
	public static readonly Vector3 START_POS = new Vector3(-0.0152f, -0.0894f, -0.0837f);

	// Token: 0x04001C37 RID: 7223
	public static readonly Vector3 MID_POS = new Vector3(-0.0152f, -0.0894f, 0.0226f);

	// Token: 0x04001C38 RID: 7224
	public static readonly Vector3 END_POS = new Vector3(-0.0152f, 0.0368f, 0.0226f);

	// Token: 0x04001C39 RID: 7225
	public static readonly Vector3 OFFSCREEN_POS = new Vector3(-0.0152f, -0.0894f, 0.13f);

	// Token: 0x04001C3A RID: 7226
	private const int TASK_DURATION_VARIATION = 2;

	// Token: 0x04001C3B RID: 7227
	private const float ROTATION_DURATION = 0.5f;

	// Token: 0x04001C3C RID: 7228
	private const float ROTATION_DELAY = 0.5f;

	// Token: 0x04001C3D RID: 7229
	private const float SLIDE_IN_TIME = 0.5f;

	// Token: 0x04001C3E RID: 7230
	private const float SLIDE_OUT_TIME = 0.25f;

	// Token: 0x04001C3F RID: 7231
	private const float RAISE_TIME = 0.5f;

	// Token: 0x04001C40 RID: 7232
	private const float LOWER_TIME = 0.25f;

	// Token: 0x04001C41 RID: 7233
	private const string SHOW_CANCEL_BUTTON_TWEEN_NAME = "ShowCancelButton";

	// Token: 0x04001C42 RID: 7234
	private const float SHOW_CANCEL_BUTTON_THRESHOLD = 30f;

	// Token: 0x02001712 RID: 5906
	[Serializable]
	public class LoadingbarTexture
	{
		// Token: 0x0400B37A RID: 45946
		public AdventureDbId adventureID;

		// Token: 0x0400B37B RID: 45947
		public ScenarioDbId scenarioId;

		// Token: 0x0400B37C RID: 45948
		public Texture texture;

		// Token: 0x0400B37D RID: 45949
		public float m_barIntensity = 1.2f;

		// Token: 0x0400B37E RID: 45950
		public float m_barIntensityIncreaseMax = 3f;
	}
}
