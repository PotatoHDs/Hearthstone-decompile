using System;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001003 RID: 4099
	[ExecuteAlways]
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class Maskable : WidgetBehavior, IPopupRendering, IBoundsDependent, IVisibleWidgetComponent
	{
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600B234 RID: 45620 RVA: 0x0036EAF5 File Offset: 0x0036CCF5
		// (set) Token: 0x0600B233 RID: 45619 RVA: 0x0036EAE2 File Offset: 0x0036CCE2
		private bool Active
		{
			get
			{
				return base.IsActive;
			}
			set
			{
				if (base.enabled == value)
				{
					return;
				}
				base.enabled = value;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600B235 RID: 45621 RVA: 0x000052EC File Offset: 0x000034EC
		public bool NeedsBounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B236 RID: 45622 RVA: 0x0036EAFD File Offset: 0x0036CCFD
		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			this.m_popupRoot = popupRoot;
			base.Owner.RegisterReadyListener(new Action<object>(this.ApplyPopupRendering), null, true);
		}

		// Token: 0x0600B237 RID: 45623 RVA: 0x0036EB1F File Offset: 0x0036CD1F
		public void DisablePopupRendering()
		{
			this.m_popupRoot = null;
			this.RemovePopupRendering(null);
		}

		// Token: 0x0600B238 RID: 45624 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool ShouldPropagatePopupRendering()
		{
			return false;
		}

		// Token: 0x0600B239 RID: 45625 RVA: 0x0036EB30 File Offset: 0x0036CD30
		private void ApplyPopupRendering(object unused)
		{
			if (this.m_popupRoot == null)
			{
				return;
			}
			foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>(true))
			{
				PopupRenderer popupRenderer = renderer.GetComponent<PopupRenderer>() ?? renderer.gameObject.AddComponent<PopupRenderer>();
				if (this.Active)
				{
					popupRenderer.SetLayerOverride(GameLayer.CameraMask);
				}
				else
				{
					popupRenderer.ClearLayerOverride();
				}
				popupRenderer.EnablePopupRendering(this.m_popupRoot);
			}
			this.SetupMaskingCamera();
		}

		// Token: 0x0600B23A RID: 45626 RVA: 0x0036EBA8 File Offset: 0x0036CDA8
		private void RemovePopupRendering(object unused)
		{
			Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				PopupRenderer component = componentsInChildren[i].GetComponent<PopupRenderer>();
				if (component != null)
				{
					if (this.Active)
					{
						component.SetLayerOverride(GameLayer.CameraMask);
					}
					else
					{
						component.ClearLayerOverride();
					}
					component.DisablePopupRendering();
				}
			}
			this.SetupMaskingCamera();
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x0600B23B RID: 45627 RVA: 0x0036DAA4 File Offset: 0x0036BCA4
		public bool IsDesiredHidden
		{
			get
			{
				return base.Owner.IsDesiredHidden;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x0600B23C RID: 45628 RVA: 0x0036DAB1 File Offset: 0x0036BCB1
		public bool IsDesiredHiddenInHierarchy
		{
			get
			{
				return base.Owner.IsDesiredHiddenInHierarchy;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x0600B23D RID: 45629 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public bool HandlesChildVisibility
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B23E RID: 45630 RVA: 0x0036EC01 File Offset: 0x0036CE01
		public void SetVisibility(bool isVisible, bool isInternal)
		{
			if (this.m_maskCamera != null)
			{
				if (isVisible)
				{
					this.m_maskCamera.enabled = (this.m_initialized && this.Active);
					return;
				}
				this.m_maskCamera.enabled = false;
			}
		}

		// Token: 0x0600B23F RID: 45631 RVA: 0x0036EC3D File Offset: 0x0036CE3D
		protected override void OnInitialize()
		{
			if (!Application.IsPlaying(this))
			{
				return;
			}
			this.m_initialized = true;
			this.SetupMaskingCamera();
			if (this.Active)
			{
				this.ActivateMask();
			}
		}

		// Token: 0x0600B240 RID: 45632 RVA: 0x0036EC63 File Offset: 0x0036CE63
		public override void OnUpdate()
		{
			base.OnUpdate();
			this.UpdateCameraClipping();
		}

		// Token: 0x0600B241 RID: 45633 RVA: 0x0036EC71 File Offset: 0x0036CE71
		public void Hide()
		{
			this.SetVisibility(false, false);
		}

		// Token: 0x0600B242 RID: 45634 RVA: 0x0036EC7B File Offset: 0x0036CE7B
		public void Show()
		{
			this.SetVisibility(true, false);
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600B243 RID: 45635 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public override bool IsChangingStates
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B244 RID: 45636 RVA: 0x0036EC88 File Offset: 0x0036CE88
		private void SetupMaskingCamera()
		{
			this.m_renderCamera = this.GetRenderCamera();
			if (this.m_renderCamera == null)
			{
				return;
			}
			GameObject gameObject;
			if (this.m_maskCamera == null)
			{
				gameObject = new GameObject("UI_Maskable_Camera (Widget: " + base.Owner.name + ")");
				gameObject.AddComponent<RenderToTextureCamera>();
				SceneUtils.SetLayer(gameObject, GameLayer.CameraMask);
				this.m_maskCamera = gameObject.AddComponent<Camera>();
			}
			else
			{
				gameObject = this.m_maskCamera.gameObject;
			}
			gameObject.transform.parent = this.m_renderCamera.gameObject.transform;
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			this.m_maskCamera.CopyFrom(this.m_renderCamera);
			this.m_maskCamera.clearFlags = CameraClearFlags.Depth;
			this.m_maskCamera.cullingMask = this.GetCullingMask();
			this.m_maskCamera.depth = this.m_renderCamera.depth + 0.01f;
			this.m_maskCamera.enabled = (this.m_initialized && this.Active);
			this.UpdateCameraClipping();
		}

		// Token: 0x0600B245 RID: 45637 RVA: 0x0036EDC0 File Offset: 0x0036CFC0
		private int GetCullingMask()
		{
			int num = GameLayer.CameraMask.LayerBit();
			if (this.m_popupRoot != null)
			{
				num |= this.m_popupRoot.PopupCamera.CullingMask;
			}
			else if (this.m_renderCamera != null)
			{
				num |= this.m_renderCamera.cullingMask;
			}
			return num;
		}

		// Token: 0x0600B246 RID: 45638 RVA: 0x0036EE15 File Offset: 0x0036D015
		private Camera GetRenderCamera()
		{
			if (this.m_popupRoot != null)
			{
				return this.m_popupRoot.PopupCamera.Camera;
			}
			return CameraUtils.FindFirstByLayer(base.gameObject.layer);
		}

		// Token: 0x0600B247 RID: 45639 RVA: 0x0036EE48 File Offset: 0x0036D048
		private void ActivateMask()
		{
			if (!this.m_initialized)
			{
				return;
			}
			if (!base.Owner.IsInitialized || base.Owner.IsChangingStates)
			{
				base.Owner.RegisterDoneChangingStatesListener(delegate(object _)
				{
					this.ApplyMaskingLayer(base.transform, false);
					if (!this.m_registeredComponents)
					{
						this.RegisterStatefulComponents();
					}
				}, null, true, true);
			}
			else
			{
				this.ApplyMaskingLayer(base.transform, false);
				if (!this.m_registeredComponents)
				{
					this.RegisterStatefulComponents();
				}
			}
			this.ApplyPopupRendering(null);
			if (this.m_maskCamera != null)
			{
				this.m_maskCamera.enabled = !this.IsDesiredHiddenInHierarchy;
			}
			PegUI pegUI = PegUI.Get();
			if (pegUI != null)
			{
				pegUI.RegisterForCameraDepthPriorityHitTest(this);
			}
		}

		// Token: 0x0600B248 RID: 45640 RVA: 0x0036EEF0 File Offset: 0x0036D0F0
		private void DeactivateMask()
		{
			if (this.m_maskCamera != null)
			{
				this.m_maskCamera.enabled = false;
			}
			PegUI pegUI = PegUI.Get();
			if (pegUI != null)
			{
				pegUI.UnregisterForCameraDepthPriorityHitTest(this);
			}
			this.RemoveMaskingLayer(base.transform, false);
			this.ApplyPopupRendering(null);
		}

		// Token: 0x0600B249 RID: 45641 RVA: 0x0036EF41 File Offset: 0x0036D141
		private void ApplyMaskingLayer(object root, bool ignoreChildren = false)
		{
			if (this == null)
			{
				return;
			}
			base.Owner.SetLayerOverrideForObject(GameLayer.CameraMask, base.gameObject, null);
		}

		// Token: 0x0600B24A RID: 45642 RVA: 0x0036EF61 File Offset: 0x0036D161
		private void RemoveMaskingLayer(object root, bool ignoreChildren = false)
		{
			if (this == null)
			{
				return;
			}
			base.Owner.SetLayerOverrideForObject(GameLayer.Default, base.gameObject, null);
		}

		// Token: 0x0600B24B RID: 45643 RVA: 0x0036EF80 File Offset: 0x0036D180
		private void RegisterStatefulComponents()
		{
			SceneUtils.WalkSelfAndChildren(base.transform, delegate(Transform current)
			{
				IStatefulWidgetComponent[] components = current.GetComponents<IStatefulWidgetComponent>();
				bool result = true;
				if (components != null)
				{
					IStatefulWidgetComponent[] array = components;
					for (int i = 0; i < array.Length; i++)
					{
						IStatefulWidgetComponent component = array[i];
						if (!((UnityEngine.Object)component == this) && !(component is VisualController) && !(component is Clickable))
						{
							component.RegisterDoneChangingStatesListener(delegate(object _)
							{
								this.HandleComponentDoneChangingStates(component);
							}, null, true, false);
							result = false;
						}
					}
				}
				return result;
			});
			this.m_registeredComponents = true;
		}

		// Token: 0x0600B24C RID: 45644 RVA: 0x0036EFA0 File Offset: 0x0036D1A0
		private void HandleComponentDoneChangingStates(IStatefulWidgetComponent statefulComponent)
		{
			if (this.Active)
			{
				Component component = (Component)statefulComponent;
				if (component != null)
				{
					this.ApplyMaskingLayer(component.gameObject, false);
				}
			}
		}

		// Token: 0x0600B24D RID: 45645 RVA: 0x0036EFD4 File Offset: 0x0036D1D4
		private void UpdateCameraClipping()
		{
			if (this.m_renderCamera == null || this.m_maskCamera == null)
			{
				return;
			}
			WidgetTransform component = base.GetComponent<WidgetTransform>();
			if (component == null)
			{
				return;
			}
			Rect rect = component.Rect;
			Vector3 vector = this.m_renderCamera.WorldToViewportPoint(base.transform.TransformPoint(rect.xMin, 0f, rect.yMin));
			Vector3 vector2 = this.m_renderCamera.WorldToViewportPoint(base.transform.TransformPoint(rect.xMax, 0f, rect.yMax));
			float num = Mathf.Clamp(vector.x, 0f, 1f);
			float num2 = Mathf.Clamp(vector.y, 0f, 1f);
			float num3 = Mathf.Clamp(vector2.x, 0f, 1f);
			float num4 = Mathf.Clamp(vector2.y, 0f, 1f);
			Rect rect2 = new Rect(num, num2, num3 - num, num4 - num2);
			if (Mathf.Approximately(0f, rect2.height) || Mathf.Approximately(0f, rect2.width))
			{
				return;
			}
			this.m_maskCamera.rect = new Rect(0f, 0f, 1f, 1f);
			this.m_maskCamera.ResetProjectionMatrix();
			Matrix4x4 projectionMatrix = this.m_maskCamera.projectionMatrix;
			Matrix4x4 projectionMatrix2 = Matrix4x4.TRS(new Vector3(-rect2.x * 2f / rect2.width, -rect2.y * 2f / rect2.height, 0f), Quaternion.identity, Vector3.one) * Matrix4x4.TRS(new Vector3(1f / rect2.width - 1f, 1f / rect2.height - 1f, 0f), Quaternion.identity, new Vector3(1f / rect2.width, 1f / rect2.height, 1f)) * projectionMatrix;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (float.IsNaN(projectionMatrix2[i, j]))
					{
						return;
					}
				}
			}
			this.m_maskCamera.rect = rect2;
			this.m_maskCamera.projectionMatrix = projectionMatrix2;
		}

		// Token: 0x0600B24E RID: 45646 RVA: 0x0036F237 File Offset: 0x0036D437
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_initialized)
			{
				this.ActivateMask();
			}
		}

		// Token: 0x0600B24F RID: 45647 RVA: 0x0036F24D File Offset: 0x0036D44D
		protected override void OnDisable()
		{
			this.DeactivateMask();
			base.OnDisable();
		}

		// Token: 0x0600B250 RID: 45648 RVA: 0x0036F25C File Offset: 0x0036D45C
		protected override void OnDestroy()
		{
			PegUI pegUI = PegUI.Get();
			if (pegUI != null)
			{
				pegUI.UnregisterForCameraDepthPriorityHitTest(this);
			}
			if (this.m_maskCamera != null)
			{
				UnityEngine.Object.Destroy(this.m_maskCamera.gameObject);
			}
			base.OnDestroy();
		}

		// Token: 0x0600B251 RID: 45649 RVA: 0x0036D90B File Offset: 0x0036BB0B
		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}

		// Token: 0x04009601 RID: 38401
		private bool m_initialized;

		// Token: 0x04009602 RID: 38402
		private bool m_registeredComponents;

		// Token: 0x04009603 RID: 38403
		private Camera m_maskCamera;

		// Token: 0x04009604 RID: 38404
		private Camera m_renderCamera;

		// Token: 0x04009605 RID: 38405
		private PopupRoot m_popupRoot;
	}
}
