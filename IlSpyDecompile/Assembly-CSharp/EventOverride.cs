using System;
using System.Collections.Generic;
using Assets;
using UnityEngine;

[CustomEditClass]
public class EventOverride : MonoBehaviour
{
	[Serializable]
	public class EventOverrideElement
	{
		public SpecialEventType EventType;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string EventPrefab;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string EventPrefab_phone;

		public GameObject Parent;

		public bool showToReturningPlayers;

		public bool showToNewPlayers;
	}

	public List<EventOverrideElement> m_SpecialEvents;

	private List<Spell> m_ActiveSpells { get; set; }

	private void Start()
	{
		m_ActiveSpells = new List<Spell>();
		if (GameMgr.Get().IsTutorial())
		{
			return;
		}
		foreach (EventOverrideElement specialEvent in m_SpecialEvents)
		{
			if (SpecialEventManager.Get().IsEventActive(specialEvent.EventType, activeIfDoesNotExist: false))
			{
				LoadSpecialEvent(specialEvent);
			}
		}
	}

	public virtual void LoadSpecialEvent(EventOverrideElement specialEvent)
	{
		if (!SpecialEventManager.Get().IsEventForcedActive(specialEvent.EventType) && ((!specialEvent.showToNewPlayers && !AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY)) || (!specialEvent.showToReturningPlayers && ReturningPlayerMgr.Get().IsInReturningPlayerMode)))
		{
			return;
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
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text);
		if (gameObject == null)
		{
			Debug.LogWarning($"Failed to load special event prefab: {text}");
			return;
		}
		gameObject.transform.SetParent(base.transform, worldPositionStays: false);
		if (specialEvent.Parent != null)
		{
			gameObject.transform.SetParent(specialEvent.Parent.transform, worldPositionStays: true);
		}
		Spell component = gameObject.GetComponent<Spell>();
		if (component != null)
		{
			component.ActivateState(SpellStateType.BIRTH);
			m_ActiveSpells.Add(component);
		}
	}

	private void OnDisable()
	{
		foreach (Spell activeSpell in m_ActiveSpells)
		{
			if (activeSpell != null && activeSpell.gameObject.activeSelf && activeSpell.IsActive())
			{
				activeSpell.ActivateState(SpellStateType.DEATH);
			}
		}
		m_ActiveSpells.Clear();
	}
}
