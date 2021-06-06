using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000ABF RID: 2751
public class UberTextRenderToTexture
{
	// Token: 0x060092E8 RID: 37608 RVA: 0x002FA0C8 File Offset: 0x002F82C8
	public void InitOnRenderObject(GameObject renderOnObject, Vector2Int renderTextureSize)
	{
		if (!this.m_renderOnObject || this.m_renderOnObject != renderOnObject)
		{
			this.m_renderOnObject = renderOnObject;
			this.m_renderOnObjectRenderer = renderOnObject.GetComponent<Renderer>();
		}
		this.CreateRenderTexture(renderTextureSize);
		this.CreateCamera();
		if (!this.m_init)
		{
			this.m_init = true;
		}
	}

	// Token: 0x060092E9 RID: 37609 RVA: 0x002FA120 File Offset: 0x002F8320
	public void InitOnPlane(GameObject parent, Vector2 planeSize, Color gradientUpperColor, Color gradientLowerColor, Vector2Int renderTextureSize)
	{
		this.CreateRenderTexture(renderTextureSize);
		this.CreateCamera();
		if (!this.m_init || !this.m_planeGameObject)
		{
			this.m_currentPlaneSize = planeSize;
			this.m_init = true;
			this.CreatePlane(parent, gradientUpperColor, gradientLowerColor);
			return;
		}
		if (this.m_currentPlaneSize != planeSize)
		{
			this.m_currentPlaneSize = planeSize;
			this.m_planeMeshFilter.sharedMesh = this.AcquireAppropriateMesh(this.m_currentPlaneSize, gradientUpperColor, gradientLowerColor);
		}
	}

	// Token: 0x060092EA RID: 37610 RVA: 0x002FA198 File Offset: 0x002F8398
	public void InitShadow(GameObject parent, Vector3 localPosition)
	{
		if (this.m_init && !this.m_renderOnObject)
		{
			if (!this.m_shadowPlaneGameObject)
			{
				this.CreateShadowPlane(parent, localPosition);
			}
			else
			{
				this.m_shadowPlaneGameObject.transform.localPosition = localPosition;
				this.m_shadowMeshFilter.sharedMesh = this.AcquireAppropriateMesh(this.m_currentPlaneSize, Color.clear, Color.clear);
				this.m_shadowMeshRenderer.enabled = true;
			}
			this.m_shadowMaterialQuery.WithTexture(this.m_renderTexture);
		}
	}

	// Token: 0x060092EB RID: 37611 RVA: 0x002FA224 File Offset: 0x002F8424
	public void Destroy()
	{
		this.ReleaseCurrentMesh();
		RenderTextureTracker.Get().DestroyRenderTexture(this.m_renderTexture);
		UberTextMaterialManager uberTextMaterialManager = UberTextMaterialManager.Get();
		uberTextMaterialManager.ReleaseMaterial(this.m_planeMaterial);
		uberTextMaterialManager.ReleaseMaterial(this.m_renderOnObjectMaterial);
		uberTextMaterialManager.ReleaseMaterial(this.m_shadowMaterial);
		uberTextMaterialManager.ReleaseMaterial(this.m_antialiasMaterial);
		if (this.m_planeGameObject)
		{
			UnityEngine.Object.DestroyImmediate(this.m_planeGameObject);
		}
		if (this.m_shadowPlaneGameObject)
		{
			UnityEngine.Object.DestroyImmediate(this.m_shadowPlaneGameObject);
		}
	}

	// Token: 0x060092EC RID: 37612 RVA: 0x002FA2AC File Offset: 0x002F84AC
	public void DoRenderToTexture()
	{
		UberTextRenderToTexture.s_renderCamera.targetTexture = this.m_renderTexture;
		if (this.m_renderOnObject && this.m_renderOnObjectRenderer)
		{
			this.m_renderOnObjectMaterial = this.ApplyMaterialIfNeeded(this.m_renderOnObjectRenderer, this.m_renderOnObjectMaterial, this.m_planeMaterialQuery);
		}
		else if (this.m_planeGameObject)
		{
			this.m_planeMaterial = this.ApplyMaterialIfNeeded(this.m_planeMeshRenderer, this.m_planeMaterial, this.m_planeMaterialQuery);
			if (this.m_shadowMeshRenderer && this.m_shadowMeshRenderer.enabled)
			{
				this.m_shadowMaterial = this.ApplyMaterialIfNeeded(this.m_shadowMeshRenderer, this.m_shadowMaterial, this.m_shadowMaterialQuery);
			}
		}
		UberTextRenderToTexture.s_renderCamera.Render();
	}

