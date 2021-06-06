using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000638 RID: 1592
public class QuestTileRewardIcon : MonoBehaviour
{
	// Token: 0x17000547 RID: 1351
	// (get) Token: 0x060059A0 RID: 22944 RVA: 0x001D3BBB File Offset: 0x001D1DBB
	private NestedPrefab GoldDoubledFX
	{
		get
		{
			if (!UniversalInputManager.UsePhoneUI)
			{
				return this.m_goldDoubledFX;
			}
			return this.m_goldDoubledFXPhone;
		}
	}

	// Token: 0x060059A1 RID: 22945 RVA: 0x001D3BD6 File Offset: 0x001D1DD6
	private void OnDestroy()
	{
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedTexture);
	}

	// Token: 0x060059A2 RID: 22946 RVA: 0x001D3BE4 File Offset: 0x001D1DE4
	public void InitWithRewardData(RewardData rewardData, bool isDoubleGoldEnabled, int renderQueue)
	{
		this.m_rewardData = rewardData;
		SceneUtils.SetRenderQueue(base.gameObject, renderQueue, false);
		if (isDoubleGoldEnabled && this.GoldDoubledFX != null && this.m_rewardData.RewardType == Reward.Type.GOLD)
		{
			GameObject gameObject = this.GoldDoubledFX.PrefabGameObject(true);
			if (gameObject != null)
			{
				this.SetDoubleGoldActive(true);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
			}
		}
		this.m_amountText.gameObject.SetActive(false);
		this.m_amountText.RenderQueue = renderQueue;
		float d;
		RewardUtils.SetupRewardIcon(this.m_rewardData, base.GetComponent<Renderer>(), this.m_amountText, out d, isDoubleGoldEnabled);
		this.SetAmountTextOffset();
		base.transform.localScale *= d;
	}

	// Token: 0x060059A3 RID: 22947 RVA: 0x001D3CC8 File Offset: 0x001D1EC8
	public void InitWithIconParams(int renderQueue, AssetReference iconTextureAssetRef, Vector2 iconTextureSourceOffset, string amountText)
	{
		this.m_rewardData = new EventRewardData();
		SceneUtils.SetRenderQueue(base.gameObject, renderQueue, false);
		this.m_amountText.RenderQueue = renderQueue;
		Material tileMaterial = base.GetComponent<Renderer>().GetMaterial();
		AssetHandleCallback<Texture> callback = delegate(AssetReference assetRef, AssetHandle<Texture> texture, object loadTextureCbData)
		{
			AssetHandle.Take<Texture>(ref this.m_loadedTexture, texture);
			if (tileMaterial != null)
			{
				tileMaterial.mainTexture = this.m_loadedTexture;
			}
		};
		AssetLoader.Get().LoadAsset<Texture>(iconTextureAssetRef, callback, null, AssetLoadingOptions.None);
		tileMaterial.mainTextureOffset = iconTextureSourceOffset;
		if (amountText != null)
		{
			this.m_amountText.Text = amountText;
			this.m_amountText.gameObject.SetActive(true);
			return;
		}
		this.m_amountText.gameObject.SetActive(false);
	}

	// Token: 0x060059A4 RID: 22948 RVA: 0x001D3D71 File Offset: 0x001D1F71
	public void OnClose()
	{
		this.SetDoubleGoldActive(false);
	}

	// Token: 0x060059A5 RID: 22949 RVA: 0x001D3D71 File Offset: 0x001D1F71
	public void OnQuestRerolled()
	{
		this.SetDoubleGoldActive(false);
	}

	// Token: 0x060059A6 RID: 22950 RVA: 0x001D3D7A File Offset: 0x001D1F7A
	private void SetDoubleGoldActive(bool active)
	{
		if (this.GoldDoubledFX != null)
		{
			this.GoldDoubledFX.gameObject.SetActive(active);
		}
	}

	// Token: 0x060059A7 RID: 22951 RVA: 0x001D3D9C File Offset: 0x001D1F9C
	private void SetAmountTextOffset()
	{
		Reward.Type rewardType = this.m_rewardData.RewardType;
		if (rewardType == Reward.Type.ARCANE_DUST)
		{
			TransformUtil.SetLocalPosX(this.m_amountText, this.m_amountText.transform.localPosition.z + 0.15f);
			TransformUtil.SetLocalPosZ(this.m_amountText, this.m_amountText.transform.localPosition.z + 0.7f);
			return;
		}
		if (rewardType != Reward.Type.GOLD)
		{
			return;
		}
		TransformUtil.SetLocalPosZ(this.m_amountText, this.m_amountText.transform.localPosition.z + 0.7f);
	}

	// Token: 0x04004CAE RID: 19630
	public UberText m_amountText;

	// Token: 0x04004CAF RID: 19631
	public NestedPrefab m_goldDoubledFX;

	// Token: 0x04004CB0 RID: 19632
	public NestedPrefab m_goldDoubledFXPhone;

	// Token: 0x04004CB1 RID: 19633
	private RewardData m_rewardData;

	// Token: 0x04004CB2 RID: 19634
	private AssetHandle<Texture> m_loadedTexture;
}
