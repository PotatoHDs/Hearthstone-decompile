using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.InGameMessage.UI
{
	// Token: 0x02001164 RID: 4452
	public class MessageModal : MonoBehaviour
	{
		// Token: 0x0600C2EA RID: 49898 RVA: 0x003B050A File Offset: 0x003AE70A
		private void Awake()
		{
			this.m_contentWidget = base.GetComponent<Widget>();
			this.m_contentWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnEventMessage));
		}

		// Token: 0x0600C2EB RID: 49899 RVA: 0x003B052F File Offset: 0x003AE72F
		public void SetMessage(MessageUIData data)
		{
			if (data == null)
			{
				Log.InGameMessage.PrintWarning("Failed to set In game message, data was null", Array.Empty<object>());
				return;
			}
			this.SetModalDataModel(data);
			this.SetContentDataModel(data);
			this.SetShopSettingsIfAvailable(data);
			this.m_callbacks = data.Callbacks;
		}

		// Token: 0x0600C2EC RID: 49900 RVA: 0x003B056C File Offset: 0x003AE76C
		private void SetShopSettingsIfAvailable(MessageUIData data)
		{
			if (data.ContentType == MessageContentType.SHOP)
			{
				ShopMessageContent shopMessageContent = data.MessageData as ShopMessageContent;
				this.m_productId = shopMessageContent.ProductID;
				this.m_openFullShop = shopMessageContent.OpenFullShop;
			}
		}

		// Token: 0x0600C2ED RID: 49901 RVA: 0x003B05A8 File Offset: 0x003AE7A8
		private void SetContentDataModel(MessageUIData data)
		{
			List<IDataModel> list = MessageDataModelFactory.CreateDataModel(data);
			if (list == null)
			{
				Log.InGameMessage.PrintError("Could not create a content model for IGM data", Array.Empty<object>());
				return;
			}
			if (this.m_contentWidget == null)
			{
				Log.InGameMessage.PrintError("Missing Content Widget! IGM will not work correctly", Array.Empty<object>());
				return;
			}
			foreach (IDataModel dataModel in list)
			{
				this.m_contentWidget.BindDataModel(dataModel, false);
			}
		}

		// Token: 0x0600C2EE RID: 49902 RVA: 0x003B0640 File Offset: 0x003AE840
		private void SetModalDataModel(MessageUIData data)
		{
			MessageModalDataModel messageModalDataModel = new MessageModalDataModel();
			messageModalDataModel.ContentType = this.GetContentTypeID(data.ContentType);
			this.m_contentWidget.BindDataModel(messageModalDataModel, false);
		}

		// Token: 0x0600C2EF RID: 49903 RVA: 0x003B0674 File Offset: 0x003AE874
		private string GetContentTypeID(MessageContentType type)
		{
			string result;
			if (MessageModal.CONTENT_ID_MAP.TryGetValue(type, out result))
			{
				return result;
			}
			return "UNKNOWN";
		}

		// Token: 0x0600C2F0 RID: 49904 RVA: 0x003B0697 File Offset: 0x003AE897
		public void OnEventMessage(string eventName)
		{
			if (eventName.Equals("FinalalizeCloseButtonPress", StringComparison.OrdinalIgnoreCase))
			{
				this.OnClosePressed();
				return;
			}
			if (eventName.Equals("ShopButtonPressed", StringComparison.OrdinalIgnoreCase))
			{
				this.OnOpenStoreButton();
			}
		}

		// Token: 0x0600C2F1 RID: 49905 RVA: 0x003B06C2 File Offset: 0x003AE8C2
		public void OnClosePressed()
		{
			Log.InGameMessage.PrintDebug("Modal close button pressed", Array.Empty<object>());
			UIMessageCallbacks callbacks = this.m_callbacks;
			if (callbacks == null)
			{
				return;
			}
			callbacks.OnClosed();
		}

		// Token: 0x0600C2F2 RID: 49906 RVA: 0x003B06F0 File Offset: 0x003AE8F0
		private void OnOpenStoreButton()
		{
			Log.InGameMessage.PrintDebug(string.Format("Opening shop page for product ID {0}", this.m_productId), Array.Empty<object>());
			Shop.OpenToProductPageWhenReady(this.m_productId, !this.m_openFullShop);
			UIMessageCallbacks callbacks = this.m_callbacks;
			if (callbacks != null)
			{
				callbacks.OnStoreOpened();
			}
			this.OnClosePressed();
		}

		// Token: 0x04009CCF RID: 40143
		private Widget m_contentWidget;

		// Token: 0x04009CD0 RID: 40144
		private UIMessageCallbacks m_callbacks;

		// Token: 0x04009CD1 RID: 40145
		private long m_productId;

		// Token: 0x04009CD2 RID: 40146
		private bool m_openFullShop;

		// Token: 0x04009CD3 RID: 40147
		private const string SHOP_BUTTON_EVENT = "ShopButtonPressed";

		// Token: 0x04009CD4 RID: 40148
		private const string FINAL_CLOSE_BUTTON_EVENT = "FinalalizeCloseButtonPress";

		// Token: 0x04009CD5 RID: 40149
		private static readonly Map<MessageContentType, string> CONTENT_ID_MAP = new Map<MessageContentType, string>
		{
			{
				MessageContentType.DEBUG,
				"DEBUG"
			},
			{
				MessageContentType.TEXT,
				"TEXT"
			},
			{
				MessageContentType.SHOP,
				"SHOP"
			}
		};

		// Token: 0x04009CD6 RID: 40150
		private const string UNKNOWN_CONTENT_TYPE_ID = "UNKNOWN";
	}
}
