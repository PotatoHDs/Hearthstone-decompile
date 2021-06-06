using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200082B RID: 2091
public class SummonOutForge : SpellImpl
{
	// Token: 0x0600703E RID: 28734 RVA: 0x00243564 File Offset: 0x00241764
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.BirthState());
	}

	// Token: 0x0600703F RID: 28735 RVA: 0x00243573 File Offset: 0x00241773
	private IEnumerator BirthState()
	{
		base.InitActorVariables();
		base.SetActorVisibility(true, false);
		base.SetVisibility(this.m_scryLines, true);
		TAG_RARITY rarity = this.m_actor.GetRarity();
		Material material = this.m_scryLines.GetComponent<Renderer>().GetMaterial();
		switch (rarity)
		{
		case TAG_RARITY.RARE:
			this.m_scryLinesMaterial.SetColor("_TintColor", SummonOutForge.RARE_COLOR);
			material.SetColor("_TintColor", SummonOutForge.RARE_COLOR);
			goto IL_111;
		case TAG_RARITY.EPIC:
			this.m_scryLinesMaterial.SetColor("_TintColor", SummonOutForge.EPIC_COLOR);
			material.SetColor("_TintColor", SummonOutForge.EPIC_COLOR);
			goto IL_111;
		case TAG_RARITY.LEGENDARY:
			this.m_scryLinesMaterial.SetColor("_TintColor", SummonOutForge.LEGENDARY_COLOR);
			material.SetColor("_TintColor", SummonOutForge.LEGENDARY_COLOR);
			goto IL_111;
		}
		this.m_scryLinesMaterial.SetColor("_TintColor", SummonOutForge.COMMON_COLOR);
		material.SetColor("_TintColor", SummonOutForge.COMMON_COLOR);
		IL_111:
		base.PlayAnimation(this.m_scryLines, "AllyInHandScryLines_ForgeOut", PlayMode.StopAll, 0f);
		base.PlayParticles(this.m_burstMotes, false);
		yield return new WaitForSeconds(0.16f);
		this.m_rootObject.SetActive(false);
		this.OnSpellFinished();
		yield break;
	}

	// Token: 0x04005A27 RID: 23079
	public GameObject m_scryLines;

	// Token: 0x04005A28 RID: 23080
	public Material m_scryLinesMaterial;

	// Token: 0x04005A29 RID: 23081
	public GameObject m_burstMotes;

	// Token: 0x04005A2A RID: 23082
	private static Color COMMON_COLOR = new Color(0.73333335f, 0.8235294f, 1f);

	// Token: 0x04005A2B RID: 23083
	private static Color RARE_COLOR = new Color(0.2f, 0.4745098f, 1f);

	// Token: 0x04005A2C RID: 23084
	private static Color EPIC_COLOR = new Color(0.54509807f, 0.23137255f, 1f);

	// Token: 0x04005A2D RID: 23085
	private static Color LEGENDARY_COLOR = new Color(1f, 0.6666667f, 0.2f);
}
