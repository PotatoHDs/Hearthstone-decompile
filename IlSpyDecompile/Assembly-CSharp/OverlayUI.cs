using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

public class OverlayUI : MonoBehaviour
{
	public CanvasAnchors m_heightScale;

	public CanvasAnchors m_widthScale;

	public Transform m_BoneParent;

	public GameObject m_clickBlocker;

	public GameObject m_QuestProgressToastBone;

	public Camera m_UICamera;

	public Camera m_PerspectiveUICamera;

	public Camera m_BackgroundUICamera;

	public Camera m_HighPriorityCamera;

	private static OverlayUI s_instance;

	private HashSet<GameObject> m_destroyOnSceneLoad = new HashSet<GameObject>();

	private bool m_clickBlockerRequested;

	private void Awake()
	{
		s_instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
		SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneChange);
		HearthstoneApplication.Get().WillReset += WillReset;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_clickBlocker != null)
		{
			m_clickBlocker.SetActive(m_clickBlockerRequested);
		}
		m_clickBlockerRequested = false;
	}

	private void OnDestroy()
	{
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= WillReset;
		}
		s_instance = null;
	}

	public static OverlayUI Get()
	{
		return s_instance;
	}

	public void AddGameObject(GameObject go, CanvasAnchor anchor = CanvasAnchor.CENTER, bool destroyOnSceneLoad = false, CanvasScaleMode scaleMode = CanvasScaleMode.HEIGHT)
	{
		CanvasAnchors canvasAnchors = ((scaleMode == CanvasScaleMode.HEIGHT) ? m_heightScale : m_widthScale);
		TransformUtil.AttachAndPreserveLocalTransform(go.transform, canvasAnchors.GetAnchor(anchor));
		if (destroyOnSceneLoad)
		{
			DestroyOnSceneLoad(go);
		}
	}

	public bool HasObject(GameObject gameObject)
	{
		if (gameObject == null)
		{
			return false;
		}
		return gameObject.transform.IsChildOf(base.transform);
	}

	public Vector3 GetRelativePosition(Vector3 worldPosition, Camera camera = null, Transform bone = null, float depth = 0f)
	{
		if (camera == null)
		{
			camera = ((SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY) ? Box.Get().GetBoxCamera().GetComponent<Camera>() : BoardCameras.Get().GetComponentInChildren<Camera>());
		}
		if (bone == null)
		{
			bone = m_heightScale.m_Center;
		}
		Vector3 position = camera.WorldToScreenPoint(worldPosition);
		Vector3 position2 = m_UICamera.ScreenToWorldPoint(position);
		position2.y = depth;
		return bone.InverseTransformPoint(position2);
	}

	public void DestroyOnSceneLoad(GameObject go)
	{
		if (!m_destroyOnSceneLoad.Contains(go))
		{
			m_destroyOnSceneLoad.Add(go);
		}
	}

	public void DontDestroyOnSceneLoad(GameObject go)
	{
		if (m_destroyOnSceneLoad.Contains(go))
		{
			m_destroyOnSceneLoad.Remove(go);
		}
	}

	public Transform FindBone(string name)
	{
		if (m_BoneParent != null)
		{
			Transform transform = m_BoneParent.Find(name);
			if (transform != null)
			{
				return transform;
			}
		}
		return base.transform;
	}

	public void RequestActivateClickBlocker()
	{
		m_clickBlockerRequested = true;
	}

	private void OnSceneChange(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		m_destroyOnSceneLoad.RemoveWhere(delegate(GameObject go)
		{
			if (go != null)
			{
				Object.Destroy(go);
				return true;
			}
			return false;
		});
	}

	private void WillReset()
	{
		m_widthScale.WillReset();
		m_heightScale.WillReset();
	}
}
