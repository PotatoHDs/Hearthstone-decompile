using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D1 RID: 2001
public class CardbackSubstituteSpell : Spell
{
	// Token: 0x06006E11 RID: 28177 RVA: 0x00237CDC File Offset: 0x00235EDC
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		this.LoadFakeActors();
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		for (int i = 0; i < this.m_fakeActors.Count; i++)
		{
			component.FsmVariables.GetFsmGameObject("Card" + (i + 1)).Value = this.m_fakeActors[i].gameObject;
		}
	}

	// Token: 0x06006E12 RID: 28178 RVA: 0x00237D46 File Offset: 0x00235F46
	private void LoadFakeActors()
	{
		this.m_fakeActors = this.SetupActor(this.m_FriendlyBones, Player.Side.FRIENDLY);
		this.m_fakeActors.AddRange(this.SetupActor(this.m_OpponentBones, Player.Side.OPPOSING));
	}

	// Token: 0x06006E13 RID: 28179 RVA: 0x00237D74 File Offset: 0x00235F74
	private List<Actor> SetupActor(List<Transform> bones, Player.Side side)
	{
		List<Actor> list = new List<Actor>();
		for (int i = 0; i < bones.Count; i++)
		{
			Actor component = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition).GetComponent<Actor>();
			component.SetCardBackSideOverride(new Player.Side?(side));
			component.UpdateAllComponents();
			list.Add(component);
			component.transform.parent = bones[i];
			GameUtils.ResetTransform(component);
		}
		return list;
	}

	// Token: 0x06006E14 RID: 28180 RVA: 0x00237DE8 File Offset: 0x00235FE8
	public override void OnSpellFinished()
	{
		foreach (Actor actor in this.m_fakeActors)
		{
			actor.Destroy();
		}
		base.OnSpellFinished();
	}

	// Token: 0x0400583E RID: 22590
	public List<Transform> m_FriendlyBones;

	// Token: 0x0400583F RID: 22591
	public List<Transform> m_OpponentBones;

	// Token: 0x04005840 RID: 22592
	private List<Actor> m_fakeActors;
}
