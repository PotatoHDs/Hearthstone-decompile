using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	public abstract class UIContext : MonoBehaviour
	{
		public enum RenderCameraType
		{
			Box,
			Board,
			OrthographicUI,
			PerspectiveUI,
			HighPriorityUI,
			IgnoreFullScreenEffects,
			BackgroundUI
		}

		public enum BlurType
		{
			None,
			Standard,
			Legacy
		}

		public class PopupRecord
		{
			public GameObject PopupInstance { get; private set; }

			public RenderCameraType CameraType { get; private set; }

			public Camera RenderCamera { get; private set; }

			public PopupRoot PopupRoot { get; private set; }

			public BlurType BlurType { get; private set; }

			public FullScreenFXMgr.ScreenEffectsInstance ScreenEffectsInstance { get; set; }

			public PopupRecord(GameObject popupInstance, PopupRoot popupRoot, RenderCameraType cameraType, Camera renderCamera, BlurType blurType)
			{
				PopupInstance = popupInstance;
				PopupRoot = popupRoot;
				CameraType = cameraType;
				RenderCamera = renderCamera;
				BlurType = blurType;
			}
		}

		private const float BlurFadeTime = 0.5f;

		private const float BlurFadeOutTime = 0.2f;

		private static readonly Map<GameLayer, RenderCameraType> m_layerToCameraTypeMap = new Map<GameLayer, RenderCameraType>
		{
			{
				GameLayer.UI,
				RenderCameraType.OrthographicUI
			},
			{
				GameLayer.HighPriorityUI,
				RenderCameraType.HighPriorityUI
			},
			{
				GameLayer.PerspectiveUI,
				RenderCameraType.PerspectiveUI
			},
			{
				GameLayer.BackgroundUI,
				RenderCameraType.BackgroundUI
			},
			{
				GameLayer.IgnoreFullScreenEffects,
				RenderCameraType.IgnoreFullScreenEffects
			}
		};

		private static UIContext s_uiContext;

		private List<PopupRecord> m_popupStack = new List<PopupRecord>();

		private Dictionary<PopupRoot, PopupCamera> m_popupCameras = new Dictionary<PopupRoot, PopupCamera>();

		public static UIContext GetRoot()
		{
			if (s_uiContext == null)
			{
				GameObject gameObject = new GameObject("UIContext");
				gameObject.AddComponent<HSDontDestroyOnLoad>();
				if (!Application.isPlaying)
				{
					s_uiContext = gameObject.AddComponent<UIContextEditMode>();
				}
				else
				{
					s_uiContext = gameObject.AddComponent<UIContextPlayMode>();
				}
			}
			return s_uiContext;
		}

		public PopupRoot ShowPopup(GameObject popupInstance, BlurType blurType = BlurType.Standard)
		{
			if (popupInstance == null || !Application.IsPlaying(popupInstance))
			{
				return null;
			}
			PopupRoot popupRoot = popupInstance.GetComponent<PopupRoot>() ?? popupInstance.AddComponent<PopupRoot>();
			if (m_popupCameras.ContainsKey(popupRoot))
			{
				return popupRoot;
			}
			PopupCamera orCreatePopupCamera = GetOrCreatePopupCamera(popupRoot);
			if (orCreatePopupCamera == null)
			{
				Log.UIStatus.PrintError("UIContext.ShowPopup: Failed to propertly setup popup camera. Aborting...");
				return null;
			}
			popupRoot.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
			popupRoot.OnDisabled -= HandlePopupDestroyedOrDisabled;
			popupRoot.OnDisabled += HandlePopupDestroyedOrDisabled;
			popupRoot.EnablePopupRendering(orCreatePopupCamera);
			RegisterPopupInternal(popupInstance, popupRoot, GetCameraTypeFromLayer((GameLayer)popupInstance.gameObject.layer), orCreatePopupCamera.Camera, blurType);
			return popupRoot;
		}

		public void DismissPopup(GameObject popupInstance)
		{
			if (!(popupInstance == null) && Application.IsPlaying(popupInstance))
			{
				PopupRoot component = popupInstance.GetComponent<PopupRoot>();
				if (!(component == null))
				{
					DismissPopupInternal(component);
				}
			}
		}

		public void DismissPopupsRecursive(GameObject root)
		{
			PopupRoot[] componentsInChildren = root.GetComponentsInChildren<PopupRoot>();
			foreach (PopupRoot popupRoot in componentsInChildren)
			{
				DismissPopupInternal(popupRoot);
			}
		}

		public void RegisterPopup(GameObject popupInstance, RenderCameraType cameraType, BlurType blurType = BlurType.Standard)
		{
			Camera camera = FindTemplateCamera(popupInstance, cameraType);
			if (camera == null)
			{
				Log.UIStatus.PrintError("UIContext.RegisterPopup: Unable to find suitable camera.");
			}
			else
			{
				RegisterPopupInternal(popupInstance, null, cameraType, camera, blurType);
			}
		}

		public void UnregisterPopup(GameObject popupInstance)
		{
			PopupRecord popupRecord = null;
			for (int i = 0; i < m_popupStack.Count; i++)
			{
				if (m_popupStack[i].PopupInstance == popupInstance)
				{
					popupRecord = m_popupStack[i];
					m_popupStack.RemoveAt(i);
					break;
				}
			}
			if (popupRecord != null && popupRecord.BlurType != 0 && FullScreenFXMgr.Get() != null)
			{
				if (popupRecord.BlurType == BlurType.Standard && !UsesMetalAPI())
				{
					FullScreenFXMgr.Get().RemoveStandardBlurVignette(popupRecord, 0.2f);
				}
				else if ((popupRecord.BlurType == BlurType.Legacy || UsesMetalAPI()) && CountPopupsWithBlurInstances(BlurType.Legacy) == 0)
				{
					FullScreenFXMgr.Get().EndStandardBlurVignette(0.5f);
				}
			}
		}

		public PopupRecord GetLatestPopup()
		{
			int num = m_popupStack.Count - 1;
			while (m_popupStack.Count > 0 && num >= 0)
			{
				if (m_popupStack[num].PopupInstance == null)
				{
					m_popupStack.RemoveAt(num);
					num--;
					continue;
				}
				return m_popupStack[num];
			}
			return null;
		}

		public void CleanupPopupCamera(PopupRoot popupRoot)
		{
			if (m_popupCameras.TryGetValue(popupRoot, out var value) && value != null)
			{
				Object.Destroy(value.gameObject);
			}
			m_popupCameras.Remove(popupRoot);
		}

		public List<PopupRecord> GetPopupsDescendingOrder()
		{
			List<PopupRecord> list = new List<PopupRecord>(m_popupStack.Count);
			for (int num = m_popupStack.Count - 1; num >= 0; num--)
			{
				if (m_popupStack[num].PopupInstance == null)
				{
					m_popupStack.RemoveAt(num);
				}
				else
				{
					list.Add(m_popupStack[num]);
				}
			}
			return list;
		}

		private void HandlePopupDestroyedOrDisabled(PopupRoot popupRoot)
		{
			DismissPopupInternal(popupRoot);
		}

		private void DismissPopupInternal(PopupRoot popupRoot)
		{
			if (!(popupRoot == null) && IsPopupRegistered(popupRoot.gameObject))
			{
				popupRoot.DisablePopupRendering();
				UnregisterPopup(popupRoot.gameObject);
				CleanupPopupCamera(popupRoot);
				if (PegUI.Get() != null)
				{
					PegUI.Get().UnregisterForCameraDepthPriorityHitTest(popupRoot);
				}
				Object.Destroy(popupRoot);
			}
		}

		private void RegisterPopupInternal(GameObject popupInstance, PopupRoot popupRoot, RenderCameraType cameraType, Camera camera, BlurType blurType)
		{
			if (IsPopupRegistered(popupInstance))
			{
				return;
			}
			PopupRecord popupRecord = new PopupRecord(popupInstance, popupRoot, cameraType, camera, blurType);
			m_popupStack.Add(popupRecord);
			if (PegUI.Get() != null && popupRoot != null)
			{
				PegUI.Get().RegisterForCameraDepthPriorityHitTest(popupRoot);
			}
			if (blurType != 0 && FullScreenFXMgr.Get() != null)
			{
				if (popupRecord.BlurType == BlurType.Standard && !UsesMetalAPI())
				{
					FullScreenFXMgr.Get().AddStandardBlurVignette(popupRecord, 0.5f);
				}
				else if ((popupRecord.BlurType == BlurType.Legacy || UsesMetalAPI()) && CountPopupsWithBlurInstances(BlurType.Legacy) == 1)
				{
					FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
				}
			}
		}

		private bool UsesMetalAPI()
		{
			if (PlatformSettings.OS != OSCategory.iOS)
			{
				return PlatformSettings.OS == OSCategory.Mac;
			}
			return true;
		}

		private bool IsPopupRegistered(GameObject popupInstance)
		{
			foreach (PopupRecord item in m_popupStack)
			{
				if (item.PopupInstance == popupInstance)
				{
					return true;
				}
			}
			return false;
		}

		private int CountPopupsWithBlurInstances(BlurType blurType)
		{
			int num = 0;
			foreach (PopupRecord item in m_popupStack)
			{
				if (item.BlurType == blurType)
				{
					num++;
				}
				else if (item.BlurType != 0 && UsesMetalAPI())
				{
					num++;
				}
			}
			return num;
		}

		private PopupCamera GetOrCreatePopupCamera(PopupRoot popupRoot)
		{
			if (popupRoot == null)
			{
				return null;
			}
			if (m_popupCameras.TryGetValue(popupRoot, out var value))
			{
				return value;
			}
			RenderCameraType cameraTypeFromLayer = GetCameraTypeFromLayer((GameLayer)popupRoot.gameObject.layer);
			Camera camera = FindTemplateCamera(popupRoot.gameObject, cameraTypeFromLayer);
			if (camera == null)
			{
				return null;
			}
			float num = (float)(m_popupCameras.Count + 1) * 0.1f;
			float num2 = camera.depth + num;
			int num3 = m_popupStack.Count - 1;
			while (m_popupStack.Count > 0 && num3 >= 0)
			{
				if (m_popupStack[num3].PopupInstance == null)
				{
					m_popupStack.RemoveAt(num3);
				}
				else
				{
					PopupRecord popupRecord = m_popupStack[num3];
					if (popupRecord.RenderCamera != null)
					{
						if (popupRecord.CameraType != RenderCameraType.HighPriorityUI || cameraTypeFromLayer == RenderCameraType.HighPriorityUI)
						{
							if (popupRecord.RenderCamera.depth > num2)
							{
								num2 = popupRecord.RenderCamera.depth + num;
							}
							break;
						}
					}
					else
					{
						Log.UIStatus.PrintWarning("UIContext.GetOrCreatePopupCamera: Missing reference to render camera for " + popupRecord.PopupInstance.name + "!");
					}
				}
				num3--;
			}
			GameObject obj = new GameObject($"PopupCamera (from:{camera.name}, owner:{popupRoot.name}, depth:{num2})");
			obj.SetActive(value: false);
			value = obj.AddComponent<PopupCamera>();
			value.MirroredCamera = camera;
			value.transform.SetParent(base.transform);
			value.Depth = num2;
			value.CullingMask = GameLayer.Reserved29.LayerBit();
			m_popupCameras[popupRoot] = value;
			obj.SetActive(value: true);
			return value;
		}

		private Camera FindTemplateCamera(PopupRoot popupRoot)
		{
			RenderCameraType cameraTypeFromLayer = GetCameraTypeFromLayer((GameLayer)popupRoot.gameObject.layer);
			return FindTemplateCamera(popupRoot.gameObject, cameraTypeFromLayer);
		}

		private Camera FindTemplateCamera(GameObject popupInstance, RenderCameraType cameraType)
		{
			if (Application.IsPlaying(popupInstance))
			{
				OverlayUI overlayUI = OverlayUI.Get();
				if (overlayUI != null && overlayUI.HasObject(popupInstance))
				{
					switch (cameraType)
					{
					case RenderCameraType.OrthographicUI:
						if (overlayUI.m_UICamera != null)
						{
							return overlayUI.m_UICamera;
						}
						break;
					case RenderCameraType.PerspectiveUI:
						if (overlayUI.m_PerspectiveUICamera != null)
						{
							return overlayUI.m_PerspectiveUICamera;
						}
						break;
					case RenderCameraType.HighPriorityUI:
						if (overlayUI.m_HighPriorityCamera != null)
						{
							return overlayUI.m_HighPriorityCamera;
						}
						break;
					case RenderCameraType.BackgroundUI:
						if (overlayUI.m_BackgroundUICamera != null)
						{
							return overlayUI.m_BackgroundUICamera;
						}
						break;
					}
				}
				if (cameraType == RenderCameraType.Box)
				{
					Box box = Box.Get();
					if (box != null && box.GetCamera() != null)
					{
						return box.GetCamera();
					}
				}
				if (cameraType == RenderCameraType.Board)
				{
					BoardCameras boardCameras = BoardCameras.Get();
					if (boardCameras != null)
					{
						Camera componentInChildren = boardCameras.GetComponentInChildren<Camera>();
						if (componentInChildren != null)
						{
							return componentInChildren;
						}
					}
				}
				if (cameraType == RenderCameraType.IgnoreFullScreenEffects)
				{
					return CameraUtils.FindFirstByLayer(GameLayer.IgnoreFullScreenEffects);
				}
			}
			return Camera.main;
		}

		private RenderCameraType GetCameraTypeFromLayer(GameLayer gameLayer)
		{
			if (m_layerToCameraTypeMap.TryGetValue(gameLayer, out var value))
			{
				return value;
			}
			if (SceneMgr.Get() != null && SceneMgr.Get().IsInGame())
			{
				return RenderCameraType.Board;
			}
			return RenderCameraType.Box;
		}
	}
}
