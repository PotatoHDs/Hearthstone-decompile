using System;
using System.Collections;
using UnityEngine;

public class PageTurn : MonoBehaviour
{
	public delegate void DelOnPageTurnComplete(object callbackData);

	public delegate void DelPositionPages(object callbackData);

	private class PageTurningData
	{
		public float m_secondsToWait;

		public DelOnPageTurnComplete m_pageTurnCompleteCallback;

		public object m_callbackData;

		public AnimationState m_animation;
	}

	private class TurnPageData
	{
		public GameObject flippingPage;

		public GameObject otherPage;

		public DelOnPageTurnComplete pageTurnCompleteCallback;

		public DelPositionPages positionPagesCallback;

		public object callbackData;
	}

	private readonly string FRONT_PAGE_NAME = "PageTurnFront";

	private readonly string BACK_PAGE_NAME = "PageTurnBack";

	private readonly string WAIT_THEN_COMPLETE_PAGE_TURN_RIGHT_COROUTINE = "WaitThenCompletePageTurnRight";

	private readonly string PAGE_TURN_LEFT_ANIM = "PageTurnLeft";

	private readonly string PAGE_TURN_RIGHT_ANIM = "PageTurnRight";

	public Shader m_MaskShader;

	public float m_TurnLeftSpeed = 1.65f;

	public float m_TurnRightSpeed = 1.65f;

	public float m_TurnLeftDelayBeforePositioningPages = 0.44f;

	private Bounds m_RenderBounds;

	private Camera m_OffscreenPageTurnCamera;

	private Camera m_OffscreenPageTurnMaskCamera;

	private GameObject m_OffscreenPageTurnCameraGO;

	private RenderTexture m_TempRenderBuffer;

	private RenderTexture m_TempMaskBuffer;

	private GameObject m_MeshGameObject;

	private GameObject m_FrontPageGameObject;

	private GameObject m_BackPageGameObject;

	private GameObject m_TheBoxOuterFrame;

	private float m_RenderOffset = 500f;

	private Vector3 m_initialPosition;

	private void Awake()
	{
		m_initialPosition = base.transform.localPosition;
		Transform transform = base.transform.Find(FRONT_PAGE_NAME);
		if (transform != null)
		{
			m_FrontPageGameObject = transform.gameObject;
		}
		if (m_FrontPageGameObject == null)
		{
			Debug.LogError("Failed to find " + FRONT_PAGE_NAME + " Object.");
		}
		transform = base.transform.Find(BACK_PAGE_NAME);
		if (transform != null)
		{
			m_BackPageGameObject = transform.gameObject;
		}
		if (m_BackPageGameObject == null)
		{
			Debug.LogError("Failed to find " + BACK_PAGE_NAME + " Object.");
		}
		Show(show: false);
		m_TheBoxOuterFrame = Box.Get().m_OuterFrame;
		CreateCamera();
		CreateRenderTexture();
		SetupMaterial();
	}

	protected void OnEnable()
	{
		if (m_OffscreenPageTurnCameraGO != null)
		{
			CreateCamera();
		}
		if (m_TempRenderBuffer != null || m_TempMaskBuffer != null)
		{
			CreateRenderTexture();
			SetupMaterial();
		}
	}

