using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

namespace Hearthstone.InGameMessage.UI
{
	public class MessageModal : MonoBehaviour
	{
		private Widget m_contentWidget;

		private UIMessageCallbacks m_callbacks;

		private long m_productId;

		private bool m_openFullShop;

		private const string SHOP_BUTTON_EVENT = "ShopButtonPressed";

		private const string FINAL_CLOSE_BUTTON_EVENT = "FinalalizeCloseButtonPress";

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

		private const string UNKNOWN_CONTENT_TYPE_ID = "UNKNOWN";

		private void Awake()
		{
			m_contentWidget = GetComponent<Widget>();
			m_contentWidget.RegisterEventListener(OnEventMessage);
		}

		public void SetMessage(MessageUIData data)
		{
			if (data == null)
			{
				Log.InGameMessage.PrintWarning("Failed to set In game message, data was null");
				return;
			}
			SetModalDataModel(data);
			SetContentDataModel(data);
			SetShopSettingsIfAvailable(data);
			m_callbacks = data.Callbacks;
		}

		private void SetShopSettingsIfAvailable(MessageUIData data)
		{
			if (data.ContentType == MessageContentType.SHOP)
			{
				ShopMessageContent shopMessageContent = data.MessageData as ShopMessageContent;
				m_productId = shopMessageContent.ProductID;
				m_openFullShop = shopMessageContent.OpenFullShop;
			}
		}

		private void SetContentDataModel(MessageUIData data)
		{
			List<IDataModel> list = MessageDataModelFactory.CreateDataModel(data);
			if (list == null)
			{
				Log.InGameMessage.PrintError("Could not create a content model for IGM data");
				return;
			}
			if (m_contentWidget == null)
			{
				Log.InGameMessage.PrintError("Missing Content Widget! IGM will not work correctly");
				return;
			}
			foreach (IDataModel item in list)
			{
				m_contentWidget.BindDataModel(item);
			}
		}

		private void SetModalDataModel(MessageUIData data)
		{
			MessageModalDataModel messageModalDataModel = new MessageModalDataModel();
			messageModalDataModel.ContentType = GetContentTypeID(data.ContentType);
			m_contentWidget.BindDataModel(messageModalDataModel);
		}

		private string GetContentTypeID(MessageContentType type)
		{
			if (CONTENT_ID_MAP.TryGetValue(type, out var value))
			{
				return value;
			}
			return "UNKNOWN";
		}

		public void OnEventMessage(string eventName)
		{
			if (eventName.Equals("FinalalizeCloseButtonPress", StringComparison.OrdinalIgnoreCase))
			{
				OnClosePressed();
			}
			else if (eventName.Equals("ShopButtonPressed", StringComparison.OrdinalIgnoreCase))
			{
				OnOpenStoreButton();
			}
		}

		public void OnClosePressed()
		{
			Log.InGameMessage.PrintDebug("Modal close button pressed");
			m_callbacks?.OnClosed();
		}

		private void OnOpenStoreButton()
		{
			Log.InGameMessage.PrintDebug($"Opening shop page for product ID {m_productId}");
			Shop.OpenToProductPageWhenReady(m_productId, !m_openFullShop);
			m_callbacks?.OnStoreOpened();
			OnClosePressed();
		}
	}
}
