using UnityEngine;

public class GuestHeroPickerButton : HeroPickerButton
{
	private GuestHeroDbfRecord m_guestHero;

	public void SetGuestHero(GuestHeroDbfRecord guestHero)
	{
		m_guestHero = guestHero;
	}

	public override GuestHeroDbfRecord GetGuestHero()
	{
		return m_guestHero;
	}

	public override void UpdateDisplay(DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		base.UpdateDisplay(def, premium);
		if (m_guestHero == null)
		{
			SetClassname(string.Empty);
			m_heroClassIcon.SetActive(value: false);
			return;
		}
		m_heroClass = GameUtils.GetTagClassFromCardDbId(m_guestHero.CardId);
		SetClassname(m_guestHero.Name);
		SetClassIcon(GetClassIconMaterial(m_heroClass));
		SetupClassIconAndName();
	}

	private void SetupClassIconAndName()
	{
		bool flag = (GetEntityDef()?.GetTag(GAME_TAG.MULTIPLE_CLASSES) ?? 0) > 0;
		Transform parent = (flag ? m_bones.m_classLabelNoIcon : m_bones.m_classLabelOneLine);
		m_classLabel.transform.parent = parent;
		m_classLabel.transform.localPosition = Vector3.zero;
		m_classLabel.transform.localScale = Vector3.one;
		m_labelGradient.transform.parent = m_bones.m_gradientOneLine;
		m_labelGradient.transform.localPosition = Vector3.zero;
		m_labelGradient.transform.localScale = Vector3.one;
		m_heroClassIcon.SetActive(!flag);
	}
}
