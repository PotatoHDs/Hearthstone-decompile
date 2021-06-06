using UnityEngine;

public class BranchScript : MonoBehaviour
{
	public float timeSpawned;

	private void Awake()
	{
		timeSpawned = Time.time;
	}

	private void Update()
	{
	}
}
