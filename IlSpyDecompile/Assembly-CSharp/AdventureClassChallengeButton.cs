using System.Collections;
using UnityEngine;

public class AdventureClassChallengeButton : PegUIElement
{
	public UberText m_Text;

	public int m_ScenarioID;

	public HighlightState m_Highlight;

	public GameObject m_RootObject;

	public GameObject m_Chest;

	public GameObject m_Checkmark;

	public Transform m_UpBone;

	public Transform m_DownBone;

	protected override void OnOver(InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		m_Highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
	}

	protected override void OnOut(InteractionState oldState)
	{
		m_Highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	public void Select(bool playSound)
	{
		if (playSound)
		{
			SoundManager.Get().LoadAndPlay("select_AI_opponent.prefab:a48887f01f79fa743a0c5de53a959b60", base.gameObject);
		}
		m_Highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		SetEnabled(enabled: false);
		Depress();
	}

	public void Deselect()
	{
		m_Highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		Raise(0.1f);
		SetEnabled(enabled: true);
	}

	public void SetPortraitMaterial(Material portraitMat)
	{
		m_RootObject.GetComponent<Renderer>().SetMaterial(1, portraitMat);
	}

	private void Raise(float time)
	{
		Hashtable args = iTween.Hash("position", m_UpBone.localPosition, "time", time, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo(m_RootObject, args);
	}

	private void Depress()
	{
		Hashtable args = iTween.Hash("position", m_DownBone.localPosition, "time", 0.1f, "easeType", iTween.EaseType.linear, "isLocal", true);
		iTween.MoveTo(m_RootObject, args);
	}
}
