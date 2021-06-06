using UnityEngine;

public class StartOfGameSpell : SuperSpell
{
	public GameObject m_InitialVO;

	public GameObject m_ResponseVO;

	public override bool AddPowerTargets()
	{
		Card sourceCard = GetSourceCard();
		EntityDef entityDef = sourceCard.GetEntity().GetEntityDef();
		Player controller = sourceCard.GetController();
		if (controller.HasSeenStartOfGameSpell(entityDef))
		{
			return false;
		}
		bool num = base.AddPowerTargets();
		if (num)
		{
			controller.MarkStartOfGameSpellAsSeen(entityDef);
		}
		return num;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		Card sourceCard = GetSourceCard();
		EntityDef entityDef = sourceCard.GetEntity().GetEntityDef();
		TAG_PREMIUM premium = sourceCard.GetPremium();
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, premium), AssetLoadingOptions.IgnorePrefabPosition);
		Actor component = gameObject.GetComponent<Actor>();
		component.SetCardDefFromCard(sourceCard);
		component.SetEntityDef(entityDef);
		component.SetPremium(premium);
		component.UpdateAllComponents();
		gameObject.SetActive(value: false);
		PlayMakerFSM component2 = GetComponent<PlayMakerFSM>();
		component2.FsmVariables.GetFsmGameObject("CardGO").Value = gameObject;
		bool flag = GameState.Get().GetFirstOpponentPlayer(sourceCard.GetController()).HasSeenStartOfGameSpell(entityDef);
		if (!flag && m_InitialVO != null)
		{
			component2.FsmVariables.GetFsmGameObject("VOLineGO").Value = m_InitialVO;
		}
		else if (flag && m_ResponseVO != null)
		{
			component2.FsmVariables.GetFsmGameObject("VOLineGO").Value = m_ResponseVO;
		}
		base.OnAction(prevStateType);
	}
}
