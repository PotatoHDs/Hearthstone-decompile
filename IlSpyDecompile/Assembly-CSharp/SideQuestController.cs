using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Spell))]
[RequireComponent(typeof(Actor))]
public class SideQuestController : MonoBehaviour
{
	public UberText m_ProgressText;

	public float m_ProgressUpdateDelay = 1f;

	public ParticleSystem m_ProgressUpdateParticles;

	private Spell m_spell;

	private Actor m_actor;

	private Entity m_entity;

	private int m_currentQuestProgress;

	private int m_questProgressTotal;

	private int m_targetQuestProgress;

	private bool m_isScalingDown;

	private void Awake()
	{
		m_currentQuestProgress = 0;
		m_targetQuestProgress = 0;
		m_isScalingDown = false;
		m_spell = GetComponent<Spell>();
		if (m_spell == null)
		{
			Log.Gameplay.PrintError("SideQuestController.Awake(): GameObject {0} does not have a Spell Component!", base.gameObject.name);
		}
		m_spell.AddSpellEventCallback(OnSpellEvent);
		m_actor = GetComponent<Actor>();
		if (m_actor == null)
		{
			Log.Gameplay.PrintError("SideQuestController.Awake(): GameObject {0} does not have an Actor Component!", base.gameObject.name);
		}
	}

	public void UpdateQuestUI(bool allowQuestComplete)
	{
		StartCoroutine(UpdateQuestUIImpl(allowQuestComplete));
	}

	private IEnumerator UpdateQuestUIImpl(bool allowQuestComplete)
	{
		Entity entity = GetEntity();
		if (entity == null)
		{
			yield break;
		}
		int a = entity.GetTag(GAME_TAG.QUEST_PROGRESS);
		a = Mathf.Min(a, m_questProgressTotal);
		if (a != m_targetQuestProgress && (allowQuestComplete || a < m_questProgressTotal))
		{
			m_targetQuestProgress = a;
			GameState.Get().SetBusy(busy: true);
			while (m_isScalingDown)
			{
				yield return null;
			}
			if (!m_spell.IsActive())
			{
				UpdateProgressText(m_targetQuestProgress);
				m_spell.ActivateState(SpellStateType.ACTION);
			}
		}
	}

	private void OnSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "ScaledUp")
		{
			StartCoroutine(UpdateQuestProgress());
		}
		else if (eventName == "ScaledDown")
		{
			m_isScalingDown = false;
			if (m_currentQuestProgress >= m_questProgressTotal)
			{
				CompleteQuest();
				return;
			}
			GameState.Get().SetBusy(busy: false);
			m_spell.ActivateState(SpellStateType.NONE);
		}
	}

	private IEnumerator UpdateQuestProgress()
	{
		bool done = false;
		while (!done)
		{
			yield return new WaitForSeconds(m_ProgressUpdateDelay);
			if (m_currentQuestProgress < m_targetQuestProgress)
			{
				UpdateProgressText(m_currentQuestProgress + 1);
			}
			else
			{
				done = true;
			}
		}
		m_isScalingDown = true;
		m_spell.GetComponent<PlayMakerFSM>().SendEvent("ScaleDown");
	}

	private void UpdateProgressText(int currentProgress)
	{
		m_currentQuestProgress = currentProgress;
		m_ProgressUpdateParticles.Stop();
		m_ProgressUpdateParticles.Play();
		m_ProgressText.Text = $"{m_currentQuestProgress}/{m_questProgressTotal}";
	}

	private void CompleteQuest()
	{
		GameState.Get().SetBusy(busy: false);
		Card card = m_entity.GetCard();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			ZoneSecret zoneSecret = card.GetZone() as ZoneSecret;
			if (zoneSecret != null && zoneSecret.GetSideQuestCount() == 1)
			{
				card.HideCard();
			}
		}
		else
		{
			card.HideCard();
		}
		m_spell.ActivateState(SpellStateType.DEATH);
	}

	private Entity GetEntity()
	{
		if (m_entity == null)
		{
			m_entity = m_actor.GetEntity();
			if (m_entity != null)
			{
				m_currentQuestProgress = m_entity.GetTag(GAME_TAG.QUEST_PROGRESS);
				m_questProgressTotal = m_entity.GetTag(GAME_TAG.QUEST_PROGRESS_TOTAL);
			}
		}
		return m_entity;
	}
}
