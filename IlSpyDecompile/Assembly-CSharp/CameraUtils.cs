using System.Collections.Generic;
using UnityEngine;

public class CameraUtils
{
	public const float PopupCameraOffset = 0.1f;

	public const float MaskCameraOffset = 0.01f;

	public const float FullScreenEffectsCameraOffset = 0.001f;

	public static Camera FindFirstByLayer(int layer)
	{
		return FindFirstByLayerMask(1 << layer);
	}

	public static Camera FindFirstByLayer(GameLayer layer)
	{
		return FindFirstByLayerMask(layer.LayerBit());
	}

	public static Camera FindFirstByLayerMask(LayerMask mask)
	{
		Camera[] allCameras = Camera.allCameras;
		foreach (Camera camera in allCameras)
		{
			if ((camera.cullingMask & (int)mask) != 0)
			{
				return camera;
			}
		}
		return null;
	}

	public static void FindAllByLayer(int layer, List<Camera> cameras)
	{
		FindAllByLayerMask(1 << layer, cameras);
	}

	public static void FindAllByLayer(GameLayer layer, List<Camera> cameras)
	{
		FindAllByLayerMask(layer.LayerBit(), cameras);
	}

	public static void FindAllByLayerMask(LayerMask mask, List<Camera> cameras)
	{
		Camera[] allCameras = Camera.allCameras;
		foreach (Camera camera in allCameras)
		{
			if ((camera.cullingMask & (int)mask) != 0)
			{
				cameras.Add(camera);
			}
		}
	}

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

	public static LayerMask CreateLayerMask(List<Camera> cameras)
	{
		LayerMask layerMask = 0;
		foreach (Camera camera in cameras)
		{
			layerMask = (int)layerMask | camera.cullingMask;
		}
		return layerMask;
	}

