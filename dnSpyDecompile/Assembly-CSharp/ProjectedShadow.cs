using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A64 RID: 2660
public class ProjectedShadow : MonoBehaviour
{
	// Token: 0x170007FD RID: 2045
	// (get) Token: 0x06008ED3 RID: 36563 RVA: 0x002E1AD0 File Offset: 0x002DFCD0
	protected Material ShadowMaterial
	{
		get
		{
			if (this.m_ShadowMaterial == null)
			{
				this.m_ShadowMaterial = new Material(this.m_ShadowShader);
				SceneUtils.SetHideFlags(this.m_ShadowMaterial, HideFlags.DontSave);
				this.m_ShadowMaterial.SetTexture("_Ramp", this.m_ShadowFalloffRamp);
				this.m_ShadowMaterial.SetTexture("_MainTex", this.m_ShadowTexture);
				this.m_ShadowMaterial.SetColor("_Color", ProjectedShadow.s_ShadowColor);
				this.m_ShadowMaterial.SetTexture("_Edge", this.m_EdgeFalloffTexture);
			}
			return this.m_ShadowMaterial;
		}
	}

	// Token: 0x170007FE RID: 2046
	// (get) Token: 0x06008ED4 RID: 36564 RVA: 0x002E1B68 File Offset: 0x002DFD68
	protected Material ContactShadowMaterial
	{
		get
		{
			if (this.m_ContactShadowMaterial == null)
			{
				this.m_ContactShadowMaterial = new Material(this.m_ContactShadowShader);
				this.m_ContactShadowMaterial.SetFloat("_Intensity", 3.5f);
				this.m_ContactShadowMaterial.SetColor("_Color", ProjectedShadow.s_ShadowColor);
				SceneUtils.SetHideFlags(this.m_ContactShadowMaterial, HideFlags.DontSave);
			}
			return this.m_ContactShadowMaterial;
		}
	}

	// Token: 0x170007FF RID: 2047
	// (get) Token: 0x06008ED5 RID: 36565 RVA: 0x002E1BD1 File Offset: 0x002DFDD1
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

