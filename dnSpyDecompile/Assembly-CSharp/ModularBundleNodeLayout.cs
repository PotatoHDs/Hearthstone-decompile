using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000710 RID: 1808
public class ModularBundleNodeLayout : MonoBehaviour
{
	// Token: 0x170005F0 RID: 1520
	// (get) Token: 0x06006501 RID: 25857 RVA: 0x0020F8B7 File Offset: 0x0020DAB7
	// (set) Token: 0x06006502 RID: 25858 RVA: 0x0020F8BF File Offset: 0x0020DABF
	public int LayoutID { get; set; }

	// Token: 0x170005F1 RID: 1521
	// (get) Token: 0x06006503 RID: 25859 RVA: 0x0020F8C8 File Offset: 0x0020DAC8
	// (set) Token: 0x06006504 RID: 25860 RVA: 0x0020F8D0 File Offset: 0x0020DAD0
	public bool IsAnimating { get; private set; }

	// Token: 0x06006505 RID: 25861 RVA: 0x0020F8DC File Offset: 0x0020DADC
	public void Initialize(GeneralStorePacksContentDisplay display, int layoutId, List<ModularBundleLayoutNodeDbfRecord> nodeData)
	{
		this.m_parentDisplay = display;
		this.LayoutID = layoutId;
		if (this.Nodes.Count > nodeData.Count)
		{
			Debug.LogWarningFormat("Node layout {0} has more nodes than there are Node Records in the MODULAR_BUNDLE_LAYOUT dbi.", new object[]
			{
				base.name
			});
		}
		else if (this.Nodes.Count < nodeData.Count)
		{
			Debug.LogWarningFormat("Node layout {0} has fewer nodes than there are Node Records in the MODULAR_BUNDLE_LAYOUT dbi.", new object[]
			{
				base.name
			});
		}
		List<int> list = new List<int>();
		int num = 0;
		while (num < nodeData.Count && num < this.Nodes.Count)
		{
			int num2 = Mathf.Clamp(nodeData[num].NodeIndex - 1, 0, this.Nodes.Count - 1);
			if (this.Nodes[num2] == null)
			{
				Debug.LogErrorFormat("Node layout {0} has unassigned Nodes elements, at index={1}", new object[]
				{
					base.name,
					num2
				});
			}
			else if (list.Contains(num2))
			{
				Debug.LogErrorFormat("Duplicate node index found for layout {0}", new object[]
				{
					base.name
				});
			}
			else
			{
				list.Add(num2);
				this.Nodes[num2].Initialize(this, nodeData[num]);
			}
			num++;
		}
	}

	// Token: 0x06006506 RID: 25862 RVA: 0x0020FA1D File Offset: 0x0020DC1D
	public GeneralStorePacksContentDisplay GetDisplay()
	{
		return this.m_parentDisplay;
	}

	// Token: 0x06006507 RID: 25863 RVA: 0x0020FA25 File Offset: 0x0020DC25
	public void PlayEntranceAnimationsInSequence(bool forceImmediate, ModularBundleNodeLayout.OnModularBundleAnimationsFinished callback, object callbackData)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.StartCoroutine(this.PlayEntranceAnimationsInSequenceCoroutine(forceImmediate, callback, callbackData));
	}

	// Token: 0x06006508 RID: 25864 RVA: 0x0020FA45 File Offset: 0x0020DC45
	private IEnumerator PlayEntranceAnimationsInSequenceCoroutine(bool forceImmediate, ModularBundleNodeLayout.OnModularBundleAnimationsFinished callback, object callbackData)
	{
		this.IsAnimating = true;
		using (List<ModularBundleNode>.Enumerator enumerator = this.Nodes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsReady())
				{
					yield return null;
				}
			}
		}
		List<ModularBundleNode>.Enumerator enumerator = default(List<ModularBundleNode>.Enumerator);
		foreach (ModularBundleNode node in this.Nodes)
		{
			if (forceImmediate)
			{
				node.EnterImmediately();
			}
			else
			{
				yield return new WaitForSeconds(node.DelayBeforeEntryAnimation);
				node.PlayEntryAnimation();
				node = null;
			}
		}
		enumerator = default(List<ModularBundleNode>.Enumerator);
		this.IsAnimating = false;
		callback(callbackData);
		yield break;
		yield break;
	}

	// Token: 0x06006509 RID: 25865 RVA: 0x0020FA69 File Offset: 0x0020DC69
	public void PlayExitAnimationsInSequence(bool forceImmediate, ModularBundleNodeLayout.OnModularBundleAnimationsFinished callback, object callbackData)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		base.StartCoroutine(this.PlayExitAnimationsInSequenceCoroutine(forceImmediate, callback, callbackData));
	}

	// Token: 0x0600650A RID: 25866 RVA: 0x0020FA89 File Offset: 0x0020DC89
	private IEnumerator PlayExitAnimationsInSequenceCoroutine(bool forceImmediate, ModularBundleNodeLayout.OnModularBundleAnimationsFinished callback, object callbackData)
	{
		this.IsAnimating = true;
		using (List<ModularBundleNode>.Enumerator enumerator = this.Nodes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsReady())
				{
					yield return null;
				}
			}
		}
		List<ModularBundleNode>.Enumerator enumerator = default(List<ModularBundleNode>.Enumerator);
		int num;
		for (int i = this.Nodes.Count - 1; i >= 0; i = num)
		{
			if (forceImmediate)
			{
				this.Nodes[i].ExitImmediately();
			}
			else
			{
				yield return new WaitForSeconds(this.Nodes[i].DelayBeforeEntryAnimation);
				this.Nodes[i].PlayExitAnimation();
			}
			num = i - 1;
		}
		this.IsAnimating = false;
		callback(callbackData);
		yield break;
		yield break;
	}

	// Token: 0x040053DB RID: 21467
	public List<ModularBundleNode> Nodes;

	// Token: 0x040053DE RID: 21470
	private GeneralStorePacksContentDisplay m_parentDisplay;

	// Token: 0x0200229F RID: 8863
	// (Invoke) Token: 0x060127DF RID: 75743
	public delegate void OnModularBundleAnimationsFinished(object callbackData);

	// Token: 0x020022A0 RID: 8864
	public struct NodeCallbackData
	{
		// Token: 0x060127E2 RID: 75746 RVA: 0x0050CED6 File Offset: 0x0050B0D6
		public NodeCallbackData(int layoutId, List<ModularBundleLayoutNodeDbfRecord> layoutNodes, string prefab, bool forceImmediate)
		{
			this.layoutId = layoutId;
			this.layoutNodes = layoutNodes;
			this.prefab = prefab;
			this.forceImmediate = forceImmediate;
		}

		// Token: 0x0400E423 RID: 58403
		public int layoutId;

		// Token: 0x0400E424 RID: 58404
		public List<ModularBundleLayoutNodeDbfRecord> layoutNodes;

		// Token: 0x0400E425 RID: 58405
		public string prefab;

		// Token: 0x0400E426 RID: 58406
		public bool forceImmediate;
	}
}
