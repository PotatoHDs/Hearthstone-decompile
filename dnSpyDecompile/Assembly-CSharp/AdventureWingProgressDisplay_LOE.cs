using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

// Token: 0x0200005E RID: 94
[CustomEditClass]
public class AdventureWingProgressDisplay_LOE : AdventureWingProgressDisplay
{
	// Token: 0x06000578 RID: 1400 RVA: 0x0001FA74 File Offset: 0x0001DC74
	private void Awake()
	{
		AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_emptyStaffObjects, true);
		AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_rodObjects, false);
		AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_headObjects, false);
		AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_pearlObjects, false);
		AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_visibleStaffObjects, false);
		if (this.m_hangingSignHitArea != null)
		{
			this.m_hangingSignHitArea.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnHangingSignClick();
			});
		}
		if (this.m_completeStaffHitArea != null)
		{
			this.m_completeStaffHitArea.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnCompleteStaffClick();
			});
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0001FB0C File Offset: 0x0001DD0C
	private void Update()
	{
		if (!AdventureScene.Get().IsDevMode)
		{
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.C))
		{
			base.StartCoroutine(this.PlayCompleteAnimationCoroutine(base.GetComponent<PlayMakerFSM>(), "OnWingDisappear", null, Option.INVALID));
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.V))
		{
			base.StartCoroutine(this.PlayCompleteAnimationCoroutine(base.GetComponent<PlayMakerFSM>(), "OnWingReappear", null, Option.INVALID));
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0001FB70 File Offset: 0x0001DD70
	public override void UpdateProgress(WingDbId wingDbId, bool linearComplete)
	{
		switch (wingDbId)
		{
		case WingDbId.LOE_TEMPLE_OF_ORSIS:
			this.m_rodComplete = linearComplete;
			break;
		case WingDbId.LOE_ULDAMAN:
			this.m_headComplete = linearComplete;
			break;
		case WingDbId.LOE_RUINED_CITY:
			this.m_pearlComplete = linearComplete;
			break;
		case WingDbId.LOE_HALL_OF_EXPLORERS:
			this.m_finalWingComplete = linearComplete;
			break;
		}
		this.UpdatePartVisibility();
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x0001FBC0 File Offset: 0x0001DDC0
	public override bool HasProgressAnimationToPlay()
	{
		if (!this.m_rodComplete || !this.m_headComplete || !this.m_pearlComplete)
		{
			return false;
		}
		if (this.m_finalWingComplete)
		{
			return !Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_REAPPEAR, false);
		}
		return !Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_DISAPPEAR, false);
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0001FC18 File Offset: 0x0001DE18
	public override void PlayProgressAnimation(AdventureWingProgressDisplay.OnAnimationComplete onAnimComplete = null)
	{
		if (!this.m_rodComplete || !this.m_headComplete || !this.m_pearlComplete)
		{
			if (onAnimComplete != null)
			{
				onAnimComplete();
			}
			return;
		}
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		if (component == null)
		{
			if (onAnimComplete != null)
			{
				onAnimComplete();
			}
			return;
		}
		if (!this.m_finalWingComplete)
		{
			if (Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_DISAPPEAR, false))
			{
				if (onAnimComplete != null)
				{
					onAnimComplete();
				}
				return;
			}
			base.StartCoroutine(this.PlayCompleteAnimationCoroutine(component, "OnWingDisappear", onAnimComplete, Option.HAS_SEEN_LOE_STAFF_DISAPPEAR));
			return;
		}
		else
		{
			if (Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_REAPPEAR, false))
			{
				if (onAnimComplete != null)
				{
					onAnimComplete();
				}
				return;
			}
			base.StartCoroutine(this.PlayCompleteAnimationCoroutine(component, "OnWingReappear", onAnimComplete, Option.HAS_SEEN_LOE_STAFF_REAPPEAR));
			return;
		}
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0001FCD4 File Offset: 0x0001DED4
	private void UpdatePartVisibility()
	{
		bool @bool = Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_DISAPPEAR, false);
		if (this.m_finalWingComplete)
		{
			bool bool2 = Options.Get().GetBool(Option.HAS_SEEN_LOE_STAFF_REAPPEAR, false);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_emptyStaffObjects, false);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_rodObjects, this.m_rodComplete && bool2);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_headObjects, this.m_headComplete && bool2);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_pearlObjects, this.m_pearlComplete && bool2);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_visibleStaffObjects, true);
		}
		else
		{
			bool flag = this.m_rodComplete && !@bool;
			bool flag2 = this.m_headComplete && !@bool;
			bool flag3 = this.m_pearlComplete && !@bool;
			bool flag4 = flag || flag2 || flag3;
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_emptyStaffObjects, !flag4);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_rodObjects, flag);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_headObjects, flag2);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_pearlObjects, flag3);
			AdventureWingProgressDisplay_LOE.SetObjectsVisibility(this.m_visibleStaffObjects, flag4);
		}
		if (this.m_hangingSignText != null)
		{
			this.m_hangingSignText.Text = (@bool ? GameStrings.Get("GLUE_ADVENTURE_LOE_STAFF_DISAPPEARED") : GameStrings.Get("GLUE_ADVENTURE_LOE_STAFF_RESERVED"));
		}
		if (this.m_completeStaffHitArea != null)
		{
			this.m_completeStaffHitArea.gameObject.SetActive(this.m_finalWingComplete && this.m_rodComplete && this.m_headComplete && this.m_pearlComplete);
		}
		if (this.m_hangingSignHitArea != null)
		{
			this.m_hangingSignHitArea.SetEnabled(!this.m_finalWingComplete && !this.m_rodComplete && !this.m_headComplete && !this.m_pearlComplete, false);
		}
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0001FE88 File Offset: 0x0001E088
	private static void SetObjectsVisibility(List<GameObject> objs, bool show)
	{
		foreach (GameObject gameObject in objs)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(show);
			}
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
	private IEnumerator PlayCompleteAnimationCoroutine(PlayMakerFSM fsm, string eventName, AdventureWingProgressDisplay.OnAnimationComplete onAnimComplete, Option seenOption)
	{
		FsmBool animComplete = fsm.FsmVariables.FindFsmBool("AnimationComplete");
		fsm.SendEvent(eventName);
		this.m_animating = true;
		if (animComplete != null)
		{
			while (!animComplete.Value)
			{
				yield return null;
			}
		}
		this.m_animating = false;
		if (seenOption != Option.INVALID)
		{
			Options.Get().SetBool(seenOption, true);
		}
		if (onAnimComplete != null)
		{
			onAnimComplete();
		}
		yield break;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0001FF0C File Offset: 0x0001E10C
	private void OnHangingSignClick()
	{
		if (this.m_animating)
		{
			return;
		}
		if (this.m_rodComplete || this.m_headComplete || this.m_pearlComplete)
		{
			return;
		}
		if (string.IsNullOrEmpty(this.m_hangingSignQuotePrefab) || string.IsNullOrEmpty(this.m_hangingSignQuoteVOLine))
		{
			return;
		}
		string legacyAssetName = new AssetReference(this.m_hangingSignQuoteVOLine).GetLegacyAssetName();
		NotificationManager.Get().CreateCharacterQuote(this.m_hangingSignQuotePrefab, GameStrings.Get(legacyAssetName), this.m_hangingSignQuoteVOLine, true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0001FF8C File Offset: 0x0001E18C
	private void OnCompleteStaffClick()
	{
		if (this.m_animating)
		{
			return;
		}
		if (!this.m_rodComplete || !this.m_headComplete || !this.m_pearlComplete || !this.m_finalWingComplete)
		{
			return;
		}
		if (string.IsNullOrEmpty(this.m_completeStaffQuotePrefab) || string.IsNullOrEmpty(this.m_completeStaffQuoteVOLine))
		{
			return;
		}
		string legacyAssetName = new AssetReference(this.m_completeStaffQuoteVOLine).GetLegacyAssetName();
		NotificationManager.Get().CreateCharacterQuote(this.m_completeStaffQuotePrefab, GameStrings.Get(legacyAssetName), this.m_completeStaffQuoteVOLine, true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
	}

	// Token: 0x040003CB RID: 971
	public UberText m_hangingSignText;

	// Token: 0x040003CC RID: 972
	public PegUIElement m_hangingSignHitArea;

	// Token: 0x040003CD RID: 973
	public PegUIElement m_completeStaffHitArea;

	// Token: 0x040003CE RID: 974
	public List<GameObject> m_emptyStaffObjects = new List<GameObject>();

	// Token: 0x040003CF RID: 975
	public List<GameObject> m_visibleStaffObjects = new List<GameObject>();

	// Token: 0x040003D0 RID: 976
	public List<GameObject> m_rodObjects = new List<GameObject>();

	// Token: 0x040003D1 RID: 977
	public List<GameObject> m_headObjects = new List<GameObject>();

	// Token: 0x040003D2 RID: 978
	public List<GameObject> m_pearlObjects = new List<GameObject>();

	// Token: 0x040003D3 RID: 979
	[CustomEditField(Sections = "VO")]
	public string m_hangingSignQuotePrefab;

	// Token: 0x040003D4 RID: 980
	[CustomEditField(Sections = "VO")]
	public string m_hangingSignQuoteVOLine;

	// Token: 0x040003D5 RID: 981
	[CustomEditField(Sections = "VO")]
	public string m_completeStaffQuotePrefab;

	// Token: 0x040003D6 RID: 982
	[CustomEditField(Sections = "VO")]
	public string m_completeStaffQuoteVOLine;

	// Token: 0x040003D7 RID: 983
	private const string s_WingDisappearAnimateEventName = "OnWingDisappear";

	// Token: 0x040003D8 RID: 984
	private const string s_WingReappearAnimateEventName = "OnWingReappear";

	// Token: 0x040003D9 RID: 985
	private const string s_CompleteAnimationVarName = "AnimationComplete";

	// Token: 0x040003DA RID: 986
	private bool m_rodComplete;

	// Token: 0x040003DB RID: 987
	private bool m_headComplete;

	// Token: 0x040003DC RID: 988
	private bool m_pearlComplete;

	// Token: 0x040003DD RID: 989
	private bool m_finalWingComplete;

	// Token: 0x040003DE RID: 990
	private bool m_animating;
}
