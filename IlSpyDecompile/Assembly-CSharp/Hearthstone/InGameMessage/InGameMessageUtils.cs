using System;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

namespace Hearthstone.InGameMessage
{
	public class InGameMessageUtils
	{
		private struct InGameMessageDef
		{
			public string m_key;

			public object m_defValue;

			public InGameMessageDef(string key, object defValue)
			{
				m_key = key;
				m_defValue = defValue;
			}
		}

		private static Dictionary<InGameMessageAttributes, InGameMessageDef> s_attributes = new Dictionary<InGameMessageAttributes, InGameMessageDef>
		{
			{
				InGameMessageAttributes.UID,
				new InGameMessageDef("uid", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT,
				new InGameMessageDef("text", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT_OFFSET_X,
				new InGameMessageDef("textoffsetx", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT_OFFSET_Y,
				new InGameMessageDef("textoffsety", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT_WIDTH,
				new InGameMessageDef("textwidth", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT_HEIGHT,
				new InGameMessageDef("textheight", string.Empty)
			},
			{
				InGameMessageAttributes.ENTRYTITLE,
				new InGameMessageDef("title", string.Empty)
			},
			{
				InGameMessageAttributes.TITLE,
				new InGameMessageDef("displaytitle", string.Empty)
			},
			{
				InGameMessageAttributes.TEXTURE_URL,
				new InGameMessageDef("texture_url", string.Empty)
			},
			{
				InGameMessageAttributes.LINK,
				new InGameMessageDef("link", string.Empty)
			},
			{
				InGameMessageAttributes.EFFECT,
				new InGameMessageDef("effect", string.Empty)
			},
			{
				InGameMessageAttributes.MAX_VIEW_COUNT,
				new InGameMessageDef("maxviewcount", 0)
			},
			{
				InGameMessageAttributes.PUBLISH_DATE,
				new InGameMessageDef("publish_details.time", default(DateTime))
			},
			{
				InGameMessageAttributes.BEGINNING_DATE,
				new InGameMessageDef("beginningdate", default(DateTime))
			},
			{
				InGameMessageAttributes.EXPIRY_DATE,
				new InGameMessageDef("expirydate", default(DateTime))
			},
			{
				InGameMessageAttributes.PRIORITY_LEVEL,
				new InGameMessageDef("prioritylevel", 0)
			},
			{
				InGameMessageAttributes.GAME_VERSION,
				new InGameMessageDef("gameversion", 0)
			},
			{
				InGameMessageAttributes.MIN_GAME_VERSION,
				new InGameMessageDef("mingameversion", 0)
			},
			{
				InGameMessageAttributes.MAX_GAME_VERSION,
				new InGameMessageDef("maxgameversion", 0)
			},
			{
				InGameMessageAttributes.PLATFORM,
				new InGameMessageDef("platform", null)
			},
			{
				InGameMessageAttributes.ANDROID_STORE,
				new InGameMessageDef("androidstore", null)
			},
			{
				InGameMessageAttributes.GAME_ATTRS,
				new InGameMessageDef("gameattrs", string.Empty)
			},
			{
				InGameMessageAttributes.DISMISS_COND,
				new InGameMessageDef("dismiss", string.Empty)
			},
			{
				InGameMessageAttributes.LAYOUT_TYPE,
				new InGameMessageDef("layouttype", string.Empty)
			},
			{
				InGameMessageAttributes.DISPLAY_IMAGE_TYPE,
				new InGameMessageDef("displayimage", string.Empty)
			},
			{
				InGameMessageAttributes.PRODUCT_ID,
				new InGameMessageDef("productid", 0L)
			},
			{
				InGameMessageAttributes.TEXT_BODY,
				new InGameMessageDef("textbody", string.Empty)
			},
			{
				InGameMessageAttributes.OPEN_FULL_SHOP,
				new InGameMessageDef("openfullshop", false)
			}
		};

		private static int ClientVersion { get; set; } = Convert.ToInt32($"{20}{4}{0:D2}");


		public static GameMessage ReadInGameMessage(JsonNode mainNode)
		{
			if (mainNode == null)
			{
				return null;
			}
			GameMessage gameMessage = new GameMessage
			{
				UID = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.UID]),
				EntryTitle = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.ENTRYTITLE]),
				Title = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.TITLE]),
				TextureUrl = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.TEXTURE_URL]),
				Link = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.LINK]),
				Effect = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.EFFECT]),
				MaxViewCount = GetAttribute<int>(mainNode, s_attributes[InGameMessageAttributes.MAX_VIEW_COUNT]),
				PublishDate = GetAttribute<DateTime>(mainNode, s_attributes[InGameMessageAttributes.PUBLISH_DATE]),
				BeginningDate = GetAttribute<DateTime>(mainNode, s_attributes[InGameMessageAttributes.BEGINNING_DATE]),
				ExpiryDate = GetAttribute<DateTime>(mainNode, s_attributes[InGameMessageAttributes.EXPIRY_DATE]),
				PriorityLevel = GetAttribute<int>(mainNode, s_attributes[InGameMessageAttributes.PRIORITY_LEVEL]),
				GameVersion = GetAttribute<int>(mainNode, s_attributes[InGameMessageAttributes.GAME_VERSION]),
				MinGameVersion = GetAttribute<int>(mainNode, s_attributes[InGameMessageAttributes.MIN_GAME_VERSION]),
				MaxGameVersion = GetAttribute<int>(mainNode, s_attributes[InGameMessageAttributes.MAX_GAME_VERSION]),
				Platform = GetAttributeList<string>(mainNode, s_attributes[InGameMessageAttributes.PLATFORM]),
				AndroidStore = GetAttributeList<string>(mainNode, s_attributes[InGameMessageAttributes.ANDROID_STORE]),
				GameAttrs = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.GAME_ATTRS]),
				DismissCond = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.DISMISS_COND]),
				LayoutType = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.LAYOUT_TYPE]),
				DisplayImageType = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.DISPLAY_IMAGE_TYPE]),
				ProductID = GetAttribute<long>(mainNode, s_attributes[InGameMessageAttributes.PRODUCT_ID]),
				TextBody = GetAttribute<string>(mainNode, s_attributes[InGameMessageAttributes.TEXT_BODY]),
				OpenFullShop = GetAttribute<bool>(mainNode, s_attributes[InGameMessageAttributes.OPEN_FULL_SHOP])
			};
			if (mainNode.ContainsKey("texts"))
			{
				foreach (object item in mainNode["texts"] as JsonList)
				{
					JsonNode aNode = item as JsonNode;
					gameMessage.m_textGroups.Add(new TextGroup
					{
						Text = GetAttribute<string>(aNode, s_attributes[InGameMessageAttributes.TEXT]),
						TextOffsetX = GetAttribute<string>(aNode, s_attributes[InGameMessageAttributes.TEXT_OFFSET_X]),
						TextOffsetY = GetAttribute<string>(aNode, s_attributes[InGameMessageAttributes.TEXT_OFFSET_Y]),
						TextWidth = GetAttribute<string>(aNode, s_attributes[InGameMessageAttributes.TEXT_WIDTH]),
						TextHeight = GetAttribute<string>(aNode, s_attributes[InGameMessageAttributes.TEXT_HEIGHT])
					});
				}
				return gameMessage;
			}
			return gameMessage;
		}

		public static List<GameMessage> GetAllMessagesFromJsonResponse(JsonNode response, ViewCountController viewCountController)
		{
			if (response == null)
			{
				return new List<GameMessage>();
			}
			List<GameMessage> list = new List<GameMessage>();
			try
			{
				JsonList rootListNode = GetRootListNode(response);
				if (rootListNode == null)
				{
					return new List<GameMessage>();
				}
				foreach (object item in rootListNode)
				{
					GameMessage gameMessage = ReadInGameMessage(item as JsonNode);
					if (gameMessage.MaxViewCount <= 0 || viewCountController == null || viewCountController.GetViewCount(gameMessage.UID) < gameMessage.MaxViewCount)
					{
						DateTime now = DateTime.Now;
						if ((!(gameMessage.BeginningDate != default(DateTime)) || !(now < gameMessage.BeginningDate)) && (!(gameMessage.ExpiryDate != default(DateTime)) || !(now > gameMessage.ExpiryDate)))
						{
							list.Add(gameMessage);
						}
					}
				}
				list.Sort(GameMessage.CompareByOrder);
				return list;
			}
			catch (Exception ex)
			{
				Debug.LogError("Failed to get correct message: " + ex);
				return new List<GameMessage>();
			}
		}

		public static string GetUIDOfAttr(InGameMessageAttributes attr)
		{
			return s_attributes[attr].m_key;
		}

		public static string QueryAnd(string query1, string query2)
		{
			return $"{{\"$and\":[{query1},{query2}]}}";
		}

		public static string QueryOr(string query1, string query2)
		{
			return $"{{\"$or\":[{query1},{query2}]}}";
		}

		public static string QueryConvertVal(object val)
		{
			if (val == null)
			{
				return "null";
			}
			if (val is string)
			{
				return string.Concat("\"", val, "\"");
			}
			if (val is bool)
			{
				return val.ToString().ToLower();
			}
			return val.ToString();
		}

		public static string QueryEqual(InGameMessageAttributes attr, object val)
		{
			return $"{{\"{GetUIDOfAttr(attr)}\":{QueryConvertVal(val)}}}";
		}

		public static string QueryOp(InGameMessageAttributes attr, object val, string op)
		{
			return $"{{\"{GetUIDOfAttr(attr)}\":{{\"{op}\":{QueryConvertVal(val)}}}}}";
		}

		public static string QueryOpIn(InGameMessageAttributes attr, object val, string op)
		{
			return $"{{\"{GetUIDOfAttr(attr)}\":{{\"{op}\":[{QueryConvertVal(val)}]}}}}";
		}

		public static string QueryLess(InGameMessageAttributes attr, object val)
		{
			return QueryOp(attr, val, "$lt");
		}

		public static string QueryGreater(InGameMessageAttributes attr, object val)
		{
			return QueryOp(attr, val, "$gt");
		}

		public static string QueryNullOr(InGameMessageAttributes attr, object val, string op)
		{
			return QueryOr(QueryEqual(attr, null), QueryOp(attr, val, op));
		}

		public static string QueryNullOrEqual(InGameMessageAttributes attr, object val)
		{
			return QueryOr(QueryEqual(attr, null), QueryEqual(attr, val));
		}

		public static string QueryNullOrLess(InGameMessageAttributes attr, object val)
		{
			return QueryNullOr(attr, val, "$lt");
		}

		public static string QueryNullOrLessEqual(InGameMessageAttributes attr, object val)
		{
			return QueryNullOr(attr, val, "$lte");
		}

		public static string QueryNullOrGreater(InGameMessageAttributes attr, object val)
		{
			return QueryNullOr(attr, val, "$gt");
		}

		public static string QueryNullOrGreaterEqual(InGameMessageAttributes attr, object val)
		{
			return QueryNullOr(attr, val, "$gte");
		}

		public static string QueryEmptyOrIncludedIn(InGameMessageAttributes attr, object val)
		{
			return QueryOr(QueryOpIn(attr, string.Empty, "$nin"), QueryOpIn(attr, val, "$in"));
		}

		public static string QueryEmptyOrRegEx(InGameMessageAttributes attr, object val)
		{
			return QueryOr(QueryEqual(attr, string.Empty), QueryOp(attr, val, "$regex"));
		}

		public static string MakeQueryString()
		{
			string query = QueryNullOrEqual(InGameMessageAttributes.GAME_VERSION, ClientVersion);
			string query2 = QueryNullOrGreaterEqual(InGameMessageAttributes.MAX_GAME_VERSION, ClientVersion);
			query = QueryAnd(query, query2);
			string query3 = QueryNullOrLessEqual(InGameMessageAttributes.MIN_GAME_VERSION, ClientVersion);
			query = QueryAnd(query, query3);
			string query4 = QueryEmptyOrIncludedIn(InGameMessageAttributes.PLATFORM, PlatformSettings.OS.ToString());
			query = QueryAnd(query, query4);
			if (PlatformSettings.OS == OSCategory.Android)
			{
				string query5 = QueryEmptyOrIncludedIn(InGameMessageAttributes.ANDROID_STORE, AndroidDeviceSettings.Get().GetAndroidStore().ToString());
				query = QueryAnd(query, query5);
			}
			string text = "";
			foreach (string attribution in GameAttributes.Get().GetAttributions(activeOnly: true))
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += "|";
				}
				text = text + "\\\\b" + attribution + "\\\\b";
			}
			if (!string.IsNullOrEmpty(text))
			{
				string query6 = QueryEmptyOrRegEx(InGameMessageAttributes.GAME_ATTRS, "[" + text + "]");
				query = QueryAnd(query, query6);
			}
			return "&query=" + query;
		}

		private static JsonList GetRootListNode(JsonNode response)
		{
			if (response.ContainsKey("entries"))
			{
				return response["entries"] as JsonList;
			}
			if (response.ContainsKey("entry"))
			{
				return new JsonList { response["entry"] };
			}
			return null;
		}

		private static T GetAttribute<T>(JsonNode aNode, InGameMessageDef attrDef)
		{
			T val = default(T);
			val = GetValueFromNode<T>(attrDef.m_key, aNode);
			if (EqualityComparer<T>.Default.Equals(val, default(T)))
			{
				return (T)attrDef.m_defValue;
			}
			return val;
		}

		private static T GetValueFromNode<T>(string keyName, JsonNode node)
		{
			node = GetNodeAt(ref keyName, node);
			if (node == null)
			{
				return default(T);
			}
			if (node.ContainsKey(keyName))
			{
				try
				{
					return (T)Convert.ChangeType(node[keyName], typeof(T));
				}
				catch
				{
				}
			}
			return default(T);
		}

		private static List<T> GetAttributeList<T>(JsonNode aNode, InGameMessageDef attrDef)
		{
			List<T> valueFromNodeList = GetValueFromNodeList<T>(attrDef.m_key, aNode);
			if (EqualityComparer<List<T>>.Default.Equals(valueFromNodeList, null))
			{
				return null;
			}
			return valueFromNodeList;
		}

		private static List<T> GetValueFromNodeList<T>(string keyName, JsonNode node)
		{
			node = GetNodeAt(ref keyName, node);
			if (node == null)
			{
				return null;
			}
			List<T> list = null;
			if (node.ContainsKey(keyName) && node[keyName] is JsonList)
			{
				list = new List<T>();
				foreach (object item in node[keyName] as JsonList)
				{
					try
					{
						list.Add((T)Convert.ChangeType(item, typeof(T)));
					}
					catch
					{
					}
				}
				if (list.Count == 0)
				{
					list = null;
				}
			}
			return list;
		}

		private static JsonNode GetNodeAt(ref string keyName, JsonNode node)
		{
			if (keyName.Contains('.'))
			{
				string[] array = keyName.Split('.');
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (i == array.Length - 1)
					{
						keyName = text;
						break;
					}
					if (node.ContainsKey(text) && node[text] is JsonNode)
					{
						node = node[text] as JsonNode;
						continue;
					}
					return null;
				}
			}
			return node;
		}
	}
}
