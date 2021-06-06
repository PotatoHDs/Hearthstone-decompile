using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x020007DD RID: 2013
public class CustomChoiceConcealSpell : CustomChoiceSpell
{
	// Token: 0x06006E41 RID: 28225 RVA: 0x00239095 File Offset: 0x00237295
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (this.m_fakeCorrectCardActor != null)
		{
			this.m_fakeCorrectCardActor.Destroy();
		}
		if (this.m_fakeCorrectCardBackActor != null)
		{
			this.m_fakeCorrectCardBackActor.Destroy();
		}
	}

	// Token: 0x06006E42 RID: 28226 RVA: 0x002390CF File Offset: 0x002372CF
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoEffects());
	}

	// Token: 0x06006E43 RID: 28227 RVA: 0x002390E5 File Offset: 0x002372E5
	private IEnumerator DoEffects()
	{
		while (!this.FindResultOfChoice())
		{
			yield return null;
		}
		if (!this.m_choseCorrectly && !this.LoadFakeActors())
		{
			this.OnSpellFinished();
			this.OnStateFinished();
			yield break;
		}
		yield return base.StartCoroutine(this.PlayChoiceEffects());
		this.ResetCorrectCardCardback();
		foreach (Card card in this.m_choiceState.m_cards)
		{
			if (!(card == this.m_correctCard) || !this.m_choseCorrectly)
			{
				card.HideCard();
			}
		}
		if (!this.m_choseCorrectly)
		{
			this.m_fakeCorrectCardActor.Show();
			yield return new WaitForSeconds(this.m_SendCardBackToOpponentsDeckDelay);
			if (!this.m_fakeCorrectCardActor.GetEntity().IsHidden())
			{
				yield return base.StartCoroutine(SpellUtils.FlipActorAndReplaceWithOtherActor(this.m_fakeCorrectCardActor, this.m_fakeCorrectCardBackActor, 0.5f));
			}
			else
			{
				this.m_fakeCorrectCardBackActor.Show();
				this.m_fakeCorrectCardActor.Hide();
			}
			this.PlayFadeAwaySpellThenFinish();
		}
		else
		{
			this.FinishSpell();
		}
		yield break;
	}

	// Token: 0x06006E44 RID: 28228 RVA: 0x002384C7 File Offset: 0x002366C7
	private void FinishSpell()
	{
		this.OnSpellFinished();
		this.OnStateFinished();
	}

	// Token: 0x06006E45 RID: 28229 RVA: 0x002390F4 File Offset: 0x002372F4
	private bool LoadFakeActors()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(this.m_correctCard.GetActorAssetPath(), AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Log.Spells.PrintError("CustomChoiceConcealSpell.LoadFakeActors(): Failed to load fake actor for card " + this.m_correctCard, Array.Empty<object>());
			return false;
		}
		global::Player.Side oppositePlayerSide = global::Player.GetOppositePlayerSide(this.m_correctCard.GetControllerSide());
		this.m_fakeCorrectCardActor = gameObject.GetComponent<Actor>();
		this.m_fakeCorrectCardActor.SetCardDefFromCard(this.m_correctCard);
		this.m_fakeCorrectCardActor.SetEntity(this.m_correctCard.GetEntity());
		this.m_fakeCorrectCardActor.SetEntityDef(this.m_correctCard.GetEntity().GetEntityDef());
		this.m_fakeCorrectCardActor.SetCardBackSideOverride(new global::Player.Side?(oppositePlayerSide));
		this.m_fakeCorrectCardActor.UpdateAllComponents();
		TransformUtil.CopyWorld(this.m_fakeCorrectCardActor, this.m_correctCard.GetActor());
		this.m_fakeCorrectCardActor.Hide();
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject2 == null)
		{
			Log.Spells.PrintError("CustomChoiceConcealSpell.LoadFakeActors(): Failed to load fake card back actor.", Array.Empty<object>());
			return false;
		}
		this.m_fakeCorrectCardBackActor = gameObject2.GetComponent<Actor>();
		this.m_fakeCorrectCardBackActor.SetCardBackSideOverride(new global::Player.Side?(oppositePlayerSide));
		this.m_fakeCorrectCardBackActor.UpdateAllComponents();
		TransformUtil.CopyWorld(this.m_fakeCorrectCardBackActor, this.m_correctCard.GetActor());
		this.m_fakeCorrectCardBackActor.Hide();
		return true;
	}

	// Token: 0x06006E46 RID: 28230 RVA: 0x00239262 File Offset: 0x00237462
	private IEnumerator DestroyChoiceEffect(Spell spell)
	{
		yield return new WaitForSeconds(2f);
		UnityEngine.Object.Destroy(spell);
		yield break;
	}

	// Token: 0x06006E47 RID: 28231 RVA: 0x00239271 File Offset: 0x00237471
	private void OnEffectStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			base.StartCoroutine(this.DestroyChoiceEffect(spell));
		}
	}

	// Token: 0x06006E48 RID: 28232 RVA: 0x00239289 File Offset: 0x00237489
	private void OnChoiceEffectSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "ResetCorrectCardCardBack")
		{
			this.ResetCorrectCardCardback();
		}
	}

	// Token: 0x06006E49 RID: 28233 RVA: 0x0023929E File Offset: 0x0023749E
	private IEnumerator PlayChoiceEffects()
	{
		int effectsToWaitFor = 0;
		Spell.FinishedCallback callback = delegate(Spell spell, object userData)
		{
			int effectsToWaitFor2 = effectsToWaitFor - 1;
			effectsToWaitFor = effectsToWaitFor2;
		};
		using (List<Card>.Enumerator enumerator = this.m_choiceState.m_cards.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Card card = enumerator.Current;
				bool flag = card.GetEntity().IsHidden();
				Spell original = flag ? this.m_HiddenCorrectChoiceSpell : this.m_CorrectChoiceSpell;
				Spell original2 = flag ? this.m_HiddenSuperCorrectChoiceSpell : this.m_SuperCorrectChoiceSpell;
				Spell original3 = flag ? this.m_HiddenWrongChoiceSpell : this.m_WrongChoiceSpell;
				Actor actor = card.GetActor();
				if (card == this.m_correctCard)
				{
					int effectsToWaitFor3 = effectsToWaitFor + 1;
					effectsToWaitFor = effectsToWaitFor3;
					Spell spell3 = this.m_choseCorrectly ? UnityEngine.Object.Instantiate<Spell>(original2) : UnityEngine.Object.Instantiate<Spell>(original);
					spell3.transform.parent = actor.transform;
					TransformUtil.Identity(spell3);
					spell3.AddFinishedCallback(callback);
					spell3.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnEffectStateFinished));
					spell3.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnChoiceEffectSpellEvent));
					spell3.Activate();
				}
				else
				{
					int effectsToWaitFor3 = effectsToWaitFor + 1;
					effectsToWaitFor = effectsToWaitFor3;
					Spell spell2 = UnityEngine.Object.Instantiate<Spell>(original3);
					spell2.transform.parent = actor.transform;
					TransformUtil.Identity(spell2);
					spell2.AddFinishedCallback(callback);
					spell2.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnEffectStateFinished));
					spell2.Activate();
				}
			}
			goto IL_1C9;
		}
		IL_1B2:
		yield return null;
		IL_1C9:
		if (effectsToWaitFor <= 0)
		{
			yield break;
		}
		goto IL_1B2;
	}

	// Token: 0x06006E4A RID: 28234 RVA: 0x002392B0 File Offset: 0x002374B0
	private void PlayFadeAwaySpellThenFinish()
	{
		Spell.FinishedCallback callback = delegate(Spell spell, object userData)
		{
			this.m_fakeCorrectCardBackActor.Hide();
			this.FinishSpell();
		};
		Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_CorrectChoiceFadeAwaySpell);
		spell2.transform.parent = this.m_correctCard.GetActor().transform;
		TransformUtil.Identity(spell2);
		spell2.AddFinishedCallback(callback);
		spell2.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnEffectStateFinished));
		spell2.Activate();
	}

	// Token: 0x06006E4B RID: 28235 RVA: 0x00239314 File Offset: 0x00237514
	private void ResetCorrectCardCardback()
	{
		this.m_correctCard.GetActor().SetCardBackSideOverride(null);
		this.m_correctCard.GetActor().UpdateCardBack();
		if (this.m_correctCard.GetControllerSide() == global::Player.Side.FRIENDLY)
		{
			this.m_correctCard.SetTransitionStyle(ZoneTransitionStyle.SLOW);
		}
	}

	// Token: 0x06006E4C RID: 28236 RVA: 0x00239364 File Offset: 0x00237564
	private bool FindResultOfChoice()
	{
		List<PowerTaskList> list = new List<PowerTaskList>();
		list.Add(GameState.Get().GetPowerProcessor().GetCurrentTaskList());
		foreach (PowerTaskList item in GameState.Get().GetPowerProcessor().GetPowerQueue().GetList())
		{
			list.Add(item);
		}
		foreach (PowerTaskList powerTaskList in list)
		{
			if (powerTaskList != null && powerTaskList.GetBlockType() == HistoryBlock.Type.POWER && powerTaskList.GetSourceEntity(true) == GameState.Get().GetEntity(this.m_choiceState.m_sourceEntityId))
			{
				if (powerTaskList.IsBlockUnended())
				{
					return false;
				}
				foreach (PowerTask powerTask in powerTaskList.GetTaskList())
				{
					Network.HistMetaData histMetaData = powerTask.GetPower() as Network.HistMetaData;
					if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.TARGET)
					{
						global::Entity entity = GameState.Get().GetEntity(histMetaData.Info[0]);
						if (entity != null)
						{
							foreach (Card card in this.m_choiceState.m_cards)
							{
								if (card.GetEntity() == entity)
								{
									this.m_correctCard = card;
									this.m_choseCorrectly = this.m_choiceState.m_chosenEntities.Contains(card.GetEntity());
									return true;
								}
							}
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04005885 RID: 22661
	public Spell m_WrongChoiceSpell;

	// Token: 0x04005886 RID: 22662
	public Spell m_CorrectChoiceSpell;

	// Token: 0x04005887 RID: 22663
	public Spell m_SuperCorrectChoiceSpell;

	// Token: 0x04005888 RID: 22664
	public Spell m_HiddenWrongChoiceSpell;

	// Token: 0x04005889 RID: 22665
	public Spell m_HiddenCorrectChoiceSpell;

	// Token: 0x0400588A RID: 22666
	public Spell m_HiddenSuperCorrectChoiceSpell;

	// Token: 0x0400588B RID: 22667
	public Spell m_CorrectChoiceFadeAwaySpell;

	// Token: 0x0400588C RID: 22668
	public float m_SendCardBackToOpponentsDeckDelay = 0.25f;

	// Token: 0x0400588D RID: 22669
	private bool m_choseCorrectly;

	// Token: 0x0400588E RID: 22670
	private Card m_correctCard;

	// Token: 0x0400588F RID: 22671
	private Actor m_fakeCorrectCardActor;

	// Token: 0x04005890 RID: 22672
	private Actor m_fakeCorrectCardBackActor;
}
