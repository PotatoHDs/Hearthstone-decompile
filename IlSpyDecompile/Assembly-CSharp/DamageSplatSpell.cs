using System.Collections;
using UnityEngine;

public class DamageSplatSpell : Spell
{
	public GameObject m_BloodSplat;

	public GameObject m_PoisonSplat;

	public GameObject m_HealSplat;

	public UberText m_DamageTextMesh;

	private GameObject m_activeSplat;

	private int m_damage;

	private bool m_poison;

	private const float SCALE_IN_TIME = 1f;

	protected override void Awake()
	{
		base.Awake();
		EnableAllRenderers(enabled: false);
	}

	public float GetDamage()
	{
		return m_damage;
	}

	public void SetDamage(int damage)
	{
		m_damage = damage;
	}

	public void SetPoisonous(bool isPoisonous)
	{
		m_poison = isPoisonous;
		m_DamageTextMesh.gameObject.SetActive(!m_poison);
	}

	public bool IsPoisonous()
	{
		return m_poison;
	}

	public void DoSplatAnims()
	{
		StopAllCoroutines();
		iTween.Stop(base.gameObject);
		StartCoroutine(SplatAnimCoroutine());
	}

	private IEnumerator SplatAnimCoroutine()
	{
		UpdateElements();
		base.transform.localScale = Vector3.zero;
		yield return null;
		OnSpellFinished();
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "easetype", iTween.EaseType.easeOutElastic));
		float seconds = 2f;
		if (IsPoisonous())
		{
			seconds = 0.8f;
		}
		yield return new WaitForSeconds(seconds);
		iTween.FadeTo(base.gameObject, 0f, 1f);
		yield return new WaitForSeconds(1.1f);
		EnableAllRenderers(enabled: false);
		OnStateFinished();
	}

	protected override void OnIdle(SpellStateType prevStateType)
	{
		StopAllCoroutines();
		UpdateElements();
		base.OnIdle(prevStateType);
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		UpdateElements();
		base.OnAction(prevStateType);
		DoSplatAnims();
	}

	protected override void OnNone(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		m_activeSplat = null;
	}

	protected override void ShowImpl()
	{
		base.ShowImpl();
		if (!(m_activeSplat == null))
		{
			SceneUtils.EnableRenderers(m_activeSplat.gameObject, enable: true);
			m_DamageTextMesh.gameObject.SetActive(!m_poison);
		}
	}

	protected override void HideImpl()
	{
		base.HideImpl();
		StopAllCoroutines();
		iTween.Stop(base.gameObject);
		EnableAllRenderers(enabled: false);
	}

	private void UpdateElements()
	{
		iTween.Stop(base.gameObject);
		iTween.FadeTo(base.gameObject, 1f, 0f);
		if (m_damage < 0)
		{
			m_DamageTextMesh.Text = $"+{Mathf.Abs(m_damage)}";
			m_activeSplat = m_HealSplat;
			SceneUtils.EnableRenderers(m_BloodSplat.gameObject, enable: false);
			if (m_PoisonSplat != null)
			{
				SceneUtils.EnableRenderers(m_PoisonSplat.gameObject, enable: false);
			}
			SceneUtils.EnableRenderers(m_HealSplat.gameObject, enable: true);
			m_DamageTextMesh.gameObject.SetActive(value: true);
		}
		else if (m_poison && m_PoisonSplat != null)
		{
			m_DamageTextMesh.Text = $"-{0}";
			m_activeSplat = m_PoisonSplat;
			SceneUtils.EnableRenderers(m_BloodSplat.gameObject, enable: false);
			SceneUtils.EnableRenderers(m_PoisonSplat.gameObject, enable: true);
			SceneUtils.EnableRenderers(m_HealSplat.gameObject, enable: false);
			m_DamageTextMesh.gameObject.SetActive(value: false);
		}
		else
		{
			m_DamageTextMesh.Text = $"-{m_damage}";
			m_activeSplat = m_BloodSplat;
			SceneUtils.EnableRenderers(m_BloodSplat.gameObject, enable: true);
			if (m_PoisonSplat != null)
			{
				SceneUtils.EnableRenderers(m_PoisonSplat.gameObject, enable: false);
			}
			SceneUtils.EnableRenderers(m_HealSplat.gameObject, enable: false);
			m_DamageTextMesh.gameObject.SetActive(value: true);
		}
	}

	private void EnableAllRenderers(bool enabled)
	{
		SceneUtils.EnableRenderers(m_BloodSplat.gameObject, enabled);
		SceneUtils.EnableRenderers(m_HealSplat.gameObject, enabled);
		if (m_PoisonSplat != null)
		{
			SceneUtils.EnableRenderers(m_PoisonSplat.gameObject, enabled);
		}
		m_DamageTextMesh.gameObject.SetActive(enabled);
	}
}
