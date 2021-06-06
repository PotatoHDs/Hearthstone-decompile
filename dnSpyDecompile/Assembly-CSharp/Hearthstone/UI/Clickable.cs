using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FFF RID: 4095
	[ExecuteAlways]
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class Clickable : WidgetBehavior, IPopupRendering, IBoundsDependent, ILayerOverridable
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x0600B1C3 RID: 45507 RVA: 0x0036D187 File Offset: 0x0036B387
		// (set) Token: 0x0600B1C2 RID: 45506 RVA: 0x0036D0EC File Offset: 0x0036B2EC
		[Overridable]
		public bool Active
		{
			get
			{
				return this.m_active && base.IsActive;
			}
			set
			{
				if (this.m_active == value)
				{
					return;
				}
				this.m_activeChanged = true;
				this.m_active = value;
				if (!this.m_active)
				{
					this.OnRelease(null);
					this.OnReleaseAll(null);
					this.OnDeselected(null);
					this.OnRollOut(null);
				}
				if (this.m_pegUiElement != null)
				{
					this.m_pegUiElement.SetEnabled(value, false);
				}
				if (this.m_boundsCollider != null)
				{
					this.m_boundsCollider.enabled = (value && base.IsActive);
				}
				this.SetMeshCollidersEnabled(value && base.IsActive);
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x0600B1C4 RID: 45508 RVA: 0x0036D199 File Offset: 0x0036B399
		public bool NeedsBounds
		{
			get
			{
				return this.m_colliderType == Clickable.ColliderType.Bounds;
			}
		}

		// Token: 0x0600B1C5 RID: 45509 RVA: 0x0036D1A4 File Offset: 0x0036B3A4
		protected override void OnDisable()
		{
			if (this.m_boundsCollider != null)
			{
				this.m_boundsCollider.enabled = false;
			}
			this.SetMeshCollidersEnabled(false);
			base.OnDisable();
		}

		// Token: 0x0600B1C6 RID: 45510 RVA: 0x0036D1CD File Offset: 0x0036B3CD
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_boundsCollider != null)
			{
				this.m_boundsCollider.enabled = this.m_active;
			}
			this.SetMeshCollidersEnabled(this.m_active);
		}

		// Token: 0x0600B1C7 RID: 45511 RVA: 0x0036D200 File Offset: 0x0036B400
		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			this.SetLayerOverride(GameLayer.Reserved29);
		}

		// Token: 0x0600B1C8 RID: 45512 RVA: 0x0036D20A File Offset: 0x0036B40A
		public void DisablePopupRendering()
		{
			this.ClearLayerOverride();
		}

		// Token: 0x0600B1C9 RID: 45513 RVA: 0x000052EC File Offset: 0x000034EC
		public bool ShouldPropagatePopupRendering()
		{
			return true;
		}

		// Token: 0x0600B1CA RID: 45514 RVA: 0x0036D214 File Offset: 0x0036B414
		private PegUIElement GetOrCreatePegUIElement()
		{
			if (this.m_pegUiElement == null)
			{
				this.m_pegUiElement = (base.gameObject.GetComponent<PegUIElement>() ?? base.gameObject.AddComponent<PegUIElement>());
				this.m_pegUiElement.hideFlags = HideFlags.DontSave;
			}
			return this.m_pegUiElement;
		}

		// Token: 0x0600B1CB RID: 45515 RVA: 0x0036D264 File Offset: 0x0036B464
		protected override void OnInitialize()
		{
			if (this.m_stateCollection == null)
			{
				this.m_stateCollection = new WidgetBehaviorStateCollection();
			}
			if (!Application.IsPlaying(this))
			{
				return;
			}
			PegUIElement orCreatePegUIElement = this.GetOrCreatePegUIElement();
			orCreatePegUIElement.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnRelease));
			orCreatePegUIElement.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.OnReleaseAll));
			orCreatePegUIElement.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnRollOver));
			orCreatePegUIElement.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnRollOut));
			orCreatePegUIElement.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnClick));
			orCreatePegUIElement.AddEventListener(UIEventType.TAP, new UIEvent.Handler(this.OnSelected));
			if (this.m_stateCollection.DoesStateExist(this.DOUBLECLICKED))
			{
				orCreatePegUIElement.AddEventListener(UIEventType.DOUBLECLICK, new UIEvent.Handler(this.OnDoubleClick));
			}
			orCreatePegUIElement.AddEventListener(UIEventType.DRAG, new UIEvent.Handler(this.OnDrag));
			orCreatePegUIElement.SetEnabled(this.m_active && !base.Owner.IsDesiredHiddenInHierarchy, true);
			Clickable.ColliderType colliderType = this.m_colliderType;
			if (colliderType != Clickable.ColliderType.Bounds)
			{
				if (colliderType != Clickable.ColliderType.Geometry)
				{
					return;
				}
				foreach (GameObject gameObject in this.m_geometryRoots)
				{
					if (!(gameObject == null))
					{
						this.ApplyPegUIProxies(gameObject.transform);
					}
				}
			}
			else
			{
				WidgetTransform component = base.gameObject.GetComponent<WidgetTransform>();
				if (component != null)
				{
					this.m_boundsCollider = component.CreateBoxCollider(base.gameObject);
					return;
				}
			}
		}

		// Token: 0x0600B1CC RID: 45516 RVA: 0x0036D3D5 File Offset: 0x0036B5D5
		private void ApplyPegUIProxies(object root)
		{
			if (this == null || root == null)
			{
				return;
			}
			SceneUtils.WalkSelfAndChildren((Transform)root, delegate(Transform child)
			{
				if (child == null)
				{
					return false;
				}
				Component[] components = child.GetComponents<Component>();
				for (int i = 0; i < components.Length; i++)
				{
					Component component = components[i];
					MeshFilter meshFilter = component as MeshFilter;
					if (meshFilter != null)
					{
						this.PopulateMeshesWithPegUIProxies(meshFilter);
					}
					else
					{
						Geometry geometry = component as Geometry;
						if (geometry != null)
						{
							geometry.RegisterReadyListener(delegate(object _)
							{
								this.PopulateGeometryMeshesWithPegUIProxies(geometry);
							}, null, true);
						}
					}
				}
				List<IAsyncInitializationBehavior> asyncBehaviors = AsyncBehaviorUtils.GetAsyncBehaviors(child);
				if (asyncBehaviors != null)
				{
					foreach (IAsyncInitializationBehavior asyncInitializationBehavior in asyncBehaviors)
					{
						asyncInitializationBehavior.RegisterReadyListener(new Action<object>(this.ApplyPegUIProxies), child, false);
					}
				}
				return true;
			});
		}

		// Token: 0x0600B1CD RID: 45517 RVA: 0x0036D3FB File Offset: 0x0036B5FB
		private void OnSelected(UIEvent e)
		{
			if (this.m_selected || !this.Active)
			{
				return;
			}
			this.m_selected = true;
			this.SetVisualState(Clickable.VisualState.Selected);
		}

		// Token: 0x0600B1CE RID: 45518 RVA: 0x0036D41D File Offset: 0x0036B61D
		private void OnDeselected(UIEvent e)
		{
			if (!this.m_selected)
			{
				return;
			}
			this.m_selected = false;
			this.SetVisualState(Clickable.VisualState.Deselected);
		}

		// Token: 0x0600B1CF RID: 45519 RVA: 0x0036D437 File Offset: 0x0036B637
		private void OnRelease(UIEvent e)
		{
			if (this.m_clicked)
			{
				this.m_clicked = false;
				this.SetVisualState(Clickable.VisualState.Released);
			}
			if (this.m_dragged)
			{
				this.m_dragged = false;
				this.SetVisualState(Clickable.VisualState.DragReleased);
			}
		}

		// Token: 0x0600B1D0 RID: 45520 RVA: 0x0036D468 File Offset: 0x0036B668
		private void OnReleaseAll(UIEvent e)
		{
			this.m_clicked = false;
			if (this.m_dragged)
			{
				this.m_dragged = false;
				this.SetVisualState(Clickable.VisualState.DragReleased);
			}
		}

		// Token: 0x0600B1D1 RID: 45521 RVA: 0x0036D489 File Offset: 0x0036B689
		private void OnRollOut(UIEvent e)
		{
			if (!this.m_hovered)
			{
				return;
			}
			this.m_hovered = false;
			this.SetVisualState(Clickable.VisualState.MouseOut);
		}

		// Token: 0x0600B1D2 RID: 45522 RVA: 0x0036D4A3 File Offset: 0x0036B6A3
		private void OnRollOver(UIEvent e)
		{
			if (this.m_hovered || !this.Active)
			{
				return;
			}
			this.m_hovered = true;
			this.SetVisualState(Clickable.VisualState.MouseOver);
		}

		// Token: 0x0600B1D3 RID: 45523 RVA: 0x0036D4C5 File Offset: 0x0036B6C5
		private void OnClick(UIEvent e)
		{
			if (this.m_clicked || !this.Active)
			{
				return;
			}
			this.m_clicked = true;
			this.SetVisualState(Clickable.VisualState.Clicked);
		}

		// Token: 0x0600B1D4 RID: 45524 RVA: 0x0036D4E7 File Offset: 0x0036B6E7
		private void OnDoubleClick(UIEvent e)
		{
			if (!this.m_clicked || !this.Active)
			{
				return;
			}
			this.m_clicked = false;
			this.SetVisualState(Clickable.VisualState.DoubleClicked);
		}

		// Token: 0x0600B1D5 RID: 45525 RVA: 0x0036D509 File Offset: 0x0036B709
		private void OnDrag(UIEvent e)
		{
			if (this.m_dragged || !this.Active)
			{
				return;
			}
			this.m_dragged = true;
			this.SetVisualState(Clickable.VisualState.DragStarted);
		}

		// Token: 0x0600B1D6 RID: 45526 RVA: 0x0036D52C File Offset: 0x0036B72C
		public override void OnUpdate()
		{
			if (this.m_activeChanged)
			{
				this.SetVisualState(this.m_active ? Clickable.VisualState.Active : Clickable.VisualState.Inactive);
				this.m_activeChanged = false;
			}
			if (this.m_stateCollection != null)
			{
				this.m_stateCollection.Update(this);
			}
		}

		// Token: 0x0600B1D7 RID: 45527 RVA: 0x0036D564 File Offset: 0x0036B764
		private bool SetVisualState(Clickable.VisualState visualState)
		{
			this.m_stateCollection.IndependentStates = true;
			switch (visualState)
			{
			case Clickable.VisualState.Active:
				this.m_stateCollection.AbortState(this.INACTIVE);
				return this.m_stateCollection.ActivateState(this, this.ACTIVE, true, false);
			case Clickable.VisualState.Inactive:
				this.m_stateCollection.AbortState(this.ACTIVE);
				return this.m_stateCollection.ActivateState(this, this.INACTIVE, true, false);
			case Clickable.VisualState.Selected:
				this.m_stateCollection.AbortState(this.DESELECTED);
				return this.m_stateCollection.ActivateState(this, this.SELECTED, true, false);
			case Clickable.VisualState.Deselected:
				this.m_stateCollection.AbortState(this.SELECTED);
				return this.m_stateCollection.ActivateState(this, this.DESELECTED, true, false);
			case Clickable.VisualState.Clicked:
				this.m_stateCollection.AbortState(this.RELEASED);
				return this.m_stateCollection.ActivateState(this, this.CLICKED, true, false);
			case Clickable.VisualState.Released:
				this.m_stateCollection.AbortState(this.CLICKED);
				return this.m_stateCollection.ActivateState(this, this.RELEASED, true, false);
			case Clickable.VisualState.MouseOver:
				this.m_stateCollection.AbortState(this.MOUSEOUT);
				return this.m_stateCollection.ActivateState(this, this.MOUSEOVER, true, false);
			case Clickable.VisualState.MouseOut:
				this.m_stateCollection.AbortState(this.MOUSEOVER);
				return this.m_stateCollection.ActivateState(this, this.MOUSEOUT, true, false);
			case Clickable.VisualState.DoubleClicked:
				this.m_stateCollection.AbortState(this.RELEASED);
				return this.m_stateCollection.ActivateState(this, this.DOUBLECLICKED, true, false);
			case Clickable.VisualState.DragStarted:
				this.m_stateCollection.AbortState(this.DRAG_RELEASED);
				return this.m_stateCollection.ActivateState(this, this.DRAG_STARTED, true, false);
			case Clickable.VisualState.DragReleased:
				this.m_stateCollection.AbortState(this.DRAG_STARTED);
				return this.m_stateCollection.ActivateState(this, this.DRAG_RELEASED, true, false);
			default:
				return false;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600B1D8 RID: 45528 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool HandlesChildLayers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B1D9 RID: 45529 RVA: 0x0036D758 File Offset: 0x0036B958
		public void SetLayerOverride(GameLayer layer)
		{
			if (this.m_originalLayer == null)
			{
				GameObject gameObject = base.gameObject;
				this.m_originalLayer = new GameLayer?((GameLayer)gameObject.layer);
				gameObject.layer = (int)layer;
			}
		}

		// Token: 0x0600B1DA RID: 45530 RVA: 0x0036D791 File Offset: 0x0036B991
		public void ClearLayerOverride()
		{
			if (this.m_originalLayer != null)
			{
				base.gameObject.layer = (int)this.m_originalLayer.Value;
				this.m_originalLayer = null;
			}
		}

		// Token: 0x0600B1DB RID: 45531 RVA: 0x0036D7C2 File Offset: 0x0036B9C2
		public virtual bool AddEventListener(UIEventType type, UIEvent.Handler handler)
		{
			return this.GetOrCreatePegUIElement().AddEventListener(type, handler);
		}

		// Token: 0x0600B1DC RID: 45532 RVA: 0x0036D7D1 File Offset: 0x0036B9D1
		public virtual object GetData()
		{
			return this.GetOrCreatePegUIElement().GetData();
		}

		// Token: 0x0600B1DD RID: 45533 RVA: 0x0036D7DE File Offset: 0x0036B9DE
		public virtual void SetData(object data)
		{
			this.GetOrCreatePegUIElement().SetData(data);
		}

		// Token: 0x0600B1DE RID: 45534 RVA: 0x0036D7EC File Offset: 0x0036B9EC
		private void PopulateGeometryMeshesWithPegUIProxies(Geometry geo)
		{
			foreach (MeshFilter meshFilter in geo.GetComponentsInChildren<MeshFilter>(true))
			{
				this.PopulateMeshesWithPegUIProxies(meshFilter);
			}
		}

		// Token: 0x0600B1DF RID: 45535 RVA: 0x0036D81C File Offset: 0x0036BA1C
		private void PopulateMeshesWithPegUIProxies(MeshFilter meshFilter)
		{
			if (this.m_geometryColliders == null)
			{
				this.m_geometryColliders = new List<Collider>();
			}
			MeshCollider meshCollider = meshFilter.gameObject.AddComponent<MeshCollider>();
			meshCollider.hideFlags = HideFlags.DontSave;
			meshCollider.sharedMesh = meshFilter.sharedMesh;
			meshCollider.enabled = this.Active;
			this.m_geometryColliders.Add(meshCollider);
			meshCollider.gameObject.AddComponent<PegUIElementProxy>().Owner = this.m_pegUiElement;
		}

		// Token: 0x0600B1E0 RID: 45536 RVA: 0x0036D88C File Offset: 0x0036BA8C
		private void SetMeshCollidersEnabled(bool enable)
		{
			if (this.m_geometryColliders == null)
			{
				return;
			}
			foreach (Collider collider in this.m_geometryColliders)
			{
				if (!(collider == null))
				{
					collider.enabled = enable;
				}
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600B1E1 RID: 45537 RVA: 0x0036D8F4 File Offset: 0x0036BAF4
		public override bool IsChangingStates
		{
			get
			{
				return this.m_stateCollection == null || this.m_stateCollection.IsChangingStates;
			}
		}

		// Token: 0x0600B1E2 RID: 45538 RVA: 0x0036D90B File Offset: 0x0036BB0B
		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}

		// Token: 0x040095CF RID: 38351
		private readonly string ACTIVE = "active";

		// Token: 0x040095D0 RID: 38352
		private readonly string INACTIVE = "inactive";

		// Token: 0x040095D1 RID: 38353
		private readonly string SELECTED = "selected";

		// Token: 0x040095D2 RID: 38354
		private readonly string DESELECTED = "deselected";

		// Token: 0x040095D3 RID: 38355
		private readonly string CLICKED = "clicked";

		// Token: 0x040095D4 RID: 38356
		private readonly string RELEASED = "released";

		// Token: 0x040095D5 RID: 38357
		private readonly string MOUSEOVER = "mouseover";

		// Token: 0x040095D6 RID: 38358
		private readonly string MOUSEOUT = "mouseout";

		// Token: 0x040095D7 RID: 38359
		private readonly string DOUBLECLICKED = "doubleclicked";

		// Token: 0x040095D8 RID: 38360
		private readonly string DRAG_STARTED = "dragstarted";

		// Token: 0x040095D9 RID: 38361
		private readonly string DRAG_RELEASED = "dragreleased";

		// Token: 0x040095DA RID: 38362
		[SerializeField]
		[HideInInspector]
		private Clickable.ColliderType m_colliderType;

		// Token: 0x040095DB RID: 38363
		[SerializeField]
		[HideInInspector]
		private GameObject[] m_geometryRoots;

		// Token: 0x040095DC RID: 38364
		[SerializeField]
		[WidgetBehaviorStateEnum(typeof(Clickable.VisualState), "")]
		private WidgetBehaviorStateCollection m_stateCollection;

		// Token: 0x040095DD RID: 38365
		private List<Collider> m_geometryColliders;

		// Token: 0x040095DE RID: 38366
		private Collider m_boundsCollider;

		// Token: 0x040095DF RID: 38367
		private PegUIElement m_pegUiElement;

		// Token: 0x040095E0 RID: 38368
		private GameLayer? m_originalLayer;

		// Token: 0x040095E1 RID: 38369
		private bool m_active = true;

		// Token: 0x040095E2 RID: 38370
		private bool m_activeChanged = true;

		// Token: 0x040095E3 RID: 38371
		private bool m_hovered;

		// Token: 0x040095E4 RID: 38372
		private bool m_selected;

		// Token: 0x040095E5 RID: 38373
		private bool m_clicked;

		// Token: 0x040095E6 RID: 38374
		private bool m_dragged;

		// Token: 0x0200282A RID: 10282
		public enum VisualState
		{
			// Token: 0x0400F8B4 RID: 63668
			Active,
			// Token: 0x0400F8B5 RID: 63669
			Inactive,
			// Token: 0x0400F8B6 RID: 63670
			Selected,
			// Token: 0x0400F8B7 RID: 63671
			Deselected,
			// Token: 0x0400F8B8 RID: 63672
			Clicked,
			// Token: 0x0400F8B9 RID: 63673
			Released,
			// Token: 0x0400F8BA RID: 63674
			MouseOver,
			// Token: 0x0400F8BB RID: 63675
			MouseOut,
			// Token: 0x0400F8BC RID: 63676
			DoubleClicked,
			// Token: 0x0400F8BD RID: 63677
			DragStarted,
			// Token: 0x0400F8BE RID: 63678
			DragReleased
		}

		// Token: 0x0200282B RID: 10283
		public enum ColliderType
		{
			// Token: 0x0400F8C0 RID: 63680
			Bounds,
			// Token: 0x0400F8C1 RID: 63681
			Geometry
		}
	}
}
