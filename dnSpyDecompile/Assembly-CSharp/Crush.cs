using System;
using UnityEngine;

// Token: 0x020007DC RID: 2012
public class Crush : Spell
{
	// Token: 0x06006E3F RID: 28223 RVA: 0x00238F4C File Offset: 0x0023714C
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		Entity entity = base.GetSourceCard().GetEntity();
		Actor actor = SceneUtils.FindComponentInParents<Actor>(this);
		GameObject gameObject = this.m_minionPieces.m_main;
		bool flag = entity.HasTag(GAME_TAG.PREMIUM);
		if (flag)
		{
			gameObject = this.m_minionPieces.m_premium;
			SceneUtils.EnableRenderers(this.m_minionPieces.m_main, false);
		}
		GameObject portraitMesh = actor.GetPortraitMesh();
		gameObject.GetComponent<Renderer>().SetMaterial(portraitMesh.GetComponent<Renderer>().GetSharedMaterial());
		gameObject.SetActive(true);
		SceneUtils.EnableRenderers(gameObject, true);
		if (entity.HasTaunt())
		{
			if (flag)
			{
				this.m_minionPieces.m_taunt.GetComponent<Renderer>().SetMaterial(this.m_premiumTauntMaterial);
			}
			this.m_minionPieces.m_taunt.SetActive(true);
			SceneUtils.EnableRenderers(this.m_minionPieces.m_taunt, true);
		}
		if (entity.IsElite())
		{
			if (flag)
			{
				this.m_minionPieces.m_legendary.GetComponent<Renderer>().SetMaterial(this.m_premiumEliteMaterial);
			}
			this.m_minionPieces.m_legendary.SetActive(true);
			SceneUtils.EnableRenderers(this.m_minionPieces.m_legendary, true);
		}
		this.m_attack.SetGameStringText(entity.GetATK().ToString());
		this.m_health.SetGameStringText(entity.GetHealth().ToString());
	}

	// Token: 0x04005880 RID: 22656
	public MinionPieces m_minionPieces;

	// Token: 0x04005881 RID: 22657
	public Material m_premiumTauntMaterial;

	// Token: 0x04005882 RID: 22658
	public Material m_premiumEliteMaterial;

	// Token: 0x04005883 RID: 22659
	public UberText m_attack;

	// Token: 0x04005884 RID: 22660
	public UberText m_health;
}
