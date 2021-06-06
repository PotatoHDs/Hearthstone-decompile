using System;
using System.Collections.Generic;
using Hearthstone.UI.Internal;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001000 RID: 4096
	[ExecuteAlways]
	[AddComponentMenu("")]
	[SelectionBase]
	[NestedReferenceScope(NestedReference.Scope.Children)]
	public abstract class CustomWidgetBehavior : WidgetBehavior, IAsyncInitializationBehavior, IVisibleWidgetComponent
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600B1E5 RID: 45541 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public override bool IsChangingStates
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600B1E6 RID: 45542 RVA: 0x0036DAA4 File Offset: 0x0036BCA4
		public bool IsDesiredHidden
		{
			get
			{
				return base.Owner.IsDesiredHidden;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x0600B1E7 RID: 45543 RVA: 0x0036DAB1 File Offset: 0x0036BCB1
		public bool IsDesiredHiddenInHierarchy
		{
			get
			{
				return base.Owner.IsDesiredHiddenInHierarchy;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x0600B1E8 RID: 45544 RVA: 0x000052EC File Offset: 0x000034EC
		public virtual bool HandlesChildVisibility
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B1E9 RID: 45545 RVA: 0x0036DAC4 File Offset: 0x0036BCC4
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
				foreach (Component component in components)
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
					SceneUtils.SetInvisibleRenderer(renderer, isVisible, ref this.m_originalRendererLayers);
				}
				if (pegUIElement != null && (this.InitializationStartTime > pegUIElement.SetEnabledLastCallTime || this.m_initialized))
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

		// Token: 0x0600B1EA RID: 45546 RVA: 0x0036DB03 File Offset: 0x0036BD03
		protected override void OnInitialize()
		{
			this.CleanUp();
			this.m_initialized = true;
			this.m_readyStateTracker.SetAndDispatch();
		}

		// Token: 0x0600B1EB RID: 45547 RVA: 0x0036DB20 File Offset: 0x0036BD20
		private void CleanUp()
		{
			foreach (OwnedByWidgetBehavior ownedByWidgetBehavior in base.GetComponentsInChildren<OwnedByWidgetBehavior>())
			{
				if (ownedByWidgetBehavior.Owner == this)
				{
					this.HandleDestroy(ownedByWidgetBehavior.gameObject);
				}
			}
		}

		// Token: 0x0600B1EC RID: 45548 RVA: 0x0036DB60 File Offset: 0x0036BD60
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_activatedEvent.SetAndDispatch();
		}

		// Token: 0x0600B1ED RID: 45549 RVA: 0x0036DB74 File Offset: 0x0036BD74
		protected override void OnDisable()
		{
			this.m_deactivatedEvent.SetAndDispatch();
			base.OnDisable();
		}

		// Token: 0x0600B1EE RID: 45550 RVA: 0x0036DB88 File Offset: 0x0036BD88
		public void RegisterActivatedListener(Action<object> listener, object payload = null)
		{
			this.m_activatedEvent.RegisterSetListener(listener, payload, false, false);
		}

		// Token: 0x0600B1EF RID: 45551 RVA: 0x0036DB99 File Offset: 0x0036BD99
		public void RegisterDeactivatedListener(Action<object> listener, object payload = null)
		{
			this.m_deactivatedEvent.RegisterSetListener(listener, payload, false, false);
		}

		// Token: 0x0600B1F0 RID: 45552 RVA: 0x0036DBAC File Offset: 0x0036BDAC
		public override void OnUpdate()
		{
			base.OnUpdate();
			foreach (CustomWidgetBehavior.PreviewableObject previewableObject in this.m_previewableObjects)
			{
				if (previewableObject.FailureException == null)
				{
					try
					{
						if (previewableObject.ShouldObjectBeRecreated(previewableObject))
						{
							this.m_readyStateTracker.Clear();
							if (previewableObject.Object != null)
							{
								this.HandleDestroy(previewableObject.Object);
							}
							CustomWidgetBehavior.PreviewableObject o = previewableObject;
							previewableObject.CreateObject(previewableObject, delegate(GameObject obj)
							{
								o.Object = obj;
								if (obj != null)
								{
									if (obj.transform.parent != this.transform)
									{
										obj.transform.SetParent(this.transform, true);
									}
									obj.AddComponent<OwnedByWidgetBehavior>().Owner = this;
									SceneUtils.SetLayer(obj, this.gameObject.layer, new int?(29));
									if (this.gameObject.layer == 31)
									{
										SceneUtils.WalkSelfAndChildren(obj.transform, delegate(Transform childTransform)
										{
											childTransform.gameObject.layer = 31;
											return true;
										});
									}
									if (this.IsDesiredHiddenInHierarchy)
									{
										this.SetVisibility(false, true);
									}
								}
								this.m_readyStateTracker.SetAndDispatch();
							});
						}
					}
					catch (Exception ex)
					{
						previewableObject.FailureException = ex;
						throw ex;
					}
				}
			}
		}

		// Token: 0x0600B1F1 RID: 45553 RVA: 0x0036DC84 File Offset: 0x0036BE84
		public virtual void Hide()
		{
			if (this.m_initialized)
			{
				this.SetVisibility(false, false);
			}
		}

		// Token: 0x0600B1F2 RID: 45554 RVA: 0x0036DC96 File Offset: 0x0036BE96
		public virtual void Show()
		{
			if (this.m_initialized)
			{
				this.SetVisibility(true, false);
			}
		}

		// Token: 0x0600B1F3 RID: 45555 RVA: 0x0036DCA8 File Offset: 0x0036BEA8
		protected override void OnDestroy()
		{
			this.CleanUp();
			base.OnDestroy();
		}

		// Token: 0x0600B1F4 RID: 45556 RVA: 0x0036DCB8 File Offset: 0x0036BEB8
		private void HandleDestroy(GameObject go)
		{
			Actor component = go.GetComponent<Actor>();
			if (component != null)
			{
				component.Destroy();
				return;
			}
			UnityEngine.Object.DestroyImmediate(go);
		}

		// Token: 0x0600B1F5 RID: 45557 RVA: 0x0036DCE4 File Offset: 0x0036BEE4
		protected CustomWidgetBehavior.IPreviewableObject CreatePreviewableObject(CustomWidgetBehavior.CreateObjectDelegate createObject, CustomWidgetBehavior.ShouldObjectBeRecreatedDelegate shouldObjectBeRecreated, object context = null)
		{
			CustomWidgetBehavior.PreviewableObject previewableObject = new CustomWidgetBehavior.PreviewableObject(createObject, shouldObjectBeRecreated);
			this.m_previewableObjects.Add(previewableObject);
			previewableObject.Context = context;
			return previewableObject;
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x0600B1F6 RID: 45558 RVA: 0x0036DD0D File Offset: 0x0036BF0D
		public bool IsReady
		{
			get
			{
				return this.m_readyStateTracker.IsSet || !this.m_initialized;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x0600B1F7 RID: 45559 RVA: 0x00005576 File Offset: 0x00003776
		public Behaviour Container
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600B1F8 RID: 45560 RVA: 0x0036DD27 File Offset: 0x0036BF27
		public void RegisterReadyListener(Action<object> listener, object payload, bool callImmediatelyIfReady = true)
		{
			this.m_readyStateTracker.RegisterSetListener(listener, payload, callImmediatelyIfReady, false);
		}

		// Token: 0x0600B1F9 RID: 45561 RVA: 0x0036DD38 File Offset: 0x0036BF38
		public void RemoveReadyListener(Action<object> listener)
		{
			this.m_readyStateTracker.RemoveSetListener(listener);
		}

		// Token: 0x040095E7 RID: 38375
		private List<CustomWidgetBehavior.PreviewableObject> m_previewableObjects = new List<CustomWidgetBehavior.PreviewableObject>();

		// Token: 0x040095E8 RID: 38376
		private FlagStateTracker m_activatedEvent;

		// Token: 0x040095E9 RID: 38377
		private FlagStateTracker m_deactivatedEvent;

		// Token: 0x040095EA RID: 38378
		private bool m_initialized;

		// Token: 0x040095EB RID: 38379
		private Map<Renderer, int> m_originalRendererLayers;

		// Token: 0x040095EC RID: 38380
		private FlagStateTracker m_readyStateTracker;

		// Token: 0x0200282D RID: 10285
		// (Invoke) Token: 0x06013B30 RID: 80688
		protected delegate void CreateObjectDelegate(CustomWidgetBehavior.IPreviewableObject previewableObject, Action<GameObject> createdCallback);

		// Token: 0x0200282E RID: 10286
		// (Invoke) Token: 0x06013B34 RID: 80692
		protected delegate bool ShouldObjectBeRecreatedDelegate(CustomWidgetBehavior.IPreviewableObject previewableObject);

		// Token: 0x0200282F RID: 10287
		protected interface IPreviewableObject
		{
			// Token: 0x17002D21 RID: 11553
			// (get) Token: 0x06013B38 RID: 80696
			// (set) Token: 0x06013B37 RID: 80695
			object Context { get; set; }
		}

		// Token: 0x02002830 RID: 10288
		private class PreviewableObject : CustomWidgetBehavior.IPreviewableObject
		{
			// Token: 0x17002D22 RID: 11554
			// (get) Token: 0x06013B39 RID: 80697 RVA: 0x0053A8B3 File Offset: 0x00538AB3
			// (set) Token: 0x06013B3A RID: 80698 RVA: 0x0053A8BB File Offset: 0x00538ABB
			public object Context { get; set; }

			// Token: 0x17002D23 RID: 11555
			// (get) Token: 0x06013B3B RID: 80699 RVA: 0x0053A8C4 File Offset: 0x00538AC4
			// (set) Token: 0x06013B3C RID: 80700 RVA: 0x0053A8CC File Offset: 0x00538ACC
			public Exception FailureException { get; set; }

			// Token: 0x06013B3D RID: 80701 RVA: 0x0053A8D5 File Offset: 0x00538AD5
			public PreviewableObject(CustomWidgetBehavior.CreateObjectDelegate createObject, CustomWidgetBehavior.ShouldObjectBeRecreatedDelegate shouldObjectBeRecreated)
			{
				this.CreateObject = createObject;
				this.ShouldObjectBeRecreated = shouldObjectBeRecreated;
			}

			// Token: 0x0400F8C4 RID: 63684
			public GameObject Object;

			// Token: 0x0400F8C5 RID: 63685
			public CustomWidgetBehavior.CreateObjectDelegate CreateObject;

			// Token: 0x0400F8C6 RID: 63686
			public CustomWidgetBehavior.ShouldObjectBeRecreatedDelegate ShouldObjectBeRecreated;
		}
	}
}
