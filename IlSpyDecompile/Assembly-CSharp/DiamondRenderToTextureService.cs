using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using UnityEngine;
using UnityEngine.Rendering;

public class DiamondRenderToTextureService : IService
{
	private struct TextureReference
	{
		public DiamondRenderToTexture Texture;

		public DiamondRenderToTextureAtlas Atlas;

		public GameObject Container;

		public int RenderingObjectId;

		public bool Remove;
	}

	private const float MIN_OFFSET_DISTANCE = -4000f;

	private const float CAMERA_SIZE = 3.45f;

	private const float CAMERA_NEAR_CLIP = -0.3f;

	private const float CAMERA_FAR_CLIP = 15f;

	private const int LAYER_MASK = 23;

	private const int RENDER_TEXTURE_SIZE = 1024;

	private const float CAMERA_PIXEL_SIZE = 0.00673828134f;

	private static readonly Vector3 CAMERA_OFFSET = new Vector3(0f, 10f, 0f);

	private static readonly Vector3 OFFSCREEN_POS = new Vector3(-4000f, -4000f, -4000f);

	private static readonly Vector3 DEFAULT_ATLAS_POSITION = OFFSCREEN_POS - new Vector3(3.45f, 0f, 3.45f);

	private Dictionary<int, TextureReference> m_textures = new Dictionary<int, TextureReference>();

	private List<DiamondRenderToTextureAtlas> m_atlases = new List<DiamondRenderToTextureAtlas>();

	private GameObject m_containerObject;

	private GameObject m_itemsContainerObject;

	private Camera m_renderCamera;

	private Vector3 m_atlasOriginPosition;

	private int m_lastAddedAtlas;

	private Vector3 m_renderCameraUp;

	private Vector3 m_renderCameraForward;

	private CommandBuffer m_atlasFilterCommandBuffer;

	private bool m_dirty;

