using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class DoneChangingEventHandler : MonoBehaviour
{
	public const string WAIT_FOR_DONE_CHANGING = "CODE_WAIT_FOR_DONE_CHANGING";

	public const string DISMISS_FROM_FOREGROUND = "CODE_DISMISS_FROM_FOREGROUND";

	public const string FINISHED_CHANGING = "CODE_DONE_CHANGING";

	[Tooltip("Hide this widget visually until it is done changing states.")]
	[SerializeField]
	private bool m_isInvisibleWhileChanging = true;

	[Tooltip("Blur the background behind this widget.")]
	[SerializeField]
	private bool m_useBackgroundBlur = true;

	private WidgetTemplate m_widget;

	private bool m_isWaiting;

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(delegate(string eventName)
		{
			if (!(eventName == "CODE_WAIT_FOR_DONE_CHANGING"))
			{
				if (eventName == "CODE_DISMISS_FROM_FOREGROUND")
				{
					DismissFromForeground();
				}
			}
			else
			{
				ShowWhenDone();
			}
		});
	}

	private void ShowWhenDone()
	{
		if (m_isWaiting)
		{
			return;
		}
		m_isWaiting = true;
		if (m_isInvisibleWhileChanging)
		{
			m_widget.Hide();
		}
		m_widget.RegisterDoneChangingStatesListener(delegate
		{
			m_widget.TriggerEvent("CODE_DONE_CHANGING", new Widget.TriggerEventParameters
			{
				NoDownwardPropagation = true
			});
			if (m_isInvisibleWhileChanging)
			{
				m_widget.Show();
			}
			if (m_useBackgroundBlur)
			{
				UIContext.GetRoot().ShowPopup(base.gameObject);
			}
			m_isWaiting = false;
		}, null, callImmediatelyIfSet: true, doOnce: true);
	}

	private void DismissFromForeground()
	{
		if (m_useBackgroundBlur)
		{
			UIContext.GetRoot().DismissPopup(base.gameObject);
		}
	}
}
