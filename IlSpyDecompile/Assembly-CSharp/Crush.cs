using UnityEngine;

public class Crush : Spell
{
	public MinionPieces m_minionPieces;

	public Material m_premiumTauntMaterial;

	public Material m_premiumEliteMaterial;

	public UberText m_attack;

	public UberText m_health;

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		Entity entity = GetSourceCard().GetEntity();
		Actor actor = SceneUtils.FindComponentInParents<Actor>(this);
		GameObject gameObject = m_minionPieces.m_main;
		bool flag = entity.HasTag(GAME_TAG.PREMIUM);
		if (flag)
		{
			gameObject = m_minionPieces.m_premium;
			SceneUtils.EnableRenderers(m_minionPieces.m_main, enable: false);
		}
		GameObject portraitMesh = actor.GetPortraitMesh();
		gameObject.GetComponent<Renderer>().SetMaterial(portraitMesh.GetComponent<Renderer>().GetSharedMaterial());
		gameObject.SetActive(value: true);
		SceneUtils.EnableRenderers(gameObject, enable: true);
		if (entity.HasTaunt())
		{
			if (flag)
			{
				m_minionPieces.m_taunt.GetComponent<Renderer>().SetMaterial(m_premiumTauntMaterial);
			}
			m_minionPieces.m_taunt.SetActive(value: true);
			SceneUtils.EnableRenderers(m_minionPieces.m_taunt, enable: true);
		}
		if (entity.IsElite())
		{
			if (flag)
			{
				m_minionPieces.m_legendary.GetComponent<Renderer>().SetMaterial(m_premiumEliteMaterial);
			}
			m_minionPieces.m_legendary.SetActive(value: true);
			SceneUtils.EnableRenderers(m_minionPieces.m_legendary, enable: true);
		}
		m_attack.SetGameStringText(entity.GetATK().ToString());
		m_health.SetGameStringText(entity.GetHealth().ToString());
	}
}