	// Token: 0x060092ED RID: 37613 RVA: 0x002FA370 File Offset: 0x002F8570
	public void ApplyAntialiasing()
	{
		if (this.m_renderOnObject)
		{
			this.m_antialiasMaterial = this.ApplyMaterialIfNeeded(this.m_renderOnObjectRenderer, this.m_antialiasMaterial, this.m_antialiasingMaterialQuery);
			return;
		}
		if (this.m_planeGameObject)
		{
			this.m_antialiasMaterial = this.ApplyMaterialIfNeeded(this.m_planeMeshRenderer, this.m_antialiasMaterial, this.m_antialiasingMaterialQuery);
		}
	}

	// Token: 0x060092EE RID: 37614 RVA: 0x002FA3D4 File Offset: 0x002F85D4
	public void SetActive(bool active)
	{
		if (this.m_planeGameObject)
		{
			this.m_planeGameObject.SetActive(active);
		}
		if (this.m_shadowPlaneGameObject)
		{
			this.m_shadowPlaneGameObject.SetActive(active);
		}
	}

	// Token: 0x060092EF RID: 37615 RVA: 0x002FA408 File Offset: 0x002F8608
	public void SetAllVisible(bool visible)
	{
		if (this.m_planeMeshRenderer)
		{
			this.m_planeMeshRenderer.enabled = visible;
		}
		if (this.m_renderOnObjectRenderer)
		{
			this.m_renderOnObjectRenderer.enabled = visible;
		}
		if (this.m_shadowMeshRenderer)
		{
			this.m_shadowMeshRenderer.enabled = visible;
		}
	}

	// Token: 0x060092F0 RID: 37616 RVA: 0x002FA460 File Offset: 0x002F8660
	public void SetGradientColors(Color gradientUpperColor, Color gradientLowerColor)
	{
		if (!this.m_planeMeshFilter)
		{
			return;
		}
		if (this.GetCurrentUsedMesh().IsValid)
		{
			this.m_planeMeshFilter.sharedMesh = this.AcquireAppropriateMesh(this.m_currentPlaneSize, gradientUpperColor, gradientLowerColor);
		}
	}

	// Token: 0x060092F1 RID: 37617 RVA: 0x002FA496 File Offset: 0x002F8696
	public void SetPlaneLocalPosition(Vector3 position)
	{
		if (this.m_planeGameObject)
		{
			this.m_planeGameObject.transform.localPosition = position;
		}
	}

	// Token: 0x060092F2 RID: 37618 RVA: 0x002FA4B6 File Offset: 0x002F86B6
	public void SetPlaneRotation(Quaternion rotation)
	{
		if (this.m_planeGameObject)
		{
			this.m_planeGameObject.transform.rotation = rotation;
		}
	}

	// Token: 0x060092F3 RID: 37619 RVA: 0x002FA4D6 File Offset: 0x002F86D6
	public void DoPlaneRotate(Vector3 rotation)
	{
		if (this.m_planeGameObject)
		{
			this.m_planeGameObject.transform.Rotate(rotation);
		}
	}

	// Token: 0x060092F4 RID: 37620 RVA: 0x002FA4F8 File Offset: 0x002F86F8
	public void SetLayer(int layer)
	{
		if (this.m_planeGameObject)
		{
			this.m_planeGameObject.layer = layer;
		}
		if (this.m_shadowPlaneGameObject)
		{
			this.m_shadowPlaneGameObject.layer = layer;
		}
		if (this.m_renderOnObject)
		{
			this.m_renderOnObject.layer = layer;
		}
	}

	// Token: 0x060092F5 RID: 37621 RVA: 0x002FA550 File Offset: 0x002F8750
	public bool HasLayer(int layer)
	{
		bool flag = false;
		if (this.m_planeGameObject)
		{
			flag = (this.m_planeGameObject.layer == layer);
		}
		if (flag && this.m_shadowPlaneGameObject)
		{
			flag = (this.m_shadowPlaneGameObject.layer == layer);
		}
		return flag;
	}

