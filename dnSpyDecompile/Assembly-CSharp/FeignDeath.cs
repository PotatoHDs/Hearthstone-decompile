using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x020007F0 RID: 2032
public class FeignDeath : SuperSpell
{
	// Token: 0x06006EC4 RID: 28356 RVA: 0x0023B597 File Offset: 0x00239797
	protected override void Awake()
	{
		base.Awake();
		this.m_RootObject.SetActive(false);
	}

	// Token: 0x06006EC5 RID: 28357 RVA: 0x0023B5AC File Offset: 0x002397AC
	protected override void OnAction(SpellStateType prevStateType)
	{
		if (!this.m_taskList.IsStartOfBlock())
		{
			base.OnAction(prevStateType);
			return;
		}
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		this.m_targets.Clear();
		for (PowerTaskList powerTaskList = this.m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetNext())
		{
			foreach (PowerTask powerTask in powerTaskList.GetTaskList())
			{
				Network.HistMetaData histMetaData = powerTask.GetPower() as Network.HistMetaData;
				if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.TARGET)
				{
					foreach (int id in histMetaData.Info)
					{
						Card card = GameState.Get().GetEntity(id).GetCard();
						this.m_targets.Add(card.gameObject);
					}
				}
			}
		}
		base.StartCoroutine(this.ActionVisual());
	}

	// Token: 0x06006EC6 RID: 28358 RVA: 0x0023B6CC File Offset: 0x002398CC
	private IEnumerator ActionVisual()
	{
		List<GameObject> fxObjects = new List<GameObject>();
		foreach (GameObject gameObject in this.m_targets)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_RootObject);
			gameObject2.SetActive(true);
			fxObjects.Add(gameObject2);
			gameObject2.transform.position = gameObject.transform.position;
			gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y + this.m_Height, gameObject2.transform.position.z);
			ParticleSystem[] componentsInChildren = gameObject2.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].Play();
			}
		}
		yield return new WaitForSeconds(1f);
		foreach (GameObject obj in fxObjects)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
	}

	// Token: 0x040058E5 RID: 22757
	public GameObject m_RootObject;

	// Token: 0x040058E6 RID: 22758
	public GameObject m_Glow;

	// Token: 0x040058E7 RID: 22759
	public float m_Height = 1f;
}
