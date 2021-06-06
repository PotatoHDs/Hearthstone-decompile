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
	// Token: 0x02001165 RID: 4453
	public class MessagePopupDisplay : IService
	{
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x0600C2F5 RID: 49909 RVA: 0x003B0782 File Offset: 0x003AE982
		public bool HasMessageToDisplay
		{
			get
			{
				return this.m_messageQueue.Count > 0;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600C2F6 RID: 49910 RVA: 0x003B0792 File Offset: 0x003AE992
		// (set) Token: 0x0600C2F7 RID: 49911 RVA: 0x003B079A File Offset: 0x003AE99A
		public bool IsDisplayingMessage { get; private set; }

		// Token: 0x0600C2F8 RID: 49912 RVA: 0x003B07A3 File Offset: 0x003AE9A3
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			this.m_feedRegistry.RegisterDefaultFeeds();
			yield break;
		}

		// Token: 0x0600C2F9 RID: 49913 RVA: 0x003B07B2 File Offset: 0x003AE9B2
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(InGameMessageScheduler)
			};
		}

		// Token: 0x0600C2FA RID: 49914 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void Shutdown()
		{
		}

		// Token: 0x0600C2FB RID: 49915 RVA: 0x003B07C8 File Offset: 0x003AE9C8
		public void QueueMessage(MessageUIData data)
		{
			if (!string.IsNullOrEmpty(data.UID))
			{
				if (this.MessageIsBeingDisplayed(data))
				{
					Log.InGameMessage.PrintInfo("Message " + data.UID + " is already being display. Ignoring the message", Array.Empty<object>());
					return;
				}
				foreach (MessageUIData messageUIData in this.m_messageQueue)
				{
					if (this.MessagesHaveSameUID(messageUIData, data))
					{
						messageUIData.CopyValues(data);
						Log.InGameMessage.PrintInfo("Message " + data.UID + " was queued to display. Updating the stored values", Array.Empty<object>());
						return;
					}
				}
			}
			if (!MessageValidator.IsMessageValid(data))
			{
				Log.InGameMessage.PrintWarning("Invalid message given with UID " + data.UID + ". Ignoring message", Array.Empty<object>());
				return;
			}
			this.m_messageQueue.Enqueue(data);
		}

		// Token: 0x0600C2FC RID: 49916 RVA: 0x003B08C4 File Offset: 0x003AEAC4
		private bool MessageIsBeingDisplayed(MessageUIData data)
		{
			return this.m_currentlyDisplayedMessage != null && this.MessagesHaveSameUID(this.m_currentlyDisplayedMessage, data);
		}

		// Token: 0x0600C2FD RID: 49917 RVA: 0x003B08DD File Offset: 0x003AEADD
		private bool MessagesHaveSameUID(MessageUIData first, MessageUIData second)
		{
			return !string.IsNullOrEmpty(first.UID) && !string.IsNullOrEmpty(second.UID) && first.UID.Equals(second.UID);
		}

		// Token: 0x0600C2FE RID: 49918 RVA: 0x003B090C File Offset: 0x003AEB0C
		public void DisplayNextMessage(Action onClosed)
		{
			if (this.IsDisplayingMessage)
			{
				Log.InGameMessage.PrintWarning("Attempted to display the an in game message while one is already being shown", Array.Empty<object>());
				return;
			}
			this.IsDisplayingMessage = true;
			MessageUIData messageUIData = this.m_messageQueue.Dequeue();
			UIMessageCallbacks callbacks = messageUIData.Callbacks;
			callbacks.OnClosed = (Action)Delegate.Combine(callbacks.OnClosed, new Action(this.OnMessageClosed));
			if (onClosed != null)
			{
				UIMessageCallbacks callbacks2 = messageUIData.Callbacks;
				callbacks2.OnClosed = (Action)Delegate.Combine(callbacks2.OnClosed, onClosed);
			}
			Processor.RunCoroutine(this.DisplayMessageWhenReady(messageUIData), null);
		}

		// Token: 0x0600C2FF RID: 49919 RVA: 0x003B099D File Offset: 0x003AEB9D
		private IEnumerator DisplayMessageWhenReady(MessageUIData dataToDisplay)
		{
			this.m_currentlyDisplayedMessage = dataToDisplay;
			this.ActivateFullscreenBlur();
			if (this.m_modalWidget == null)
			{
				this.CreateModal();
			}
			while (this.m_modalWidget == null || !this.m_modalWidget.IsReady || this.m_messageModal == null)
			{
				yield return null;
			}
			try
			{
				this.m_messageModal.SetMessage(dataToDisplay);
				this.m_modalWidget.Show();
				yield break;
			}
			catch (Exception ex)
			{
				Log.InGameMessage.PrintError("Exception showing IGM. Forcing close: " + ex.Message, Array.Empty<object>());
				if (dataToDisplay.Callbacks.OnClosed != null)
				{
					dataToDisplay.Callbacks.OnClosed();
				}
				else
				{
					this.OnMessageClosed();
				}
				ExceptionReporter exceptionReporter = ExceptionReporter.Get();
				if (exceptionReporter != null)
				{
					exceptionReporter.ReportCaughtException(ex.Message, ex.StackTrace);
				}
				yield break;
			}
			yield break;
		}

		// Token: 0x0600C300 RID: 49920 RVA: 0x003B09B3 File Offset: 0x003AEBB3
		private void OnModalWidgetReady(object _)
		{
			if (this.m_modalWidget != null)
			{
				this.InitializeModalWidget(this.m_modalWidget);
			}
		}

		// Token: 0x0600C301 RID: 49921 RVA: 0x003B09CF File Offset: 0x003AEBCF
		private void InitializeModalWidget(Widget modalWidget)
		{
			this.m_messageModal = modalWidget.GetComponentInChildren<MessageModal>();
			if (this.m_messageModal == null)
			{
				Log.InGameMessage.PrintError("Could not find Message Modal component. IGM will not function!", Array.Empty<object>());
				return;
			}
		}

		// Token: 0x0600C302 RID: 49922 RVA: 0x003B0A00 File Offset: 0x003AEC00
		private void CreateModal()
		{
			this.m_modalWidget = WidgetInstance.Create(MessagePopupDisplay.m_modalMessageReference, false);
			this.m_modalWidget.RegisterReadyListener(new Action<object>(this.OnModalWidgetReady), null, true);
			OverlayUI.Get().AddGameObject(this.m_modalWidget.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		}

		// Token: 0x0600C303 RID: 49923 RVA: 0x003B0A54 File Offset: 0x003AEC54
		private void OnMessageClosed()
		{
			this.m_currentlyDisplayedMessage = null;
			this.DeactivateFullscreenBlur();
			this.DestroyModal();
			this.IsDisplayingMessage = false;
		}

		// Token: 0x0600C304 RID: 49924 RVA: 0x003B0A70 File Offset: 0x003AEC70
		private void DestroyModal()
		{
			if (this.m_modalWidget)
			{
				UnityEngine.Object.Destroy(this.m_modalWidget);
				this.m_modalWidget = null;
				this.m_messageModal = null;
			}
		}

		// Token: 0x0600C305 RID: 49925 RVA: 0x003B0A98 File Offset: 0x003AEC98
		private void ActivateFullscreenBlur()
		{
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (fullScreenFXMgr == null)
			{
				return;
			}
			fullScreenFXMgr.StartStandardBlurVignette(1f);
		}

		// Token: 0x0600C306 RID: 49926 RVA: 0x003B0AAE File Offset: 0x003AECAE
		private void DeactivateFullscreenBlur()
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(1f, null);
		}

		// Token: 0x04009CD8 RID: 40152
		private static readonly AssetReference m_modalMessageReference = new AssetReference("MessageModal.prefab:7d258ca7826c5ba4c8e86d37eb6e909d");

		// Token: 0x04009CD9 RID: 40153
		private Widget m_modalWidget;

		// Token: 0x04009CDA RID: 40154
		private MessageModal m_messageModal;

		// Token: 0x04009CDB RID: 40155
		private MessageUIData m_currentlyDisplayedMessage;

		// Token: 0x04009CDC RID: 40156
		private Queue<MessageUIData> m_messageQueue = new Queue<MessageUIData>();

		// Token: 0x04009CDD RID: 40157
		private MessageFeedRegistry m_feedRegistry = new MessageFeedRegistry();

		// Token: 0x04009CDE RID: 40158
		private const float BLUR_FADE_TIME = 1f;
	}
}
