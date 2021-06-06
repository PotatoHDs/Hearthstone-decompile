using System;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class TooltipPanel : MonoBehaviour
{
	// Token: 0x1700050C RID: 1292
	// (get) Token: 0x060030DD RID: 12509 RVA: 0x000FB741 File Offset: 0x000F9941
	public bool Destroyed
	{
		get
		{
			return this.m_destroyed || !this.m_name || !this.m_body;
		}
	}

	// Token: 0x060030DE RID: 12510 RVA: 0x000FB768 File Offset: 0x000F9968
	private void Awake()
	{
		SceneUtils.SetLayer(base.gameObject, GameLayer.Tooltip);
		TooltipPanel.FORGE_SCALE = TooltipPanel.COLLECTION_MANAGER_SCALE;
		this.scaleToUse = TooltipPanel.GAMEPLAY_SCALE;
	}

	// Token: 0x060030DF RID: 12511 RVA: 0x000FB791 File Offset: 0x000F9991
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.m_name);
		this.m_name = null;
		UnityEngine.Object.Destroy(this.m_body);
		this.m_body = null;
		UnityEngine.Object.Destroy(this.m_background);
		this.m_background = null;
		this.m_destroyed = true;
	}

	// Token: 0x060030E0 RID: 12512 RVA: 0x000FB7D0 File Offset: 0x000F99D0
	public void Reset()
	{
		base.transform.localScale = Vector3.one;
		base.transform.eulerAngles = Vector3.zero;
	}

	// Token: 0x060030E1 RID: 12513 RVA: 0x000FB7F2 File Offset: 0x000F99F2
	public void SetScale(float newScale)
	{
		this.scaleToUse = newScale;
		base.transform.localScale = new Vector3(this.scaleToUse, this.scaleToUse, this.scaleToUse);
	}

	// Token: 0x060030E2 RID: 12514 RVA: 0x000FB81D File Offset: 0x000F9A1D
	public virtual void Initialize(string keywordName, string keywordText)
	{
		this.SetName(keywordName);
		this.SetBodyText(keywordText);
		base.gameObject.SetActive(true);
		this.m_name.UpdateNow(false);
		this.m_body.UpdateNow(false);
	}

	// Token: 0x060030E3 RID: 12515 RVA: 0x000FB851 File Offset: 0x000F9A51
	public void SetName(string s)
	{
		this.m_name.Text = s;
	}

	// Token: 0x060030E4 RID: 12516 RVA: 0x000FB85F File Offset: 0x000F9A5F
	public void SetBodyText(string s)
	{
		this.m_body.Text = s;
	}

	// Token: 0x060030E5 RID: 12517 RVA: 0x000FB870 File Offset: 0x000F9A70
	public virtual float GetHeight()
	{
		return this.m_background.GetComponent<Renderer>().bounds.size.z;
	}

	// Token: 0x060030E6 RID: 12518 RVA: 0x000FB89C File Offset: 0x000F9A9C
	public virtual float GetWidth()
	{
		return this.m_background.GetComponent<Renderer>().bounds.size.x;
	}

	// Token: 0x060030E7 RID: 12519 RVA: 0x000FB8C6 File Offset: 0x000F9AC6
	public bool IsTextRendered()
	{
		return this.m_name.IsDone() && this.m_body.IsDone();
	}

	// Token: 0x060030E8 RID: 12520 RVA: 0x000FB8E4 File Offset: 0x000F9AE4
	public void ShiftBodyText()
	{
		if (!this.Destroyed && this.m_name.Text.Length == 0)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_body.transform.position += new Vector3(0f, 0f, this.m_name.Height + this.m_name.LineSpacing * this.k_scaleOffsetPhone);
				return;
			}
			if (PlatformSettings.IsMobileRuntimeOS && !UniversalInputManager.UsePhoneUI)
			{
				this.m_body.transform.position += new Vector3(0f, 0f, this.m_name.Height + this.m_name.LineSpacing * this.k_scaleOffsetTablet);
				return;
			}
			this.m_body.transform.position += new Vector3(0f, 0f, this.m_name.Height + this.m_name.LineSpacing * this.k_scaleOffset);
		}
	}

	// Token: 0x04001B27 RID: 6951
	public UberText m_name;

	// Token: 0x04001B28 RID: 6952
	public UberText m_body;

	// Token: 0x04001B29 RID: 6953
	public GameObject m_background;

	// Token: 0x04001B2A RID: 6954
	private float k_scaleOffset = 1.2f;

	// Token: 0x04001B2B RID: 6955
	private float k_scaleOffsetPhone = 4.2f;

	// Token: 0x04001B2C RID: 6956
	private float k_scaleOffsetTablet = 2f;

	// Token: 0x04001B2D RID: 6957
	private bool m_destroyed;

	// Token: 0x04001B2E RID: 6958
	protected float m_initialBackgroundHeight;

	// Token: 0x04001B2F RID: 6959
	protected Vector3 m_initialBackgroundScale = Vector3.zero;

	// Token: 0x04001B30 RID: 6960
	public const float GAMEPLAY_SCALE_FOR_SHOW_TOOLTIP = 0.75f;

	// Token: 0x04001B31 RID: 6961
	public static PlatformDependentValue<float> HAND_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.65f,
		Phone = 0.8f
	};

	// Token: 0x04001B32 RID: 6962
	public static PlatformDependentValue<float> GAMEPLAY_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.75f,
		Phone = 1.4f
	};

	// Token: 0x04001B33 RID: 6963
	public static PlatformDependentValue<float> GAMEPLAY_SCALE_LARGE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.9f,
		Phone = 0.625f
	};

	// Token: 0x04001B34 RID: 6964
	public static PlatformDependentValue<float> HISTORY_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.48f,
		Phone = 0.853f
	};

	// Token: 0x04001B35 RID: 6965
	public static PlatformDependentValue<float> MULLIGAN_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 0.65f,
		Phone = 0.4f
	};

	// Token: 0x04001B36 RID: 6966
	public const float GAMEPLAY_HERO_POWER_SCALE = 0.6f;

	// Token: 0x04001B37 RID: 6967
	public static PlatformDependentValue<float> BOX_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 8f,
		Phone = 4.5f
	};

	// Token: 0x04001B38 RID: 6968
	public const float OPEN_BOX_SCALE_FOR_SHOW_TOOLTIP = 4f;

	// Token: 0x04001B39 RID: 6969
	public static PlatformDependentValue<float> COLLECTION_MANAGER_SCALE = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		PC = 4f,
		Phone = 8f
	};

	// Token: 0x04001B3A RID: 6970
	public static PlatformDependentValue<float> FORGE_SCALE = TooltipPanel.COLLECTION_MANAGER_SCALE;

	// Token: 0x04001B3B RID: 6971
	public static PlatformDependentValue<float> ADVENTURE_SCALE = TooltipPanel.COLLECTION_MANAGER_SCALE;

	// Token: 0x04001B3C RID: 6972
	public const float PACK_OPENING_SCALE = 2.75f;

	// Token: 0x04001B3D RID: 6973
	public const float UNOPENED_PACK_SCALE = 5f;

	// Token: 0x04001B3E RID: 6974
	public const float DECK_HELPER_SCALE = 3.75f;

	// Token: 0x04001B3F RID: 6975
	protected float scaleToUse;
}
