using System;
using UnityEngine;

// Token: 0x02000307 RID: 775
public class EnemyEmoteHandler : MonoBehaviour
{
	// Token: 0x060029B6 RID: 10678 RVA: 0x000D3FCC File Offset: 0x000D21CC
	private void Awake()
	{
		EnemyEmoteHandler.s_instance = this;
		base.GetComponent<Collider>().enabled = false;
		this.m_squelchEmoteStartingScale = this.m_SquelchEmote.transform.localScale;
		this.m_squelched = new Map<int, bool>(8);
		for (int i = 0; i < 8; i++)
		{
			this.m_squelched.Add(i + 1, false);
		}
		this.m_SquelchEmoteText.gameObject.SetActive(false);
		this.m_SquelchEmoteBackplate.enabled = false;
		this.m_SquelchEmote.transform.localScale = Vector3.zero;
	}

	// Token: 0x060029B7 RID: 10679 RVA: 0x000D405A File Offset: 0x000D225A
	private void OnDestroy()
	{
		EnemyEmoteHandler.s_instance = null;
	}

	// Token: 0x060029B8 RID: 10680 RVA: 0x000D4062 File Offset: 0x000D2262
	public static EnemyEmoteHandler Get()
	{
		return EnemyEmoteHandler.s_instance;
	}

	// Token: 0x060029B9 RID: 10681 RVA: 0x000D4069 File Offset: 0x000D2269
	public bool AreEmotesActive()
	{
		return this.m_emotesShown;
	}

	// Token: 0x060029BA RID: 10682 RVA: 0x000D4071 File Offset: 0x000D2271
	public bool IsSquelched(int playerId)
	{
		return this.m_squelched.ContainsKey(playerId) && this.m_squelched[playerId];
	}

