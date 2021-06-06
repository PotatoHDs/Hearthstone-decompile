using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009A2 RID: 2466
public class CameraUtils
{
	// Token: 0x06008687 RID: 34439 RVA: 0x002B71B3 File Offset: 0x002B53B3
	public static Camera FindFirstByLayer(int layer)
	{
		return CameraUtils.FindFirstByLayerMask(1 << layer);
	}

	// Token: 0x06008688 RID: 34440 RVA: 0x002B71C5 File Offset: 0x002B53C5
	public static Camera FindFirstByLayer(GameLayer layer)
	{
		return CameraUtils.FindFirstByLayerMask(layer.LayerBit());
	}

	// Token: 0x06008689 RID: 34441 RVA: 0x002B71D8 File Offset: 0x002B53D8
	public static Camera FindFirstByLayerMask(LayerMask mask)
	{
		foreach (Camera camera in Camera.allCameras)
		{
			if ((camera.cullingMask & mask) != 0)
			{
				return camera;
			}
		}
		return null;
	}

	// Token: 0x0600868A RID: 34442 RVA: 0x002B720F File Offset: 0x002B540F
	public static void FindAllByLayer(int layer, List<Camera> cameras)
	{
		CameraUtils.FindAllByLayerMask(1 << layer, cameras);
	}

	// Token: 0x0600868B RID: 34443 RVA: 0x002B7222 File Offset: 0x002B5422
	public static void FindAllByLayer(GameLayer layer, List<Camera> cameras)
	{
		CameraUtils.FindAllByLayerMask(layer.LayerBit(), cameras);
	}

	// Token: 0x0600868C RID: 34444 RVA: 0x002B7238 File Offset: 0x002B5438
	public static void FindAllByLayerMask(LayerMask mask, List<Camera> cameras)
	{
		foreach (Camera camera in Camera.allCameras)
		{
			if ((camera.cullingMask & mask) != 0)
			{
				cameras.Add(camera);
			}
		}
	}

