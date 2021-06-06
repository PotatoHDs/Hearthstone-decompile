using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class AdventureClassChallengeButton : PegUIElement
{
	// Token: 0x060001B2 RID: 434 RVA: 0x0000A252 File Offset: 0x00008452
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6", base.gameObject);
		this.m_Highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000A27C File Offset: 0x0000847C
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		this.m_Highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000A28C File Offset: 0x0000848C
	public void Select(bool playSound)
	{
		if (playSound)
		{
			SoundManager.Get().LoadAndPlay("select_AI_opponent.prefab:a48887f01f79fa743a0c5de53a959b60", base.gameObject);
		}
		this.m_Highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		this.SetEnabled(false, false);
		this.Depress();
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000A2C7 File Offset: 0x000084C7
	public void Deselect()
	{
		this.m_Highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		this.Raise(0.1f);
		this.SetEnabled(true, false);
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000A2EA File Offset: 0x000084EA
	public void SetPortraitMaterial(Material portraitMat)
	{
		this.m_RootObject.GetComponent<Renderer>().SetMaterial(1, portraitMat);
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000A300 File Offset: 0x00008500
	private void Raise(float time)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_UpBone.localPosition,
			"time",
			time,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_RootObject, args);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x0000A374 File Offset: 0x00008574
	private void Depress()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_DownBone.localPosition,
			"time",
			0.1f,
			"easeType",
			iTween.EaseType.linear,
			"isLocal",
			true
		});
		iTween.MoveTo(this.m_RootObject, args);
	}

	// Token: 0x0400012B RID: 299
	public UberText m_Text;

	// Token: 0x0400012C RID: 300
	public int m_ScenarioID;

	// Token: 0x0400012D RID: 301
	public HighlightState m_Highlight;

	// Token: 0x0400012E RID: 302
	public GameObject m_RootObject;

	// Token: 0x0400012F RID: 303
	public GameObject m_Chest;

	// Token: 0x04000130 RID: 304
	public GameObject m_Checkmark;

	// Token: 0x04000131 RID: 305
	public Transform m_UpBone;

	// Token: 0x04000132 RID: 306
	public Transform m_DownBone;
}
