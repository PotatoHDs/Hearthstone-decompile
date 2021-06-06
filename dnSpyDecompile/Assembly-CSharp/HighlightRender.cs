using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000A3C RID: 2620
public class HighlightRender : MonoBehaviour
{
	// Token: 0x170007ED RID: 2029
	// (get) Token: 0x06008CDD RID: 36061 RVA: 0x002D1BB6 File Offset: 0x002CFDB6
	protected Material HighlightMaterial
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.m_HighlightShader);
				SceneUtils.SetHideFlags(this.m_Material, HideFlags.DontSave);
			}
			return this.m_Material;
		}
	}

	// Token: 0x170007EE RID: 2030
	// (get) Token: 0x06008CDE RID: 36062 RVA: 0x002D1BEA File Offset: 0x002CFDEA
	protected Material MultiSampleMaterial
	{
		get
		{
			if (this.m_MultiSampleMaterial == null)
			{
				this.m_MultiSampleMaterial = new Material(this.m_MultiSampleShader);
				SceneUtils.SetHideFlags(this.m_MultiSampleMaterial, HideFlags.DontSave);
			}
			return this.m_MultiSampleMaterial;
		}
	}

	// Token: 0x170007EF RID: 2031
	// (get) Token: 0x06008CDF RID: 36063 RVA: 0x002D1C1E File Offset: 0x002CFE1E
	protected Material MultiSampleBlendMaterial
	{
		get
		{
			if (this.m_MultiSampleBlendMaterial == null)
			{
				this.m_MultiSampleBlendMaterial = new Material(this.m_MultiSampleBlendShader);
				SceneUtils.SetHideFlags(this.m_MultiSampleBlendMaterial, HideFlags.DontSave);
			}
			return this.m_MultiSampleBlendMaterial;
		}
	}

	// Token: 0x170007F0 RID: 2032
	// (get) Token: 0x06008CE0 RID: 36064 RVA: 0x002D1C52 File Offset: 0x002CFE52
	protected Material BlendMaterial
	{
		get
		{
			if (this.m_BlendMaterial == null)
			{
				this.m_BlendMaterial = new Material(this.m_BlendShader);
				SceneUtils.SetHideFlags(this.m_BlendMaterial, HideFlags.DontSave);
			}
			return this.m_BlendMaterial;
		}
	}

	// Token: 0x06008CE1 RID: 36065 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected void OnApplicationFocus(bool state)
	{
	}

	// Token: 0x06008CE2 RID: 36066 RVA: 0x002D1C88 File Offset: 0x002CFE88
	protected void OnDisable()
	{
		if (this.m_Material)
		{
			UnityEngine.Object.Destroy(this.m_Material);
		}
		if (this.m_MultiSampleMaterial)
		{
			UnityEngine.Object.Destroy(this.m_MultiSampleMaterial);
		}
		if (this.m_BlendMaterial)
		{
			UnityEngine.Object.Destroy(this.m_BlendMaterial);
		}
		if (this.m_MultiSampleBlendMaterial)
		{
			UnityEngine.Object.Destroy(this.m_MultiSampleBlendMaterial);
		}
		if (this.m_VisibilityStates != null)
		{
			this.m_VisibilityStates.Clear();
		}
		if (this.m_CameraTexture != null)
		{
			if (RenderTexture.active == this.m_CameraTexture)
			{
				RenderTexture.active = null;
			}
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_CameraTexture);
			this.m_CameraTexture = null;
		}
		this.m_Initialized = false;
	}

	// Token: 0x06008CE3 RID: 36067 RVA: 0x002D1D4C File Offset: 0x002CFF4C
	protected void Initialize()
	{
		if (this.m_Initialized)
		{
			return;
		}
		this.m_Initialized = true;
		if (this.m_HighlightShader == null)
		{
			this.m_HighlightShader = ShaderUtils.FindShader(this.HIGHLIGHT_SHADER_NAME);
		}
		if (!this.m_HighlightShader)
		{
			Debug.LogError("Failed to load Highlight Shader: " + this.HIGHLIGHT_SHADER_NAME);
			base.enabled = false;
		}
		base.GetComponent<Renderer>().GetMaterial().shader = this.m_HighlightShader;
		if (this.m_MultiSampleShader == null)
		{
			this.m_MultiSampleShader = ShaderUtils.FindShader(this.MULTISAMPLE_SHADER_NAME);
		}
		if (!this.m_MultiSampleShader)
		{
			Debug.LogError("Failed to load Highlight Shader: " + this.MULTISAMPLE_SHADER_NAME);
			base.enabled = false;
		}
		if (this.m_MultiSampleBlendShader == null)
		{
			this.m_MultiSampleBlendShader = ShaderUtils.FindShader(this.MULTISAMPLE_BLEND_SHADER_NAME);
		}
		if (!this.m_MultiSampleBlendShader)
		{
			Debug.LogError("Failed to load Highlight Shader: " + this.MULTISAMPLE_BLEND_SHADER_NAME);
			base.enabled = false;
		}
		if (this.m_BlendShader == null)
		{
			this.m_BlendShader = ShaderUtils.FindShader(this.BLEND_SHADER_NAME);
		}
		if (!this.m_BlendShader)
		{
			Debug.LogError("Failed to load Highlight Shader: " + this.BLEND_SHADER_NAME);
			base.enabled = false;
		}
		if (this.m_RootTransform == null)
		{
			Transform parent = base.transform.parent.parent;
			if (parent.GetComponent<ActorStateMgr>())
			{
				this.m_RootTransform = parent.parent;
			}
			else
			{
				this.m_RootTransform = parent;
			}
			if (this.m_RootTransform == null)
			{
				Debug.LogError("m_RootTransform is null. Highlighting disabled!");
				base.enabled = false;
			}
		}
		this.m_VisibilityStates = new Map<Renderer, bool>();
		HighlightSilhouetteInclude[] componentsInChildren = this.m_RootTransform.GetComponentsInChildren<HighlightSilhouetteInclude>();
		if (componentsInChildren != null)
		{
			HighlightSilhouetteInclude[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				Renderer component = array[i].gameObject.GetComponent<Renderer>();
				if (!(component == null))
				{
					this.m_VisibilityStates.Add(component, false);
				}
			}
		}
		this.m_UnlitColorShader = ShaderUtils.FindShader(this.UNLIT_COLOR_SHADER_NAME);
		if (!this.m_UnlitColorShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + this.UNLIT_COLOR_SHADER_NAME);
		}
		this.m_UnlitGreyShader = ShaderUtils.FindShader(this.UNLIT_GREY_SHADER_NAME);
		if (!this.m_UnlitGreyShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + this.UNLIT_GREY_SHADER_NAME);
		}
		this.m_UnlitLightGreyShader = ShaderUtils.FindShader(this.UNLIT_LIGHTGREY_SHADER_NAME);
		if (!this.m_UnlitLightGreyShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + this.UNLIT_LIGHTGREY_SHADER_NAME);
		}
		this.m_UnlitDarkGreyShader = ShaderUtils.FindShader(this.UNLIT_DARKGREY_SHADER_NAME);
		if (!this.m_UnlitDarkGreyShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + this.UNLIT_DARKGREY_SHADER_NAME);
		}
		this.m_UnlitBlackShader = ShaderUtils.FindShader(this.UNLIT_BLACK_SHADER_NAME);
		if (!this.m_UnlitBlackShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + this.UNLIT_BLACK_SHADER_NAME);
		}
		this.m_UnlitWhiteShader = ShaderUtils.FindShader(this.UNLIT_WHITE_SHADER_NAME);
		if (!this.m_UnlitWhiteShader)
		{
			Debug.LogError("Failed to load Highlight Rendering Shader: " + this.UNLIT_WHITE_SHADER_NAME);
		}
	}

	// Token: 0x06008CE4 RID: 36068 RVA: 0x002D2082 File Offset: 0x002D0282
	protected void Update()
	{
		if (this.m_CameraTexture && this.m_Initialized && !this.m_CameraTexture.IsCreated())
		{
			this.CreateSilhouetteTexture();
		}
	}

	// Token: 0x06008CE5 RID: 36069 RVA: 0x002D20AC File Offset: 0x002D02AC
	[ContextMenu("Export Silhouette Texture")]
	public void ExportSilhouetteTexture()
	{
		RenderTexture.active = this.m_CameraTexture;
		Texture2D texture2D = new Texture2D(this.m_RenderSizeX, this.m_RenderSizeY, TextureFormat.RGB24, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)this.m_RenderSizeX, (float)this.m_RenderSizeY), 0, 0, false);
		texture2D.Apply();
		string text = Application.dataPath + "/SilhouetteTexture.png";
		File.WriteAllBytes(text, texture2D.EncodeToPNG());
		RenderTexture.active = null;
		Debug.Log(string.Format("Silhouette Texture Created: {0}", text));
	}

	// Token: 0x06008CE6 RID: 36070 RVA: 0x002D2136 File Offset: 0x002D0336
	public void CreateSilhouetteTexture()
	{
		this.CreateSilhouetteTexture(false);
	}

	// Token: 0x06008CE7 RID: 36071 RVA: 0x002D2140 File Offset: 0x002D0340
	public void CreateSilhouetteTexture(bool force)
	{
		this.Initialize();
		if (!this.VisibilityStatesChanged() && !force)
		{
			return;
		}
		this.SetupRenderObjects();
		if (this.m_RenderPlane == null)
		{
			return;
		}
		if (this.m_RenderSizeX < 1 || this.m_RenderSizeY < 1)
		{
			return;
		}
		bool enabled = base.GetComponent<Renderer>().enabled;
		base.GetComponent<Renderer>().enabled = false;
		RenderTexture temporary = RenderTexture.GetTemporary((int)((float)this.m_RenderSizeX * 0.3f), (int)((float)this.m_RenderSizeY * 0.3f), 24);
		RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)this.m_RenderSizeX * 0.3f), (int)((float)this.m_RenderSizeY * 0.3f), 24);
		RenderTexture temporary3 = RenderTexture.GetTemporary((int)((float)this.m_RenderSizeX * 0.3f), (int)((float)this.m_RenderSizeY * 0.3f), 24);
		RenderTexture temporary4 = RenderTexture.GetTemporary((int)((float)this.m_RenderSizeX * 0.5f), (int)((float)this.m_RenderSizeY * 0.5f), 24);
		RenderTexture temporary5 = RenderTexture.GetTemporary((int)((float)this.m_RenderSizeX * 0.5f), (int)((float)this.m_RenderSizeY * 0.5f), 24);
		RenderTexture temporary6 = RenderTexture.GetTemporary(this.m_RenderSizeX, this.m_RenderSizeY, 24);
		RenderTexture temporary7 = RenderTexture.GetTemporary(this.m_RenderSizeX, this.m_RenderSizeY, 24);
		RenderTexture temporary8 = RenderTexture.GetTemporary(this.m_RenderSizeX, this.m_RenderSizeY, 24);
		RenderTexture temporary9 = RenderTexture.GetTemporary((int)((float)this.m_RenderSizeX * 0.92f), (int)((float)this.m_RenderSizeY * 0.92f), 24);
		this.m_CameraTexture.DiscardContents();
		this.m_Camera.clearFlags = CameraClearFlags.Color;
		this.m_Camera.depth = Camera.main.depth - 1f;
		this.m_Camera.clearFlags = CameraClearFlags.Color;
		this.m_Camera.orthographicSize = this.m_CameraOrthoSize + 0.1f * this.m_SilouetteClipSize;
		this.m_Camera.targetTexture = temporary9;
		this.m_Camera.RenderWithShader(this.m_UnlitWhiteShader, "Highlight");
		this.m_Camera.depth = Camera.main.depth - 5f;
		this.m_Camera.orthographicSize = this.m_CameraOrthoSize - 0.2f * this.m_SilouetteRenderSize;
		this.m_Camera.targetTexture = temporary;
		this.m_Camera.RenderWithShader(this.m_UnlitDarkGreyShader, "Highlight");
		this.m_Camera.depth = Camera.main.depth - 4f;
		this.m_Camera.orthographicSize = this.m_CameraOrthoSize - 0.25f * this.m_SilouetteRenderSize;
		this.m_Camera.targetTexture = temporary2;
		this.m_Camera.RenderWithShader(this.m_UnlitGreyShader, "Highlight");
		this.SampleBlend(temporary, temporary2, temporary3, 1.25f);
		this.m_Camera.depth = Camera.main.depth - 3f;
		this.m_Camera.orthographicSize = this.m_CameraOrthoSize - 0.01f * this.m_SilouetteRenderSize;
		this.m_Camera.targetTexture = temporary4;
		this.m_Camera.RenderWithShader(this.m_UnlitLightGreyShader, "Highlight");
		this.SampleBlend(temporary3, temporary4, temporary5, 1.25f);
		this.m_Camera.depth = Camera.main.depth - 2f;
		this.m_Camera.orthographicSize = this.m_CameraOrthoSize - -0.05f * this.m_SilouetteRenderSize;
		this.m_Camera.targetTexture = temporary6;
		this.m_Camera.RenderWithShader(this.m_UnlitWhiteShader, "Highlight");
		this.SampleBlend(temporary5, temporary6, temporary7, 1f);
		this.Sample(temporary7, temporary8, 1.5f);
		this.BlendMaterial.SetTexture("_BlendTex", temporary9);
		float num = 0.8f;
		Graphics.BlitMultiTap(temporary8, this.m_CameraTexture, this.BlendMaterial, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(temporary3);
		RenderTexture.ReleaseTemporary(temporary4);
		RenderTexture.ReleaseTemporary(temporary5);
		RenderTexture.ReleaseTemporary(temporary6);
		RenderTexture.ReleaseTemporary(temporary7);
		RenderTexture.ReleaseTemporary(temporary8);
		RenderTexture.ReleaseTemporary(temporary9);
		this.m_Camera.orthographicSize = this.m_CameraOrthoSize;
		base.GetComponent<Renderer>().enabled = enabled;
		this.RestoreRenderObjects();
	}

	// Token: 0x170007F1 RID: 2033
	// (get) Token: 0x06008CE8 RID: 36072 RVA: 0x002D25B9 File Offset: 0x002D07B9
	public RenderTexture SilhouetteTexture
	{
		get
		{
			return this.m_CameraTexture;
		}
	}

	// Token: 0x06008CE9 RID: 36073 RVA: 0x002D25C1 File Offset: 0x002D07C1
	public bool isTextureCreated()
	{
		return this.m_CameraTexture && this.m_CameraTexture.IsCreated();
	}

	// Token: 0x06008CEA RID: 36074 RVA: 0x002D25E0 File Offset: 0x002D07E0
	private void SetupRenderObjects()
	{
		if (this.m_RootTransform == null)
		{
			this.m_RenderPlane = null;
			return;
		}
		HighlightRender.s_offset -= 10f;
		if (HighlightRender.s_offset < -9000f)
		{
			HighlightRender.s_offset = -2000f;
		}
		this.m_OrgPosition = this.m_RootTransform.position;
		this.m_OrgRotation = this.m_RootTransform.rotation;
		this.m_OrgScale = this.m_RootTransform.localScale;
		Vector3 position = Vector3.left * HighlightRender.s_offset;
		this.m_RootTransform.position = position;
		this.SetWorldScale(this.m_RootTransform, Vector3.one);
		this.m_RootTransform.rotation = Quaternion.identity;
		Bounds bounds = base.GetComponent<Renderer>().bounds;
		float x = bounds.size.x;
		float num = bounds.size.z;
		if (num < bounds.size.y)
		{
			num = bounds.size.y;
		}
		if (x > num)
		{
			this.m_RenderSizeX = 200;
			this.m_RenderSizeY = (int)(200f * (num / x));
		}
		else
		{
			this.m_RenderSizeX = (int)(200f * (x / num));
			this.m_RenderSizeY = 200;
		}
		this.m_CameraOrthoSize = num * 0.5f;
		if (this.m_CameraTexture == null)
		{
			if (this.m_RenderSizeX < 1 || this.m_RenderSizeY < 1)
			{
				this.m_RenderSizeX = 200;
				this.m_RenderSizeY = 200;
			}
			this.m_CameraTexture = RenderTextureTracker.Get().CreateNewTexture(this.m_RenderSizeX, this.m_RenderSizeY, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
			this.m_CameraTexture.format = RenderTextureFormat.ARGB32;
		}
		HighlightState componentInChildren = this.m_RootTransform.GetComponentInChildren<HighlightState>();
		if (componentInChildren == null)
		{
			Debug.LogError("Can not find Highlight(HighlightState component) object for selection highlighting.");
			this.m_RenderPlane = null;
			return;
		}
		componentInChildren.transform.localPosition = Vector3.zero;
		HighlightRender componentInChildren2 = this.m_RootTransform.GetComponentInChildren<HighlightRender>();
		if (componentInChildren2 == null)
		{
			Debug.LogError("Can not find render plane object(HighlightRender component) for selection highlighting.");
			this.m_RenderPlane = null;
			return;
		}
		this.m_RenderPlane = componentInChildren2.gameObject;
		this.m_RenderScale = HighlightRender.GetWorldScale(this.m_RenderPlane.transform).x;
		this.CreateCamera(componentInChildren2.transform);
	}

	// Token: 0x06008CEB RID: 36075 RVA: 0x002D281E File Offset: 0x002D0A1E
	private void RestoreRenderObjects()
	{
		this.m_RootTransform.position = this.m_OrgPosition;
		this.m_RootTransform.rotation = this.m_OrgRotation;
		this.m_RootTransform.localScale = this.m_OrgScale;
		this.m_RenderPlane = null;
	}

	// Token: 0x06008CEC RID: 36076 RVA: 0x002D285C File Offset: 0x002D0A5C
	private bool VisibilityStatesChanged()
	{
		bool result = false;
		HighlightSilhouetteInclude[] componentsInChildren = this.m_RootTransform.GetComponentsInChildren<HighlightSilhouetteInclude>();
		List<Renderer> list = new List<Renderer>();
		HighlightSilhouetteInclude[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Renderer component = array[i].gameObject.GetComponent<Renderer>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		foreach (Renderer renderer in list)
		{
			bool flag = renderer.enabled && renderer.gameObject.activeInHierarchy;
			if (!this.m_VisibilityStates.ContainsKey(renderer))
			{
				this.m_VisibilityStates.Add(renderer, flag);
				if (flag)
				{
					result = true;
				}
			}
			else if (this.m_VisibilityStates[renderer] != flag)
			{
				this.m_VisibilityStates[renderer] = flag;
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06008CED RID: 36077 RVA: 0x002D294C File Offset: 0x002D0B4C
	private List<GameObject> GetExcludedObjects()
	{
		List<GameObject> list = new List<GameObject>();
		HighlightExclude[] componentsInChildren = this.m_RootTransform.GetComponentsInChildren<HighlightExclude>();
		if (componentsInChildren == null)
		{
			return null;
		}
		HighlightExclude[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Transform[] componentsInChildren2 = array[i].GetComponentsInChildren<Transform>();
			if (componentsInChildren2 != null)
			{
				foreach (Transform transform in componentsInChildren2)
				{
					list.Add(transform.gameObject);
				}
			}
		}
		list.Add(base.gameObject);
		list.Add(base.transform.parent.gameObject);
		return list;
	}

	// Token: 0x06008CEE RID: 36078 RVA: 0x002D29DC File Offset: 0x002D0BDC
	private bool isHighlighExclude(Transform objXform)
	{
		if (objXform == null)
		{
			return true;
		}
		HighlightExclude component = objXform.GetComponent<HighlightExclude>();
		if (component && component.enabled)
		{
			return true;
		}
		Transform parent = objXform.transform.parent;
		if (parent != null)
		{
			int num = 0;
			while ((parent != this.m_RootTransform || parent != null) && !(parent == null) && num <= 25)
			{
				HighlightExclude component2 = parent.GetComponent<HighlightExclude>();
				if (component2 != null && component2.ExcludeChildren)
				{
					return true;
				}
				parent = parent.parent;
				num++;
			}
		}
		return false;
	}

	// Token: 0x06008CEF RID: 36079 RVA: 0x002D2A74 File Offset: 0x002D0C74
	private void CreateCamera(Transform renderPlane)
	{
		if (this.m_Camera != null)
		{
			return;
		}
		GameObject gameObject = new GameObject();
		this.m_Camera = gameObject.AddComponent<Camera>();
		gameObject.name = renderPlane.name + "_SilhouetteCamera";
		SceneUtils.SetHideFlags(gameObject, HideFlags.HideAndDontSave);
		this.m_Camera.orthographic = true;
		this.m_Camera.orthographicSize = this.m_CameraOrthoSize;
		this.m_Camera.transform.position = renderPlane.position;
		this.m_Camera.transform.rotation = renderPlane.rotation;
		this.m_Camera.transform.Rotate(90f, 180f, 0f);
		this.m_Camera.transform.parent = renderPlane;
		this.m_Camera.nearClipPlane = -this.m_RenderScale + 1f;
		this.m_Camera.farClipPlane = this.m_RenderScale + 1f;
		this.m_Camera.depth = Camera.main.depth - 5f;
		this.m_Camera.backgroundColor = Color.black;
		this.m_Camera.clearFlags = CameraClearFlags.Color;
		this.m_Camera.depthTextureMode = DepthTextureMode.None;
		this.m_Camera.renderingPath = RenderingPath.VertexLit;
		this.m_Camera.allowHDR = false;
		this.m_Camera.SetReplacementShader(this.m_UnlitColorShader, "");
		this.m_Camera.targetTexture = null;
		this.m_Camera.enabled = false;
	}

	// Token: 0x06008CF0 RID: 36080 RVA: 0x002D2BF4 File Offset: 0x002D0DF4
	private void Sample(RenderTexture source, RenderTexture dest, float off)
	{
		if (source == null || dest == null)
		{
			return;
		}
		dest.DiscardContents();
		Graphics.BlitMultiTap(source, dest, this.MultiSampleMaterial, new Vector2[]
		{
			new Vector2(-off, -off),
			new Vector2(-off, off),
			new Vector2(off, off),
			new Vector2(off, -off)
		});
	}

	// Token: 0x06008CF1 RID: 36081 RVA: 0x002D2C6C File Offset: 0x002D0E6C
	private void SampleBlend(RenderTexture source, RenderTexture blend, RenderTexture dest, float off)
	{
		if (source == null || dest == null || blend == null)
		{
			return;
		}
		this.MultiSampleBlendMaterial.SetTexture("_BlendTex", blend);
		dest.DiscardContents();
		Graphics.BlitMultiTap(source, dest, this.MultiSampleBlendMaterial, new Vector2[]
		{
			new Vector2(-off, -off),
			new Vector2(-off, off),
			new Vector2(off, off),
			new Vector2(off, -off)
		});
	}

	// Token: 0x06008CF2 RID: 36082 RVA: 0x002D2D04 File Offset: 0x002D0F04
	public static Vector3 GetWorldScale(Transform transform)
	{
		Vector3 vector = transform.localScale;
		Transform parent = transform.parent;
		while (parent != null)
		{
			vector = Vector3.Scale(vector, parent.localScale);
			parent = parent.parent;
		}
		return vector;
	}

	// Token: 0x06008CF3 RID: 36083 RVA: 0x002D2D40 File Offset: 0x002D0F40
	public void SetWorldScale(Transform xform, Vector3 scale)
	{
		GameObject gameObject = new GameObject();
		Transform transform = gameObject.transform;
		transform.parent = null;
		transform.localRotation = Quaternion.identity;
		transform.localScale = Vector3.one;
		Transform parent = xform.parent;
		xform.parent = transform;
		xform.localScale = scale;
		xform.parent = parent;
		UnityEngine.Object.Destroy(gameObject);
	}

	// Token: 0x040075A6 RID: 30118
	private readonly string MULTISAMPLE_SHADER_NAME = "Custom/Selection/HighlightMultiSample";

	// Token: 0x040075A7 RID: 30119
	private readonly string MULTISAMPLE_BLEND_SHADER_NAME = "Custom/Selection/HighlightMultiSampleBlend";

	// Token: 0x040075A8 RID: 30120
	private readonly string BLEND_SHADER_NAME = "Custom/Selection/HighlightMaskBlend";

	// Token: 0x040075A9 RID: 30121
	private readonly string HIGHLIGHT_SHADER_NAME = "Custom/Selection/Highlight";

	// Token: 0x040075AA RID: 30122
	private readonly string UNLIT_COLOR_SHADER_NAME = "Custom/UnlitColor";

	// Token: 0x040075AB RID: 30123
	private readonly string UNLIT_GREY_SHADER_NAME = "Custom/Unlit/Color/Grey";

	// Token: 0x040075AC RID: 30124
	private readonly string UNLIT_LIGHTGREY_SHADER_NAME = "Custom/Unlit/Color/LightGrey";

	// Token: 0x040075AD RID: 30125
	private readonly string UNLIT_DARKGREY_SHADER_NAME = "Custom/Unlit/Color/DarkGrey";

	// Token: 0x040075AE RID: 30126
	private readonly string UNLIT_BLACK_SHADER_NAME = "Custom/Unlit/Color/BlackOverlay";

	// Token: 0x040075AF RID: 30127
	private readonly string UNLIT_WHITE_SHADER_NAME = "Custom/Unlit/Color/White";

	// Token: 0x040075B0 RID: 30128
	private const float RENDER_SIZE1 = 0.3f;

	// Token: 0x040075B1 RID: 30129
	private const float RENDER_SIZE2 = 0.3f;

	// Token: 0x040075B2 RID: 30130
	private const float RENDER_SIZE3 = 0.5f;

	// Token: 0x040075B3 RID: 30131
	private const float RENDER_SIZE4 = 0.92f;

	// Token: 0x040075B4 RID: 30132
	private const float ORTHO_SIZE1 = 0.2f;

	// Token: 0x040075B5 RID: 30133
	private const float ORTHO_SIZE2 = 0.25f;

	// Token: 0x040075B6 RID: 30134
	private const float ORTHO_SIZE3 = 0.01f;

	// Token: 0x040075B7 RID: 30135
	private const float ORTHO_SIZE4 = -0.05f;

	// Token: 0x040075B8 RID: 30136
	private const float BLUR_BLEND1 = 1.25f;

	// Token: 0x040075B9 RID: 30137
	private const float BLUR_BLEND2 = 1.25f;

	// Token: 0x040075BA RID: 30138
	private const float BLUR_BLEND3 = 1f;

	// Token: 0x040075BB RID: 30139
	private const float BLUR_BLEND4 = 1.5f;

	// Token: 0x040075BC RID: 30140
	private const int SILHOUETTE_RENDER_SIZE = 200;

	// Token: 0x040075BD RID: 30141
	private const int SILHOUETTE_RENDER_DEPTH = 24;

	// Token: 0x040075BE RID: 30142
	private const int MAX_HIGHLIGHT_EXCLUDE_PARENT_SEARCH = 25;

	// Token: 0x040075BF RID: 30143
	public Transform m_RootTransform;

	// Token: 0x040075C0 RID: 30144
	public float m_SilouetteRenderSize = 1f;

	// Token: 0x040075C1 RID: 30145
	public float m_SilouetteClipSize = 1f;

	// Token: 0x040075C2 RID: 30146
	private GameObject m_RenderPlane;

	// Token: 0x040075C3 RID: 30147
	private float m_RenderScale = 1f;

	// Token: 0x040075C4 RID: 30148
	private Vector3 m_OrgPosition;

	// Token: 0x040075C5 RID: 30149
	private Quaternion m_OrgRotation;

	// Token: 0x040075C6 RID: 30150
	private Vector3 m_OrgScale;

	// Token: 0x040075C7 RID: 30151
	private Shader m_MultiSampleShader;

	// Token: 0x040075C8 RID: 30152
	private Shader m_MultiSampleBlendShader;

	// Token: 0x040075C9 RID: 30153
	private Shader m_BlendShader;

	// Token: 0x040075CA RID: 30154
	private Shader m_HighlightShader;

	// Token: 0x040075CB RID: 30155
	private Shader m_UnlitColorShader;

	// Token: 0x040075CC RID: 30156
	private Shader m_UnlitGreyShader;

	// Token: 0x040075CD RID: 30157
	private Shader m_UnlitLightGreyShader;

	// Token: 0x040075CE RID: 30158
	private Shader m_UnlitDarkGreyShader;

	// Token: 0x040075CF RID: 30159
	private Shader m_UnlitBlackShader;

	// Token: 0x040075D0 RID: 30160
	private Shader m_UnlitWhiteShader;

	// Token: 0x040075D1 RID: 30161
	private RenderTexture m_CameraTexture;

	// Token: 0x040075D2 RID: 30162
	private Camera m_Camera;

	// Token: 0x040075D3 RID: 30163
	private float m_CameraOrthoSize;

	// Token: 0x040075D4 RID: 30164
	private Map<Renderer, bool> m_VisibilityStates;

	// Token: 0x040075D5 RID: 30165
	private Map<Transform, Vector3> m_ObjectsOrginalPosition;

	// Token: 0x040075D6 RID: 30166
	private int m_RenderSizeX = 200;

	// Token: 0x040075D7 RID: 30167
	private int m_RenderSizeY = 200;

	// Token: 0x040075D8 RID: 30168
	private bool m_Initialized;

	// Token: 0x040075D9 RID: 30169
	private static float s_offset = -2000f;

	// Token: 0x040075DA RID: 30170
	private Material m_Material;

	// Token: 0x040075DB RID: 30171
	private Material m_MultiSampleMaterial;

	// Token: 0x040075DC RID: 30172
	private Material m_MultiSampleBlendMaterial;

	// Token: 0x040075DD RID: 30173
	private Material m_BlendMaterial;
}
