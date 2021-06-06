using HutongGames.PlayMaker.Actions;
using UnityEngine;

public class iTweenFSMEvents : MonoBehaviour
{
	public static int itweenIDCount;

	public int itweenID;

	public iTweenFsmAction itweenFSMAction;

	public bool donotfinish;

	public bool islooping;

	public string targetGameObjectName;

	private void iTweenOnStart(int aniTweenID)
	{
		if (itweenID == aniTweenID && itweenFSMAction != null && itweenFSMAction.Fsm != null)
		{
			itweenFSMAction.Fsm.Event(itweenFSMAction.startEvent);
		}
	}

	private void iTweenOnComplete(int aniTweenID)
	{
		if (itweenID != aniTweenID || itweenFSMAction == null || itweenFSMAction.Fsm == null)
		{
			return;
		}
		if (islooping)
		{
			if (!donotfinish)
			{
				itweenFSMAction.Fsm.Event(itweenFSMAction.finishEvent);
				itweenFSMAction.Finish();
			}
		}
		else
		{
			itweenFSMAction.Fsm.Event(itweenFSMAction.finishEvent);
			itweenFSMAction.Finish();
		}
	}
}
