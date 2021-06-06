using System;
using UnityEngine;

public class UnopenedPack : PegUIElement
{
	public UnopenedPackStack m_SingleStack;

	public UnopenedPackStack m_MultipleStack;

	public GameObject m_LockRibbon;

	public GameObject m_AmountBanner;

	public UberText m_AmountText;

	public UberText m_LockedRibbonText;

	public Spell m_AlertEvent;

	public Spell m_DragStartEvent;

	public Spell m_DragStopEvent;

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

	private NetCache.BoosterStack m_boosterStack = new NetCache.BoosterStack
	{
		Id = 0,
		Count = 0
	};

	private UnopenedPack m_draggedPack;

	private UnopenedPack m_creatorPack;

	private bool m_isRotatedPack;

	protected override void Awake()
	{
		base.Awake();
		UpdateState();
	}

	public NetCache.BoosterStack GetBoosterStack()
	{
		return m_boosterStack;
	}

	public void AddBoosters(int numNewBoosters)
	{
		m_boosterStack.Count += numNewBoosters;
		UpdateState();
	}

	public void AddBooster()
	{
		AddBoosters(1);
	}

	public void SetBoosterStack(NetCache.BoosterStack boosterStack)
	{
		m_boosterStack = boosterStack;
		if (GameDbf.Booster.GetRecord(boosterStack.Id) != null)
		{
			m_isRotatedPack = GameUtils.IsBoosterRotated((BoosterDbId)boosterStack.Id, DateTime.UtcNow);
		}
		UpdateState();
	}

	public void RemoveBooster()
	{
		m_boosterStack.Count--;
		if (m_boosterStack.Count < 0)
		{
			Debug.LogWarning("UnopenedPack.RemoveBooster(): Removed a booster pack from a stack with no boosters");
			m_boosterStack.Count = 0;
		}
		UpdateState();
	}

	public UnopenedPack AcquireDraggedPack()
	{
		if (m_draggedPack != null)
		{
			return m_draggedPack;
		}
		Vector3 position = base.transform.position;
		position.y -= 5000f;
		m_draggedPack = UnityEngine.Object.Instantiate(this, position, base.transform.rotation);
		TransformUtil.CopyWorldScale(m_draggedPack, this);
		m_draggedPack.transform.parent = base.transform.parent;
		UIBScrollableItem component = m_draggedPack.GetComponent<UIBScrollableItem>();
		if (component != null)
		{
			component.m_active = UIBScrollableItem.ActiveState.Inactive;
		}
		m_draggedPack.m_creatorPack = this;
		m_draggedPack.gameObject.AddComponent<DragRotator>().SetInfo(m_DragRotatorInfo);
		m_draggedPack.m_DragStartEvent.Activate();
		return m_draggedPack;
	}

	public void ReleaseDraggedPack()
	{
		if (!(m_draggedPack == null))
		{
			UnopenedPack draggedPack = m_draggedPack;
			m_draggedPack = null;
			draggedPack.m_DragStopEvent.AddStateFinishedCallback(OnDragStopSpellStateFinished, draggedPack);
			draggedPack.m_DragStopEvent.Activate();
			UpdateState();
		}
	}

	public UnopenedPack GetDraggedPack()
	{
		return m_draggedPack;
	}

	public UnopenedPack GetCreatorPack()
	{
		return m_creatorPack;
	}

	public void PlayAlert()
	{
		m_AlertEvent.ActivateState(SpellStateType.BIRTH);
	}

	public void StopAlert()
	{
		m_AlertEvent.ActivateState(SpellStateType.DEATH);
	}

	public bool CanOpenPack()
	{
		NetCache.BoosterStack boosterStack = GetBoosterStack();
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
		if (eventType == SpecialEventType.UNKNOWN)
		{
			return false;
		}
		if (m_isRotatedPack && !RankMgr.Get().WildCardsAllowedInCurrentLeague())
		{
			return false;
		}
		if (eventType == SpecialEventType.IGNORE)
		{
			return true;
		}
		if (SpecialEventManager.Get().IsEventActive(eventType, activeIfDoesNotExist: false))
		{
			return true;
		}
		if (GameUtils.AtPrereleaseEvent() && SpecialEventManager.Get().IsEventActive(record.PrereleaseOpenPackEvent, activeIfDoesNotExist: false))
		{
			return true;
		}
		return false;
	}

	public void UpdateState()
	{
		bool flag = CanOpenPack();
		if (m_LockRibbon != null)
		{
			m_LockRibbon.SetActive(!flag);
		}
		bool flag2 = m_boosterStack.Count == 0;
		bool flag3 = m_boosterStack.Count > 1;
		m_SingleStack.m_RootObject.SetActive((!flag3 || !flag) && !flag2);
		m_MultipleStack.m_RootObject.SetActive(flag3 && !flag2 && flag);
		m_AmountBanner.SetActive(flag3);
		m_AmountText.enabled = flag3;
		if (flag3)
		{
			m_AmountText.Text = m_boosterStack.Count.ToString();
		}
		if (m_isRotatedPack && RankMgr.Get().GetLocalPlayerStandardLeagueConfig().LockWildBoosters && m_LockedRibbonText != null)
		{
			m_LockedRibbonText.Text = GameStrings.Get("GLUE_NEW_PLAYER_AVAILABLE_AT_LEAGUE_PROMO");
		}
	}

	private void OnDragStopSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			UnityEngine.Object.Destroy(((UnopenedPack)userData).gameObject);
		}
	}
}
