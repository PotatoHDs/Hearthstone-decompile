using System.Collections;
using UnityEngine;

public class ShaderPreCompiler : MonoBehaviour
{
	private readonly string[] GOLDEN_UBER_KEYWORDS1 = new string[2] { "FX3_ADDBLEND", "FX3_ALPHABLEND" };

	private readonly string[] GOLDEN_UBER_KEYWORDS2 = new string[3] { "LAYER3", "FX3_FLOWMAP", "LAYER4" };

	private readonly Vector3[] MESH_VERTS = new Vector3[3]
	{
		Vector3.zero,
		Vector3.zero,
		Vector3.zero
	};

	private readonly Vector2[] MESH_UVS = new Vector2[3]
	{
		new Vector2(0f, 0f),
		new Vector2(1f, 0f),
		new Vector2(0f, 1f)
	};

	private readonly Vector3[] MESH_NORMALS = new Vector3[3]
	{
		Vector3.up,
		Vector3.up,
		Vector3.up
	};

	private readonly Vector4[] MESH_TANGENTS = new Vector4[3]
	{
		new Vector4(1f, 0f, 0f, 0f),
		new Vector4(1f, 0f, 0f, 0f),
		new Vector4(1f, 0f, 0f, 0f)
	};

	private readonly int[] MESH_TRIANGLES = new int[3] { 2, 1, 0 };

	public Shader m_GoldenUberShader;

	public Shader[] m_StartupCompileShaders;

	public Shader[] m_SceneChangeCompileShaders;

	protected static Map<string, Shader> s_shaderCache = new Map<string, Shader>();

	private bool SceneChangeShadersCompiled;

	private bool PremiumShadersCompiled;

	private void Start()
	{
		if (GraphicsManager.Get().isVeryLowQualityDevice())
		{
			Debug.Log("ShaderPreCompiler: Disabled, very low quality mode");
			return;
		}
		if (GraphicsManager.Get().RenderQualityLevel != 0)
		{
			StartCoroutine(WarmupShaders(m_StartupCompileShaders));
		}
		SceneMgr.Get().RegisterScenePreUnloadEvent(WarmupSceneChangeShader);
		AddShader(m_GoldenUberShader.name, m_GoldenUberShader);
		Shader[] startupCompileShaders = m_StartupCompileShaders;
		foreach (Shader shader in startupCompileShaders)
		{
			if (!(shader == null))
			{
				AddShader(shader.name, shader);
			}
		}
		startupCompileShaders = m_SceneChangeCompileShaders;
		foreach (Shader shader2 in startupCompileShaders)
		{
			if (!(shader2 == null))
			{
				AddShader(shader2.name, shader2);
			}
		}
	}

	public static Shader GetShader(string shaderName)
	{
		if (s_shaderCache.TryGetValue(shaderName, out var value))
		{
			return value;
		}
		value = Shader.Find(shaderName);
		if (value != null)
		{
			s_shaderCache.Add(shaderName, value);
		}
		return value;
	}

	private void AddShader(string shaderName, Shader shader)
	{
		if (!s_shaderCache.ContainsKey(shaderName))
		{
			s_shaderCache.Add(shaderName, shader);
		}
	}

	private void WarmupSceneChangeShader(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if ((SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY || SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER || SceneMgr.Get().IsInTavernBrawlMode()) && Network.ShouldBeConnectedToAurora())
		{
			StartCoroutine(WarmupGoldenUberShader());
			PremiumShadersCompiled = true;
		}
		if (prevMode == SceneMgr.Mode.HUB && !SceneChangeShadersCompiled)
		{
			SceneChangeShadersCompiled = true;
			if (GraphicsManager.Get().RenderQualityLevel != 0)
			{
				StartCoroutine(WarmupShaders(m_SceneChangeCompileShaders));
			}
			if (SceneChangeShadersCompiled && PremiumShadersCompiled)
			{
				SceneMgr.Get().UnregisterScenePreUnloadEvent(WarmupSceneChangeShader);
			}
		}
	}

	private IEnumerator WarmupGoldenUberShader()
	{
		float totalTime = 0f;
		string[] gOLDEN_UBER_KEYWORDS = GOLDEN_UBER_KEYWORDS1;
		foreach (string kw1 in gOLDEN_UBER_KEYWORDS)
		{
			string[] gOLDEN_UBER_KEYWORDS2 = GOLDEN_UBER_KEYWORDS2;
			foreach (string text in gOLDEN_UBER_KEYWORDS2)
			{
				ShaderVariantCollection shaderVariantCollection = new ShaderVariantCollection();
				ShaderVariantCollection.ShaderVariant variant = default(ShaderVariantCollection.ShaderVariant);
				variant.shader = m_GoldenUberShader;
				variant.keywords = new string[2] { kw1, text };
				shaderVariantCollection.Add(variant);
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				shaderVariantCollection.WarmUp();
				float realtimeSinceStartup2 = Time.realtimeSinceStartup;
				totalTime += realtimeSinceStartup2 - realtimeSinceStartup;
				Log.Graphics.Print($"Golden Uber Shader Compile: {m_GoldenUberShader.name} Keywords: {kw1}, {text} ({realtimeSinceStartup2 - realtimeSinceStartup}s)");
				yield return null;
			}
		}
		Log.Graphics.Print("Profiling Shader Warmup: " + totalTime);
	}

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
				Log.Graphics.Print($"Shader Compile: {shader.name} ({realtimeSinceStartup2 - realtimeSinceStartup}s)");
				yield return null;
			}
		}
	}

	private GameObject CreateMesh(string name)
	{
		GameObject obj = new GameObject();
		obj.name = name;
		obj.transform.parent = base.gameObject.transform;
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localRotation = Quaternion.identity;
		obj.transform.localScale = Vector3.one;
		obj.AddComponent<MeshFilter>();
		obj.AddComponent<MeshRenderer>();
		Mesh mesh = new Mesh
		{
			vertices = MESH_VERTS,
			uv = MESH_UVS,
			normals = MESH_NORMALS,
			tangents = MESH_TANGENTS,
			triangles = MESH_TRIANGLES
		};
		obj.GetComponent<MeshFilter>().mesh = mesh;
		return obj;
	}

	private Material CreateMaterial(string name, Shader shader)
	{
		return new Material(shader)
		{
			name = name
		};
	}
}
