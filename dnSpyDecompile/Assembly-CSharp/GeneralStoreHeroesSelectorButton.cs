using System;

// Token: 0x020006FC RID: 1788
[CustomEditClass]
public class GeneralStoreHeroesSelectorButton : PegUIElement
{
	// Token: 0x060063CF RID: 25551 RVA: 0x00208401 File Offset: 0x00206601
	protected override void Awake()
	{
		base.Awake();
		if (UniversalInputManager.UsePhoneUI)
		{
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.WIDTH);
			return;
		}
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
	}

	// Token: 0x060063D0 RID: 25552 RVA: 0x0020843C File Offset: 0x0020663C
	protected override void OnDestroy()
	{
		DefLoader.DisposableFullDef currentDef = this.m_currentDef;
		if (currentDef != null)
		{
			currentDef.Dispose();
		}
		this.m_currentDef = null;
		base.OnDestroy();
	}

	// Token: 0x060063D1 RID: 25553 RVA: 0x0020845C File Offset: 0x0020665C
	public int GetHeroDbId()
	{
		return this.m_cardHero.ID;
	}

	// Token: 0x060063D2 RID: 25554 RVA: 0x00208469 File Offset: 0x00206669
	public string GetHeroCardMiniGuid()
	{
		return GameUtils.TranslateDbIdToCardId(this.m_cardHero.CardId, false);
	}

	// Token: 0x060063D3 RID: 25555 RVA: 0x0020847C File Offset: 0x0020667C
	public int GetHeroCardDbId()
	{
		return GameUtils.TranslateCardIdToDbId(this.GetHeroCardMiniGuid(), false);
	}

	// Token: 0x060063D4 RID: 25556 RVA: 0x0020848A File Offset: 0x0020668A
	public void SetCardHeroDbfRecord(CardHeroDbfRecord cardHero)
	{
		this.m_cardHero = cardHero;
	}

	// Token: 0x060063D5 RID: 25557 RVA: 0x00208493 File Offset: 0x00206693
	public CardHeroDbfRecord GetCardHeroDbfRecord()
	{
		return this.m_cardHero;
	}

	// Token: 0x060063D6 RID: 25558 RVA: 0x0020849B File Offset: 0x0020669B
	public void SetSortOrder(int sortOrder)
	{
		this.m_sortOrder = sortOrder;
	}

	// Token: 0x060063D7 RID: 25559 RVA: 0x002084A4 File Offset: 0x002066A4
	public int GetSortOrder()
	{
		return this.m_sortOrder;
	}

	// Token: 0x060063D8 RID: 25560 RVA: 0x002084AC File Offset: 0x002066AC
	public void SetPurchased(bool purchased)
	{
		this.m_purchased = purchased;
	}

	// Token: 0x060063D9 RID: 25561 RVA: 0x002084B5 File Offset: 0x002066B5
	public bool GetPurchased()
	{
		return this.m_purchased;
	}

	// Token: 0x060063DA RID: 25562 RVA: 0x002084BD File Offset: 0x002066BD
	public void UpdatePortrait(GeneralStoreHeroesSelectorButton rhs)
	{
		this.UpdatePortrait(rhs.m_currentDef);
	}

	// Token: 0x060063DB RID: 25563 RVA: 0x002084CC File Offset: 0x002066CC
	public void UpdatePortrait(DefLoader.DisposableFullDef def)
	{
		this.m_heroActor.SetFullDef(def);
		this.m_heroActor.UpdateAllComponents();
		this.m_heroActor.SetUnlit();
		DefLoader.DisposableFullDef currentDef = this.m_currentDef;
		if (currentDef != null)
		{
			currentDef.Dispose();
		}
		this.m_currentDef = ((def != null) ? def.Share() : null);
	}

	// Token: 0x060063DC RID: 25564 RVA: 0x0020851E File Offset: 0x0020671E
	public void UpdateName(GeneralStoreHeroesSelectorButton rhs)
	{
		this.UpdateName(rhs.m_heroName.Text);
	}

	// Token: 0x060063DD RID: 25565 RVA: 0x00208531 File Offset: 0x00206731
	public void UpdateName(string name)
	{
		if (this.m_heroName != null)
		{
			this.m_heroName.Text = name;
		}
	}

	// Token: 0x060063DE RID: 25566 RVA: 0x00208550 File Offset: 0x00206750
	public void Select()
	{
		if (this.m_selected)
		{
			return;
		}
		this.m_selected = true;
		this.m_highlight.ChangeState((base.GetInteractionState() == PegUIElement.InteractionState.Up) ? ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE : ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		if (!string.IsNullOrEmpty(this.m_selectSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_selectSound);
		}
	}

	// Token: 0x060063DF RID: 25567 RVA: 0x002085AC File Offset: 0x002067AC
	public void Unselect()
	{
		if (!this.m_selected)
		{
			return;
		}
		this.m_selected = false;
		this.m_highlight.ChangeState(ActorStateType.NONE);
		if (!string.IsNullOrEmpty(this.m_unselectSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_unselectSound);
		}
	}

	// Token: 0x060063E0 RID: 25568 RVA: 0x000052EC File Offset: 0x000034EC
	public bool IsAvailable()
	{
		return true;
	}

	// Token: 0x060063E1 RID: 25569 RVA: 0x002085F8 File Offset: 0x002067F8
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		base.OnOver(oldState);
		if (this.m_highlight != null && this.IsAvailable())
		{
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
			if (!string.IsNullOrEmpty(this.m_mouseOverSound))
			{
				SoundManager.Get().LoadAndPlay(this.m_mouseOverSound);
			}
		}
	}

	// Token: 0x060063E2 RID: 25570 RVA: 0x00208652 File Offset: 0x00206852
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		base.OnOut(oldState);
		if (this.m_highlight != null && this.IsAvailable())
		{
			this.m_highlight.ChangeState(this.m_selected ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.NONE);
		}
	}

	// Token: 0x060063E3 RID: 25571 RVA: 0x0020868A File Offset: 0x0020688A
	protected override void OnRelease()
	{
		base.OnRelease();
		if (this.m_highlight != null && this.IsAvailable())
		{
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
		}
	}

	// Token: 0x060063E4 RID: 25572 RVA: 0x002086B6 File Offset: 0x002068B6
	protected override void OnPress()
	{
		base.OnPress();
		if (this.m_highlight != null && this.IsAvailable())
		{
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}

	// Token: 0x040052D3 RID: 21203
	public Actor m_heroActor;

	// Token: 0x040052D4 RID: 21204
	public UberText m_heroName;

	// Token: 0x040052D5 RID: 21205
	public HighlightState m_highlight;

	// Token: 0x040052D6 RID: 21206
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_selectSound;

	// Token: 0x040052D7 RID: 21207
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_unselectSound;

	// Token: 0x040052D8 RID: 21208
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_mouseOverSound;

	// Token: 0x040052D9 RID: 21209
	private string m_heroId;

	// Token: 0x040052DA RID: 21210
	private int m_sortOrder;

	// Token: 0x040052DB RID: 21211
	private bool m_selected;

	// Token: 0x040052DC RID: 21212
	private bool m_purchased;

	// Token: 0x040052DD RID: 21213
	private DefLoader.DisposableFullDef m_currentDef;

	// Token: 0x040052DE RID: 21214
	private CardHeroDbfRecord m_cardHero;
}
