using System.Collections.Generic;
using UnityEngine;

public class ActorState : MonoBehaviour
{
	public ActorStateType m_StateType;

	public List<ActorStateAnimObject> m_ExternalAnimatedObjects;

	private ActorStateMgr m_stateMgr;

	private bool m_playing;

	private bool m_initialized;

	private void Start()
	{
		m_stateMgr = SceneUtils.FindComponentInParents<ActorStateMgr>(base.gameObject);
		foreach (ActorStateAnimObject externalAnimatedObject in m_ExternalAnimatedObjects)
		{
			externalAnimatedObject.Init();
		}
		m_initialized = true;
		if (m_playing)
		{
			base.gameObject.SetActive(value: true);
			PlayNow();
		}
	}

	public void Play()
	{
		if (!m_playing)
		{
			m_playing = true;
			if (m_initialized)
			{
				base.gameObject.SetActive(value: true);
				PlayNow();
			}
		}
	}

	public void Stop(List<ActorState> nextStateList)
	{
		if (!m_playing)
		{
			return;
		}
		m_playing = false;
		if (!m_initialized)
		{
			return;
		}
		if (GetComponent<Animation>() != null)
		{
			GetComponent<Animation>().Stop();
		}
		if (nextStateList == null)
		{
			foreach (ActorStateAnimObject externalAnimatedObject in m_ExternalAnimatedObjects)
			{
				externalAnimatedObject.Stop();
			}
		}
		else
		{
			foreach (ActorStateAnimObject externalAnimatedObject2 in m_ExternalAnimatedObjects)
			{
				externalAnimatedObject2.Stop(nextStateList);
			}
		}
		base.gameObject.SetActive(value: false);
	}

	public float GetAnimationDuration()
	{
		float num = 0f;
		for (int i = 0; i < m_ExternalAnimatedObjects.Count; i++)
		{
			if (m_ExternalAnimatedObjects[i].m_GameObject != null)
			{
				num = Mathf.Max(m_ExternalAnimatedObjects[i].m_AnimClip.length, num);
			}
		}
		return num;
	}

	public void ShowState()
	{
		base.gameObject.SetActive(value: true);
		Play();
	}

	public void HideState()
	{
		Stop(null);
		base.gameObject.SetActive(value: false);
	}

	private void OnChangeState(ActorStateType stateType)
	{
		m_stateMgr.ChangeState(stateType);
	}

	private void PlayNow()
	{
		if (GetComponent<Animation>() != null)
		{
			GetComponent<Animation>().Play();
		}
		foreach (ActorStateAnimObject externalAnimatedObject in m_ExternalAnimatedObjects)
		{
			externalAnimatedObject.Play();
		}
	}
}
