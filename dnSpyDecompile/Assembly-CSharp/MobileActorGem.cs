using System;
using UnityEngine;

// Token: 0x02000329 RID: 809
public class MobileActorGem : MonoBehaviour
{
	// Token: 0x06002DDF RID: 11743 RVA: 0x000E8F34 File Offset: 0x000E7134
	private void Awake()
	{
		if (PlatformSettings.IsMobile())
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				if (this.m_gemType == MobileActorGem.GemType.CardPlay)
				{
					base.gameObject.transform.localScale *= 1.6f;
					this.m_uberText.transform.localScale *= 0.9f;
					this.m_uberText.OutlineSize = 3.2f;
				}
				else if (this.m_gemType == MobileActorGem.GemType.CardHero_Attack)
				{
					base.gameObject.transform.localScale *= 1.6f;
					TransformUtil.SetLocalPosX(base.gameObject, base.gameObject.transform.localPosition.x - 0.075f);
					TransformUtil.SetLocalPosZ(base.gameObject, base.gameObject.transform.localPosition.z + 0.255f);
					this.m_uberText.transform.localScale *= 0.9f;
					this.m_uberText.OutlineSize = 3.2f;
				}
				else if (this.m_gemType == MobileActorGem.GemType.CardHero_Health)
				{
					base.gameObject.transform.localScale *= 1.6f;
					TransformUtil.SetLocalPosX(base.gameObject, base.gameObject.transform.localPosition.x + 0.05f);
					TransformUtil.SetLocalPosZ(base.gameObject, base.gameObject.transform.localPosition.z + 0.255f);
					this.m_uberText.transform.localPosition = new Vector3(0f, 0.154f, -0.0235f);
					this.m_uberText.OutlineSize = 3.6f;
				}
				else if (this.m_gemType == MobileActorGem.GemType.CardHero_Armor)
				{
					base.gameObject.transform.localScale *= 1.15f;
					TransformUtil.SetLocalPosX(base.gameObject, 0.06f);
					TransformUtil.SetLocalPosZ(base.gameObject, base.gameObject.transform.localPosition.z - 0.3f);
					this.m_uberText.transform.localScale *= 1.4f;
					this.m_uberText.FontSize = 50;
					this.m_uberText.CharacterSize = 8f;
					this.m_uberText.OutlineSize = 3.2f;
				}
				else if (this.m_gemType == MobileActorGem.GemType.CardHeroPower)
				{
					TransformUtil.SetLocalScaleXZ(base.gameObject, new Vector2(1.334f * base.gameObject.transform.localScale.x, 1.334f * base.gameObject.transform.localScale.z));
					TransformUtil.SetLocalScaleXY(this.m_uberText, new Vector2(1.5f * this.m_uberText.transform.localScale.x, 1.5f * this.m_uberText.transform.localScale.y));
					TransformUtil.SetLocalPosZ(this.m_uberText, this.m_uberText.transform.localPosition.z + 0.04f);
				}
			}
			else if (this.m_gemType == MobileActorGem.GemType.CardPlay)
			{
				base.gameObject.transform.localScale *= 1.3f;
				this.m_uberText.transform.localScale *= 0.9f;
				this.m_uberText.OutlineSize = 3.2f;
			}
			TransformUtil.SetLocalPosX(base.gameObject, base.gameObject.transform.localPosition.x + this.m_additionalOffset.x);
			TransformUtil.SetLocalPosY(base.gameObject, base.gameObject.transform.localPosition.y + this.m_additionalOffset.y);
			TransformUtil.SetLocalPosZ(base.gameObject, base.gameObject.transform.localPosition.z + this.m_additionalOffset.z);
		}
	}

	// Token: 0x0400194C RID: 6476
	public UberText m_uberText;

	// Token: 0x0400194D RID: 6477
	public MobileActorGem.GemType m_gemType;

	// Token: 0x0400194E RID: 6478
	public Vector3 m_additionalOffset = Vector3.zero;

	// Token: 0x020016B2 RID: 5810
	public enum GemType
	{
		// Token: 0x0400B169 RID: 45417
		CardPlay,
		// Token: 0x0400B16A RID: 45418
		CardHero_Health,
		// Token: 0x0400B16B RID: 45419
		CardHero_Attack,
		// Token: 0x0400B16C RID: 45420
		CardHero_Armor,
		// Token: 0x0400B16D RID: 45421
		CardHeroPower
	}
}