	// Token: 0x060092F6 RID: 37622 RVA: 0x002FA59C File Offset: 0x002F879C
	public void SetAntialiasOffset(float offset)
	{
		Vector2 vector = this.m_renderTexture.texelSize * offset;
		this.m_antialiasingMaterialQuery.WithOffsetX(vector.x);
		this.m_antialiasingMaterialQuery.WithOffsetY(vector.y);
	}

	// Token: 0x060092F7 RID: 37623 RVA: 0x002FA5DF File Offset: 0x002F87DF
	public void SetAntialiasEdge(float edge)
	{
		this.m_antialiasingMaterialQuery.WithEdge(edge);
	}

	// Token: 0x060092F8 RID: 37624 RVA: 0x002FA5F0 File Offset: 0x002F87F0
	public void SetShadowOffset(float offset)
	{
		Vector2 vector = this.m_renderTexture.texelSize * offset;
		this.m_shadowMaterialQuery.WithOffsetX(vector.x);
		this.m_shadowMaterialQuery.WithOffsetY(vector.y);
	}

	// Token: 0x060092F9 RID: 37625 RVA: 0x002FA633 File Offset: 0x002F8833
	public void SetShadowColor(Color color)
	{
		this.m_shadowMaterialQuery.WithColor(color);
	}

	// Token: 0x060092FA RID: 37626 RVA: 0x002FA644 File Offset: 0x002F8844
	public PopupRenderer GetPopupRenderer()
	{
		if (!this.m_popupRendererComponent)
		{
			if (this.m_renderOnObject && this.m_renderOnObjectRenderer)
			{
				this.m_popupRendererComponent = this.m_renderOnObject.GetComponent<PopupRenderer>();
				if (!this.m_popupRendererComponent)
				{
					this.m_popupRendererComponent = this.m_renderOnObject.AddComponent<PopupRenderer>();
				}
			}
			else if (this.m_planeGameObject)
			{
				this.m_popupRendererComponent = this.m_planeGameObject.AddComponent<PopupRenderer>();
			}
		}
		return this.m_popupRendererComponent;
	}

	// Token: 0x060092FB RID: 37627 RVA: 0x002FA6CD File Offset: 0x002F88CD
	public void DisablePopupRenderer()
	{
		if (this.m_popupRendererComponent)
		{
			this.m_popupRendererComponent.DisablePopupRendering();
		}
	}

	// Token: 0x060092FC RID: 37628 RVA: 0x002FA6E8 File Offset: 0x002F88E8
	public void SetCameraPosition(UberText.AlignmentOptions alignment, UberText.AnchorOptions anchor, Vector2 size, float offset)
	{
		float num = -3000f;
		Vector2 vector = size * 0.5f;
		if (alignment == UberText.AlignmentOptions.Left)
		{
			num += vector.x;
		}
		if (alignment == UberText.AlignmentOptions.Right)
		{
			num -= vector.x;
		}
		float num2 = 0f;
		if (anchor == UberText.AnchorOptions.Upper)
		{
			num2 += vector.y;
		}
		if (anchor == UberText.AnchorOptions.Lower)
		{
			num2 -= vector.y;
		}
		Vector3 position = new Vector3(num, 3000f - num2, offset);
		UberTextRenderToTexture.s_cameraGameObject.transform.position = position;
		UberTextRenderToTexture.s_renderCamera.orthographicSize = vector.y;
	}

	// Token: 0x060092FD RID: 37629 RVA: 0x002FA770 File Offset: 0x002F8970
	public void SetCameraBackgroundColor(Color color)
	{
		UberTextRenderToTexture.s_renderCamera.backgroundColor = new Color(color.r, color.g, color.b, 0f);
	}

	// Token: 0x060092FE RID: 37630 RVA: 0x002FA798 File Offset: 0x002F8998
	public void RemoveShadow()
	{
		if (this.m_shadowMeshRenderer)
		{
			this.m_shadowMeshRenderer.enabled = false;
		}
	}

