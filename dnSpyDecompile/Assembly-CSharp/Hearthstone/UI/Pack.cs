using System;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FDF RID: 4063
	[AddComponentMenu("")]
	[WidgetBehaviorDescription(Path = "Hearthstone/Pack", UniqueWithinCategory = "asset")]
	public class Pack : CustomWidgetBehavior
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600B0D8 RID: 45272 RVA: 0x0036A109 File Offset: 0x00368309
		// (set) Token: 0x0600B0D9 RID: 45273 RVA: 0x0036A111 File Offset: 0x00368311
		[Overridable]
		public bool HideBanner
		{
			get
			{
				return this.m_hideBanner;
			}
			set
			{
				this.m_hideBanner = value;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x0600B0DA RID: 45274 RVA: 0x0036A11A File Offset: 0x0036831A
		// (set) Token: 0x0600B0DB RID: 45275 RVA: 0x0036A137 File Offset: 0x00368337
		[Overridable]
		public bool ShowShadow
		{
			get
			{
				return this.m_animPack != null && this.m_animPack.IsShowingShadow;
			}
			set
			{
				if (this.m_animPack != null && this.m_animPack.IsShowingShadow != value)
				{
					this.m_showShadow = value;
					this.m_animPack.IsShowingShadow = value;
				}
			}
		}

		// Token: 0x0600B0DC RID: 45276 RVA: 0x0036A168 File Offset: 0x00368368
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.m_displayedPack = BoosterDbId.INVALID;
			this.m_displayedPackCount = 0;
			this.m_isBannerDisplayed = false;
			IJobDependency[] dependencies;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, new Type[]
			{
				typeof(IAssetLoader),
				typeof(WidgetRunner)
			});
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("Pack.CreatePreviewableObject", new Action(this.CreatePreviewableObject), JobFlags.StartImmediately, dependencies));
		}

		// Token: 0x0600B0DD RID: 45277 RVA: 0x0036A1D6 File Offset: 0x003683D6
		private void CreatePreviewableObject()
		{
			base.CreatePreviewableObject(delegate(CustomWidgetBehavior.IPreviewableObject previewable, Action<GameObject> callback)
			{
				this.m_displayedPack = BoosterDbId.INVALID;
				this.m_displayedPackCount = 0;
				if (AssetLoader.Get() == null)
				{
					Debug.LogWarning("Hearthstone.UI.Pack.OnInitialize() - AssetLoader not available");
					callback(null);
					return;
				}
				int count;
				this.GetDesiredPackAndCount(out this.m_displayedPack, out count);
				if (this.m_displayedPack == BoosterDbId.INVALID)
				{
					callback(null);
					return;
				}
				string input = null;
				using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStorePackPrefab(this.m_displayedPack))
				{
					StorePackDef storePackDef = assetHandle ? assetHandle.Asset.GetComponent<StorePackDef>() : null;
					if (storePackDef == null)
					{
						callback(null);
						return;
					}
					input = storePackDef.GetLowPolyPrefab();
				}
				AssetHandle<GameObject> assetHandle2 = AssetLoader.Get().LoadAsset<GameObject>(input, AssetLoadingOptions.None);
				if (!assetHandle2)
				{
					callback(null);
					return;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(assetHandle2.Asset, base.transform);
				DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
				if (disposablesCleaner != null)
				{
					disposablesCleaner.Attach(gameObject, assetHandle2);
				}
				gameObject.transform.SetParent(base.transform, false);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				this.UpdateBanner(count);
				this.m_animPack = base.GetComponentInChildren<AnimatedLowPolyPack>(true);
				if (!this.m_animPack)
				{
					Debug.LogError("Pack is missing a required child component of type AnimatedLowPolyPack");
				}
				callback(gameObject);
			}, delegate(CustomWidgetBehavior.IPreviewableObject o)
			{
				BoosterDbId boosterDbId;
				int num;
				this.GetDesiredPackAndCount(out boosterDbId, out num);
				bool flag = this.ShouldDisplayBanner(num);
				return this.m_displayedPack != boosterDbId || this.m_isBannerDisplayed != flag || (flag && this.m_displayedPackCount != num);
			}, null);
		}

		// Token: 0x0600B0DE RID: 45278 RVA: 0x0036A1F8 File Offset: 0x003683F8
		private void GetDesiredPackAndCount(out BoosterDbId boosterType, out int count)
		{
			boosterType = this.m_defaultPack;
			count = this.m_bannerCount;
			if (this.m_useDataModel)
			{
				IDataModel dataModel;
				if (base.GetDataModel(25, out dataModel))
				{
					PackDataModel packDataModel = (PackDataModel)dataModel;
					boosterType = packDataModel.Type;
					count = packDataModel.Quantity;
					return;
				}
				if (Application.isPlaying)
				{
					boosterType = BoosterDbId.INVALID;
				}
			}
		}

		// Token: 0x0600B0DF RID: 45279 RVA: 0x0036A24B File Offset: 0x0036844B
		private bool ShouldDisplayBanner(int count)
		{
			return count > 1 && !this.m_hideBanner;
		}

		// Token: 0x0600B0E0 RID: 45280 RVA: 0x0036A25C File Offset: 0x0036845C
		private void UpdateBanner(int count)
		{
			this.m_isBannerDisplayed = false;
			this.m_displayedPackCount = count;
			AnimatedLowPolyPack componentInChildren = base.GetComponentInChildren<AnimatedLowPolyPack>(true);
			if (componentInChildren != null)
			{
				if (this.ShouldDisplayBanner(count))
				{
					this.m_isBannerDisplayed = true;
					componentInChildren.UpdateBannerCountImmediately(this.m_displayedPackCount);
					return;
				}
				componentInChildren.HideBanner();
			}
		}

		// Token: 0x0400956F RID: 38255
		[Tooltip("This is the pack displayed by default. INVALID means nothing will be displayed.")]
		[SerializeField]
		private BoosterDbId m_defaultPack = BoosterDbId.CLASSIC;

		// Token: 0x04009570 RID: 38256
		[Tooltip("If true, it will use data model 'pack' whenever bound.")]
		[SerializeField]
		private bool m_useDataModel = true;

		// Token: 0x04009571 RID: 38257
		[SerializeField]
		private int m_bannerCount;

		// Token: 0x04009572 RID: 38258
		[Tooltip("If true, the count banner will be hidden, even if count is greater than 1.")]
		[SerializeField]
		private bool m_hideBanner;

		// Token: 0x04009573 RID: 38259
		[Tooltip("If true, the shadow will be shown.")]
		[SerializeField]
		private bool m_showShadow;

		// Token: 0x04009574 RID: 38260
		private BoosterDbId m_displayedPack;

		// Token: 0x04009575 RID: 38261
		private int m_displayedPackCount;

		// Token: 0x04009576 RID: 38262
		private bool m_isBannerDisplayed;

		// Token: 0x04009577 RID: 38263
		private AnimatedLowPolyPack m_animPack;
	}
}