	public static Plane CreateTopPlane(Camera camera)
	{
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 1f, camera.nearClipPlane));
		Vector3 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
		Vector3 inNormal = Vector3.Cross(camera.ViewportToWorldPoint(new Vector3(0f, 1f, camera.farClipPlane)) - vector, vector2 - vector);
		inNormal.Normalize();
		return new Plane(inNormal, vector);
	}

	public static Plane CreateBottomPlane(Camera camera)
	{
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane));
		Vector3 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 0f, camera.nearClipPlane));
		Vector3 inNormal = Vector3.Cross(camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane)) - vector, vector2 - vector);
		inNormal.Normalize();
		return new Plane(inNormal, vector);
	}

	public static Bounds GetNearClipBounds(Camera camera)
	{
		Vector3 center = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.nearClipPlane));
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane));
		Vector3 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
		Vector3 size = new Vector3(vector2.x - vector.x, vector2.y - vector.y, vector2.z - vector.z);
		return new Bounds(center, size);
	}

	public static Bounds GetFarClipBounds(Camera camera)
	{
		Vector3 center = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camera.farClipPlane));
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane));
		Vector3 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.farClipPlane));
		Vector3 size = new Vector3(vector2.x - vector.x, vector2.y - vector.y, vector2.z - vector.z);
		return new Bounds(center, size);
	}

	public static Rect CreateGUIViewportRect(Camera camera, Component topLeft, Component bottomRight)
	{
		return CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	public static Rect CreateGUIViewportRect(Camera camera, GameObject topLeft, Component bottomRight)
	{
		return CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	public static Rect CreateGUIViewportRect(Camera camera, Component topLeft, GameObject bottomRight)
	{
		return CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	public static Rect CreateGUIViewportRect(Camera camera, GameObject topLeft, GameObject bottomRight)
	{
		return CreateGUIViewportRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	public static Rect CreateGUIViewportRect(Camera camera, Vector3 worldTopLeft, Vector3 worldBottomRight)
	{
		Vector3 vector = camera.WorldToViewportPoint(worldTopLeft);
		Vector3 vector2 = camera.WorldToViewportPoint(worldBottomRight);
		return new Rect(vector.x, 1f - vector.y, vector2.x - vector.x, vector.y - vector2.y);
	}

	public static Rect CreateGUIScreenRect(Camera camera, Component topLeft, Component bottomRight)
	{
		return CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	public static Rect CreateGUIScreenRect(Camera camera, GameObject topLeft, Component bottomRight)
	{
		return CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	public static Rect CreateGUIScreenRect(Camera camera, Component topLeft, GameObject bottomRight)
	{
		return CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	public static Rect CreateGUIScreenRect(Camera camera, GameObject topLeft, GameObject bottomRight)
	{
		return CreateGUIScreenRect(camera, topLeft.transform.position, bottomRight.transform.position);
	}

	public static Rect CreateGUIScreenRect(Camera camera, Vector3 worldTopLeft, Vector3 worldBottomRight)
	{
		Vector3 vector = camera.WorldToScreenPoint(worldTopLeft);
		Vector3 vector2 = camera.WorldToScreenPoint(worldBottomRight);
		return new Rect(vector.x, vector2.y, vector2.x - vector.x, vector.y - vector2.y);
	}

	public static bool Raycast(Camera camera, Vector3 screenPoint, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		if (!camera.pixelRect.Contains(screenPoint))
		{
			return false;
		}
		return Physics.Raycast(camera.ScreenPointToRay(screenPoint), out hitInfo, camera.farClipPlane, camera.cullingMask);
	}

	public static bool Raycast(Camera camera, Vector3 screenPoint, LayerMask layerMask, out RaycastHit hitInfo)
	{
		hitInfo = default(RaycastHit);
		if (!camera.pixelRect.Contains(screenPoint))
		{
			return false;
		}
		return Physics.Raycast(camera.ScreenPointToRay(screenPoint), out hitInfo, camera.farClipPlane, layerMask);
	}

	public static bool RaycastAll(Camera camera, Vector3 screenPoint, LayerMask layerMask, out RaycastHit[] hitInfos)
	{
		if (!camera.pixelRect.Contains(screenPoint))
		{
			hitInfos = null;
			return false;
		}
		Ray ray = camera.ScreenPointToRay(screenPoint);
		hitInfos = Physics.RaycastAll(ray, camera.farClipPlane, layerMask);
		if (hitInfos != null)
		{
			return hitInfos.Length != 0;
		}
		return false;
	}

	public static GameObject CreateInputBlocker(Camera camera)
	{
		return CreateInputBlocker(camera, string.Empty, null, null, 0f);
	}

	public static GameObject CreateInputBlocker(Camera camera, string name)
	{
		return CreateInputBlocker(camera, name, null, null, 0f);
	}

	public static GameObject CreateInputBlocker(Camera camera, string name, Component parent)
	{
		return CreateInputBlocker(camera, name, parent, parent, 0f);
	}

	public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, float worldOffset)
	{
		return CreateInputBlocker(camera, name, parent, parent, worldOffset);
	}

	public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, Component relative)
	{
		return CreateInputBlocker(camera, name, parent, relative, 0f);
	}

	public static GameObject CreateInputBlocker(Camera camera, string name, Component parent, Component relative, float worldOffset)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.layer = camera.gameObject.layer;
		gameObject.transform.parent = ((parent == null) ? null : parent.transform);
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.rotation = Quaternion.Inverse(camera.transform.rotation);
		if (relative == null)
		{
			gameObject.transform.position = GetPosInFrontOfCamera(camera, camera.nearClipPlane + worldOffset);
		}
		else
		{
			gameObject.transform.position = GetPosInFrontOfCamera(camera, relative.transform.position, worldOffset);
		}
		Bounds farClipBounds = GetFarClipBounds(camera);
		Vector3 vector = ((!(parent == null)) ? TransformUtil.ComputeWorldScale(parent) : Vector3.one);
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

	public static float ScreenToWorldDist(Camera camera, float screenDist)
	{
		return ScreenToWorldDist(camera, screenDist, camera.nearClipPlane);
	}

	public static float ScreenToWorldDist(Camera camera, float screenDist, float worldDist)
	{
		Vector3 vector = camera.ScreenToWorldPoint(new Vector3(0f, 0f, worldDist));
		return camera.ScreenToWorldPoint(new Vector3(screenDist, 0f, worldDist)).x - vector.x;
	}

	public static float ScreenToWorldDist(Camera camera, float screenDist, Vector3 worldPoint)
	{
		float worldDist = Vector3.Distance(camera.transform.position, worldPoint);
		return ScreenToWorldDist(camera, screenDist, worldDist);
	}

	public static Vector3 GetPosInFrontOfCamera(Camera camera, float worldDistance)
	{
		Vector3 position = camera.transform.position + new Vector3(0f, 0f, worldDistance);
		float magnitude = camera.transform.InverseTransformPoint(position).magnitude;
		Vector3 position2 = new Vector3(0f, 0f, magnitude);
		return camera.transform.TransformPoint(position2);
	}

	public static Vector3 GetPosInFrontOfCamera(Camera camera, Vector3 worldPoint)
	{
		return GetPosInFrontOfCamera(camera, worldPoint, 0f);
	}

	public static Vector3 GetPosInFrontOfCamera(Camera camera, Vector3 worldPoint, float worldOffset)
	{
		Vector3 position = camera.transform.position;
		Vector3 forward = camera.transform.forward;
		Vector3 vector = (new Plane(-forward, worldPoint).GetDistanceToPoint(position) + worldOffset) * forward;
		return position + vector;
	}

	public static Vector3 GetNearestPosInFrontOfCamera(Camera camera, float worldOffset = 0f)
	{
		return GetPosInFrontOfCamera(camera, camera.nearClipPlane + worldOffset);
	}

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
}
