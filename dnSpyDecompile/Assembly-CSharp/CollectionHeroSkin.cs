using System;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class CollectionHeroSkin : MonoBehaviour
{
	// Token: 0x06001062 RID: 4194 RVA: 0x0005B73C File Offset: 0x0005993C
	public void Awake()
	{
		Actor component = base.gameObject.GetComponent<Actor>();
		if (component != null)
		{
			component.SetUseShortName(true);
			if (UniversalInputManager.UsePhoneUI)
			{
				component.OverrideNameText(null);
			}
		}
		this.ShowName = this.m_showName;
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x06001063 RID: 4195 RVA: 0x0005B784 File Offset: 0x00059984
	// (set) Token: 0x06001064 RID: 4196 RVA: 0x0005B78C File Offset: 0x0005998C
	public bool ShowName
	{
		get
		{
			return this.m_showName;
		}
		set
		{
			this.m_showName = value;
			Actor component = base.gameObject.GetComponent<Actor>();
			UberText activeNameText = this.GetActiveNameText();
			component.OverrideNameText(activeNameText);
			if (this.m_nameShadow != null)
			{
				this.m_nameShadow.gameObject.SetActive(this.m_showName && !UniversalInputManager.UsePhoneUI);
			}
		}
	}

	// Token: 0x06001065 RID: 4197 RVA: 0x0005B7F0 File Offset: 0x000599F0
	public void SetClass(TAG_CLASS classTag)
	{
		if (this.m_classIcon != null)
		{
			Vector2 value = CollectionPageManager.s_classTextureOffsets[classTag];
			Renderer component = this.m_classIcon.GetComponent<Renderer>();
			(Application.isPlaying ? component.GetMaterial() : component.GetSharedMaterial()).SetTextureOffset("_MainTex", value);
		}
		if (this.m_favoriteBannerText != null)
		{
			this.m_favoriteBannerText.Text = GameStrings.Format("GLUE_COLLECTION_MANAGER_FAVORITE_DEFAULT_TEXT", new object[]
			{
				GameStrings.GetClassName(classTag)
			});
		}
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x0005B875 File Offset: 0x00059A75
	public void ShowShadow(bool show)
	{
		if (this.m_shadow == null)
		{
			return;
		}
		this.m_shadow.SetActive(show);
	}

	// Token: 0x06001067 RID: 4199 RVA: 0x0005B892 File Offset: 0x00059A92
	public void ShowFavoriteBanner(bool show)
	{
		if (this.m_favoriteBanner == null)
		{
			return;
		}
		this.m_favoriteBanner.SetActive(show);
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x0005B8AF File Offset: 0x00059AAF
	public void ShowSocketFX()
	{
		if (this.m_socketFX == null || !this.m_socketFX.gameObject.activeInHierarchy)
		{
			return;
		}
		this.m_socketFX.gameObject.SetActive(true);
		this.m_socketFX.Activate();
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x0005B8EE File Offset: 0x00059AEE
	public void HideSocketFX()
	{
		if (this.m_socketFX != null)
		{
			this.m_socketFX.Deactivate();
		}
	}

	// Token: 0x0600106A RID: 4202 RVA: 0x0005B90C File Offset: 0x00059B0C
	public void ShowCollectionManagerText()
	{
		Actor component = base.gameObject.GetComponent<Actor>();
		if (component != null)
		{
			UberText activeNameText = this.GetActiveNameText();
			component.OverrideNameText(activeNameText);
			if (component.isMissingCard())
			{
				component.UpdateMissingCardArt();
			}
		}
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x0005B94A File Offset: 0x00059B4A
	private UberText GetActiveNameText()
	{
		if (!this.m_showName)
		{
			return null;
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			return this.m_name;
		}
		return this.m_collectionManagerName;
	}

	// Token: 0x0600106C RID: 4204 RVA: 0x0005B970 File Offset: 0x00059B70
	[ContextMenu("Toggle Missing Card Effect")]
	private void ToggleMissingCardEffect()
	{
		Actor component = base.gameObject.GetComponent<Actor>();
		if (component != null)
		{
			if (component.isMissingCard())
			{
				component.DisableMissingCardEffect();
			}
			else
			{
				component.MissingCardEffect(true);
			}
			component.UpdateAllComponents();
		}
	}

	// Token: 0x04000AF6 RID: 2806
	public MeshRenderer m_classIcon;

	// Token: 0x04000AF7 RID: 2807
	public GameObject m_favoriteBanner;

	// Token: 0x04000AF8 RID: 2808
	public UberText m_favoriteBannerText;

	// Token: 0x04000AF9 RID: 2809
	public GameObject m_shadow;

	// Token: 0x04000AFA RID: 2810
	public Spell m_socketFX;

	// Token: 0x04000AFB RID: 2811
	public UberText m_name;

	// Token: 0x04000AFC RID: 2812
	public GameObject m_nameShadow;

	// Token: 0x04000AFD RID: 2813
	public UberText m_collectionManagerName;

	// Token: 0x04000AFE RID: 2814
	private bool m_showName = true;
}
