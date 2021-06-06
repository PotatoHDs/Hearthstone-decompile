using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004F RID: 79
[CustomEditClass]
public class AdventureSubScene : MonoBehaviour
{
	// Token: 0x06000482 RID: 1154 RVA: 0x0001B2A1 File Offset: 0x000194A1
	public void SetIsLoaded(bool loaded)
	{
		this.m_IsLoaded = loaded;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0001B2AA File Offset: 0x000194AA
	public bool IsLoaded()
	{
		return this.m_IsLoaded;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0001B2B2 File Offset: 0x000194B2
	public void AddSubSceneTransitionFinishedListener(AdventureSubScene.SubSceneTransitionFinished dlg)
	{
		this.m_SubSceneTransitionListeners.Add(dlg);
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0001B2C0 File Offset: 0x000194C0
	public void RemoveSubSceneTransitionFinishedListener(AdventureSubScene.SubSceneTransitionFinished dlg)
	{
		this.m_SubSceneTransitionListeners.Remove(dlg);
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0001B2CF File Offset: 0x000194CF
	public void NotifyTransitionComplete()
	{
		this.FireSubSceneTransitionFinishedEvent();
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0001B2D8 File Offset: 0x000194D8
	private void FireSubSceneTransitionFinishedEvent()
	{
		AdventureSubScene.SubSceneTransitionFinished[] array = this.m_SubSceneTransitionListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x04000329 RID: 809
	[CustomEditField(Sections = "Animation Settings")]
	public float m_TransitionAnimationTime = 1f;

	// Token: 0x0400032A RID: 810
	[CustomEditField(Sections = "Bounds Settings")]
	public Vector3_MobileOverride m_SubSceneBounds;

	// Token: 0x0400032B RID: 811
	[CustomEditField(Sections = "Transition Settings")]
	public bool m_reverseTransitionAfterThisSubscene;

	// Token: 0x0400032C RID: 812
	public bool m_reverseTransitionBeforeThisSubscene;

	// Token: 0x0400032D RID: 813
	private bool m_IsLoaded;

	// Token: 0x0400032E RID: 814
	private List<AdventureSubScene.SubSceneTransitionFinished> m_SubSceneTransitionListeners = new List<AdventureSubScene.SubSceneTransitionFinished>();

	// Token: 0x02001326 RID: 4902
	// (Invoke) Token: 0x0600D69D RID: 54941
	public delegate void SubSceneTransitionFinished();
}
