using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[CustomEditClass]
public class UNGMusic : MonoBehaviour
{
	// Token: 0x06000B47 RID: 2887 RVA: 0x000426A4 File Offset: 0x000408A4
	private void Start()
	{
		this.m_CorrectNotesHit.Clear();
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x000426B1 File Offset: 0x000408B1
	private void Update()
	{
		this.HandleHits();
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x000426BC File Offset: 0x000408BC
	private void HandleHits()
	{
		int i = 0;
		while (i < this.m_MusicNotes.Count)
		{
			if (this.m_MusicNotes[i] != null && UniversalInputManager.Get().GetMouseButtonUp(0) && this.IsOver(this.m_MusicNotes[i].m_CollisionObject))
			{
				this.m_MusicNotes[i].m_CollisionObject.GetComponent<PlayMakerFSM>().SendEvent(this.m_PlayedEvent);
				if (i == this.m_NoteSequence[this.m_CorrectNotesHit.Count])
				{
					this.m_CorrectNotesHit.Add(i);
					if (this.m_CorrectNotesHit.Count == this.m_NoteSequence.Count)
					{
						foreach (PlayMakerFSM playMakerFSM in this.m_RewardCollisionObject.GetComponents<PlayMakerFSM>())
						{
							if (playMakerFSM.FsmName == this.m_RewardFSMName)
							{
								playMakerFSM.SendEvent(this.m_RewardEvent);
							}
							else if (playMakerFSM.FsmName == this.m_ClickableFSMName)
							{
								playMakerFSM.SendEvent(this.m_ClickableEvent);
							}
						}
						this.m_CorrectNotesHit.Clear();
						return;
					}
					break;
				}
				else
				{
					this.m_CorrectNotesHit.Clear();
					if (i == this.m_NoteSequence[0])
					{
						this.m_CorrectNotesHit.Add(i);
						return;
					}
					break;
				}
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x000402EE File Offset: 0x0003E4EE
	private bool IsOver(GameObject go)
	{
		return go && InputUtil.IsPlayMakerMouseInputAllowed(go) && UniversalInputManager.Get().InputIsOver(go);
	}

	// Token: 0x0400078A RID: 1930
	public List<MusicNote> m_MusicNotes;

	// Token: 0x0400078B RID: 1931
	public string m_PlayedEvent;

	// Token: 0x0400078C RID: 1932
	public List<int> m_NoteSequence;

	// Token: 0x0400078D RID: 1933
	public GameObject m_RewardCollisionObject;

	// Token: 0x0400078E RID: 1934
	public string m_RewardFSMName;

	// Token: 0x0400078F RID: 1935
	public string m_RewardEvent;

	// Token: 0x04000790 RID: 1936
	public GameObject m_ClickableFSM;

	// Token: 0x04000791 RID: 1937
	public string m_ClickableFSMName;

	// Token: 0x04000792 RID: 1938
	public string m_ClickableEvent;

	// Token: 0x04000793 RID: 1939
	private List<int> m_CorrectNotesHit = new List<int>();
}
