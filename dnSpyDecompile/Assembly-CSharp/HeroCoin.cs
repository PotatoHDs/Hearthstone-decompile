using System;
using System.Collections;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x02000743 RID: 1859
public class HeroCoin : PegUIElement
{
	// Token: 0x0600694F RID: 26959 RVA: 0x0022506B File Offset: 0x0022326B
	protected override void Awake()
	{
		base.Awake();
		this.m_tooltip.SetActive(false);
	}

	// Token: 0x06006950 RID: 26960 RVA: 0x0022507F File Offset: 0x0022327F
	private void Start()
	{
		this.m_coinLabel.Text = GameStrings.Get("GLOBAL_PLAY");
	}

	// Token: 0x06006951 RID: 26961 RVA: 0x00225096 File Offset: 0x00223296
	protected override void OnDestroy()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameLoaded));
		}
		base.OnDestroy();
	}

	// Token: 0x06006952 RID: 26962 RVA: 0x002250BC File Offset: 0x002232BC
	public int GetMissionId()
	{
		return this.m_missionID;
	}

	// Token: 0x06006953 RID: 26963 RVA: 0x002250C4 File Offset: 0x002232C4
	public void SetCoinInfo(UnityEngine.Vector2 goldTexture, UnityEngine.Vector2 grayTexture, UnityEngine.Vector2 crackTexture, int missionID)
	{
		List<Material> materials = base.GetComponent<Renderer>().GetMaterials();
		this.m_material = materials[0];
		this.m_grayMaterial = materials[1];
		this.m_goldTexture = goldTexture;
		this.m_material.mainTextureOffset = this.m_goldTexture;
		this.m_grayTexture = grayTexture;
		this.m_grayMaterial.mainTextureOffset = this.m_grayTexture;
		this.m_material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
		this.m_grayMaterial.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
		this.m_crackTexture = crackTexture;
		this.m_cracks.GetComponent<Renderer>().GetMaterial().mainTextureOffset = this.m_crackTexture;
		this.m_missionID = missionID;
		this.m_tooltipText.Text = GameUtils.GetMissionHeroName(missionID);
		this.m_originalPosition = base.transform.localPosition;
		this.m_originalXPosition = this.m_coinX.transform.localPosition;
	}

	// Token: 0x06006954 RID: 26964 RVA: 0x002251DC File Offset: 0x002233DC
	public void SetCoinPressCallback(HeroCoin.CoinPressCallback callback)
	{
		this.m_coinPressCallback = callback;
	}

	// Token: 0x06006955 RID: 26965 RVA: 0x002251E5 File Offset: 0x002233E5
	public void SetLessonAsset(string lessonAsset)
	{
		this.m_lessonAsset = lessonAsset;
	}

	// Token: 0x06006956 RID: 26966 RVA: 0x002251EE File Offset: 0x002233EE
	public string GetLessonAsset()
	{
		return this.m_lessonAsset;
	}

	// Token: 0x06006957 RID: 26967 RVA: 0x002251F8 File Offset: 0x002233F8
	public void SetProgress(HeroCoin.CoinStatus status)
	{
		base.gameObject.SetActive(true);
		this.m_currentStatus = status;
		if (status == HeroCoin.CoinStatus.DEFEATED)
		{
			this.m_material.mainTextureOffset = this.m_grayTexture;
			this.m_cracks.SetActive(true);
			this.m_coinX.SetActive(true);
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			this.m_inputEnabled = false;
			this.m_coinLabel.gameObject.SetActive(false);
			return;
		}
		if (status == HeroCoin.CoinStatus.ACTIVE)
		{
			this.m_cracks.SetActive(false);
			this.m_coinX.SetActive(false);
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			this.m_inputEnabled = true;
			return;
		}
		if (status == HeroCoin.CoinStatus.UNREVEALED_TO_ACTIVE)
		{
			this.m_material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
			this.m_grayMaterial.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
			Hashtable args = iTween.Hash(new object[]
			{
				"y",
				3f,
				"z",
				this.m_originalPosition.z - 0.2f,
				"time",
				0.5f,
				"isLocal",
				true,
				"easetype",
				iTween.EaseType.easeOutCubic
			});
			iTween.MoveTo(base.gameObject, args);
			Hashtable args2 = iTween.Hash(new object[]
			{
				"rotation",
				new Vector3(0f, 0f, 0f),
				"time",
				1f,
				"isLocal",
				true,
				"delay",
				0.5f,
				"easetype",
				iTween.EaseType.easeOutElastic
			});
			iTween.RotateTo(base.gameObject, args2);
			Hashtable args3 = iTween.Hash(new object[]
			{
				"y",
				this.m_originalPosition.y,
				"z",
				this.m_originalPosition.z,
				"time",
				0.5f,
				"isLocal",
				true,
				"delay",
				1.75f,
				"easetype",
				iTween.EaseType.easeOutCubic
			});
			iTween.MoveTo(base.gameObject, args3);
			Hashtable args4 = iTween.Hash(new object[]
			{
				"from",
				0,
				"to",
				1,
				"time",
				0.25f,
				"delay",
				1.5f,
				"easetype",
				iTween.EaseType.easeOutCirc,
				"onupdate",
				"OnUpdateAlphaVal",
				"oncomplete",
				"EnableInput",
				"oncompletetarget",
				base.gameObject
			});
			iTween.ValueTo(base.gameObject, args4);
			SoundManager.Get().LoadAndPlay("tutorial_mission_hero_coin_rises.prefab:42026163dad364742abef7e15cb2e6cc", base.gameObject);
			base.StartCoroutine(this.ShowCoinText());
			this.m_inputEnabled = false;
			return;
		}
		if (status == HeroCoin.CoinStatus.ACTIVE_TO_DEFEATED)
		{
			this.m_coinX.transform.localPosition = new Vector3(0f, 10f, UniversalInputManager.UsePhoneUI ? 0f : -0.23f);
			this.m_cracks.SetActive(true);
			this.m_coinX.SetActive(true);
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			this.m_inputEnabled = false;
			RenderUtils.SetAlpha(this.m_coinX, 0f);
			RenderUtils.SetAlpha(this.m_cracks, 0f);
			Hashtable args5 = iTween.Hash(new object[]
			{
				"y",
				this.m_originalXPosition.y,
				"z",
				this.m_originalXPosition.z,
				"time",
				0.25f,
				"isLocal",
				true,
				"easetype",
				iTween.EaseType.easeInCirc
			});
			iTween.MoveTo(this.m_coinX, args5);
			Hashtable args6 = iTween.Hash(new object[]
			{
				"amount",
				1,
				"delay",
				0,
				"time",
				0.25f,
				"easeType",
				iTween.EaseType.easeInCirc
			});
			iTween.FadeTo(this.m_coinX, args6);
			Hashtable args7 = iTween.Hash(new object[]
			{
				"amount",
				1,
				"delay",
				0.15f,
				"time",
				0.25f,
				"easeType",
				iTween.EaseType.easeInCirc
			});
			iTween.FadeTo(this.m_cracks, args7);
			SoundManager.Get().LoadAndPlay("tutorial_mission_x_descend.prefab:079781bfd4ce602448860b74693d61bd", base.gameObject);
			base.StartCoroutine(this.SwitchCoinToGray());
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
			{
				GameState.Get().GetGameEntity().NotifyOfDefeatCoinAnimation();
				return;
			}
		}
		else
		{
			base.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			this.m_coinLabel.gameObject.SetActive(false);
			this.m_cracks.SetActive(false);
			this.m_coinX.SetActive(false);
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			this.m_inputEnabled = false;
		}
	}

	// Token: 0x06006958 RID: 26968 RVA: 0x00225804 File Offset: 0x00223A04
	public IEnumerator ShowCoinText()
	{
		yield return new WaitForSeconds(1.5f);
		this.m_coinLabel.gameObject.SetActive(true);
		yield break;
	}

	// Token: 0x06006959 RID: 26969 RVA: 0x00225813 File Offset: 0x00223A13
	public IEnumerator SwitchCoinToGray()
	{
		yield return new WaitForSeconds(0.25f);
		this.m_material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
		this.m_grayMaterial.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
		this.m_coinLabel.gameObject.SetActive(false);
		iTween.ShakePosition(Camera.main.gameObject, iTween.Hash(new object[]
		{
			"amount",
			new Vector3(0.2f, 0.2f, 0.2f),
			"name",
			"HeroCoin",
			"time",
			0.5f,
			"delay",
			0,
			"axis",
			"none"
		}));
		yield break;
	}

	// Token: 0x0600695A RID: 26970 RVA: 0x00225822 File Offset: 0x00223A22
	public void HideTooltip()
	{
		this.m_tooltip.SetActive(false);
	}

	// Token: 0x0600695B RID: 26971 RVA: 0x00225830 File Offset: 0x00223A30
	public void EnableInput()
	{
		this.m_inputEnabled = true;
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
	}

	// Token: 0x0600695C RID: 26972 RVA: 0x00225848 File Offset: 0x00223A48
	public void FinishIntroScaling()
	{
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnOut));
		this.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPress));
		this.m_originalMiddleWidth = this.m_middle.GetComponent<Renderer>().bounds.size.x;
	}

	// Token: 0x0600695D RID: 26973 RVA: 0x002258B4 File Offset: 0x00223AB4
	private void ShowTooltip()
	{
		if (this.m_currentStatus == HeroCoin.CoinStatus.UNREVEALED)
		{
			return;
		}
		this.m_tooltip.SetActive(true);
		float num = 0f;
		float x = (this.m_tooltipText.GetTextWorldSpaceBounds().size.x + num) / this.m_originalMiddleWidth;
		TransformUtil.SetLocalScaleX(this.m_middle, x);
		float num2 = this.m_originalMiddleWidth * 0.223f;
		float z = this.m_originalMiddleWidth * 0.01f;
		TransformUtil.SetPoint(this.m_leftCap, Anchor.RIGHT, this.m_middle, Anchor.LEFT, new Vector3(num2, 0f, z));
		TransformUtil.SetPoint(this.m_rightCap, Anchor.LEFT, this.m_middle, Anchor.RIGHT, new Vector3(-num2, 0f, z));
	}

	// Token: 0x0600695E RID: 26974 RVA: 0x00225968 File Offset: 0x00223B68
	private void OnOver(UIEvent e)
	{
		if (this.m_nextTutorialStarted)
		{
			return;
		}
		if (iTween.HasTween(base.gameObject))
		{
			return;
		}
		this.ShowTooltip();
		if (!this.m_inputEnabled)
		{
			return;
		}
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_MOUSE_OVER);
		SoundManager.Get().LoadAndPlay("tutorial_mission_hero_coin_mouse_over.prefab:1fd7d833d7e2ffa469d6572bb08c4582", base.gameObject);
	}

	// Token: 0x0600695F RID: 26975 RVA: 0x002259C3 File Offset: 0x00223BC3
	private void OnOut(UIEvent e)
	{
		this.HideTooltip();
		if (this.m_nextTutorialStarted)
		{
			return;
		}
		if (!this.m_inputEnabled)
		{
			return;
		}
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
	}

	// Token: 0x06006960 RID: 26976 RVA: 0x002259EC File Offset: 0x00223BEC
	private void OnPress(UIEvent e)
	{
		if (!this.m_inputEnabled)
		{
			return;
		}
		if (this.m_nextTutorialStarted)
		{
			return;
		}
		this.m_inputEnabled = false;
		SoundManager.Get().LoadAndPlay("tutorial_mission_hero_coin_play_select.prefab:781c9e3f238e74b4894b5ad9a05c4e35", base.gameObject);
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		if (this.m_coinPressCallback != null)
		{
			this.m_coinPressCallback();
			return;
		}
		LoadingScreen.Get().AddTransitionBlocker();
		LoadingScreen.Get().AddTransitionObject(base.gameObject);
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameLoaded));
		this.StartNextTutorial();
		base.GetComponentInChildren<PlayMakerFSM>().SendEvent("Action");
	}

	// Token: 0x06006961 RID: 26977 RVA: 0x00225A94 File Offset: 0x00223C94
	private void OnGameLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		base.GetComponentInChildren<PlayMakerFSM>().SendEvent("Death");
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnGameLoaded));
		base.StartCoroutine(this.WaitThenTransition());
	}

	// Token: 0x06006962 RID: 26978 RVA: 0x00225ACA File Offset: 0x00223CCA
	private IEnumerator WaitThenTransition()
	{
		yield return new WaitForSeconds(1.25f);
		LoadingScreen.Get().NotifyTransitionBlockerComplete();
		yield break;
	}

	// Token: 0x06006963 RID: 26979 RVA: 0x00225AD4 File Offset: 0x00223CD4
	private void StartNextTutorial()
	{
		this.m_nextTutorialStarted = true;
		FullScreenFXMgr.Get().ResetCount();
		GameMgr.Get().FindGame(GameType.GT_TUTORIAL, FormatType.FT_WILD, this.m_missionID, 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x04005622 RID: 22050
	public GameObject m_coin;

	// Token: 0x04005623 RID: 22051
	public GameObject m_coinX;

	// Token: 0x04005624 RID: 22052
	public GameObject m_cracks;

	// Token: 0x04005625 RID: 22053
	public HighlightState m_highlight;

	// Token: 0x04005626 RID: 22054
	public UberText m_coinLabel;

	// Token: 0x04005627 RID: 22055
	public GameObject m_tooltip;

	// Token: 0x04005628 RID: 22056
	public UberText m_tooltipText;

	// Token: 0x04005629 RID: 22057
	public GameObject m_leftCap;

	// Token: 0x0400562A RID: 22058
	public GameObject m_rightCap;

	// Token: 0x0400562B RID: 22059
	public GameObject m_middle;

	// Token: 0x0400562C RID: 22060
	public GameObject m_explosionPrefab;

	// Token: 0x0400562D RID: 22061
	public bool m_inputEnabled;

	// Token: 0x0400562E RID: 22062
	private UnityEngine.Vector2 m_goldTexture;

	// Token: 0x0400562F RID: 22063
	private UnityEngine.Vector2 m_grayTexture;

	// Token: 0x04005630 RID: 22064
	private UnityEngine.Vector2 m_crackTexture;

	// Token: 0x04005631 RID: 22065
	private string m_lessonAsset;

	// Token: 0x04005632 RID: 22066
	private UnityEngine.Vector2 m_lessonCoords;

	// Token: 0x04005633 RID: 22067
	private int m_missionID;

	// Token: 0x04005634 RID: 22068
	private HeroCoin.CoinStatus m_currentStatus;

	// Token: 0x04005635 RID: 22069
	private float m_originalMiddleWidth;

	// Token: 0x04005636 RID: 22070
	private Vector3 m_originalPosition;

	// Token: 0x04005637 RID: 22071
	private Material m_material;

	// Token: 0x04005638 RID: 22072
	private Material m_grayMaterial;

	// Token: 0x04005639 RID: 22073
	private Vector3 m_originalXPosition;

	// Token: 0x0400563A RID: 22074
	private bool m_nextTutorialStarted;

	// Token: 0x0400563B RID: 22075
	private HeroCoin.CoinPressCallback m_coinPressCallback;

	// Token: 0x0200231B RID: 8987
	// (Invoke) Token: 0x060129D6 RID: 76246
	public delegate void CoinPressCallback();

	// Token: 0x0200231C RID: 8988
	public enum CoinStatus
	{
		// Token: 0x0400E5CA RID: 58826
		ACTIVE,
		// Token: 0x0400E5CB RID: 58827
		DEFEATED,
		// Token: 0x0400E5CC RID: 58828
		UNREVEALED,
		// Token: 0x0400E5CD RID: 58829
		UNREVEALED_TO_ACTIVE,
		// Token: 0x0400E5CE RID: 58830
		ACTIVE_TO_DEFEATED
	}
}
