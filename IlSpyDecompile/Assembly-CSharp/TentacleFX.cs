using System.Collections.Generic;
using UnityEngine;

public class TentacleFX : MonoBehaviour
{
	public GameObject m_TentacleRoot;

	public List<GameObject> m_Tentacles;

	public bool doRotate = true;

	private void Start()
	{
		foreach (GameObject tentacle in m_Tentacles)
		{
			tentacle.GetComponent<Renderer>().GetMaterial().SetFloat("_Seed", Random.Range(0f, 2f));
		}
		if (doRotate)
		{
			m_TentacleRoot.transform.Rotate(Vector3.up, Random.Range(0f, 360f), Space.Self);
		}
	}
}
