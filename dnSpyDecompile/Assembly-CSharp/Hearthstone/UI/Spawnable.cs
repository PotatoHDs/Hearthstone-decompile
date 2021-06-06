using System;
using System.Collections.Generic;
using System.Diagnostics;
using Blizzard.T5.AssetManager;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001005 RID: 4101
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class Spawnable : WidgetBehavior, IPopupRendering, ILayerOverridable, IVisibleWidgetComponent
	{
		// Token: 0x0600B258 RID: 45656 RVA: 0x0036F3EC File Offset: 0x0036D5EC
		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			this.m_isPopupRenderingEnabled = true;
			if (this.m_isVisible)
			{
				this.SetLayerOverride(GameLayer.Reserved29);
			}
			if (this.m_renderer != null)
			{
				if (this.m_popupRenderer == null)
				{
					this.m_popupRenderer = this.m_renderer.gameObject.AddComponent<PopupRenderer>();
				}
				this.m_popupRenderer.EnablePopupRendering(popupRoot);
			}
		}

		// Token: 0x0600B259 RID: 45657 RVA: 0x0036F44E File Offset: 0x0036D64E
		public void DisablePopupRendering()
		{
			this.m_isPopupRenderingEnabled = false;
			if (this.m_isVisible)
			{
				this.ClearLayerOverride();
			}
			if (this.m_popupRenderer != null)
			{
				this.m_popupRenderer.DisablePopupRendering();
			}
		}

		// Token: 0x0600B25A RID: 45658 RVA: 0x000052EC File Offset: 0x000034EC
		public bool ShouldPropagatePopupRendering()
		{
			return true;
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600B25B RID: 45659 RVA: 0x0036DAA4 File Offset: 0x0036BCA4
		public bool IsDesiredHidden
		{
			get
			{
				return base.Owner.IsDesiredHidden;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600B25C RID: 45660 RVA: 0x0036E342 File Offset: 0x0036C542
		public bool IsDesiredHiddenInHierarchy
		{
			get
			{
				return base.Owner.IsDesiredHiddenInHierarchy;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600B25D RID: 45661 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool HandlesChildVisibility
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B25E RID: 45662 RVA: 0x0036F480 File Offset: 0x0036D680
		public void SetVisibility(bool isVisible, bool isInternal)
		{
			this.m_isVisible = isVisible;
			if (!isVisible)
			{
				if (isInternal)
				{
					this.m_originalLayer = new GameLayer?((GameLayer)base.gameObject.layer);
					base.gameObject.layer = 28;
					if (this.m_widget != null)
					{
						this.m_widget.SetLayerOverride((GameLayer)base.gameObject.layer);
						return;
					}
					if (this.m_renderer != null)
					{
						this.m_renderer.gameObject.layer = base.gameObject.layer;
						return;
					}
				}
				else
				{
					this.SetLayerOverride(GameLayer.InvisibleRender);
				}
				return;
			}
			if (this.m_isPopupRenderingEnabled)
			{
				this.SetLayerOverride(GameLayer.Reserved29);
				return;
			}
			if (isInternal && (this.m_overrideLayer != null || this.m_originalLayer != null))
			{
				GameObject gameObject = base.gameObject;
				GameLayer? overrideLayer = this.m_overrideLayer;
				gameObject.layer = (int)((overrideLayer != null) ? overrideLayer : this.m_originalLayer).Value;
				if (this.m_widget != null)
				{
					this.m_widget.SetLayerOverride((GameLayer)base.gameObject.layer);
				}
				else if (this.m_renderer != null)
				{
					this.m_renderer.gameObject.layer = base.gameObject.layer;
				}
				this.m_overrideLayer = null;
				this.m_originalLayer = null;
				return;
			}
			this.ClearLayerOverride();
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x0600B25F RID: 45663 RVA: 0x0036F5E6 File Offset: 0x0036D7E6
		// (set) Token: 0x0600B260 RID: 45664 RVA: 0x0036F5EE File Offset: 0x0036D7EE
		[Overridable]
		public bool ItemShouldUseScript
		{
			get
			{
				return this.m_hasOverride;
			}
			set
			{
				this.m_hasOverride = !value;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (set) Token: 0x0600B261 RID: 45665 RVA: 0x0036F5FA File Offset: 0x0036D7FA
		[Overridable]
		public int ItemID
		{
			set
			{
				this.m_overrideValue = value;
				this.CreateItemByID(value);
				this.m_hasOverride = true;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (set) Token: 0x0600B262 RID: 45666 RVA: 0x0036F616 File Offset: 0x0036D816
		[Overridable]
		public string ItemName
		{
			set
			{
				this.m_overrideValue = value;
				this.CreateItemByName(value);
				this.m_hasOverride = true;
			}
		}

		// Token: 0x0600B263 RID: 45667 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected override void OnInitialize()
		{
		}

		// Token: 0x0600B264 RID: 45668 RVA: 0x0036F630 File Offset: 0x0036D830
		public override void OnUpdate()
		{
			int localDataVersion = base.GetLocalDataVersion();
			if (this.m_lastDataVerion != localDataVersion)
			{
				this.HandleDataChanged();
				this.m_lastDataVerion = localDataVersion;
			}
		}

		// Token: 0x0600B265 RID: 45669 RVA: 0x0036F65C File Offset: 0x0036D85C
		public override bool TryIncrementDataVersion(int id)
		{
			HashSet<int> dataModelIDs = this.m_valueScript.GetDataModelIDs();
			if (dataModelIDs == null || !dataModelIDs.Contains(id))
			{
				return false;
			}
			base.IncrementLocalDataVersion();
			return true;
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x0600B266 RID: 45670 RVA: 0x0036F68C File Offset: 0x0036D88C
		public override bool IsChangingStates
		{
			get
			{
				return this.m_isLoading;
			}
		}

		// Token: 0x0600B267 RID: 45671 RVA: 0x0036F694 File Offset: 0x0036D894
		private void HandleDataChanged()
		{
			if (this.m_hasOverride)
			{
				return;
			}
			ScriptContext.EvaluationResults evaluationResults = new ScriptContext().Evaluate(this.m_valueScript.Script, this);
			if (evaluationResults.ErrorCode == ScriptContext.ErrorCodes.Success)
			{
				this.HandleItemChanged(evaluationResults.Value);
			}
		}

		// Token: 0x0600B268 RID: 45672 RVA: 0x0036F6D5 File Offset: 0x0036D8D5
		private void HandleOverrideChange()
		{
			if (!this.m_hasOverride || this.m_overrideValue == null)
			{
				this.m_hasOverride = false;
				return;
			}
			this.HandleItemChanged(this.m_overrideValue);
		}

		// Token: 0x0600B269 RID: 45673 RVA: 0x0036F6FB File Offset: 0x0036D8FB
		private void HandleItemChanged(object value)
		{
			if (value is string)
			{
				this.CreateItemByName((string)value);
				return;
			}
			if (value is IConvertible)
			{
				this.CreateItemByID(Convert.ToInt32(value));
			}
		}

		// Token: 0x0600B26A RID: 45674 RVA: 0x0036F728 File Offset: 0x0036D928
		private void HandleTextureItemLoaded()
		{
			this.m_material.mainTexture = this.m_texture;
			this.m_renderer.SetMaterial(this.m_material);
			this.m_renderer.enabled = true;
			this.m_failedAssetLoadCount = 0;
			this.m_startedAssetLoadCount = 0;
			this.m_isLoading = false;
			base.HandleDoneChangingStates();
		}

		// Token: 0x0600B26B RID: 45675 RVA: 0x0036F780 File Offset: 0x0036D980
		private void HandleTextureItemCleanUp()
		{
			this.m_material = null;
			AssetHandle.SafeDispose<Material>(ref this.m_materialHandle);
			this.m_texture = null;
			AssetHandle.SafeDispose<Texture2D>(ref this.m_textureHandle);
			if (this.m_renderer != null)
			{
				this.HandleGameObjectCleanUp(this.m_renderer.gameObject);
				this.m_renderer = null;
				this.m_popupRenderer = null;
			}
			this.m_failedAssetLoadCount = 0;
			this.m_startedAssetLoadCount = 0;
		}

		// Token: 0x0600B26C RID: 45676 RVA: 0x0036F7EC File Offset: 0x0036D9EC
		private void HandleWidgetItemCleanUp()
		{
			if (this.m_widget == null)
			{
				return;
			}
			base.Owner.RemoveNestedInstance(this.m_widget);
			this.HandleGameObjectCleanUp(this.m_widget.gameObject);
			this.m_widget = null;
		}

		// Token: 0x0600B26D RID: 45677 RVA: 0x000A6C55 File Offset: 0x000A4E55
		private void HandleGameObjectCleanUp(GameObject go)
		{
			UnityEngine.Object.Destroy(go);
		}

		// Token: 0x0600B26E RID: 45678 RVA: 0x0036F828 File Offset: 0x0036DA28
		private void HandleTextureItemError<T>(AssetHandle<T> assetHandle) where T : UnityEngine.Object
		{
			AssetHandle.SafeDispose<T>(ref assetHandle);
			this.m_failedAssetLoadCount++;
			if (this.m_failedAssetLoadCount >= this.m_startedAssetLoadCount)
			{
				this.m_isLoading = false;
				UnityEngine.Debug.LogErrorFormat("Failed to load texture icon for {0} in {1}!", new object[]
				{
					base.name,
					base.Owner.name
				});
				this.HandleTextureItemCleanUp();
				base.HandleDoneChangingStates();
			}
		}

		// Token: 0x0600B26F RID: 45679 RVA: 0x0036F894 File Offset: 0x0036DA94
		private void CreateItemByName(string name)
		{
			if (string.IsNullOrEmpty(name) || this.m_spawnableLibrary == null)
			{
				return;
			}
			SpawnableLibrary.ItemData itemDataByName = this.m_spawnableLibrary.GetItemDataByName(name);
			if (itemDataByName == null)
			{
				return;
			}
			this.CreateItem(itemDataByName);
		}

		// Token: 0x0600B270 RID: 45680 RVA: 0x0036F8D0 File Offset: 0x0036DAD0
		private void CreateItemByID(int id)
		{
			if (string.IsNullOrEmpty(base.name) || this.m_spawnableLibrary == null)
			{
				return;
			}
			SpawnableLibrary.ItemData itemDataByID = this.m_spawnableLibrary.GetItemDataByID(id);
			if (itemDataByID == null)
			{
				return;
			}
			this.CreateItem(itemDataByID);
		}

		// Token: 0x0600B271 RID: 45681 RVA: 0x0036F914 File Offset: 0x0036DB14
		private void CreateItem(SpawnableLibrary.ItemData itemData)
		{
			if (string.IsNullOrEmpty(itemData.Reference) || itemData == this.m_itemData)
			{
				return;
			}
			this.m_itemData = itemData;
			SpawnableLibrary.ItemType itemType = itemData.ItemType;
			if (itemType == SpawnableLibrary.ItemType.Texture)
			{
				this.CreateTextureItem(itemData);
				return;
			}
			if (itemType != SpawnableLibrary.ItemType.Widget)
			{
				return;
			}
			this.CreateWidgetItem(itemData);
		}

		// Token: 0x0600B272 RID: 45682 RVA: 0x0036F960 File Offset: 0x0036DB60
		private void CreateWidgetItem(SpawnableLibrary.ItemData itemData)
		{
			this.HandleWidgetItemCleanUp();
			this.HandleTextureItemCleanUp();
			this.m_isLoading = true;
			WidgetInstance widgetInstance = WidgetInstance.Create(itemData.Reference, false);
			widgetInstance.transform.SetParent(base.transform, false);
			widgetInstance.transform.localPosition = Vector3.zero;
			widgetInstance.transform.localRotation = Quaternion.identity;
			widgetInstance.transform.localScale = Vector3.one;
			widgetInstance.SetLayerOverride((GameLayer)base.gameObject.layer);
			widgetInstance.gameObject.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			widgetInstance.name = string.Format("Spawnable Library Item: {0}", itemData.Name);
			base.Owner.AddNestedInstance(widgetInstance, base.gameObject);
			base.HandleStartChangingStates();
			widgetInstance.RegisterDoneChangingStatesListener(delegate(object _)
			{
				this.m_isLoading = false;
				base.HandleDoneChangingStates();
			}, null, true, true);
			this.m_widget = widgetInstance;
		}

		// Token: 0x0600B273 RID: 45683 RVA: 0x0036FA38 File Offset: 0x0036DC38
		private void CreateTextureItem(SpawnableLibrary.ItemData itemData)
		{
			this.m_isLoading = true;
			base.HandleStartChangingStates();
			this.HandleWidgetItemCleanUp();
			this.HandleTextureItemCleanUp();
			if (this.m_spawnableLibrary == null || string.IsNullOrEmpty(this.m_spawnableLibrary.BaseMaterial))
			{
				return;
			}
			this.CreateRenderer(itemData.Name);
			this.m_startedAssetLoadCount++;
			this.m_textureAsyncOperationId++;
			AssetLoader.Get().LoadAsset<Texture2D>(itemData.Reference, delegate(AssetReference assetRef, AssetHandle<Texture2D> assetHandle, object asyncOperationId)
			{
				if (assetHandle == null)
				{
					this.HandleTextureItemError<Texture2D>(assetHandle);
					return;
				}
				if (this.m_textureAsyncOperationId != (int)asyncOperationId)
				{
					AssetHandle.SafeDispose<Texture2D>(ref assetHandle);
					return;
				}
				this.m_texture = assetHandle.Asset;
				this.m_textureHandle = assetHandle;
				if (this.m_material != null)
				{
					this.HandleTextureItemLoaded();
				}
			}, this.m_textureAsyncOperationId, AssetLoadingOptions.DisableLocalization);
			this.m_startedAssetLoadCount++;
			this.m_materialAsyncOperationId++;
			AssetLoader.Get().LoadAsset<Material>(this.m_spawnableLibrary.BaseMaterial, delegate(AssetReference assetRef, AssetHandle<Material> assetHandle, object asyncOperationId)
			{
				if (assetHandle == null)
				{
					this.HandleTextureItemError<Material>(assetHandle);
					return;
				}
				if (this.m_materialAsyncOperationId != (int)asyncOperationId)
				{
					AssetHandle.SafeDispose<Material>(ref assetHandle);
					return;
				}
				this.m_material = new Material(assetHandle.Asset);
				this.m_material.name = "?" + this.m_material.name;
				this.m_materialHandle = assetHandle;
				if (this.m_texture != null)
				{
					this.HandleTextureItemLoaded();
				}
			}, this.m_materialAsyncOperationId, AssetLoadingOptions.DisableLocalization);
		}

		// Token: 0x0600B274 RID: 45684 RVA: 0x0036FB24 File Offset: 0x0036DD24
		private void CreateRenderer(string itemName)
		{
			WidgetTransform component = base.GetComponent<WidgetTransform>();
			WidgetTransform.FacingDirection facing = (component != null) ? component.Facing : WidgetTransform.FacingDirection.YPositive;
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
			gameObject.transform.SetParent(base.transform, false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = WidgetTransform.GetRotationFromZNegativeToDesiredFacing(facing);
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = base.gameObject.layer;
			gameObject.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			this.m_renderer = gameObject.GetComponent<MeshRenderer>();
			this.m_renderer.gameObject.name = string.Format("Spawnable Library Item: {0}", itemName);
			this.m_renderer.enabled = false;
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600B275 RID: 45685 RVA: 0x000052EC File Offset: 0x000034EC
		public bool HandlesChildLayers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B276 RID: 45686 RVA: 0x0036FBE4 File Offset: 0x0036DDE4
		public void ClearLayerOverride()
		{
			if (this.m_originalLayer != null)
			{
				base.gameObject.layer = (int)this.m_originalLayer.Value;
				if (this.m_widget != null)
				{
					this.m_widget.SetLayerOverride(this.m_originalLayer.Value);
				}
				else if (this.m_renderer != null)
				{
					this.m_renderer.gameObject.layer = base.gameObject.layer;
				}
				this.m_originalLayer = null;
				this.m_overrideLayer = null;
			}
		}

		// Token: 0x0600B277 RID: 45687 RVA: 0x0036FC7C File Offset: 0x0036DE7C
		public void SetLayerOverride(GameLayer layer)
		{
			this.m_originalLayer = new GameLayer?((GameLayer)base.gameObject.layer);
			this.m_overrideLayer = new GameLayer?(layer);
			base.gameObject.layer = (int)this.m_overrideLayer.Value;
			if (this.m_widget != null)
			{
				this.m_widget.SetLayerOverride(layer);
				return;
			}
			if (this.m_renderer != null)
			{
				this.m_renderer.gameObject.layer = (int)layer;
			}
		}

		// Token: 0x0600B278 RID: 45688 RVA: 0x0036FCFB File Offset: 0x0036DEFB
		protected override void OnDestroy()
		{
			this.HandleTextureItemCleanUp();
			this.m_textureAsyncOperationId++;
			this.m_materialAsyncOperationId++;
			base.OnDestroy();
		}

		// Token: 0x0600B279 RID: 45689 RVA: 0x0036D90B File Offset: 0x0036BB0B
		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}

		// Token: 0x04009608 RID: 38408
		private const string SpawnedItemName = "Spawnable Library Item: {0}";

		// Token: 0x04009609 RID: 38409
		[Tooltip("A script that needs to evaluate to a string or an int.")]
		[SerializeField]
		private ScriptString m_valueScript;

		// Token: 0x0400960A RID: 38410
		[Tooltip("A reference to the spawnable library to load the item from.")]
		[SerializeField]
		private SpawnableLibrary m_spawnableLibrary;

		// Token: 0x0400960B RID: 38411
		private AssetHandle<Material> m_materialHandle;

		// Token: 0x0400960C RID: 38412
		private AssetHandle<Texture2D> m_textureHandle;

		// Token: 0x0400960D RID: 38413
		private Material m_material;

		// Token: 0x0400960E RID: 38414
		private Texture2D m_texture;

		// Token: 0x0400960F RID: 38415
		private GameLayer? m_originalLayer;

		// Token: 0x04009610 RID: 38416
		private GameLayer? m_overrideLayer;

		// Token: 0x04009611 RID: 38417
		private PopupRenderer m_popupRenderer;

		// Token: 0x04009612 RID: 38418
		private Renderer m_renderer;

		// Token: 0x04009613 RID: 38419
		private WidgetInstance m_widget;

		// Token: 0x04009614 RID: 38420
		private SpawnableLibrary.ItemData m_itemData;

		// Token: 0x04009615 RID: 38421
		private int m_materialAsyncOperationId;

		// Token: 0x04009616 RID: 38422
		private int m_textureAsyncOperationId;

		// Token: 0x04009617 RID: 38423
		private int m_lastDataVerion;

		// Token: 0x04009618 RID: 38424
		private int m_startedAssetLoadCount;

		// Token: 0x04009619 RID: 38425
		private int m_failedAssetLoadCount;

		// Token: 0x0400961A RID: 38426
		private bool m_hasOverride;

		// Token: 0x0400961B RID: 38427
		private object m_overrideValue;

		// Token: 0x0400961C RID: 38428
		private bool m_isLoading;

		// Token: 0x0400961D RID: 38429
		private bool m_isPopupRenderingEnabled;

		// Token: 0x0400961E RID: 38430
		private bool m_isVisible;
	}
}