	// Token: 0x060029BB RID: 10683 RVA: 0x000D4090 File Offset: 0x000D2290
	private bool AnySquelched()
	{
		using (Map<int, bool>.ValueCollection.Enumerator enumerator = this.m_squelched.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060029BC RID: 10684 RVA: 0x000D40EC File Offset: 0x000D22EC
	public void ShowEmotes()
	{
		if (this.m_emotesShown)
		{
			return;
		}
		this.m_emotesShown = true;
		base.GetComponent<Collider>().enabled = true;
		this.m_shownAtFrame = Time.frameCount;
		if (this.AnySquelched())
		{
			this.m_SquelchEmoteText.Text = GameStrings.Get(this.m_UnsquelchStringTag);
		}
		else
		{
			this.m_SquelchEmoteText.Text = GameStrings.Get(this.m_SquelchStringTag);
		}
		this.m_SquelchEmoteBackplate.enabled = true;
		this.m_SquelchEmoteText.gameObject.SetActive(true);
		this.m_SquelchEmote.GetComponent<Collider>().enabled = true;
		iTween.Stop(this.m_SquelchEmote);
		iTween.ScaleTo(this.m_SquelchEmote, iTween.Hash(new object[]
		{
			"scale",
			this.m_squelchEmoteStartingScale,
			"time",
			0.5f,
			"ignoretimescale",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
	}

	// Token: 0x060029BD RID: 10685 RVA: 0x000D41F4 File Offset: 0x000D23F4
	public void HideEmotes()
	{
		if (!this.m_emotesShown)
		{
			return;
		}
		this.m_emotesShown = false;
		base.GetComponent<Collider>().enabled = false;
		this.m_SquelchEmote.GetComponent<Collider>().enabled = false;
		iTween.Stop(this.m_SquelchEmote);
		iTween.ScaleTo(this.m_SquelchEmote, iTween.Hash(new object[]
		{
			"scale",
			Vector3.zero,
			"time",
			0.1f,
			"ignoretimescale",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"FinishDisable"
		}));
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x000D42C4 File Offset: 0x000D24C4
	public void HandleInput()
	{
		RaycastHit raycastHit;
		if (!this.HitTestEmotes(out raycastHit))
		{
			this.HideEmotes();
			return;
		}
		if (raycastHit.transform.gameObject != this.m_SquelchEmote)
		{
			if (this.m_squelchMousedOver)
			{
				this.MouseOutSquelch();
				this.m_squelchMousedOver = false;
			}
		}
		else if (!this.m_squelchMousedOver)
		{
			this.m_squelchMousedOver = true;
			this.MouseOverSquelch();
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			if (this.m_squelchMousedOver)
			{
				this.DoSquelchClick();
				return;
			}
			if (UniversalInputManager.Get().IsTouchMode() && Time.frameCount != this.m_shownAtFrame)
			{
				this.HideEmotes();
			}
		}
	}

	// Token: 0x060029BF RID: 10687 RVA: 0x000D4364 File Offset: 0x000D2564
	public bool IsMouseOverEmoteOption()
	{
		RaycastHit raycastHit;
		return UniversalInputManager.Get().GetInputHitInfo(GameLayer.Default.LayerBit(), out raycastHit) && raycastHit.transform.gameObject == this.m_SquelchEmote;
	}

	// Token: 0x060029C0 RID: 10688 RVA: 0x000D43A8 File Offset: 0x000D25A8
	private void MouseOverSquelch()
	{
		iTween.ScaleTo(this.m_SquelchEmote, iTween.Hash(new object[]
		{
			"scale",
			this.m_squelchEmoteStartingScale * 1.1f,
			"time",
			0.2f,
			"ignoretimescale",
			true
		}));
	}

	// Token: 0x060029C1 RID: 10689 RVA: 0x000D4414 File Offset: 0x000D2614
	private void MouseOutSquelch()
	{
		iTween.ScaleTo(this.m_SquelchEmote, iTween.Hash(new object[]
		{
			"scale",
			this.m_squelchEmoteStartingScale,
			"time",
			0.2f,
			"ignoretimescale",
			true
		}));
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x000D4473 File Offset: 0x000D2673
	private void DoSquelchClick()
	{
		this.m_squelched[GameState.Get().GetOpposingPlayerId()] = !this.m_squelched[GameState.Get().GetOpposingPlayerId()];
		this.HideEmotes();
	}

	// Token: 0x060029C3 RID: 10691 RVA: 0x000D44A8 File Offset: 0x000D26A8
	private bool HitTestEmotes(out RaycastHit hitInfo)
	{
		return UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast.LayerBit(), out hitInfo) && (this.IsMousedOverHero(hitInfo) || this.IsMousedOverSelf(hitInfo) || this.IsMousedOverEmote(hitInfo));
	}

	// Token: 0x060029C4 RID: 10692 RVA: 0x000D4500 File Offset: 0x000D2700
	private bool IsMousedOverHero(RaycastHit cardHitInfo)
	{
		Actor actor = SceneUtils.FindComponentInParents<Actor>(cardHitInfo.transform);
		if (actor == null)
		{
			return false;
		}
		Card card = actor.GetCard();
		return !(card == null) && card.GetEntity().IsHero();
	}

	// Token: 0x060029C5 RID: 10693 RVA: 0x000D2A6C File Offset: 0x000D0C6C
	private bool IsMousedOverSelf(RaycastHit cardHitInfo)
	{
		return base.GetComponent<Collider>() == cardHitInfo.collider;
	}

	// Token: 0x060029C6 RID: 10694 RVA: 0x000D4547 File Offset: 0x000D2747
	private bool IsMousedOverEmote(RaycastHit cardHitInfo)
	{
		return cardHitInfo.transform == this.m_SquelchEmote.transform;
	}

	// Token: 0x040017A6 RID: 6054
	public GameObject m_SquelchEmote;

	// Token: 0x040017A7 RID: 6055
	public MeshRenderer m_SquelchEmoteBackplate;

	// Token: 0x040017A8 RID: 6056
	public UberText m_SquelchEmoteText;

	// Token: 0x040017A9 RID: 6057
	public string m_SquelchStringTag;

	// Token: 0x040017AA RID: 6058
	public string m_UnsquelchStringTag;

	// Token: 0x040017AB RID: 6059
	private static EnemyEmoteHandler s_instance;

	// Token: 0x040017AC RID: 6060
	private Vector3 m_squelchEmoteStartingScale;

	// Token: 0x040017AD RID: 6061
	private bool m_emotesShown;

	// Token: 0x040017AE RID: 6062
	private int m_shownAtFrame;

	// Token: 0x040017AF RID: 6063
	private bool m_squelchMousedOver;

	// Token: 0x040017B0 RID: 6064
	private Map<int, bool> m_squelched;

	// Token: 0x040017B1 RID: 6065
	private const int PLAYERS_IN_BATTLEGROUNDS = 8;
}
