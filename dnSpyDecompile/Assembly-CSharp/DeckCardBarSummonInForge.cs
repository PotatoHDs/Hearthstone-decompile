using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007E5 RID: 2021
public class DeckCardBarSummonInForge : SpellImpl
{
	// Token: 0x06006E7B RID: 28283 RVA: 0x0023A155 File Offset: 0x00238355
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.BirthState());
	}

	// Token: 0x06006E7C RID: 28284 RVA: 0x0023A164 File Offset: 0x00238364
	private IEnumerator BirthState()
	{
		base.InitActorVariables();
		base.SetAnimationTime(this.m_echoQuad, "Secret_AbilityEchoOut_Forge", 0f);
		base.SetVisibility(this.m_echoQuad, true);
		Material material = base.GetMaterial(this.m_echoQuad, this.m_echoQuadMaterial, false, 0);
		switch (this.m_actor.GetRarity())
		{
		case TAG_RARITY.RARE:
			base.SetMaterialColor(this.m_echoQuad, material, "_Color", DeckCardBarSummonInForge.RARE_COLOR, 0);
			base.SetMaterialColor(this.m_fxEvaporate, this.m_fxEvaporateMaterial, "_TintColor", DeckCardBarSummonInForge.RARE_TINT_COLOR, 0);
			goto IL_169;
		case TAG_RARITY.EPIC:
			base.SetMaterialColor(this.m_echoQuad, material, "_Color", DeckCardBarSummonInForge.EPIC_COLOR, 0);
			base.SetMaterialColor(this.m_fxEvaporate, this.m_fxEvaporateMaterial, "_TintColor", DeckCardBarSummonInForge.EPIC_TINT_COLOR, 0);
			goto IL_169;
		case TAG_RARITY.LEGENDARY:
			base.SetMaterialColor(this.m_echoQuad, material, "_Color", DeckCardBarSummonInForge.LEGENDARY_COLOR, 0);
			base.SetMaterialColor(this.m_fxEvaporate, this.m_fxEvaporateMaterial, "_TintColor", DeckCardBarSummonInForge.LEGENDARY_TINT_COLOR, 0);
			goto IL_169;
		}
		base.SetMaterialColor(this.m_echoQuad, material, "_Color", DeckCardBarSummonInForge.COMMON_COLOR, 0);
		base.SetMaterialColor(this.m_fxEvaporate, this.m_fxEvaporateMaterial, "_TintColor", DeckCardBarSummonInForge.COMMON_TINT_COLOR, 0);
		IL_169:
		base.SetActorVisibility(true, true);
		base.PlayParticles(this.m_fxEvaporate, false);
		base.SetAnimationSpeed(this.m_echoQuad, "Secret_AbilityEchoOut_Forge", 0.2f);
		base.PlayAnimation(this.m_echoQuad, "Secret_AbilityEchoOut_Forge", PlayMode.StopAll, 0f);
		this.OnSpellFinished();
		yield return new WaitForSeconds(1f);
		base.SetVisibility(this.m_echoQuad, false);
		yield break;
	}

	// Token: 0x040058A5 RID: 22693
	public GameObject m_echoQuad;

	// Token: 0x040058A6 RID: 22694
	public Material m_echoQuadMaterial;

	// Token: 0x040058A7 RID: 22695
	public GameObject m_fxEvaporate;

	// Token: 0x040058A8 RID: 22696
	public Material m_fxEvaporateMaterial;

	// Token: 0x040058A9 RID: 22697
	private static Color COMMON_COLOR = new Color(1f, 1f, 1f);

	// Token: 0x040058AA RID: 22698
	private static Color COMMON_TINT_COLOR = new Color(0.92156863f, 0.94509804f, 1f);

	// Token: 0x040058AB RID: 22699
	private static Color RARE_COLOR = new Color(0.16470589f, 0.40784314f, 1f);

	// Token: 0x040058AC RID: 22700
	private static Color RARE_TINT_COLOR = new Color(0.16470589f, 0.40784314f, 1f);

	// Token: 0x040058AD RID: 22701
	private static Color EPIC_COLOR = new Color(0.41568628f, 0.16470589f, 1f);

	// Token: 0x040058AE RID: 22702
	private static Color EPIC_TINT_COLOR = new Color(0.41568628f, 0.16470589f, 0.99215686f);

	// Token: 0x040058AF RID: 22703
	private static Color LEGENDARY_COLOR = new Color(0.76862746f, 0.5411765f, 0.14901961f);

	// Token: 0x040058B0 RID: 22704
	private static Color LEGENDARY_TINT_COLOR = new Color(0.6666667f, 0.4745098f, 0.12941177f);
}
