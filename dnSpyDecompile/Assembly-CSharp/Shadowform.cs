using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200081D RID: 2077
public class Shadowform : SuperSpell
{
	// Token: 0x06006FE0 RID: 28640 RVA: 0x002417E0 File Offset: 0x0023F9E0
	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		this.OnSpellFinished();
		if (this.m_ShadowformMaterial == null)
		{
			return;
		}
		if (this.m_StartFXCoroutine != null)
		{
			base.StopCoroutine(this.m_StartFXCoroutine);
		}
		this.m_StartFXCoroutine = base.StartCoroutine(this.StartShadowformFX());
	}

	// Token: 0x06006FE1 RID: 28641 RVA: 0x0024182F File Offset: 0x0023FA2F
	private IEnumerator StartShadowformFX()
	{
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(this);
		this.m_OriginalMaterial = actor.GetPortraitMaterial();
		yield return null;
		actor.SetShadowform(true);
		this.m_MaterialInstance = new Material(this.m_ShadowformMaterial);
		Texture staticPortraitTexture = actor.GetStaticPortraitTexture();
		this.m_MaterialInstance.mainTexture = staticPortraitTexture;
		actor.SetPortraitMaterial(this.m_MaterialInstance);
		GameObject portraitMesh = actor.GetPortraitMesh();
		Material mat = portraitMesh.GetComponent<Renderer>().GetMaterial(actor.m_portraitMatIdx);
		Action<object> action = delegate(object desat)
		{
			mat.SetFloat("_Desaturate", (float)desat);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeInTime,
			"from",
			0f,
			"to",
			this.m_Desaturate,
			"onupdate",
			action,
			"onupdatetarget",
			actor.gameObject
		});
		iTween.ValueTo(actor.gameObject, args);
		Action<object> action2 = delegate(object col)
		{
			mat.SetColor("_Color", (Color)col);
		};
		Hashtable args2 = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeInTime,
			"from",
			Color.white,
			"to",
			this.m_Tint,
			"onupdate",
			action2,
			"onupdatetarget",
			actor.gameObject
		});
		iTween.ValueTo(actor.gameObject, args2);
		Action<object> action3 = delegate(object desat)
		{
			mat.SetFloat("_Contrast", (float)desat);
		};
		Hashtable args3 = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeInTime,
			"from",
			0f,
			"to",
			this.m_Contrast,
			"onupdate",
			action3,
			"onupdatetarget",
			actor.gameObject
		});
		iTween.ValueTo(actor.gameObject, args3);
		Action<object> action4 = delegate(object desat)
		{
			mat.SetFloat("_Intensity", (float)desat);
		};
		Hashtable args4 = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeInTime,
			"from",
			1f,
			"to",
			this.m_Intensity,
			"onupdate",
			action4,
			"onupdatetarget",
			actor.gameObject
		});
		iTween.ValueTo(actor.gameObject, args4);
		Action<object> action5 = delegate(object desat)
		{
			mat.SetFloat("_FxIntensity", (float)desat);
		};
		Hashtable args5 = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeInTime,
			"from",
			0f,
			"to",
			this.m_FxIntensity,
			"onupdate",
			action5,
			"onupdatetarget",
			actor.gameObject
		});
		iTween.ValueTo(actor.gameObject, args5);
		yield break;
	}

	// Token: 0x06006FE2 RID: 28642 RVA: 0x0024183E File Offset: 0x0023FA3E
	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		if (this.m_StartFXCoroutine != null)
		{
			base.StopCoroutine(this.m_StartFXCoroutine);
		}
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(this);
		actor.SetShadowform(false);
		actor.UpdateAllComponents();
		actor.SetPortraitMaterial(this.m_OriginalMaterial);
	}

	// Token: 0x040059B0 RID: 22960
	public Material m_ShadowformMaterial;

	// Token: 0x040059B1 RID: 22961
	public int m_MaterialIndex = 1;

	// Token: 0x040059B2 RID: 22962
	public float m_FadeInTime = 1f;

	// Token: 0x040059B3 RID: 22963
	public float m_Desaturate = 0.8f;

	// Token: 0x040059B4 RID: 22964
	public Color m_Tint = new Color(0.69140625f, 0.328125f, 0.8046875f, 1f);

	// Token: 0x040059B5 RID: 22965
	public float m_Contrast = -0.29f;

	// Token: 0x040059B6 RID: 22966
	public float m_Intensity = 0.85f;

	// Token: 0x040059B7 RID: 22967
	public float m_FxIntensity = 4f;

	// Token: 0x040059B8 RID: 22968
	private Material m_MaterialInstance;

	// Token: 0x040059B9 RID: 22969
	private Material m_OriginalMaterial;

	// Token: 0x040059BA RID: 22970
	private Coroutine m_StartFXCoroutine;
}
