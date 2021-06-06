using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x02000A83 RID: 2691
public class ScreenEffectsMgr : IService, IHasUpdate
{
	// Token: 0x1700082C RID: 2092
	// (get) Token: 0x06009031 RID: 36913 RVA: 0x002ECF70 File Offset: 0x002EB170
	public bool IsActive
	{
		get
		{
			return this.m_enabled;
		}
	}

	// Token: 0x06009032 RID: 36914 RVA: 0x002ECF78 File Offset: 0x002EB178
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		if (ScreenEffectsMgr.m_ActiveScreenEffects == null)
		{
			ScreenEffectsMgr.m_ActiveScreenEffects = new List<ScreenEffect>();
		}
		yield return new WaitForMainCamera();
		this.OnEnable();
		yield break;
	}

	// Token: 0x06009033 RID: 36915 RVA: 0x001B35BC File Offset: 0x001B17BC
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(UniversalInputManager)
		};
	}

	// Token: 0x06009034 RID: 36916 RVA: 0x002ECF87 File Offset: 0x002EB187
	public void Shutdown()
	{
		this.OnDisable();
		if (ScreenEffectsMgr.m_ActiveScreenEffects != null)
		{
			ScreenEffectsMgr.m_ActiveScreenEffects.Clear();
			ScreenEffectsMgr.m_ActiveScreenEffects = null;
		}
	}

	// Token: 0x06009035 RID: 36917 RVA: 0x002ECFA8 File Offset: 0x002EB1A8
	public void Update()
	{
		if (this.m_MainCamera == null)
		{
			if (Camera.main == null)
			{
				return;
			}
			this.Init();
		}
		if (this.m_ScreenEffectsRender == null)
		{
			return;
		}
		if (ScreenEffectsMgr.m_ActiveScreenEffects != null && ScreenEffectsMgr.m_ActiveScreenEffects.Count > 0)
		{
			if (!this.m_ScreenEffectsRender.enabled)
			{
				this.m_ScreenEffectsRender.enabled = true;
			}
		}
		else if (this.m_ScreenEffectsRender.enabled)
		{
			this.m_ScreenEffectsRender.enabled = false;
		}
		this.UpdateCameraTransform();
	}

	// Token: 0x06009036 RID: 36918 RVA: 0x002ED034 File Offset: 0x002EB234
	public void SetActive(bool enabled)
	{
		if (this.m_enabled == enabled)
		{
			return;
		}
		this.m_enabled = enabled;
		if (this.m_enabled)
		{
			this.OnEnable();
			return;
		}
		this.OnDisable();
	}

	// Token: 0x06009037 RID: 36919 RVA: 0x002ED05C File Offset: 0x002EB25C
	private void OnDisable()
	{
		if (this.m_EffectsObjectsCameraGO != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_EffectsObjectsCameraGO);
		}
		if (this.m_ScreenEffectsRender != null)
		{
			this.m_ScreenEffectsRender.enabled = false;
		}
	}

	// Token: 0x06009038 RID: 36920 RVA: 0x002ED091 File Offset: 0x002EB291
	private void OnEnable()
	{
		if (Camera.main == null)
		{
			return;
		}
		this.Init();
	}

	// Token: 0x06009039 RID: 36921 RVA: 0x002ED0A7 File Offset: 0x002EB2A7
	public static ScreenEffectsMgr Get()
	{
		return HearthstoneServices.Get<ScreenEffectsMgr>();
	}

	// Token: 0x0600903A RID: 36922 RVA: 0x002ED0AE File Offset: 0x002EB2AE
	public static void RegisterScreenEffect(ScreenEffect effect)
	{
		if (ScreenEffectsMgr.m_ActiveScreenEffects == null)
		{
			ScreenEffectsMgr.m_ActiveScreenEffects = new List<ScreenEffect>();
		}
		if (!ScreenEffectsMgr.m_ActiveScreenEffects.Contains(effect))
		{
			ScreenEffectsMgr.m_ActiveScreenEffects.Add(effect);
		}
	}

	// Token: 0x0600903B RID: 36923 RVA: 0x002ED0D9 File Offset: 0x002EB2D9
	public static void UnRegisterScreenEffect(ScreenEffect effect)
	{
		if (ScreenEffectsMgr.m_ActiveScreenEffects == null)
		{
			return;
		}
		ScreenEffectsMgr.m_ActiveScreenEffects.Remove(effect);
	}

	// Token: 0x0600903C RID: 36924 RVA: 0x002ED0EF File Offset: 0x002EB2EF
	public int GetActiveScreenEffectsCount()
	{
		if (ScreenEffectsMgr.m_ActiveScreenEffects == null)
		{
			return 0;
		}
		return ScreenEffectsMgr.m_ActiveScreenEffects.Count;
	}

	// Token: 0x0600903D RID: 36925 RVA: 0x002ED104 File Offset: 0x002EB304
	private void Init()
	{
		this.m_MainCamera = Camera.main;
		if (this.m_MainCamera == null)
		{
			return;
		}
		this.m_ScreenEffectsRender = this.m_MainCamera.GetComponent<ScreenEffectsRender>();
		if (this.m_ScreenEffectsRender == null)
		{
			this.m_ScreenEffectsRender = this.m_MainCamera.gameObject.AddComponent<ScreenEffectsRender>();
			this.m_MainCamera.allowHDR = false;
		}
		else
		{
			this.m_ScreenEffectsRender.enabled = true;
		}
		this.CreateCamera(out this.m_EffectsObjectsCamera, out this.m_EffectsObjectsCameraGO, "ScreenEffectsObjectRenderCamera");
		this.m_EffectsObjectsCamera.depth = this.m_MainCamera.depth - 1f;
		this.m_EffectsObjectsCamera.clearFlags = CameraClearFlags.Color;
		this.m_EffectsObjectsCamera.backgroundColor = Color.clear;
		SceneUtils.SetLayer(this.m_EffectsObjectsCameraGO, 23, null);
		this.m_EffectsObjectsCamera.enabled = false;
		this.m_ScreenEffectsRender.m_EffectsObjectsCamera = this.m_EffectsObjectsCamera;
		this.m_EffectsObjectsCamera.cullingMask = 8388865;
	}

	// Token: 0x0600903E RID: 36926 RVA: 0x002ED20C File Offset: 0x002EB40C
	private void CreateCamera(out Camera camera, out GameObject cameraGO, string cameraName)
	{
		cameraGO = new GameObject(cameraName);
		SceneUtils.SetLayer(cameraGO, GameLayer.CameraMask);
		this.UpdateCameraTransform();
		camera = cameraGO.AddComponent<Camera>();
		camera.CopyFrom(this.m_MainCamera);
		camera.clearFlags = CameraClearFlags.Nothing;
		UniversalInputManager universalInputManager;
		if (HearthstoneServices.TryGet<UniversalInputManager>(out universalInputManager))
		{
			universalInputManager.AddIgnoredCamera(camera);
		}
	}

	// Token: 0x0600903F RID: 36927 RVA: 0x002ED260 File Offset: 0x002EB460
	private void UpdateCameraTransform()
	{
		if (this.m_EffectsObjectsCameraGO == null || this.m_MainCamera == null)
		{
			return;
		}
		Transform transform = this.m_MainCamera.transform;
		this.m_EffectsObjectsCameraGO.transform.position = transform.position;
		this.m_EffectsObjectsCameraGO.transform.rotation = transform.rotation;
	}

	// Token: 0x06009040 RID: 36928 RVA: 0x002ED2C4 File Offset: 0x002EB4C4
	private void CreateBackPlane(Camera camera)
	{
		Vector3 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane));
		Vector3 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.farClipPlane));
		Vector3 vector3 = new Vector3((vector2.x - vector.x) * 0.5f, (vector2.y - vector.y) * 0.5f, (vector2.z - vector.z) * 0.5f);
		float farClipPlane = camera.farClipPlane;
		camera.gameObject.AddComponent<MeshFilter>();
		camera.gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[]
		{
			new Vector3(-vector3.x, -vector3.z, farClipPlane),
			new Vector3(vector3.x, -vector3.z, farClipPlane),
			new Vector3(-vector3.x, vector3.z, farClipPlane),
			new Vector3(vector3.x, vector3.z, farClipPlane)
		};
		mesh.colors = new Color[]
		{
			Color.black,
			Color.black,
			Color.black,
			Color.black
		};
		mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		mesh.normals = new Vector3[]
		{
			Vector3.up,
			Vector3.up,
			Vector3.up,
			Vector3.up
		};
		mesh.triangles = new int[]
		{
			3,
			1,
			2,
			2,
			1,
			0
		};
		camera.gameObject.GetComponent<Renderer>().GetComponent<MeshFilter>().mesh = mesh;
		Material material = new Material(Shader.Find("Hidden/ScreenEffectsBackPlane"));
		camera.gameObject.GetComponent<Renderer>().SetSharedMaterial(material);
	}

	// Token: 0x0400791B RID: 31003
	private Camera m_EffectsObjectsCamera;

	// Token: 0x0400791C RID: 31004
	private GameObject m_EffectsObjectsCameraGO;

	// Token: 0x0400791D RID: 31005
	private Camera m_MainCamera;

	// Token: 0x0400791E RID: 31006
	private ScreenEffectsRender m_ScreenEffectsRender;

	// Token: 0x0400791F RID: 31007
	private bool m_enabled;

	// Token: 0x04007920 RID: 31008
	private static List<ScreenEffect> m_ActiveScreenEffects;

	// Token: 0x020026D5 RID: 9941
	public enum EFFECT_TYPE
	{
		// Token: 0x0400F247 RID: 62023
		Glow,
		// Token: 0x0400F248 RID: 62024
		Distortion
	}
}
