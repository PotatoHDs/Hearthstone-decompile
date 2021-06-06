using System;
using System.Collections.Generic;
using Hearthstone.UI;
using UnityEngine;

public class DuelsPlayMat : MonoBehaviour
{
	public AsyncReference m_livesReference;

	public AsyncReference m_vaultReference;

	public AsyncReference m_vaultLeverReference;

	private Clickable m_leverButton;

	private Widget m_livesWidget;

	private Widget m_vaultWidget;

	private bool m_livesWidgetLoaded;

	private bool m_vaultWidgetLoaded;

	private List<Action> m_vaultDoorOpenedListeners;

	private List<Action> m_vaultDoorClickedListeners;

	public void Start()
	{
		m_livesReference.RegisterReadyListener<Widget>(OnLivesWidgetReady);
		m_vaultReference.RegisterReadyListener<Widget>(OnVaultWidgetReady);
		m_vaultLeverReference.RegisterReadyListener<Clickable>(OnLeverClickableReady);
		m_vaultDoorOpenedListeners = new List<Action>();
		m_vaultDoorClickedListeners = new List<Action>();
	}

	public bool IsReady()
	{
		if (m_livesWidgetLoaded)
		{
			return m_vaultWidgetLoaded;
		}
		return false;
	}

	public void SetLeverButtonEnabled(bool enabled)
	{
		m_leverButton.enabled = enabled;
		if (enabled)
		{
			m_leverButton.GetComponent<VisualController>().SetState(DuelsConfig.LEVER_GLOW_STATE);
		}
	}

	public void RegisterVaultDoorOpenedListener(Action a)
	{
		if (!m_vaultDoorOpenedListeners.Contains(a))
		{
			m_vaultDoorOpenedListeners.Add(a);
		}
	}

	public void RemoveVaultDoorOpenedListener(Action a)
	{
		m_vaultDoorOpenedListeners.Remove(a);
	}

	public void OnVaultDoorOpened()
	{
		for (int i = 0; i < m_vaultDoorOpenedListeners.Count; i++)
		{
			m_vaultDoorOpenedListeners[i]();
		}
	}

	public void RegisterVaultDoorClickedListener(Action a)
	{
		if (!m_vaultDoorClickedListeners.Contains(a))
		{
			m_vaultDoorClickedListeners.Add(a);
		}
	}

	public void RemoveVaultDoorClickedListener(Action a)
	{
		m_vaultDoorClickedListeners.Remove(a);
	}

	public void OnVaultDoorClicked()
	{
		for (int i = 0; i < m_vaultDoorClickedListeners.Count; i++)
		{
			m_vaultDoorClickedListeners[i]();
		}
	}

	private void OnLivesWidgetReady(Widget w)
	{
		m_livesWidget = w;
		m_livesWidget.RegisterDoneChangingStatesListener(OnLivesWidgetDoneChangingStates);
	}

	private void OnLivesWidgetDoneChangingStates(object obj)
	{
		m_livesWidgetLoaded = true;
		m_livesWidget.RemoveStartChangingStatesListener(OnLivesWidgetDoneChangingStates);
	}

	private void OnVaultWidgetReady(Widget w)
	{
		m_vaultWidget = w;
		m_vaultWidget.RegisterDoneChangingStatesListener(OnLivesWidgetDoneChangingStates);
		m_vaultWidget.RegisterEventListener(OnVaultEvent);
	}

	private void OnVaultWidgetDoneChangingStates(object obj)
	{
		m_vaultWidgetLoaded = true;
		m_vaultWidget.RemoveStartChangingStatesListener(OnLivesWidgetDoneChangingStates);
	}

	private void OnVaultEvent(string eventName)
	{
		if (eventName == DuelsConfig.DOOR_OPENED_EVENT)
		{
			OnVaultDoorOpened();
		}
		else if (eventName == DuelsConfig.DOOR_LEVEL_CLICKED)
		{
			OnVaultDoorClicked();
		}
	}

	private void OnLeverClickableReady(Clickable c)
	{
		m_leverButton = c;
	}
}
