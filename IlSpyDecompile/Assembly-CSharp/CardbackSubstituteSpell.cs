using System.Collections.Generic;
using UnityEngine;

public class CardbackSubstituteSpell : Spell
{
	public List<Transform> m_FriendlyBones;

	public List<Transform> m_OpponentBones;

	private List<Actor> m_fakeActors;

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		LoadFakeActors();
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		for (int i = 0; i < m_fakeActors.Count; i++)
		{
			component.FsmVariables.GetFsmGameObject("Card" + (i + 1)).Value = m_fakeActors[i].gameObject;
		}
	}

	private void LoadFakeActors()
	{
		m_fakeActors = SetupActor(m_FriendlyBones, Player.Side.FRIENDLY);
		m_fakeActors.AddRange(SetupActor(m_OpponentBones, Player.Side.OPPOSING));
	}

	private List<Actor> SetupActor(List<Transform> bones, Player.Side side)
	{
		List<Actor> list = new List<Actor>();
		for (int i = 0; i < bones.Count; i++)
		{
			Actor component = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition).GetComponent<Actor>();
			component.SetCardBackSideOverride(side);
			component.UpdateAllComponents();
			list.Add(component);
			component.transform.parent = bones[i];
			GameUtils.ResetTransform(component);
		}
		return list;
	}

	public override void OnSpellFinished()
	{
		foreach (Actor fakeActor in m_fakeActors)
		{
			fakeActor.Destroy();
		}
		base.OnSpellFinished();
	}
}
