using System;
using UnityEngine;

// Token: 0x02000A2F RID: 2607
public class FullScreenAntialiasing : MonoBehaviour
{
	// Token: 0x170007D0 RID: 2000
	// (get) Token: 0x06008C10 RID: 35856 RVA: 0x002CD70F File Offset: 0x002CB90F
	protected Material FXAA_Material
	{
		get
		{
			if (this.m_FXAA_Material == null)
			{
				this.m_FXAA_Material = new Material(this.m_FXAA_Shader);
				SceneUtils.SetHideFlags(this.m_FXAA_Material, HideFlags.DontSave);
			}
			return this.m_FXAA_Material;
		}
	}

	// Token: 0x06008C11 RID: 35857 RVA: 0x002CD743 File Offset: 0x002CB943
	private void Awake()
	{
		base.gameObject.GetComponent<Camera>().enabled = true;
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06008C12 RID: 35858 RVA: 0x002CD764 File Offset: 0x002CB964
	private void Start()
	{
		if (this.m_FXAA_Shader == null || this.FXAA_Material == null)
		{
			base.enabled = false;
		}
		if (!this.m_FXAA_Shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06008C13 RID: 35859 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x06008C14 RID: 35860 RVA: 0x002CD79D File Offset: 0x002CB99D
	private void OnDisable()
	{
		if (this.m_FXAA_Material != null)
		{
			UnityEngine.Object.Destroy(this.m_FXAA_Material);
		}
	}

	// Token: 0x06008C15 RID: 35861 RVA: 0x002CD7B8 File Offset: 0x002CB9B8
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, this.FXAA_Material);
	}

	// Token: 0x04007500 RID: 29952
	public Shader m_FXAA_Shader;

	// Token: 0x04007501 RID: 29953
	private Material m_FXAA_Material;
}