	private List<DiamondRenderToTexture> m_texturesToRemove = new List<DiamondRenderToTexture>();

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_lastAddedAtlas = 0;
		SetupObjects();
		Processor.RegisterLateUpdateDelegate(LateUpdate);
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
	}

	private void LateUpdate()
	{
		if (NeedsUpdate())
		{
			RemoveUnusedTextures();
			RenderAllAtlases();
		}
	}

	public bool Register(DiamondRenderToTexture r2t)
	{
		if (!r2t.m_ObjectToRender)
		{
			return false;
		}
		int instanceID = r2t.m_ObjectToRender.GetInstanceID();
		int instanceID2 = r2t.GetInstanceID();
		if (m_textures.TryGetValue(instanceID2, out var value))
		{
			if (value.RenderingObjectId == r2t.m_ObjectToRender.GetInstanceID())
			{
				if (value.Remove)
				{
					value.Remove = false;
					m_textures[instanceID2] = value;
				}
				return true;
			}
			RemoveTexture(value);
		}
		if (!r2t.m_AllowRepetition)
		{
			foreach (KeyValuePair<int, TextureReference> texture in m_textures)
			{
				if (r2t.IsEqual(texture.Value.Texture))
				{
					return false;
				}
			}
		}
		DiamondRenderToTextureAtlas atlas = AppendToAtlas(r2t);
		TextureReference textureReference = default(TextureReference);
		textureReference.Texture = r2t;
		textureReference.Atlas = atlas;
		textureReference.RenderingObjectId = instanceID;
		TextureReference value2 = textureReference;
		if (r2t.m_HideRenderObject)
		{
			GameObject gameObject = new GameObject("R2T_" + r2t.name);
			gameObject.transform.parent = m_itemsContainerObject.transform;
			r2t.transform.parent = gameObject.transform;
			r2t.m_ObjectToRender.transform.parent = gameObject.transform;
			value2.Container = gameObject;
		}
		m_textures.Add(instanceID2, value2);
		m_dirty = true;
		return true;
	}

	public void Unregister(DiamondRenderToTexture r2t)
	{
		int instanceID = r2t.GetInstanceID();
		if (m_textures.TryGetValue(instanceID, out var value))
		{
			value.Remove = true;
			m_textures[instanceID] = value;
			m_texturesToRemove.Add(r2t);
			m_dirty = true;
		}
	}

	private void SetupObjects()
	{
		m_containerObject = new GameObject("AtlasedRenderToTexture");
		m_containerObject.transform.position = OFFSCREEN_POS;
		m_itemsContainerObject = new GameObject("Items");
		m_itemsContainerObject.transform.parent = m_containerObject.transform;
		UnityEngine.Object.DontDestroyOnLoad(m_containerObject);
		CreateRenderCamera();
	}

	private void CreateRenderCamera()
	{
		GameObject gameObject = new GameObject("RenderCamera", typeof(RenderToTextureCamera));
		gameObject.transform.parent = m_containerObject.transform;
		gameObject.transform.localPosition = CAMERA_OFFSET;
		gameObject.transform.Rotate(90f, 0f, 0f);
		m_renderCamera = gameObject.AddComponent<Camera>();
		m_renderCamera.orthographic = true;
		m_renderCamera.orthographicSize = 3.45f;
		m_renderCamera.nearClipPlane = -0.3f;
		m_renderCamera.farClipPlane = 15f;
		m_renderCamera.clearFlags = CameraClearFlags.Color;
		m_renderCamera.backgroundColor = Color.clear;
		m_renderCamera.depthTextureMode = DepthTextureMode.None;
		m_renderCamera.renderingPath = RenderingPath.Forward;
		m_renderCamera.cullingMask = 0;
		m_renderCamera.forceIntoRenderTexture = true;
		m_renderCamera.allowHDR = true;
		m_renderCamera.enabled = false;
		m_renderCameraUp = gameObject.transform.up;
		m_renderCameraForward = gameObject.transform.forward;
	}

	private bool NeedsUpdate()
	{
		return m_dirty;
	}

	private void RemoveUnusedTextures()
	{
		if (m_texturesToRemove.Count <= 0)
		{
			return;
		}
		foreach (DiamondRenderToTexture item in m_texturesToRemove)
		{
			int instanceID = item.GetInstanceID();
			if (m_textures.TryGetValue(instanceID, out var value) && value.Remove)
			{
				RemoveTexture(value);
			}
		}
		CleanAtlases();
		m_texturesToRemove.Clear();
	}

	private void RemoveTexture(TextureReference reference)
	{
		reference.Atlas.Remove(reference.Texture);
		m_textures.Remove(reference.Texture.GetInstanceID());
		if ((bool)reference.Container)
		{
			reference.Texture?.RestoreOriginalParents();
			UnityEngine.Object.Destroy(reference.Container);
			reference.Container = null;
		}
	}

	private DiamondRenderToTextureAtlas AppendToAtlas(DiamondRenderToTexture r2t)
	{
		foreach (DiamondRenderToTextureAtlas atlase in m_atlases)
		{
			if (atlase.Insert(r2t))
			{
				return atlase;
			}
		}
		m_atlases.Add(new DiamondRenderToTextureAtlas(m_lastAddedAtlas, 1024, 1024));
		m_lastAddedAtlas++;
		DiamondRenderToTextureAtlas diamondRenderToTextureAtlas = m_atlases[m_lastAddedAtlas - 1];
		diamondRenderToTextureAtlas.Insert(r2t);
		return diamondRenderToTextureAtlas;
	}

	private void RenderAllAtlases()
	{
		bool flag = false;
		m_atlasOriginPosition = DEFAULT_ATLAS_POSITION;
		foreach (DiamondRenderToTextureAtlas atlase in m_atlases)
		{
			if (atlase.Dirty || atlase.IsRealTime)
			{
				RenderAtlas(atlase, m_atlasOriginPosition);
			}
			flag |= atlase.IsRealTime;
			m_atlasOriginPosition.y += 0.5f;
		}
		m_dirty = flag;
	}

	private void RenderAtlas(DiamondRenderToTextureAtlas atlas, Vector3 atlasOrigin)
	{
		foreach (DiamondRenderToTextureAtlas.RegisteredTexture registeredTexture in atlas.RegisteredTextures)
		{
			DiamondRenderToTexture diamondRenderToTexture = registeredTexture.DiamondRenderToTexture;
			if ((bool)diamondRenderToTexture)
			{
				diamondRenderToTexture.PushTransform();
				if (diamondRenderToTexture.m_HideRenderObject)
				{
					diamondRenderToTexture.m_ObjectToRender.SetActive(value: true);
				}
				PositionObjectForAtlas(registeredTexture, atlasOrigin);
			}
		}
		m_renderCamera.farClipPlane = m_renderCamera.transform.position.y - atlasOrigin.y + 0.1f;
		m_renderCamera.targetTexture = atlas.Texture;
		m_renderCamera.backgroundColor = atlas.ClearColor;
		atlas.Render(m_renderCamera);
		m_renderCamera.Render();
		m_renderCamera.RemoveAllCommandBuffers();
		foreach (DiamondRenderToTextureAtlas.RegisteredTexture registeredTexture2 in atlas.RegisteredTextures)
		{
			DiamondRenderToTexture diamondRenderToTexture2 = registeredTexture2.DiamondRenderToTexture;
			if ((bool)diamondRenderToTexture2 && !diamondRenderToTexture2.m_HideRenderObject)
			{
				diamondRenderToTexture2.HasAtlasPosition = false;
				diamondRenderToTexture2.PopTransform();
			}
		}
		atlas.Dirty = false;
	}

	private void PositionObjectForAtlas(DiamondRenderToTextureAtlas.RegisteredTexture texture, Vector3 atlasPosition)
	{
		DiamondRenderToTexture diamondRenderToTexture = texture.DiamondRenderToTexture;
		if (diamondRenderToTexture.m_HideRenderObject && diamondRenderToTexture.HasAtlasPosition && diamondRenderToTexture.MaintainsAtlasPosition())
		{
			diamondRenderToTexture.transform.hasChanged = false;
			return;
		}
		if (!diamondRenderToTexture.m_ObjectToRender.activeInHierarchy)
		{
			diamondRenderToTexture.transform.hasChanged = false;
			return;
		}
		diamondRenderToTexture.ResetTransform(atlasPosition);
		if (diamondRenderToTexture.HasAtlasPosition)
		{
			diamondRenderToTexture.RestoreAtlasPosition();
		}
		else
		{
			float scaleApplied = ScaleObjectToAtlasPosition(texture);
			Quaternion rotationApplied = RotateTowardsCamera(texture);
			MoveToAtlasPosition(texture, atlasPosition, scaleApplied, rotationApplied);
		}
		diamondRenderToTexture.CaptureAtlasPosition();
		diamondRenderToTexture.RestoreParents();
		diamondRenderToTexture.transform.hasChanged = false;
	}

	private float ScaleObjectToAtlasPosition(DiamondRenderToTextureAtlas.RegisteredTexture texture)
	{
		DiamondRenderToTexture diamondRenderToTexture = texture.DiamondRenderToTexture;
		float b = 0.00673828134f * (float)texture.AtlasPosition.height / diamondRenderToTexture.WorldBounds.x;
		float num = Mathf.Max(0.00673828134f * (float)texture.AtlasPosition.width / diamondRenderToTexture.WorldBounds.y, b);
		Vector3 vector = Vector3.one * num;
		Transform transform = diamondRenderToTexture.m_ObjectToRender.transform;
		Vector3 localScale = transform.localScale;
		if (localScale == vector)
		{
			return 1f;
		}
		localScale *= num;
		transform.localScale = localScale;
		diamondRenderToTexture.transform.localScale *= num;
		return num;
	}

	private void MoveToAtlasPosition(DiamondRenderToTextureAtlas.RegisteredTexture texture, Vector3 atlasOrigin, float scaleApplied, Quaternion rotationApplied)
	{
		DiamondRenderToTexture diamondRenderToTexture = texture.DiamondRenderToTexture;
		Transform transform = diamondRenderToTexture.transform;
		Vector3 vector = 0.00673828134f * new Vector3(texture.AtlasPosition.x, 0f, texture.AtlasPosition.y);
		diamondRenderToTexture.m_ObjectToRender.transform.position = atlasOrigin + vector - diamondRenderToTexture.WorldPivotOffset;
		transform.position = atlasOrigin + vector - diamondRenderToTexture.WorldPivotOffset;
	}

	private Quaternion RotateTowardsCamera(DiamondRenderToTextureAtlas.RegisteredTexture texture)
	{
		DiamondRenderToTexture diamondRenderToTexture = texture.DiamondRenderToTexture;
		Transform transform = diamondRenderToTexture.m_ObjectToRender.transform;
		Transform transform2 = diamondRenderToTexture.transform;
		Vector3 forward = transform2.forward;
		Vector3 up = transform2.up;
		Quaternion quaternion = Quaternion.FromToRotation(transform.forward, up);
		Quaternion quaternion2 = Quaternion.FromToRotation(quaternion * transform.up, forward) * quaternion;
		Quaternion quaternion3 = Quaternion.FromToRotation(forward, -m_renderCameraForward);
		Quaternion quaternion4 = Quaternion.FromToRotation(quaternion3 * transform2.up, m_renderCameraUp) * quaternion3;
		transform.rotation = quaternion4 * quaternion2 * transform.rotation;
		transform2.rotation = quaternion4 * transform2.rotation;
		return quaternion4;
	}

	private void CleanAtlases()
	{
		for (int num = m_atlases.Count - 1; num >= 0; num--)
		{
			DiamondRenderToTextureAtlas diamondRenderToTextureAtlas = m_atlases[num];
			if (diamondRenderToTextureAtlas.IsEmpty())
			{
				diamondRenderToTextureAtlas.Destroy();
				m_atlases.RemoveAt(num);
				m_lastAddedAtlas--;
			}
		}
	}
}
