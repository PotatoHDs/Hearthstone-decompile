using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SetRenderQue : MonoBehaviour
{
	public int queue = 1;

	public bool includeChildren;

	public int[] queues;

	private Renderer m_Renderer;

	private void Awake()
	{
		m_Renderer = GetComponent<Renderer>();
	}

	private void Start()
	{
		if (includeChildren)
		{
			Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in componentsInChildren)
			{
				if (!(renderer == null))
				{
					renderer.sortingOrder += queue;
				}
			}
		}
		else
		{
			if (m_Renderer == null)
			{
				return;
			}
			m_Renderer.sortingOrder += queue;
		}
		if (queues == null || m_Renderer == null)
		{
			return;
		}
		List<Material> sharedMaterials = m_Renderer.GetSharedMaterials();
		if (sharedMaterials == null)
		{
			return;
		}
		int count = sharedMaterials.Count;
		for (int j = 0; j < queues.Length && j < count; j++)
		{
			int num = queues[j];
			if (num == 0 || num == queue)
			{
				continue;
			}
			Material sharedMaterial = m_Renderer.GetSharedMaterial(j);
			if (!(sharedMaterial == null))
			{
				if (num < 0)
				{
					Debug.LogWarning($"WARNING: Using negative renderQueue for {base.transform.root.name}'s {base.gameObject.name} (renderQueue = {queues[j]})");
				}
				Material material = new Material(sharedMaterial);
				material.renderQueue += num;
				m_Renderer.SetMaterial(j, material);
			}
		}
	}
}
