using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000A8E RID: 2702
public class ShaderPreCompiler : MonoBehaviour
{
	// Token: 0x0600908D RID: 37005 RVA: 0x002EE4F8 File Offset: 0x002EC6F8
	private void Start()
	{
		if (GraphicsManager.Get().isVeryLowQualityDevice())
		{
			Debug.Log("ShaderPreCompiler: Disabled, very low quality mode");
			return;
		}
		if (GraphicsManager.Get().RenderQualityLevel != GraphicsQuality.Low)
		{
			base.StartCoroutine(this.WarmupShaders(this.m_StartupCompileShaders));
		}
		SceneMgr.Get().RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.WarmupSceneChangeShader));
		this.AddShader(this.m_GoldenUberShader.name, this.m_GoldenUberShader);
		foreach (Shader shader in this.m_StartupCompileShaders)
		{
			if (!(shader == null))
			{
				this.AddShader(shader.name, shader);
			}
		}
		foreach (Shader shader2 in this.m_SceneChangeCompileShaders)
		{
			if (!(shader2 == null))
			{
				this.AddShader(shader2.name, shader2);
			}
		}
	}

	// Token: 0x0600908E RID: 37006 RVA: 0x002EE5C8 File Offset: 0x002EC7C8
	public static Shader GetShader(string shaderName)
	{
		Shader shader;
		if (ShaderPreCompiler.s_shaderCache.TryGetValue(shaderName, out shader))
		{
			return shader;
		}
		shader = Shader.Find(shaderName);
		if (shader != null)
		{
			ShaderPreCompiler.s_shaderCache.Add(shaderName, shader);
		}
		return shader;
	}

	// Token: 0x0600908F RID: 37007 RVA: 0x002EE603 File Offset: 0x002EC803
	private void AddShader(string shaderName, Shader shader)
	{
		if (ShaderPreCompiler.s_shaderCache.ContainsKey(shaderName))
		{
			return;
		}
		ShaderPreCompiler.s_shaderCache.Add(shaderName, shader);
	}

	// Token: 0x06009090 RID: 37008 RVA: 0x002EE620 File Offset: 0x002EC820
	private void WarmupSceneChangeShader(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if ((SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY || SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER || SceneMgr.Get().IsInTavernBrawlMode()) && Network.ShouldBeConnectedToAurora())
		{
			base.StartCoroutine(this.WarmupGoldenUberShader());
			this.PremiumShadersCompiled = true;
		}
		if (prevMode != SceneMgr.Mode.HUB)
		{
			return;
		}
		if (this.SceneChangeShadersCompiled)
		{
			return;
		}
		this.SceneChangeShadersCompiled = true;
		if (GraphicsManager.Get().RenderQualityLevel != GraphicsQuality.Low)
		{
			base.StartCoroutine(this.WarmupShaders(this.m_SceneChangeCompileShaders));
		}
		if (this.SceneChangeShadersCompiled && this.PremiumShadersCompiled)
		{
			SceneMgr.Get().UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.WarmupSceneChangeShader));
		}
	}

	// Token: 0x06009091 RID: 37009 RVA: 0x002EE6C9 File Offset: 0x002EC8C9
	private IEnumerator WarmupGoldenUberShader()
	{
		float totalTime = 0f;
		foreach (string kw in this.GOLDEN_UBER_KEYWORDS1)
		{
			foreach (string text in this.GOLDEN_UBER_KEYWORDS2)
			{
				ShaderVariantCollection shaderVariantCollection = new ShaderVariantCollection();
				shaderVariantCollection.Add(new ShaderVariantCollection.ShaderVariant
				{
					shader = this.m_GoldenUberShader,
					keywords = new string[]
					{
						kw,
						text
					}
				});
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				shaderVariantCollection.WarmUp();
				float realtimeSinceStartup2 = Time.realtimeSinceStartup;
				totalTime += realtimeSinceStartup2 - realtimeSinceStartup;
				Log.Graphics.Print(string.Format("Golden Uber Shader Compile: {0} Keywords: {1}, {2} ({3}s)", new object[]
				{
					this.m_GoldenUberShader.name,
					kw,
					text,
					realtimeSinceStartup2 - realtimeSinceStartup
				}), Array.Empty<object>());
				yield return null;
			}
			string[] array2 = null;
			kw = null;
		}
		string[] array = null;
		Log.Graphics.Print("Profiling Shader Warmup: " + totalTime, Array.Empty<object>());
		yield break;
	}

	// Token: 0x06009092 RID: 37010 RVA: 0x002EE6D8 File Offset: 0x002EC8D8
	private IEnumerator WarmupShaders(Shader[] shaders)
	{
		float totalTime = 0f;
		foreach (Shader shader in shaders)
		{
			if (!(shader == null))
			{
				ShaderVariantCollection shaderVariantCollection = new ShaderVariantCollection();
				shaderVariantCollection.Add(new ShaderVariantCollection.ShaderVariant
				{
					shader = shader
				});
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				shaderVariantCollection.WarmUp();
				float realtimeSinceStartup2 = Time.realtimeSinceStartup;
				totalTime += realtimeSinceStartup2 - realtimeSinceStartup;
				Log.Graphics.Print(string.Format("Shader Compile: {0} ({1}s)", shader.name, realtimeSinceStartup2 - realtimeSinceStartup), Array.Empty<object>());
				yield return null;
			}
		}
		Shader[] array = null;
		yield break;
	}

	// Token: 0x06009093 RID: 37011 RVA: 0x002EE6E8 File Offset: 0x002EC8E8
	private GameObject CreateMesh(string name)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = name;
		gameObject.transform.parent = base.gameObject.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.AddComponent<MeshFilter>();
		gameObject.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh();
		mesh.vertices = this.MESH_VERTS;
		mesh.uv = this.MESH_UVS;
		mesh.normals = this.MESH_NORMALS;
		mesh.tangents = this.MESH_TANGENTS;
		mesh.triangles = this.MESH_TRIANGLES;
		gameObject.GetComponent<MeshFilter>().mesh = mesh;
		return gameObject;
	}

	// Token: 0x06009094 RID: 37012 RVA: 0x002EE7A3 File Offset: 0x002EC9A3
	private Material CreateMaterial(string name, Shader shader)
	{
		return new Material(shader)
		{
			name = name
		};
	}

	// Token: 0x06009095 RID: 37013 RVA: 0x002EE7B4 File Offset: 0x002EC9B4
	public ShaderPreCompiler()
	{
		int[] array = new int[3];
		array[0] = 2;
		array[1] = 1;
		this.MESH_TRIANGLES = array;
		base..ctor();
	}

	// Token: 0x04007957 RID: 31063
	private readonly string[] GOLDEN_UBER_KEYWORDS1 = new string[]
	{
		"FX3_ADDBLEND",
		"FX3_ALPHABLEND"
	};

	// Token: 0x04007958 RID: 31064
	private readonly string[] GOLDEN_UBER_KEYWORDS2 = new string[]
	{
		"LAYER3",
		"FX3_FLOWMAP",
		"LAYER4"
	};

	// Token: 0x04007959 RID: 31065
	private readonly Vector3[] MESH_VERTS = new Vector3[]
	{
		Vector3.zero,
		Vector3.zero,
		Vector3.zero
	};

	// Token: 0x0400795A RID: 31066
	private readonly Vector2[] MESH_UVS = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f)
	};

	// Token: 0x0400795B RID: 31067
	private readonly Vector3[] MESH_NORMALS = new Vector3[]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	// Token: 0x0400795C RID: 31068
	private readonly Vector4[] MESH_TANGENTS = new Vector4[]
	{
		new Vector4(1f, 0f, 0f, 0f),
		new Vector4(1f, 0f, 0f, 0f),
		new Vector4(1f, 0f, 0f, 0f)
	};

	// Token: 0x0400795D RID: 31069
	private readonly int[] MESH_TRIANGLES;

	// Token: 0x0400795E RID: 31070
	public Shader m_GoldenUberShader;

	// Token: 0x0400795F RID: 31071
	public Shader[] m_StartupCompileShaders;

	// Token: 0x04007960 RID: 31072
	public Shader[] m_SceneChangeCompileShaders;

	// Token: 0x04007961 RID: 31073
	protected static Map<string, Shader> s_shaderCache = new Map<string, Shader>();

	// Token: 0x04007962 RID: 31074
	private bool SceneChangeShadersCompiled;

	// Token: 0x04007963 RID: 31075
	private bool PremiumShadersCompiled;
}
