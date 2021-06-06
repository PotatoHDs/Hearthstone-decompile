using System.Collections;
using UnityEngine;

public class DeckCardBarSummonInForge : SpellImpl
{
	public GameObject m_echoQuad;

	public Material m_echoQuadMaterial;

	public GameObject m_fxEvaporate;

	public Material m_fxEvaporateMaterial;

	private static Color COMMON_COLOR = new Color(1f, 1f, 1f);

	private static Color COMMON_TINT_COLOR = new Color(47f / 51f, 241f / 255f, 1f);

	private static Color RARE_COLOR = new Color(14f / 85f, 104f / 255f, 1f);

	private static Color RARE_TINT_COLOR = new Color(14f / 85f, 104f / 255f, 1f);

	private static Color EPIC_COLOR = new Color(106f / 255f, 14f / 85f, 1f);

	private static Color EPIC_TINT_COLOR = new Color(106f / 255f, 14f / 85f, 253f / 255f);

	private static Color LEGENDARY_COLOR = new Color(196f / 255f, 46f / 85f, 38f / 255f);

	private static Color LEGENDARY_TINT_COLOR = new Color(2f / 3f, 121f / 255f, 11f / 85f);

	protected override void OnBirth(SpellStateType prevStateType)
	{
		StartCoroutine(BirthState());
	}

	private IEnumerator BirthState()
	{
		InitActorVariables();
		SetAnimationTime(m_echoQuad, "Secret_AbilityEchoOut_Forge", 0f);
		SetVisibility(m_echoQuad, visible: true);
		Material material = GetMaterial(m_echoQuad, m_echoQuadMaterial);
		switch (m_actor.GetRarity())
		{
		case TAG_RARITY.RARE:
			SetMaterialColor(m_echoQuad, material, "_Color", RARE_COLOR);
			SetMaterialColor(m_fxEvaporate, m_fxEvaporateMaterial, "_TintColor", RARE_TINT_COLOR);
			break;
		case TAG_RARITY.EPIC:
			SetMaterialColor(m_echoQuad, material, "_Color", EPIC_COLOR);
			SetMaterialColor(m_fxEvaporate, m_fxEvaporateMaterial, "_TintColor", EPIC_TINT_COLOR);
			break;
		case TAG_RARITY.LEGENDARY:
			SetMaterialColor(m_echoQuad, material, "_Color", LEGENDARY_COLOR);
			SetMaterialColor(m_fxEvaporate, m_fxEvaporateMaterial, "_TintColor", LEGENDARY_TINT_COLOR);
			break;
		default:
			SetMaterialColor(m_echoQuad, material, "_Color", COMMON_COLOR);
			SetMaterialColor(m_fxEvaporate, m_fxEvaporateMaterial, "_TintColor", COMMON_TINT_COLOR);
			break;
		}
		SetActorVisibility(visible: true, ignoreSpells: true);
		PlayParticles(m_fxEvaporate, includeChildren: false);
		SetAnimationSpeed(m_echoQuad, "Secret_AbilityEchoOut_Forge", 0.2f);
		PlayAnimation(m_echoQuad, "Secret_AbilityEchoOut_Forge", PlayMode.StopAll);
		OnSpellFinished();
		yield return new WaitForSeconds(1f);
		SetVisibility(m_echoQuad, visible: false);
	}
}
