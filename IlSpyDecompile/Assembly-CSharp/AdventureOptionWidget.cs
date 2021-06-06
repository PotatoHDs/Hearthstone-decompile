using System;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[CustomEditClass]
public abstract class AdventureOptionWidget : MonoBehaviour
{
	public delegate void OptionAcknowledgedCallback();

	private const string OPTION_INTRO_COMPLETE = "CODE_OPTION_INTRO_COMPLETE";

	private const string OPTION_OUTRO_COMPLETE = "CODE_OPTION_OUTRO_COMPLETE";

	[CustomEditField(Sections = "Widget References")]
	public AsyncReference m_widgetReference;

	[CustomEditField(Sections = "Widget References")]
	public AsyncReference m_clickableReference;

	[CustomEditField(Sections = "Visual Controller State Names")]
	public string m_ControllerIntroStateName = "PLAY_MOTE_IN";

	[CustomEditField(Sections = "Visual Controller State Names")]
	public string m_ControllerOutroStateName = "PLAY_MOTE_OUT";

	protected long m_databaseId;

	protected bool m_isEnabled = true;

	protected bool m_isVisible = true;

	protected bool m_isIntroPlaying;

	protected bool m_isOutroPlaying;

	protected bool m_isClickableInitialized;

	protected AdventureLoadoutOptionDataModel m_dataModel = new AdventureLoadoutOptionDataModel();

	protected WidgetInstance m_widgetInstance;

	protected Clickable m_clickable;

	protected OptionAcknowledgedCallback m_acknowledgedCallback;

	protected Delegate m_selectedCallback;

	protected Delegate m_rolloverCallback;

	protected Delegate m_rolloutCallback;

	[CustomEditField(Sections = "Properties (Instance-Only)")]
	public bool IsNewlyUnlocked
	{
		get
		{
			return m_dataModel.NewlyUnlocked;
		}
		set
		{
			m_dataModel.NewlyUnlocked = value;
		}
	}

	[CustomEditField(Sections = "Properties (Instance-Only)")]
	public virtual bool IsReady
	{
		get
		{
			if (m_widgetInstance != null && m_widgetInstance.IsReady && !m_widgetInstance.IsChangingStates)
			{
				return m_clickable != null;
			}
			return false;
		}
	}

	[CustomEditField(Sections = "Properties (Read-Only)")]
	public bool IsIntroPlaying => m_isIntroPlaying;

	[CustomEditField(Sections = "Properties (Read-Only)")]
	public bool IsOutroPlaying => m_isOutroPlaying;

	private void Awake()
	{
		m_widgetReference.RegisterReadyListener<WidgetInstance>(OnWidgetInstanceReady);
	}

	protected virtual void OnWidgetInstanceReady(WidgetInstance widgetInstance)
	{
		if (widgetInstance == null)
		{
			Error.AddDevWarning("UI Issue!", "m_widgetReference is not set in the AdventureOptionWidget.cs! Cannot initialize its properties.");
			return;
		}
		m_widgetInstance = widgetInstance;
		m_widgetInstance.BindDataModel(m_dataModel);
		m_widgetInstance.TriggerEvent("SetUpState");
		m_widgetInstance.RegisterEventListener(OnIntroSequenceComplete);
		m_widgetInstance.RegisterEventListener(OnOutroSequenceComplete);
		SetVisible(isVisible: false);
	}

	protected virtual void OnClickableReady(Clickable clickable)
	{
		if (clickable == null)
		{
			Error.AddDevWarning("UI Issue!", "m_clickableReference is not set in the AdventureOptionWidget.cs! Cannot initialize its properties.");
			return;
		}
		m_clickable = clickable;
		SetInteractionEnabled(bEnable: false);
		m_clickable.AddEventListener(UIEventType.ROLLOVER, delegate
		{
			Rollover();
		});
		m_clickable.AddEventListener(UIEventType.ROLLOUT, delegate
		{
			Rollout();
		});
	}

	protected virtual void OnIntroFinished()
	{
		SetInteractionEnabled(bEnable: true);
		m_isIntroPlaying = false;
	}

