using System.Collections.Generic;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class Clickable : WidgetBehavior, IPopupRendering, IBoundsDependent, ILayerOverridable
	{
		public enum VisualState
		{
			Active,
			Inactive,
			Selected,
			Deselected,
			Clicked,
			Released,
			MouseOver,
			MouseOut,
			DoubleClicked,
			DragStarted,
			DragReleased
		}

		public enum ColliderType
		{
			Bounds,
			Geometry
		}

		private readonly string ACTIVE = "active";

		private readonly string INACTIVE = "inactive";

		private readonly string SELECTED = "selected";

		private readonly string DESELECTED = "deselected";

		private readonly string CLICKED = "clicked";

		private readonly string RELEASED = "released";

		private readonly string MOUSEOVER = "mouseover";

		private readonly string MOUSEOUT = "mouseout";

		private readonly string DOUBLECLICKED = "doubleclicked";

		private readonly string DRAG_STARTED = "dragstarted";

		private readonly string DRAG_RELEASED = "dragreleased";

		[SerializeField]
		[HideInInspector]
		private ColliderType m_colliderType;

		[SerializeField]
		[HideInInspector]
		private GameObject[] m_geometryRoots;

		[SerializeField]
		[WidgetBehaviorStateEnum(typeof(VisualState), "")]
		private WidgetBehaviorStateCollection m_stateCollection;

		private List<Collider> m_geometryColliders;

		private Collider m_boundsCollider;

		private PegUIElement m_pegUiElement;

		private GameLayer? m_originalLayer;

		private bool m_active = true;

		private bool m_activeChanged = true;

		private bool m_hovered;

		private bool m_selected;

		private bool m_clicked;

		private bool m_dragged;

		[Overridable]
		public bool Active
		{
			get
			{
				if (m_active)
				{
					return base.IsActive;
				}
				return false;
			}
			set
			{
				if (m_active != value)
				{
					m_activeChanged = true;
					m_active = value;
					if (!m_active)
					{
						OnRelease(null);
						OnReleaseAll(null);
						OnDeselected(null);
						OnRollOut(null);
					}
					if (m_pegUiElement != null)
					{
						m_pegUiElement.SetEnabled(value);
					}
					if (m_boundsCollider != null)
					{
						m_boundsCollider.enabled = value && base.IsActive;
					}
					SetMeshCollidersEnabled(value && base.IsActive);
				}
			}
		}

		public bool NeedsBounds => m_colliderType == ColliderType.Bounds;

		public bool HandlesChildLayers => false;

		public override bool IsChangingStates
		{
			get
			{
				if (m_stateCollection != null)
				{
					return m_stateCollection.IsChangingStates;
				}
				return true;
			}
		}

		protected override void OnDisable()
		{
			if (m_boundsCollider != null)
			{
				m_boundsCollider.enabled = false;
			}
			SetMeshCollidersEnabled(enable: false);
			base.OnDisable();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (m_boundsCollider != null)
			{
				m_boundsCollider.enabled = m_active;
			}
			SetMeshCollidersEnabled(m_active);
		}

		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			SetLayerOverride(GameLayer.Reserved29);
		}

		public void DisablePopupRendering()
		{
			ClearLayerOverride();
		}

		public bool ShouldPropagatePopupRendering()
		{
			return true;
		}

		private PegUIElement GetOrCreatePegUIElement()
		{
			if (m_pegUiElement == null)
			{
				m_pegUiElement = base.gameObject.GetComponent<PegUIElement>() ?? base.gameObject.AddComponent<PegUIElement>();
				m_pegUiElement.hideFlags = HideFlags.DontSave;
			}
			return m_pegUiElement;
		}

		protected override void OnInitialize()
		{
			if (m_stateCollection == null)
			{
				m_stateCollection = new WidgetBehaviorStateCollection();
			}
			if (!Application.IsPlaying(this))
			{
				return;
			}
			PegUIElement orCreatePegUIElement = GetOrCreatePegUIElement();
			orCreatePegUIElement.AddEventListener(UIEventType.RELEASE, OnRelease);
			orCreatePegUIElement.AddEventListener(UIEventType.RELEASEALL, OnReleaseAll);
			orCreatePegUIElement.AddEventListener(UIEventType.ROLLOVER, OnRollOver);
			orCreatePegUIElement.AddEventListener(UIEventType.ROLLOUT, OnRollOut);
			orCreatePegUIElement.AddEventListener(UIEventType.PRESS, OnClick);
			orCreatePegUIElement.AddEventListener(UIEventType.TAP, OnSelected);
			if (m_stateCollection.DoesStateExist(DOUBLECLICKED))
			{
				orCreatePegUIElement.AddEventListener(UIEventType.DOUBLECLICK, OnDoubleClick);
			}
			orCreatePegUIElement.AddEventListener(UIEventType.DRAG, OnDrag);
			orCreatePegUIElement.SetEnabled(m_active && !base.Owner.IsDesiredHiddenInHierarchy, isInternal: true);
			switch (m_colliderType)
			{
			case ColliderType.Bounds:
			{
				WidgetTransform component = base.gameObject.GetComponent<WidgetTransform>();
				if (component != null)
				{
					m_boundsCollider = component.CreateBoxCollider(base.gameObject);
				}
				break;
			}
			case ColliderType.Geometry:
			{
				GameObject[] geometryRoots = m_geometryRoots;
				foreach (GameObject gameObject in geometryRoots)
				{
					if (!(gameObject == null))
					{
						ApplyPegUIProxies(gameObject.transform);
					}
				}
				break;
			}
			}
		}

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
				foreach (Component component in components)
				{
					MeshFilter meshFilter = component as MeshFilter;
					if (meshFilter != null)
					{
						PopulateMeshesWithPegUIProxies(meshFilter);
					}
					else
					{
						Geometry geometry = component as Geometry;
						if (geometry != null)
						{
							geometry.RegisterReadyListener(delegate
							{
								PopulateGeometryMeshesWithPegUIProxies(geometry);
							});
						}
					}
				}
				List<IAsyncInitializationBehavior> asyncBehaviors = AsyncBehaviorUtils.GetAsyncBehaviors(child);
				if (asyncBehaviors != null)
				{
					foreach (IAsyncInitializationBehavior item in asyncBehaviors)
					{
						item.RegisterReadyListener(ApplyPegUIProxies, child, callImmediatelyIfReady: false);
					}
				}
				return true;
			});
		}

		private void OnSelected(UIEvent e)
		{
			if (!m_selected && Active)
			{
				m_selected = true;
				SetVisualState(VisualState.Selected);
			}
		}

		private void OnDeselected(UIEvent e)
		{
			if (m_selected)
			{
				m_selected = false;
				SetVisualState(VisualState.Deselected);
			}
		}

		private void OnRelease(UIEvent e)
		{
			if (m_clicked)
			{
				m_clicked = false;
				SetVisualState(VisualState.Released);
			}
			if (m_dragged)
			{
				m_dragged = false;
				SetVisualState(VisualState.DragReleased);
			}
		}

		private void OnReleaseAll(UIEvent e)
		{
			m_clicked = false;
			if (m_dragged)
			{
				m_dragged = false;
				SetVisualState(VisualState.DragReleased);
			}
		}

		private void OnRollOut(UIEvent e)
		{
			if (m_hovered)
			{
				m_hovered = false;
				SetVisualState(VisualState.MouseOut);
			}
		}

		private void OnRollOver(UIEvent e)
		{
			if (!m_hovered && Active)
			{
				m_hovered = true;
				SetVisualState(VisualState.MouseOver);
			}
		}

		private void OnClick(UIEvent e)
		{
			if (!m_clicked && Active)
			{
				m_clicked = true;
				SetVisualState(VisualState.Clicked);
			}
		}

		private void OnDoubleClick(UIEvent e)
		{
			if (m_clicked && Active)
			{
				m_clicked = false;
				SetVisualState(VisualState.DoubleClicked);
			}
		}

		private void OnDrag(UIEvent e)
		{
			if (!m_dragged && Active)
			{
				m_dragged = true;
				SetVisualState(VisualState.DragStarted);
			}
		}

		public override void OnUpdate()
		{
			if (m_activeChanged)
			{
				SetVisualState((!m_active) ? VisualState.Inactive : VisualState.Active);
				m_activeChanged = false;
			}
			if (m_stateCollection != null)
			{
				m_stateCollection.Update(this);
			}
		}

		private bool SetVisualState(VisualState visualState)
		{
			m_stateCollection.IndependentStates = true;
			switch (visualState)
			{
			case VisualState.Active:
				m_stateCollection.AbortState(INACTIVE);
				return m_stateCollection.ActivateState(this, ACTIVE);
			case VisualState.Inactive:
				m_stateCollection.AbortState(ACTIVE);
				return m_stateCollection.ActivateState(this, INACTIVE);
			case VisualState.Selected:
				m_stateCollection.AbortState(DESELECTED);
				return m_stateCollection.ActivateState(this, SELECTED);
			case VisualState.Deselected:
				m_stateCollection.AbortState(SELECTED);
				return m_stateCollection.ActivateState(this, DESELECTED);
			case VisualState.Clicked:
				m_stateCollection.AbortState(RELEASED);
				return m_stateCollection.ActivateState(this, CLICKED);
			case VisualState.Released:
				m_stateCollection.AbortState(CLICKED);
				return m_stateCollection.ActivateState(this, RELEASED);
			case VisualState.MouseOver:
				m_stateCollection.AbortState(MOUSEOUT);
				return m_stateCollection.ActivateState(this, MOUSEOVER);
			case VisualState.MouseOut:
				m_stateCollection.AbortState(MOUSEOVER);
				return m_stateCollection.ActivateState(this, MOUSEOUT);
			case VisualState.DoubleClicked:
				m_stateCollection.AbortState(RELEASED);
				return m_stateCollection.ActivateState(this, DOUBLECLICKED);
			case VisualState.DragStarted:
				m_stateCollection.AbortState(DRAG_RELEASED);
				return m_stateCollection.ActivateState(this, DRAG_STARTED);
			case VisualState.DragReleased:
				m_stateCollection.AbortState(DRAG_STARTED);
				return m_stateCollection.ActivateState(this, DRAG_RELEASED);
			default:
				return false;
			}
		}

		public void SetLayerOverride(GameLayer layer)
		{
			if (!m_originalLayer.HasValue)
			{
				GameObject gameObject = base.gameObject;
				m_originalLayer = (GameLayer)gameObject.layer;
				gameObject.layer = (int)layer;
			}
		}

		public void ClearLayerOverride()
		{
			if (m_originalLayer.HasValue)
			{
				base.gameObject.layer = (int)m_originalLayer.Value;
				m_originalLayer = null;
			}
		}

		public virtual bool AddEventListener(UIEventType type, UIEvent.Handler handler)
		{
			return GetOrCreatePegUIElement().AddEventListener(type, handler);
		}

		public virtual object GetData()
		{
			return GetOrCreatePegUIElement().GetData();
		}

		public virtual void SetData(object data)
		{
			GetOrCreatePegUIElement().SetData(data);
		}

		private void PopulateGeometryMeshesWithPegUIProxies(Geometry geo)
		{
			MeshFilter[] componentsInChildren = geo.GetComponentsInChildren<MeshFilter>(includeInactive: true);
			foreach (MeshFilter meshFilter in componentsInChildren)
			{
				PopulateMeshesWithPegUIProxies(meshFilter);
			}
		}

		private void PopulateMeshesWithPegUIProxies(MeshFilter meshFilter)
		{
			if (m_geometryColliders == null)
			{
				m_geometryColliders = new List<Collider>();
			}
			MeshCollider meshCollider = meshFilter.gameObject.AddComponent<MeshCollider>();
			meshCollider.hideFlags = HideFlags.DontSave;
			meshCollider.sharedMesh = meshFilter.sharedMesh;
			meshCollider.enabled = Active;
			m_geometryColliders.Add(meshCollider);
			meshCollider.gameObject.AddComponent<PegUIElementProxy>().Owner = m_pegUiElement;
		}

		private void SetMeshCollidersEnabled(bool enable)
		{
			if (m_geometryColliders == null)
			{
				return;
			}
			foreach (Collider geometryCollider in m_geometryColliders)
			{
				if (!(geometryCollider == null))
				{
					geometryCollider.enabled = enable;
				}
			}
		}

		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}
	}
}
