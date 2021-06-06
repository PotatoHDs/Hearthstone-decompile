using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000ACA RID: 2762
public class ArrowModeButton : PegUIElement
{
	// Token: 0x0600935F RID: 37727 RVA: 0x002FCA4E File Offset: 0x002FAC4E
	protected override void Awake()
	{
		base.Awake();
		SoundManager.Get().Load("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		SoundManager.Get().Load("deck_select_button_press.prefab:46d62875e039445439070cc9f7480d48");
	}

	// Token: 0x06009360 RID: 37728 RVA: 0x002FCA80 File Offset: 0x002FAC80
	public void Activate(bool activate)
	{
		if (activate == base.IsEnabled())
		{
			return;
		}
		this.SetEnabled(activate, false);
		if (!activate && this.m_highlight != null)
		{
			this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
		this.m_numFlips++;
		if (iTweenManager.Get().GetTweenForObject(base.gameObject) != null)
		{
			return;
		}
		this.Flip();
	}

	// Token: 0x06009361 RID: 37729 RVA: 0x002FCAE8 File Offset: 0x002FACE8
	public void ActivateHighlight(bool highlightOn)
	{
		if (this.m_highlight == null)
		{
			return;
		}
		this.m_isHighlighted = highlightOn;
		ActorStateType stateType = this.m_isHighlighted ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.HIGHLIGHT_OFF;
		this.m_highlight.ChangeState(stateType);
	}

	// Token: 0x06009362 RID: 37730 RVA: 0x002FCB27 File Offset: 0x002FAD27
	protected override void OnRelease()
	{
		if (this.m_highlight == null)
		{
			return;
		}
		SoundManager.Get().LoadAndPlay("deck_select_button_press.prefab:46d62875e039445439070cc9f7480d48");
		this.m_isHighlighted = false;
		this.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	// Token: 0x06009363 RID: 37731 RVA: 0x002FCB64 File Offset: 0x002FAD64
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9");
		if (this.m_highlight == null)
		{
			return;
		}
		ActorStateType stateType = this.m_isHighlighted ? ActorStateType.HIGHLIGHT_PRIMARY_MOUSE_OVER : ActorStateType.HIGHLIGHT_MOUSE_OVER;
		this.m_highlight.ChangeState(stateType);
	}

	// Token: 0x06009364 RID: 37732 RVA: 0x002FCBB0 File Offset: 0x002FADB0
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		if (this.m_highlight == null)
		{
			return;
		}
		ActorStateType stateType = this.m_isHighlighted ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.HIGHLIGHT_OFF;
		this.m_highlight.ChangeState(stateType);
	}

	// Token: 0x06009365 RID: 37733 RVA: 0x002FCBE8 File Offset: 0x002FADE8
	private void Flip()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			new Vector3(180f, 0f, 0f),
			"time",
			0.5f,
			"easeType",
			iTween.EaseType.easeOutElastic,
			"space",
			Space.Self,
			"oncomplete",
			"OnFlipComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateAdd(base.gameObject, args);
	}

	// Token: 0x06009366 RID: 37734 RVA: 0x002FCC8E File Offset: 0x002FAE8E
	private void OnFlipComplete()
	{
		this.m_numFlips--;
		if (this.m_numFlips > 0)
		{
			this.Flip();
		}
	}

	// Token: 0x04007B7E RID: 31614
	public HighlightState m_highlight;

	// Token: 0x04007B7F RID: 31615
	private int m_numFlips;

	// Token: 0x04007B80 RID: 31616
	private bool m_isHighlighted;
}
