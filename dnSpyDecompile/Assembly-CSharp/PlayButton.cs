using System;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000B27 RID: 2855
public class PlayButton : PegUIElement
{
	// Token: 0x0600976E RID: 38766 RVA: 0x0030F076 File Offset: 0x0030D276
	protected override void Awake()
	{
		base.Awake();
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().Load("play_button_mouseover.prefab:359a8482de643b141bb9afb5a351fe33");
		}
		this.m_playButtonHighlightState = base.gameObject.GetComponentInChildren<HighlightState>();
		base.SetOriginalLocalPosition();
	}

	// Token: 0x0600976F RID: 38767 RVA: 0x0030F0B1 File Offset: 0x0030D2B1
	protected void Start()
	{
		this.m_isStarted = true;
		this.m_fsm = base.GetComponent<PlayMakerFSM>();
		this.m_visualController = base.GetComponent<VisualController>();
		if (base.IsEnabled())
		{
			this.Enable();
			return;
		}
		this.Disable(false);
	}

	// Token: 0x06009770 RID: 38768 RVA: 0x0030F0E8 File Offset: 0x0030D2E8
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("play_button_mouseover.prefab:359a8482de643b141bb9afb5a351fe33", base.gameObject);
		if (this.m_playButtonHighlightState != null)
		{
			this.m_playButtonHighlightState.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_MOUSE_OVER);
		}
	}

	// Token: 0x06009771 RID: 38769 RVA: 0x0030F120 File Offset: 0x0030D320
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			base.GetOriginalLocalPosition(),
			"isLocal",
			true,
			"time",
			0.25f
		}));
		if (this.m_playButtonHighlightState != null)
		{
			this.m_playButtonHighlightState.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		if (this.m_visualController != null)
		{
			this.m_visualController.SetState("RELEASED");
		}
	}

	// Token: 0x06009772 RID: 38770 RVA: 0x0030F1BA File Offset: 0x0030D3BA
	public void ChangeHighlightState(ActorStateType stateType)
	{
		if (this.m_playButtonHighlightState == null)
		{
			return;
		}
		this.m_playButtonHighlightState.ChangeState(stateType);
	}

	// Token: 0x06009773 RID: 38771 RVA: 0x0030F1D8 File Offset: 0x0030D3D8
	public void Disable(bool keepLabelTextVisible = false)
	{
		this.SetEnabled(false, false);
		if (!this.m_isStarted)
		{
			return;
		}
		if (this.m_fsm != null && !keepLabelTextVisible)
		{
			this.m_fsm.SendEvent("Cancel");
		}
		if (this.m_playButtonHighlightState != null)
		{
			this.m_playButtonHighlightState.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (this.m_visualController != null)
		{
			this.m_visualController.SetState("DISABLED");
		}
	}

	// Token: 0x06009774 RID: 38772 RVA: 0x0030F254 File Offset: 0x0030D454
	public void Enable()
	{
		this.SetEnabled(true, false);
		this.m_newPlayButtonText.UpdateNow(false);
		if (!this.m_isStarted)
		{
			return;
		}
		if (this.m_newPlayButtonText != null)
		{
			this.m_newPlayButtonText.TextAlpha = 1f;
		}
		if (this.m_fsm != null)
		{
			this.m_fsm.SendEvent("Birth");
		}
		if (this.m_playButtonHighlightState != null)
		{
			this.m_playButtonHighlightState.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		if (this.m_visualController != null)
		{
			this.m_visualController.SetState("ENABLED");
		}
	}

	// Token: 0x06009775 RID: 38773 RVA: 0x0030F2F8 File Offset: 0x0030D4F8
	protected override void OnPress()
	{
		Vector3 originalLocalPosition = base.GetOriginalLocalPosition();
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			originalLocalPosition + this.m_pressMovement,
			"isLocal",
			true,
			"time",
			0.25f
		}));
		this.ChangeHighlightState(ActorStateType.HIGHLIGHT_OFF);
		SoundManager.Get().LoadAndPlay("collection_manager_select_hero.prefab:248ea6ef307bf88468af342d2c2bd2e7");
		if (this.m_visualController != null)
		{
			this.m_visualController.SetState("PRESSED");
		}
	}

	// Token: 0x06009776 RID: 38774 RVA: 0x0030F3A0 File Offset: 0x0030D5A0
	protected override void OnRelease()
	{
		iTween.MoveTo(base.gameObject, iTween.Hash(new object[]
		{
			"position",
			base.GetOriginalLocalPosition(),
			"isLocal",
			true,
			"time",
			0.25f
		}));
		if (this.m_visualController != null)
		{
			this.m_visualController.SetState("RELEASED");
		}
	}

	// Token: 0x06009777 RID: 38775 RVA: 0x0030F41E File Offset: 0x0030D61E
	public void SetText(string newText)
	{
		if (this.m_newPlayButtonText != null)
		{
			this.m_newPlayButtonText.Text = newText;
		}
	}

	// Token: 0x06009778 RID: 38776 RVA: 0x0030F43A File Offset: 0x0030D63A
	public void SetSecondaryText(string newText)
	{
		if (this.m_playButtonSecondaryText != null)
		{
			this.m_playButtonSecondaryText.Text = newText;
		}
	}

	// Token: 0x04007ECB RID: 32459
	public Vector3 m_pressMovement = new Vector3(0f, -0.9f, 0f);

	// Token: 0x04007ECC RID: 32460
	public UberText m_newPlayButtonText;

	// Token: 0x04007ECD RID: 32461
	public UberText m_playButtonSecondaryText;

	// Token: 0x04007ECE RID: 32462
	protected HighlightState m_playButtonHighlightState;

	// Token: 0x04007ECF RID: 32463
	private PlayMakerFSM m_fsm;

	// Token: 0x04007ED0 RID: 32464
	private bool m_isStarted;

	// Token: 0x04007ED1 RID: 32465
	private VisualController m_visualController;

	// Token: 0x04007ED2 RID: 32466
	private const string PLAY_BUTTON_ENABLED_STATE = "ENABLED";

	// Token: 0x04007ED3 RID: 32467
	private const string PLAY_BUTTON_DISABLED_STATE = "DISABLED";

	// Token: 0x04007ED4 RID: 32468
	private const string PLAY_BUTTON_PRESSED_STATE = "PRESSED";

	// Token: 0x04007ED5 RID: 32469
	private const string PLAY_BUTTON_RELEASED_STATE = "RELEASED";
}
