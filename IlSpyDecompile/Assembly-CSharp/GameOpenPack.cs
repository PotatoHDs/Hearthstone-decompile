using System.Collections;
using UnityEngine;

public class GameOpenPack : MonoBehaviour
{
	public PlayMakerFSM m_playMakerFSM;

	private bool clickedOnPack;

	private bool fullyLoaded;

	public void Finish()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().GetGameEntity().NotifyOfCustomIntroFinished();
		}
	}

	public void PlayJainaLine()
	{
		GameState.Get().GetGameEntity().SendCustomEvent(66);
	}

	public void PlayHoggerLine()
	{
		_ = MulliganManager.Get() == null;
	}

	private IEnumerator PlayHoggerAfterVersus()
	{
		yield return new WaitForSeconds(1f);
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		SoundManager.Get().Play(heroCard.GetAnnouncerLine(Card.AnnouncerLineType.DEFAULT));
	}

	public void RaiseBoardLights()
	{
		Board.Get().RaiseTheLights();
	}

	public void Begin()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().GetGameEntity().NotifyOfGamePackOpened();
		}
	}

	public void NotifyOfFullyLoaded()
	{
		fullyLoaded = true;
	}

	public void NotifyOfMouseOver()
	{
		if (fullyLoaded && !clickedOnPack)
		{
			m_playMakerFSM.SendEvent("Birth");
		}
	}

	public void NotifyOfMouseOff()
	{
		if (fullyLoaded && !clickedOnPack)
		{
			m_playMakerFSM.SendEvent("Cancel");
		}
	}

	public void HandleClick()
	{
		if (fullyLoaded && !clickedOnPack && SceneMgr.Get().IsSceneLoaded() && (!(LoadingScreen.Get() != null) || !LoadingScreen.Get().IsTransitioning()))
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.Misc_Tutorial01PackOpen);
			clickedOnPack = true;
			m_playMakerFSM.SendEvent("Action");
		}
	}
}