	// Token: 0x060092FF RID: 37631 RVA: 0x002FA7B3 File Offset: 0x002F89B3
	public void SetRenderQueueIncrement(int value)
	{
		if (this.m_planeMeshRenderer)
		{
			this.m_planeMeshRenderer.sortingOrder = value;
		}
		this.m_planeMaterialQuery.WithIncrementRenderQueue(value);
		this.m_antialiasingMaterialQuery.WithIncrementRenderQueue(value);
	}

	// Token: 0x06009300 RID: 37632 RVA: 0x002FA7E8 File Offset: 0x002F89E8
	public void SetShadowRenderQueueIncrement(int value)
	{
		if (this.m_shadowMeshRenderer)
		{
			this.m_shadowMeshRenderer.sortingOrder = value;
		}
		this.m_shadowMaterialQuery.WithIncrementRenderQueue(value);
	}

	// Token: 0x06009301 RID: 37633 RVA: 0x002FA810 File Offset: 0x002F8A10
	public bool IsRenderTextureCreated()
	{
		return this.m_renderTexture && this.m_renderTexture.IsCreated();
	}

	// Token: 0x06009302 RID: 37634 RVA: 0x002FA82C File Offset: 0x002F8A2C
	private void CreatePlane(GameObject parent, Color gradientUpperColor, Color gradientLowerColor)
	{
		string text = "UberText_RenderPlane_" + parent.name;
		for (int i = parent.transform.childCount - 1; i >= 0; i--)
		{
			Transform child = parent.transform.GetChild(i);
			if (child && child.gameObject.name == text)
			{
				UnityEngine.Object.DestroyImmediate(child.gameObject);
			}
		}
		this.m_planeGameObject = new GameObject();
		this.m_planeGameObject.name = text;
		SceneUtils.SetHideFlags(this.m_planeGameObject, HideFlags.DontSave);
		this.m_planeGameObject.layer = parent.layer;
		this.m_planeGameObject.transform.parent = parent.transform;
		this.m_planeGameObject.transform.localScale = Vector3.one;
		this.m_planeMeshFilter = this.m_planeGameObject.AddComponent<MeshFilter>();
		this.m_planeMeshFilter.sharedMesh = this.AcquireAppropriateMesh(this.m_currentPlaneSize, gradientUpperColor, gradientLowerColor);
		this.m_planeMeshRenderer = this.m_planeGameObject.AddComponent<MeshRenderer>();
	}

	// Token: 0x06009303 RID: 37635 RVA: 0x002FA930 File Offset: 0x002F8B30
	private Mesh AcquireAppropriateMesh(Vector2 meshSize, Color gradientUpperColor, Color gradientLowerColor)
	{
		if (this.m_currentUsedMeshId != -1)
		{
			this.ReleaseCurrentMesh();
		}
		Mesh mesh = null;
		int i;
		for (i = 0; i < UberTextRenderToTexture.s_usedMeshesList.Count; i++)
		{
			UberTextRenderToTexture.UsedMesh usedMesh = UberTextRenderToTexture.s_usedMeshesList[i];
			if (usedMesh.PlaneSize == meshSize && usedMesh.GradientUpperColor == gradientUpperColor && usedMesh.GradientLowerColor == gradientLowerColor)
			{
				mesh = usedMesh.Mesh;
				break;
			}
		}
		if (!mesh)
		{
			mesh = this.CreateMesh(meshSize, gradientUpperColor, gradientLowerColor);
			UberTextRenderToTexture.s_usedMeshesList.Add(new UberTextRenderToTexture.UsedMesh
			{
				Mesh = mesh,
				PlaneSize = meshSize,
				GradientUpperColor = gradientUpperColor,
				GradientLowerColor = gradientLowerColor,
				ReferenceCounter = 1,
				IsValid = true,
				Id = UberTextRenderToTexture.s_newUsedMeshId
			});
			this.m_currentUsedMeshId = UberTextRenderToTexture.s_newUsedMeshId;
			UberTextRenderToTexture.s_newUsedMeshId++;
		}
		else
		{
			UberTextRenderToTexture.UsedMesh usedMesh2 = UberTextRenderToTexture.s_usedMeshesList[i];
			usedMesh2.ReferenceCounter++;
			UberTextRenderToTexture.s_usedMeshesList[i] = usedMesh2;
			this.m_currentUsedMeshId = usedMesh2.Id;
		}
		return mesh;
	}

