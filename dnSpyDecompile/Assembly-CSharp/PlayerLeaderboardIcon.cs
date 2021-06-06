using System;
using UnityEngine;

// Token: 0x02000334 RID: 820
public class PlayerLeaderboardIcon : MonoBehaviour
{
	// Token: 0x06002F14 RID: 12052 RVA: 0x000F0043 File Offset: 0x000EE243
	public void SetText(string text)
	{
		if (text == "")
		{
			this.ClearText();
		}
		else
		{
			this.m_text.gameObject.SetActive(true);
		}
		this.m_text.Text = text;
	}

	// Token: 0x06002F15 RID: 12053 RVA: 0x000F0077 File Offset: 0x000EE277
	public void ClearText()
	{
		this.m_text.Text = "";
		this.m_text.gameObject.SetActive(false);
	}

	// Token: 0x06002F16 RID: 12054 RVA: 0x000F009C File Offset: 0x000EE29C
	public void SetPlaymakerValue(string name, int value)
	{
		PlayMakerFSM component = base.gameObject.GetComponent<PlayMakerFSM>();
		if (component != null && component.FsmVariables.GetFsmInt(name) != null)
		{
			component.FsmVariables.GetFsmInt(name).Value = value;
			component.SendEvent("Action");
		}
	}

	// Token: 0x06002F17 RID: 12055 RVA: 0x000F00EC File Offset: 0x000EE2EC
	public void PlaymakerShow()
	{
		PlayMakerFSM component = base.gameObject.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.SetState("Show");
			return;
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002F18 RID: 12056 RVA: 0x000F0128 File Offset: 0x000EE328
	public bool PlaymakerIsShowing()
	{
		PlayMakerFSM component = base.gameObject.GetComponent<PlayMakerFSM>();
		return component != null && component.ActiveStateName == "Show";
	}

	// Token: 0x04001A43 RID: 6723
	public GameObject m_icon;

	// Token: 0x04001A44 RID: 6724
	public UberText m_text;

	// Token: 0x04001A45 RID: 6725
	private const string SHOW_PLAYMAKER_STATE = "Show";
}
