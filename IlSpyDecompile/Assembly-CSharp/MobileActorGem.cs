using UnityEngine;

public class MobileActorGem : MonoBehaviour
{
	public enum GemType
	{
		CardPlay,
		CardHero_Health,
		CardHero_Attack,
		CardHero_Armor,
		CardHeroPower
	}

	public UberText m_uberText;

	public GemType m_gemType;

	public Vector3 m_additionalOffset = Vector3.zero;

	private void Awake()
	{
		if (!PlatformSettings.IsMobile())
		{
			return;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_gemType == GemType.CardPlay)
			{
				base.gameObject.transform.localScale *= 1.6f;
				m_uberText.transform.localScale *= 0.9f;
				m_uberText.OutlineSize = 3.2f;
			}
			else if (m_gemType == GemType.CardHero_Attack)
			{
				base.gameObject.transform.localScale *= 1.6f;
				TransformUtil.SetLocalPosX(base.gameObject, base.gameObject.transform.localPosition.x - 0.075f);
				TransformUtil.SetLocalPosZ(base.gameObject, base.gameObject.transform.localPosition.z + 0.255f);
				m_uberText.transform.localScale *= 0.9f;
				m_uberText.OutlineSize = 3.2f;
			}
			else if (m_gemType == GemType.CardHero_Health)
			{
				base.gameObject.transform.localScale *= 1.6f;
				TransformUtil.SetLocalPosX(base.gameObject, base.gameObject.transform.localPosition.x + 0.05f);
				TransformUtil.SetLocalPosZ(base.gameObject, base.gameObject.transform.localPosition.z + 0.255f);
				m_uberText.transform.localPosition = new Vector3(0f, 0.154f, -0.0235f);
				m_uberText.OutlineSize = 3.6f;
			}
			else if (m_gemType == GemType.CardHero_Armor)
			{
				base.gameObject.transform.localScale *= 1.15f;
				TransformUtil.SetLocalPosX(base.gameObject, 0.06f);
				TransformUtil.SetLocalPosZ(base.gameObject, base.gameObject.transform.localPosition.z - 0.3f);
				m_uberText.transform.localScale *= 1.4f;
				m_uberText.FontSize = 50;
				m_uberText.CharacterSize = 8f;
				m_uberText.OutlineSize = 3.2f;
			}
			else if (m_gemType == GemType.CardHeroPower)
			{
				TransformUtil.SetLocalScaleXZ(base.gameObject, new Vector2(1.334f * base.gameObject.transform.localScale.x, 1.334f * base.gameObject.transform.localScale.z));
				TransformUtil.SetLocalScaleXY(m_uberText, new Vector2(1.5f * m_uberText.transform.localScale.x, 1.5f * m_uberText.transform.localScale.y));
				TransformUtil.SetLocalPosZ(m_uberText, m_uberText.transform.localPosition.z + 0.04f);
			}
		}
		else if (m_gemType == GemType.CardPlay)
		{
			base.gameObject.transform.localScale *= 1.3f;
			m_uberText.transform.localScale *= 0.9f;
			m_uberText.OutlineSize = 3.2f;
		}
		TransformUtil.SetLocalPosX(base.gameObject, base.gameObject.transform.localPosition.x + m_additionalOffset.x);
		TransformUtil.SetLocalPosY(base.gameObject, base.gameObject.transform.localPosition.y + m_additionalOffset.y);
		TransformUtil.SetLocalPosZ(base.gameObject, base.gameObject.transform.localPosition.z + m_additionalOffset.z);
	}
}
