using System;
using UnityEngine;

// Token: 0x02000827 RID: 2087
public class StartOfGameSpell : SuperSpell
{
	// Token: 0x0600702E RID: 28718 RVA: 0x00242ED0 File Offset: 0x002410D0
	public override bool AddPowerTargets()
	{
		Card sourceCard = base.GetSourceCard();
		EntityDef entityDef = sourceCard.GetEntity().GetEntityDef();
		Player controller = sourceCard.GetController();
		if (controller.HasSeenStartOfGameSpell(entityDef))
		{
			return false;
		}
		bool flag = base.AddPowerTargets();
		if (flag)
		{
			controller.MarkStartOfGameSpellAsSeen(entityDef);
		}
		return flag;
	}

	// Token: 0x0600702F RID: 28719 RVA: 0x00242F10 File Offset: 0x00241110
	protected override void OnAction(SpellStateType prevStateType)
	{
		Card sourceCard = base.GetSourceCard();
		EntityDef entityDef = sourceCard.GetEntity().GetEntityDef();
		TAG_PREMIUM premium = sourceCard.GetPremium();
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(entityDef, premium), AssetLoadingOptions.IgnorePrefabPosition);
		Actor component = gameObject.GetComponent<Actor>();
		component.SetCardDefFromCard(sourceCard);
		component.SetEntityDef(entityDef);
		component.SetPremium(premium);
		component.UpdateAllComponents();
		gameObject.SetActive(false);
		PlayMakerFSM component2 = base.GetComponent<PlayMakerFSM>();
		component2.FsmVariables.GetFsmGameObject("CardGO").Value = gameObject;
		bool flag = GameState.Get().GetFirstOpponentPlayer(sourceCard.GetController()).HasSeenStartOfGameSpell(entityDef);
		if (!flag && this.m_InitialVO != null)
		{
			component2.FsmVariables.GetFsmGameObject("VOLineGO").Value = this.m_InitialVO;
		}
		else if (flag && this.m_ResponseVO != null)
		{
			component2.FsmVariables.GetFsmGameObject("VOLineGO").Value = this.m_ResponseVO;
		}
		base.OnAction(prevStateType);
	}

	// Token: 0x040059FB RID: 23035
	public GameObject m_InitialVO;

	// Token: 0x040059FC RID: 23036
	public GameObject m_ResponseVO;
}
