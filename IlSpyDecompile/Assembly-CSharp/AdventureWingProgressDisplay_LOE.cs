using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

[CustomEditClass]
public class AdventureWingProgressDisplay_LOE : AdventureWingProgressDisplay
{
	public UberText m_hangingSignText;

	public PegUIElement m_hangingSignHitArea;

	public PegUIElement m_completeStaffHitArea;

	public List<GameObject> m_emptyStaffObjects = new List<GameObject>();

	public List<GameObject> m_visibleStaffObjects = new List<GameObject>();

	public List<GameObject> m_rodObjects = new List<GameObject>();

	public List<GameObject> m_headObjects = new List<GameObject>();

	public List<GameObject> m_pearlObjects = new List<GameObject>();

	[CustomEditField(Sections = "VO")]
	public string m_hangingSignQuotePrefab;

	[CustomEditField(Sections = "VO")]
	public string m_hangingSignQuoteVOLine;

	[CustomEditField(Sections = "VO")]
	public string m_completeStaffQuotePrefab;

	[CustomEditField(Sections = "VO")]
	public string m_completeStaffQuoteVOLine;

	private const string s_WingDisappearAnimateEventName = "OnWingDisappear";

	private const string s_WingReappearAnimateEventName = "OnWingReappear";

	private const string s_CompleteAnimationVarName = "AnimationComplete";

	private bool m_rodComplete;

	private bool m_headComplete;

	private bool m_pearlComplete;

	private bool m_finalWingComplete;

	private bool m_animating;

