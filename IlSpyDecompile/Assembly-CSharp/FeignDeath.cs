using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeignDeath : SuperSpell
{
	public GameObject m_RootObject;

	public GameObject m_Glow;

	public float m_Height = 1f;

	protected override void Awake()
	{
		base.Awake();
		m_RootObject.SetActive(value: false);
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		if (!m_taskList.IsStartOfBlock())
		{
			base.OnAction(prevStateType);
			return;
		}
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		m_targets.Clear();
		for (PowerTaskList powerTaskList = m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetNext())
		{
			foreach (PowerTask task in powerTaskList.GetTaskList())
			{
				Network.HistMetaData histMetaData = task.GetPower() as Network.HistMetaData;
				if (histMetaData == null || histMetaData.MetaType != 0)
				{
					continue;
				}
				foreach (int item in histMetaData.Info)
				{
					Card card = GameState.Get().GetEntity(item).GetCard();
					m_targets.Add(card.gameObject);
				}
			}
		}
		StartCoroutine(ActionVisual());
	}

	private IEnumerator ActionVisual()
	{
		List<GameObject> fxObjects = new List<GameObject>();
		foreach (GameObject target in m_targets)
		{
			GameObject gameObject = Object.Instantiate(m_RootObject);
			gameObject.SetActive(value: true);
			fxObjects.Add(gameObject);
			gameObject.transform.position = target.transform.position;
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + m_Height, gameObject.transform.position.z);
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Play();
			}
		}
		yield return new WaitForSeconds(1f);
		foreach (GameObject item in fxObjects)
		{
			Object.Destroy(item);
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