	// Token: 0x06008ED6 RID: 36566 RVA: 0x002E1C08 File Offset: 0x002DFE08
	protected void Start()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null && graphicsManager.RealtimeShadows && !this.m_enabledAlongsideRealtimeShadows)
		{
			base.enabled = false;
		}
		if (this.m_ShadowShader == null)
		{
			this.m_ShadowShader = ShaderUtils.FindShader("Custom/ProjectedShadow");
		}
		if (!this.m_ShadowShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/ProjectedShadow");
			base.enabled = false;
		}
		if (this.m_ContactShadowShader == null)
		{
			this.m_ContactShadowShader = ShaderUtils.FindShader("Custom/ContactShadow");
		}
		if (!this.m_ContactShadowShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/ContactShadow");
			base.enabled = false;
		}
		if (this.m_ShadowFalloffRamp == null)
		{
			this.m_ShadowFalloffRamp = (Resources.Load("Textures/ProjectedShadowRamp") as Texture2D);
		}
		if (!this.m_ShadowFalloffRamp)
		{
			Debug.LogError("Failed to load Projected Shadow Ramp: Textures/ProjectedShadowRamp");
			base.enabled = false;
		}
		if (this.m_EdgeFalloffTexture == null)
		{
			this.m_EdgeFalloffTexture = (Resources.Load("Textures/ProjectedShadowEdgeAlpha") as Texture2D);
		}
		if (!this.m_EdgeFalloffTexture)
		{
			Debug.LogError("Failed to load Projected Shadow Edge Falloff Texture: Textures/ProjectedShadowEdgeAlpha");
			base.enabled = false;
		}
		if (this.m_MultiSampleShader == null)
		{
			this.m_MultiSampleShader = ShaderUtils.FindShader("Custom/Selection/HighlightMultiSample");
		}
		if (!this.m_MultiSampleShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/Selection/HighlightMultiSample");
			base.enabled = false;
		}
		this.m_UnlitWhiteShader = ShaderUtils.FindShader("Custom/Unlit/Color/White");
		if (!this.m_UnlitWhiteShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/Unlit/Color/White");
		}
		this.m_UnlitLightGreyShader = ShaderUtils.FindShader("Custom/Unlit/Color/LightGrey");
		if (!this.m_UnlitLightGreyShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/Unlit/Color/LightGrey");
		}
		this.m_UnlitDarkGreyShader = ShaderUtils.FindShader("Custom/Unlit/Color/DarkGrey");
		if (!this.m_UnlitDarkGreyShader)
		{
			Debug.LogError("Failed to load Projected Shadow Shader: Custom/Unlit/Color/DarkGrey");
		}
		if (Board.Get() != null)
		{
			base.StartCoroutine(this.AssignBoardHeight_WaitForBoardStandardGameLoaded());
		}
		Actor component = base.GetComponent<Actor>();
		if (component != null)
		{
			this.m_RootObject = component.GetRootObject();
		}
		else
		{
			GameObject gameObject = SceneUtils.FindChildBySubstring(base.gameObject, "RootObject");
			if (gameObject != null)
			{
				this.m_RootObject = gameObject;
			}
			else
			{
				this.m_RootObject = base.gameObject;
			}
		}
		this.m_ShadowMaterial = this.ShadowMaterial;
	}

	// Token: 0x06008ED7 RID: 36567 RVA: 0x002E1E53 File Offset: 0x002E0053
	private IEnumerator AssignBoardHeight_WaitForBoardStandardGameLoaded()
	{
		SceneMgr sceneMgr;
		if (!HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			yield break;
		}
		while (sceneMgr.GetMode() == SceneMgr.Mode.GAMEPLAY && Gameplay.Get().GetBoardLayout() == null)
		{
			yield return null;
		}
		if (sceneMgr.GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			Transform transform = Board.Get().FindBone("CenterPointBone");
			if (transform != null)
			{
				this.m_BoardHeight = transform.position.y;
				this.m_HasBoardHeight = true;
			}
		}
		yield break;
	}

	// Token: 0x06008ED8 RID: 36568 RVA: 0x002E1E64 File Offset: 0x002E0064
	protected void LateUpdate()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null && graphicsManager.RealtimeShadows && !this.m_enabledAlongsideRealtimeShadows)
		{
			base.enabled = false;
			return;
		}
		this.Render();
		if (this.m_ContactShadow)
		{
			this.RenderContactShadow();
		}
	}

	// Token: 0x06008ED9 RID: 36569 RVA: 0x002E1EA8 File Offset: 0x002E00A8
	private void OnDisable()
	{
		if (this.m_Projector != null)
		{
			this.m_Projector.enabled = false;
		}
		if (this.m_PlaneMesh)
		{
			UnityEngine.Object.DestroyImmediate(this.m_PlaneMesh);
			UnityEngine.Object.DestroyImmediate(this.m_PlaneGameObject.GetComponent<MeshFilter>().mesh);
			this.m_PlaneGameObject.GetComponent<MeshFilter>().mesh = null;
			this.m_PlaneMesh = null;
		}
		if (this.m_PlaneGameObject)
		{
			UnityEngine.Object.DestroyImmediate(this.m_PlaneGameObject);
			this.m_PlaneGameObject = null;
		}
		if (RenderTexture.active == this.m_ShadowTexture || RenderTexture.active == this.m_ContactShadowTexture)
		{
			RenderTexture.active = null;
		}
		if (this.m_ShadowTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_ShadowTexture);
			this.m_ShadowTexture = null;
		}
		if (this.m_ContactShadowTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_ContactShadowTexture);
			this.m_ContactShadowTexture = null;
		}
	}

	// Token: 0x06008EDA RID: 36570 RVA: 0x002E1FA8 File Offset: 0x002E01A8
	protected void OnDestroy()
	{
		if (this.m_ContactShadowMaterial)
		{
			UnityEngine.Object.Destroy(this.m_ContactShadowMaterial);
		}
		if (this.m_ShadowMaterial)
		{
			UnityEngine.Object.Destroy(this.m_ShadowMaterial);
		}
		if (this.m_MultiSampleMaterial)
		{
			UnityEngine.Object.Destroy(this.m_MultiSampleMaterial);
		}
		if (this.m_Camera)
		{
			UnityEngine.Object.Destroy(this.m_Camera.gameObject);
		}
		if (this.m_ProjectorGameObject)
		{
			UnityEngine.Object.Destroy(this.m_ProjectorGameObject);
		}
		if (this.m_ShadowTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_ShadowTexture);
			this.m_ShadowTexture = null;
		}
		if (this.m_ContactShadowTexture)
		{
			RenderTextureTracker.Get().DestroyRenderTexture(this.m_ContactShadowTexture);
			this.m_ContactShadowTexture = null;
		}
		if (this.m_PlaneMesh)
		{
			UnityEngine.Object.DestroyImmediate(this.m_PlaneMesh);
			UnityEngine.Object.DestroyImmediate(this.m_PlaneGameObject.GetComponent<MeshFilter>().mesh);
			this.m_PlaneGameObject.GetComponent<MeshFilter>().mesh = null;
			this.m_PlaneMesh = null;
		}
		if (this.m_PlaneGameObject)
		{
			UnityEngine.Object.DestroyImmediate(this.m_PlaneGameObject);
			this.m_PlaneGameObject = null;
		}
	}

	// Token: 0x06008EDB RID: 36571 RVA: 0x002E20E0 File Offset: 0x002E02E0
	private void OnDrawGizmos()
	{
		float num = this.m_ShadowProjectorSize * TransformUtil.ComputeWorldScale(base.transform).x * 2f;
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = new Color(0.6f, 0.15f, 0.6f);
		if (this.m_ContactShadow)
		{
			Gizmos.DrawWireCube(this.m_ContactOffset, new Vector3(num, 0f, num));
		}
		else
		{
			Gizmos.DrawWireCube(Vector3.zero, new Vector3(num, 0f, num));
		}
		Gizmos.matrix = Matrix4x4.identity;
	}

	// Token: 0x06008EDC RID: 36572 RVA: 0x002E2178 File Offset: 0x002E0378
	public void Render()
	{
		if (!this.m_ShadowEnabled || (this.m_RootObject && !this.m_RootObject.activeSelf))
		{
			if (this.m_Projector && this.m_Projector.enabled)
			{
				this.m_Projector.enabled = false;
			}
			if (this.m_PlaneGameObject)
			{
				this.m_PlaneGameObject.SetActive(false);
			}
			return;
		}
		float x = TransformUtil.ComputeWorldScale(base.transform).x;
		this.m_AdjustedShadowProjectorSize = this.m_ShadowProjectorSize * x;
		if (this.m_Projector == null)
		{
			this.CreateProjector();
		}
		if (this.m_Camera == null)
		{
			this.CreateCamera();
		}
		float y = base.transform.position.y;
		float num = this.m_HasBoardHeight ? this.m_BoardHeight : (y - Mathf.Max(0f, this.m_AutoDisableHeight) - float.Epsilon);
		float num2 = (y - num) * 0.3f;
		this.m_AdjustedShadowProjectorSize += Mathf.Lerp(0f, 0.5f, num2 * 0.5f);
		if (this.m_ContactShadow)
		{
			float num3 = num + 0.08f;
			if (num2 < num3)
			{
				if (this.m_PlaneGameObject == null)
				{
					this.m_isDirtyContactShadow = true;
				}
				else if (!this.m_PlaneGameObject.activeSelf)
				{
					this.m_isDirtyContactShadow = true;
				}
				float value = Mathf.Clamp((num3 - num2) / 0.08f, 0f, 1f);
				if (this.m_ContactShadowTexture && this.m_PlaneGameObject)
				{
					Renderer component = this.m_PlaneGameObject.GetComponent<Renderer>();
					Material material = component ? component.GetSharedMaterial() : null;
					if (material)
					{
						material.mainTexture = this.m_ContactShadowTexture;
						material.color = ProjectedShadow.s_ShadowColor;
						material.SetFloat("_Alpha", value);
					}
				}
			}
			else if (this.m_PlaneGameObject != null)
			{
				this.m_PlaneGameObject.SetActive(false);
			}
		}
		if (num2 < this.m_AutoDisableHeight && this.m_AutoBoardHeightDisable)
		{
			this.m_Projector.enabled = false;
			UnityEngine.Object.DestroyImmediate(this.m_ShadowTexture);
			this.m_ShadowTexture = null;
			return;
		}
		this.m_Projector.enabled = true;
		float num4 = 0f;
		if (base.transform.parent != null)
		{
			num4 = Mathf.Lerp(-0.7f, 1.8f, base.transform.parent.position.x / 17f * -1f) * num2;
		}
		Vector3 position = new Vector3(base.transform.position.x - num4 - num2 * 0.25f, base.transform.position.y, base.transform.position.z - num2 * 0.8f);
		this.m_ProjectorTransform.position = position;
		this.m_ProjectorTransform.Translate(this.m_ProjectionOffset);
		if (!this.m_ContinuousRendering)
		{
			Quaternion rotation = base.transform.rotation;
			float num5 = (1f - rotation.z) * 0.5f + 0.5f;
			float num6 = rotation.x * 0.5f;
			this.m_Projector.aspectRatio = num5 - num6;
			this.m_Projector.orthographicSize = this.m_AdjustedShadowProjectorSize + num6;
			this.m_ProjectorTransform.rotation = Quaternion.identity;
			this.m_ProjectorTransform.Rotate(90f, rotation.eulerAngles.y, 0f);
		}
		else
		{
			this.m_ProjectorTransform.rotation = Quaternion.identity;
			this.m_ProjectorTransform.Rotate(90f, 0f, 0f);
			this.m_Projector.orthographicSize = this.m_AdjustedShadowProjectorSize;
		}
		int num7 = 64;
		if (this.m_ShadowTexture == null)
		{
			this.m_ShadowTexture = RenderTextureTracker.Get().CreateNewTexture(num7, num7, 32, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
			this.m_ShadowTexture.wrapMode = TextureWrapMode.Clamp;
			this.RenderShadowMask();
			return;
		}
		if (this.m_ContinuousRendering || !this.m_ShadowTexture.IsCreated())
		{
			this.RenderShadowMask();
		}
	}

	// Token: 0x06008EDD RID: 36573 RVA: 0x002E259D File Offset: 0x002E079D
	public static void SetShadowColor(Color color)
	{
		ProjectedShadow.s_ShadowColor = color;
	}

	// Token: 0x06008EDE RID: 36574 RVA: 0x002E25A5 File Offset: 0x002E07A5
	public void EnableShadow()
	{
		this.m_ShadowEnabled = true;
	}

	// Token: 0x06008EDF RID: 36575 RVA: 0x002E25B0 File Offset: 0x002E07B0
	public void EnableShadow(float FadeInTime)
	{
		this.m_ShadowEnabled = true;
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			0,
			"to",
			1,
			"time",
			FadeInTime,
			"easetype",
			iTween.EaseType.easeInCubic,
			"onupdate",
			"UpdateShadowColor",
			"onupdatetarget",
			base.gameObject,
			"name",
			"ProjectedShadowFade"
		});
		iTween.StopByName(base.gameObject, "ProjectedShadowFade");
		iTween.ValueTo(base.gameObject, args);
	}

	// Token: 0x06008EE0 RID: 36576 RVA: 0x002E2667 File Offset: 0x002E0867
	public void DisableShadow()
	{
		this.DisableShadowProjector();
	}

	// Token: 0x06008EE1 RID: 36577 RVA: 0x002E2670 File Offset: 0x002E0870
	public void DisableShadow(float FadeOutTime)
	{
		if (this.m_Projector == null || !this.m_ShadowEnabled)
		{
			return;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			1,
			"to",
			0,
			"time",
			FadeOutTime,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"onupdate",
			"UpdateShadowColor",
			"onupdatetarget",
			base.gameObject,
			"name",
			"ProjectedShadowFade",
			"oncomplete",
			"DisableShadowProjector"
		});
		iTween.StopByName(base.gameObject, "ProjectedShadowFade");
		iTween.ValueTo(base.gameObject, args);
	}

	// Token: 0x06008EE2 RID: 36578 RVA: 0x002E2749 File Offset: 0x002E0949
	public void UpdateContactShadow(Spell spell, SpellStateType prevStateType, object userData)
	{
		this.UpdateContactShadow();
	}

	// Token: 0x06008EE3 RID: 36579 RVA: 0x002E2749 File Offset: 0x002E0949
	public void UpdateContactShadow(Spell spell, object userData)
	{
		this.UpdateContactShadow();
	}

	// Token: 0x06008EE4 RID: 36580 RVA: 0x002E2749 File Offset: 0x002E0949
	public void UpdateContactShadow(Spell spell)
	{
		this.UpdateContactShadow();
	}

	// Token: 0x06008EE5 RID: 36581 RVA: 0x002E2751 File Offset: 0x002E0951
	public void UpdateContactShadow()
	{
		if (!this.m_ContactShadow)
		{
			return;
		}
		this.m_isDirtyContactShadow = true;
	}

	// Token: 0x06008EE6 RID: 36582 RVA: 0x002E2763 File Offset: 0x002E0963
	private void DisableShadowProjector()
	{
		if (this.m_Projector != null)
		{
			this.m_Projector.enabled = false;
		}
		this.m_ShadowEnabled = false;
	}

	// Token: 0x06008EE7 RID: 36583 RVA: 0x002E2788 File Offset: 0x002E0988
	private void UpdateShadowColor(float val)
	{
		if (this.m_Projector == null || this.m_Projector.material == null)
		{
			return;
		}
		Color value = Color.Lerp(new Color(0.5f, 0.5f, 0.5f, 0.5f), ProjectedShadow.s_ShadowColor, val);
		this.m_Projector.material.SetColor("_Color", value);
	}

	// Token: 0x06008EE8 RID: 36584 RVA: 0x002E27F4 File Offset: 0x002E09F4
	private void RenderShadowMask()
	{
		this.m_ShadowTexture.DiscardContents();
		this.m_Camera.depth = Camera.main.depth - 3f;
		this.m_Camera.clearFlags = CameraClearFlags.Color;
		Vector3 position = base.transform.position;
		Vector3 localScale = base.transform.localScale;
		ProjectedShadow.s_offset -= 10f;
		if (ProjectedShadow.s_offset < -19000f)
		{
			ProjectedShadow.s_offset = -12000f;
		}
		Vector3 position2 = Vector3.left * ProjectedShadow.s_offset;
		base.transform.position = position2;
		this.m_Camera.transform.position = position2;
		this.m_Camera.transform.rotation = Quaternion.identity;
		this.m_Camera.transform.Rotate(90f, 0f, 0f);
		RenderTexture temporary = RenderTexture.GetTemporary(80, 80);
		RenderTexture temporary2 = RenderTexture.GetTemporary(80, 80);
		this.m_Camera.targetTexture = temporary;
		float x = TransformUtil.ComputeWorldScale(base.transform).x;
		this.m_Camera.orthographicSize = this.m_ShadowProjectorSize * x - 0.11f - 0.05f;
		this.m_Camera.RenderWithShader(this.m_UnlitWhiteShader, "Highlight");
		this.Sample(temporary, temporary2, 0.6f);
		this.Sample(temporary2, this.m_ShadowTexture, 0.8f);
		this.ShadowMaterial.SetTexture("_MainTex", this.m_ShadowTexture);
		this.ShadowMaterial.SetColor("_Color", ProjectedShadow.s_ShadowColor);
		base.transform.position = position;
		base.transform.localScale = localScale;
		RenderTexture.ReleaseTemporary(temporary);
		RenderTexture.ReleaseTemporary(temporary2);
	}

	// Token: 0x06008EE9 RID: 36585 RVA: 0x002E29AC File Offset: 0x002E0BAC
	private IEnumerator DelayRenderContactShadow()
	{
		yield return null;
		this.m_isDirtyContactShadow = true;
		yield break;
	}

	// Token: 0x06008EEA RID: 36586 RVA: 0x002E29BC File Offset: 0x002E0BBC
	private void RenderContactShadow()
	{
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null && graphicsManager.RealtimeShadows && !this.m_enabledAlongsideRealtimeShadows)
		{
			base.enabled = false;
		}
		if (this.m_ContactShadowTexture != null && !this.m_isDirtyContactShadow && this.m_ContactShadowTexture.IsCreated())
		{
			return;
		}
		float x = TransformUtil.ComputeWorldScale(base.transform).x;
		this.m_AdjustedShadowProjectorSize = this.m_ShadowProjectorSize * x;
		if (this.m_Camera == null)
		{
			this.CreateCamera();
		}
		if (this.m_PlaneGameObject == null)
		{
			this.CreateRenderPlane();
		}
		this.m_PlaneGameObject.SetActive(true);
		if (this.m_ContactShadowTexture == null)
		{
			this.m_ContactShadowTexture = RenderTextureTracker.Get().CreateNewTexture(80, 80, 32, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
		}
		Quaternion localRotation = base.transform.localRotation;
		Vector3 position = base.transform.position;
		Vector3 localScale = base.transform.localScale;
		ProjectedShadow.s_offset -= 10f;
		if (ProjectedShadow.s_offset < -19000f)
		{
			ProjectedShadow.s_offset = -12000f;
		}
		Vector3 position2 = Vector3.left * ProjectedShadow.s_offset;
		base.transform.position = position2;
		base.transform.rotation = Quaternion.identity;
		this.SetWorldScale(base.transform, Vector3.one);
		this.m_Camera.transform.position = position2;
		this.m_Camera.transform.rotation = Quaternion.identity;
		this.m_Camera.transform.Rotate(90f, 0f, 0f);
		RenderTexture temporary = RenderTexture.GetTemporary(80, 80);
		this.m_Camera.depth = Camera.main.depth - 3f;
		this.m_Camera.clearFlags = CameraClearFlags.Color;
		this.m_Camera.targetTexture = temporary;
		this.m_Camera.orthographicSize = this.m_ShadowProjectorSize - 0.11f - 0.05f;
		this.m_Camera.RenderWithShader(this.m_UnlitDarkGreyShader, "Highlight");
		this.m_ContactShadowTexture.DiscardContents();
		this.Sample(temporary, this.m_ContactShadowTexture, 0.6f);
		base.transform.localRotation = localRotation;
		base.transform.position = position;
		base.transform.localScale = localScale;
		this.m_PlaneGameObject.GetComponent<Renderer>().GetSharedMaterial().mainTexture = this.m_ContactShadowTexture;
		this.m_isDirtyContactShadow = false;
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x06008EEB RID: 36587 RVA: 0x002E2C34 File Offset: 0x002E0E34
	private void Sample(RenderTexture source, RenderTexture dest, float off)
	{
		Graphics.BlitMultiTap(source, dest, this.MultiSampleMaterial, new Vector2[]
		{
			new Vector2(-off, -off),
			new Vector2(-off, off),
			new Vector2(off, off),
			new Vector2(off, -off)
		});
	}

	// Token: 0x06008EEC RID: 36588 RVA: 0x002E2C90 File Offset: 0x002E0E90
	private void CreateProjector()
	{
		if (this.m_Projector != null)
		{
			UnityEngine.Object.Destroy(this.m_Projector);
			this.m_Projector = null;
		}
		if (this.m_ProjectorGameObject != null)
		{
			UnityEngine.Object.Destroy(this.m_ProjectorGameObject);
			this.m_ProjectorGameObject = null;
			this.m_ProjectorTransform = null;
		}
		this.m_ProjectorGameObject = new GameObject(string.Format("{0}_{1}", base.name, "ShadowProjector"));
		this.m_Projector = this.m_ProjectorGameObject.AddComponent<Projector>();
		this.m_ProjectorTransform = this.m_ProjectorGameObject.transform;
		this.m_ProjectorTransform.Rotate(90f, 0f, 0f);
		if (this.m_RootObject != null)
		{
			this.m_ProjectorTransform.parent = this.m_RootObject.transform;
		}
		this.m_Projector.nearClipPlane = 0f;
		this.m_Projector.farClipPlane = this.m_ProjectionFarClip;
		this.m_Projector.orthographic = true;
		this.m_Projector.orthographicSize = this.m_AdjustedShadowProjectorSize;
		SceneUtils.SetHideFlags(this.m_Projector, HideFlags.HideAndDontSave);
		this.m_Projector.material = this.m_ShadowMaterial;
	}

	// Token: 0x06008EED RID: 36589 RVA: 0x002E2DC0 File Offset: 0x002E0FC0
	private void CreateCamera()
	{
		if (this.m_Camera != null)
		{
			UnityEngine.Object.Destroy(this.m_Camera);
		}
		GameObject gameObject = new GameObject();
		this.m_Camera = gameObject.AddComponent<Camera>();
		gameObject.name = base.name + "_ShadowCamera";
		SceneUtils.SetHideFlags(gameObject, HideFlags.HideAndDontSave);
		this.m_Camera.orthographic = true;
		this.m_Camera.orthographicSize = this.m_AdjustedShadowProjectorSize;
		this.m_Camera.transform.position = base.transform.position;
		this.m_Camera.transform.rotation = base.transform.rotation;
		this.m_Camera.transform.Rotate(90f, 0f, 0f);
		if (this.m_RootObject != null)
		{
			this.m_Camera.transform.parent = this.m_RootObject.transform;
		}
		this.m_Camera.nearClipPlane = -3f;
		this.m_Camera.farClipPlane = 3f;
		if (Camera.main != null)
		{
			this.m_Camera.depth = Camera.main.depth - 5f;
		}
		else
		{
			this.m_Camera.depth = -4f;
		}
		this.m_Camera.backgroundColor = Color.black;
		this.m_Camera.clearFlags = CameraClearFlags.Color;
		this.m_Camera.depthTextureMode = DepthTextureMode.None;
		this.m_Camera.renderingPath = RenderingPath.Forward;
		this.m_Camera.allowHDR = false;
		this.m_Camera.SetReplacementShader(this.m_UnlitWhiteShader, "Highlight");
		this.m_Camera.enabled = false;
	}

	// Token: 0x06008EEE RID: 36590 RVA: 0x002E2F70 File Offset: 0x002E1170
	private void CreateRenderPlane()
	{
		if (this.m_PlaneGameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_PlaneGameObject);
		}
		this.m_PlaneGameObject = new GameObject();
		this.m_PlaneGameObject.name = base.name + "_ContactShadowRenderPlane";
		if (this.m_RootObject != null)
		{
			this.m_PlaneGameObject.transform.parent = this.m_RootObject.transform;
		}
		this.m_PlaneGameObject.transform.localPosition = this.m_ContactOffset;
		this.m_PlaneGameObject.transform.localRotation = Quaternion.identity;
		this.m_PlaneGameObject.transform.localScale = new Vector3(0.98f, 1f, 0.98f);
		this.m_PlaneGameObject.AddComponent<MeshFilter>();
		this.m_PlaneGameObject.AddComponent<MeshRenderer>();
		SceneUtils.SetHideFlags(this.m_PlaneGameObject, HideFlags.HideAndDontSave);
		Mesh mesh = new Mesh();
		mesh.name = "ContactShadowMeshPlane";
		float shadowProjectorSize = this.m_ShadowProjectorSize;
		float shadowProjectorSize2 = this.m_ShadowProjectorSize;
		mesh.vertices = new Vector3[]
		{
			new Vector3(-shadowProjectorSize, 0f, -shadowProjectorSize2),
			new Vector3(shadowProjectorSize, 0f, -shadowProjectorSize2),
			new Vector3(-shadowProjectorSize, 0f, shadowProjectorSize2),
			new Vector3(shadowProjectorSize, 0f, shadowProjectorSize2)
		};
		mesh.uv = this.PLANE_UVS;
		mesh.normals = this.PLANE_NORMALS;
		mesh.triangles = this.PLANE_TRIANGLES;
		this.m_PlaneMesh = (this.m_PlaneGameObject.GetComponent<MeshFilter>().mesh = mesh);
		this.m_PlaneMesh.RecalculateBounds();
		this.m_ContactShadowMaterial = this.ContactShadowMaterial;
		this.m_ContactShadowMaterial.color = ProjectedShadow.s_ShadowColor;
		if (this.m_ContactShadowMaterial)
		{
			this.m_PlaneGameObject.GetComponent<Renderer>().SetSharedMaterial(this.m_ContactShadowMaterial);
		}
	}

	// Token: 0x06008EEF RID: 36591 RVA: 0x002E315C File Offset: 0x002E135C
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

	// Token: 0x0400772B RID: 30507
	private const int RENDER_SIZE = 64;

	// Token: 0x0400772C RID: 30508
	private const string SHADER_NAME = "Custom/ProjectedShadow";

	// Token: 0x0400772D RID: 30509
	private const string CONTACT_SHADER_NAME = "Custom/ContactShadow";

	// Token: 0x0400772E RID: 30510
	private const string SHADER_FALLOFF_RAMP = "Textures/ProjectedShadowRamp";

	// Token: 0x0400772F RID: 30511
	private const string EDGE_FALLOFF_TEXTURE = "Textures/ProjectedShadowEdgeAlpha";

	// Token: 0x04007730 RID: 30512
	private const string GAMEOBJECT_NAME_EXT = "ShadowProjector";

	// Token: 0x04007731 RID: 30513
	private const string UNLIT_WHITE_SHADER_NAME = "Custom/Unlit/Color/White";

	// Token: 0x04007732 RID: 30514
	private const string UNLIT_LIGHTGREY_SHADER_NAME = "Custom/Unlit/Color/LightGrey";

	// Token: 0x04007733 RID: 30515
	private const string UNLIT_DARKGREY_SHADER_NAME = "Custom/Unlit/Color/DarkGrey";

	// Token: 0x04007734 RID: 30516
	private const string MULTISAMPLE_SHADER_NAME = "Custom/Selection/HighlightMultiSample";

	// Token: 0x04007735 RID: 30517
	private const float NEARCLIP_PLANE = 0f;

	// Token: 0x04007736 RID: 30518
	private const float SHADOW_OFFSET_SCALE = 0.3f;

	// Token: 0x04007737 RID: 30519
	private const float RENDERMASK_OFFSET = 0.11f;

	// Token: 0x04007738 RID: 30520
	private const float RENDERMASK_BLUR = 0.6f;

	// Token: 0x04007739 RID: 30521
	private const float RENDERMASK_BLUR2 = 0.8f;

	// Token: 0x0400773A RID: 30522
	private const float CONTACT_SHADOW_SCALE = 0.98f;

	// Token: 0x0400773B RID: 30523
	private const float CONTACT_SHADOW_FADE_IN_HEIGHT = 0.08f;

	// Token: 0x0400773C RID: 30524
	private const float CONTACT_SHADOW_INTENSITY = 3.5f;

	// Token: 0x0400773D RID: 30525
	private const int CONTACT_SHADOW_RESOLUTION = 80;

	// Token: 0x0400773E RID: 30526
	private readonly Vector2[] PLANE_UVS = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f),
		new Vector2(1f, 1f)
	};

	// Token: 0x0400773F RID: 30527
	private readonly Vector3[] PLANE_NORMALS = new Vector3[]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	// Token: 0x04007740 RID: 30528
	private readonly int[] PLANE_TRIANGLES = new int[]
	{
		3,
		1,
		2,
		2,
		1,
		0
	};

	// Token: 0x04007741 RID: 30529
	public float m_ShadowProjectorSize = 1.5f;

	// Token: 0x04007742 RID: 30530
	public bool m_ShadowEnabled;

	// Token: 0x04007743 RID: 30531
	public bool m_AutoBoardHeightDisable;

	// Token: 0x04007744 RID: 30532
	public float m_AutoDisableHeight;

	// Token: 0x04007745 RID: 30533
	public bool m_ContinuousRendering;

	// Token: 0x04007746 RID: 30534
	public float m_ProjectionFarClip = 10f;

	// Token: 0x04007747 RID: 30535
	public Vector3 m_ProjectionOffset;

	// Token: 0x04007748 RID: 30536
	public bool m_ContactShadow;

	// Token: 0x04007749 RID: 30537
	public Vector3 m_ContactOffset = Vector3.zero;

	// Token: 0x0400774A RID: 30538
	public bool m_isDirtyContactShadow = true;

	// Token: 0x0400774B RID: 30539
	public bool m_enabledAlongsideRealtimeShadows;

	// Token: 0x0400774C RID: 30540
	private static float s_offset = -12000f;

	// Token: 0x0400774D RID: 30541
	private static Color s_ShadowColor = new Color(0.098f, 0.098f, 0.235f, 0.45f);

	// Token: 0x0400774E RID: 30542
	private GameObject m_RootObject;

	// Token: 0x0400774F RID: 30543
	private GameObject m_ProjectorGameObject;

	// Token: 0x04007750 RID: 30544
	private Transform m_ProjectorTransform;

	// Token: 0x04007751 RID: 30545
	private Projector m_Projector;

	// Token: 0x04007752 RID: 30546
	private Camera m_Camera;

	// Token: 0x04007753 RID: 30547
	private RenderTexture m_ShadowTexture;

	// Token: 0x04007754 RID: 30548
	private RenderTexture m_ContactShadowTexture;

	// Token: 0x04007755 RID: 30549
	private float m_AdjustedShadowProjectorSize = 1.5f;

	// Token: 0x04007756 RID: 30550
	private float m_BoardHeight = 0.2f;

	// Token: 0x04007757 RID: 30551
	private bool m_HasBoardHeight;

	// Token: 0x04007758 RID: 30552
	private Mesh m_PlaneMesh;

	// Token: 0x04007759 RID: 30553
	private GameObject m_PlaneGameObject;

	// Token: 0x0400775A RID: 30554
	private Texture2D m_ShadowFalloffRamp;

	// Token: 0x0400775B RID: 30555
	private Texture2D m_EdgeFalloffTexture;

	// Token: 0x0400775C RID: 30556
	private Shader m_ShadowShader;

	// Token: 0x0400775D RID: 30557
	private Shader m_UnlitWhiteShader;

	// Token: 0x0400775E RID: 30558
	private Shader m_UnlitDarkGreyShader;

	// Token: 0x0400775F RID: 30559
	private Shader m_UnlitLightGreyShader;

	// Token: 0x04007760 RID: 30560
	private Material m_ShadowMaterial;

	// Token: 0x04007761 RID: 30561
	private Shader m_ContactShadowShader;

	// Token: 0x04007762 RID: 30562
	private Material m_ContactShadowMaterial;

	// Token: 0x04007763 RID: 30563
	private Shader m_MultiSampleShader;

	// Token: 0x04007764 RID: 30564
	private Material m_MultiSampleMaterial;
}
