using System;
using System.Collections;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class InfoPopupMulliganIntro
{
	// Token: 0x06002CE9 RID: 11497 RVA: 0x000E231F File Offset: 0x000E051F
	public void Show(MonoBehaviour monoBehaviour, string introPopupWidgetName, string boardBoneName, bool skipPopup = false)
	{
		EndTurnButton.Get().AddInputBlocker();
		this.SetHeroLit(GameState.Get().GetFriendlySidePlayer());
		this.SetHeroLit(GameState.Get().GetOpposingSidePlayer());
		monoBehaviour.StartCoroutine(this.ShowPopup(introPopupWidgetName, boardBoneName, skipPopup));
	}

	// Token: 0x06002CEA RID: 11498 RVA: 0x000E235C File Offset: 0x000E055C
	protected void SetHeroLit(Player player)
	{
		player.GetHeroCard().GetActor().SetLit();
	}

	// Token: 0x06002CEB RID: 11499 RVA: 0x000E236E File Offset: 0x000E056E
	protected IEnumerator ShowPopup(string introPopupWidgetName, string boardBoneName, bool skipPopup = false)
	{
		SceneMgr.Get().NotifySceneLoaded();
		while (LoadingScreen.Get().IsPreviousSceneActive() || LoadingScreen.Get().IsFadingOut())
		{
			yield return null;
		}
		GameMgr.Get().UpdatePresence();
		if (!skipPopup)
		{
			this.m_introPopup = WidgetInstance.Create(introPopupWidgetName, false);
			if (!this.m_introPopup)
			{
				yield break;
			}
			while (!this.m_introPopup.IsReady)
			{
				yield return null;
			}
			Vector3 position = Board.Get().FindBone(boardBoneName).position;
			this.m_introPopup.transform.localPosition = position;
			this.m_introSpell = this.m_introPopup.GetComponentInChildren<Spell>();
			if (!this.m_introSpell)
			{
				yield break;
			}
			this.m_introSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnIntroSpellStateFinished));
			this.m_introSpell.ActivateState(SpellStateType.BIRTH);
			while (!this.m_introSpell.IsFinished())
			{
				yield return null;
			}
		}
		Board.Get().RaiseTheLights();
		EndTurnButton.Get().RemoveInputBlocker();
		TurnStartManager.Get().BeginListeningForTurnEvents(false);
		MulliganManager.Get().SkipMulligan();
		yield break;
	}

	// Token: 0x06002CEC RID: 11500 RVA: 0x000E2392 File Offset: 0x000E0592
	private void OnIntroSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (this.m_introSpell.GetActiveState() == SpellStateType.NONE)
		{
			UnityEngine.Object.Destroy(this.m_introSpell);
			this.m_introSpell = null;
			UnityEngine.Object.Destroy(this.m_introPopup);
			this.m_introPopup = null;
		}
	}

	// Token: 0x040018D3 RID: 6355
	private WidgetInstance m_introPopup;

	// Token: 0x040018D4 RID: 6356
	private Spell m_introSpell;
}
