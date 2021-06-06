using System.Diagnostics;
using Hearthstone.UI.Logging;
using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	[DisallowMultipleComponent]
	public class Maskable : WidgetBehavior, IPopupRendering, IBoundsDependent, IVisibleWidgetComponent
	{
		private bool m_initialized;

		private bool m_registeredComponents;

		private Camera m_maskCamera;

		private Camera m_renderCamera;

		private PopupRoot m_popupRoot;

		private bool Active
		{
			get
			{
				return base.IsActive;
			}
			set
			{
				if (base.enabled != value)
				{
					base.enabled = value;
				}
			}
		}

		public bool NeedsBounds => true;

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

		public bool HandlesChildVisibility => false;

		public override bool IsChangingStates => false;

		public void EnablePopupRendering(PopupRoot popupRoot)
		{
			m_popupRoot = popupRoot;
			base.Owner.RegisterReadyListener(ApplyPopupRendering);
		}

		public void DisablePopupRendering()
		{
			m_popupRoot = null;
			RemovePopupRendering(null);
		}

		public bool ShouldPropagatePopupRendering()
		{
			return false;
		}

		private void ApplyPopupRendering(object unused)
		{
			if (m_popupRoot == null)
			{
				return;
			}
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>(includeInactive: true);
			foreach (Renderer renderer in componentsInChildren)
			{
				PopupRenderer popupRenderer = renderer.GetComponent<PopupRenderer>() ?? renderer.gameObject.AddComponent<PopupRenderer>();
				if (Active)
				{
					popupRenderer.SetLayerOverride(GameLayer.CameraMask);
				}
				else
				{
					popupRenderer.ClearLayerOverride();
				}
				popupRenderer.EnablePopupRendering(m_popupRoot);
			}
			SetupMaskingCamera();
		}

		private void RemovePopupRendering(object unused)
		{
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				PopupRenderer component = componentsInChildren[i].GetComponent<PopupRenderer>();
				if (component != null)
				{
					if (Active)
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
			SetupMaskingCamera();
		}

		public void SetVisibility(bool isVisible, bool isInternal)
		{
			if (m_maskCamera != null)
			{
				if (isVisible)
				{
					m_maskCamera.enabled = m_initialized && Active;
				}
				else
				{
					m_maskCamera.enabled = false;
				}
			}
		}

		protected override void OnInitialize()
		{
			if (Application.IsPlaying(this))
			{
				m_initialized = true;
				SetupMaskingCamera();
				if (Active)
				{
					ActivateMask();
				}
			}
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			UpdateCameraClipping();
		}

		public void Hide()
		{
			SetVisibility(isVisible: false, isInternal: false);
		}

		public void Show()
		{
			SetVisibility(isVisible: true, isInternal: false);
		}

		private void SetupMaskingCamera()
		{
			m_renderCamera = GetRenderCamera();
			if (!(m_renderCamera == null))
			{
				GameObject gameObject = null;
				if (m_maskCamera == null)
				{
					gameObject = new GameObject("UI_Maskable_Camera (Widget: " + base.Owner.name + ")");
					gameObject.AddComponent<RenderToTextureCamera>();
					SceneUtils.SetLayer(gameObject, GameLayer.CameraMask);
					m_maskCamera = gameObject.AddComponent<Camera>();
				}
				else
				{
					gameObject = m_maskCamera.gameObject;
				}
				gameObject.transform.parent = m_renderCamera.gameObject.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
				m_maskCamera.CopyFrom(m_renderCamera);
				m_maskCamera.clearFlags = CameraClearFlags.Depth;
				m_maskCamera.cullingMask = GetCullingMask();
				m_maskCamera.depth = m_renderCamera.depth + 0.01f;
				m_maskCamera.enabled = m_initialized && Active;
				UpdateCameraClipping();
			}
		}

		private int GetCullingMask()
		{
			int num = GameLayer.CameraMask.LayerBit();
			if (m_popupRoot != null)
			{
				num |= m_popupRoot.PopupCamera.CullingMask;
			}
			else if (m_renderCamera != null)
			{
				num |= m_renderCamera.cullingMask;
			}
			return num;
		}

		private Camera GetRenderCamera()
		{
			if (m_popupRoot != null)
			{
				return m_popupRoot.PopupCamera.Camera;
			}
			return CameraUtils.FindFirstByLayer(base.gameObject.layer);
		}

		private void ActivateMask()
		{
			if (!m_initialized)
			{
				return;
			}
			if (!base.Owner.IsInitialized || base.Owner.IsChangingStates)
			{
				base.Owner.RegisterDoneChangingStatesListener(delegate
				{
					ApplyMaskingLayer(base.transform);
					if (!m_registeredComponents)
					{
						RegisterStatefulComponents();
					}
				}, null, callImmediatelyIfSet: true, doOnce: true);
			}
			else
			{
				ApplyMaskingLayer(base.transform);
				if (!m_registeredComponents)
				{
					RegisterStatefulComponents();
				}
			}
			ApplyPopupRendering(null);
			if (m_maskCamera != null)
			{
				m_maskCamera.enabled = !IsDesiredHiddenInHierarchy;
			}
			PegUI pegUI = PegUI.Get();
			if (pegUI != null)
			{
				pegUI.RegisterForCameraDepthPriorityHitTest(this);
			}
		}

		private void DeactivateMask()
		{
			if (m_maskCamera != null)
			{
				m_maskCamera.enabled = false;
			}
			PegUI pegUI = PegUI.Get();
			if (pegUI != null)
			{
				pegUI.UnregisterForCameraDepthPriorityHitTest(this);
			}
			RemoveMaskingLayer(base.transform);
			ApplyPopupRendering(null);
		}

		private void ApplyMaskingLayer(object root, bool ignoreChildren = false)
		{
			if (!(this == null))
			{
				base.Owner.SetLayerOverrideForObject(GameLayer.CameraMask, base.gameObject);
			}
		}

		private void RemoveMaskingLayer(object root, bool ignoreChildren = false)
		{
			if (!(this == null))
			{
				base.Owner.SetLayerOverrideForObject(GameLayer.Default, base.gameObject);
			}
		}

		private void RegisterStatefulComponents()
		{
			SceneUtils.WalkSelfAndChildren(base.transform, delegate(Transform current)
			{
				IStatefulWidgetComponent[] components = current.GetComponents<IStatefulWidgetComponent>();
				bool result = true;
				if (components != null)
				{
					IStatefulWidgetComponent[] array = components;
					foreach (IStatefulWidgetComponent component in array)
					{
						if (!((Object)component == this) && !(component is VisualController) && !(component is Clickable))
						{
							component.RegisterDoneChangingStatesListener(delegate
							{
								HandleComponentDoneChangingStates(component);
							});
							result = false;
						}
					}
				}
				return result;
			});
			m_registeredComponents = true;
		}

		private void HandleComponentDoneChangingStates(IStatefulWidgetComponent statefulComponent)
		{
			if (Active)
			{
				Component component = (Component)statefulComponent;
				if (component != null)
				{
					ApplyMaskingLayer(component.gameObject);
				}
			}
		}

		private void UpdateCameraClipping()
		{
			if (m_renderCamera == null || m_maskCamera == null)
			{
				return;
			}
			WidgetTransform component = GetComponent<WidgetTransform>();
			if (component == null)
			{
				return;
			}
			Rect rect = component.Rect;
			Vector3 vector = m_renderCamera.WorldToViewportPoint(base.transform.TransformPoint(rect.xMin, 0f, rect.yMin));
			Vector3 vector2 = m_renderCamera.WorldToViewportPoint(base.transform.TransformPoint(rect.xMax, 0f, rect.yMax));
			float num = Mathf.Clamp(vector.x, 0f, 1f);
			float num2 = Mathf.Clamp(vector.y, 0f, 1f);
			float num3 = Mathf.Clamp(vector2.x, 0f, 1f);
			float num4 = Mathf.Clamp(vector2.y, 0f, 1f);
			Rect rect2 = new Rect(num, num2, num3 - num, num4 - num2);
			if (Mathf.Approximately(0f, rect2.height) || Mathf.Approximately(0f, rect2.width))
			{
				return;
			}
			m_maskCamera.rect = new Rect(0f, 0f, 1f, 1f);
			m_maskCamera.ResetProjectionMatrix();
			Matrix4x4 projectionMatrix = m_maskCamera.projectionMatrix;
			Matrix4x4 projectionMatrix2 = Matrix4x4.TRS(new Vector3((0f - rect2.x) * 2f / rect2.width, (0f - rect2.y) * 2f / rect2.height, 0f), Quaternion.identity, Vector3.one) * Matrix4x4.TRS(new Vector3(1f / rect2.width - 1f, 1f / rect2.height - 1f, 0f), Quaternion.identity, new Vector3(1f / rect2.width, 1f / rect2.height, 1f)) * projectionMatrix;
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
			m_maskCamera.rect = rect2;
			m_maskCamera.projectionMatrix = projectionMatrix2;
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (m_initialized)
			{
				ActivateMask();
			}
		}

		protected override void OnDisable()
		{
			DeactivateMask();
			base.OnDisable();
		}

		protected override void OnDestroy()
		{
			PegUI pegUI = PegUI.Get();
			if (pegUI != null)
			{
				pegUI.UnregisterForCameraDepthPriorityHitTest(this);
			}
			if (m_maskCamera != null)
			{
				Object.Destroy(m_maskCamera.gameObject);
			}
			base.OnDestroy();
		}

		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, LogLevel.Info, type);
		}
	}
}
