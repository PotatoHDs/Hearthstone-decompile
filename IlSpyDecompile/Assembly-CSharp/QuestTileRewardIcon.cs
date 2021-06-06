using Blizzard.T5.AssetManager;
using UnityEngine;

public class QuestTileRewardIcon : MonoBehaviour
{
	public UberText m_amountText;

	public NestedPrefab m_goldDoubledFX;

	public NestedPrefab m_goldDoubledFXPhone;

	private RewardData m_rewardData;

	private AssetHandle<Texture> m_loadedTexture;

	private NestedPrefab GoldDoubledFX
	{
		get
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				return m_goldDoubledFX;
			}
			return m_goldDoubledFXPhone;
		}
	}

	private void OnDestroy()
	{
		AssetHandle.SafeDispose(ref m_loadedTexture);
	}

	public void InitWithRewardData(RewardData rewardData, bool isDoubleGoldEnabled, int renderQueue)
	{
		m_rewardData = rewardData;
		SceneUtils.SetRenderQueue(base.gameObject, renderQueue);
		if (isDoubleGoldEnabled && GoldDoubledFX != null && m_rewardData.RewardType == Reward.Type.GOLD)
		{
			GameObject gameObject = GoldDoubledFX.PrefabGameObject(instantiateIfNeeded: true);
			if (gameObject != null)
			{
				SetDoubleGoldActive(active: true);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
			}
		}
		m_amountText.gameObject.SetActive(value: false);
		m_amountText.RenderQueue = renderQueue;
		RewardUtils.SetupRewardIcon(m_rewardData, GetComponent<Renderer>(), m_amountText, out var amountToScaleReward, isDoubleGoldEnabled);
		SetAmountTextOffset();
		base.transform.localScale *= amountToScaleReward;
	}

	public void InitWithIconParams(int renderQueue, AssetReference iconTextureAssetRef, Vector2 iconTextureSourceOffset, string amountText)
	{
		m_rewardData = new EventRewardData();
		SceneUtils.SetRenderQueue(base.gameObject, renderQueue);
		m_amountText.RenderQueue = renderQueue;
		Material tileMaterial = GetComponent<Renderer>().GetMaterial();
		AssetHandleCallback<Texture> callback = delegate(AssetReference assetRef, AssetHandle<Texture> texture, object loadTextureCbData)
		{
			AssetHandle.Take(ref m_loadedTexture, texture);
			if (tileMaterial != null)
			{
				tileMaterial.mainTexture = m_loadedTexture;
			}
		};
		AssetLoader.Get().LoadAsset(iconTextureAssetRef, callback);
		tileMaterial.mainTextureOffset = iconTextureSourceOffset;
		if (amountText != null)
		{
			m_amountText.Text = amountText;
			m_amountText.gameObject.SetActive(value: true);
		}
		else
		{
			m_amountText.gameObject.SetActive(value: false);
		}
	}

	public void OnClose()
	{
		SetDoubleGoldActive(active: false);
	}

	public void OnQuestRerolled()
	{
		SetDoubleGoldActive(active: false);
	}

	private void SetDoubleGoldActive(bool active)
	{
		if (GoldDoubledFX != null)
		{
			GoldDoubledFX.gameObject.SetActive(active);
		}
	}

	private void SetAmountTextOffset()
	{
		switch (m_rewardData.RewardType)
		{
		case Reward.Type.ARCANE_DUST:
			TransformUtil.SetLocalPosX(m_amountText, m_amountText.transform.localPosition.z + 0.15f);
			TransformUtil.SetLocalPosZ(m_amountText, m_amountText.transform.localPosition.z + 0.7f);
			break;
		case Reward.Type.GOLD:
			TransformUtil.SetLocalPosZ(m_amountText, m_amountText.transform.localPosition.z + 0.7f);
			break;
		}
	}
}
