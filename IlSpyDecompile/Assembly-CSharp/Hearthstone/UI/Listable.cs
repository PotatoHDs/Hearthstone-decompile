using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using Hearthstone.Util;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class Listable : WidgetBehavior, IBoundsDependent, IPopupRendering, ILayerOverridable, IVisibleWidgetComponent
	{
		public enum LayoutMode
		{
			Vertical,
			Horizontal
		}

		[Tooltip("A mode that determines which direction the list should be laid out in.")]
		[SerializeField]
		private LayoutMode m_layoutMode;

		[Tooltip("A reference to the Widget to be used as the item template.")]
		[SerializeField]
		private WeakAssetReference m_itemTemplate;

		[Tooltip("A script that needs to evaluate to a data model list that is used to generate the list of items.")]
		[SerializeField]
		private ScriptString m_valueScript;

		private readonly List<WidgetInstance> m_widgetItems = new List<WidgetInstance>();

		private readonly HashSet<string> m_widgetsDoneChangingStates = new HashSet<string>();

		private BoxCollider m_listableBounds;

		private bool m_initialized;

		private HashSet<int> m_dataModelIDs;

		private string m_lastScriptString;

		private int m_lastDataVersion;

		private GameLayer? m_overrideLayer;

		private bool m_isOverrideLayerApplied;

		private bool m_isVisible = true;

		private PopupRoot m_popupRoot;

		private HashSet<IPopupRendering> m_popupComponents;

		public IEnumerable<WidgetInstance> WidgetItems => m_widgetItems;

		public LayoutMode Layout
		{
			get
			{
				return m_layoutMode;
			}
			set
			{
				m_layoutMode = value;
			}
		}

		public bool IsDirty => m_lastDataVersion != GetLocalDataVersion();

		public bool NeedsBounds => false;

		public override bool IsChangingStates
		{
			get
			{
				if (m_initialized && !IsDirty)
				{
					return m_widgetsDoneChangingStates.Count < m_widgetItems.Count;
				}
				return true;
			}
		}

		public bool IsDesiredHidden => base.Owner.IsDesiredHiddenInHierarchy;

		public bool IsDesiredHiddenInHierarchy => base.Owner.IsDesiredHiddenInHierarchy;

		public bool HandlesChildVisibility => true;

		public bool HandlesChildLayers => true;

		public BoxCollider GetOrCreateColliderFromItemBounds(bool isEnabled = true)
		{
			Bounds bounds = m_widgetItems.Select((WidgetInstance widget) => WidgetTransform.GetBoundsOfWidgetTransforms(widget.transform, base.transform.worldToLocalMatrix)).Aggregate(delegate(Bounds encapsulation, Bounds next)
			{
				encapsulation.Encapsulate(next);
				return encapsulation;
			});
			if (m_listableBounds == null)
			{
				m_listableBounds = base.gameObject.AddComponent<BoxCollider>();
			}
			m_listableBounds.center = bounds.center;
			m_listableBounds.size = bounds.size;
			m_listableBounds.enabled = isEnabled;
			return m_listableBounds;
		}

		public bool ShouldPropagatePopupRendering()
		{
			return false;
		}

		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			m_popupRoot = popupRoot;
			if (m_popupComponents == null)
			{
				m_popupComponents = new HashSet<IPopupRendering>();
			}
			foreach (WidgetInstance itemWidget in m_widgetItems)
			{
				if (m_widgetsDoneChangingStates.Contains(itemWidget.name))
				{
					PopupRoot.ApplyPopupRendering(m_popupRoot, itemWidget.transform, m_popupComponents);
					continue;
				}
				itemWidget.RegisterDoneChangingStatesListener(delegate
				{
					if (m_popupRoot != null)
					{
						PopupRoot.ApplyPopupRendering(m_popupRoot, itemWidget.transform, m_popupComponents);
					}
				}, null, callImmediatelyIfSet: true, doOnce: true);
			}
		}

		public void DisablePopupRendering()
		{
			foreach (IPopupRendering popupComponent in m_popupComponents)
			{
				if (popupComponent != null && !(popupComponent as Object == null))
				{
					popupComponent.DisablePopupRendering();
				}
			}
			m_popupRoot = null;
			m_popupComponents.Clear();
			m_popupComponents = null;
		}

		private void RemovePopupRendering(Transform objectRoot)
		{
			IPopupRendering[] componentsInChildren = objectRoot.GetComponentsInChildren<IPopupRendering>(includeInactive: true);
			if (componentsInChildren == null)
			{
				return;
			}
			IPopupRendering[] array = componentsInChildren;
			foreach (IPopupRendering popupRendering in array)
			{
				if (popupRendering != null && popupRendering as Object != null)
				{
					popupRendering.DisablePopupRendering();
					m_popupComponents.Remove(popupRendering);
				}
			}
		}

		protected override void OnInitialize()
		{
		}

		public override void OnUpdate()
		{
			int localDataVersion = GetLocalDataVersion();
			if (m_lastDataVersion != localDataVersion)
			{
				HandleDataChanged();
				m_lastDataVersion = localDataVersion;
				if (base.IsChangingStatesInternally)
				{
					CheckIfDoneChangingStates();
				}
			}
		}

		public override bool TryIncrementDataVersion(int id)
		{
			if (m_dataModelIDs == null || m_lastScriptString != m_valueScript.Script)
			{
				m_dataModelIDs = m_valueScript.GetDataModelIDs();
				m_lastScriptString = m_valueScript.Script;
			}
			if (!m_dataModelIDs.Contains(id))
			{
				return false;
			}
			IncrementLocalDataVersion();
			return true;
		}

		public void SetVisibility(bool isVisible, bool isInternal)
		{
			if (isVisible == m_isVisible)
			{
				return;
			}
			m_isVisible = isVisible;
			if (m_isVisible && !IsChangingStates)
			{
				foreach (WidgetInstance widgetItem in m_widgetItems)
				{
					if (!(widgetItem == null))
					{
						widgetItem.Show();
						if (m_overrideLayer.HasValue)
						{
							widgetItem.SetLayerOverride(m_overrideLayer.Value);
							m_isOverrideLayerApplied = true;
						}
					}
				}
			}
			else
			{
				if (m_isVisible)
				{
					return;
				}
				foreach (WidgetInstance widgetItem2 in m_widgetItems)
				{
					if (!(widgetItem2 == null))
					{
						widgetItem2.Hide();
					}
				}
			}
		}

		public void SetLayerOverride(GameLayer layer)
		{
			m_overrideLayer = layer;
			if (!m_isVisible)
			{
				return;
			}
			foreach (WidgetInstance widgetItem in m_widgetItems)
			{
				widgetItem.SetLayerOverride(layer);
			}
			m_isOverrideLayerApplied = true;
		}

		public void ClearLayerOverride()
		{
			if (m_overrideLayer.HasValue && m_isOverrideLayerApplied)
			{
				foreach (WidgetInstance widgetItem in m_widgetItems)
				{
					widgetItem.ClearLayerOverride();
				}
			}
			m_isOverrideLayerApplied = false;
			m_overrideLayer = null;
		}

		private void AddListItem()
		{
			WidgetInstance itemWidget = WidgetInstance.Create(m_itemTemplate.AssetString);
			GameObject gameObject = itemWidget.gameObject;
			gameObject.name = $"ListItem_{m_widgetItems.Count}";
			gameObject.hideFlags = HideFlags.DontSave;
			if (!Application.IsPlaying(this))
			{
				gameObject.hideFlags |= HideFlags.NotEditable;
			}
			GameUtils.SetParent(gameObject, base.gameObject, withRotation: true);
			itemWidget.RegisterStartChangingStatesListener(delegate
			{
				HandleStartChangingStates();
				m_widgetsDoneChangingStates.Remove(itemWidget.name);
			});
			itemWidget.RegisterDoneChangingStatesListener(delegate
			{
				HandleDoneChangingStates(itemWidget);
			});
			itemWidget.RegisterDoneChangingStatesListener(delegate
			{
				if (m_popupRoot != null)
				{
					PopupRoot.ApplyPopupRendering(m_popupRoot, itemWidget.transform, m_popupComponents);
				}
			}, null, callImmediatelyIfSet: true, doOnce: true);
			base.Owner.AddNestedInstance(itemWidget, base.gameObject);
			m_widgetItems.Add(itemWidget);
			itemWidget.Initialize();
			itemWidget.Hide();
		}

		private void RemoveListItem()
		{
			if (m_widgetItems.Count != 0)
			{
				WidgetInstance widgetInstance = m_widgetItems.Last();
				m_widgetItems.Remove(widgetInstance);
				m_widgetsDoneChangingStates.Remove(widgetInstance.name);
				base.Owner.RemoveNestedInstance(widgetInstance);
				if (m_popupRoot != null)
				{
					RemovePopupRendering(widgetInstance.transform);
				}
				if (Application.IsPlaying(this))
				{
					Object.Destroy(widgetInstance.gameObject);
				}
				else
				{
					Object.DestroyImmediate(widgetInstance.gameObject);
				}
			}
		}

		private void HandleDataChanged()
		{
			ScriptContext.EvaluationResults evaluationResults = new ScriptContext().Evaluate(m_valueScript.Script, this);
			IDataModelList dataModelList = evaluationResults.Value as IDataModelList;
			ArrayList arrayList = ((dataModelList == null) ? (evaluationResults.Value as ArrayList) : null);
			if (arrayList != null)
			{
				dataModelList = new DataModelList<IDataModel>();
				foreach (object item in arrayList)
				{
					dataModelList.Add(item as IDataModel);
				}
			}
			int num = dataModelList?.Count ?? 0;
			bool flag = false;
			if (m_widgetItems.Count < num)
			{
				flag = true;
			}
			else if (m_widgetItems.Count > num)
			{
				flag = true;
			}
			while (m_widgetItems.Count < num)
			{
				AddListItem();
			}
			while (m_widgetItems.Count > num)
			{
				RemoveListItem();
			}
			if (m_widgetItems.Count > 0)
			{
				BindWidgetDataModels(m_widgetItems, dataModelList?.Cast<IDataModel>(), num);
				flag |= m_widgetItems.Any((WidgetInstance widget) => widget.IsChangingStates);
			}
			if (flag)
			{
				HandleStartChangingStates();
			}
		}

		private static void BindWidgetDataModels(IEnumerable<Widget> widgets, IEnumerable<IDataModel> dataModels, int count)
		{
			FunctionalUtil.Zip(widgets, dataModels, Enumerable.Range(0, count), Enumerable.Repeat(count, count), BindWidget);
		}

		private static void BindWidget(Widget widget, IDataModel dataModel, int index, int count)
		{
			if (!(widget == null))
			{
				widget.BindDataModel(GetMetaDataModel(widget, index, count));
				if (dataModel != null)
				{
					widget.BindDataModel(dataModel);
				}
			}
		}

		private void HandleDoneChangingStates(WidgetInstance itemWidget)
		{
			m_widgetsDoneChangingStates.Add(itemWidget.name);
			CheckIfDoneChangingStates();
		}

		private void CheckIfDoneChangingStates()
		{
			if (m_widgetsDoneChangingStates.Count >= m_widgetItems.Count && !IsDirty)
			{
				HandleLayout();
				m_initialized = true;
				HandleDoneChangingStates();
			}
			_ = m_initialized;
		}

		private void HandleLayout()
		{
			Vector3 layoutDirection = GetLayoutDirection();
			float startOfNextItem = 0f;
			foreach (WidgetInstance widgetItem in m_widgetItems)
			{
				Bounds boundsOfWidgetTransforms = WidgetTransform.GetBoundsOfWidgetTransforms(widgetItem.transform, base.transform.worldToLocalMatrix);
				Vector3 nextLayoutPosition = GetNextLayoutPosition(boundsOfWidgetTransforms, layoutDirection, ref startOfNextItem);
				widgetItem.transform.localPosition += nextLayoutPosition;
				if (m_isVisible)
				{
					widgetItem.Show();
					if (m_overrideLayer.HasValue)
					{
						widgetItem.SetLayerOverride(m_overrideLayer.Value);
						m_isOverrideLayerApplied = true;
					}
				}
			}
		}

		private static ListableDataModel GetMetaDataModel(Widget widget, int i, int size)
		{
			ListableDataModel listableDataModel = null;
			listableDataModel = ((!widget.GetDataModel(258, out var model)) ? new ListableDataModel() : (model as ListableDataModel));
			listableDataModel.ItemIndex = i;
			listableDataModel.ListSize = size;
			listableDataModel.IsFirstItem = i == 0;
			listableDataModel.IsLastItem = i == size - 1;
			return listableDataModel;
		}

		private Vector3 GetNextLayoutPosition(Bounds bounds, Vector3 layoutDirection, ref float startOfNextItem)
		{
			float a = Vector3.Dot(bounds.min, layoutDirection);
			float b = Vector3.Dot(bounds.max, layoutDirection);
			float num = Mathf.Min(a, b);
			float num2 = startOfNextItem - num;
			float num3 = Mathf.Abs(Vector3.Dot(bounds.size, layoutDirection));
			startOfNextItem += num3;
			return layoutDirection * num2;
		}

		private Vector3 GetLayoutDirection()
		{
			Vector3 vector = new Vector3(0f, -1f, 0f);
			LayoutMode layoutMode = m_layoutMode;
			if (layoutMode != 0 && layoutMode == LayoutMode.Horizontal)
			{
				vector = new Vector3(1f, 0f, 0f);
			}
			WidgetTransform component = GetComponent<WidgetTransform>();
			return WidgetTransform.GetRotationFromZNegativeToDesiredFacing((component != null) ? component.Facing : WidgetTransform.FacingDirection.YPositive) * vector;
		}

		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}
	}
}