	// Token: 0x06009304 RID: 37636 RVA: 0x002FAA50 File Offset: 0x002F8C50
	private void ReleaseCurrentMesh()
	{
		if (this.m_currentUsedMeshId == -1)
		{
			return;
		}
		int i = UberTextRenderToTexture.s_usedMeshesList.Count - 1;
		while (i >= 0)
		{
			UberTextRenderToTexture.UsedMesh usedMesh = UberTextRenderToTexture.s_usedMeshesList[i];
			if (usedMesh.Id == this.m_currentUsedMeshId)
			{
				usedMesh.ReferenceCounter--;
				if (usedMesh.ReferenceCounter <= 0)
				{
					UberTextRenderToTexture.s_usedMeshesList.RemoveAt(i);
					this.m_currentUsedMeshId = -1;
					return;
				}
				UberTextRenderToTexture.s_usedMeshesList[i] = usedMesh;
				return;
			}
			else
			{
				i--;
			}
		}
	}

	// Token: 0x06009305 RID: 37637 RVA: 0x002FAAD0 File Offset: 0x002F8CD0
	private Mesh CreateMesh(Vector2 meshSize, Color gradientUpperColor, Color gradientLowerColor)
	{
		float num = meshSize.x * 0.5f;
		float num2 = meshSize.y * 0.5f;
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[]
		{
			new Vector3(-num, 0f, -num2),
			new Vector3(num, 0f, -num2),
			new Vector3(-num, 0f, num2),
			new Vector3(num, 0f, num2)
		};
		if (gradientLowerColor != Color.clear || gradientUpperColor != Color.clear)
		{
			mesh.colors = new Color[]
			{
				gradientUpperColor,
				gradientUpperColor,
				gradientLowerColor,
				gradientLowerColor
			};
		}
		mesh.uv = this.PLANE_UVS;
		mesh.normals = this.PLANE_NORMALS;
		mesh.triangles = this.PLANE_TRIANGLES;
		mesh.RecalculateBounds();
		return mesh;
	}

	// Token: 0x06009306 RID: 37638 RVA: 0x002FABCC File Offset: 0x002F8DCC
	private UberTextRenderToTexture.UsedMesh GetCurrentUsedMesh()
	{
		if (this.m_currentUsedMeshId != -1)
		{
			foreach (UberTextRenderToTexture.UsedMesh usedMesh in UberTextRenderToTexture.s_usedMeshesList)
			{
				if (usedMesh.Id == this.m_currentUsedMeshId)
				{
					return usedMesh;
				}
			}
		}
		return default(UberTextRenderToTexture.UsedMesh);
	}

	// Token: 0x06009307 RID: 37639 RVA: 0x002FAC40 File Offset: 0x002F8E40
	private void CreateCamera()
	{
		if (UberTextRenderToTexture.s_renderCamera)
		{
			return;
		}
		UberTextRenderToTexture.s_cameraGameObject = new GameObject();
		UberTextRenderToTexture.s_cameraGameObject.AddComponent<RenderToTextureCamera>();
		UberTextRenderToTexture.s_cameraGameObject.name = "UberText_RenderCamera";
		SceneUtils.SetHideFlags(UberTextRenderToTexture.s_cameraGameObject, HideFlags.HideAndDontSave);
		UberTextRenderToTexture.s_renderCamera = UberTextRenderToTexture.s_cameraGameObject.AddComponent<Camera>();
		UberTextRenderToTexture.s_renderCamera.orthographic = true;
		UberTextRenderToTexture.s_renderCamera.nearClipPlane = -0.1f;
		UberTextRenderToTexture.s_renderCamera.farClipPlane = 0.1f;
		Camera main = Camera.main;
		if (main)
		{
			UberTextRenderToTexture.s_renderCamera.depth = main.depth - 50f;
		}
		UberTextRenderToTexture.s_renderCamera.clearFlags = CameraClearFlags.Color;
		UberTextRenderToTexture.s_renderCamera.depthTextureMode = DepthTextureMode.None;
		UberTextRenderToTexture.s_renderCamera.renderingPath = RenderingPath.Forward;
		UberTextRenderToTexture.s_renderCamera.cullingMask = UberTextRenderToTexture.RENDER_LAYER_BIT;
		UberTextRenderToTexture.s_renderCamera.enabled = false;
	}

