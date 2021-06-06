using Hearthstone.UI;
using UnityEngine;

public class PlayButton : PegUIElement
{
	public Vector3 m_pressMovement = new Vector3(0f, -0.9f, 0f);

	public UberText m_newPlayButtonText;

	public UberText m_playButtonSecondaryText;

	protected HighlightState m_playButtonHighlightState;

	private PlayMakerFSM m_fsm;

	private bool m_isStarted;

	private VisualController m_visualController;

	private const string PLAY_BUTTON_ENABLED_STATE = "ENABLED";

	private const string PLAY_BUTTON_DISABLED_STATE = "DISABLED";

	private const string PLAY_BUTTON_PRESSED_STATE = "PRESSED";

	private const string PLAY_BUTTON_RELEASED_STATE = "RELEASED";

	protected override void Awake()
	{
		base.Awake();
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().Load("play_button_mouseover.prefab:359a8482de643b141bb9afb5a351fe33");
		}
		m_playButtonHighlightState = base.gameObject.GetComponentInChildren<HighlightState>();
		SetOriginalLocalPosition();
	}

	protected void Start()
	{
		m_isStarted = true;
		m_fsm = GetComponent<PlayMakerFSM>();
		m_visualController = GetComponent<VisualController>();
		if (IsEnabled())
		{
			Enable();
		}
		else
		{
			Disable();
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		SoundManager.Get().LoadAndPlay("play_button_mouseover.prefab:359a8482de643b141bb9afb5a351fe33", base.gameObject);
		if (m_playButtonHighlightState != null)
		{
			m_playButtonHighlightState.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_MOUSE_OVER);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		iTween.MoveTo(base.gameObject, iTween.Hash("position", GetOriginalLocalPosition(), "isLocal", true, "time", 0.25f));
		if (m_playButtonHighlightState != null)
		{
			m_playButtonHighlightState.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		if (m_visualController != null)
		{
			m_visualController.SetState("RELEASED");
		}
	}

	public void ChangeHighlightState(ActorStateType stateType)
	{
		if (!(m_playButtonHighlightState == null))
		{
			m_playButtonHighlightState.ChangeState(stateType);
		}
	}

	public void Disable(bool keepLabelTextVisible = false)
	{
		SetEnabled(enabled: false);
		if (m_isStarted)
		{
			if (m_fsm != null && !keepLabelTextVisible)
			{
				m_fsm.SendEvent("Cancel");
			}
			if (m_playButtonHighlightState != null)
			{
				m_playButtonHighlightState.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			}
			if (m_visualController != null)
			{
				m_visualController.SetState("DISABLED");
			}
		}
	}

	public void Enable()
	{
		SetEnabled(enabled: true);
		m_newPlayButtonText.UpdateNow();
		if (m_isStarted)
		{
			if (m_newPlayButtonText != null)
			{
				m_newPlayButtonText.TextAlpha = 1f;
			}
			if (m_fsm != null)
			{
				m_fsm.SendEvent("Birth");
			}
			if (m_playButtonHighlightState != null)
			{
				m_playButtonHighlightState.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			if (m_visualController != null)
			{
				m_visualController.SetState("ENABLED");
			}
		}
	}

	protected override void OnPress()
	{
		Vector3 originalLocalPosition = GetOriginalLocalPosition();
		iTween.MoveTo(base.gameObject, iTween.Hash("position", originalLocalPosition + m_pressMovement, "isLocal", true, "time", 0.25f));
		ChangeHighlightState(ActorStateType.HIGHLIGHT_OFF);
		SoundManager.Get().LoadAndPlay("collection_manager_select_hero.prefab:248ea6ef307bf88468af342d2c2bd2e7");
		if (m_visualController != null)
		{
			m_visualController.SetState("PRESSED");
		}
	}

	protected override void OnRelease()
	{
		iTween.MoveTo(base.gameObject, iTween.Hash("position", GetOriginalLocalPosition(), "isLocal", true, "time", 0.25f));
		if (m_visualController != null)
		{
			m_visualController.SetState("RELEASED");
		}
	}

	public void SetText(string newText)
	{
		if (m_newPlayButtonText != null)
		{
			m_newPlayButtonText.Text = newText;
		}
	}

	public void SetSecondaryText(string newText)
	{
		if (m_playButtonSecondaryText != null)
		{
			m_playButtonSecondaryText.Text = newText;
		}
	}
}
