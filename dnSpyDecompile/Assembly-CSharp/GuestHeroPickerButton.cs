using System;
using UnityEngine;

// Token: 0x02000B16 RID: 2838
public class GuestHeroPickerButton : HeroPickerButton
{
	// Token: 0x060096DD RID: 38621 RVA: 0x0030CEB4 File Offset: 0x0030B0B4
	public void SetGuestHero(GuestHeroDbfRecord guestHero)
	{
		this.m_guestHero = guestHero;
	}

	// Token: 0x060096DE RID: 38622 RVA: 0x0030CEBD File Offset: 0x0030B0BD
	public override GuestHeroDbfRecord GetGuestHero()
	{
		return this.m_guestHero;
	}

	// Token: 0x060096DF RID: 38623 RVA: 0x0030CEC8 File Offset: 0x0030B0C8
	public override void UpdateDisplay(DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		base.UpdateDisplay(def, premium);
		if (this.m_guestHero == null)
		{
			base.SetClassname(string.Empty);
			this.m_heroClassIcon.SetActive(false);
			return;
		}
		this.m_heroClass = GameUtils.GetTagClassFromCardDbId(this.m_guestHero.CardId);
		base.SetClassname(this.m_guestHero.Name);
		base.SetClassIcon(base.GetClassIconMaterial(this.m_heroClass));
		this.SetupClassIconAndName();
	}

	// Token: 0x060096E0 RID: 38624 RVA: 0x0030CF44 File Offset: 0x0030B144
	private void SetupClassIconAndName()
	{
		EntityDef entityDef = base.GetEntityDef();
		bool flag = ((entityDef != null) ? entityDef.GetTag(GAME_TAG.MULTIPLE_CLASSES) : 0) > 0;
		Transform parent = flag ? this.m_bones.m_classLabelNoIcon : this.m_bones.m_classLabelOneLine;
		this.m_classLabel.transform.parent = parent;
		this.m_classLabel.transform.localPosition = Vector3.zero;
		this.m_classLabel.transform.localScale = Vector3.one;
		this.m_labelGradient.transform.parent = this.m_bones.m_gradientOneLine;
		this.m_labelGradient.transform.localPosition = Vector3.zero;
		this.m_labelGradient.transform.localScale = Vector3.one;
		this.m_heroClassIcon.SetActive(!flag);
	}

	// Token: 0x04007E59 RID: 32345
	private GuestHeroDbfRecord m_guestHero;
}
