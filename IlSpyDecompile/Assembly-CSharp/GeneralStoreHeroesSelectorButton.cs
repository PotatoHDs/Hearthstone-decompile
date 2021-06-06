[CustomEditClass]
public class GeneralStoreHeroesSelectorButton : PegUIElement
{
	public Actor m_heroActor;

	public UberText m_heroName;

	public HighlightState m_highlight;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_selectSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_unselectSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_mouseOverSound;

	private string m_heroId;

	private int m_sortOrder;

	private bool m_selected;

	private bool m_purchased;

	private DefLoader.DisposableFullDef m_currentDef;

	private CardHeroDbfRecord m_cardHero;

	protected override void Awake()
	{
		base.Awake();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, destroyOnSceneLoad: false, CanvasScaleMode.WIDTH);
		}
		else
		{
			OverlayUI.Get().AddGameObject(base.gameObject);
		}
	}

	protected override void OnDestroy()
	{
		m_currentDef?.Dispose();
		m_currentDef = null;
		base.OnDestroy();
	}

	public int GetHeroDbId()
	{
		return m_cardHero.ID;
	}

	public string GetHeroCardMiniGuid()
	{
		return GameUtils.TranslateDbIdToCardId(m_cardHero.CardId);
	}

	public int GetHeroCardDbId()
	{
		return GameUtils.TranslateCardIdToDbId(GetHeroCardMiniGuid());
	}

	public void SetCardHeroDbfRecord(CardHeroDbfRecord cardHero)
	{
		m_cardHero = cardHero;
	}

	public CardHeroDbfRecord GetCardHeroDbfRecord()
	{
		return m_cardHero;
	}

	public void SetSortOrder(int sortOrder)
	{
		m_sortOrder = sortOrder;
	}

	public int GetSortOrder()
	{
		return m_sortOrder;
	}

	public void SetPurchased(bool purchased)
	{
		m_purchased = purchased;
	}

	public bool GetPurchased()
	{
		return m_purchased;
	}

	public void UpdatePortrait(GeneralStoreHeroesSelectorButton rhs)
	{
		UpdatePortrait(rhs.m_currentDef);
	}

	public void UpdatePortrait(DefLoader.DisposableFullDef def)
	{
		m_heroActor.SetFullDef(def);
		m_heroActor.UpdateAllComponents();
		m_heroActor.SetUnlit();
		m_currentDef?.Dispose();
		m_currentDef = def?.Share();
	}

	public void UpdateName(GeneralStoreHeroesSelectorButton rhs)
	{
		UpdateName(rhs.m_heroName.Text);
	}

	public void UpdateName(string name)
	{
		if (m_heroName != null)
		{
			m_heroName.Text = name;
		}
	}

	public void Select()
	{
		if (!m_selected)
		{
			m_selected = true;
			m_highlight.ChangeState((GetInteractionState() == InteractionState.Up) ? ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE : ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			if (!string.IsNullOrEmpty(m_selectSound))
			{
				SoundManager.Get().LoadAndPlay(m_selectSound);
			}
		}
	}

	public void Unselect()
	{
		if (m_selected)
		{
			m_selected = false;
			m_highlight.ChangeState(ActorStateType.NONE);
			if (!string.IsNullOrEmpty(m_unselectSound))
			{
				SoundManager.Get().LoadAndPlay(m_unselectSound);
			}
		}
	}

	public bool IsAvailable()
	{
		return true;
	}

	protected override void OnOver(InteractionState oldState)
	{
		base.OnOver(oldState);
		if (m_highlight != null && IsAvailable())
		{
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
			if (!string.IsNullOrEmpty(m_mouseOverSound))
			{
				SoundManager.Get().LoadAndPlay(m_mouseOverSound);
			}
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		base.OnOut(oldState);
		if (m_highlight != null && IsAvailable())
		{
			m_highlight.ChangeState(m_selected ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.NONE);
		}
	}

	protected override void OnRelease()
	{
		base.OnRelease();
		if (m_highlight != null && IsAvailable())
		{
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_SECONDARY_ACTIVE);
		}
	}

	protected override void OnPress()
	{
		base.OnPress();
		if (m_highlight != null && IsAvailable())
		{
			m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}
}
