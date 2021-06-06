using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Spell))]
[RequireComponent(typeof(Actor))]
public class PuzzleController : MonoBehaviour
{
	public UberText m_ProgressText;

	public NestedPrefab m_PuzzleProgressUIContainer;

	public string m_PuzzleUIBoneName = "QuestUI";

	public ParticleSystem m_ProgressUpdateParticles;

	public float m_PuzzleCompleteDelay = 1f;

	private Spell m_spell;

	private Actor m_actor;

	private Entity m_entity;

	private PuzzleProgressUI m_puzzleProgressUI;

	private void Awake()
	{
		m_spell = GetComponent<Spell>();
		if (m_spell == null)
		{
			Log.Gameplay.PrintError("PuzzleController.Awake(): GameObject {0} does not have a Spell Component!", base.gameObject.name);
		}
		m_spell.AddSpellEventCallback(OnSpellEvent);
		m_actor = GetComponent<Actor>();
		if (m_actor == null)
		{
			Log.Gameplay.PrintError("PuzzleController.Awake(): GameObject {0} does not have an Actor Component!", base.gameObject.name);
		}
	}

	private void Start()
	{
		m_puzzleProgressUI = m_PuzzleProgressUIContainer.PrefabGameObject(instantiateIfNeeded: true).GetComponent<PuzzleProgressUI>();
		m_puzzleProgressUI.Hide();
		Transform parent = Board.Get().FindBone(m_PuzzleUIBoneName);
		m_puzzleProgressUI.transform.parent = parent;
		TransformUtil.Identity(m_puzzleProgressUI);
	}

	public void OnDestroy()
	{
		NotifyMousedOut();
	}

	public void NotifyMousedOver()
	{
		if (!GetEntity().HasTag(GAME_TAG.PUZZLE_COMPLETED))
		{
			StopCoroutine("WaitThenShowPuzzleUI");
			StartCoroutine("WaitThenShowPuzzleUI");
		}
	}

	public void NotifyMousedOut()
	{
		StopCoroutine("WaitThenShowPuzzleUI");
		m_puzzleProgressUI.Hide();
	}

	private IEnumerator WaitThenShowPuzzleUI()
	{
		yield return new WaitForSeconds(InputManager.Get().m_MouseOverDelay);
		if (GetEntity() != null)
		{
			m_puzzleProgressUI.UpdateText(GetEntity());
			m_puzzleProgressUI.Show();
		}
	}

	public void OnRealTimePuzzleCompleted(int newValue)
	{
		if (newValue == 1)
		{
			EndTurnButton endTurnButton = EndTurnButton.Get();
			if (endTurnButton != null)
			{
				endTurnButton.AddInputBlocker();
			}
		}
	}

	public void UpdatePuzzleUI()
	{
		UpdateProgressText();
		if (GetEntity().HasTag(GAME_TAG.PUZZLE_COMPLETED))
		{
			GameState.Get().SetBusy(busy: true);
			m_ProgressUpdateParticles.Stop();
			m_ProgressUpdateParticles.Play();
			m_spell.ActivateState(SpellStateType.ACTION);
		}
	}

	private void OnSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "FlashCompleted")
		{
			GameState.Get().SetBusy(busy: false);
		}
	}

	private void UpdateProgressText()
	{
		m_ProgressText.Text = $"{GetEntity().GetTag(GAME_TAG.PUZZLE_PROGRESS)}/{GetEntity().GetTag(GAME_TAG.PUZZLE_PROGRESS_TOTAL)}";
		m_puzzleProgressUI.UpdateText(GetEntity());
	}

	private Entity GetEntity()
	{
		if (m_entity == null)
		{
			m_entity = m_actor.GetEntity();
		}
		return m_entity;
	}
}
