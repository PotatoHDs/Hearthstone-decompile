using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A14 RID: 2580
[CustomEditClass]
public class CameraMask : MonoBehaviour
{
	// Token: 0x06008B4B RID: 35659 RVA: 0x002C8665 File Offset: 0x002C6865
	private void Update()
	{
		if (this.m_RealtimeUpdate)
		{
			this.UpdateCameraClipping();
		}
	}

	// Token: 0x06008B4C RID: 35660 RVA: 0x002C8678 File Offset: 0x002C6878
	private void OnDisable()
	{
		if (this.m_MaskCamera != null && UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().RemoveCameraMaskCamera(this.m_MaskCamera);
		}
		if (this.m_MaskCameraGameObject != null)
		{
			UnityEngine.Object.Destroy(this.m_MaskCameraGameObject);
		}
		this.m_MaskCamera = null;
	}

	// Token: 0x06008B4D RID: 35661 RVA: 0x002C86CB File Offset: 0x002C68CB
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06008B4E RID: 35662 RVA: 0x002C86D4 File Offset: 0x002C68D4
	private void OnDrawGizmos()
	{
		Matrix4x4 matrix = default(Matrix4x4);
		if (this.m_UpVector == CameraMask.CAMERA_MASK_UP_VECTOR.Z)
		{
			matrix.SetTRS(base.transform.position, Quaternion.identity, base.transform.lossyScale);
		}
		else
		{
			matrix.SetTRS(base.transform.position, Quaternion.Euler(90f, 0f, 0f), base.transform.lossyScale);
		}
		Gizmos.matrix = matrix;
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(this.m_Width, this.m_Height, 0f));
		Gizmos.matrix = Matrix4x4.identity;
	}

	// Token: 0x06008B4F RID: 35663 RVA: 0x002C8781 File Offset: 0x002C6981
	[ContextMenu("UpdateMask")]
	public void UpdateMask()
	{
		this.UpdateCameraClipping();
	}

	// Token: 0x06008B50 RID: 35664 RVA: 0x002C878C File Offset: 0x002C698C
	private bool Init()
	{
		if (this.m_MaskCamera != null)
		{
			return false;
		}
		if (this.m_MaskCameraGameObject != null)
		{
			UnityEngine.Object.Destroy(this.m_MaskCameraGameObject);
		}
		this.m_RenderCamera = (this.m_UseCameraFromLayer ? CameraUtils.FindFirstByLayer(this.m_CameraFromLayer) : Camera.main);
		if (this.m_RenderCamera == null)
		{
			return false;
		}
		this.m_MaskCameraGameObject = new GameObject("MaskCamera");
		SceneUtils.SetLayer(this.m_MaskCameraGameObject, GameLayer.CameraMask);
		this.m_MaskCameraGameObject.transform.parent = this.m_RenderCamera.gameObject.transform;
		this.m_MaskCameraGameObject.transform.localPosition = Vector3.zero;
		this.m_MaskCameraGameObject.transform.localRotation = Quaternion.identity;
		this.m_MaskCameraGameObject.transform.localScale = Vector3.one;
		int num = GameLayer.CameraMask.LayerBit();
		foreach (GameLayer gameLayer in this.m_CullingMasks)
		{
			num |= gameLayer.LayerBit();
		}
		this.m_MaskCamera = this.m_MaskCameraGameObject.AddComponent<Camera>();
		this.m_MaskCamera.CopyFrom(this.m_RenderCamera);
		this.m_MaskCamera.clearFlags = CameraClearFlags.Depth;
		this.m_MaskCamera.cullingMask = num;
		this.m_MaskCamera.depth = this.m_RenderCamera.depth + 1f;
		if (this.m_ClipObjects == null)
		{
			this.m_ClipObjects = base.gameObject;
		}
		Transform[] componentsInChildren = this.m_ClipObjects.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			GameObject gameObject = componentsInChildren[i].gameObject;
			if (!(gameObject == null))
			{
				SceneUtils.SetLayer(gameObject, GameLayer.CameraMask);
			}
		}
		this.UpdateCameraClipping();
		UniversalInputManager.Get().AddCameraMaskCamera(this.m_MaskCamera);
		return true;
	}

	// Token: 0x06008B51 RID: 35665 RVA: 0x002C8984 File Offset: 0x002C6B84
	private void UpdateCameraClipping()
	{
		if (this.m_RenderCamera == null && !this.Init())
		{
			return;
		}
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		if (this.m_UpVector == CameraMask.CAMERA_MASK_UP_VECTOR.Y)
		{
			zero = new Vector3(base.transform.position.x - this.m_Width * 0.5f * base.transform.lossyScale.x, base.transform.position.y, base.transform.position.z - this.m_Height * 0.5f * base.transform.lossyScale.z);
			zero2 = new Vector3(base.transform.position.x + this.m_Width * 0.5f * base.transform.lossyScale.x, base.transform.position.y, base.transform.position.z + this.m_Height * 0.5f * base.transform.lossyScale.z);
		}
		else
		{
			zero = new Vector3(base.transform.position.x - this.m_Width * 0.5f * base.transform.lossyScale.x, base.transform.position.y - this.m_Height * 0.5f * base.transform.lossyScale.y, base.transform.position.z);
			zero2 = new Vector3(base.transform.position.x + this.m_Width * 0.5f * base.transform.lossyScale.x, base.transform.position.y + this.m_Height * 0.5f * base.transform.lossyScale.y, base.transform.position.z);
		}
		Vector3 vector = this.m_RenderCamera.WorldToViewportPoint(zero);
		Vector3 vector2 = this.m_RenderCamera.WorldToViewportPoint(zero2);
		if (vector.x < 0f && vector2.x < 0f)
		{
			if (this.m_MaskCamera.enabled)
			{
				this.m_MaskCamera.enabled = false;
			}
			return;
		}
		if (vector.x > 1f && vector2.x > 1f)
		{
			if (this.m_MaskCamera.enabled)
			{
				this.m_MaskCamera.enabled = false;
			}
			return;
		}
		if (vector.y < 0f && vector2.y < 0f)
		{
			if (this.m_MaskCamera.enabled)
			{
				this.m_MaskCamera.enabled = false;
			}
			return;
		}
		if (vector.y > 1f && vector2.y > 1f)
		{
			if (this.m_MaskCamera.enabled)
			{
				this.m_MaskCamera.enabled = false;
			}
			return;
		}
		if (!this.m_MaskCamera.enabled)
		{
			this.m_MaskCamera.enabled = true;
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
		this.m_MaskCamera.rect = new Rect(0f, 0f, 1f, 1f);
		this.m_MaskCamera.ResetProjectionMatrix();
		Matrix4x4 projectionMatrix = this.m_MaskCamera.projectionMatrix;
		this.m_MaskCamera.rect = rect;
		this.m_MaskCamera.projectionMatrix = Matrix4x4.TRS(new Vector3(-rect.x * 2f / rect.width, -rect.y * 2f / rect.height, 0f), Quaternion.identity, Vector3.one) * Matrix4x4.TRS(new Vector3(1f / rect.width - 1f, 1f / rect.height - 1f, 0f), Quaternion.identity, new Vector3(1f / rect.width, 1f / rect.height, 1f)) * projectionMatrix;
	}

	// Token: 0x040073CB RID: 29643
	[CustomEditField(Sections = "Mask Settings")]
	public GameObject m_ClipObjects;

	// Token: 0x040073CC RID: 29644
	[CustomEditField(Sections = "Mask Settings")]
	public CameraMask.CAMERA_MASK_UP_VECTOR m_UpVector;

	// Token: 0x040073CD RID: 29645
	[CustomEditField(Sections = "Mask Settings")]
	public float m_Width = 1f;

	// Token: 0x040073CE RID: 29646
	[CustomEditField(Sections = "Mask Settings")]
	public float m_Height = 1f;

	// Token: 0x040073CF RID: 29647
	[CustomEditField(Sections = "Mask Settings")]
	public bool m_RealtimeUpdate;

	// Token: 0x040073D0 RID: 29648
	[CustomEditField(Sections = "Render Camera")]
	public bool m_UseCameraFromLayer;

	// Token: 0x040073D1 RID: 29649
	[CustomEditField(Sections = "Render Camera", Parent = "m_UseCameraFromLayer")]
	public GameLayer m_CameraFromLayer;

	// Token: 0x040073D2 RID: 29650
	[CustomEditField(Sections = "Render Camera")]
	public List<GameLayer> m_CullingMasks = new List<GameLayer>
	{
		GameLayer.Default,
		GameLayer.IgnoreFullScreenEffects
	};

	// Token: 0x040073D3 RID: 29651
	private Camera m_RenderCamera;

	// Token: 0x040073D4 RID: 29652
	private Camera m_MaskCamera;

	// Token: 0x040073D5 RID: 29653
	private GameObject m_MaskCameraGameObject;

	// Token: 0x0200268A RID: 9866
	public enum CAMERA_MASK_UP_VECTOR
	{
		// Token: 0x0400F0F7 RID: 61687
		Y,
		// Token: 0x0400F0F8 RID: 61688
		Z
	}
}