	// Token: 0x0600868D RID: 34445 RVA: 0x002B7274 File Offset: 0x002B5474
	public static Camera FindFullScreenEffectsCamera(bool activeOnly)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("MainFullscreenFXCamera");
		if (gameObject == null)
		{
			return null;
		}
		FullScreenEffects component = gameObject.GetComponent<FullScreenEffects>();
		if (component == null)
		{
			return null;
		}
		if (!activeOnly || component.IsActive)
		{
			return component.Camera;
		}
		return null;
	}

	// Token: 0x0600868E RID: 34446 RVA: 0x002B72BC File Offset: 0x002B54BC
	public static LayerMask CreateLayerMask(List<Camera> cameras)
	{
		LayerMask layerMask = 0;
		foreach (Camera camera in cameras)
		{
			layerMask |= camera.cullingMask;
		}
		return layerMask;
	}

	// Token: 0x0600868F RID: 34447 RVA: 0x002B7320 File Offset: 0x002B5520
	public static Plane CreateTopPlane(Camera camera)
	{
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 1f, camera.nearClipPlane));
		Vector3 a = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
		Vector3 inNormal = Vector3.Cross(camera.ViewportToWorldPoint(new Vector3(0f, 1f, camera.farClipPlane)) - vector, a - vector);
		inNormal.Normalize();
		return new Plane(inNormal, vector);
	}

	// Token: 0x06008690 RID: 34448 RVA: 0x002B73A4 File Offset: 0x002B55A4
	public static Plane CreateBottomPlane(Camera camera)
	{
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane));
		Vector3 a = camera.ViewportToWorldPoint(new Vector3(1f, 0f, camera.nearClipPlane));
		Vector3 inNormal = Vector3.Cross(camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane)) - vector, a - vector);
		inNormal.Normalize();
		return new Plane(inNormal, vector);
	}

	// Token: 0x06008691 RID: 34449 RVA: 0x002B7428 File Offset: 0x002B5628
	public static Bounds GetNearClipBounds(Camera camera)
	{
		Vector3 center = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane));
		Vector3 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
		Vector3 size = new Vector3(vector2.x - vector.x, vector2.y - vector.y, vector2.z - vector.z);
		return new Bounds(center, size);
	}

	// Token: 0x06008692 RID: 34450 RVA: 0x002B74BC File Offset: 0x002B56BC
	public static Bounds GetFarClipBounds(Camera camera)
	{
		Vector3 center = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.farClipPlane));
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane));
		Vector3 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.farClipPlane));
		Vector3 size = new Vector3(vector2.x - vector.x, vector2.y - vector.y, vector2.z - vector.z);
		return new Bounds(center, size);
	}

	// Token: 0x06008693 RID: 34451 RVA: 0x002B7550 File Offset: 0x002B5750
	public static Rect CreateGUIViewportRect(Camera camera, Component topLeft, Component bottomRight)
	{
		return CameraUtils.CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	// Token: 0x06008694 RID: 34452 RVA: 0x002B756E File Offset: 0x002B576E
	public static Rect CreateGUIViewportRect(Camera camera, GameObject topLeft, Component bottomRight)
	{
		return CameraUtils.CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	// Token: 0x06008695 RID: 34453 RVA: 0x002B758C File Offset: 0x002B578C
	public static Rect CreateGUIViewportRect(Camera camera, Component topLeft, GameObject bottomRight)
	{
		return CameraUtils.CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	// Token: 0x06008696 RID: 34454 RVA: 0x002B75AA File Offset: 0x002B57AA
	public static Rect CreateGUIViewportRect(Camera camera, GameObject topLeft, GameObject bottomRight)
	{
		return CameraUtils.CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	// Token: 0x06008697 RID: 34455 RVA: 0x002B75C8 File Offset: 0x002B57C8
	public static Rect CreateGUIViewportRect(Camera camera, Vector3 worldTopLeft, Vector3 worldBottomRight)
	{
		Vector3 vector = camera.WorldToViewportPoint(worldTopLeft);
		Vector3 vector2 = camera.WorldToViewportPoint(worldBottomRight);
		return new Rect(vector.x, 1f - vector.y, vector2.x - vector.x, vector.y - vector2.y);
	}

	// Token: 0x06008698 RID: 34456 RVA: 0x002B7616 File Offset: 0x002B5816
	public static Rect CreateGUIScreenRect(Camera camera, Component topLeft, Component bottomRight)
	{
		return CameraUtils.CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	// Token: 0x06008699 RID: 34457 RVA: 0x002B7634 File Offset: 0x002B5834
	public static Rect CreateGUIScreenRect(Camera camera, GameObject topLeft, Component bottomRight)
	{
		return CameraUtils.CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	// Token: 0x0600869A RID: 34458 RVA: 0x002B7652 File Offset: 0x002B5852
	public static Rect CreateGUIScreenRect(Camera camera, Component topLeft, GameObject bottomRight)
	{
		return CameraUtils.CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	// Token: 0x0600869B RID: 34459 RVA: 0x002B7670 File Offset: 0x002B5870
	public static Rect CreateGUIScreenRect(Camera camera, GameObject topLeft, GameObject bottomRight)
	{
		return CameraUtils.CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	// Token: 0x0600869C RID: 34460 RVA: 0x002B7690 File Offset: 0x002B5890
	public static Rect CreateGUIScreenRect(Camera camera, Vector3 worldTopLeft, Vector3 worldBottomRight)
	{
		Vector3 vector = camera.WorldToScreenPoint(worldTopLeft);
		Vector3 vector2 = camera.WorldToScreenPoint(worldBottomRight);
		return new Rect(vector.x, vector2.y, vector2.x - vector.x, vector.y - vector2.y);
	}

	// Token: 0x0600869D RID: 34461 RVA: 0x002B76D8 File Offset: 0x002B58D8
	public static bool Raycast(Camera camera, Vector3 screenPoint, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		return camera.pixelRect.Contains(screenPoint) && Physics.Raycast(camera.ScreenPointToRay(screenPoint), out hitInfo, camera.farClipPlane, camera.cullingMask);
	}

	// Token: 0x0600869E RID: 34462 RVA: 0x002B7718 File Offset: 0x002B5918
	public static bool Raycast(Camera camera, Vector3 screenPoint, LayerMask layerMask, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		return camera.pixelRect.Contains(screenPoint) && Physics.Raycast(camera.ScreenPointToRay(screenPoint), out hitInfo, camera.farClipPlane, layerMask);
	}

	// Token: 0x0600869F RID: 34463 RVA: 0x002B7758 File Offset: 0x002B5958
	public static bool RaycastAll(Camera camera, Vector3 screenPoint, LayerMask layerMask, out RaycastHit[] hitInfos)
	{
		if (!camera.pixelRect.Contains(screenPoint))
		{
			hitInfos = null;
			return false;
		}
		Ray ray = camera.ScreenPointToRay(screenPoint);
		hitInfos = Physics.RaycastAll(ray, camera.farClipPlane, layerMask);
		return hitInfos != null && hitInfos.Length != 0;
	}

	// Token: 0x060086A0 RID: 34464 RVA: 0x002B77A3 File Offset: 0x002B59A3
	public static GameObject CreateInputBlocker(Camera camera)
	{
		return CameraUtils.CreateInputBlocker(camera, string.Empty, null, null, 0f);
	}

	// Token: 0x060086A1 RID: 34465 RVA: 0x002B77B7 File Offset: 0x002B59B7
	public static GameObject CreateInputBlocker(Camera camera, string name)
	{
		return CameraUtils.CreateInputBlocker(camera, name, null, null, 0f);
	}

	// Token: 0x060086A2 RID: 34466 RVA: 0x002B77C7 File Offset: 0x002B59C7
	public static GameObject CreateInputBlocker(Camera camera, string name, Component parent)
	{
		return CameraUtils.CreateInputBlocker(camera, name, parent, parent, 0f);
	}

	// Token: 0x060086A3 RID: 34467 RVA: 0x002B77D7 File Offset: 0x002B59D7
	public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, float worldOffset)
	{
		return CameraUtils.CreateInputBlocker(camera, name, parent, parent, worldOffset);
	}

	// Token: 0x060086A4 RID: 34468 RVA: 0x002B77E3 File Offset: 0x002B59E3
	public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, Component relative)
	{
		return CameraUtils.CreateInputBlocker(camera, name, parent, relative, 0f);
	}

	// Token: 0x060086A5 RID: 34469 RVA: 0x002B77F4 File Offset: 0x002B59F4
	public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, Component relative, float worldOffset)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.layer = camera.gameObject.layer;
		gameObject.transform.parent = ((parent == null) ? null : parent.transform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.rotation = Quaternion.Inverse(camera.transform.rotation);
		if (relative == null)
		{
			gameObject.transform.position = CameraUtils.GetPosInFrontOfCamera(camera, camera.nearClipPlane + worldOffset);
		}
		else
		{
			gameObject.transform.position = CameraUtils.GetPosInFrontOfCamera(camera, relative.transform.position, worldOffset);
		}
		Bounds farClipBounds = CameraUtils.GetFarClipBounds(camera);
		Vector3 vector;
		if (parent == null)
		{
			vector = Vector3.one;
		}
		else
		{
			vector = TransformUtil.ComputeWorldScale(parent);
		}
		Vector3 size = default(Vector3);
		size.x = farClipBounds.size.x / vector.x;
		if (farClipBounds.size.z > 0f)
		{
			size.y = farClipBounds.size.z / vector.z;
		}
		else
		{
			size.y = farClipBounds.size.y / vector.y;
		}
		gameObject.AddComponent<BoxCollider>().size = size;
		return gameObject;
	}

	// Token: 0x060086A6 RID: 34470 RVA: 0x002B793B File Offset: 0x002B5B3B
	public static float ScreenToWorldDist(Camera camera, float screenDist)
	{
		return CameraUtils.ScreenToWorldDist(camera, screenDist, camera.nearClipPlane);
	}

	// Token: 0x060086A7 RID: 34471 RVA: 0x002B794C File Offset: 0x002B5B4C
	public static float ScreenToWorldDist(Camera camera, float screenDist, float worldDist)
	{
		Vector3 vector = camera.ScreenToWorldPoint(new Vector3(0f, 0f, worldDist));
		return camera.ScreenToWorldPoint(new Vector3(screenDist, 0f, worldDist)).x - vector.x;
	}

	// Token: 0x060086A8 RID: 34472 RVA: 0x002B7990 File Offset: 0x002B5B90
	public static float ScreenToWorldDist(Camera camera, float screenDist, Vector3 worldPoint)
	{
		float worldDist = Vector3.Distance(camera.transform.position, worldPoint);
		return CameraUtils.ScreenToWorldDist(camera, screenDist, worldDist);
	}

	// Token: 0x060086A9 RID: 34473 RVA: 0x002B79B8 File Offset: 0x002B5BB8
	public static Vector3 GetPosInFrontOfCamera(Camera camera, float worldDistance)
	{
		Vector3 position = camera.transform.position + new Vector3(0f, 0f, worldDistance);
		float magnitude = camera.transform.InverseTransformPoint(position).magnitude;
		Vector3 position2 = new Vector3(0f, 0f, magnitude);
		return camera.transform.TransformPoint(position2);
	}

	// Token: 0x060086AA RID: 34474 RVA: 0x002B7A19 File Offset: 0x002B5C19
	public static Vector3 GetPosInFrontOfCamera(Camera camera, Vector3 worldPoint)
	{
		return CameraUtils.GetPosInFrontOfCamera(camera, worldPoint, 0f);
	}

	// Token: 0x060086AB RID: 34475 RVA: 0x002B7A28 File Offset: 0x002B5C28
	public static Vector3 GetPosInFrontOfCamera(Camera camera, Vector3 worldPoint, float worldOffset)
	{
		Vector3 position = camera.transform.position;
		Vector3 forward = camera.transform.forward;
		Plane plane = new Plane(-forward, worldPoint);
		Vector3 b = (plane.GetDistanceToPoint(position) + worldOffset) * forward;
		return position + b;
	}

	// Token: 0x060086AC RID: 34476 RVA: 0x002B7A73 File Offset: 0x002B5C73
	public static Vector3 GetNearestPosInFrontOfCamera(Camera camera, float worldOffset = 0f)
	{
		return CameraUtils.GetPosInFrontOfCamera(camera, camera.nearClipPlane + worldOffset);
	}

	// Token: 0x060086AD RID: 34477 RVA: 0x002B7A84 File Offset: 0x002B5C84
	public static Camera GetMainCamera()
	{
		if (Application.isPlaying && Box.Get() != null)
		{
			return Box.Get().GetCamera();
		}
		if (Application.isPlaying && BoardCameras.Get() != null)
		{
			return BoardCameras.Get().GetComponentInChildren<Camera>();
		}
		return Camera.main;
	}

	// Token: 0x040071F9 RID: 29177
	public const float PopupCameraOffset = 0.1f;

	// Token: 0x040071FA RID: 29178
	public const float MaskCameraOffset = 0.01f;

	// Token: 0x040071FB RID: 29179
	public const float FullScreenEffectsCameraOffset = 0.001f;
}
