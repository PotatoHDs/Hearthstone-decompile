using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public class AdventureDungeonCrawlTreasureOption : AdventureOptionWidget
{
	public delegate void TreasureSelectedOptionCallback(long cardDbId);

	public delegate void TreasureHoverOptionCallback(long cardDbId);

	[Serializable]
	public class SpellDisplayData
	{
		public SpellType m_SpellType;

		public Vector3 m_RelativeLocation;

		public SpellDisplayData(SpellType spellType, Vector3 position)
		{
			m_SpellType = spellType;
			m_RelativeLocation = position;
		}
	}

	[CustomEditField(Sections = "Card Spell Types")]
	public List<SpellDisplayData> MoteInSpells = new List<SpellDisplayData>
	{
		new SpellDisplayData(SpellType.SUMMON_IN_FORGE, Vector3.zero),
		new SpellDisplayData(SpellType.BURST_RARE, new Vector3(0.2f, 0.2f, 0.2f))
	};

	[CustomEditField(Sections = "Card Spell Types")]
	public List<SpellDisplayData> MoteOutSpells = new List<SpellDisplayData>
	{
		new SpellDisplayData(SpellType.BURN, new Vector3(0.2f, 0.2f, 0f))
	};

	[CustomEditField(Sections = "Card Spell Types")]
	public List<SpellDisplayData> MoteOutSpellsForSelected = new List<SpellDisplayData>
	{
		new SpellDisplayData(SpellType.SUMMON_OUT_FORGE, new Vector3(0.2f, 0.2f, 0.2f))
	};

	private Hearthstone.UI.Card m_cardWidget;

	private Actor m_cardActor;

	private List<Spell> m_cachedSpells = new List<Spell>();

	[CustomEditField(Sections = "Properties (Read-Only)")]
	public Actor CardActor => m_cardActor;

	[CustomEditField(Sections = "Properties (Read-Only)")]
	public override bool IsReady
	{
		get
		{
			if (base.IsReady && m_cardWidget != null)
			{
				return m_cardActor != null;
			}
			return false;
		}
	}

	[CustomEditField(Sections = "Properties (Read-Only)")]
	public long CardId => m_databaseId;

	protected override void OnWidgetInstanceReady(WidgetInstance widgetInstance)
	{
		base.OnWidgetInstanceReady(widgetInstance);
		if (!(m_widgetInstance == null))
		{
			m_cardWidget = m_widgetInstance.GetComponentInChildren<Hearthstone.UI.Card>();
			if (m_cardWidget != null)
			{
				m_cardWidget.RegisterCardLoadedListener(OnCardActorLoaded);
			}
			SetVisible(isVisible: false);
		}
	}

	protected override void OnClickableReady(Clickable clickable)
	{
		base.OnClickableReady(clickable);
		m_clickable.AddEventListener(UIEventType.RELEASE, delegate
		{
			Rollout();
		});
	}

	protected override void OnIntroFinished()
	{
		base.OnIntroFinished();
		foreach (Spell cachedSpell in m_cachedSpells)
		{
			UnityEngine.Object.Destroy(cachedSpell);
		}
		m_cachedSpells.Clear();
	}

	protected override void OnOutroFinished()
	{
		base.OnOutroFinished();
		foreach (Spell cachedSpell in m_cachedSpells)
		{
			UnityEngine.Object.Destroy(cachedSpell);
		}
		m_cachedSpells.Clear();
	}

	protected override void Rollover()
	{
		base.Rollover();
		if (m_cardActor != null)
		{
			m_cardActor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		}
	}

	protected override void Rollout()
	{
		base.Rollout();
		if (m_cardActor != null)
		{
			m_cardActor.SetActorState(ActorStateType.CARD_IDLE);
		}
	}

	private void OnCardActorLoaded(Actor cardActor)
	{
		m_cardActor = cardActor;
		if (m_cardActor != null)
		{
			SetVisible(m_isVisible);
		}
	}

	private void PlaySpells(List<SpellDisplayData> spellTypes)
	{
		if (spellTypes == null || spellTypes.Count <= 0)
		{
			return;
		}
		if (m_cardWidget == null)
		{
			Debug.LogError("AdventureTreasureOption.PlaySpells - m_cardWidget was null!");
			return;
		}
		if (m_cardActor == null)
		{
			Debug.LogError("AdventureTreasureOption.PlaySpells - m_cardActor was null!");
			return;
		}
		foreach (SpellDisplayData item in spellTypes.Where((SpellDisplayData r) => r.m_SpellType != SpellType.NONE).ToList())
		{
			Spell spell = m_cardActor.GetSpell(item.m_SpellType);
			if (spell == null)
			{
				Debug.LogErrorFormat("AdventureDungeonCrawlTreasureOption.PlaySpells - {0} spell type was null!", item.m_SpellType);
			}
			else
			{
				spell.SetLocalPosition(item.m_RelativeLocation);
				spell.ActivateState(SpellStateType.BIRTH);
				m_cachedSpells.Add(spell);
			}
		}
	}

	public void Init(long cardDbId, bool locked, string lockedText, bool upgraded, bool completed, bool newlyUnlocked, OptionAcknowledgedCallback acknowledgedCallback)
	{
		m_databaseId = cardDbId;
		string text = null;
		InitWidget(text, locked, lockedText, upgraded, completed, newlyUnlocked, acknowledgedCallback);
		Rollout();
	}

	public override void Select()
	{
		base.Select();
		TreasureSelectedOptionCallback treasureSelectedOptionCallback = m_selectedCallback as TreasureSelectedOptionCallback;
		if (treasureSelectedOptionCallback == null)
		{
			Log.Adventures.PrintError("Attempting to execute a callback for the AdventureDungeonCrawlTreasureOption, but no callback was provided!");
		}
		else
		{
			treasureSelectedOptionCallback(m_databaseId);
		}
	}

	public override void PlayIntro()
	{
		base.PlayIntro();
		PlaySpells(MoteInSpells);
	}

	public override void PlayOutro()
	{
		base.PlayOutro();
		Rollout();
		PlaySpells(m_dataModel.IsSelectedOption ? MoteOutSpellsForSelected : MoteOutSpells);
	}
}
