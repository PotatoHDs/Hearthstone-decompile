using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularBundleNodeLayout : MonoBehaviour
{
	public delegate void OnModularBundleAnimationsFinished(object callbackData);

	public struct NodeCallbackData
	{
		public int layoutId;

		public List<ModularBundleLayoutNodeDbfRecord> layoutNodes;

		public string prefab;

		public bool forceImmediate;

		public NodeCallbackData(int layoutId, List<ModularBundleLayoutNodeDbfRecord> layoutNodes, string prefab, bool forceImmediate)
		{
			this.layoutId = layoutId;
			this.layoutNodes = layoutNodes;
			this.prefab = prefab;
			this.forceImmediate = forceImmediate;
		}
	}

	public List<ModularBundleNode> Nodes;

	private GeneralStorePacksContentDisplay m_parentDisplay;

	public int LayoutID { get; set; }

	public bool IsAnimating { get; private set; }

	public void Initialize(GeneralStorePacksContentDisplay display, int layoutId, List<ModularBundleLayoutNodeDbfRecord> nodeData)
	{
		m_parentDisplay = display;
		LayoutID = layoutId;
		if (Nodes.Count > nodeData.Count)
		{
			Debug.LogWarningFormat("Node layout {0} has more nodes than there are Node Records in the MODULAR_BUNDLE_LAYOUT dbi.", base.name);
		}
		else if (Nodes.Count < nodeData.Count)
		{
			Debug.LogWarningFormat("Node layout {0} has fewer nodes than there are Node Records in the MODULAR_BUNDLE_LAYOUT dbi.", base.name);
		}
		List<int> list = new List<int>();
		for (int i = 0; i < nodeData.Count && i < Nodes.Count; i++)
		{
			int num = Mathf.Clamp(nodeData[i].NodeIndex - 1, 0, Nodes.Count - 1);
			if (Nodes[num] == null)
			{
				Debug.LogErrorFormat("Node layout {0} has unassigned Nodes elements, at index={1}", base.name, num);
			}
			else if (list.Contains(num))
			{
				Debug.LogErrorFormat("Duplicate node index found for layout {0}", base.name);
			}
			else
			{
				list.Add(num);
				Nodes[num].Initialize(this, nodeData[i]);
			}
		}
	}

	public GeneralStorePacksContentDisplay GetDisplay()
	{
		return m_parentDisplay;
	}

	public void PlayEntranceAnimationsInSequence(bool forceImmediate, OnModularBundleAnimationsFinished callback, object callbackData)
	{
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(PlayEntranceAnimationsInSequenceCoroutine(forceImmediate, callback, callbackData));
		}
	}

	private IEnumerator PlayEntranceAnimationsInSequenceCoroutine(bool forceImmediate, OnModularBundleAnimationsFinished callback, object callbackData)
	{
		IsAnimating = true;
		foreach (ModularBundleNode node2 in Nodes)
		{
			if (!node2.IsReady())
			{
				yield return null;
			}
		}
		foreach (ModularBundleNode node in Nodes)
		{
			if (forceImmediate)
			{
				node.EnterImmediately();
				continue;
			}
			yield return new WaitForSeconds(node.DelayBeforeEntryAnimation);
			node.PlayEntryAnimation();
		}
		IsAnimating = false;
		callback(callbackData);
	}

	public void PlayExitAnimationsInSequence(bool forceImmediate, OnModularBundleAnimationsFinished callback, object callbackData)
	{
		if (base.gameObject.activeInHierarchy)
		{
			StartCoroutine(PlayExitAnimationsInSequenceCoroutine(forceImmediate, callback, callbackData));
		}
	}

	private IEnumerator PlayExitAnimationsInSequenceCoroutine(bool forceImmediate, OnModularBundleAnimationsFinished callback, object callbackData)
	{
		IsAnimating = true;
		foreach (ModularBundleNode node in Nodes)
		{
			if (!node.IsReady())
			{
				yield return null;
			}
		}
		int i = Nodes.Count - 1;
		while (i >= 0)
		{
			if (forceImmediate)
			{
				Nodes[i].ExitImmediately();
			}
			else
			{
				yield return new WaitForSeconds(Nodes[i].DelayBeforeEntryAnimation);
				Nodes[i].PlayExitAnimation();
			}
			int num = i - 1;
			i = num;
		}
		IsAnimating = false;
		callback(callbackData);
	}
}
