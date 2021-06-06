using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005D7 RID: 1495
public class GameOpenPack : MonoBehaviour
{
	// Token: 0x060051A2 RID: 20898 RVA: 0x001ACEA9 File Offset: 0x001AB0A9
	public void Finish()
	{
		if (GameState.Get() == null)
		{
			return;
		}
		GameState.Get().GetGameEntity().NotifyOfCustomIntroFinished();
	}

	// Token: 0x060051A3 RID: 20899 RVA: 0x001ACEC2 File Offset: 0x001AB0C2
	public void PlayJainaLine()
	{
		GameState.Get().GetGameEntity().SendCustomEvent(66);
	}

	// Token: 0x060051A4 RID: 20900 RVA: 0x001ACED5 File Offset: 0x001AB0D5
	public void PlayHoggerLine()
	{
		MulliganManager.Get() == null;
	}

	// Token: 0x060051A5 RID: 20901 RVA: 0x001ACEE3 File Offset: 0x001AB0E3
	private IEnumerator PlayHoggerAfterVersus()
	{
		yield return new WaitForSeconds(1f);
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		SoundManager.Get().Play(heroCard.GetAnnouncerLine(Card.AnnouncerLineType.DEFAULT), null, null, null);
		yield break;
	}

	// Token: 0x060051A6 RID: 20902 RVA: 0x001ACEEB File Offset: 0x001AB0EB
	public void RaiseBoardLights()
	{
		Board.Get().RaiseTheLights();
	}

	// Token: 0x060051A7 RID: 20903 RVA: 0x001ACEF7 File Offset: 0x001AB0F7
	public void Begin()
	{
		if (GameState.Get() == null)
		{
			return;
		}
		GameState.Get().GetGameEntity().NotifyOfGamePackOpened();
	}

	// Token: 0x060051A8 RID: 20904 RVA: 0x001ACF10 File Offset: 0x001AB110
	public void NotifyOfFullyLoaded()
	{
		this.fullyLoaded = true;
	}

	// Token: 0x060051A9 RID: 20905 RVA: 0x001ACF19 File Offset: 0x001AB119
	public void NotifyOfMouseOver()
	{
		if (!this.fullyLoaded)
		{
			return;
		}
		if (this.clickedOnPack)
		{
			return;
		}
		this.m_playMakerFSM.SendEvent("Birth");
	}

	// Token: 0x060051AA RID: 20906 RVA: 0x001ACF3D File Offset: 0x001AB13D
	public void NotifyOfMouseOff()
	{
		if (!this.fullyLoaded)
		{
			return;
		}
		if (this.clickedOnPack)
		{
			return;
		}
		this.m_playMakerFSM.SendEvent("Cancel");
	}

	// Token: 0x060051AB RID: 20907 RVA: 0x001ACF64 File Offset: 0x001AB164
	public void HandleClick()
	{
		if (!this.fullyLoaded)
		{
			return;
		}
		if (this.clickedOnPack)
		{
			return;
		}
		if (!SceneMgr.Get().IsSceneLoaded())
		{
			return;
		}
		if (LoadingScreen.Get() != null && LoadingScreen.Get().IsTransitioning())
		{
			return;
		}
		MusicManager.Get().StartPlaylist(MusicPlaylistType.Misc_Tutorial01PackOpen);
		this.clickedOnPack = true;
		this.m_playMakerFSM.SendEvent("Action");
	}

	// Token: 0x0400492D RID: 18733
	public PlayMakerFSM m_playMakerFSM;

	// Token: 0x0400492E RID: 18734
	private bool clickedOnPack;

	// Token: 0x0400492F RID: 18735
	private bool fullyLoaded;
}
