using System.Collections;
using UnityEngine;

public class SummonOutForge : SpellImpl
{
	public GameObject m_scryLines;

	public Material m_scryLinesMaterial;

	public GameObject m_burstMotes;

	private static Color COMMON_COLOR = new Color(11f / 15f, 0.8235294f, 1f);

	private static Color RARE_COLOR = new Color(0.2f, 121f / 255f, 1f);

	private static Color EPIC_COLOR = new Color(139f / 255f, 59f / 255f, 1f);

	private static Color LEGENDARY_COLOR = new Color(1f, 2f / 3f, 0.2f);

	protected override void OnBirth(SpellStateType prevStateType)
	{
		StartCoroutine(BirthState());
	}

	private IEnumerator BirthState()
	{
		InitActorVariables();
		SetActorVisibility(visible: true, ignoreSpells: false);
		SetVisibility(m_scryLines, visible: true);
		TAG_RARITY rarity = m_actor.GetRarity();
		Material material = m_scryLines.GetComponent<Renderer>().GetMaterial();
		switch (rarity)
		{
		case TAG_RARITY.RARE:
			m_scryLinesMaterial.SetColor("_TintColor", RARE_COLOR);
			material.SetColor("_TintColor", RARE_COLOR);
			break;
		case TAG_RARITY.EPIC:
			m_scryLinesMaterial.SetColor("_TintColor", EPIC_COLOR);
			material.SetColor("_TintColor", EPIC_COLOR);
			break;
		case TAG_RARITY.LEGENDARY:
			m_scryLinesMaterial.SetColor("_TintColor", LEGENDARY_COLOR);
			material.SetColor("_TintColor", LEGENDARY_COLOR);
			break;
		default:
			m_scryLinesMaterial.SetColor("_TintColor", COMMON_COLOR);
			material.SetColor("_TintColor", COMMON_COLOR);
			break;
		}
		PlayAnimation(m_scryLines, "AllyInHandScryLines_ForgeOut", PlayMode.StopAll);
		PlayParticles(m_burstMotes, includeChildren: false);
		yield return new WaitForSeconds(0.16f);
		m_rootObject.SetActive(value: false);
		OnSpellFinished();
	}
}
