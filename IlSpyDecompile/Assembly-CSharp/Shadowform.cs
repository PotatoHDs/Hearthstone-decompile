using System;
using System.Collections;
using UnityEngine;

public class Shadowform : SuperSpell
{
	public Material m_ShadowformMaterial;

	public int m_MaterialIndex = 1;

	public float m_FadeInTime = 1f;

	public float m_Desaturate = 0.8f;

	public Color m_Tint = new Color(177f / 256f, 21f / 64f, 103f / 128f, 1f);

	public float m_Contrast = -0.29f;

	public float m_Intensity = 0.85f;

	public float m_FxIntensity = 4f;

	private Material m_MaterialInstance;

	private Material m_OriginalMaterial;

	private Coroutine m_StartFXCoroutine;

	protected override void OnBirth(SpellStateType prevStateType)
	{
		base.OnBirth(prevStateType);
		OnSpellFinished();
		if (!(m_ShadowformMaterial == null))
		{
			if (m_StartFXCoroutine != null)
			{
				StopCoroutine(m_StartFXCoroutine);
			}
			m_StartFXCoroutine = StartCoroutine(StartShadowformFX());
		}
	}

	private IEnumerator StartShadowformFX()
	{
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(this);
		m_OriginalMaterial = actor.GetPortraitMaterial();
		yield return null;
		actor.SetShadowform(shadowform: true);
		m_MaterialInstance = new Material(m_ShadowformMaterial);
		Texture staticPortraitTexture = actor.GetStaticPortraitTexture();
		m_MaterialInstance.mainTexture = staticPortraitTexture;
		actor.SetPortraitMaterial(m_MaterialInstance);
		GameObject portraitMesh = actor.GetPortraitMesh();
		Material mat = portraitMesh.GetComponent<Renderer>().GetMaterial(actor.m_portraitMatIdx);
		Action<object> action = delegate(object desat)
		{
			mat.SetFloat("_Desaturate", (float)desat);
		};
		Hashtable args = iTween.Hash("time", m_FadeInTime, "from", 0f, "to", m_Desaturate, "onupdate", action, "onupdatetarget", actor.gameObject);
		iTween.ValueTo(actor.gameObject, args);
		Action<object> action2 = delegate(object col)
		{
			mat.SetColor("_Color", (Color)col);
		};
		Hashtable args2 = iTween.Hash("time", m_FadeInTime, "from", Color.white, "to", m_Tint, "onupdate", action2, "onupdatetarget", actor.gameObject);
		iTween.ValueTo(actor.gameObject, args2);
		Action<object> action3 = delegate(object desat)
		{
			mat.SetFloat("_Contrast", (float)desat);
		};
		Hashtable args3 = iTween.Hash("time", m_FadeInTime, "from", 0f, "to", m_Contrast, "onupdate", action3, "onupdatetarget", actor.gameObject);
		iTween.ValueTo(actor.gameObject, args3);
		Action<object> action4 = delegate(object desat)
		{
			mat.SetFloat("_Intensity", (float)desat);
		};
		Hashtable args4 = iTween.Hash("time", m_FadeInTime, "from", 1f, "to", m_Intensity, "onupdate", action4, "onupdatetarget", actor.gameObject);
		iTween.ValueTo(actor.gameObject, args4);
		Action<object> action5 = delegate(object desat)
		{
			mat.SetFloat("_FxIntensity", (float)desat);
		};
		Hashtable args5 = iTween.Hash("time", m_FadeInTime, "from", 0f, "to", m_FxIntensity, "onupdate", action5, "onupdatetarget", actor.gameObject);
		iTween.ValueTo(actor.gameObject, args5);
	}

	protected override void OnDeath(SpellStateType prevStateType)
	{
		base.OnDeath(prevStateType);
		if (m_StartFXCoroutine != null)
		{
			StopCoroutine(m_StartFXCoroutine);
		}
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(this);
		actor.SetShadowform(shadowform: false);
		actor.UpdateAllComponents();
		actor.SetPortraitMaterial(m_OriginalMaterial);
	}
}