	// Token: 0x06009308 RID: 37640 RVA: 0x002FAD24 File Offset: 0x002F8F24
	private void CreateRenderTexture(Vector2Int size)
	{
		if (this.m_renderTexture)
		{
			if (this.m_renderTexture.width == size.x && this.m_renderTexture.height == size.y)
			{
				return;
			}
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_renderTexture);
		}
		this.FindSupportedTextureFormat();
		this.m_renderTexture = RenderTextureTracker.Get().CreateNewTexture(size.x, size.y, 0, UberTextRenderToTexture.s_textureFormat, RenderTextureReadWrite.Default);
		SceneUtils.SetHideFlags(this.m_renderTexture, HideFlags.HideAndDontSave);
		this.m_planeMaterialQuery.WithTexture(this.m_renderTexture);
		this.m_antialiasingMaterialQuery.WithTexture(this.m_renderTexture);
	}

	// Token: 0x06009309 RID: 37641 RVA: 0x002FADD4 File Offset: 0x002F8FD4
	private void FindSupportedTextureFormat()
	{
		if (!UberTextRenderToTexture.s_hasLookedForSupportedTextureFormat)
		{
			UberTextRenderToTexture.s_hasLookedForSupportedTextureFormat = true;
			bool flag = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB4444) && PlatformSettings.RuntimeOS != OSCategory.PC;
			if (UberTextRenderToTexture.s_textureFormat == RenderTextureFormat.DefaultHDR && flag)
			{
				UberTextRenderToTexture.s_textureFormat = RenderTextureFormat.ARGB4444;
			}
			if (UberTextRenderToTexture.s_textureFormat == RenderTextureFormat.DefaultHDR && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32))
			{
				UberTextRenderToTexture.s_textureFormat = RenderTextureFormat.ARGB32;
			}
		}
	}

	// Token: 0x0600930A RID: 37642 RVA: 0x002FAE30 File Offset: 0x002F9030
	private void CreateShadowPlane(GameObject parent, Vector3 localPostion)
	{
		this.m_shadowPlaneGameObject = new GameObject();
		this.m_shadowPlaneGameObject.name = "UberText_ShadowPlane_" + parent.name;
		SceneUtils.SetHideFlags(this.m_shadowPlaneGameObject, HideFlags.DontSave);
		this.m_shadowPlaneGameObject.layer = parent.layer;
		this.m_shadowPlaneGameObject.transform.parent = this.m_planeGameObject.transform;
		this.m_shadowPlaneGameObject.transform.localRotation = Quaternion.identity;
		this.m_shadowPlaneGameObject.transform.localScale = Vector3.one;
		this.m_shadowPlaneGameObject.transform.localPosition = localPostion;
		this.m_shadowMeshFilter = this.m_shadowPlaneGameObject.AddComponent<MeshFilter>();
		this.m_shadowMeshFilter.sharedMesh = this.AcquireAppropriateMesh(this.m_currentPlaneSize, Color.clear, Color.clear);
		this.m_shadowMeshRenderer = this.m_shadowPlaneGameObject.AddComponent<MeshRenderer>();
	}

	// Token: 0x0600930B RID: 37643 RVA: 0x002FAF1C File Offset: 0x002F911C
	private UberTextMaterial ApplyMaterialIfNeeded(Renderer renderer, UberTextMaterial currentMaterial, UberTextMaterialQuery query)
	{
		if (currentMaterial == null || !currentMaterial.HasQuery(query))
		{
			UberTextMaterialManager uberTextMaterialManager = UberTextMaterialManager.Get();
			if (currentMaterial != null)
			{
				uberTextMaterialManager.ReleaseMaterial(currentMaterial);
			}
			currentMaterial = uberTextMaterialManager.FetchMaterial(query);
			Material material = currentMaterial.Acquire();
			renderer.SetSharedMaterial(material);
		}
		else if (!currentMaterial.IsStillBound(renderer))
		{
			currentMaterial.Rebound(renderer, 0);
		}
		return currentMaterial;
	}

	// Token: 0x04007B14 RID: 31508
	private const int INVALID_MESH_ID = -1;

	// Token: 0x04007B15 RID: 31509
	private GameObject m_renderOnObject;

	// Token: 0x04007B16 RID: 31510
	private Renderer m_renderOnObjectRenderer;

	// Token: 0x04007B17 RID: 31511
	private GameObject m_planeGameObject;

	// Token: 0x04007B18 RID: 31512
	private MeshFilter m_planeMeshFilter;

	// Token: 0x04007B19 RID: 31513
	private MeshRenderer m_planeMeshRenderer;

	// Token: 0x04007B1A RID: 31514
	private GameObject m_shadowPlaneGameObject;

	// Token: 0x04007B1B RID: 31515
	private MeshFilter m_shadowMeshFilter;

	// Token: 0x04007B1C RID: 31516
	private MeshRenderer m_shadowMeshRenderer;

	// Token: 0x04007B1D RID: 31517
	private Vector2 m_currentPlaneSize;

	// Token: 0x04007B1E RID: 31518
	private int m_currentUsedMeshId = -1;

	// Token: 0x04007B1F RID: 31519
	private PlaneMaterialQuery m_planeMaterialQuery = new PlaneMaterialQuery();

	// Token: 0x04007B20 RID: 31520
	private AntiAlisaingMaterialQuery m_antialiasingMaterialQuery = new AntiAlisaingMaterialQuery();

	// Token: 0x04007B21 RID: 31521
	private ShadowMaterialQuery m_shadowMaterialQuery = new ShadowMaterialQuery();

	// Token: 0x04007B22 RID: 31522
	private UberTextMaterial m_planeMaterial;

	// Token: 0x04007B23 RID: 31523
	private UberTextMaterial m_renderOnObjectMaterial;

	// Token: 0x04007B24 RID: 31524
	private UberTextMaterial m_shadowMaterial;

	// Token: 0x04007B25 RID: 31525
	private UberTextMaterial m_antialiasMaterial;

	// Token: 0x04007B26 RID: 31526
	private bool m_shouldBeVisible;

	// Token: 0x04007B27 RID: 31527
	private bool m_init;

	// Token: 0x04007B28 RID: 31528
	private PopupRenderer m_popupRendererComponent;

	// Token: 0x04007B29 RID: 31529
	private readonly Vector2[] PLANE_UVS = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	// Token: 0x04007B2A RID: 31530
	private readonly Vector3[] PLANE_NORMALS = new Vector3[]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	// Token: 0x04007B2B RID: 31531
	private readonly int[] PLANE_TRIANGLES = new int[]
	{
		3,
		1,
		2,
		2,
		1,
		0
	};

	// Token: 0x04007B2C RID: 31532
	private static readonly List<UberTextRenderToTexture.UsedMesh> s_usedMeshesList = new List<UberTextRenderToTexture.UsedMesh>(5);

	// Token: 0x04007B2D RID: 31533
	private static int s_newUsedMeshId = 0;

	// Token: 0x04007B2E RID: 31534
	private static int RENDER_LAYER_BIT = GameLayer.InvisibleRender.LayerBit();

	// Token: 0x04007B2F RID: 31535
	private static Camera s_renderCamera;

	// Token: 0x04007B30 RID: 31536
	private static GameObject s_cameraGameObject;

	// Token: 0x04007B31 RID: 31537
	private static bool s_hasLookedForSupportedTextureFormat;

	// Token: 0x04007B32 RID: 31538
	private static RenderTextureFormat s_textureFormat = RenderTextureFormat.DefaultHDR;

	// Token: 0x04007B33 RID: 31539
	private RenderTexture m_renderTexture;

	// Token: 0x020026F0 RID: 9968
	private struct UsedMesh
	{
		// Token: 0x0400F2A8 RID: 62120
		public bool IsValid;

		// Token: 0x0400F2A9 RID: 62121
		public Mesh Mesh;

		// Token: 0x0400F2AA RID: 62122
		public Vector2 PlaneSize;

		// Token: 0x0400F2AB RID: 62123
		public Color GradientUpperColor;

		// Token: 0x0400F2AC RID: 62124
		public Color GradientLowerColor;

		// Token: 0x0400F2AD RID: 62125
		public int ReferenceCounter;

		// Token: 0x0400F2AE RID: 62126
		public int Id;
	}
}
