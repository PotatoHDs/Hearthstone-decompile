using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x020006F4 RID: 1780
public class GeneralStoreAdventureContentDisplay : MonoBehaviour
{
	// Token: 0x06006355 RID: 25429 RVA: 0x00206255 File Offset: 0x00204455
	private void Awake()
	{
		if (this.m_leavingSoonButton != null)
		{
			this.m_leavingSoonButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.OnLeavingSoonButtonClicked();
			});
		}
	}

	// Token: 0x06006356 RID: 25430 RVA: 0x0020627E File Offset: 0x0020447E
	private void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_logoTexture);
	}

	// Token: 0x06006357 RID: 25431 RVA: 0x0020628C File Offset: 0x0020448C
	public void UpdateAdventureType(StoreAdventureDef advDef, AdventureDbfRecord advRecord)
	{
		if (advDef == null)
		{
			return;
		}
		AssetLoader.Get().LoadAsset<Texture>(advDef.m_logoTextureName, delegate(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object data)
		{
			if (!loadedTexture)
			{
				Debug.LogError(string.Format("Failed to load texture {0}!", assetRef));
				return;
			}
			AssetHandle.Take<Texture>(ref this.m_logoTexture, loadedTexture);
			this.m_logo.GetMaterial().mainTexture = this.m_logoTexture;
		}, null, AssetLoadingOptions.None);
		this.m_keyArt.SetMaterial(advDef.m_keyArt);
		if (this.m_leavingSoonBanner != null)
		{
			this.m_leavingSoonBanner.SetActive(advRecord.LeavingSoon);
			if (advRecord.LeavingSoon)
			{
				this.m_leavingSoonInfoText = advRecord.LeavingSoonText;
			}
		}
	}

	// Token: 0x06006358 RID: 25432 RVA: 0x00206310 File Offset: 0x00204510
	public void SetPreOrder(bool preorder)
	{
		if (this.m_rewardChest != null && !UniversalInputManager.UsePhoneUI)
		{
			this.m_rewardChest.gameObject.SetActive(!preorder);
		}
		if (this.m_rewardsFrame != null)
		{
			this.m_rewardsFrame.SetActive(!preorder);
		}
		if (this.m_preorderFrame != null)
		{
			this.m_preorderFrame.SetActive(preorder);
		}
	}

	// Token: 0x06006359 RID: 25433 RVA: 0x00206382 File Offset: 0x00204582
	private void OnLeavingSoonButtonClicked()
	{
		DialogManager.Get().ShowPopup(new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_STORE_ADVENTURE_LEAVING_SOON"),
			m_text = this.m_leavingSoonInfoText,
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		});
	}

	// Token: 0x04005265 RID: 21093
	public PegUIElement m_rewardChest;

	// Token: 0x04005266 RID: 21094
	public GameObject m_rewardsFrame;

	// Token: 0x04005267 RID: 21095
	public GameObject m_preorderFrame;

	// Token: 0x04005268 RID: 21096
	public GameObject m_leavingSoonBanner;

	// Token: 0x04005269 RID: 21097
	public UIBButton m_leavingSoonButton;

	// Token: 0x0400526A RID: 21098
	public MeshRenderer m_logo;

	// Token: 0x0400526B RID: 21099
	public MeshRenderer m_keyArt;

	// Token: 0x0400526C RID: 21100
	private string m_leavingSoonInfoText;

	// Token: 0x0400526D RID: 21101
	private AssetHandle<Texture> m_logoTexture;
}
