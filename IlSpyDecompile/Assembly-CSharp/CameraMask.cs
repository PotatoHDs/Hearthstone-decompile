using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class CameraMask : MonoBehaviour
{
	public enum CAMERA_MASK_UP_VECTOR
	{
		Y,
		Z
	}

	[CustomEditField(Sections = "Mask Settings")]
	public GameObject m_ClipObjects;

	[CustomEditField(Sections = "Mask Settings")]
	public CAMERA_MASK_UP_VECTOR m_UpVector;

	[CustomEditField(Sections = "Mask Settings")]
	public float m_Width = 1f;

	[CustomEditField(Sections = "Mask Settings")]
	public float m_Height = 1f;

	[CustomEditField(Sections = "Mask Settings")]
	public bool m_RealtimeUpdate;

	[CustomEditField(Sections = "Render Camera")]
	public bool m_UseCameraFromLayer;

	[CustomEditField(Sections = "Render Camera", Parent = "m_UseCameraFromLayer")]
	public GameLayer m_CameraFromLayer;

	[CustomEditField(Sections = "Render Camera")]
	public List<GameLayer> m_CullingMasks = new List<GameLayer>
	{
		GameLayer.Default,
		GameLayer.IgnoreFullScreenEffects
	};

	private Camera m_RenderCamera;

	private Camera m_MaskCamera;

	private GameObject m_MaskCameraGameObject;

	private void Update()
	{
		if (m_RealtimeUpdate)
		{
			UpdateCameraClipping();
		}
	}

	private void OnDisable()
	{
		if (m_MaskCamera != null && UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().RemoveCameraMaskCamera(m_MaskCamera);
		}
		if (m_MaskCameraGameObject != null)
		{
			Object.Destroy(m_MaskCameraGameObject);
		}
		m_MaskCamera = null;
	}

	private void OnEnable()
	{
		Init();
	}

	private void OnDrawGizmos()
	{
		Matrix4x4 matrix = default(Matrix4x4);
		if (m_UpVector == CAMERA_MASK_UP_VECTOR.Z)
		{
			matrix.SetTRS(base.transform.position, Quaternion.identity, base.transform.lossyScale);
		}
		else
		{
			matrix.SetTRS(base.transform.position, Quaternion.Euler(90f, 0f, 0f), base.transform.lossyScale);
		}
		Gizmos.matrix = matrix;
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(m_Width, m_Height, 0f));
		Gizmos.matrix = Matrix4x4.identity;
	}

	[ContextMenu("UpdateMask")]
	public void UpdateMask()
	{
		UpdateCameraClipping();
	}

	private bool Init()
	{
		if (m_MaskCamera != null)
		{
			return false;
		}
		if (m_MaskCameraGameObject != null)
		{
			Object.Destroy(m_MaskCameraGameObject);
		}
		m_RenderCamera = (m_UseCameraFromLayer ? CameraUtils.FindFirstByLayer(m_CameraFromLayer) : Camera.main);
		if (m_RenderCamera == null)
		{
			return false;
		}
		m_MaskCameraGameObject = new GameObject("MaskCamera");
		SceneUtils.SetLayer(m_MaskCameraGameObject, GameLayer.CameraMask);
		m_MaskCameraGameObject.transform.parent = m_RenderCamera.gameObject.transform;
		m_MaskCameraGameObject.transform.localPosition = Vector3.zero;
		m_MaskCameraGameObject.transform.localRotation = Quaternion.identity;
		m_MaskCameraGameObject.transform.localScale = Vector3.one;
		int num = GameLayer.CameraMask.LayerBit();
		foreach (GameLayer cullingMask in m_CullingMasks)
		{
			num |= cullingMask.LayerBit();
		}
		m_MaskCamera = m_MaskCameraGameObject.AddComponent<Camera>();
		m_MaskCamera.CopyFrom(m_RenderCamera);
		m_MaskCamera.clearFlags = CameraClearFlags.Depth;
		m_MaskCamera.cullingMask = num;
		m_MaskCamera.depth = m_RenderCamera.depth + 1f;
		if (m_ClipObjects == null)
		{
			m_ClipObjects = base.gameObject;
		}
		Transform[] componentsInChildren = m_ClipObjects.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			GameObject gameObject = componentsInChildren[i].gameObject;
			if (!(gameObject == null))
			{
				SceneUtils.SetLayer(gameObject, GameLayer.CameraMask);
			}
		}
		UpdateCameraClipping();
		UniversalInputManager.Get().AddCameraMaskCamera(m_MaskCamera);
		return true;
	}

	private void UpdateCameraClipping()
	{
		if (m_RenderCamera == null && !Init())
		{
			return;
		}
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		if (m_UpVector == CAMERA_MASK_UP_VECTOR.Y)
		{
			zero = new Vector3(base.transform.position.x - m_Width * 0.5f * base.transform.lossyScale.x, base.transform.position.y, base.transform.position.z - m_Height * 0.5f * base.transform.lossyScale.z);
			zero2 = new Vector3(base.transform.position.x + m_Width * 0.5f * base.transform.lossyScale.x, base.transform.position.y, base.transform.position.z + m_Height * 0.5f * base.transform.lossyScale.z);
		}
		else
		{
			zero = new Vector3(base.transform.position.x - m_Width * 0.5f * base.transform.lossyScale.x, base.transform.position.y - m_Height * 0.5f * base.transform.lossyScale.y, base.transform.position.z);
			zero2 = new Vector3(base.transform.position.x + m_Width * 0.5f * base.transform.lossyScale.x, base.transform.position.y + m_Height * 0.5f * base.transform.lossyScale.y, base.transform.position.z);
		}
		Vector3 vector = m_RenderCamera.WorldToViewportPoint(zero);
		Vector3 vector2 = m_RenderCamera.WorldToViewportPoint(zero2);
		if (vector.x < 0f && vector2.x < 0f)
		{
			if (m_MaskCamera.enabled)
			{
				m_MaskCamera.enabled = false;
			}
			return;
		}
		if (vector.x > 1f && vector2.x > 1f)
		{
			if (m_MaskCamera.enabled)
			{
				m_MaskCamera.enabled = false;
			}
			return;
		}
		if (vector.y < 0f && vector2.y < 0f)
		{
			if (m_MaskCamera.enabled)
			{
				m_MaskCamera.enabled = false;
			}
			return;
		}
		if (vector.y > 1f && vector2.y > 1f)
		{
			if (m_MaskCamera.enabled)
			{
				m_MaskCamera.enabled = false;
			}
			return;
		}
		if (!m_MaskCamera.enabled)
		{
			m_MaskCamera.enabled = true;
		}
		Rect rect = new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
		if (rect.x < 0f)
		{
			rect.width += rect.x;
			rect.x = 0f;
		}
		if (rect.y < 0f)
		{
			rect.height += rect.y;
			rect.y = 0f;
		}
		if (rect.x > 1f)
		{
			rect.width -= rect.x;
			rect.x = 1f;
		}
		if (rect.y > 1f)
		{
			rect.height -= rect.y;
			rect.y = 1f;
		}
		rect.width = Mathf.Min(1f - rect.x, rect.width);
		rect.height = Mathf.Min(1f - rect.y, rect.height);
		m_MaskCamera.rect = new Rect(0f, 0f, 1f, 1f);
		m_MaskCamera.ResetProjectionMatrix();
		Matrix4x4 projectionMatrix = m_MaskCamera.projectionMatrix;
		m_MaskCamera.rect = rect;
		m_MaskCamera.projectionMatrix = Matrix4x4.TRS(new Vector3((0f - rect.x) * 2f / rect.width, (0f - rect.y) * 2f / rect.height, 0f), Quaternion.identity, Vector3.one) * Matrix4x4.TRS(new Vector3(1f / rect.width - 1f, 1f / rect.height - 1f, 0f), Quaternion.identity, new Vector3(1f / rect.width, 1f / rect.height, 1f)) * projectionMatrix;
	}
}
