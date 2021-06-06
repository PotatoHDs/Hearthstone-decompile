using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsecrationEffect : MonoBehaviour
{
	public float m_StartDelayMin = 2f;

	public float m_StartDelayMax = 3f;

	public float m_LiftTime = 1f;

	public float m_LiftHeightMin = 2f;

	public float m_LiftHeightMax = 3f;

	public float m_LiftRotMin = -15f;

	public float m_LiftRotMax = 15f;

	public float m_HoverTime = 0.8f;

	public float m_SlamTime = 0.2f;

	public float m_Bounceness = 0.2f;

	public GameObject m_StartImpact;

	public GameObject m_EndImpact;

	public float m_TotalTime;

	private SuperSpell m_SuperSpell;

	private List<GameObject> m_ImpactObjects;

	private AudioSource m_ImpactSound;

	private void Awake()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_LiftHeightMin = 3f;
			m_LiftHeightMax = 5f;
		}
	}

	private void Start()
	{
		Spell component = GetComponent<Spell>();
		if (component == null)
		{
			base.enabled = false;
		}
		m_SuperSpell = component.GetSuperSpellParent();
		m_ImpactSound = GetComponent<AudioSource>();
		m_ImpactObjects = new List<GameObject>();
	}

	private void OnDestroy()
	{
		if (m_ImpactObjects.Count <= 0)
		{
			return;
		}
		foreach (GameObject impactObject in m_ImpactObjects)
		{
			Object.Destroy(impactObject);
		}
	}

	private void StartAnimation()
	{
		if (m_SuperSpell == null)
		{
			return;
		}
		int num = 0;
		foreach (GameObject target in m_SuperSpell.GetTargets())
		{
			Vector3 position = target.transform.position;
			Quaternion rotation = target.transform.rotation;
			num++;
			float num2 = Random.Range(m_StartDelayMin, m_StartDelayMax);
			GameObject gameObject = Object.Instantiate(m_StartImpact, position, rotation);
			m_ImpactObjects.Add(gameObject);
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem obj in componentsInChildren)
			{
				ParticleSystem.MainModule main = obj.main;
				main.startDelay = num2;
				obj.Play();
			}
			num2 += 0.2f;
			float num3 = Random.Range(m_LiftHeightMin, m_LiftHeightMax);
			Hashtable args = iTween.Hash("time", m_LiftTime, "delay", num2, "position", new Vector3(position.x, position.y + num3, position.z), "easetype", iTween.EaseType.easeOutQuad, "name", $"Lift_{target.name}_{num}");
			iTween.MoveTo(target, args);
			Vector3 eulerAngles = rotation.eulerAngles;
			eulerAngles.x += Random.Range(m_LiftRotMin, m_LiftRotMax);
			eulerAngles.z += Random.Range(m_LiftRotMin, m_LiftRotMax);
			Hashtable args2 = iTween.Hash("time", m_LiftTime + m_HoverTime + m_SlamTime * 0.8f, "delay", num2, "rotation", eulerAngles, "easetype", iTween.EaseType.easeOutQuad, "name", $"LiftRot_{target.name}_{num}");
			iTween.RotateTo(target, args2);
			float num4 = m_StartDelayMax + m_LiftTime + m_HoverTime;
			Hashtable args3 = iTween.Hash("time", m_SlamTime, "delay", num4, "position", position, "easetype", iTween.EaseType.easeInCubic, "name", $"SlamPos_{target.name}_{num}");
			iTween.MoveTo(target, args3);
			Hashtable args4 = iTween.Hash("time", m_SlamTime * 0.8f, "delay", num4 + m_SlamTime * 0.2f, "rotation", Vector3.zero, "easetype", iTween.EaseType.easeInQuad, "oncomplete", "Finished", "oncompletetarget", base.gameObject, "name", $"SlamRot_{target.name}_{num}");
			iTween.RotateTo(target, args4);
			m_TotalTime = num4 + m_SlamTime;
			if ((bool)target.GetComponentInChildren<MinionShake>())
			{
				MinionShake.ShakeObject(target, ShakeMinionType.RandomDirection, target.transform.position, ShakeMinionIntensity.LargeShake, 1f, 0.1f, num4 + m_SlamTime, ignoreAnimationPlaying: true, ignoreHeight: true);
			}
			else
			{
				Bounce bounce = target.GetComponent<Bounce>();
				if (bounce == null)
				{
					bounce = target.AddComponent<Bounce>();
				}
				bounce.m_BounceAmount = num3 * m_Bounceness;
				bounce.m_BounceSpeed = 3.5f * Random.Range(0.8f, 1.3f);
				bounce.m_BounceCount = 3;
				bounce.m_Bounceness = m_Bounceness;
				bounce.m_Delay = num4 + m_SlamTime;
				bounce.StartAnimation();
			}
			GameObject gameObject2 = Object.Instantiate(m_EndImpact, position, rotation);
			m_ImpactObjects.Add(gameObject2);
			componentsInChildren = gameObject2.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem obj2 in componentsInChildren)
			{
				ParticleSystem.MainModule main2 = obj2.main;
				main2.startDelay = num4 + m_SlamTime;
				obj2.Play();
			}
		}
	}

	private void Finished()
	{
		SoundManager.Get().Play(m_ImpactSound);
		CameraShakeMgr.Shake(Camera.main, new Vector3(0.15f, 0.15f, 0.15f), 0.9f);
	}
}
