using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class AdventureSubScene : MonoBehaviour
{
	public delegate void SubSceneTransitionFinished();

	[CustomEditField(Sections = "Animation Settings")]
	public float m_TransitionAnimationTime = 1f;

	[CustomEditField(Sections = "Bounds Settings")]
	public Vector3_MobileOverride m_SubSceneBounds;

	[CustomEditField(Sections = "Transition Settings")]
	public bool m_reverseTransitionAfterThisSubscene;

	public bool m_reverseTransitionBeforeThisSubscene;

	private bool m_IsLoaded;

	private List<SubSceneTransitionFinished> m_SubSceneTransitionListeners = new List<SubSceneTransitionFinished>();

	public void SetIsLoaded(bool loaded)
	{
		m_IsLoaded = loaded;
	}

	public bool IsLoaded()
	{
		return m_IsLoaded;
	}

	public void AddSubSceneTransitionFinishedListener(SubSceneTransitionFinished dlg)
	{
		m_SubSceneTransitionListeners.Add(dlg);
	}

	public void RemoveSubSceneTransitionFinishedListener(SubSceneTransitionFinished dlg)
	{
		m_SubSceneTransitionListeners.Remove(dlg);
	}

	public void NotifyTransitionComplete()
	{
		FireSubSceneTransitionFinishedEvent();
	}

	private void FireSubSceneTransitionFinishedEvent()
	{
		SubSceneTransitionFinished[] array = m_SubSceneTransitionListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}
}