	protected void OnDisable()
	{
		if (m_TempRenderBuffer != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_TempRenderBuffer);
		}
		if (m_TempMaskBuffer != null)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(m_TempMaskBuffer);
		}
		if (m_OffscreenPageTurnCameraGO != null)
		{
			UnityEngine.Object.Destroy(m_OffscreenPageTurnCameraGO);
		}
		if (m_OffscreenPageTurnCamera != null)
		{
			UnityEngine.Object.Destroy(m_OffscreenPageTurnCamera);
		}
		if (m_OffscreenPageTurnMaskCamera != null)
		{
			UnityEngine.Object.Destroy(m_OffscreenPageTurnMaskCamera);
		}
	}

	public void TurnRight(GameObject flippingPage, GameObject otherPage, DelOnPageTurnComplete pageTurnCompleteCallback, DelPositionPages positionPagesCallback, object callbackData)
	{
		Render(flippingPage);
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
		GetComponent<Animation>().Stop(PAGE_TURN_RIGHT_ANIM);
		GetComponent<Animation>()[PAGE_TURN_RIGHT_ANIM].time = 0f;
		GetComponent<Animation>()[PAGE_TURN_RIGHT_ANIM].speed = m_TurnRightSpeed;
		GetComponent<Animation>().Play(PAGE_TURN_RIGHT_ANIM);
		m_FrontPageGameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Alpha", 1f);
		m_BackPageGameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Alpha", 1f);
		float secondsToWait = GetComponent<Animation>()[PAGE_TURN_RIGHT_ANIM].length / m_TurnRightSpeed;
		PageTurningData value = new PageTurningData
		{
			m_secondsToWait = secondsToWait,
			m_pageTurnCompleteCallback = pageTurnCompleteCallback,
			m_callbackData = callbackData
		};
		StopCoroutine(WAIT_THEN_COMPLETE_PAGE_TURN_RIGHT_COROUTINE);
		StartCoroutine(WAIT_THEN_COMPLETE_PAGE_TURN_RIGHT_COROUTINE, value);
		positionPagesCallback?.Invoke(callbackData);
	}

	public void TurnLeft(GameObject flippingPage, GameObject otherPage, DelOnPageTurnComplete pageTurnCompleteCallback, DelPositionPages positionPagesCallback, object callbackData)
	{
		TurnPageData turnPageData = new TurnPageData();
		turnPageData.flippingPage = flippingPage;
		turnPageData.otherPage = otherPage;
		turnPageData.pageTurnCompleteCallback = pageTurnCompleteCallback;
		turnPageData.positionPagesCallback = positionPagesCallback;
		turnPageData.callbackData = callbackData;
		StopCoroutine("TurnLeftPage");
		StartCoroutine("TurnLeftPage", turnPageData);
	}

	private IEnumerator TurnLeftPage(TurnPageData pageData)
	{
		yield return null;
		yield return null;
		yield return null;
		GameObject flippingPage = pageData.flippingPage;
		GameObject otherPage = pageData.otherPage;
		DelOnPageTurnComplete pageTurnCompleteCallback = pageData.pageTurnCompleteCallback;
		DelPositionPages positionPagesCallback = pageData.positionPagesCallback;
		object callbackData = pageData.callbackData;
		Vector3 position = flippingPage.transform.position;
		Vector3 position2 = otherPage.transform.position;
		flippingPage.transform.position = position2;
		otherPage.transform.position = position;
		Render(flippingPage);
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
		m_FrontPageGameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Alpha", 1f);
		m_BackPageGameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Alpha", 1f);
		GetComponent<Animation>().Stop(PAGE_TURN_LEFT_ANIM);
		GetComponent<Animation>()[PAGE_TURN_LEFT_ANIM].time = 0.22f;
		GetComponent<Animation>()[PAGE_TURN_LEFT_ANIM].speed = m_TurnLeftSpeed;
		GetComponent<Animation>().Play(PAGE_TURN_LEFT_ANIM);
		while (GetComponent<Animation>()[PAGE_TURN_LEFT_ANIM].time < Math.Min(GetComponent<Animation>()[PAGE_TURN_LEFT_ANIM].length, m_TurnLeftDelayBeforePositioningPages))
		{
			yield return null;
		}
		positionPagesCallback?.Invoke(callbackData);
		PageTurningData pageTurningData = new PageTurningData
		{
			m_secondsToWait = 0f,
			m_pageTurnCompleteCallback = pageTurnCompleteCallback,
			m_callbackData = callbackData,
			m_animation = GetComponent<Animation>()[PAGE_TURN_LEFT_ANIM]
		};
		StartCoroutine(WaitThenCompletePageTurnLeft(pageTurningData));
	}

	private IEnumerator WaitThenCompletePageTurnLeft(PageTurningData pageTurningData)
	{
		while (GetComponent<Animation>().isPlaying)
		{
			yield return null;
		}
		Time.captureFramerate = 0;
		Show(show: false);
		if (pageTurningData.m_pageTurnCompleteCallback != null)
		{
			pageTurningData.m_pageTurnCompleteCallback(pageTurningData.m_callbackData);
		}
	}

	private void CreateCamera()
	{
		if (m_OffscreenPageTurnCameraGO == null)
		{
			if (m_OffscreenPageTurnCamera != null)
			{
				UnityEngine.Object.DestroyImmediate(m_OffscreenPageTurnCamera);
			}
			m_OffscreenPageTurnCameraGO = new GameObject();
			m_OffscreenPageTurnCamera = m_OffscreenPageTurnCameraGO.AddComponent<Camera>();
			m_OffscreenPageTurnCameraGO.name = base.name + "_OffScreenPageTurnCamera";
			SetupCamera(m_OffscreenPageTurnCamera);
		}
		if (m_OffscreenPageTurnMaskCamera == null)
		{
			GameObject gameObject = new GameObject();
			m_OffscreenPageTurnMaskCamera = gameObject.AddComponent<Camera>();
			gameObject.name = base.name + "_OffScreenPageTurnMaskCamera";
			SetupCamera(m_OffscreenPageTurnMaskCamera);
			m_OffscreenPageTurnMaskCamera.SetReplacementShader(m_MaskShader, "BasePage");
		}
	}

	private void SetupCamera(Camera camera)
	{
		camera.orthographic = true;
		camera.transform.parent = base.transform;
		camera.nearClipPlane = -20f;
		camera.farClipPlane = 20f;
		camera.depth = ((Camera.main == null) ? 0f : (Camera.main.depth + 100f));
		camera.backgroundColor = Color.black;
		camera.clearFlags = CameraClearFlags.Color;
		camera.cullingMask = GameLayer.Default.LayerBit() | GameLayer.CardRaycast.LayerBit();
		camera.enabled = false;
		camera.renderingPath = RenderingPath.Forward;
		camera.allowHDR = false;
		camera.transform.Rotate(90f, 0f, 0f);
		SceneUtils.SetHideFlags(camera, HideFlags.HideAndDontSave);
	}

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
		switch (renderQualityLevel)
		{
		case GraphicsQuality.Medium:
			num2 = 1024;
			break;
		case GraphicsQuality.Low:
			num2 = 512;
			break;
		}
		if (m_TempRenderBuffer == null)
		{
			if (renderQualityLevel == GraphicsQuality.High)
			{
				m_TempRenderBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 16, RenderTextureFormat.ARGB32);
			}
			else
			{
				bool num3 = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB1555) && PlatformSettings.RuntimeOS != OSCategory.Mac;
				bool flag = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB4444) && PlatformSettings.RuntimeOS != OSCategory.PC;
				if (num3)
				{
					m_TempRenderBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 16, RenderTextureFormat.ARGB1555);
				}
				else if (renderQualityLevel == GraphicsQuality.Low && flag)
				{
					m_TempRenderBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 16, RenderTextureFormat.ARGB4444);
				}
				else
				{
					m_TempRenderBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 16);
				}
			}
			m_TempRenderBuffer.Create();
		}
		if (m_TempMaskBuffer == null)
		{
			RenderTextureFormat format = (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.R8) ? RenderTextureFormat.R8 : RenderTextureFormat.Default);
			m_TempMaskBuffer = RenderTextureTracker.Get().CreateNewTexture(num2, num2, 0, format);
			m_TempMaskBuffer.Create();
		}
		if (m_OffscreenPageTurnCamera != null)
		{
			m_OffscreenPageTurnCamera.targetTexture = m_TempRenderBuffer;
		}
		if (m_OffscreenPageTurnMaskCamera != null)
		{
			m_OffscreenPageTurnMaskCamera.targetTexture = m_TempMaskBuffer;
		}
	}

	private void Render(GameObject page)
	{
		Show(show: true);
		m_FrontPageGameObject.SetActive(value: true);
		m_BackPageGameObject.SetActive(value: true);
		SetCameraSize(m_OffscreenPageTurnCamera);
		m_OffscreenPageTurnCameraGO.transform.position = base.transform.position;
		bool flag = m_FrontPageGameObject.GetComponent<Renderer>().enabled;
		bool flag2 = m_BackPageGameObject.GetComponent<Renderer>().enabled;
		m_FrontPageGameObject.GetComponent<Renderer>().enabled = false;
		m_BackPageGameObject.GetComponent<Renderer>().enabled = false;
		bool activeSelf = m_TheBoxOuterFrame.activeSelf;
		m_TheBoxOuterFrame.SetActive(value: false);
		m_OffscreenPageTurnCamera.Render();
		SetCameraSize(m_OffscreenPageTurnMaskCamera);
		m_OffscreenPageTurnMaskCamera.transform.position = base.transform.position;
		m_OffscreenPageTurnMaskCamera.RenderWithShader(m_MaskShader, "BasePage");
		m_FrontPageGameObject.GetComponent<Renderer>().enabled = flag;
		m_BackPageGameObject.GetComponent<Renderer>().enabled = flag2;
		m_TheBoxOuterFrame.SetActive(activeSelf);
	}

	private void SetCameraSize(Camera camera)
	{
		camera.orthographicSize = GetWorldScale(m_FrontPageGameObject.transform).x / 2f;
	}

	public void SetBackPageMaterial(Material material)
	{
		m_BackPageGameObject.GetComponent<Renderer>().SetMaterial(material);
	}

	private void SetupMaterial()
	{
		Material material = m_FrontPageGameObject.GetComponent<Renderer>().GetMaterial();
		material.mainTexture = m_TempRenderBuffer;
		material.SetTexture("_MaskTex", m_TempMaskBuffer);
		material.renderQueue = 3001;
		Material material2 = m_BackPageGameObject.GetComponent<Renderer>().GetMaterial();
		material2.SetTexture("_MaskTex", m_TempMaskBuffer);
		material2.renderQueue = 3002;
	}

	private void Show(bool show)
	{
		base.transform.localPosition = (show ? m_initialPosition : (Vector3.right * m_RenderOffset));
	}

	private IEnumerator WaitThenCompletePageTurnRight(PageTurningData pageTurningData)
	{
		yield return new WaitForSeconds(pageTurningData.m_secondsToWait);
		Time.captureFramerate = 0;
		Show(show: false);
		if (pageTurningData.m_pageTurnCompleteCallback != null)
		{
			pageTurningData.m_pageTurnCompleteCallback(pageTurningData.m_callbackData);
		}
	}

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
}
