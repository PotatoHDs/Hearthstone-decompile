using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200003E RID: 62
[CustomEditClass]
public class AdventureDungeonCrawlTreasureOption : AdventureOptionWidget
{
	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600031A RID: 794 RVA: 0x00013A41 File Offset: 0x00011C41
	[CustomEditField(Sections = "Properties (Read-Only)")]
	public Actor CardActor
	{
		get
		{
			return this.m_cardActor;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600031B RID: 795 RVA: 0x00013A49 File Offset: 0x00011C49
	[CustomEditField(Sections = "Properties (Read-Only)")]
	public override bool IsReady
	{
		get
		{
			return base.IsReady && this.m_cardWidget != null && this.m_cardActor != null;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B5EC File Offset: 0x000097EC
	[CustomEditField(Sections = "Properties (Read-Only)")]
	public long CardId
	{
		get
		{
			return this.m_databaseId;
		}
	}

	// Token: 0x0600031D RID: 797 RVA: 0x00013A70 File Offset: 0x00011C70
	protected override void OnWidgetInstanceReady(WidgetInstance widgetInstance)
	{
		base.OnWidgetInstanceReady(widgetInstance);
		if (this.m_widgetInstance == null)
		{
			return;
		}
		this.m_cardWidget = this.m_widgetInstance.GetComponentInChildren<Hearthstone.UI.Card>();
		if (this.m_cardWidget != null)
		{
			this.m_cardWidget.RegisterCardLoadedListener(new Hearthstone.UI.Card.OnCardActorLoadedDelegate(this.OnCardActorLoaded));
		}
		this.SetVisible(false);
	}

	// Token: 0x0600031E RID: 798 RVA: 0x00013AD0 File Offset: 0x00011CD0
	protected override void OnClickableReady(Clickable clickable)
	{
		base.OnClickableReady(clickable);
		this.m_clickable.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Rollout();
		});
	}

	// Token: 0x0600031F RID: 799 RVA: 0x00013AF4 File Offset: 0x00011CF4
	protected override void OnIntroFinished()
	{
		base.OnIntroFinished();
		foreach (Spell obj in this.m_cachedSpells)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.m_cachedSpells.Clear();
	}

	// Token: 0x06000320 RID: 800 RVA: 0x00013B58 File Offset: 0x00011D58
	protected override void OnOutroFinished()
	{
		base.OnOutroFinished();
		foreach (Spell obj in this.m_cachedSpells)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.m_cachedSpells.Clear();
	}

	// Token: 0x06000321 RID: 801 RVA: 0x00013BBC File Offset: 0x00011DBC
	protected override void Rollover()
	{
		base.Rollover();
		if (this.m_cardActor != null)
		{
			this.m_cardActor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
		}
	}

	// Token: 0x06000322 RID: 802 RVA: 0x00013BDE File Offset: 0x00011DDE
	protected override void Rollout()
	{
		base.Rollout();
		if (this.m_cardActor != null)
		{
			this.m_cardActor.SetActorState(ActorStateType.CARD_IDLE);
		}
	}

	// Token: 0x06000323 RID: 803 RVA: 0x00013C00 File Offset: 0x00011E00
	private void OnCardActorLoaded(Actor cardActor)
	{
		this.m_cardActor = cardActor;
		if (this.m_cardActor != null)
		{
			this.SetVisible(this.m_isVisible);
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x00013C24 File Offset: 0x00011E24
	private void PlaySpells(List<AdventureDungeonCrawlTreasureOption.SpellDisplayData> spellTypes)
	{
		if (spellTypes == null || spellTypes.Count <= 0)
		{
			return;
		}
		if (this.m_cardWidget == null)
		{
			Debug.LogError("AdventureTreasureOption.PlaySpells - m_cardWidget was null!");
			return;
		}
		if (this.m_cardActor == null)
		{
			Debug.LogError("AdventureTreasureOption.PlaySpells - m_cardActor was null!");
			return;
		}
		foreach (AdventureDungeonCrawlTreasureOption.SpellDisplayData spellDisplayData in (from r in spellTypes
		where r.m_SpellType > SpellType.NONE
		select r).ToList<AdventureDungeonCrawlTreasureOption.SpellDisplayData>())
		{
			Spell spell = this.m_cardActor.GetSpell(spellDisplayData.m_SpellType);
			if (spell == null)
			{
				Debug.LogErrorFormat("AdventureDungeonCrawlTreasureOption.PlaySpells - {0} spell type was null!", new object[]
				{
					spellDisplayData.m_SpellType
				});
			}
			else
			{
				spell.SetLocalPosition(spellDisplayData.m_RelativeLocation);
				spell.ActivateState(SpellStateType.BIRTH);
				this.m_cachedSpells.Add(spell);
			}
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00013D30 File Offset: 0x00011F30
	public void Init(long cardDbId, bool locked, string lockedText, bool upgraded, bool completed, bool newlyUnlocked, AdventureOptionWidget.OptionAcknowledgedCallback acknowledgedCallback)
	{
		this.m_databaseId = cardDbId;
		string name = null;
		base.InitWidget(name, locked, lockedText, upgraded, completed, newlyUnlocked, acknowledgedCallback);
		this.Rollout();
	}

	// Token: 0x06000326 RID: 806 RVA: 0x00013D60 File Offset: 0x00011F60
	public override void Select()
	{
		base.Select();
		AdventureDungeonCrawlTreasureOption.TreasureSelectedOptionCallback treasureSelectedOptionCallback = this.m_selectedCallback as AdventureDungeonCrawlTreasureOption.TreasureSelectedOptionCallback;
		if (treasureSelectedOptionCallback == null)
		{
			Log.Adventures.PrintError("Attempting to execute a callback for the AdventureDungeonCrawlTreasureOption, but no callback was provided!", Array.Empty<object>());
			return;
		}
		treasureSelectedOptionCallback(this.m_databaseId);
	}

	// Token: 0x06000327 RID: 807 RVA: 0x00013DA3 File Offset: 0x00011FA3
	public override void PlayIntro()
	{
		base.PlayIntro();
		this.PlaySpells(this.MoteInSpells);
	}

	// Token: 0x06000328 RID: 808 RVA: 0x00013DB7 File Offset: 0x00011FB7
	public override void PlayOutro()
	{
		base.PlayOutro();
		this.Rollout();
		this.PlaySpells(this.m_dataModel.IsSelectedOption ? this.MoteOutSpellsForSelected : this.MoteOutSpells);
	}

	// Token: 0x04000241 RID: 577
	[CustomEditField(Sections = "Card Spell Types")]
	public List<AdventureDungeonCrawlTreasureOption.SpellDisplayData> MoteInSpells = new List<AdventureDungeonCrawlTreasureOption.SpellDisplayData>
	{
		new AdventureDungeonCrawlTreasureOption.SpellDisplayData(SpellType.SUMMON_IN_FORGE, Vector3.zero),
		new AdventureDungeonCrawlTreasureOption.SpellDisplayData(SpellType.BURST_RARE, new Vector3(0.2f, 0.2f, 0.2f))
	};

	// Token: 0x04000242 RID: 578
	[CustomEditField(Sections = "Card Spell Types")]
	public List<AdventureDungeonCrawlTreasureOption.SpellDisplayData> MoteOutSpells = new List<AdventureDungeonCrawlTreasureOption.SpellDisplayData>
	{
		new AdventureDungeonCrawlTreasureOption.SpellDisplayData(SpellType.BURN, new Vector3(0.2f, 0.2f, 0f))
	};

	// Token: 0x04000243 RID: 579
	[CustomEditField(Sections = "Card Spell Types")]
	public List<AdventureDungeonCrawlTreasureOption.SpellDisplayData> MoteOutSpellsForSelected = new List<AdventureDungeonCrawlTreasureOption.SpellDisplayData>
	{
		new AdventureDungeonCrawlTreasureOption.SpellDisplayData(SpellType.SUMMON_OUT_FORGE, new Vector3(0.2f, 0.2f, 0.2f))
	};

	// Token: 0x04000244 RID: 580
	private Hearthstone.UI.Card m_cardWidget;

	// Token: 0x04000245 RID: 581
	private Actor m_cardActor;

	// Token: 0x04000246 RID: 582
	private List<Spell> m_cachedSpells = new List<Spell>();

	// Token: 0x020012E9 RID: 4841
	// (Invoke) Token: 0x0600D5D3 RID: 54739
	public delegate void TreasureSelectedOptionCallback(long cardDbId);

	// Token: 0x020012EA RID: 4842
	// (Invoke) Token: 0x0600D5D7 RID: 54743
	public delegate void TreasureHoverOptionCallback(long cardDbId);

	// Token: 0x020012EB RID: 4843
	[Serializable]
	public class SpellDisplayData
	{
		// Token: 0x0600D5DA RID: 54746 RVA: 0x003E8986 File Offset: 0x003E6B86
		public SpellDisplayData(SpellType spellType, Vector3 position)
		{
			this.m_SpellType = spellType;
			this.m_RelativeLocation = position;
		}

		// Token: 0x0400A517 RID: 42263
		public SpellType m_SpellType;

		// Token: 0x0400A518 RID: 42264
		public Vector3 m_RelativeLocation;
	}
}
