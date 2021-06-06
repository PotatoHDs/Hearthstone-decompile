using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000A71 RID: 2673
public class DiamondRenderToTextureService : IService
{
	// Token: 0x06008FAB RID: 36779 RVA: 0x002E89FF File Offset: 0x002E6BFF
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_lastAddedAtlas = 0;
		this.SetupObjects();
		Processor.RegisterLateUpdateDelegate(new Action(this.LateUpdate));
		yield break;
	}

	// Token: 0x06008FAC RID: 36780 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x06008FAD RID: 36781 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06008FAE RID: 36782 RVA: 0x002E8A0E File Offset: 0x002E6C0E
	private void LateUpdate()
	{
		if (!this.NeedsUpdate())
		{
			return;
		}
		this.RemoveUnusedTextures();
		this.RenderAllAtlases();
	}

	// Token: 0x06008FAF RID: 36783 RVA: 0x002E8A28 File Offset: 0x002E6C28
	public bool Register(DiamondRenderToTexture r2t)
	{
		if (!r2t.m_ObjectToRender)
		{
			return false;
		}
		int instanceID = r2t.m_ObjectToRender.GetInstanceID();
		int instanceID2 = r2t.GetInstanceID();
		DiamondRenderToTextureService.TextureReference textureReference;
		if (this.m_textures.TryGetValue(instanceID2, out textureReference))
		{
			if (textureReference.RenderingObjectId == r2t.m_ObjectToRender.GetInstanceID())
			{
				if (textureReference.Remove)
				{
					textureReference.Remove = false;
					this.m_textures[instanceID2] = textureReference;
				}
				return true;
			}
			this.RemoveTexture(textureReference);
		}
		if (!r2t.m_AllowRepetition)
		{
			foreach (KeyValuePair<int, DiamondRenderToTextureService.TextureReference> keyValuePair in this.m_textures)
			{
				if (r2t.IsEqual(keyValuePair.Value.Texture))
				{
					return false;
				}
			}
		}
		DiamondRenderToTextureAtlas atlas = this.AppendToAtlas(r2t);
		DiamondRenderToTextureService.TextureReference value = new DiamondRenderToTextureService.TextureReference
		{
			Texture = r2t,
			Atlas = atlas,
			RenderingObjectId = instanceID
		};
		if (r2t.m_HideRenderObject)
		{
			GameObject gameObject = new GameObject("R2T_" + r2t.name);
			gameObject.transform.parent = this.m_itemsContainerObject.transform;
			r2t.transform.parent = gameObject.transform;
			r2t.m_ObjectToRender.transform.parent = gameObject.transform;
			value.Container = gameObject;
		}
		this.m_textures.Add(instanceID2, value);
		this.m_dirty = true;
		return true;
	}

	// Token: 0x06008FB0 RID: 36784 RVA: 0x002E8BB4 File Offset: 0x002E6DB4
	public void Unregister(DiamondRenderToTexture r2t)
	{
		int instanceID = r2t.GetInstanceID();
		DiamondRenderToTextureService.TextureReference value;
		if (this.m_textures.TryGetValue(instanceID, out value))
		{
			value.Remove = true;
			this.m_textures[instanceID] = value;
			this.m_texturesToRemove.Add(r2t);
			this.m_dirty = true;
		}
	}

	// Token: 0x06008FB1 RID: 36785 RVA: 0x002E8C00 File Offset: 0x002E6E00
	private void SetupObjects()
	{
		this.m_containerObject = new GameObject("AtlasedRenderToTexture");
		this.m_containerObject.transform.position = DiamondRenderToTextureService.OFFSCREEN_POS;
		this.m_itemsContainerObject = new GameObject("Items");
		this.m_itemsContainerObject.transform.parent = this.m_containerObject.transform;
		UnityEngine.Object.DontDestroyOnLoad(this.m_containerObject);
		this.CreateRenderCamera();
	}

	// Token: 0x06008FB2 RID: 36786 RVA: 0x002E8C70 File Offset: 0x002E6E70
	private void CreateRenderCamera()
	{
		GameObject gameObject = new GameObject("RenderCamera", new Type[]
		{
			typeof(RenderToTextureCamera)
		});
		gameObject.transform.parent = this.m_containerObject.transform;
		gameObject.transform.localPosition = DiamondRenderToTextureService.CAMERA_OFFSET;
		gameObject.transform.Rotate(90f, 0f, 0f);
		this.m_renderCamera = gameObject.AddComponent<Camera>();
		this.m_renderCamera.orthographic = true;
		this.m_renderCamera.orthographicSize = 3.45f;
		this.m_renderCamera.nearClipPlane = -0.3f;
		this.m_renderCamera.farClipPlane = 15f;
		this.m_renderCamera.clearFlags = CameraClearFlags.Color;
		this.m_renderCamera.backgroundColor = Color.clear;
		this.m_renderCamera.depthTextureMode = DepthTextureMode.None;
		this.m_renderCamera.renderingPath = RenderingPath.Forward;
		this.m_renderCamera.cullingMask = 0;
		this.m_renderCamera.forceIntoRenderTexture = true;
		this.m_renderCamera.allowHDR = true;
		this.m_renderCamera.enabled = false;
		this.m_renderCameraUp = gameObject.transform.up;
		this.m_renderCameraForward = gameObject.transform.forward;
	}

	// Token: 0x06008FB3 RID: 36787 RVA: 0x002E8DA9 File Offset: 0x002E6FA9
	private bool NeedsUpdate()
	{
		return this.m_dirty;
	}

	// Token: 0x06008FB4 RID: 36788 RVA: 0x002E8DB4 File Offset: 0x002E6FB4
	private void RemoveUnusedTextures()
	{
		if (this.m_texturesToRemove.Count > 0)
		{
			foreach (DiamondRenderToTexture diamondRenderToTexture in this.m_texturesToRemove)
			{
				int instanceID = diamondRenderToTexture.GetInstanceID();
				DiamondRenderToTextureService.TextureReference textureReference;
				if (this.m_textures.TryGetValue(instanceID, out textureReference) && textureReference.Remove)
				{
					this.RemoveTexture(textureReference);
				}
			}
			this.CleanAtlases();
			this.m_texturesToRemove.Clear();
		}
	}

	// Token: 0x06008FB5 RID: 36789 RVA: 0x002E8E48 File Offset: 0x002E7048
	private void RemoveTexture(DiamondRenderToTextureService.TextureReference reference)
	{
		reference.Atlas.Remove(reference.Texture);
		this.m_textures.Remove(reference.Texture.GetInstanceID());
		if (reference.Container)
		{
			DiamondRenderToTexture texture = reference.Texture;
			if (texture != null)
			{
				texture.RestoreOriginalParents();
			}
			UnityEngine.Object.Destroy(reference.Container);
			reference.Container = null;
		}
	}

	// Token: 0x06008FB6 RID: 36790 RVA: 0x002E8EB0 File Offset: 0x002E70B0
	private DiamondRenderToTextureAtlas AppendToAtlas(DiamondRenderToTexture r2t)
	{
		foreach (DiamondRenderToTextureAtlas diamondRenderToTextureAtlas in this.m_atlases)
		{
			if (diamondRenderToTextureAtlas.Insert(r2t))
			{
				return diamondRenderToTextureAtlas;
			}
		}
		this.m_atlases.Add(new DiamondRenderToTextureAtlas(this.m_lastAddedAtlas, 1024, 1024, RenderTextureFormat.ARGB32));
		this.m_lastAddedAtlas++;
		DiamondRenderToTextureAtlas diamondRenderToTextureAtlas2 = this.m_atlases[this.m_lastAddedAtlas - 1];
		diamondRenderToTextureAtlas2.Insert(r2t);
		return diamondRenderToTextureAtlas2;
	}

	// Token: 0x06008FB7 RID: 36791 RVA: 0x002E8F58 File Offset: 0x002E7158
	private void RenderAllAtlases()
	{
		bool flag = false;
		this.m_atlasOriginPosition = DiamondRenderToTextureService.DEFAULT_ATLAS_POSITION;
		foreach (DiamondRenderToTextureAtlas diamondRenderToTextureAtlas in this.m_atlases)
		{
			if (diamondRenderToTextureAtlas.Dirty || diamondRenderToTextureAtlas.IsRealTime)
			{
				this.RenderAtlas(diamondRenderToTextureAtlas, this.m_atlasOriginPosition);
			}
			flag |= diamondRenderToTextureAtlas.IsRealTime;
			this.m_atlasOriginPosition.y = this.m_atlasOriginPosition.y + 0.5f;
		}
		this.m_dirty = flag;
	}

	// Token: 0x06008FB8 RID: 36792 RVA: 0x002E8FF4 File Offset: 0x002E71F4
	private void RenderAtlas(DiamondRenderToTextureAtlas atlas, Vector3 atlasOrigin)
	{
		foreach (DiamondRenderToTextureAtlas.RegisteredTexture registeredTexture in atlas.RegisteredTextures)
		{
			DiamondRenderToTexture diamondRenderToTexture = registeredTexture.DiamondRenderToTexture;
			if (diamondRenderToTexture)
			{
				diamondRenderToTexture.PushTransform();
				if (diamondRenderToTexture.m_HideRenderObject)
				{
					diamondRenderToTexture.m_ObjectToRender.SetActive(true);
				}
				this.PositionObjectForAtlas(registeredTexture, atlasOrigin);
			}
		}
		this.m_renderCamera.farClipPlane = this.m_renderCamera.transform.position.y - atlasOrigin.y + 0.1f;
		this.m_renderCamera.targetTexture = atlas.Texture;
		this.m_renderCamera.backgroundColor = atlas.ClearColor;
		atlas.Render(this.m_renderCamera);
		this.m_renderCamera.Render();
		this.m_renderCamera.RemoveAllCommandBuffers();
		foreach (DiamondRenderToTextureAtlas.RegisteredTexture registeredTexture2 in atlas.RegisteredTextures)
		{
			DiamondRenderToTexture diamondRenderToTexture2 = registeredTexture2.DiamondRenderToTexture;
			if (diamondRenderToTexture2 && !diamondRenderToTexture2.m_HideRenderObject)
			{
				diamondRenderToTexture2.HasAtlasPosition = false;
				diamondRenderToTexture2.PopTransform();
			}
		}
		atlas.Dirty = false;
	}

	// Token: 0x06008FB9 RID: 36793 RVA: 0x002E9148 File Offset: 0x002E7348
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
			float scaleApplied = this.ScaleObjectToAtlasPosition(texture);
			Quaternion rotationApplied = this.RotateTowardsCamera(texture);
			this.MoveToAtlasPosition(texture, atlasPosition, scaleApplied, rotationApplied);
		}
		diamondRenderToTexture.CaptureAtlasPosition();
		diamondRenderToTexture.RestoreParents();
		diamondRenderToTexture.transform.hasChanged = false;
	}

	// Token: 0x06008FBA RID: 36794 RVA: 0x002E91E4 File Offset: 0x002E73E4
	private float ScaleObjectToAtlasPosition(DiamondRenderToTextureAtlas.RegisteredTexture texture)
	{
		DiamondRenderToTexture diamondRenderToTexture = texture.DiamondRenderToTexture;
		float b = 0.0067382813f * (float)texture.AtlasPosition.height / diamondRenderToTexture.WorldBounds.x;
		float num = Mathf.Max(0.0067382813f * (float)texture.AtlasPosition.width / diamondRenderToTexture.WorldBounds.y, b);
		Vector3 rhs = Vector3.one * num;
		Transform transform = diamondRenderToTexture.m_ObjectToRender.transform;
		Vector3 vector = transform.localScale;
		if (vector == rhs)
		{
			return 1f;
		}
		vector *= num;
		transform.localScale = vector;
		diamondRenderToTexture.transform.localScale *= num;
		return num;
	}

	// Token: 0x06008FBB RID: 36795 RVA: 0x002E929C File Offset: 0x002E749C
	private void MoveToAtlasPosition(DiamondRenderToTextureAtlas.RegisteredTexture texture, Vector3 atlasOrigin, float scaleApplied, Quaternion rotationApplied)
	{
		DiamondRenderToTexture diamondRenderToTexture = texture.DiamondRenderToTexture;
		Transform transform = diamondRenderToTexture.transform;
		Vector3 b = 0.0067382813f * new Vector3((float)texture.AtlasPosition.x, 0f, (float)texture.AtlasPosition.y);
		diamondRenderToTexture.m_ObjectToRender.transform.position = atlasOrigin + b - diamondRenderToTexture.WorldPivotOffset;
		transform.position = atlasOrigin + b - diamondRenderToTexture.WorldPivotOffset;
	}

	// Token: 0x06008FBC RID: 36796 RVA: 0x002E9320 File Offset: 0x002E7520
	private Quaternion RotateTowardsCamera(DiamondRenderToTextureAtlas.RegisteredTexture texture)
	{
		DiamondRenderToTexture diamondRenderToTexture = texture.DiamondRenderToTexture;
		Transform transform = diamondRenderToTexture.m_ObjectToRender.transform;
		Transform transform2 = diamondRenderToTexture.transform;
		Vector3 forward = transform2.forward;
		Vector3 up = transform2.up;
		Quaternion quaternion = Quaternion.FromToRotation(transform.forward, up);
		Quaternion rhs = Quaternion.FromToRotation(quaternion * transform.up, forward) * quaternion;
		Quaternion quaternion2 = Quaternion.FromToRotation(forward, -this.m_renderCameraForward);
		Quaternion quaternion3 = Quaternion.FromToRotation(quaternion2 * transform2.up, this.m_renderCameraUp) * quaternion2;
		transform.rotation = quaternion3 * rhs * transform.rotation;
		transform2.rotation = quaternion3 * transform2.rotation;
		return quaternion3;
	}

	// Token: 0x06008FBD RID: 36797 RVA: 0x002E93E0 File Offset: 0x002E75E0
	private void CleanAtlases()
	{
		for (int i = this.m_atlases.Count - 1; i >= 0; i--)
		{
			DiamondRenderToTextureAtlas diamondRenderToTextureAtlas = this.m_atlases[i];
			if (diamondRenderToTextureAtlas.IsEmpty())
			{
				diamondRenderToTextureAtlas.Destroy();
				this.m_atlases.RemoveAt(i);
				this.m_lastAddedAtlas--;
			}
		}
	}

	// Token: 0x0400785F RID: 30815
	private const float MIN_OFFSET_DISTANCE = -4000f;

	// Token: 0x04007860 RID: 30816
	private const float CAMERA_SIZE = 3.45f;

	// Token: 0x04007861 RID: 30817
	private const float CAMERA_NEAR_CLIP = -0.3f;

	// Token: 0x04007862 RID: 30818
	private const float CAMERA_FAR_CLIP = 15f;

	// Token: 0x04007863 RID: 30819
	private const int LAYER_MASK = 23;

	// Token: 0x04007864 RID: 30820
	private const int RENDER_TEXTURE_SIZE = 1024;

	// Token: 0x04007865 RID: 30821
	private const float CAMERA_PIXEL_SIZE = 0.0067382813f;

	// Token: 0x04007866 RID: 30822
	private static readonly Vector3 CAMERA_OFFSET = new Vector3(0f, 10f, 0f);

	// Token: 0x04007867 RID: 30823
	private static readonly Vector3 OFFSCREEN_POS = new Vector3(-4000f, -4000f, -4000f);

	// Token: 0x04007868 RID: 30824
	private static readonly Vector3 DEFAULT_ATLAS_POSITION = DiamondRenderToTextureService.OFFSCREEN_POS - new Vector3(3.45f, 0f, 3.45f);

	// Token: 0x04007869 RID: 30825
	private Dictionary<int, DiamondRenderToTextureService.TextureReference> m_textures = new Dictionary<int, DiamondRenderToTextureService.TextureReference>();

	// Token: 0x0400786A RID: 30826
	private List<DiamondRenderToTextureAtlas> m_atlases = new List<DiamondRenderToTextureAtlas>();

	// Token: 0x0400786B RID: 30827
	private GameObject m_containerObject;

	// Token: 0x0400786C RID: 30828
	private GameObject m_itemsContainerObject;

	// Token: 0x0400786D RID: 30829
	private Camera m_renderCamera;

	// Token: 0x0400786E RID: 30830
	private Vector3 m_atlasOriginPosition;

	// Token: 0x0400786F RID: 30831
	private int m_lastAddedAtlas;

	// Token: 0x04007870 RID: 30832
	private Vector3 m_renderCameraUp;

	// Token: 0x04007871 RID: 30833
	private Vector3 m_renderCameraForward;

	// Token: 0x04007872 RID: 30834
	private CommandBuffer m_atlasFilterCommandBuffer;

	// Token: 0x04007873 RID: 30835
	private bool m_dirty;

	// Token: 0x04007874 RID: 30836
	private List<DiamondRenderToTexture> m_texturesToRemove = new List<DiamondRenderToTexture>();

	// Token: 0x020026CC RID: 9932
	private struct TextureReference
	{
		// Token: 0x0400F21A RID: 61978
		public DiamondRenderToTexture Texture;

		// Token: 0x0400F21B RID: 61979
		public DiamondRenderToTextureAtlas Atlas;

		// Token: 0x0400F21C RID: 61980
		public GameObject Container;

		// Token: 0x0400F21D RID: 61981
		public int RenderingObjectId;

		// Token: 0x0400F21E RID: 61982
		public bool Remove;
	}
}
