using System.Collections.Generic;
using UnityEngine;

public class AnimateVars : MonoBehaviour
{
	public List<GameObject> m_objects;

	public float amount;

	public string varName;

	private List<Renderer> m_renderers;

	public void AnimateValue()
	{
		foreach (Renderer renderer in m_renderers)
		{
			if (renderer != null)
			{
				renderer.GetMaterial().SetFloat(varName, amount);
			}
		}
	}

	private void Start()
	{
		m_renderers = new List<Renderer>();
		foreach (GameObject @object in m_objects)
		{
			if (!(@object == null))
			{
				m_renderers.Add(@object.GetComponent<Renderer>());
			}
		}
	}

	private void Update()
	{
		AnimateValue();
	}
}
