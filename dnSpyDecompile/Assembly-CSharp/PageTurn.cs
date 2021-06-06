using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000137 RID: 311
public class PageTurn : MonoBehaviour
{
	// Token: 0x06001461 RID: 5217 RVA: 0x00074DC8 File Offset: 0x00072FC8
	private void Awake()
	{
		this.m_initialPosition = base.transform.localPosition;
		Transform transform = base.transform.Find(this.FRONT_PAGE_NAME);
		if (transform != null)
		{
			this.m_FrontPageGameObject = transform.gameObject;
		}
		if (this.m_FrontPageGameObject == null)
		{
			Debug.LogError("Failed to find " + this.FRONT_PAGE_NAME + " Object.");
		}
		transform = base.transform.Find(this.BACK_PAGE_NAME);
		if (transform != null)
		{
			this.m_BackPageGameObject = transform.gameObject;
		}
		if (this.m_BackPageGameObject == null)
		{
			Debug.LogError("Failed to find " + this.BACK_PAGE_NAME + " Object.");
		}
		this.Show(false);
		this.m_TheBoxOuterFrame = Box.Get().m_OuterFrame;
		this.CreateCamera();
		this.CreateRenderTexture();
		this.SetupMaterial();
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x00074EAD File Offset: 0x000730AD
	protected void OnEnable()
	{
		if (this.m_OffscreenPageTurnCameraGO != null)
		{
			this.CreateCamera();
		}
		if (this.m_TempRenderBuffer != null || this.m_TempMaskBuffer != null)
		{
			this.CreateRenderTexture();
			this.SetupMaterial();
		}
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x00074EEC File Offset: 0x000730EC
	protected void OnDisable()
	{
		if (this.m_TempRenderBuffer != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_TempRenderBuffer);
		}
		if (this.m_TempMaskBuffer != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_TempMaskBuffer);
		}
		if (this.m_OffscreenPageTurnCameraGO != null)
		{
			UnityEngine.Object.Destroy(this.m_OffscreenPageTurnCameraGO);
		}
		if (this.m_OffscreenPageTurnCamera != null)
		{
			UnityEngine.Object.Destroy(this.m_OffscreenPageTurnCamera);
		}
		if (this.m_OffscreenPageTurnMaskCamera != null)
		{
			UnityEngine.Object.Destroy(this.m_OffscreenPageTurnMaskCamera);
		}
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x00074F80 File Offset: 0x00073180
	public void TurnRight(GameObject flippingPage, GameObject otherPage, PageTurn.DelOnPageTurnComplete pageTurnCompleteCallback, PageTurn.DelPositionPages positionPagesCallback, object callbackData)
	{
		this.Render(flippingPage);
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			Time.captureFramerate = 18;
		}
		else if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Medium)
		{
			Time.captureFramerate = 24;
		}
		else
		{
			Time.captureFramerate = 30;
		}
		base.GetComponent<Animation>().Stop(this.PAGE_TURN_RIGHT_ANIM);
		base.GetComponent<Animation>()[this.PAGE_TURN_RIGHT_ANIM].time = 0f;
		base.GetComponent<Animation>()[this.PAGE_TURN_RIGHT_ANIM].speed = this.m_TurnRightSpeed;
		base.GetComponent<Animation>().Play(this.PAGE_TURN_RIGHT_ANIM);
		this.m_FrontPageGameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Alpha", 1f);
		this.m_BackPageGameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Alpha", 1f);
		float secondsToWait = base.GetComponent<Animation>()[this.PAGE_TURN_RIGHT_ANIM].length / this.m_TurnRightSpeed;
		PageTurn.PageTurningData value = new PageTurn.PageTurningData
		{
			m_secondsToWait = secondsToWait,
			m_pageTurnCompleteCallback = pageTurnCompleteCallback,
			m_callbackData = callbackData
		};
		base.StopCoroutine(this.WAIT_THEN_COMPLETE_PAGE_TURN_RIGHT_COROUTINE);
		base.StartCoroutine(this.WAIT_THEN_COMPLETE_PAGE_TURN_RIGHT_COROUTINE, value);
		if (positionPagesCallback != null)
		{
			positionPagesCallback(callbackData);
		}
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x000750C0 File Offset: 0x000732C0
	public void TurnLeft(GameObject flippingPage, GameObject otherPage, PageTurn.DelOnPageTurnComplete pageTurnCompleteCallback, PageTurn.DelPositionPages positionPagesCallback, object callbackData)
	{
		PageTurn.TurnPageData turnPageData = new PageTurn.TurnPageData();
		turnPageData.flippingPage = flippingPage;
		turnPageData.otherPage = otherPage;
		turnPageData.pageTurnCompleteCallback = pageTurnCompleteCallback;
		turnPageData.positionPagesCallback = positionPagesCallback;
		turnPageData.callbackData = callbackData;
		base.StopCoroutine("TurnLeftPage");
		base.StartCoroutine("TurnLeftPage", turnPageData);
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x00075110 File Offset: 0x00073310
	private IEnumerator TurnLeftPage(PageTurn.TurnPageData pageData)
	{
		yield return null;
		yield return null;
		yield return null;
		GameObject flippingPage = pageData.flippingPage;
		GameObject otherPage = pageData.otherPage;
		PageTurn.DelOnPageTurnComplete pageTurnCompleteCallback = pageData.pageTurnCompleteCallback;
		PageTurn.DelPositionPages positionPagesCallback = pageData.positionPagesCallback;
		object callbackData = pageData.callbackData;
		Vector3 position = flippingPage.transform.position;
		Vector3 position2 = otherPage.transform.position;
		flippingPage.transform.position = position2;
		otherPage.transform.position = position;
		this.Render(flippingPage);
		flippingPage.transform.position = position;
		otherPage.transform.position = position2;
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			Time.captureFramerate = 18;
		}
		else if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Medium)
		{
			Time.captureFramerate = 24;
		}
		else
		{
			Time.captureFramerate = 30;
		}
		this.m_FrontPageGameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Alpha", 1f);
		this.m_BackPageGameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Alpha", 1f);
		base.GetComponent<Animation>().Stop(this.PAGE_TURN_LEFT_ANIM);
		base.GetComponent<Animation>()[this.PAGE_TURN_LEFT_ANIM].time = 0.22f;
		base.GetComponent<Animation>()[this.PAGE_TURN_LEFT_ANIM].speed = this.m_TurnLeftSpeed;
		base.GetComponent<Animation>().Play(this.PAGE_TURN_LEFT_ANIM);
		while (base.GetComponent<Animation>()[this.PAGE_TURN_LEFT_ANIM].time < Math.Min(base.GetComponent<Animation>()[this.PAGE_TURN_LEFT_ANIM].length, this.m_TurnLeftDelayBeforePositioningPages))
		{
			yield return null;
		}
		if (positionPagesCallback != null)
		{
			positionPagesCallback(callbackData);
		}
		PageTurn.PageTurningData pageTurningData = new PageTurn.PageTurningData
		{
			m_secondsToWait = 0f,
			m_pageTurnCompleteCallback = pageTurnCompleteCallback,
			m_callbackData = callbackData,
			m_animation = base.GetComponent<Animation>()[this.PAGE_TURN_LEFT_ANIM]
		};
		base.StartCoroutine(this.WaitThenCompletePageTurnLeft(pageTurningData));
		yield break;
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x00075126 File Offset: 0x00073326
	private IEnumerator WaitThenCompletePageTurnLeft(PageTurn.PageTurningData pageTurningData)
	{
		while (base.GetComponent<Animation>().isPlaying)
		{
			yield return null;
		}
		Time.captureFramerate = 0;
		this.Show(false);
		if (pageTurningData.m_pageTurnCompleteCallback == null)
		{
			yield break;
		}
		pageTurningData.m_pageTurnCompleteCallback(pageTurningData.m_callbackData);
		yield break;
	}

