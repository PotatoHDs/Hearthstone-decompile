using System.Collections.Generic;
using UnityEngine;

public class AnimateTransitions : MonoBehaviour
{
	public List<GameObject> m_TargetList;

	public float amount;

	private List<Renderer> rend;

	public void StartTransitions()
	{
		foreach (Renderer item in rend)
		{
			item.GetMaterial().SetFloat("_Transistion", amount);
		}
	}

	private void Start()
	{
		rend = new List<Renderer>();
		foreach (GameObject target in m_TargetList)
		{
			if (!(target == null))
			{
				rend.Add(target.GetComponent<Renderer>());
			}
		}
	}

	private void Update()
	{
		StartTransitions();
	}
}
