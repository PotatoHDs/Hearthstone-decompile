using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02000FFA RID: 4090
	public abstract class UIContext : MonoBehaviour
	{
		// Token: 0x0600B1AB RID: 45483 RVA: 0x0036C7F0 File Offset: 0x0036A9F0
		public static UIContext GetRoot()
		{
			if (UIContext.s_uiContext == null)
			{
				GameObject gameObject = new GameObject("UIContext");
				gameObject.AddComponent<HSDontDestroyOnLoad>();
				if (!Application.isPlaying)
				{
					UIContext.s_uiContext = gameObject.AddComponent<UIContextEditMode>();
				}
				else
				{
					UIContext.s_uiContext = gameObject.AddComponent<UIContextPlayMode>();
				}
			}
			return UIContext.s_uiContext;
		}

		// Token: 0x0600B1AC RID: 45484 RVA: 0x0036C840 File Offset: 0x0036AA40
		public PopupRoot ShowPopup(GameObject popupInstance, UIContext.BlurType blurType = UIContext.BlurType.Standard)
		{
			if (popupInstance == null || !Application.IsPlaying(popupInstance))
			{
				return null;
			}
			PopupRoot popupRoot = popupInstance.GetComponent<PopupRoot>() ?? popupInstance.AddComponent<PopupRoot>();
			if (this.m_popupCameras.ContainsKey(popupRoot))
			{
				return popupRoot;
			}
			PopupCamera orCreatePopupCamera = this.GetOrCreatePopupCamera(popupRoot);
			if (orCreatePopupCamera == null)
			{
				Log.UIStatus.PrintError("UIContext.ShowPopup: Failed to propertly setup popup camera. Aborting...", Array.Empty<object>());
				return null;
			}
			popupRoot.hideFlags = (HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			popupRoot.OnDisabled -= this.HandlePopupDestroyedOrDisabled;
			popupRoot.OnDisabled += this.HandlePopupDestroyedOrDisabled;
			popupRoot.EnablePopupRendering(orCreatePopupCamera);
			this.RegisterPopupInternal(popupInstance, popupRoot, this.GetCameraTypeFromLayer((GameLayer)popupInstance.gameObject.layer), orCreatePopupCamera.Camera, blurType);
			return popupRoot;
		}

		// Token: 0x0600B1AD RID: 45485 RVA: 0x0036C8FC File Offset: 0x0036AAFC
		public void DismissPopup(GameObject popupInstance)
		{
			if (popupInstance == null || !Application.IsPlaying(popupInstance))
			{
				return;
			}
			PopupRoot component = popupInstance.GetComponent<PopupRoot>();
			if (component == null)
			{
				return;
			}
			this.DismissPopupInternal(component);
		}

		// Token: 0x0600B1AE RID: 45486 RVA: 0x0036C934 File Offset: 0x0036AB34
		public void DismissPopupsRecursive(GameObject root)
		{
			foreach (PopupRoot popupRoot in root.GetComponentsInChildren<PopupRoot>())
			{
				this.DismissPopupInternal(popupRoot);
			}
		}

		// Token: 0x0600B1AF RID: 45487 RVA: 0x0036C964 File Offset: 0x0036AB64
		public void RegisterPopup(GameObject popupInstance, UIContext.RenderCameraType cameraType, UIContext.BlurType blurType = UIContext.BlurType.Standard)
		{
			Camera camera = this.FindTemplateCamera(popupInstance, cameraType);
			if (camera == null)
			{
				Log.UIStatus.PrintError("UIContext.RegisterPopup: Unable to find suitable camera.", Array.Empty<object>());
				return;
			}
			this.RegisterPopupInternal(popupInstance, null, cameraType, camera, blurType);
		}

		// Token: 0x0600B1B0 RID: 45488 RVA: 0x0036C9A4 File Offset: 0x0036ABA4
		public void UnregisterPopup(GameObject popupInstance)
		{
			UIContext.PopupRecord popupRecord = null;
			for (int i = 0; i < this.m_popupStack.Count; i++)
			{
				if (this.m_popupStack[i].PopupInstance == popupInstance)
				{
					popupRecord = this.m_popupStack[i];
					this.m_popupStack.RemoveAt(i);
					break;
				}
			}
			if (popupRecord != null && popupRecord.BlurType != UIContext.BlurType.None && FullScreenFXMgr.Get() != null)
			{
				if (popupRecord.BlurType == UIContext.BlurType.Standard && !this.UsesMetalAPI())
				{
					FullScreenFXMgr.Get().RemoveStandardBlurVignette(popupRecord, 0.2f, null);
					return;
				}
				if ((popupRecord.BlurType == UIContext.BlurType.Legacy || this.UsesMetalAPI()) && this.CountPopupsWithBlurInstances(UIContext.BlurType.Legacy) == 0)
				{
					FullScreenFXMgr.Get().EndStandardBlurVignette(0.5f, null);
				}
			}
		}

		// Token: 0x0600B1B1 RID: 45489 RVA: 0x0036CA5C File Offset: 0x0036AC5C
		public UIContext.PopupRecord GetLatestPopup()
		{
			int num = this.m_popupStack.Count - 1;
			while (this.m_popupStack.Count > 0 && num >= 0)
			{
				if (!(this.m_popupStack[num].PopupInstance == null))
				{
					return this.m_popupStack[num];
				}
				this.m_popupStack.RemoveAt(num);
				num--;
			}
			return null;
		}

		// Token: 0x0600B1B2 RID: 45490 RVA: 0x0036CAC4 File Offset: 0x0036ACC4
		public void CleanupPopupCamera(PopupRoot popupRoot)
		{
			PopupCamera popupCamera;
			if (this.m_popupCameras.TryGetValue(popupRoot, out popupCamera) && popupCamera != null)
			{
				UnityEngine.Object.Destroy(popupCamera.gameObject);
			}
			this.m_popupCameras.Remove(popupRoot);
		}

		// Token: 0x0600B1B3 RID: 45491 RVA: 0x0036CB04 File Offset: 0x0036AD04
		public List<UIContext.PopupRecord> GetPopupsDescendingOrder()
		{
			List<UIContext.PopupRecord> list = new List<UIContext.PopupRecord>(this.m_popupStack.Count);
			for (int i = this.m_popupStack.Count - 1; i >= 0; i--)
			{
				if (this.m_popupStack[i].PopupInstance == null)
				{
					this.m_popupStack.RemoveAt(i);
				}
				else
				{
					list.Add(this.m_popupStack[i]);
				}
			}
			return list;
		}

		// Token: 0x0600B1B4 RID: 45492 RVA: 0x0036CB74 File Offset: 0x0036AD74
		private void HandlePopupDestroyedOrDisabled(PopupRoot popupRoot)
		{
			this.DismissPopupInternal(popupRoot);
		}

		// Token: 0x0600B1B5 RID: 45493 RVA: 0x0036CB80 File Offset: 0x0036AD80
		private void DismissPopupInternal(PopupRoot popupRoot)
		{
			if (popupRoot == null)
			{
				return;
			}
			if (!this.IsPopupRegistered(popupRoot.gameObject))
			{
				return;
			}
			popupRoot.DisablePopupRendering();
			this.UnregisterPopup(popupRoot.gameObject);
			this.CleanupPopupCamera(popupRoot);
			if (PegUI.Get() != null)
			{
				PegUI.Get().UnregisterForCameraDepthPriorityHitTest(popupRoot);
			}
			UnityEngine.Object.Destroy(popupRoot);
		}

		// Token: 0x0600B1B6 RID: 45494 RVA: 0x0036CBE0 File Offset: 0x0036ADE0
		private void RegisterPopupInternal(GameObject popupInstance, PopupRoot popupRoot, UIContext.RenderCameraType cameraType, Camera camera, UIContext.BlurType blurType)
		{
			if (!this.IsPopupRegistered(popupInstance))
			{
				UIContext.PopupRecord popupRecord = new UIContext.PopupRecord(popupInstance, popupRoot, cameraType, camera, blurType);
				this.m_popupStack.Add(popupRecord);
				if (PegUI.Get() != null && popupRoot != null)
				{
					PegUI.Get().RegisterForCameraDepthPriorityHitTest(popupRoot);
				}
				if (blurType != UIContext.BlurType.None && FullScreenFXMgr.Get() != null)
				{
					if (popupRecord.BlurType == UIContext.BlurType.Standard && !this.UsesMetalAPI())
					{
						FullScreenFXMgr.Get().AddStandardBlurVignette(popupRecord, 0.5f);
						return;
					}
					if ((popupRecord.BlurType == UIContext.BlurType.Legacy || this.UsesMetalAPI()) && this.CountPopupsWithBlurInstances(UIContext.BlurType.Legacy) == 1)
					{
						FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
					}
				}
			}
		}

		// Token: 0x0600B1B7 RID: 45495 RVA: 0x0036CC8A File Offset: 0x0036AE8A
		private bool UsesMetalAPI()
		{
			return PlatformSettings.OS == OSCategory.iOS || PlatformSettings.OS == OSCategory.Mac;
		}

		// Token: 0x0600B1B8 RID: 45496 RVA: 0x0036CCA0 File Offset: 0x0036AEA0
		private bool IsPopupRegistered(GameObject popupInstance)
		{
			using (List<UIContext.PopupRecord>.Enumerator enumerator = this.m_popupStack.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.PopupInstance == popupInstance)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600B1B9 RID: 45497 RVA: 0x0036CD00 File Offset: 0x0036AF00
		private int CountPopupsWithBlurInstances(UIContext.BlurType blurType)
		{
			int num = 0;
			foreach (UIContext.PopupRecord popupRecord in this.m_popupStack)
			{
				if (popupRecord.BlurType == blurType)
				{
					num++;
				}
				else if (popupRecord.BlurType != UIContext.BlurType.None && this.UsesMetalAPI())
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600B1BA RID: 45498 RVA: 0x0036CD74 File Offset: 0x0036AF74
		private PopupCamera GetOrCreatePopupCamera(PopupRoot popupRoot)
		{
			if (popupRoot == null)
			{
				return null;
			}
			PopupCamera popupCamera;
			if (this.m_popupCameras.TryGetValue(popupRoot, out popupCamera))
			{
				return popupCamera;
			}
			UIContext.RenderCameraType cameraTypeFromLayer = this.GetCameraTypeFromLayer((GameLayer)popupRoot.gameObject.layer);
			Camera camera = this.FindTemplateCamera(popupRoot.gameObject, cameraTypeFromLayer);
			if (camera == null)
			{
				return null;
			}
			float num = (float)(this.m_popupCameras.Count + 1) * 0.1f;
			float num2 = camera.depth + num;
			int num3 = this.m_popupStack.Count - 1;
			while (this.m_popupStack.Count > 0 && num3 >= 0)
			{
				if (this.m_popupStack[num3].PopupInstance == null)
				{
					this.m_popupStack.RemoveAt(num3);
				}
				else
				{
					UIContext.PopupRecord popupRecord = this.m_popupStack[num3];
					if (popupRecord.RenderCamera != null)
					{
						if (popupRecord.CameraType != UIContext.RenderCameraType.HighPriorityUI || cameraTypeFromLayer == UIContext.RenderCameraType.HighPriorityUI)
						{
							if (popupRecord.RenderCamera.depth > num2)
							{
								num2 = popupRecord.RenderCamera.depth + num;
								break;
							}
							break;
						}
					}
					else
					{
						Log.UIStatus.PrintWarning("UIContext.GetOrCreatePopupCamera: Missing reference to render camera for " + popupRecord.PopupInstance.name + "!", Array.Empty<object>());
					}
				}
				num3--;
			}
			GameObject gameObject = new GameObject(string.Format("PopupCamera (from:{0}, owner:{1}, depth:{2})", camera.name, popupRoot.name, num2));
			gameObject.SetActive(false);
			popupCamera = gameObject.AddComponent<PopupCamera>();
			popupCamera.MirroredCamera = camera;
			popupCamera.transform.SetParent(base.transform);
			popupCamera.Depth = num2;
			popupCamera.CullingMask = GameLayer.Reserved29.LayerBit();
			this.m_popupCameras[popupRoot] = popupCamera;
			gameObject.SetActive(true);
			return popupCamera;
		}

		// Token: 0x0600B1BB RID: 45499 RVA: 0x0036CF2C File Offset: 0x0036B12C
		private Camera FindTemplateCamera(PopupRoot popupRoot)
		{
			UIContext.RenderCameraType cameraTypeFromLayer = this.GetCameraTypeFromLayer((GameLayer)popupRoot.gameObject.layer);
			return this.FindTemplateCamera(popupRoot.gameObject, cameraTypeFromLayer);
		}

		// Token: 0x0600B1BC RID: 45500 RVA: 0x0036CF58 File Offset: 0x0036B158
		private Camera FindTemplateCamera(GameObject popupInstance, UIContext.RenderCameraType cameraType)
		{
			if (Application.IsPlaying(popupInstance))
			{
				OverlayUI overlayUI = OverlayUI.Get();
				if (overlayUI != null && overlayUI.HasObject(popupInstance))
				{
					switch (cameraType)
					{
					case UIContext.RenderCameraType.OrthographicUI:
						if (overlayUI.m_UICamera != null)
						{
							return overlayUI.m_UICamera;
						}
						break;
					case UIContext.RenderCameraType.PerspectiveUI:
						if (overlayUI.m_PerspectiveUICamera != null)
						{
							return overlayUI.m_PerspectiveUICamera;
						}
						break;
					case UIContext.RenderCameraType.HighPriorityUI:
						if (overlayUI.m_HighPriorityCamera != null)
						{
							return overlayUI.m_HighPriorityCamera;
						}
						break;
					case UIContext.RenderCameraType.BackgroundUI:
						if (overlayUI.m_BackgroundUICamera != null)
						{
							return overlayUI.m_BackgroundUICamera;
						}
						break;
					}
				}
				if (cameraType == UIContext.RenderCameraType.Box)
				{
					Box box = Box.Get();
					if (box != null && box.GetCamera() != null)
					{
						return box.GetCamera();
					}
				}
				if (cameraType == UIContext.RenderCameraType.Board)
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
				if (cameraType == UIContext.RenderCameraType.IgnoreFullScreenEffects)
				{
					return CameraUtils.FindFirstByLayer(GameLayer.IgnoreFullScreenEffects);
				}
			}
			return Camera.main;
		}

		// Token: 0x0600B1BD RID: 45501 RVA: 0x0036D058 File Offset: 0x0036B258
		private UIContext.RenderCameraType GetCameraTypeFromLayer(GameLayer gameLayer)
		{
			UIContext.RenderCameraType result;
			if (UIContext.m_layerToCameraTypeMap.TryGetValue(gameLayer, out result))
			{
				return result;
			}
			if (SceneMgr.Get() != null && SceneMgr.Get().IsInGame())
			{
				return UIContext.RenderCameraType.Board;
			}
			return UIContext.RenderCameraType.Box;
		}

		// Token: 0x040095C3 RID: 38339
		private const float BlurFadeTime = 0.5f;

		// Token: 0x040095C4 RID: 38340
		private const float BlurFadeOutTime = 0.2f;

		// Token: 0x040095C5 RID: 38341
		private static readonly Map<GameLayer, UIContext.RenderCameraType> m_layerToCameraTypeMap = new Map<GameLayer, UIContext.RenderCameraType>
		{
			{
				GameLayer.UI,
				UIContext.RenderCameraType.OrthographicUI
			},
			{
				GameLayer.HighPriorityUI,
				UIContext.RenderCameraType.HighPriorityUI
			},
			{
				GameLayer.PerspectiveUI,
				UIContext.RenderCameraType.PerspectiveUI
			},
			{
				GameLayer.BackgroundUI,
				UIContext.RenderCameraType.BackgroundUI
			},
			{
				GameLayer.IgnoreFullScreenEffects,
				UIContext.RenderCameraType.IgnoreFullScreenEffects
			}
		};

		// Token: 0x040095C6 RID: 38342
		private static UIContext s_uiContext;

		// Token: 0x040095C7 RID: 38343
		private List<UIContext.PopupRecord> m_popupStack = new List<UIContext.PopupRecord>();

		// Token: 0x040095C8 RID: 38344
		private Dictionary<PopupRoot, PopupCamera> m_popupCameras = new Dictionary<PopupRoot, PopupCamera>();

		// Token: 0x02002827 RID: 10279
		public enum RenderCameraType
		{
			// Token: 0x0400F8A2 RID: 63650
			Box,
			// Token: 0x0400F8A3 RID: 63651
			Board,
			// Token: 0x0400F8A4 RID: 63652
			OrthographicUI,
			// Token: 0x0400F8A5 RID: 63653
			PerspectiveUI,
			// Token: 0x0400F8A6 RID: 63654
			HighPriorityUI,
			// Token: 0x0400F8A7 RID: 63655
			IgnoreFullScreenEffects,
			// Token: 0x0400F8A8 RID: 63656
			BackgroundUI
		}

		// Token: 0x02002828 RID: 10280
		public enum BlurType
		{
			// Token: 0x0400F8AA RID: 63658
			None,
			// Token: 0x0400F8AB RID: 63659
			Standard,
			// Token: 0x0400F8AC RID: 63660
			Legacy
		}

		// Token: 0x02002829 RID: 10281
		public class PopupRecord
		{
			// Token: 0x17002D1B RID: 11547
			// (get) Token: 0x06013B20 RID: 80672 RVA: 0x0053A80D File Offset: 0x00538A0D
			// (set) Token: 0x06013B21 RID: 80673 RVA: 0x0053A815 File Offset: 0x00538A15
			public GameObject PopupInstance { get; private set; }

			// Token: 0x17002D1C RID: 11548
			// (get) Token: 0x06013B22 RID: 80674 RVA: 0x0053A81E File Offset: 0x00538A1E
			// (set) Token: 0x06013B23 RID: 80675 RVA: 0x0053A826 File Offset: 0x00538A26
			public UIContext.RenderCameraType CameraType { get; private set; }

			// Token: 0x17002D1D RID: 11549
			// (get) Token: 0x06013B24 RID: 80676 RVA: 0x0053A82F File Offset: 0x00538A2F
			// (set) Token: 0x06013B25 RID: 80677 RVA: 0x0053A837 File Offset: 0x00538A37
			public Camera RenderCamera { get; private set; }

			// Token: 0x17002D1E RID: 11550
			// (get) Token: 0x06013B26 RID: 80678 RVA: 0x0053A840 File Offset: 0x00538A40
			// (set) Token: 0x06013B27 RID: 80679 RVA: 0x0053A848 File Offset: 0x00538A48
			public PopupRoot PopupRoot { get; private set; }

			// Token: 0x17002D1F RID: 11551
			// (get) Token: 0x06013B28 RID: 80680 RVA: 0x0053A851 File Offset: 0x00538A51
			// (set) Token: 0x06013B29 RID: 80681 RVA: 0x0053A859 File Offset: 0x00538A59
			public UIContext.BlurType BlurType { get; private set; }

			// Token: 0x17002D20 RID: 11552
			// (get) Token: 0x06013B2A RID: 80682 RVA: 0x0053A862 File Offset: 0x00538A62
			// (set) Token: 0x06013B2B RID: 80683 RVA: 0x0053A86A File Offset: 0x00538A6A
			public FullScreenFXMgr.ScreenEffectsInstance ScreenEffectsInstance { get; set; }

			// Token: 0x06013B2C RID: 80684 RVA: 0x0053A873 File Offset: 0x00538A73
			public PopupRecord(GameObject popupInstance, PopupRoot popupRoot, UIContext.RenderCameraType cameraType, Camera renderCamera, UIContext.BlurType blurType)
			{
				this.PopupInstance = popupInstance;
				this.PopupRoot = popupRoot;
				this.CameraType = cameraType;
				this.RenderCamera = renderCamera;
				this.BlurType = blurType;
			}
		}
	}
}