	// Token: 0x06001468 RID: 5224 RVA: 0x0007513C File Offset: 0x0007333C
	private void CreateCamera()
	{
		if (this.m_OffscreenPageTurnCameraGO == null)
		{
			if (this.m_OffscreenPageTurnCamera != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_OffscreenPageTurnCamera);
			}
			this.m_OffscreenPageTurnCameraGO = new GameObject();
			this.m_OffscreenPageTurnCamera = this.m_OffscreenPageTurnCameraGO.AddComponent<Camera>();
			this.m_OffscreenPageTurnCameraGO.name = base.name + "_OffScreenPageTurnCamera";
			this.SetupCamera(this.m_OffscreenPageTurnCamera);
		}
		if (this.m_OffscreenPageTurnMaskCamera == null)
		{
			GameObject gameObject = new GameObject();
			this.m_OffscreenPageTurnMaskCamera = gameObject.AddComponent<Camera>();
			gameObject.name = base.name + "_OffScreenPageTurnMaskCamera";
			this.SetupCamera(this.m_OffscreenPageTurnMaskCamera);
			this.m_OffscreenPageTurnMaskCamera.SetReplacementShader(this.m_MaskShader, "BasePage");
		}
	}

	// Token: 0x06001469 RID: 5225 RVA: 0x0007520C File Offset: 0x0007340C
	private void SetupCamera(Camera camera)
	{
		camera.orthographic = true;
		camera.transform.parent = base.transform;
		camera.nearClipPlane = -20f;
		camera.farClipPlane = 20f;
		camera.depth = ((Camera.main == null) ? 0f : (Camera.main.depth + 100f));
		camera.backgroundColor = Color.black;
		camera.clearFlags = CameraClearFlags.Color;
		camera.cullingMask = (GameLayer.Default.LayerBit() | GameLayer.CardRaycast.LayerBit());
		camera.enabled = false;
		camera.renderingPath = RenderingPath.Forward;
		camera.allowHDR = false;
		camera.transform.Rotate(90f, 0f, 0f);
		SceneUtils.SetHideFlags(camera, HideFlags.HideAndDontSave);
	}

	// Token: 0x0600146A RID: 5226 RVA: 0x000752D0 File Offset: 0x000734D0
	private void CreateRenderTexture()
	{
		int num = Screen.currentResolution.width;
		if (num < Screen.currentResolution.height)
		{
			num = Screen.currentResolution.height;
		}
		int num2 = 512;
		if (num > 640)
		{
			num2 = 1024;
		}
		if (num > 1280)
		{
			num2 = 2048;
		}
		if (num > 2500)
		{
			num2 = 4096;
		}
		GraphicsQuality renderQualityLevel = GraphicsManager.Get().RenderQualityLevel;
		if (renderQualityLevel == GraphicsQuality.Medium)
		{
			num2 = 1024;
		}
		else if (renderQualityLevel == GraphicsQuality.Low)
		{
			num2 = 512;
		}
		if (this.m_TempRenderBuffer == null)
		{
			if (renderQualityLevel == GraphicsQuality.High)
			{
				this.m_TempRenderBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 16, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
			}
			else
			{
				bool flag = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB1555) && PlatformSettings.RuntimeOS != OSCategory.Mac;
				bool flag2 = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB4444) && PlatformSettings.RuntimeOS != OSCategory.PC;
				if (flag)
				{
					this.m_TempRenderBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 16, RenderTextureFormat.ARGB1555, RenderTextureReadWrite.Default);
				}
				else if (renderQualityLevel == GraphicsQuality.Low && flag2)
				{
					this.m_TempRenderBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 16, RenderTextureFormat.ARGB4444, RenderTextureReadWrite.Default);
				}
				else
				{
					this.m_TempRenderBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 16, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
				}
			}
			this.m_TempRenderBuffer.Create();
		}
		if (this.m_TempMaskBuffer == null)
		{
			RenderTextureFormat format = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8) ? RenderTextureFormat.R8 : RenderTextureFormat.Default;
			this.m_TempMaskBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 0, format, RenderTextureReadWrite.Default);
			this.m_TempMaskBuffer.Create();
		}
		if (this.m_OffscreenPageTurnCamera != null)
		{
			this.m_OffscreenPageTurnCamera.targetTexture = this.m_TempRenderBuffer;
		}
		if (this.m_OffscreenPageTurnMaskCamera != null)
		{
			this.m_OffscreenPageTurnMaskCamera.targetTexture = this.m_TempMaskBuffer;
		}
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x00075490 File Offset: 0x00073690
	private void Render(GameObject page)
	{
		this.Show(true);
		this.m_FrontPageGameObject.SetActive(true);
		this.m_BackPageGameObject.SetActive(true);
		this.SetCameraSize(this.m_OffscreenPageTurnCamera);
		this.m_OffscreenPageTurnCameraGO.transform.position = base.transform.position;
		bool enabled = this.m_FrontPageGameObject.GetComponent<Renderer>().enabled;
		bool enabled2 = this.m_BackPageGameObject.GetComponent<Renderer>().enabled;
		this.m_FrontPageGameObject.GetComponent<Renderer>().enabled = false;
		this.m_BackPageGameObject.GetComponent<Renderer>().enabled = false;
		bool activeSelf = this.m_TheBoxOuterFrame.activeSelf;
		this.m_TheBoxOuterFrame.SetActive(false);
		this.m_OffscreenPageTurnCamera.Render();
		this.SetCameraSize(this.m_OffscreenPageTurnMaskCamera);
		this.m_OffscreenPageTurnMaskCamera.transform.position = base.transform.position;
		this.m_OffscreenPageTurnMaskCamera.RenderWithShader(this.m_MaskShader, "BasePage");
		this.m_FrontPageGameObject.GetComponent<Renderer>().enabled = enabled;
		this.m_BackPageGameObject.GetComponent<Renderer>().enabled = enabled2;
		this.m_TheBoxOuterFrame.SetActive(activeSelf);
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x000755B5 File Offset: 0x000737B5
	private void SetCameraSize(Camera camera)
	{
		camera.orthographicSize = PageTurn.GetWorldScale(this.m_FrontPageGameObject.transform).x / 2f;
	}

	// Token: 0x0600146D RID: 5229 RVA: 0x000755D8 File Offset: 0x000737D8
	public void SetBackPageMaterial(Material material)
	{
		this.m_BackPageGameObject.GetComponent<Renderer>().SetMaterial(material);
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x000755EC File Offset: 0x000737EC
	private void SetupMaterial()
	{
		Material material = this.m_FrontPageGameObject.GetComponent<Renderer>().GetMaterial();
		material.mainTexture = this.m_TempRenderBuffer;
		material.SetTexture("_MaskTex", this.m_TempMaskBuffer);
		material.renderQueue = 3001;
		Material material2 = this.m_BackPageGameObject.GetComponent<Renderer>().GetMaterial();
		material2.SetTexture("_MaskTex", this.m_TempMaskBuffer);
		material2.renderQueue = 3002;
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0007565B File Offset: 0x0007385B
	private void Show(bool show)
	{
		base.transform.localPosition = (show ? this.m_initialPosition : (Vector3.right * this.m_RenderOffset));
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x00075683 File Offset: 0x00073883
	private IEnumerator WaitThenCompletePageTurnRight(PageTurn.PageTurningData pageTurningData)
	{
		yield return new WaitForSeconds(pageTurningData.m_secondsToWait);
		Time.captureFramerate = 0;
		this.Show(false);
		if (pageTurningData.m_pageTurnCompleteCallback == null)
		{
			yield break;
		}
		pageTurningData.m_pageTurnCompleteCallback(pageTurningData.m_callbackData);
		yield break;
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x0007569C File Offset: 0x0007389C
	public static Vector3 GetWorldScale(Transform transform)
	{
		Vector3 vector = transform.localScale;
		Transform parent = transform.parent;
		while (parent != null)
		{
			vector = Vector3.Scale(vector, parent.localScale);
			parent = parent.parent;
		}
		return vector;
	}

	// Token: 0x04000D92 RID: 3474
	private readonly string FRONT_PAGE_NAME = "PageTurnFront";

	// Token: 0x04000D93 RID: 3475
	private readonly string BACK_PAGE_NAME = "PageTurnBack";

	// Token: 0x04000D94 RID: 3476
	private readonly string WAIT_THEN_COMPLETE_PAGE_TURN_RIGHT_COROUTINE = "WaitThenCompletePageTurnRight";

	// Token: 0x04000D95 RID: 3477
	private readonly string PAGE_TURN_LEFT_ANIM = "PageTurnLeft";

	// Token: 0x04000D96 RID: 3478
	private readonly string PAGE_TURN_RIGHT_ANIM = "PageTurnRight";

	// Token: 0x04000D97 RID: 3479
	public Shader m_MaskShader;

	// Token: 0x04000D98 RID: 3480
	public float m_TurnLeftSpeed = 1.65f;

	// Token: 0x04000D99 RID: 3481
	public float m_TurnRightSpeed = 1.65f;

	// Token: 0x04000D9A RID: 3482
	public float m_TurnLeftDelayBeforePositioningPages = 0.44f;

	// Token: 0x04000D9B RID: 3483
	private Bounds m_RenderBounds;

	// Token: 0x04000D9C RID: 3484
	private Camera m_OffscreenPageTurnCamera;

	// Token: 0x04000D9D RID: 3485
	private Camera m_OffscreenPageTurnMaskCamera;

	// Token: 0x04000D9E RID: 3486
	private GameObject m_OffscreenPageTurnCameraGO;

	// Token: 0x04000D9F RID: 3487
	private RenderTexture m_TempRenderBuffer;

	// Token: 0x04000DA0 RID: 3488
	private RenderTexture m_TempMaskBuffer;

	// Token: 0x04000DA1 RID: 3489
	private GameObject m_MeshGameObject;

	// Token: 0x04000DA2 RID: 3490
	private GameObject m_FrontPageGameObject;

	// Token: 0x04000DA3 RID: 3491
	private GameObject m_BackPageGameObject;

	// Token: 0x04000DA4 RID: 3492
	private GameObject m_TheBoxOuterFrame;

	// Token: 0x04000DA5 RID: 3493
	private float m_RenderOffset = 500f;

	// Token: 0x04000DA6 RID: 3494
	private Vector3 m_initialPosition;

	// Token: 0x020014D6 RID: 5334
	// (Invoke) Token: 0x0600DC67 RID: 56423
	public delegate void DelOnPageTurnComplete(object callbackData);

	// Token: 0x020014D7 RID: 5335
	// (Invoke) Token: 0x0600DC6B RID: 56427
	public delegate void DelPositionPages(object callbackData);

	// Token: 0x020014D8 RID: 5336
	private class PageTurningData
	{
		// Token: 0x0400AB09 RID: 43785
		public float m_secondsToWait;

		// Token: 0x0400AB0A RID: 43786
		public PageTurn.DelOnPageTurnComplete m_pageTurnCompleteCallback;

		// Token: 0x0400AB0B RID: 43787
		public object m_callbackData;

		// Token: 0x0400AB0C RID: 43788
		public AnimationState m_animation;
	}

	// Token: 0x020014D9 RID: 5337
	private class TurnPageData
	{
		// Token: 0x0400AB0D RID: 43789
		public GameObject flippingPage;

		// Token: 0x0400AB0E RID: 43790
		public GameObject otherPage;

		// Token: 0x0400AB0F RID: 43791
		public PageTurn.DelOnPageTurnComplete pageTurnCompleteCallback;

		// Token: 0x0400AB10 RID: 43792
		public PageTurn.DelPositionPages positionPagesCallback;

		// Token: 0x0400AB11 RID: 43793
		public object callbackData;
	}
}
