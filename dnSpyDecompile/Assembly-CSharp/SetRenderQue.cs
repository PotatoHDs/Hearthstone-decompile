using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A8A RID: 2698
[RequireComponent(typeof(Renderer))]
public class SetRenderQue : MonoBehaviour
{
	// Token: 0x06009075 RID: 36981 RVA: 0x002EDED5 File Offset: 0x002EC0D5
	private void Awake()
	{
		this.m_Renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06009076 RID: 36982 RVA: 0x002EDEE4 File Offset: 0x002EC0E4
	private void Start()
	{
		if (this.includeChildren)
		{
			foreach (Renderer renderer in base.GetComponentsInChildren<Renderer>())
			{
				if (!(renderer == null))
				{
					renderer.sortingOrder += this.queue;
				}
			}
		}
		else
		{
			if (this.m_Renderer == null)
			{
				return;
			}
			this.m_Renderer.sortingOrder += this.queue;
		}
		if (this.queues == null)
		{
			return;
		}
		if (this.m_Renderer == null)
		{
			return;
		}
		List<Material> sharedMaterials = this.m_Renderer.GetSharedMaterials();
		if (sharedMaterials == null)
		{
			return;
		}
		int count = sharedMaterials.Count;
		int num = 0;
		while (num < this.queues.Length && num < count)
		{
			int num2 = this.queues[num];
			if (num2 != 0 && num2 != this.queue)
			{
				Material sharedMaterial = this.m_Renderer.GetSharedMaterial(num);
				if (!(sharedMaterial == null))
				{
					if (num2 < 0)
					{
						Debug.LogWarning(string.Format("WARNING: Using negative renderQueue for {0}'s {1} (renderQueue = {2})", base.transform.root.name, base.gameObject.name, this.queues[num]));
					}
					Material material = new Material(sharedMaterial);
					material.renderQueue += num2;
					this.m_Renderer.SetMaterial(num, material);
				}
			}
			num++;
		}
	}

	// Token: 0x04007946 RID: 31046
	public int queue = 1;

	// Token: 0x04007947 RID: 31047
	public bool includeChildren;

	// Token: 0x04007948 RID: 31048
	public int[] queues;

	// Token: 0x04007949 RID: 31049
	private Renderer m_Renderer;
}
