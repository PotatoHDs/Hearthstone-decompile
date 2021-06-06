using System;
using UnityEngine;

// Token: 0x02000322 RID: 802
public class ManaCounter : MonoBehaviour
{
	// Token: 0x06002D86 RID: 11654 RVA: 0x000E7760 File Offset: 0x000E5960
	private void Awake()
	{
		this.m_textMesh = base.GetComponent<UberText>();
		if (this.m_Side != Player.Side.FRIENDLY)
		{
			if (this.m_availableManaPhone != null)
			{
				string message = "The property m_availableManaPhone is set on ManaCounter for non-friendly mana crystals. This should be null.";
				SceneDebugger.Get().AddErrorMessage(message);
			}
			if (this.m_permanentManaPhone != null)
			{
				string message2 = "The property m_permanentManaPhone is set on ManaCounter for non-friendly mana crystals. This should be null.";
				SceneDebugger.Get().AddErrorMessage(message2);
			}
		}
	}

	// Token: 0x06002D87 RID: 11655 RVA: 0x000E77C0 File Offset: 0x000E59C0
	private void Start()
	{
		this.m_textMesh.Text = GameStrings.Format("GAMEPLAY_MANA_COUNTER", new object[]
		{
			"0",
			"0"
		});
	}

	// Token: 0x06002D88 RID: 11656 RVA: 0x000E77F0 File Offset: 0x000E59F0
	public void InitializeLargeResourceGameObject(string resourcePath)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (this.m_phoneGem != null)
		{
			UnityEngine.Object.Destroy(this.m_phoneGem);
		}
		this.m_phoneGem = AssetLoader.Get().InstantiatePrefab(resourcePath, AssetLoadingOptions.IgnorePrefabPosition);
		GameUtils.SetParent(this.m_phoneGem, this.m_phoneGemContainer, true);
	}

	// Token: 0x06002D89 RID: 11657 RVA: 0x000E784C File Offset: 0x000E5A4C
	public void SetPlayer(Player player)
	{
		this.m_player = player;
	}

	// Token: 0x06002D8A RID: 11658 RVA: 0x000E7855 File Offset: 0x000E5A55
	public Player GetPlayer()
	{
		return this.m_player;
	}

	// Token: 0x06002D8B RID: 11659 RVA: 0x000E785D File Offset: 0x000E5A5D
	public GameObject GetPhoneGem()
	{
		return this.m_phoneGem;
	}

	// Token: 0x06002D8C RID: 11660 RVA: 0x000E7868 File Offset: 0x000E5A68
	public void UpdateText()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		int tag = this.m_player.GetTag(GAME_TAG.RESOURCES);
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(true);
		}
		int numAvailableResources = this.m_player.GetNumAvailableResources();
		string text;
		if (UniversalInputManager.UsePhoneUI && tag >= 10)
		{
			text = numAvailableResources.ToString();
		}
		else
		{
			text = GameStrings.Format("GAMEPLAY_MANA_COUNTER", new object[]
			{
				numAvailableResources,
				tag
			});
		}
		this.m_textMesh.Text = text;
		if (UniversalInputManager.UsePhoneUI && this.m_availableManaPhone != null && this.m_Side == Player.Side.FRIENDLY)
		{
			this.m_availableManaPhone.Text = numAvailableResources.ToString();
			this.m_permanentManaPhone.Text = tag.ToString();
		}
	}

	// Token: 0x04001900 RID: 6400
	public Player.Side m_Side;

	// Token: 0x04001901 RID: 6401
	public GameObject m_phoneGemContainer;

	// Token: 0x04001902 RID: 6402
	public UberText m_availableManaPhone;

	// Token: 0x04001903 RID: 6403
	public UberText m_permanentManaPhone;

	// Token: 0x04001904 RID: 6404
	private Player m_player;

	// Token: 0x04001905 RID: 6405
	private UberText m_textMesh;

	// Token: 0x04001906 RID: 6406
	private GameObject m_phoneGem;
}
