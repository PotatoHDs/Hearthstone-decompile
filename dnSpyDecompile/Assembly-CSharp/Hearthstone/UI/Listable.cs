using System;
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
	// Token: 0x02001002 RID: 4098
	[ExecuteAlways]
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class Listable : WidgetBehavior, IBoundsDependent, IPopupRendering, ILayerOverridable, IVisibleWidgetComponent
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600B210 RID: 45584 RVA: 0x0036E011 File Offset: 0x0036C211
		public IEnumerable<WidgetInstance> WidgetItems
		{
			get
			{
				return this.m_widgetItems;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600B211 RID: 45585 RVA: 0x0036E019 File Offset: 0x0036C219
		// (set) Token: 0x0600B212 RID: 45586 RVA: 0x0036E021 File Offset: 0x0036C221
		public Listable.LayoutMode Layout
		{
			get
			{
				return this.m_layoutMode;
			}
			set
			{
				this.m_layoutMode = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x0600B213 RID: 45587 RVA: 0x0036E02A File Offset: 0x0036C22A
		public bool IsDirty
		{
			get
			{
				return this.m_lastDataVersion != base.GetLocalDataVersion();
			}
		}

		// Token: 0x0600B214 RID: 45588 RVA: 0x0036E040 File Offset: 0x0036C240
		public BoxCollider GetOrCreateColliderFromItemBounds(bool isEnabled = true)
		{
			Bounds bounds = (from widget in this.m_widgetItems
			select WidgetTransform.GetBoundsOfWidgetTransforms(widget.transform, base.transform.worldToLocalMatrix)).Aggregate(delegate(Bounds encapsulation, Bounds next)
			{
				encapsulation.Encapsulate(next);
				return encapsulation;
			});
			if (this.m_listableBounds == null)
			{
				this.m_listableBounds = base.gameObject.AddComponent<BoxCollider>();
			}
			this.m_listableBounds.center = bounds.center;
			this.m_listableBounds.size = bounds.size;
			this.m_listableBounds.enabled = isEnabled;
			return this.m_listableBounds;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x0600B215 RID: 45589 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool NeedsBounds
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B216 RID: 45590 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool ShouldPropagatePopupRendering()
		{
			return false;
		}

		// Token: 0x0600B217 RID: 45591 RVA: 0x0036E0E0 File Offset: 0x0036C2E0
		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			this.m_popupRoot = popupRoot;
			if (this.m_popupComponents == null)
			{
				this.m_popupComponents = new HashSet<IPopupRendering>();
			}
			using (List<WidgetInstance>.Enumerator enumerator = this.m_widgetItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					WidgetInstance itemWidget = enumerator.Current;
					if (this.m_widgetsDoneChangingStates.Contains(itemWidget.name))
					{
						PopupRoot.ApplyPopupRendering(this.m_popupRoot, itemWidget.transform, this.m_popupComponents);
					}
					else
					{
						itemWidget.RegisterDoneChangingStatesListener(delegate(object _)
						{
							if (this.m_popupRoot != null)
							{
								PopupRoot.ApplyPopupRendering(this.m_popupRoot, itemWidget.transform, this.m_popupComponents);
							}
						}, null, true, true);
					}
				}
			}
		}

		// Token: 0x0600B218 RID: 45592 RVA: 0x0036E1A8 File Offset: 0x0036C3A8
		public void DisablePopupRendering()
		{
			foreach (IPopupRendering popupRendering in this.m_popupComponents)
			{
				if (popupRendering != null && !(popupRendering as UnityEngine.Object == null))
				{
					popupRendering.DisablePopupRendering();
				}
			}
			this.m_popupRoot = null;
			this.m_popupComponents.Clear();
			this.m_popupComponents = null;
		}

		// Token: 0x0600B219 RID: 45593 RVA: 0x0036E224 File Offset: 0x0036C424
		private void RemovePopupRendering(Transform objectRoot)
		{
			IPopupRendering[] componentsInChildren = objectRoot.GetComponentsInChildren<IPopupRendering>(true);
			if (componentsInChildren == null)
			{
				return;
			}
			foreach (IPopupRendering popupRendering in componentsInChildren)
			{
				if (popupRendering != null && popupRendering as UnityEngine.Object != null)
				{
					popupRendering.DisablePopupRendering();
					this.m_popupComponents.Remove(popupRendering);
				}
			}
		}

		// Token: 0x0600B21A RID: 45594 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected override void OnInitialize()
		{
		}

		// Token: 0x0600B21B RID: 45595 RVA: 0x0036E278 File Offset: 0x0036C478
		public override void OnUpdate()
		{
			int localDataVersion = base.GetLocalDataVersion();
			if (this.m_lastDataVersion != localDataVersion)
			{
				this.HandleDataChanged();
				this.m_lastDataVersion = localDataVersion;
				if (base.IsChangingStatesInternally)
				{
					this.CheckIfDoneChangingStates();
				}
			}
		}

		// Token: 0x0600B21C RID: 45596 RVA: 0x0036E2B0 File Offset: 0x0036C4B0
		public override bool TryIncrementDataVersion(int id)
		{
			if (this.m_dataModelIDs == null || this.m_lastScriptString != this.m_valueScript.Script)
			{
				this.m_dataModelIDs = this.m_valueScript.GetDataModelIDs();
				this.m_lastScriptString = this.m_valueScript.Script;
			}
			if (!this.m_dataModelIDs.Contains(id))
			{
				return false;
			}
			base.IncrementLocalDataVersion();
			return true;
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x0600B21D RID: 45597 RVA: 0x0036E316 File Offset: 0x0036C516
		public override bool IsChangingStates
		{
			get
			{
				return !this.m_initialized || this.IsDirty || this.m_widgetsDoneChangingStates.Count < this.m_widgetItems.Count;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x0600B21E RID: 45598 RVA: 0x0036E342 File Offset: 0x0036C542
		public bool IsDesiredHidden
		{
			get
			{
				return base.Owner.IsDesiredHiddenInHierarchy;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600B21F RID: 45599 RVA: 0x0036E342 File Offset: 0x0036C542
		public bool IsDesiredHiddenInHierarchy
		{
			get
			{
				return base.Owner.IsDesiredHiddenInHierarchy;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600B220 RID: 45600 RVA: 0x000052EC File Offset: 0x000034EC
		public bool HandlesChildVisibility
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B221 RID: 45601 RVA: 0x0036E350 File Offset: 0x0036C550
		public void SetVisibility(bool isVisible, bool isInternal)
		{
			if (isVisible == this.m_isVisible)
			{
				return;
			}
			this.m_isVisible = isVisible;
			if (this.m_isVisible && !this.IsChangingStates)
			{
				using (List<WidgetInstance>.Enumerator enumerator = this.m_widgetItems.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						WidgetInstance widgetInstance = enumerator.Current;
						if (!(widgetInstance == null))
						{
							widgetInstance.Show();
							if (this.m_overrideLayer != null)
							{
								widgetInstance.SetLayerOverride(this.m_overrideLayer.Value);
								this.m_isOverrideLayerApplied = true;
							}
						}
					}
					return;
				}
			}
			if (!this.m_isVisible)
			{
				foreach (WidgetInstance widgetInstance2 in this.m_widgetItems)
				{
					if (!(widgetInstance2 == null))
					{
						widgetInstance2.Hide();
					}
				}
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600B222 RID: 45602 RVA: 0x000052EC File Offset: 0x000034EC
		public bool HandlesChildLayers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B223 RID: 45603 RVA: 0x0036E444 File Offset: 0x0036C644
		public void SetLayerOverride(GameLayer layer)
		{
			this.m_overrideLayer = new GameLayer?(layer);
			if (this.m_isVisible)
			{
				foreach (WidgetInstance widgetInstance in this.m_widgetItems)
				{
					widgetInstance.SetLayerOverride(layer);
				}
				this.m_isOverrideLayerApplied = true;
			}
		}

		// Token: 0x0600B224 RID: 45604 RVA: 0x0036E4B0 File Offset: 0x0036C6B0
		public void ClearLayerOverride()
		{
			if (this.m_overrideLayer != null && this.m_isOverrideLayerApplied)
			{
				foreach (WidgetInstance widgetInstance in this.m_widgetItems)
				{
					widgetInstance.ClearLayerOverride();
				}
			}
			this.m_isOverrideLayerApplied = false;
			this.m_overrideLayer = null;
		}

		// Token: 0x0600B225 RID: 45605 RVA: 0x0036E528 File Offset: 0x0036C728
		private void AddListItem()
		{
			WidgetInstance itemWidget = WidgetInstance.Create(this.m_itemTemplate.AssetString, false);
			GameObject gameObject = itemWidget.gameObject;
			gameObject.name = string.Format("ListItem_{0}", this.m_widgetItems.Count);
			gameObject.hideFlags = HideFlags.DontSave;
			if (!Application.IsPlaying(this))
			{
				gameObject.hideFlags |= HideFlags.NotEditable;
			}
			GameUtils.SetParent(gameObject, base.gameObject, true);
			itemWidget.RegisterStartChangingStatesListener(delegate(object _)
			{
				this.HandleStartChangingStates();
				this.m_widgetsDoneChangingStates.Remove(itemWidget.name);
			}, null, true, false);
			itemWidget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				this.HandleDoneChangingStates(itemWidget);
			}, null, true, false);
			itemWidget.RegisterDoneChangingStatesListener(delegate(object _)
			{
				if (this.m_popupRoot != null)
				{
					PopupRoot.ApplyPopupRendering(this.m_popupRoot, itemWidget.transform, this.m_popupComponents);
				}
			}, null, true, true);
			base.Owner.AddNestedInstance(itemWidget, base.gameObject);
			this.m_widgetItems.Add(itemWidget);
			itemWidget.Initialize();
			itemWidget.Hide();
		}

		// Token: 0x0600B226 RID: 45606 RVA: 0x0036E63C File Offset: 0x0036C83C
		private void RemoveListItem()
		{
			if (this.m_widgetItems.Count == 0)
			{
				return;
			}
			WidgetInstance widgetInstance = this.m_widgetItems.Last<WidgetInstance>();
			this.m_widgetItems.Remove(widgetInstance);
			this.m_widgetsDoneChangingStates.Remove(widgetInstance.name);
			base.Owner.RemoveNestedInstance(widgetInstance);
			if (this.m_popupRoot != null)
			{
				this.RemovePopupRendering(widgetInstance.transform);
			}
			if (Application.IsPlaying(this))
			{
				UnityEngine.Object.Destroy(widgetInstance.gameObject);
				return;
			}
			UnityEngine.Object.DestroyImmediate(widgetInstance.gameObject);
		}

		// Token: 0x0600B227 RID: 45607 RVA: 0x0036E6C8 File Offset: 0x0036C8C8
		private void HandleDataChanged()
		{
			ScriptContext.EvaluationResults evaluationResults = new ScriptContext().Evaluate(this.m_valueScript.Script, this);
			IDataModelList dataModelList = evaluationResults.Value as IDataModelList;
			ArrayList arrayList = (dataModelList == null) ? (evaluationResults.Value as ArrayList) : null;
			if (arrayList != null)
			{
				dataModelList = new DataModelList<IDataModel>();
				foreach (object obj in arrayList)
				{
					dataModelList.Add(obj as IDataModel);
				}
			}
			int num = (dataModelList != null) ? dataModelList.Count : 0;
			bool flag = false;
			if (this.m_widgetItems.Count < num)
			{
				flag = true;
			}
			else if (this.m_widgetItems.Count > num)
			{
				flag = true;
			}
			while (this.m_widgetItems.Count < num)
			{
				this.AddListItem();
			}
			while (this.m_widgetItems.Count > num)
			{
				this.RemoveListItem();
			}
			if (this.m_widgetItems.Count > 0)
			{
				Listable.BindWidgetDataModels(this.m_widgetItems, (dataModelList != null) ? dataModelList.Cast<IDataModel>() : null, num);
				flag |= this.m_widgetItems.Any((WidgetInstance widget) => widget.IsChangingStates);
			}
			if (flag)
			{
				base.HandleStartChangingStates();
			}
		}

		// Token: 0x0600B228 RID: 45608 RVA: 0x0036E824 File Offset: 0x0036CA24
		private static void BindWidgetDataModels(IEnumerable<Widget> widgets, IEnumerable<IDataModel> dataModels, int count)
		{
			FunctionalUtil.Zip<Widget, IDataModel, int, int>(widgets, dataModels, Enumerable.Range(0, count), Enumerable.Repeat<int>(count, count), new Action<Widget, IDataModel, int, int>(Listable.BindWidget));
		}

		// Token: 0x0600B229 RID: 45609 RVA: 0x0036E847 File Offset: 0x0036CA47
		private static void BindWidget(Widget widget, IDataModel dataModel, int index, int count)
		{
			if (widget == null)
			{
				return;
			}
			widget.BindDataModel(Listable.GetMetaDataModel(widget, index, count), false);
			if (dataModel != null)
			{
				widget.BindDataModel(dataModel, false);
			}
		}

		// Token: 0x0600B22A RID: 45610 RVA: 0x0036E86D File Offset: 0x0036CA6D
		private void HandleDoneChangingStates(WidgetInstance itemWidget)
		{
			this.m_widgetsDoneChangingStates.Add(itemWidget.name);
			this.CheckIfDoneChangingStates();
		}

		// Token: 0x0600B22B RID: 45611 RVA: 0x0036E887 File Offset: 0x0036CA87
		private void CheckIfDoneChangingStates()
		{
			if (this.m_widgetsDoneChangingStates.Count >= this.m_widgetItems.Count && !this.IsDirty)
			{
				this.HandleLayout();
				this.m_initialized = true;
				base.HandleDoneChangingStates();
			}
			bool initialized = this.m_initialized;
		}

		// Token: 0x0600B22C RID: 45612 RVA: 0x0036E8C4 File Offset: 0x0036CAC4
		private void HandleLayout()
		{
			Vector3 layoutDirection = this.GetLayoutDirection();
			float num = 0f;
			foreach (WidgetInstance widgetInstance in this.m_widgetItems)
			{
				Bounds boundsOfWidgetTransforms = WidgetTransform.GetBoundsOfWidgetTransforms(widgetInstance.transform, base.transform.worldToLocalMatrix);
				Vector3 nextLayoutPosition = this.GetNextLayoutPosition(boundsOfWidgetTransforms, layoutDirection, ref num);
				widgetInstance.transform.localPosition += nextLayoutPosition;
				if (this.m_isVisible)
				{
					widgetInstance.Show();
					if (this.m_overrideLayer != null)
					{
						widgetInstance.SetLayerOverride(this.m_overrideLayer.Value);
						this.m_isOverrideLayerApplied = true;
					}
				}
			}
		}

		// Token: 0x0600B22D RID: 45613 RVA: 0x0036E990 File Offset: 0x0036CB90
		private static ListableDataModel GetMetaDataModel(Widget widget, int i, int size)
		{
			IDataModel dataModel;
			ListableDataModel listableDataModel;
			if (widget.GetDataModel(258, out dataModel))
			{
				listableDataModel = (dataModel as ListableDataModel);
			}
			else
			{
				listableDataModel = new ListableDataModel();
			}
			listableDataModel.ItemIndex = i;
			listableDataModel.ListSize = size;
			listableDataModel.IsFirstItem = (i == 0);
			listableDataModel.IsLastItem = (i == size - 1);
			return listableDataModel;
		}

		// Token: 0x0600B22E RID: 45614 RVA: 0x0036E9E4 File Offset: 0x0036CBE4
		private Vector3 GetNextLayoutPosition(Bounds bounds, Vector3 layoutDirection, ref float startOfNextItem)
		{
			float a = Vector3.Dot(bounds.min, layoutDirection);
			float b = Vector3.Dot(bounds.max, layoutDirection);
			float num = Mathf.Min(a, b);
			float d = startOfNextItem - num;
			float num2 = Mathf.Abs(Vector3.Dot(bounds.size, layoutDirection));
			startOfNextItem += num2;
			return layoutDirection * d;
		}

		// Token: 0x0600B22F RID: 45615 RVA: 0x0036EA38 File Offset: 0x0036CC38
		private Vector3 GetLayoutDirection()
		{
			Vector3 vector = new Vector3(0f, -1f, 0f);
			Listable.LayoutMode layoutMode = this.m_layoutMode;
			if (layoutMode != Listable.LayoutMode.Vertical && layoutMode == Listable.LayoutMode.Horizontal)
			{
				vector = new Vector3(1f, 0f, 0f);
			}
			WidgetTransform component = base.GetComponent<WidgetTransform>();
			vector = WidgetTransform.GetRotationFromZNegativeToDesiredFacing((component != null) ? component.Facing : WidgetTransform.FacingDirection.YPositive) * vector;
			return vector;
		}

		// Token: 0x0600B230 RID: 45616 RVA: 0x0036D90B File Offset: 0x0036BB0B
		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}

		// Token: 0x040095F2 RID: 38386
		[Tooltip("A mode that determines which direction the list should be laid out in.")]
		[SerializeField]
		private Listable.LayoutMode m_layoutMode;

		// Token: 0x040095F3 RID: 38387
		[Tooltip("A reference to the Widget to be used as the item template.")]
		[SerializeField]
		private WeakAssetReference m_itemTemplate;

		// Token: 0x040095F4 RID: 38388
		[Tooltip("A script that needs to evaluate to a data model list that is used to generate the list of items.")]
		[SerializeField]
		private ScriptString m_valueScript;

		// Token: 0x040095F5 RID: 38389
		private readonly List<WidgetInstance> m_widgetItems = new List<WidgetInstance>();

		// Token: 0x040095F6 RID: 38390
		private readonly HashSet<string> m_widgetsDoneChangingStates = new HashSet<string>();

		// Token: 0x040095F7 RID: 38391
		private BoxCollider m_listableBounds;

		// Token: 0x040095F8 RID: 38392
		private bool m_initialized;

		// Token: 0x040095F9 RID: 38393
		private HashSet<int> m_dataModelIDs;

		// Token: 0x040095FA RID: 38394
		private string m_lastScriptString;

		// Token: 0x040095FB RID: 38395
		private int m_lastDataVersion;

		// Token: 0x040095FC RID: 38396
		private GameLayer? m_overrideLayer;

		// Token: 0x040095FD RID: 38397
		private bool m_isOverrideLayerApplied;

		// Token: 0x040095FE RID: 38398
		private bool m_isVisible = true;

		// Token: 0x040095FF RID: 38399
		private PopupRoot m_popupRoot;

		// Token: 0x04009600 RID: 38400
		private HashSet<IPopupRendering> m_popupComponents;

		// Token: 0x02002835 RID: 10293
		public enum LayoutMode
		{
			// Token: 0x0400F8D2 RID: 63698
			Vertical,
			// Token: 0x0400F8D3 RID: 63699
			Horizontal
		}
	}
}