	protected virtual void OnOutroFinished()
	{
		SetEnabled(isEnable: false);
		base.gameObject.SetActive(value: false);
		m_isOutroPlaying = false;
	}

	protected virtual void Rollover()
	{
		if (m_acknowledgedCallback == null)
		{
			Log.Adventures.PrintError("Attempting to invoke callback for the OptionAcknowledgedCallback, but no callback was provided!");
		}
		else
		{
			m_acknowledgedCallback();
		}
	}

	protected virtual void Rollout()
	{
	}

	private void OnIntroSequenceComplete(string eventName)
	{
		if (m_isIntroPlaying && !(eventName != "CODE_OPTION_INTRO_COMPLETE"))
		{
			OnIntroFinished();
		}
	}

	private void OnOutroSequenceComplete(string eventName)
	{
		if (m_isOutroPlaying && !(eventName != "CODE_OPTION_OUTRO_COMPLETE"))
		{
			OnOutroFinished();
		}
	}

	protected void InitWidget(string name, bool locked, string lockedText, bool upgraded, bool completed, bool newlyUnlocked, OptionAcknowledgedCallback acknowledgedCallback)
	{
		m_dataModel.Name = name;
		m_dataModel.Locked = locked;
		m_dataModel.LockedText = lockedText;
		m_dataModel.Completed = completed;
		m_dataModel.NewlyUnlocked = newlyUnlocked;
		m_dataModel.IsUpgraded = upgraded;
		if (m_widgetInstance != null)
		{
			m_widgetInstance.TriggerEvent("SetUpState");
		}
		m_acknowledgedCallback = acknowledgedCallback;
		Deselect();
		base.gameObject.SetActive(value: true);
	}

	protected void InitClickable()
	{
		if (!m_isClickableInitialized)
		{
			m_isClickableInitialized = true;
			m_clickableReference.RegisterReadyListener<Clickable>(OnClickableReady);
		}
	}

	public AdventureLoadoutOptionDataModel GetDataModel()
	{
		return m_dataModel;
	}

	public void SetOptionCallbacks(Delegate selectedCallback, Delegate rolloverCallback = null, Delegate rolloutCallback = null)
	{
		m_selectedCallback = selectedCallback;
		m_rolloverCallback = rolloverCallback;
		m_rolloutCallback = rolloutCallback;
		InitClickable();
	}

	public virtual void Select()
	{
		if (m_dataModel != null && !m_dataModel.Locked)
		{
			m_dataModel.IsSelectedOption = true;
		}
	}

	public void Deselect()
	{
		if (m_dataModel != null)
		{
			m_dataModel.IsSelectedOption = false;
		}
	}

	public virtual void PlayIntro()
	{
		m_isIntroPlaying = true;
		m_widgetInstance.TriggerEvent(m_ControllerIntroStateName);
		SetVisible(isVisible: true);
	}

	public virtual void PlayOutro()
	{
		SetInteractionEnabled(bEnable: false);
		m_widgetInstance.TriggerEvent(m_ControllerOutroStateName);
		m_isOutroPlaying = true;
	}

	public void SetEnabled(bool isEnable)
	{
		m_isEnabled = isEnable;
		SetInteractionEnabled(isEnable);
		SetVisible(isEnable);
	}

	public void SetInteractionEnabled(bool bEnable)
	{
		if (!(m_clickable == null))
		{
			m_clickable.Active = bEnable;
			Collider component = m_clickable.GetComponent<Collider>();
			if (component == null)
			{
				Debug.LogWarning("AdventureOptionWidget tried to disable interaction but couldn't find the collider component!");
			}
			else
			{
				component.enabled = bEnable;
			}
		}
	}

	public virtual void SetVisible(bool isVisible)
	{
		if (isVisible != m_isVisible)
		{
			m_isVisible = isVisible;
			if (isVisible)
			{
				m_widgetInstance.Show();
			}
			else
			{
				m_widgetInstance.Hide();
			}
		}
	}
}
