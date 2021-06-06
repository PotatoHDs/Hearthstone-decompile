using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000900 RID: 2304
[RequireComponent(typeof(Spell))]
[RequireComponent(typeof(Actor))]
public class PuzzleController : MonoBehaviour
{
	// Token: 0x0600803E RID: 32830 RVA: 0x0029B1BC File Offset: 0x002993BC
	private void Awake()
	{
		this.m_spell = base.GetComponent<Spell>();
		if (this.m_spell == null)
		{
			Log.Gameplay.PrintError("PuzzleController.Awake(): GameObject {0} does not have a Spell Component!", new object[]
			{
				base.gameObject.name
			});
		}
		this.m_spell.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnSpellEvent));
		this.m_actor = base.GetComponent<Actor>();
		if (this.m_actor == null)
		{
			Log.Gameplay.PrintError("PuzzleController.Awake(): GameObject {0} does not have an Actor Component!", new object[]
			{
				base.gameObject.name
			});
		}
	}

	// Token: 0x0600803F RID: 32831 RVA: 0x0029B25C File Offset: 0x0029945C
	private void Start()
	{
		this.m_puzzleProgressUI = this.m_PuzzleProgressUIContainer.PrefabGameObject(true).GetComponent<PuzzleProgressUI>();
		this.m_puzzleProgressUI.Hide();
		Transform parent = Board.Get().FindBone(this.m_PuzzleUIBoneName);
		this.m_puzzleProgressUI.transform.parent = parent;
		TransformUtil.Identity(this.m_puzzleProgressUI);
	}

	// Token: 0x06008040 RID: 32832 RVA: 0x0029B2B8 File Offset: 0x002994B8
	public void OnDestroy()
	{
		this.NotifyMousedOut();
	}

	// Token: 0x06008041 RID: 32833 RVA: 0x0029B2C0 File Offset: 0x002994C0
	public void NotifyMousedOver()
	{
		if (!this.GetEntity().HasTag(GAME_TAG.PUZZLE_COMPLETED))
		{
			base.StopCoroutine("WaitThenShowPuzzleUI");
			base.StartCoroutine("WaitThenShowPuzzleUI");
		}
	}

	// Token: 0x06008042 RID: 32834 RVA: 0x0029B2EB File Offset: 0x002994EB
	public void NotifyMousedOut()
	{
		base.StopCoroutine("WaitThenShowPuzzleUI");
		this.m_puzzleProgressUI.Hide();
	}

	// Token: 0x06008043 RID: 32835 RVA: 0x0029B303 File Offset: 0x00299503
	private IEnumerator WaitThenShowPuzzleUI()
	{
		yield return new WaitForSeconds(InputManager.Get().m_MouseOverDelay);
		if (this.GetEntity() == null)
		{
			yield break;
		}
		this.m_puzzleProgressUI.UpdateText(this.GetEntity());
		this.m_puzzleProgressUI.Show();
		yield break;
	}

	// Token: 0x06008044 RID: 32836 RVA: 0x0029B314 File Offset: 0x00299514
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

	// Token: 0x06008045 RID: 32837 RVA: 0x0029B33C File Offset: 0x0029953C
	public void UpdatePuzzleUI()
	{
		this.UpdateProgressText();
		if (this.GetEntity().HasTag(GAME_TAG.PUZZLE_COMPLETED))
		{
			GameState.Get().SetBusy(true);
			this.m_ProgressUpdateParticles.Stop();
			this.m_ProgressUpdateParticles.Play();
			this.m_spell.ActivateState(SpellStateType.ACTION);
		}
	}

	// Token: 0x06008046 RID: 32838 RVA: 0x0029B38E File Offset: 0x0029958E
	private void OnSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "FlashCompleted")
		{
			GameState.Get().SetBusy(false);
		}
	}

	// Token: 0x06008047 RID: 32839 RVA: 0x0029B3A8 File Offset: 0x002995A8
	private void UpdateProgressText()
	{
		this.m_ProgressText.Text = string.Format("{0}/{1}", this.GetEntity().GetTag(GAME_TAG.PUZZLE_PROGRESS), this.GetEntity().GetTag(GAME_TAG.PUZZLE_PROGRESS_TOTAL));
		this.m_puzzleProgressUI.UpdateText(this.GetEntity());
	}

	// Token: 0x06008048 RID: 32840 RVA: 0x0029B405 File Offset: 0x00299605
	private Entity GetEntity()
	{
		if (this.m_entity == null)
		{
			this.m_entity = this.m_actor.GetEntity();
		}
		return this.m_entity;
	}

	// Token: 0x04006936 RID: 26934
	public UberText m_ProgressText;

	// Token: 0x04006937 RID: 26935
	public NestedPrefab m_PuzzleProgressUIContainer;

	// Token: 0x04006938 RID: 26936
	public string m_PuzzleUIBoneName = "QuestUI";

	// Token: 0x04006939 RID: 26937
	public ParticleSystem m_ProgressUpdateParticles;

	// Token: 0x0400693A RID: 26938
	public float m_PuzzleCompleteDelay = 1f;

	// Token: 0x0400693B RID: 26939
	private Spell m_spell;

	// Token: 0x0400693C RID: 26940
	private Actor m_actor;

	// Token: 0x0400693D RID: 26941
	private Entity m_entity;

	// Token: 0x0400693E RID: 26942
	private PuzzleProgressUI m_puzzleProgressUI;
}
