using System;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Pack", UniqueWithinCategory = "asset")]
	public class Pack : CustomWidgetBehavior
	{
		[Tooltip("This is the pack displayed by default. INVALID means nothing will be displayed.")]
		[SerializeField]
		private BoosterDbId m_defaultPack = BoosterDbId.CLASSIC;

		[Tooltip("If true, it will use data model 'pack' whenever bound.")]
		[SerializeField]
		private bool m_useDataModel = true;

		[SerializeField]
		private int m_bannerCount;

		[Tooltip("If true, the count banner will be hidden, even if count is greater than 1.")]
		[SerializeField]
		private bool m_hideBanner;

		[Tooltip("If true, the shadow will be shown.")]
		[SerializeField]
		private bool m_showShadow;

		private BoosterDbId m_displayedPack;

		private int m_displayedPackCount;

		private bool m_isBannerDisplayed;

		private AnimatedLowPolyPack m_animPack;

		[Overridable]
		public bool HideBanner
		{
			get
			{
				return m_hideBanner;
			}
			set
			{
				m_hideBanner = value;
			}
		}

		[Overridable]
		public bool ShowShadow
		{
			get
			{
				if (m_animPack != null)
				{
					return m_animPack.IsShowingShadow;
				}
				return false;
			}
			set
			{
				if (m_animPack != null && m_animPack.IsShowingShadow != value)
				{
					m_showShadow = value;
					m_animPack.IsShowingShadow = value;
				}
			}
		}

		protected override void OnInitialize()
		{
			base.OnInitialize();
			m_displayedPack = BoosterDbId.INVALID;
			m_displayedPackCount = 0;
			m_isBannerDisplayed = false;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out var serviceDependencies, typeof(IAssetLoader), typeof(WidgetRunner));
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("Pack.CreatePreviewableObject", CreatePreviewableObject, JobFlags.StartImmediately, serviceDependencies));
		}

		private void CreatePreviewableObject()
		{
			CreatePreviewableObject(delegate(IPreviewableObject previewable, Action<GameObject> callback)
			{
				m_displayedPack = BoosterDbId.INVALID;
				m_displayedPackCount = 0;
				if (AssetLoader.Get() == null)
				{
					Debug.LogWarning("Hearthstone.UI.Pack.OnInitialize() - AssetLoader not available");
					callback(null);
				}
				else
				{
					GetDesiredPackAndCount(out m_displayedPack, out var count2);
					if (m_displayedPack == BoosterDbId.INVALID)
					{
						callback(null);
					}
					else
					{
						string text = null;
						using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStorePackPrefab(m_displayedPack))
						{
							StorePackDef storePackDef = (assetHandle ? assetHandle.Asset.GetComponent<StorePackDef>() : null);
							if (storePackDef == null)
							{
								callback(null);
								return;
							}
							text = storePackDef.GetLowPolyPrefab();
						}
						AssetHandle<GameObject> assetHandle2 = AssetLoader.Get().LoadAsset<GameObject>(text);
						if (!assetHandle2)
						{
							callback(null);
						}
						else
						{
							GameObject gameObject = UnityEngine.Object.Instantiate(assetHandle2.Asset, base.transform);
							HearthstoneServices.Get<DisposablesCleaner>()?.Attach(gameObject, assetHandle2);
							gameObject.transform.SetParent(base.transform, worldPositionStays: false);
							gameObject.transform.localPosition = Vector3.zero;
							gameObject.transform.localRotation = Quaternion.identity;
							gameObject.transform.localScale = Vector3.one;
							UpdateBanner(count2);
							m_animPack = GetComponentInChildren<AnimatedLowPolyPack>(includeInactive: true);
							if (!m_animPack)
							{
								Debug.LogError("Pack is missing a required child component of type AnimatedLowPolyPack");
							}
							callback(gameObject);
						}
					}
				}
			}, delegate
			{
				GetDesiredPackAndCount(out var boosterType, out var count);
				bool flag = ShouldDisplayBanner(count);
				return m_displayedPack != boosterType || m_isBannerDisplayed != flag || (flag && m_displayedPackCount != count);
			});
		}

		private void GetDesiredPackAndCount(out BoosterDbId boosterType, out int count)
		{
			boosterType = m_defaultPack;
			count = m_bannerCount;
			if (m_useDataModel)
			{
				if (GetDataModel(25, out var dataModel))
				{
					PackDataModel packDataModel = (PackDataModel)dataModel;
					boosterType = packDataModel.Type;
					count = packDataModel.Quantity;
				}
				else if (Application.isPlaying)
				{
					boosterType = BoosterDbId.INVALID;
				}
			}
		}

		private bool ShouldDisplayBanner(int count)
		{
			if (count > 1)
			{
				return !m_hideBanner;
			}
			return false;
		}

		private void UpdateBanner(int count)
		{
			m_isBannerDisplayed = false;
			m_displayedPackCount = count;
			AnimatedLowPolyPack componentInChildren = GetComponentInChildren<AnimatedLowPolyPack>(includeInactive: true);
			if (componentInChildren != null)
			{
				if (ShouldDisplayBanner(count))
				{
					m_isBannerDisplayed = true;
					componentInChildren.UpdateBannerCountImmediately(m_displayedPackCount);
				}
				else
				{
					componentInChildren.HideBanner();
				}
			}
		}
	}
}
