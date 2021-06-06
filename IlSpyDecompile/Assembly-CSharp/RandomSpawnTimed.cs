using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnTimed : MonoBehaviour
{
	public float minWaitTime = 5f;

	public float maxWaitTime = 15f;

	public float killX = 10f;

	public float killZ = 10f;

	public GameObject objPrefab;

	private List<GameObject> listOfObjs;

	private void Start()
	{
		listOfObjs = new List<GameObject>();
		StartCoroutine(RespawnLoop());
	}

	private IEnumerator RespawnLoop()
	{
		while (true)
		{
			float seconds = Random.Range(minWaitTime, maxWaitTime);
			yield return new WaitForSeconds(seconds);
			listOfObjs.Add(Object.Instantiate(objPrefab, base.transform.position, Random.rotation));
		}
	}

	private void Update()
	{
		for (int i = 0; i < listOfObjs.Count; i++)
		{
			if (Mathf.Abs(listOfObjs[i].transform.position.x - base.gameObject.transform.position.x) > killX || Mathf.Abs(listOfObjs[i].transform.position.z - base.gameObject.transform.position.z) > killZ)
			{
				GameObject obj = listOfObjs[i];
				listOfObjs.Remove(listOfObjs[i]);
				Object.Destroy(obj);
				i--;
			}
		}
	}
}
