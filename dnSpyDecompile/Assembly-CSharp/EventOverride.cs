using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

// Token: 0x02000A29 RID: 2601
[CustomEditClass]
public class EventOverride : MonoBehaviour
{
	// Token: 0x170007C7 RID: 1991
	// (get) Token: 0x06008BE4 RID: 35812 RVA: 0x002CC6FC File Offset: 0x002CA8FC
	// (set) Token: 0x06008BE5 RID: 35813 RVA: 0x002CC704 File Offset: 0x002CA904
	private List<Spell> m_ActiveSpells { get; set; }

	// Token: 0x06008BE6 RID: 35814 RVA: 0x002CC710 File Offset: 0x002CA910
	private void Start()
	{
		this.m_ActiveSpells = new List<Spell>();
		if (!GameMgr.Get().IsTutorial())
		{
			foreach (EventOverride.EventOverrideElement eventOverrideElement in this.m_SpecialEvents)
			{
				if (SpecialEventManager.Get().IsEventActive(eventOverrideElement.EventType, false))
				{
					this.LoadSpecialEvent(eventOverrideElement);
				}
			}
		}
	}

	// Token: 0x06008BE7 RID: 35815 RVA: 0x002CC790 File Offset: 0x002CA990
	public virtual void LoadSpecialEvent(EventOverride.EventOverrideElement specialEvent)
	{
		if (!SpecialEventManager.Get().IsEventForcedActive(specialEvent.EventType))
		{
			if (!specialEvent.showToNewPlayers && !AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY))
			{
				return;
			}
			if (!specialEvent.showToReturningPlayers && ReturningPlayerMgr.Get().IsInReturningPlayerMode)
			{
				return;
			}
		}
		string text = specialEvent.EventPrefab;
		if (PlatformSettings.Screen == ScreenCategory.Phone && !string.IsNullOrEmpty(specialEvent.EventPrefab_phone))
		{
			text = specialEvent.EventPrefab_phone;
		}
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Debug.LogWarning(string.Format("Failed to load special event prefab: {0}", text));
			return;
		}
		gameObject.transform.SetParent(base.transform, false);
		if (specialEvent.Parent != null)
		{
			gameObject.transform.SetParent(specialEvent.Parent.transform, true);
		}
		Spell component = gameObject.GetComponent<Spell>();
		if (component != null)
		{
			component.ActivateState(SpellStateType.BIRTH);
			this.m_ActiveSpells.Add(component);
		}
	}

	// Token: 0x06008BE8 RID: 35816 RVA: 0x002CC88C File Offset: 0x002CAA8C
	private void OnDisable()
	{
		foreach (Spell spell in this.m_ActiveSpells)
		{
			if (spell != null && spell.gameObject.activeSelf && spell.IsActive())
			{
				spell.ActivateState(SpellStateType.DEATH);
			}
		}
		this.m_ActiveSpells.Clear();
	}

	// Token: 0x040074CA RID: 29898
	public List<EventOverride.EventOverrideElement> m_SpecialEvents;

	// Token: 0x0200269C RID: 9884
	[Serializable]
	public class EventOverrideElement
	{
		// Token: 0x0400F13B RID: 61755
		public SpecialEventType EventType;

		// Token: 0x0400F13C RID: 61756
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string EventPrefab;

		// Token: 0x0400F13D RID: 61757
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string EventPrefab_phone;

		// Token: 0x0400F13E RID: 61758
		public GameObject Parent;

		// Token: 0x0400F13F RID: 61759
		public bool showToReturningPlayers;

		// Token: 0x0400F140 RID: 61760
		public bool showToNewPlayers;
	}
}
