using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class ScreenEffectsMgr : IService, IHasUpdate
{
	public enum EFFECT_TYPE
	{
		Glow,
		Distortion
	}

	private Camera m_EffectsObjectsCamera;

	private GameObject m_EffectsObjectsCameraGO;

	private Camera m_MainCamera;

	private ScreenEffectsRender m_ScreenEffectsRender;

	private bool m_enabled;

	private static List<ScreenEffect> m_ActiveScreenEffects;

	public bool IsActive => m_enabled;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (m_ActiveScreenEffects == null)
		{
			m_ActiveScreenEffects = new List<ScreenEffect>();
		}
		yield return new WaitForMainCamera();
		OnEnable();
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(UniversalInputManager) };
	}

	public void Shutdown()
	{
		OnDisable();
		if (m_ActiveScreenEffects != null)
		{
			m_ActiveScreenEffects.Clear();
			m_ActiveScreenEffects = null;
		}
	}

	public void Update()
	{
		if (m_MainCamera == null)
		{
			if (Camera.main == null)
			{
				return;
			}
			Init();
		}
		if (m_ScreenEffectsRender == null)
		{
			return;
		}
		if (m_ActiveScreenEffects != null && m_ActiveScreenEffects.Count > 0)
		{
			if (!m_ScreenEffectsRender.enabled)
			{
				m_ScreenEffectsRender.enabled = true;
			}
		}
		else if (m_ScreenEffectsRender.enabled)
		{
			m_ScreenEffectsRender.enabled = false;
		}
		UpdateCameraTransform();
	}

	public void SetActive(bool enabled)
	{
		if (m_enabled != enabled)
		{
			m_enabled = enabled;
			if (m_enabled)
			{
				OnEnable();
			}
			else
			{
				OnDisable();
			}
		}
	}

	private void OnDisable()
	{
		if (m_EffectsObjectsCameraGO != null)
		{
			UnityEngine.Object.DestroyImmediate(m_EffectsObjectsCameraGO);
		}
		if (m_ScreenEffectsRender != null)
		{
			m_ScreenEffectsRender.enabled = false;
		}
	}

	private void OnEnable()
	{
		if (!(Camera.main == null))
		{
			Init();
		}
	}

	public static ScreenEffectsMgr Get()
	{
		return HearthstoneServices.Get<ScreenEffectsMgr>();
	}

	public static void RegisterScreenEffect(ScreenEffect effect)
	{
		if (m_ActiveScreenEffects == null)
		{
			m_ActiveScreenEffects = new List<ScreenEffect>();
		}
		if (!m_ActiveScreenEffects.Contains(effect))
		{
			m_ActiveScreenEffects.Add(effect);
		}
	}

	public static void UnRegisterScreenEffect(ScreenEffect effect)
	{
		if (m_ActiveScreenEffects != null)
		{
			m_ActiveScreenEffects.Remove(effect);
		}
	}

	public int GetActiveScreenEffectsCount()
	{
		if (m_ActiveScreenEffects == null)
		{
			return 0;
		}
		return m_ActiveScreenEffects.Count;
	}

	private void Init()
	{
		m_MainCamera = Camera.main;
		if (!(m_MainCamera == null))
		{
			m_ScreenEffectsRender = m_MainCamera.GetComponent<ScreenEffectsRender>();
			if (m_ScreenEffectsRender == null)
			{
				m_ScreenEffectsRender = m_MainCamera.gameObject.AddComponent<ScreenEffectsRender>();
				m_MainCamera.allowHDR = false;
			}
			else
			{
				m_ScreenEffectsRender.enabled = true;
			}
			CreateCamera(out m_EffectsObjectsCamera, out m_EffectsObjectsCameraGO, "ScreenEffectsObjectRenderCamera");
			m_EffectsObjectsCamera.depth = m_MainCamera.depth - 1f;
			m_EffectsObjectsCamera.clearFlags = CameraClearFlags.Color;
			m_EffectsObjectsCamera.backgroundColor = Color.clear;
			SceneUtils.SetLayer(m_EffectsObjectsCameraGO, 23);
			m_EffectsObjectsCamera.enabled = false;
			m_ScreenEffectsRender.m_EffectsObjectsCamera = m_EffectsObjectsCamera;
			m_EffectsObjectsCamera.cullingMask = 8388865;
		}
	}

	private void CreateCamera(out Camera camera, out GameObject cameraGO, string cameraName)
	{
		cameraGO = new GameObject(cameraName);
		SceneUtils.SetLayer(cameraGO, GameLayer.CameraMask);
		UpdateCameraTransform();
		camera = cameraGO.AddComponent<Camera>();
		camera.CopyFrom(m_MainCamera);
		camera.clearFlags = CameraClearFlags.Nothing;
		if (HearthstoneServices.TryGet<UniversalInputManager>(out var service))
		{
			service.AddIgnoredCamera(camera);
		}
	}

	private void UpdateCameraTransform()
	{
		if (!(m_EffectsObjectsCameraGO == null) && !(m_MainCamera == null))
		{
			Transform transform = m_MainCamera.transform;
			m_EffectsObjectsCameraGO.transform.position = transform.position;
			m_EffectsObjectsCameraGO.transform.rotation = transform.rotation;
		}
	}

	private void CreateBackPlane(Camera camera)
	{
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane));
		Vector3 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.farClipPlane));
		Vector3 vector3 = new Vector3((vector2.x - vector.x) * 0.5f, (vector2.y - vector.y) * 0.5f, (vector2.z - vector.z) * 0.5f);
		float farClipPlane = camera.farClipPlane;
		camera.gameObject.AddComponent<MeshFilter>();
		camera.gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[4]
		{
			new Vector3(0f - vector3.x, 0f - vector3.z, farClipPlane),
			new Vector3(vector3.x, 0f - vector3.z, farClipPlane),
			new Vector3(0f - vector3.x, vector3.z, farClipPlane),
			new Vector3(vector3.x, vector3.z, farClipPlane)
		};
		mesh.colors = new Color[4]
		{
			Color.black,
			Color.black,
			Color.black,
			Color.black
		};
		mesh.uv = new Vector2[4]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		mesh.normals = new Vector3[4]
		{
			Vector3.up,
			Vector3.up,
			Vector3.up,
			Vector3.up
		};
		mesh.triangles = new int[6] { 3, 1, 2, 2, 1, 0 };
		camera.gameObject.GetComponent<Renderer>().GetComponent<MeshFilter>().mesh = mesh;
		Material material = new Material(Shader.Find("Hidden/ScreenEffectsBackPlane"));
		camera.gameObject.GetComponent<Renderer>().SetSharedMaterial(material);
	}
}
