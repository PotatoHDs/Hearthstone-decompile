using System;
using UnityEngine;

// Token: 0x0200061B RID: 1563
public class UnopenedPack : PegUIElement
{
	// Token: 0x06005796 RID: 22422 RVA: 0x001CAB31 File Offset: 0x001C8D31
	protected override void Awake()
	{
		base.Awake();
		this.UpdateState();
	}

	// Token: 0x06005797 RID: 22423 RVA: 0x001CAB3F File Offset: 0x001C8D3F
	public NetCache.BoosterStack GetBoosterStack()
	{
		return this.m_boosterStack;
	}

	// Token: 0x06005798 RID: 22424 RVA: 0x001CAB47 File Offset: 0x001C8D47
	public void AddBoosters(int numNewBoosters)
	{
		this.m_boosterStack.Count += numNewBoosters;
		this.UpdateState();
	}

	// Token: 0x06005799 RID: 22425 RVA: 0x001CAB62 File Offset: 0x001C8D62
	public void AddBooster()
	{
		this.AddBoosters(1);
	}

	// Token: 0x0600579A RID: 22426 RVA: 0x001CAB6B File Offset: 0x001C8D6B
	public void SetBoosterStack(NetCache.BoosterStack boosterStack)
	{
		this.m_boosterStack = boosterStack;
		if (GameDbf.Booster.GetRecord(boosterStack.Id) != null)
		{
			this.m_isRotatedPack = GameUtils.IsBoosterRotated((BoosterDbId)boosterStack.Id, DateTime.UtcNow);
		}
		this.UpdateState();
	}

	// Token: 0x0600579B RID: 22427 RVA: 0x001CABA4 File Offset: 0x001C8DA4
	public void RemoveBooster()
	{
		NetCache.BoosterStack boosterStack = this.m_boosterStack;
		int count = boosterStack.Count;
		boosterStack.Count = count - 1;
		if (this.m_boosterStack.Count < 0)
		{
			Debug.LogWarning("UnopenedPack.RemoveBooster(): Removed a booster pack from a stack with no boosters");
			this.m_boosterStack.Count = 0;
		}
		this.UpdateState();
	}

	// Token: 0x0600579C RID: 22428 RVA: 0x001CABF0 File Offset: 0x001C8DF0
	public UnopenedPack AcquireDraggedPack()
	{
		if (this.m_draggedPack != null)
		{
			return this.m_draggedPack;
		}
		Vector3 position = base.transform.position;
		position.y -= 5000f;
		this.m_draggedPack = UnityEngine.Object.Instantiate<UnopenedPack>(this, position, base.transform.rotation);
		TransformUtil.CopyWorldScale(this.m_draggedPack, this);
		this.m_draggedPack.transform.parent = base.transform.parent;
		UIBScrollableItem component = this.m_draggedPack.GetComponent<UIBScrollableItem>();
		if (component != null)
		{
			component.m_active = UIBScrollableItem.ActiveState.Inactive;
		}
		this.m_draggedPack.m_creatorPack = this;
		this.m_draggedPack.gameObject.AddComponent<DragRotator>().SetInfo(this.m_DragRotatorInfo);
		this.m_draggedPack.m_DragStartEvent.Activate();
		return this.m_draggedPack;
	}