	private void Awake()
	{
		SetObjectsVisibility(m_emptyStaffObjects, show: true);
		SetObjectsVisibility(m_rodObjects, show: false);
		SetObjectsVisibility(m_headObjects, show: false);
		SetObjectsVisibility(m_pearlObjects, show: false);
		SetObjectsVisibility(m_visibleStaffObjects, show: false);
		if (m_hangingSignHitArea != null)
		{
			m_hangingSignHitArea.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnHangingSignClick();
			});
		}
		if (m_completeStaffHitArea != null)
		{
			m_completeStaffHitArea.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnCompleteStaffClick();
			});
		}
	}

	private void Update()
	{
		if (AdventureScene.Get().IsDevMode)
		{
			if (InputCollection.GetKeyDown(KeyCode.C))
			{
				StartCoroutine(PlayCompleteAnimationCoroutine(GetComponent<PlayMakerFSM>(), "OnWingDisappear", null, Option.INVALID));
			}
			else if (InputCollection.GetKeyDown(KeyCode.V))
			{
				StartCoroutine(PlayCompleteAnimationCoroutine(GetComponent<PlayMakerFSM>(), "OnWingReappear", null, Option.INVALID));
			}
		}
	}

	public override void UpdateProgress(WingDbId wingDbId, bool linearComplete)
	{
		switch (wingDbId)
		{
		case WingDbId.LOE_TEMPLE_OF_ORSIS:
			m_rodComplete = linearComplete;
			break;
		case WingDbId.LOE_ULDAMAN:
			m_headComplete = linearComplete;
			break;
		case WingDbId.LOE_RUINED_CITY:
			m_pearlComplete = linearComplete;
			break;
		case WingDbId.LOE_HALL_OF_EXPLORERS:
			m_finalWingComplete = linearComplete;
			break;
		}
		UpdatePartVisibility();
	}

	public override bool HasProgressAnimationToPlay()
	{
		if (!m_rodComplete || !m_headComplete || !m_pearlComplete)
		{
			return false;
		}
		if (m_finalWingComplete)
		{
			return !Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_REAPPEAR, defaultVal: false);
		}
		return !Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_DISAPPEAR, defaultVal: false);
	}

	public override void PlayProgressAnimation(OnAnimationComplete onAnimComplete = null)
	{
		if (!m_rodComplete || !m_headComplete || !m_pearlComplete)
		{
			onAnimComplete?.Invoke();
			return;
		}
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			onAnimComplete?.Invoke();
		}
		else if (!m_finalWingComplete)
		{
			if (Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_DISAPPEAR, defaultVal: false))
			{
				onAnimComplete?.Invoke();
			}
			else
			{
				StartCoroutine(PlayCompleteAnimationCoroutine(component, "OnWingDisappear", onAnimComplete, Option.HAS_SEEN_LOE_STAFF_DISAPPEAR));
			}
		}
		else if (Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_REAPPEAR, defaultVal: false))
		{
			onAnimComplete?.Invoke();
		}
		else
		{
			StartCoroutine(PlayCompleteAnimationCoroutine(component, "OnWingReappear", onAnimComplete, Option.HAS_SEEN_LOE_STAFF_REAPPEAR));
		}
	}

	private void UpdatePartVisibility()
	{
		bool @bool = Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_DISAPPEAR, defaultVal: false);
		if (m_finalWingComplete)
		{
			bool bool2 = Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_REAPPEAR, defaultVal: false);
			SetObjectsVisibility(m_emptyStaffObjects, show: false);
			SetObjectsVisibility(m_rodObjects, m_rodComplete && bool2);
			SetObjectsVisibility(m_headObjects, m_headComplete && bool2);
			SetObjectsVisibility(m_pearlObjects, m_pearlComplete && bool2);
			SetObjectsVisibility(m_visibleStaffObjects, show: true);
		}
		else
		{
			bool flag = m_rodComplete && !@bool;
			bool flag2 = m_headComplete && !@bool;
			bool flag3 = m_pearlComplete && !@bool;
			bool flag4 = flag || flag2 || flag3;
			SetObjectsVisibility(m_emptyStaffObjects, !flag4);
			SetObjectsVisibility(m_rodObjects, flag);
			SetObjectsVisibility(m_headObjects, flag2);
			SetObjectsVisibility(m_pearlObjects, flag3);
			SetObjectsVisibility(m_visibleStaffObjects, flag4);
		}
		if (m_hangingSignText != null)
		{
			m_hangingSignText.Text = (@bool ? GameStrings.Get("GLUE_ADVENTURE_LOE_STAFF_DISAPPEARED") : GameStrings.Get("GLUE_ADVENTURE_LOE_STAFF_RESERVED"));
		}
		if (m_completeStaffHitArea != null)
		{
			m_completeStaffHitArea.gameObject.SetActive(m_finalWingComplete && m_rodComplete && m_headComplete && m_pearlComplete);
		}
		if (m_hangingSignHitArea != null)
		{
			m_hangingSignHitArea.SetEnabled(!m_finalWingComplete && !m_rodComplete && !m_headComplete && !m_pearlComplete);
		}
	}

	private static void SetObjectsVisibility(List<GameObject> objs, bool show)
	{
		foreach (GameObject obj in objs)
		{
			if (obj != null)
			{
				obj.SetActive(show);
			}
		}
	}

	private IEnumerator PlayCompleteAnimationCoroutine(PlayMakerFSM fsm, string eventName, OnAnimationComplete onAnimComplete, Option seenOption)
	{
		FsmBool animComplete = fsm.FsmVariables.FindFsmBool("AnimationComplete");
		fsm.SendEvent(eventName);
		m_animating = true;
		if (animComplete != null)
		{
			while (!animComplete.Value)
			{
				yield return null;
			}
		}
		m_animating = false;
		if (seenOption != 0)
		{
			Options.Get().SetBool(seenOption, val: true);
		}
		onAnimComplete?.Invoke();
	}

	private void OnHangingSignClick()
	{
		if (!m_animating && !m_rodComplete && !m_headComplete && !m_pearlComplete && !string.IsNullOrEmpty(m_hangingSignQuotePrefab) && !string.IsNullOrEmpty(m_hangingSignQuoteVOLine))
		{
			string legacyAssetName = new AssetReference(m_hangingSignQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(m_hangingSignQuotePrefab, GameStrings.Get(legacyAssetName), m_hangingSignQuoteVOLine);
		}
	}

	private void OnCompleteStaffClick()
	{
		if (!m_animating && m_rodComplete && m_headComplete && m_pearlComplete && m_finalWingComplete && !string.IsNullOrEmpty(m_completeStaffQuotePrefab) && !string.IsNullOrEmpty(m_completeStaffQuoteVOLine))
		{
			string legacyAssetName = new AssetReference(m_completeStaffQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(m_completeStaffQuotePrefab, GameStrings.Get(legacyAssetName), m_completeStaffQuoteVOLine);
		}
	}
}
