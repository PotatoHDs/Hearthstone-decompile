using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	[SelectionBase]
	[NestedReferenceScope(NestedReference.Scope.Children)]
	public abstract class CustomWidgetBehavior : WidgetBehavior, IAsyncInitializationBehavior, IVisibleWidgetComponent
	{
		protected delegate void CreateObjectDelegate(IPreviewableObject previewableObject, Action<GameObject> createdCallback);

		protected delegate bool ShouldObjectBeRecreatedDelegate(IPreviewableObject previewableObject);

		protected interface IPreviewableObject
		{
			object Context { get; set; }
		}

		private class PreviewableObject : IPreviewableObject
		{
			public GameObject Object;

			public CreateObjectDelegate CreateObject;

			public ShouldObjectBeRecreatedDelegate ShouldObjectBeRecreated;

			public object Context { get; set; }

			public Exception FailureException { get; set; }

			public PreviewableObject(CreateObjectDelegate createObject, ShouldObjectBeRecreatedDelegate shouldObjectBeRecreated)
			{
				CreateObject = createObject;
				ShouldObjectBeRecreated = shouldObjectBeRecreated;
			}
		}

		private List<PreviewableObject> m_previewableObjects = new List<PreviewableObject>();

		private FlagStateTracker m_activatedEvent;

		private FlagStateTracker m_deactivatedEvent;

		private bool m_initialized;

		private Map<Renderer, int> m_originalRendererLayers;

		private FlagStateTracker m_readyStateTracker;

		public override bool IsChangingStates => false;

		public bool IsDesiredHidden => base.Owner.IsDesiredHidden;

		public bool IsDesiredHiddenInHierarchy
		{
			get
			{
				if (base.Owner.IsDesiredHiddenInHierarchy)
				{
					return true;
				}
				return false;
			}
		}

		public virtual bool HandlesChildVisibility => true;

		public bool IsReady
		{
			get
			{
				if (!m_readyStateTracker.IsSet)
				{
					return !m_initialized;
				}
				return true;
			}
		}

		public Behaviour Container => this;

		public virtual void SetVisibility(bool isVisible, bool isInternal)
		{
			SceneUtils.WalkSelfAndChildren(base.transform, delegate(Transform current)
			{
				bool flag = false;
				Component[] components = current.GetComponents<Component>();
				Renderer renderer = null;
				PegUIElement pegUIElement = null;
				UberText uberText = null;
				IVisibleWidgetComponent visibleWidgetComponent = null;
				Component[] array = components;
				foreach (Component component in array)
				{
					if (component is Renderer)
					{
						renderer = (Renderer)component;
					}
					else if (component is PegUIElement)
					{
						pegUIElement = (PegUIElement)component;
					}
					else if (component is UberText)
					{
						uberText = (UberText)component;
					}
					else if (component is IVisibleWidgetComponent)
					{
						visibleWidgetComponent = (IVisibleWidgetComponent)component;
					}
				}
				if (renderer != null)
				{
					SceneUtils.SetInvisibleRenderer(renderer, isVisible, ref m_originalRendererLayers);
				}
				if (pegUIElement != null && (base.InitializationStartTime > pegUIElement.SetEnabledLastCallTime || m_initialized))
				{
					pegUIElement.SetEnabled(isVisible, isInternal);
				}
				if (uberText != null)
				{
					if (isVisible)
					{
						uberText.Show();
					}
					else
					{
						uberText.Hide();
					}
					flag = true;
				}
				if (visibleWidgetComponent != null && (Component)visibleWidgetComponent != this)
				{
					visibleWidgetComponent.SetVisibility(isVisible && !visibleWidgetComponent.IsDesiredHidden, isInternal);
					flag = visibleWidgetComponent.HandlesChildVisibility;
				}
				return !flag;
			});
		}

		protected override void OnInitialize()
		{
			CleanUp();
			m_initialized = true;
			m_readyStateTracker.SetAndDispatch();
		}

		private void CleanUp()
		{
			OwnedByWidgetBehavior[] componentsInChildren = GetComponentsInChildren<OwnedByWidgetBehavior>();
			foreach (OwnedByWidgetBehavior ownedByWidgetBehavior in componentsInChildren)
			{
				if (ownedByWidgetBehavior.Owner == this)
				{
					HandleDestroy(ownedByWidgetBehavior.gameObject);
				}
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			m_activatedEvent.SetAndDispatch();
		}

		protected override void OnDisable()
		{
			m_deactivatedEvent.SetAndDispatch();
			base.OnDisable();
		}

		public void RegisterActivatedListener(Action<object> listener, object payload = null)
		{
			m_activatedEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet: false);
		}

		public void RegisterDeactivatedListener(Action<object> listener, object payload = null)
		{
			m_deactivatedEvent.RegisterSetListener(listener, payload, callImmediatelyIfSet: false);
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			PreviewableObject o;
			foreach (PreviewableObject previewableObject in m_previewableObjects)
			{
				if (previewableObject.FailureException != null)
				{
					continue;
				}
				try
				{
					if (!previewableObject.ShouldObjectBeRecreated(previewableObject))
					{
						continue;
					}
					m_readyStateTracker.Clear();
					if (previewableObject.Object != null)
					{
						HandleDestroy(previewableObject.Object);
					}
					o = previewableObject;
					previewableObject.CreateObject(previewableObject, delegate(GameObject obj)
					{
						o.Object = obj;
						if (obj != null)
						{
							if (obj.transform.parent != base.transform)
							{
								obj.transform.SetParent(base.transform, worldPositionStays: true);
							}
							obj.AddComponent<OwnedByWidgetBehavior>().Owner = this;
							SceneUtils.SetLayer(obj, base.gameObject.layer, 29);
							if (base.gameObject.layer == 31)
							{
								SceneUtils.WalkSelfAndChildren(obj.transform, delegate(Transform childTransform)
								{
									childTransform.gameObject.layer = 31;
									return true;
								});
							}
							if (IsDesiredHiddenInHierarchy)
							{
								SetVisibility(isVisible: false, isInternal: true);
							}
						}
						m_readyStateTracker.SetAndDispatch();
					});
				}
				catch (Exception ex)
				{
					Exception ex3 = (previewableObject.FailureException = ex);
					throw ex3;
				}
			}
		}

		public virtual void Hide()
		{
			if (m_initialized)
			{
				SetVisibility(isVisible: false, isInternal: false);
			}
		}

		public virtual void Show()
		{
			if (m_initialized)
			{
				SetVisibility(isVisible: true, isInternal: false);
			}
		}

		protected override void OnDestroy()
		{
			CleanUp();
			base.OnDestroy();
		}

		private void HandleDestroy(GameObject go)
		{
			Actor component = go.GetComponent<Actor>();
			if (component != null)
			{
				component.Destroy();
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(go);
			}
		}

		protected IPreviewableObject CreatePreviewableObject(CreateObjectDelegate createObject, ShouldObjectBeRecreatedDelegate shouldObjectBeRecreated, object context = null)
		{
			PreviewableObject previewableObject = new PreviewableObject(createObject, shouldObjectBeRecreated);
			m_previewableObjects.Add(previewableObject);
			previewableObject.Context = context;
			return previewableObject;
		}

		public void RegisterReadyListener(Action<object> listener, object payload, bool callImmediatelyIfReady = true)
		{
			m_readyStateTracker.RegisterSetListener(listener, payload, callImmediatelyIfReady);
		}

		public void RemoveReadyListener(Action<object> listener)
		{
			m_readyStateTracker.RemoveSetListener(listener);
		}
	}
}
