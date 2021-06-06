using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimironsHead : SuperSpell
{
	public GameObject m_root;

	public GameObject m_highPosBone;

	public GameObject m_minionPosBone;

	public GameObject m_background;

	public GameObject m_minionElectricity;

	public GameObject m_minionGlow;

	public GameObject m_mimironNegative;

	public GameObject m_mimironFlare;

	public GameObject m_mimironGlow;

	public GameObject m_mimironElectricity;

	public Spell m_voltSpawnOverrideSpell;

	public string m_perMinionSound;

	public string[] m_startSounds;

	private Card m_volt;

	private Card m_mimiron;

	private List<Card> m_mechMinions = new List<Card>();

	private Transform m_voltParent;

	private Color m_clear = new Color(1f, 1f, 1f, 0f);

	private Map<GameObject, List<GameObject>> m_cleanup = new Map<GameObject, List<GameObject>>();

	private bool m_isNegFlash;

	private float m_flashDelay = 0.15f;

	private float m_mimironHighTime = 1.5f;

	private float m_minionHighTime = 2f;

	private float m_sparkDelay = 0.3f;

	private float m_absorbTime = 1f;

	private float m_glowTime = 0.5f;

	private PowerTaskList m_waitForTaskList;

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		Card card = m_taskList.GetSourceEntity().GetCard();
		if (m_taskList.IsOrigin())
		{
			List<PowerTaskList> list = new List<PowerTaskList>();
			for (PowerTaskList powerTaskList = m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetNext())
			{
				list.Add(powerTaskList);
			}
			foreach (PowerTaskList item in list)
			{
				foreach (PowerTask task in item.GetTaskList())
				{
					Network.PowerHistory power = task.GetPower();
					if (power.Type == Network.PowerType.TAG_CHANGE)
					{
						Network.HistTagChange histTagChange = power as Network.HistTagChange;
						if (histTagChange.Tag == 360 && histTagChange.Value == 1)
						{
							Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
							if (entity == null)
							{
								Debug.LogWarning($"{this}.AddPowerTargets() - WARNING trying to target entity with id {histTagChange.Entity} but there is no entity with that id");
								continue;
							}
							Card card2 = entity.GetCard();
							if (card2 != card)
							{
								m_mechMinions.Add(card2);
							}
							else
							{
								m_mimiron = card2;
							}
							m_waitForTaskList = item;
						}
					}
					if (power.Type == Network.PowerType.FULL_ENTITY)
					{
						Network.Entity entity2 = (power as Network.HistFullEntity).Entity;
						Entity entity3 = GameState.Get().GetEntity(entity2.ID);
						if (entity3 == null)
						{
							Debug.LogWarning($"{this}.AddPowerTargets() - WARNING trying to target entity with id {entity2.ID} but there is no entity with that id");
						}
						else if (!(entity3.GetCardId() != "GVG_111t"))
						{
							Card card3 = (m_volt = entity3.GetCard());
							m_waitForTaskList = item;
						}
					}
				}
			}
			if (m_volt != null && m_mimiron != null && m_mechMinions.Count > 0)
			{
				m_mimiron.IgnoreDeath(ignore: true);
				foreach (Card mechMinion in m_mechMinions)
				{
					mechMinion.IgnoreDeath(ignore: true);
				}
				foreach (Card card4 in card.GetController().GetBattlefieldZone().GetCards())
				{
					card4.SetDoNotSort(on: true);
				}
			}
			else
			{
				m_volt = null;
				m_mimiron = null;
				m_mechMinions.Clear();
			}
		}
		if (m_volt == null || m_mimiron == null || m_mechMinions.Count == 0 || m_taskList != m_waitForTaskList)
		{
			return false;
		}
		foreach (Card card5 in card.GetController().GetBattlefieldZone().GetCards())
		{
			card5.SetDoNotSort(on: true);
		}
		return true;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		if ((bool)m_voltSpawnOverrideSpell)
		{
			m_volt.OverrideCustomSpawnSpell(UnityEngine.Object.Instantiate(m_voltSpawnOverrideSpell));
		}
		StartCoroutine(TransformEffect());
	}

	private IEnumerator TransformEffect()
	{
		string[] startSounds = m_startSounds;
		foreach (string text in startSounds)
		{
			SoundManager.Get().LoadAndPlay(text);
		}
		m_volt.SetDoNotSort(on: true);
		m_taskList.DoAllTasks();
		while (!m_taskList.IsComplete())
		{
			yield return null;
		}
		m_volt.GetActor().Hide();
		GameObject gameObject = m_volt.GetActor().gameObject;
		m_voltParent = gameObject.transform.parent;
		gameObject.transform.parent = m_highPosBone.transform;
		gameObject.transform.localPosition = new Vector3(0f, -0.1f, 0f);
		m_root.transform.parent = null;
		m_root.transform.localPosition = Vector3.zero;
		iTween.MoveTo(m_mimiron.gameObject, iTween.Hash("position", m_highPosBone.transform.localPosition, "easetype", iTween.EaseType.easeOutQuart, "time", m_mimironHighTime, "delay", 0.5f));
		yield return new WaitForSeconds(0.5f + m_mimironHighTime / 5f);
		TransformMinions();
	}

	private void TransformMinions()
	{
		float num = 1f;
		Vector3 vector = new Vector3(0f, 0f, 2.3f);
		List<int> list = new List<int>();
		for (int i = 0; i < m_mechMinions.Count; i++)
		{
			list.Add(i);
		}
		List<int> list2 = new List<int>();
		for (int j = 0; j < m_mechMinions.Count; j++)
		{
			int index = UnityEngine.Random.Range(0, list.Count);
			list2.Add(list[index]);
			list.RemoveAt(index);
		}
		for (int k = 0; k < m_mechMinions.Count; k++)
		{
			Vector3 vector2 = Quaternion.Euler(0f, 360 / m_mechMinions.Count * list2[k] + 60, 0f) * vector;
			m_minionPosBone.transform.localPosition = m_highPosBone.transform.localPosition + vector2;
			GameObject gameObject = m_mechMinions[k].GetActor().gameObject;
			float num2 = num / (float)m_mechMinions.Count * (float)k;
			StartCoroutine(MinionPlayFX(gameObject, m_minionElectricity, num2 / 2f));
			List<Vector3> list3 = new List<Vector3>();
			Vector3 vector3 = new Vector3(UnityEngine.Random.Range(-2f, 2f), 0f, UnityEngine.Random.Range(-2f, 2f));
			list3.Add(gameObject.transform.position + (m_minionPosBone.transform.localPosition - gameObject.transform.position) / 4f + vector3);
			list3.Add(m_minionPosBone.transform.localPosition);
			if (k < m_mechMinions.Count - 1)
			{
				iTween.MoveTo(gameObject, iTween.Hash("path", list3.ToArray(), "easetype", iTween.EaseType.easeInOutSine, "delay", num2, "time", m_minionHighTime / (float)m_mechMinions.Count));
				continue;
			}
			iTween.MoveTo(gameObject, iTween.Hash("path", list3.ToArray(), "easetype", iTween.EaseType.easeInOutSine, "delay", num2, "time", m_minionHighTime / (float)m_mechMinions.Count, "oncomplete", (Action<object>)delegate
			{
				FadeInBackground();
			}));
		}
	}

	private IEnumerator MinionPlayFX(GameObject minion, GameObject FX, float delay)
	{
		GameObject minionFX = UnityEngine.Object.Instantiate(FX);
		minionFX.transform.parent = minion.transform;
		minionFX.transform.localPosition = new Vector3(0f, 0.5f, 0f);
		if (!m_cleanup.ContainsKey(minion))
		{
			m_cleanup.Add(minion, new List<GameObject>());
		}
		m_cleanup[minion].Add(minionFX);
		yield return new WaitForSeconds(delay);
		minionFX.GetComponent<ParticleSystem>().Play();
	}

	private IEnumerator MimironNegativeFX()
	{
		while (m_isNegFlash)
		{
			yield return new WaitForSeconds(m_flashDelay);
			m_mimironNegative.SetActive(!m_mimironNegative.activeSelf);
			if (m_flashDelay > 0.05f)
			{
				m_flashDelay -= 0.01f;
			}
		}
		m_mimironNegative.SetActive(value: false);
	}

	private void MinionCleanup(GameObject minion)
	{
		if (!m_cleanup.ContainsKey(minion))
		{
			return;
		}
		foreach (GameObject item in m_cleanup[minion])
		{
			if (item != null)
			{
				UnityEngine.Object.Destroy(item);
			}
		}
	}

	private void FadeInBackground()
	{
		m_background.SetActive(value: true);
		m_background.GetComponent<Renderer>().GetMaterial().SetColor("_Color", m_clear);
		HighlightState componentInChildren = m_volt.GetActor().gameObject.GetComponentInChildren<HighlightState>();
		if (componentInChildren != null)
		{
			componentInChildren.Hide();
		}
		iTween.ColorTo(m_background, iTween.Hash("r", 1f, "g", 1f, "b", 1f, "a", 1f, "time", 0.5f, "oncomplete", (Action<object>)delegate
		{
			MimironPowerUp();
		}));
	}

	private void SetGlow(Material glowMat, float newVal, string colorVal = "_TintColor")
	{
		glowMat.SetColor(colorVal, Color.Lerp(m_clear, Color.white, newVal));
	}

	private void MimironPowerUp()
	{
		m_mimironElectricity.GetComponent<ParticleSystem>().Play();
		for (int i = 0; i < m_mechMinions.Count; i++)
		{
			GameObject gameObject = m_mechMinions[i].GetActor().gameObject;
			GameObject gameObject2 = UnityEngine.Object.Instantiate(m_minionGlow);
			if (!m_cleanup.ContainsKey(gameObject))
			{
				m_cleanup.Add(gameObject, new List<GameObject>());
			}
			m_cleanup[gameObject].Add(gameObject2);
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.transform.localPosition = new Vector3(0f, 0.5f, 0f);
			float num = m_absorbTime / (float)m_mechMinions.Count * (float)i;
			Material rendererMaterial = gameObject2.GetComponent<Renderer>().GetMaterial();
			rendererMaterial.SetColor("_TintColor", m_clear);
			SceneUtils.EnableRenderers(gameObject2, enable: true);
			if (i < m_mechMinions.Count - 1)
			{
				iTween.ValueTo(gameObject2, iTween.Hash("from", 0f, "to", 1f, "time", m_glowTime, "delay", 0.1f + num + m_sparkDelay, "onstart", (Action<object>)delegate
				{
					SoundManager.Get().LoadAndPlay(m_perMinionSound);
				}, "onupdate", (Action<object>)delegate(object newVal)
				{
					SetGlow(rendererMaterial, (float)newVal);
				}));
				iTween.ValueTo(gameObject2, iTween.Hash("from", 1f, "to", 0f, "time", m_glowTime, "delay", 0.1f + num + m_sparkDelay + m_glowTime, "onupdate", (Action<object>)delegate(object newVal)
				{
					SetGlow(rendererMaterial, (float)newVal);
				}));
				continue;
			}
			iTween.ValueTo(gameObject2, iTween.Hash("from", 0f, "to", 1f, "time", m_glowTime, "delay", 0.1f + num + m_sparkDelay, "onstart", (Action<object>)delegate
			{
				SoundManager.Get().LoadAndPlay(m_perMinionSound);
			}, "onupdate", (Action<object>)delegate(object newVal)
			{
				SetGlow(rendererMaterial, (float)newVal);
			}, "oncomplete", (Action<object>)delegate
			{
				AbsorbMinions();
			}));
			iTween.ValueTo(gameObject2, iTween.Hash("from", 1f, "to", 0f, "time", m_glowTime, "delay", 0.1f + num + m_sparkDelay + m_glowTime, "onupdate", (Action<object>)delegate(object newVal)
			{
				SetGlow(rendererMaterial, (float)newVal);
			}));
		}
	}

	private void AbsorbMinions()
	{
		Vector3 vector = new Vector3(0f, -1f, 0f);
		for (int i = 0; i < m_mechMinions.Count; i++)
		{
			float num = m_absorbTime / (float)m_mechMinions.Count * (float)i;
			GameObject minion = m_mechMinions[i].GetActor().gameObject;
			if (i < m_mechMinions.Count - 1)
			{
				iTween.MoveTo(minion, iTween.Hash("position", m_highPosBone.transform.localPosition + vector, "easetype", iTween.EaseType.easeInOutSine, "delay", m_glowTime + num + m_sparkDelay, "time", 0.5f, "oncomplete", (Action<object>)delegate
				{
					MinionCleanup(minion);
				}));
				continue;
			}
			iTween.MoveTo(minion, iTween.Hash("position", m_highPosBone.transform.localPosition + vector, "easetype", iTween.EaseType.easeInOutSine, "delay", m_glowTime + num + m_sparkDelay, "time", 0.5f, "oncomplete", (Action<object>)delegate
			{
				MinionCleanup(minion);
				FlareMimiron();
			}));
		}
		m_isNegFlash = true;
		StartCoroutine(MimironNegativeFX());
	}

	private void FlareMimiron()
	{
		Material mimironGlowMaterial = m_mimironGlow.GetComponent<Renderer>().GetMaterial();
		Material mimironFlareMaterial = m_mimironFlare.GetComponent<Renderer>().GetMaterial();
		mimironGlowMaterial.SetColor("_TintColor", m_clear);
		mimironFlareMaterial.SetColor("_TintColor", m_clear);
		m_mimironGlow.SetActive(value: true);
		m_mimironFlare.SetActive(value: true);
		iTween.ValueTo(m_mimironGlow, iTween.Hash("from", 0f, "to", 0.7f, "time", 0.3, "onupdate", (Action<object>)delegate(object newVal)
		{
			SetGlow(mimironGlowMaterial, (float)newVal);
		}));
		iTween.ValueTo(m_mimironFlare, iTween.Hash("from", 0f, "to", 2.5f, "time", 0.3f, "onupdate", (Action<object>)delegate(object newVal)
		{
			SetGlow(mimironFlareMaterial, (float)newVal, "_Intensity");
		}, "oncomplete", (Action<object>)delegate
		{
			UnflareMimiron();
		}));
	}

	private void UnflareMimiron()
	{
		m_volt.SetDoNotSort(on: false);
		ZonePlay battlefieldZone = m_volt.GetController().GetBattlefieldZone();
		foreach (Card card in battlefieldZone.GetCards())
		{
			card.SetDoNotSort(on: false);
		}
		battlefieldZone.UpdateLayout();
		DestroyMinions();
		m_volt.GetActor().Show();
		Material mimironGlowMaterial = m_mimironGlow.GetComponent<Renderer>().GetMaterial();
		Material mimironFlareMaterial = m_mimironFlare.GetComponent<Renderer>().GetMaterial();
		mimironGlowMaterial.SetColor("_TintColor", m_clear);
		mimironFlareMaterial.SetColor("_TintColor", m_clear);
		m_mimironGlow.SetActive(value: true);
		m_mimironFlare.SetActive(value: true);
		iTween.ValueTo(m_mimironGlow, iTween.Hash("from", 0.7f, "to", 0f, "time", 0.3, "onupdate", (Action<object>)delegate(object newVal)
		{
			SetGlow(mimironGlowMaterial, (float)newVal);
		}));
		iTween.ValueTo(m_mimironFlare, iTween.Hash("from", 2.5f, "to", 0f, "time", 0.3f, "onupdate", (Action<object>)delegate(object newVal)
		{
			SetGlow(mimironFlareMaterial, (float)newVal, "_Intensity");
		}, "oncomplete", (Action<object>)delegate
		{
			FadeOutBackground();
		}));
		m_isNegFlash = false;
		OnSpellFinished();
	}

	private void FadeOutBackground()
	{
		m_mimironGlow.SetActive(value: false);
		m_mimironFlare.SetActive(value: false);
		iTween.ColorTo(m_background, iTween.Hash("r", 1f, "g", 1f, "b", 1f, "a", 0f, "time", 0.5f, "oncomplete", (Action<object>)delegate
		{
			RaiseVolt();
		}));
	}

	private void DestroyMinions()
	{
		foreach (Card mechMinion in m_mechMinions)
		{
			mechMinion.IgnoreDeath(ignore: false);
			mechMinion.SetDoNotSort(on: false);
			mechMinion.GetActor().Destroy();
		}
		m_mimiron.IgnoreDeath(ignore: false);
		m_mimiron.SetDoNotSort(on: false);
		m_mimiron.GetActor().Destroy();
	}

	private void RaiseVolt()
	{
		m_mimironElectricity.GetComponent<ParticleSystem>().Stop();
		m_background.GetComponent<Renderer>().GetMaterial().SetColor("_Color", m_clear);
		m_background.SetActive(value: false);
		GameObject gameObject = m_volt.GetActor().gameObject;
		gameObject.transform.parent = m_voltParent;
		iTween.MoveTo(gameObject, iTween.Hash("position", gameObject.transform.localPosition + new Vector3(0f, 3f, 0f), "time", 0.2f, "islocal", true, "oncomplete", (Action<object>)delegate
		{
			DropV07tron();
		}));
	}

	private void DropV07tron()
	{
		iTween.MoveTo(m_volt.GetActor().gameObject, iTween.Hash("position", Vector3.zero, "time", 0.3f, "islocal", true));
		Finish();
	}

	private void Finish()
	{
		m_volt = null;
		m_mimiron = null;
		m_mechMinions.Clear();
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