	// Token: 0x0600579D RID: 22429 RVA: 0x001CACC8 File Offset: 0x001C8EC8
	public void ReleaseDraggedPack()
	{
		if (this.m_draggedPack == null)
		{
			return;
		}
		UnopenedPack draggedPack = this.m_draggedPack;
		this.m_draggedPack = null;
		draggedPack.m_DragStopEvent.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnDragStopSpellStateFinished), draggedPack);
		draggedPack.m_DragStopEvent.Activate();
		this.UpdateState();
	}

	// Token: 0x0600579E RID: 22430 RVA: 0x001CAD1B File Offset: 0x001C8F1B
	public UnopenedPack GetDraggedPack()
	{
		return this.m_draggedPack;
	}

	// Token: 0x0600579F RID: 22431 RVA: 0x001CAD23 File Offset: 0x001C8F23
	public UnopenedPack GetCreatorPack()
	{
		return this.m_creatorPack;
	}

	// Token: 0x060057A0 RID: 22432 RVA: 0x001CAD2B File Offset: 0x001C8F2B
	public void PlayAlert()
	{
		this.m_AlertEvent.ActivateState(SpellStateType.BIRTH);
	}

	// Token: 0x060057A1 RID: 22433 RVA: 0x001CAD39 File Offset: 0x001C8F39
	public void StopAlert()
	{
		this.m_AlertEvent.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x060057A2 RID: 22434 RVA: 0x001CAD48 File Offset: 0x001C8F48
	public bool CanOpenPack()
	{
		NetCache.BoosterStack boosterStack = this.GetBoosterStack();
		if (boosterStack == null)
		{
			return false;
		}
		BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterStack.Id);
		if (record == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(record.OpenPackEvent))
		{
			return false;
		}
		SpecialEventType eventType = SpecialEventManager.GetEventType(record.OpenPackEvent);
		return eventType != SpecialEventType.UNKNOWN && (!this.m_isRotatedPack || RankMgr.Get().WildCardsAllowedInCurrentLeague()) && (eventType == SpecialEventType.IGNORE || SpecialEventManager.Get().IsEventActive(eventType, false) || (GameUtils.AtPrereleaseEvent() && SpecialEventManager.Get().IsEventActive(record.PrereleaseOpenPackEvent, false)));
	}

	// Token: 0x060057A3 RID: 22435 RVA: 0x001CADE0 File Offset: 0x001C8FE0
	public void UpdateState()
	{
		bool flag = this.CanOpenPack();
		if (this.m_LockRibbon != null)
		{
			this.m_LockRibbon.SetActive(!flag);
		}
		bool flag2 = this.m_boosterStack.Count == 0;
		bool flag3 = this.m_boosterStack.Count > 1;
		this.m_SingleStack.m_RootObject.SetActive((!flag3 || !flag) && !flag2);
		this.m_MultipleStack.m_RootObject.SetActive(flag3 && !flag2 && flag);
		this.m_AmountBanner.SetActive(flag3);
		this.m_AmountText.enabled = flag3;
		if (flag3)
		{
			this.m_AmountText.Text = this.m_boosterStack.Count.ToString();
		}
		if (this.m_isRotatedPack && RankMgr.Get().GetLocalPlayerStandardLeagueConfig().LockWildBoosters && this.m_LockedRibbonText != null)
		{
			this.m_LockedRibbonText.Text = GameStrings.Get("GLUE_NEW_PLAYER_AVAILABLE_AT_LEAGUE_PROMO");
		}
	}

	// Token: 0x060057A4 RID: 22436 RVA: 0x001CAEDD File Offset: 0x001C90DD
	private void OnDragStopSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(((UnopenedPack)userData).gameObject);
	}

	// Token: 0x04004B30 RID: 19248
	public UnopenedPackStack m_SingleStack;

	// Token: 0x04004B31 RID: 19249
	public UnopenedPackStack m_MultipleStack;

	// Token: 0x04004B32 RID: 19250
	public GameObject m_LockRibbon;

	// Token: 0x04004B33 RID: 19251
	public GameObject m_AmountBanner;

	// Token: 0x04004B34 RID: 19252
	public UberText m_AmountText;

	// Token: 0x04004B35 RID: 19253
	public UberText m_LockedRibbonText;

	// Token: 0x04004B36 RID: 19254
	public Spell m_AlertEvent;

	// Token: 0x04004B37 RID: 19255
	public Spell m_DragStartEvent;

	// Token: 0x04004B38 RID: 19256
	public Spell m_DragStopEvent;

	// Token: 0x04004B39 RID: 19257
	public DragRotatorInfo m_DragRotatorInfo = new DragRotatorInfo
	{
		m_PitchInfo = new DragRotatorAxisInfo
		{
			m_ForceMultiplier = 3f,
			m_MinDegrees = -55f,
			m_MaxDegrees = 55f,
			m_RestSeconds = 2f
		},
		m_RollInfo = new DragRotatorAxisInfo
		{
			m_ForceMultiplier = 4.5f,
			m_MinDegrees = -60f,
			m_MaxDegrees = 60f,
			m_RestSeconds = 2f
		}
	};

	// Token: 0x04004B3A RID: 19258
	private NetCache.BoosterStack m_boosterStack = new NetCache.BoosterStack
	{
		Id = 0,
		Count = 0
	};

	// Token: 0x04004B3B RID: 19259
	private UnopenedPack m_draggedPack;

	// Token: 0x04004B3C RID: 19260
	private UnopenedPack m_creatorPack;

	// Token: 0x04004B3D RID: 19261
	private bool m_isRotatedPack;
}
