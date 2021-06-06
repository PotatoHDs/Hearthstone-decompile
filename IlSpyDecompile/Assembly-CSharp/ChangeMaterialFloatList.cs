using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialFloatList : MonoBehaviour
{
	public List<GameObject> m_TargetList;

	public string m_propertyName;

	public float m_propertyValue;

	private List<Renderer> rend;

	private int m_materialProperty;

	private Material m_mat;

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
		foreach (Renderer item in rend)
		{
			item.GetMaterial().SetFloat(m_propertyName, m_propertyValue);
		}
	}
}
