using System;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

// Token: 0x02000B26 RID: 2854
public class OverlayUI : MonoBehaviour
{
	// Token: 0x0600975F RID: 38751 RVA: 0x0030EE2C File Offset: 0x0030D02C
	private void Awake()
	{
		OverlayUI.s_instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneChange));
		HearthstoneApplication.Get().WillReset += this.WillReset;
	}

	// Token: 0x06009760 RID: 38752 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x06009761 RID: 38753 RVA: 0x0030EE6B File Offset: 0x0030D06B
	private void Update()
	{
		if (this.m_clickBlocker != null)
		{
			this.m_clickBlocker.SetActive(this.m_clickBlockerRequested);
		}
		this.m_clickBlockerRequested = false;
	}

	// Token: 0x06009762 RID: 38754 RVA: 0x0030EE93 File Offset: 0x0030D093
	private void OnDestroy()
	{
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= this.WillReset;
		}
		OverlayUI.s_instance = null;
	}

	// Token: 0x06009763 RID: 38755 RVA: 0x0030EEBE File Offset: 0x0030D0BE
	public static OverlayUI Get()
	{
		return OverlayUI.s_instance;
	}

	// Token: 0x06009764 RID: 38756 RVA: 0x0030EEC8 File Offset: 0x0030D0C8
	public void AddGameObject(GameObject go, CanvasAnchor anchor = CanvasAnchor.CENTER, bool destroyOnSceneLoad = false, CanvasScaleMode scaleMode = CanvasScaleMode.HEIGHT)
	{
		CanvasAnchors canvasAnchors = (scaleMode == CanvasScaleMode.HEIGHT) ? this.m_heightScale : this.m_widthScale;
		TransformUtil.AttachAndPreserveLocalTransform(go.transform, canvasAnchors.GetAnchor(anchor));
		if (destroyOnSceneLoad)
		{
			this.DestroyOnSceneLoad(go);
		}
	}

	// Token: 0x06009765 RID: 38757 RVA: 0x0030EF05 File Offset: 0x0030D105
	public bool HasObject(GameObject gameObject)
	{
		return !(gameObject == null) && gameObject.transform.IsChildOf(base.transform);
	}

	// Token: 0x06009766 RID: 38758 RVA: 0x0030EF24 File Offset: 0x0030D124
	public Vector3 GetRelativePosition(Vector3 worldPosition, Camera camera = null, Transform bone = null, float depth = 0f)
	{
		if (camera == null)
		{
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
			{
				camera = BoardCameras.Get().GetComponentInChildren<Camera>();
			}
			else
			{
				camera = Box.Get().GetBoxCamera().GetComponent<Camera>();
			}
		}
		if (bone == null)
		{
			bone = this.m_heightScale.m_Center;
		}
		Vector3 position = camera.WorldToScreenPoint(worldPosition);
		Vector3 position2 = this.m_UICamera.ScreenToWorldPoint(position);
		position2.y = depth;
		return bone.InverseTransformPoint(position2);
	}

	// Token: 0x06009767 RID: 38759 RVA: 0x0030EFA1 File Offset: 0x0030D1A1
	public void DestroyOnSceneLoad(GameObject go)
	{
		if (!this.m_destroyOnSceneLoad.Contains(go))
		{
			this.m_destroyOnSceneLoad.Add(go);
		}
	}

	// Token: 0x06009768 RID: 38760 RVA: 0x0030EFBE File Offset: 0x0030D1BE
	public void DontDestroyOnSceneLoad(GameObject go)
	{
		if (this.m_destroyOnSceneLoad.Contains(go))
		{
			this.m_destroyOnSceneLoad.Remove(go);
		}
	}

	// Token: 0x06009769 RID: 38761 RVA: 0x0030EFDC File Offset: 0x0030D1DC
	public Transform FindBone(string name)
	{
		if (this.m_BoneParent != null)
		{
			Transform transform = this.m_BoneParent.Find(name);
			if (transform != null)
			{
				return transform;
			}
		}
		return base.transform;
	}

	// Token: 0x0600976A RID: 38762 RVA: 0x0030F015 File Offset: 0x0030D215
	public void RequestActivateClickBlocker()
	{
		this.m_clickBlockerRequested = true;
	}

	// Token: 0x0600976B RID: 38763 RVA: 0x0030F01E File Offset: 0x0030D21E
	private void OnSceneChange(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		this.m_destroyOnSceneLoad.RemoveWhere(delegate(GameObject go)
		{
			if (go != null)
			{
				UnityEngine.Object.Destroy(go);
				return true;
			}
			return false;
		});
	}

	// Token: 0x0600976C RID: 38764 RVA: 0x0030F04B File Offset: 0x0030D24B
	private void WillReset()
	{
		this.m_widthScale.WillReset();
		this.m_heightScale.WillReset();
	}

	// Token: 0x04007EBF RID: 32447
	public CanvasAnchors m_heightScale;

	// Token: 0x04007EC0 RID: 32448
	public CanvasAnchors m_widthScale;

	// Token: 0x04007EC1 RID: 32449
	public Transform m_BoneParent;

	// Token: 0x04007EC2 RID: 32450
	public GameObject m_clickBlocker;

	// Token: 0x04007EC3 RID: 32451
	public GameObject m_QuestProgressToastBone;

	// Token: 0x04007EC4 RID: 32452
	public Camera m_UICamera;

	// Token: 0x04007EC5 RID: 32453
	public Camera m_PerspectiveUICamera;

	// Token: 0x04007EC6 RID: 32454
	public Camera m_BackgroundUICamera;

	// Token: 0x04007EC7 RID: 32455
	public Camera m_HighPriorityCamera;

	// Token: 0x04007EC8 RID: 32456
	private static OverlayUI s_instance;

	// Token: 0x04007EC9 RID: 32457
	private HashSet<GameObject> m_destroyOnSceneLoad = new HashSet<GameObject>();

	// Token: 0x04007ECA RID: 32458
	private bool m_clickBlockerRequested;
}
