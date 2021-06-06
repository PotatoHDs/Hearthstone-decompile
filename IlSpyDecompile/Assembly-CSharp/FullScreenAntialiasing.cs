using UnityEngine;

public class FullScreenAntialiasing : MonoBehaviour
{
	public Shader m_FXAA_Shader;

	private Material m_FXAA_Material;

	protected Material FXAA_Material
	{
		get
		{
			if (m_FXAA_Material == null)
			{
				m_FXAA_Material = new Material(m_FXAA_Shader);
				SceneUtils.SetHideFlags(m_FXAA_Material, HideFlags.DontSave);
			}
			return m_FXAA_Material;
		}
	}

	private void Awake()
	{
		base.gameObject.GetComponent<Camera>().enabled = true;
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
	}

	private void Start()
	{
		if (m_FXAA_Shader == null || FXAA_Material == null)
		{
			base.enabled = false;
		}
		if (!m_FXAA_Shader.isSupported)
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
	}

	private void OnDisable()
	{
		if (m_FXAA_Material != null)
		{
			Object.Destroy(m_FXAA_Material);
		}
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, FXAA_Material);
	}
}
