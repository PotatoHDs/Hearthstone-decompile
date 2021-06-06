using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A6B RID: 2667
public class RandomSpawnTimed : MonoBehaviour
{
	// Token: 0x06008F12 RID: 36626 RVA: 0x002E3C3C File Offset: 0x002E1E3C
	private void Start()
	{
		this.listOfObjs = new List<GameObject>();
		base.StartCoroutine(this.RespawnLoop());
	}

	// Token: 0x06008F13 RID: 36627 RVA: 0x002E3C56 File Offset: 0x002E1E56
	private IEnumerator RespawnLoop()
	{
		for (;;)
		{
			float seconds = UnityEngine.Random.Range(this.minWaitTime, this.maxWaitTime);
			yield return new WaitForSeconds(seconds);
			this.listOfObjs.Add(UnityEngine.Object.Instantiate<GameObject>(this.objPrefab, base.transform.position, UnityEngine.Random.rotation));
		}
		yield break;
	}

	// Token: 0x06008F14 RID: 36628 RVA: 0x002E3C68 File Offset: 0x002E1E68
	private void Update()
	{
		for (int i = 0; i < this.listOfObjs.Count; i++)
		{
			if (Mathf.Abs(this.listOfObjs[i].transform.position.x - base.gameObject.transform.position.x) > this.killX || Mathf.Abs(this.listOfObjs[i].transform.position.z - base.gameObject.transform.position.z) > this.killZ)
			{
				UnityEngine.Object obj = this.listOfObjs[i];
				this.listOfObjs.Remove(this.listOfObjs[i]);
				UnityEngine.Object.Destroy(obj);
				i--;
			}
		}
	}

	// Token: 0x04007791 RID: 30609
	public float minWaitTime = 5f;

	// Token: 0x04007792 RID: 30610
	public float maxWaitTime = 15f;

	// Token: 0x04007793 RID: 30611
	public float killX = 10f;

	// Token: 0x04007794 RID: 30612
	public float killZ = 10f;

	// Token: 0x04007795 RID: 30613
	public GameObject objPrefab;

	// Token: 0x04007796 RID: 30614
	private List<GameObject> listOfObjs;
}
