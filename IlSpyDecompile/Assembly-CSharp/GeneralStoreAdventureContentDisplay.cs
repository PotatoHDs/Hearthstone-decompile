using Blizzard.T5.AssetManager;
using UnityEngine;

public class GeneralStoreAdventureContentDisplay : MonoBehaviour
{
	public PegUIElement m_rewardChest;

	public GameObject m_rewardsFrame;

	public GameObject m_preorderFrame;

	public GameObject m_leavingSoonBanner;

	public UIBButton m_leavingSoonButton;

	public MeshRenderer m_logo;

	public MeshRenderer m_keyArt;

	private string m_leavingSoonInfoText;

	private AssetHandle<Texture> m_logoTexture;

	private void Awake()
	{
		if (m_leavingSoonButton != null)
		{
			m_leavingSoonButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnLeavingSoonButtonClicked();
			});
		}
	}

	private void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_logoTexture);
	}

	public void UpdateAdventureType(StoreAdventureDef advDef, AdventureDbfRecord advRecord)
	{
		if (advDef == null)
		{
			return;
		}
		AssetLoader.Get().LoadAsset(advDef.m_logoTextureName, delegate(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object data)
		{
			if (!loadedTexture)
			{
				Debug.LogError($"Failed to load texture {assetRef}!");
			}
			else
			{
				AssetHandle.Take(ref m_logoTexture, loadedTexture);
				m_logo.GetMaterial().mainTexture = m_logoTexture;
			}
		});
		m_keyArt.SetMaterial(advDef.m_keyArt);
		if (m_leavingSoonBanner != null)
		{
			m_leavingSoonBanner.SetActive(advRecord.LeavingSoon);
			if (advRecord.LeavingSoon)
			{
				m_leavingSoonInfoText = advRecord.LeavingSoonText;
			}
		}
	}

	public void SetPreOrder(bool preorder)
	{
		if (m_rewardChest != null && !UniversalInputManager.UsePhoneUI)
		{
			m_rewardChest.gameObject.SetActive(!preorder);
		}
		if (m_rewardsFrame != null)
		{
			m_rewardsFrame.SetActive(!preorder);
		}
		if (m_preorderFrame != null)
		{
			m_preorderFrame.SetActive(preorder);
		}
	}

	private void OnLeavingSoonButtonClicked()
	{
		DialogManager.Get().ShowPopup(new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_STORE_ADVENTURE_LEAVING_SOON"),
			m_text = m_leavingSoonInfoText,
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		});
	}
}
