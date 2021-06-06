using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007DE RID: 2014
public class CustomChoiceRevealSpell : CustomChoiceSpell
{
	// Token: 0x06006E4F RID: 28239 RVA: 0x002395AE File Offset: 0x002377AE
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.DoEffect();
	}

	// Token: 0x06006E50 RID: 28240 RVA: 0x002395BD File Offset: 0x002377BD
	public override void OnSpellEvent(string eventName, object eventData)
	{
		base.OnSpellEvent(eventName, eventData);
		if (eventName == "showCards")
		{
			base.StartCoroutine(this.ShowCards());
		}
	}

	// Token: 0x06006E51 RID: 28241 RVA: 0x002395E4 File Offset: 0x002377E4
	private void DoEffect()
	{
		foreach (Card card in this.m_choiceState.m_cards)
		{
			card.SetInputEnabled(false);
		}
		this.LoadFakeActors();
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		component.FsmVariables.GetFsmGameObject("CardA").Value = this.m_fakeActors[0].gameObject;
		component.FsmVariables.GetFsmGameObject("CardB").Value = this.m_fakeActors[1].gameObject;
		component.FsmVariables.GetFsmGameObject("CardC").Value = this.m_fakeActors[2].gameObject;
	}

	// Token: 0x06006E52 RID: 28242 RVA: 0x002396B8 File Offset: 0x002378B8
	private void LoadFakeActors()
	{
		Player.Side value = this.m_choiceState.m_isFriendly ? Player.Side.OPPOSING : Player.Side.FRIENDLY;
		this.m_fakeActors = new List<Actor>();
		for (int i = 0; i < this.m_choiceState.m_cards.Count; i++)
		{
			Actor component = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition).GetComponent<Actor>();
			component.SetCardBackSideOverride(new Player.Side?(value));
			component.UpdateAllComponents();
			TransformUtil.CopyWorld(component, this.m_choiceState.m_cards[i].GetActor());
			this.m_fakeActors.Add(component);
			if (i < this.m_bones.Count)
			{
				component.transform.parent = this.m_bones[i];
				component.transform.position = this.m_bones[i].position;
			}
		}
	}

	// Token: 0x06006E53 RID: 28243 RVA: 0x00239799 File Offset: 0x00237999
	private IEnumerator ShowCards()
	{
		int num;
		for (int i = 0; i < this.m_fakeActors.Count; i = num)
		{
			Card card = this.m_choiceState.m_cards[i];
			Actor actor = this.m_fakeActors[i];
			if (!card.GetEntity().IsHidden())
			{
				if (i == this.m_fakeActors.Count - 1)
				{
					yield return base.StartCoroutine(SpellUtils.FlipActorAndReplaceWithCard(actor, card, 0.5f));
				}
				else
				{
					base.StartCoroutine(SpellUtils.FlipActorAndReplaceWithCard(actor, card, 0.5f));
				}
			}
			else
			{
				card.ShowCard();
				Player.Side value = this.m_choiceState.m_isFriendly ? Player.Side.OPPOSING : Player.Side.FRIENDLY;
				card.GetActor().SetCardBackSideOverride(new Player.Side?(value));
				card.GetActor().UpdateAllComponents();
				actor.Hide();
			}
			num = i + 1;
		}
		this.OnSpellFinished();
		this.OnStateFinished();
		foreach (Card card2 in this.m_choiceState.m_cards)
		{
			card2.SetInputEnabled(true);
		}
		using (List<Actor>.Enumerator enumerator2 = this.m_fakeActors.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				Actor actor2 = enumerator2.Current;
				actor2.Destroy();
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x04005891 RID: 22673
	public List<Transform> m_bones;

	// Token: 0x04005892 RID: 22674
	private List<Actor> m_fakeActors;
}
