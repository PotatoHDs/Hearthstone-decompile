using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.BlizzardErrorMobile;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.InGameMessage.UI
{
	public class MessagePopupDisplay : IService
	{
		private static readonly AssetReference m_modalMessageReference = new AssetReference("MessageModal.prefab:7d258ca7826c5ba4c8e86d37eb6e909d");

		private Widget m_modalWidget;

		private MessageModal m_messageModal;

		private MessageUIData m_currentlyDisplayedMessage;

		private Queue<MessageUIData> m_messageQueue = new Queue<MessageUIData>();

		private MessageFeedRegistry m_feedRegistry = new MessageFeedRegistry();

		private const float BLUR_FADE_TIME = 1f;

		public bool HasMessageToDisplay => m_messageQueue.Count > 0;

		public bool IsDisplayingMessage { get; private set; }

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			m_feedRegistry.RegisterDefaultFeeds();
			yield break;
		}

		public Type[] GetDependencies()
		{
			return new Type[1] { typeof(InGameMessageScheduler) };
		}

		public void Shutdown()
		{
		}

		public void QueueMessage(MessageUIData data)
		{
			if (!string.IsNullOrEmpty(data.UID))
			{
				if (MessageIsBeingDisplayed(data))
				{
					Log.InGameMessage.PrintInfo("Message " + data.UID + " is already being display. Ignoring the message");
					return;
				}
				foreach (MessageUIData item in m_messageQueue)
				{
					if (MessagesHaveSameUID(item, data))
					{
						item.CopyValues(data);
						Log.InGameMessage.PrintInfo("Message " + data.UID + " was queued to display. Updating the stored values");
						return;
					}
				}
			}
			if (!MessageValidator.IsMessageValid(data))
			{
				Log.InGameMessage.PrintWarning("Invalid message given with UID " + data.UID + ". Ignoring message");
			}
			else
			{
				m_messageQueue.Enqueue(data);
			}
		}

		private bool MessageIsBeingDisplayed(MessageUIData data)
		{
			if (m_currentlyDisplayedMessage != null)
			{
				return MessagesHaveSameUID(m_currentlyDisplayedMessage, data);
			}
			return false;
		}

		private bool MessagesHaveSameUID(MessageUIData first, MessageUIData second)
		{
			if (!string.IsNullOrEmpty(first.UID) && !string.IsNullOrEmpty(second.UID))
			{
				return first.UID.Equals(second.UID);
			}
			return false;
		}

		public void DisplayNextMessage(Action onClosed)
		{
			if (IsDisplayingMessage)
			{
				Log.InGameMessage.PrintWarning("Attempted to display the an in game message while one is already being shown");
				return;
			}
			IsDisplayingMessage = true;
			MessageUIData messageUIData = m_messageQueue.Dequeue();
			UIMessageCallbacks callbacks = messageUIData.Callbacks;
			callbacks.OnClosed = (Action)Delegate.Combine(callbacks.OnClosed, new Action(OnMessageClosed));
			if (onClosed != null)
			{
				UIMessageCallbacks callbacks2 = messageUIData.Callbacks;
				callbacks2.OnClosed = (Action)Delegate.Combine(callbacks2.OnClosed, onClosed);
			}
			Processor.RunCoroutine(DisplayMessageWhenReady(messageUIData));
		}

		private IEnumerator DisplayMessageWhenReady(MessageUIData dataToDisplay)
		{
			m_currentlyDisplayedMessage = dataToDisplay;
			ActivateFullscreenBlur();
			if (m_modalWidget == null)
			{
				CreateModal();
			}
			while (m_modalWidget == null || !m_modalWidget.IsReady || m_messageModal == null)
			{
				yield return null;
			}
			try
			{
				m_messageModal.SetMessage(dataToDisplay);
				m_modalWidget.Show();
			}
			catch (Exception ex)
			{
				Log.InGameMessage.PrintError("Exception showing IGM. Forcing close: " + ex.Message);
				if (dataToDisplay.Callbacks.OnClosed != null)
				{
					dataToDisplay.Callbacks.OnClosed();
				}
				else
				{
					OnMessageClosed();
				}
				ExceptionReporter.Get()?.ReportCaughtException(ex.Message, ex.StackTrace);
			}
		}

		private void OnModalWidgetReady(object _)
		{
			if (m_modalWidget != null)
			{
				InitializeModalWidget(m_modalWidget);
			}
		}

		private void InitializeModalWidget(Widget modalWidget)
		{
			m_messageModal = modalWidget.GetComponentInChildren<MessageModal>();
			if (m_messageModal == null)
			{
				Log.InGameMessage.PrintError("Could not find Message Modal component. IGM will not function!");
			}
		}

		private void CreateModal()
		{
			m_modalWidget = WidgetInstance.Create(m_modalMessageReference);
			m_modalWidget.RegisterReadyListener(OnModalWidgetReady);
			OverlayUI.Get().AddGameObject(m_modalWidget.gameObject);
		}

		private void OnMessageClosed()
		{
			m_currentlyDisplayedMessage = null;
			DeactivateFullscreenBlur();
			DestroyModal();
			IsDisplayingMessage = false;
		}

		private void DestroyModal()
		{
			if ((bool)m_modalWidget)
			{
				UnityEngine.Object.Destroy(m_modalWidget);
				m_modalWidget = null;
				m_messageModal = null;
			}
		}

		private void ActivateFullscreenBlur()
		{
			FullScreenFXMgr.Get()?.StartStandardBlurVignette(1f);
		}

		private void DeactivateFullscreenBlur()
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(1f);
		}
	}
}
