using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class UNGMusic : MonoBehaviour
{
	public List<MusicNote> m_MusicNotes;

	public string m_PlayedEvent;

	public List<int> m_NoteSequence;

	public GameObject m_RewardCollisionObject;

	public string m_RewardFSMName;

	public string m_RewardEvent;

	public GameObject m_ClickableFSM;

	public string m_ClickableFSMName;

	public string m_ClickableEvent;

	private List<int> m_CorrectNotesHit = new List<int>();

	private void Start()
	{
		m_CorrectNotesHit.Clear();
	}

	private void Update()
	{
		HandleHits();
	}

	private void HandleHits()
	{
		for (int i = 0; i < m_MusicNotes.Count; i++)
		{
			if (m_MusicNotes[i] == null || !UniversalInputManager.Get().GetMouseButtonUp(0) || !IsOver(m_MusicNotes[i].m_CollisionObject))
			{
				continue;
			}
			m_MusicNotes[i].m_CollisionObject.GetComponent<PlayMakerFSM>().SendEvent(m_PlayedEvent);
			if (i == m_NoteSequence[m_CorrectNotesHit.Count])
			{
				m_CorrectNotesHit.Add(i);
				if (m_CorrectNotesHit.Count != m_NoteSequence.Count)
				{
					break;
				}
				PlayMakerFSM[] components = m_RewardCollisionObject.GetComponents<PlayMakerFSM>();
				foreach (PlayMakerFSM playMakerFSM in components)
				{
					if (playMakerFSM.FsmName == m_RewardFSMName)
					{
						playMakerFSM.SendEvent(m_RewardEvent);
					}
					else if (playMakerFSM.FsmName == m_ClickableFSMName)
					{
						playMakerFSM.SendEvent(m_ClickableEvent);
					}
				}
				m_CorrectNotesHit.Clear();
			}
			else
			{
				m_CorrectNotesHit.Clear();
				if (i == m_NoteSequence[0])
				{
					m_CorrectNotesHit.Add(i);
				}
			}
			break;
		}
	}

	private bool IsOver(GameObject go)
	{
		if (!go)
		{
			return false;
		}
		if (!InputUtil.IsPlayMakerMouseInputAllowed(go))
		{
			return false;
		}
		if (!UniversalInputManager.Get().InputIsOver(go))
		{
			return false;
		}
		return true;
	}
}
