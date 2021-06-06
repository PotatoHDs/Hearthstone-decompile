using System;
using System.Collections.Generic;
using System.Diagnostics;
using Blizzard.T5.AssetManager;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class Spawnable : WidgetBehavior, IPopupRendering, ILayerOverridable, IVisibleWidgetComponent
	{
		private const string SpawnedItemName = "Spawnable Library Item: {0}";

		[Tooltip("A script that needs to evaluate to a string or an int.")]
		[SerializeField]
		private ScriptString m_valueScript;

		[Tooltip("A reference to the spawnable library to load the item from.")]
		[SerializeField]
		private SpawnableLibrary m_spawnableLibrary;

		private AssetHandle<Material> m_materialHandle;

		private AssetHandle<Texture2D> m_textureHandle;

		private Material m_material;

		private Texture2D m_texture;

		private GameLayer? m_originalLayer;

		private GameLayer? m_overrideLayer;

		private PopupRenderer m_popupRenderer;

		private Renderer m_renderer;

		private WidgetInstance m_widget;

		private SpawnableLibrary.ItemData m_itemData;

		private int m_materialAsyncOperationId;

		private int m_textureAsyncOperationId;

		private int m_lastDataVerion;

		private int m_startedAssetLoadCount;

		private int m_failedAssetLoadCount;

		private bool m_hasOverride;

		private object m_overrideValue;

		private bool m_isLoading;

		private bool m_isPopupRenderingEnabled;

		private bool m_isVisible;

		public bool IsDesiredHidden => base.Owner.IsDesiredHidden;

		public bool IsDesiredHiddenInHierarchy => base.Owner.IsDesiredHiddenInHierarchy;

		public bool HandlesChildVisibility => false;

		[Overridable]
		public bool ItemShouldUseScript
		{
			get
			{
				return m_hasOverride;
			}
			set
			{
				m_hasOverride = !value;
			}
		}

		[Overridable]
		public int ItemID
		{
			set
			{
				m_overrideValue = value;
				CreateItemByID(value);
				m_hasOverride = true;
			}
		}

		[Overridable]
		public string ItemName
		{
			set
			{
				m_overrideValue = value;
				CreateItemByName(value);
				m_hasOverride = true;
			}
		}

		public override bool IsChangingStates => m_isLoading;

		public bool HandlesChildLayers => true;

		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			m_isPopupRenderingEnabled = true;
			if (m_isVisible)
			{
				SetLayerOverride(GameLayer.Reserved29);
			}
			if (m_renderer != null)
			{
				if (m_popupRenderer == null)
				{
					m_popupRenderer = m_renderer.gameObject.AddComponent<PopupRenderer>();
				}
				m_popupRenderer.EnablePopupRendering(popupRoot);
			}
		}

		public void DisablePopupRendering()
		{
			m_isPopupRenderingEnabled = false;
			if (m_isVisible)
			{
				ClearLayerOverride();
			}
			if (m_popupRenderer != null)
			{
				m_popupRenderer.DisablePopupRendering();
			}
		}

		public bool ShouldPropagatePopupRendering()
		{
			return true;
		}

		public void SetVisibility(bool isVisible, bool isInternal)
		{
			m_isVisible = isVisible;
			if (isVisible)
			{
				if (m_isPopupRenderingEnabled)
				{
					SetLayerOverride(GameLayer.Reserved29);
				}
				else if (isInternal && (m_overrideLayer.HasValue || m_originalLayer.HasValue))
				{
					base.gameObject.layer = (int)(m_overrideLayer ?? m_originalLayer).Value;
					if (m_widget != null)
					{
						m_widget.SetLayerOverride((GameLayer)base.gameObject.layer);
					}
					else if (m_renderer != null)
					{
						m_renderer.gameObject.layer = base.gameObject.layer;
					}
					m_overrideLayer = null;
					m_originalLayer = null;
				}
				else
				{
					ClearLayerOverride();
				}
			}
			else if (isInternal)
			{
				m_originalLayer = (GameLayer)base.gameObject.layer;
				base.gameObject.layer = 28;
				if (m_widget != null)
				{
					m_widget.SetLayerOverride((GameLayer)base.gameObject.layer);
				}
				else if (m_renderer != null)
				{
					m_renderer.gameObject.layer = base.gameObject.layer;
				}
			}
			else
			{
				SetLayerOverride(GameLayer.InvisibleRender);
			}
		}

		protected override void OnInitialize()
		{
		}

		public override void OnUpdate()
		{
			int localDataVersion = GetLocalDataVersion();
			if (m_lastDataVerion != localDataVersion)
			{
				HandleDataChanged();
				m_lastDataVerion = localDataVersion;
			}
		}

		public override bool TryIncrementDataVersion(int id)
		{
			HashSet<int> hashSet = null;
			hashSet = m_valueScript.GetDataModelIDs();
			if (hashSet == null || !hashSet.Contains(id))
			{
				return false;
			}
			IncrementLocalDataVersion();
			return true;
		}

		private void HandleDataChanged()
		{
			if (!m_hasOverride)
			{
				ScriptContext.EvaluationResults evaluationResults = new ScriptContext().Evaluate(m_valueScript.Script, this);
				if (evaluationResults.ErrorCode == ScriptContext.ErrorCodes.Success)
				{
					HandleItemChanged(evaluationResults.Value);
				}
			}
		}

		private void HandleOverrideChange()
		{
			if (!m_hasOverride || m_overrideValue == null)
			{
				m_hasOverride = false;
			}
			else
			{
				HandleItemChanged(m_overrideValue);
			}
		}

		private void HandleItemChanged(object value)
		{
			if (value is string)
			{
				CreateItemByName((string)value);
			}
			else if (value is IConvertible)
			{
				CreateItemByID(Convert.ToInt32(value));
			}
		}

		private void HandleTextureItemLoaded()
		{
			m_material.mainTexture = m_texture;
			m_renderer.SetMaterial(m_material);
			m_renderer.enabled = true;
			m_failedAssetLoadCount = 0;
			m_startedAssetLoadCount = 0;
			m_isLoading = false;
			HandleDoneChangingStates();
		}

		private void HandleTextureItemCleanUp()
		{
			m_material = null;
			AssetHandle.SafeDispose(ref m_materialHandle);
			m_texture = null;
			AssetHandle.SafeDispose(ref m_textureHandle);
			if (m_renderer != null)
			{
				HandleGameObjectCleanUp(m_renderer.gameObject);
				m_renderer = null;
				m_popupRenderer = null;
			}
			m_failedAssetLoadCount = 0;
			m_startedAssetLoadCount = 0;
		}

		private void HandleWidgetItemCleanUp()
		{
			if (!(m_widget == null))
			{
				base.Owner.RemoveNestedInstance(m_widget);
				HandleGameObjectCleanUp(m_widget.gameObject);
				m_widget = null;
			}
		}

		private void HandleGameObjectCleanUp(GameObject go)
		{
			UnityEngine.Object.Destroy(go);
		}

		private void HandleTextureItemError<T>(AssetHandle<T> assetHandle) where T : UnityEngine.Object
		{
			AssetHandle.SafeDispose(ref assetHandle);
			m_failedAssetLoadCount++;
			if (m_failedAssetLoadCount >= m_startedAssetLoadCount)
			{
				m_isLoading = false;
				UnityEngine.Debug.LogErrorFormat("Failed to load texture icon for {0} in {1}!", base.name, base.Owner.name);
				HandleTextureItemCleanUp();
				HandleDoneChangingStates();
			}
		}

		private void CreateItemByName(string name)
		{
			if (!string.IsNullOrEmpty(name) && !(m_spawnableLibrary == null))
			{
				SpawnableLibrary.ItemData itemDataByName = m_spawnableLibrary.GetItemDataByName(name);
				if (itemDataByName != null)
				{
					CreateItem(itemDataByName);
				}
			}
		}

		private void CreateItemByID(int id)
		{
			if (!string.IsNullOrEmpty(base.name) && !(m_spawnableLibrary == null))
			{
				SpawnableLibrary.ItemData itemDataByID = m_spawnableLibrary.GetItemDataByID(id);
				if (itemDataByID != null)
				{
					CreateItem(itemDataByID);
				}
			}
		}

		private void CreateItem(SpawnableLibrary.ItemData itemData)
		{
			if (!string.IsNullOrEmpty(itemData.Reference) && itemData != m_itemData)
			{
				m_itemData = itemData;
				switch (itemData.ItemType)
				{
				case SpawnableLibrary.ItemType.Texture:
					CreateTextureItem(itemData);
					break;
				case SpawnableLibrary.ItemType.Widget:
					CreateWidgetItem(itemData);
					break;
				}
			}
		}

		private void CreateWidgetItem(SpawnableLibrary.ItemData itemData)
		{
			HandleWidgetItemCleanUp();
			HandleTextureItemCleanUp();
			m_isLoading = true;
			WidgetInstance widgetInstance = WidgetInstance.Create(itemData.Reference);
			widgetInstance.transform.SetParent(base.transform, worldPositionStays: false);
			widgetInstance.transform.localPosition = Vector3.zero;
			widgetInstance.transform.localRotation = Quaternion.identity;
			widgetInstance.transform.localScale = Vector3.one;
			widgetInstance.SetLayerOverride((GameLayer)base.gameObject.layer);
			widgetInstance.gameObject.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
			widgetInstance.name = $"Spawnable Library Item: {itemData.Name}";
			base.Owner.AddNestedInstance(widgetInstance, base.gameObject);
			HandleStartChangingStates();
			widgetInstance.RegisterDoneChangingStatesListener(delegate
			{
				m_isLoading = false;
				HandleDoneChangingStates();
			}, null, callImmediatelyIfSet: true, doOnce: true);
			m_widget = widgetInstance;
		}

		private void CreateTextureItem(SpawnableLibrary.ItemData itemData)
		{
			m_isLoading = true;
			HandleStartChangingStates();
			HandleWidgetItemCleanUp();
			HandleTextureItemCleanUp();
			if (m_spawnableLibrary == null || string.IsNullOrEmpty(m_spawnableLibrary.BaseMaterial))
			{
				return;
			}
			CreateRenderer(itemData.Name);
			m_startedAssetLoadCount++;
			m_textureAsyncOperationId++;
			AssetLoader.Get().LoadAsset(itemData.Reference, delegate(AssetReference assetRef, AssetHandle<Texture2D> assetHandle, object asyncOperationId)
			{
				if (assetHandle == null)
				{
					HandleTextureItemError(assetHandle);
				}
				else if (m_textureAsyncOperationId != (int)asyncOperationId)
				{
					AssetHandle.SafeDispose(ref assetHandle);
				}
				else
				{
					m_texture = assetHandle.Asset;
					m_textureHandle = assetHandle;
					if (m_material != null)
					{
						HandleTextureItemLoaded();
					}
				}
			}, m_textureAsyncOperationId, AssetLoadingOptions.DisableLocalization);
			m_startedAssetLoadCount++;
			m_materialAsyncOperationId++;
			AssetLoader.Get().LoadAsset(m_spawnableLibrary.BaseMaterial, delegate(AssetReference assetRef, AssetHandle<Material> assetHandle, object asyncOperationId)
			{
				if (assetHandle == null)
				{
					HandleTextureItemError(assetHandle);
				}
				else if (m_materialAsyncOperationId != (int)asyncOperationId)
				{
					AssetHandle.SafeDispose(ref assetHandle);
				}
				else
				{
					m_material = new Material(assetHandle.Asset);
					m_material.name = "?" + m_material.name;
					m_materialHandle = assetHandle;
					if (m_texture != null)
					{
						HandleTextureItemLoaded();
					}
				}
			}, m_materialAsyncOperationId, AssetLoadingOptions.DisableLocalization);
		}

		private void CreateRenderer(string itemName)
		{
			WidgetTransform component = GetComponent<WidgetTransform>();
			WidgetTransform.FacingDirection facing = ((component != null) ? component.Facing : WidgetTransform.FacingDirection.YPositive);
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
			gameObject.transform.SetParent(base.transform, worldPositionStays: false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = WidgetTransform.GetRotationFromZNegativeToDesiredFacing(facing);
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = base.gameObject.layer;
			gameObject.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
			m_renderer = gameObject.GetComponent<MeshRenderer>();
			m_renderer.gameObject.name = $"Spawnable Library Item: {itemName}";
			m_renderer.enabled = false;
		}

		public void ClearLayerOverride()
		{
			if (m_originalLayer.HasValue)
			{
				base.gameObject.layer = (int)m_originalLayer.Value;
				if (m_widget != null)
				{
					m_widget.SetLayerOverride(m_originalLayer.Value);
				}
				else if (m_renderer != null)
				{
					m_renderer.gameObject.layer = base.gameObject.layer;
				}
				m_originalLayer = null;
				m_overrideLayer = null;
			}
		}

		public void SetLayerOverride(GameLayer layer)
		{
			m_originalLayer = (GameLayer)base.gameObject.layer;
			m_overrideLayer = layer;
			base.gameObject.layer = (int)m_overrideLayer.Value;
			if (m_widget != null)
			{
				m_widget.SetLayerOverride(layer);
			}
			else if (m_renderer != null)
			{
				m_renderer.gameObject.layer = (int)layer;
			}
		}

		protected override void OnDestroy()
		{
			HandleTextureItemCleanUp();
			m_textureAsyncOperationId++;
			m_materialAsyncOperationId++;
			base.OnDestroy();
		}

		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}
	}
}
