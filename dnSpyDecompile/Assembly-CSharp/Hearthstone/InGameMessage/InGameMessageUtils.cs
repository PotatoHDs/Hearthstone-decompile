using System;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine;

namespace Hearthstone.InGameMessage
{
	// Token: 0x02001156 RID: 4438
	public class InGameMessageUtils
	{
		// Token: 0x0600C283 RID: 49795 RVA: 0x003AEC0C File Offset: 0x003ACE0C
		public static GameMessage ReadInGameMessage(JsonNode mainNode)
		{
			if (mainNode == null)
			{
				return null;
			}
			GameMessage gameMessage = new GameMessage
			{
				UID = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.UID]),
				EntryTitle = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.ENTRYTITLE]),
				Title = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.TITLE]),
				TextureUrl = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.TEXTURE_URL]),
				Link = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.LINK]),
				Effect = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.EFFECT]),
				MaxViewCount = InGameMessageUtils.GetAttribute<int>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.MAX_VIEW_COUNT]),
				PublishDate = InGameMessageUtils.GetAttribute<DateTime>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.PUBLISH_DATE]),
				BeginningDate = InGameMessageUtils.GetAttribute<DateTime>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.BEGINNING_DATE]),
				ExpiryDate = InGameMessageUtils.GetAttribute<DateTime>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.EXPIRY_DATE]),
				PriorityLevel = InGameMessageUtils.GetAttribute<int>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.PRIORITY_LEVEL]),
				GameVersion = InGameMessageUtils.GetAttribute<int>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.GAME_VERSION]),
				MinGameVersion = InGameMessageUtils.GetAttribute<int>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.MIN_GAME_VERSION]),
				MaxGameVersion = InGameMessageUtils.GetAttribute<int>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.MAX_GAME_VERSION]),
				Platform = InGameMessageUtils.GetAttributeList<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.PLATFORM]),
				AndroidStore = InGameMessageUtils.GetAttributeList<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.ANDROID_STORE]),
				GameAttrs = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.GAME_ATTRS]),
				DismissCond = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.DISMISS_COND]),
				LayoutType = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.LAYOUT_TYPE]),
				DisplayImageType = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.DISPLAY_IMAGE_TYPE]),
				ProductID = InGameMessageUtils.GetAttribute<long>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.PRODUCT_ID]),
				TextBody = InGameMessageUtils.GetAttribute<string>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.TEXT_BODY]),
				OpenFullShop = InGameMessageUtils.GetAttribute<bool>(mainNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.OPEN_FULL_SHOP])
			};
			if (mainNode.ContainsKey("texts"))
			{
				foreach (object obj in (mainNode["texts"] as JsonList))
				{
					JsonNode aNode = obj as JsonNode;
					gameMessage.m_textGroups.Add(new TextGroup
					{
						Text = InGameMessageUtils.GetAttribute<string>(aNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.TEXT]),
						TextOffsetX = InGameMessageUtils.GetAttribute<string>(aNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.TEXT_OFFSET_X]),
						TextOffsetY = InGameMessageUtils.GetAttribute<string>(aNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.TEXT_OFFSET_Y]),
						TextWidth = InGameMessageUtils.GetAttribute<string>(aNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.TEXT_WIDTH]),
						TextHeight = InGameMessageUtils.GetAttribute<string>(aNode, InGameMessageUtils.s_attributes[InGameMessageAttributes.TEXT_HEIGHT])
					});
				}
			}
			return gameMessage;
		}

		// Token: 0x0600C284 RID: 49796 RVA: 0x003AEF34 File Offset: 0x003AD134
		public static List<GameMessage> GetAllMessagesFromJsonResponse(JsonNode response, ViewCountController viewCountController)
		{
			if (response == null)
			{
				return new List<GameMessage>();
			}
			List<GameMessage> list = new List<GameMessage>();
			List<GameMessage> result;
			try
			{
				JsonList rootListNode = InGameMessageUtils.GetRootListNode(response);
				if (rootListNode == null)
				{
					result = new List<GameMessage>();
				}
				else
				{
					foreach (object obj in rootListNode)
					{
						GameMessage gameMessage = InGameMessageUtils.ReadInGameMessage(obj as JsonNode);
						if (gameMessage.MaxViewCount <= 0 || viewCountController == null || viewCountController.GetViewCount(gameMessage.UID) < gameMessage.MaxViewCount)
						{
							DateTime now = DateTime.Now;
							if ((!(gameMessage.BeginningDate != default(DateTime)) || !(now < gameMessage.BeginningDate)) && (!(gameMessage.ExpiryDate != default(DateTime)) || !(now > gameMessage.ExpiryDate)))
							{
								list.Add(gameMessage);
							}
						}
					}
					list.Sort(new Comparison<GameMessage>(GameMessage.CompareByOrder));
					result = list;
				}
			}
			catch (Exception arg)
			{
				Debug.LogError("Failed to get correct message: " + arg);
				result = new List<GameMessage>();
			}
			return result;
		}

		// Token: 0x0600C285 RID: 49797 RVA: 0x003AF070 File Offset: 0x003AD270
		public static string GetUIDOfAttr(InGameMessageAttributes attr)
		{
			return InGameMessageUtils.s_attributes[attr].m_key;
		}

		// Token: 0x0600C286 RID: 49798 RVA: 0x003AF082 File Offset: 0x003AD282
		public static string QueryAnd(string query1, string query2)
		{
			return string.Format("{{\"$and\":[{0},{1}]}}", query1, query2);
		}

		// Token: 0x0600C287 RID: 49799 RVA: 0x003AF090 File Offset: 0x003AD290
		public static string QueryOr(string query1, string query2)
		{
			return string.Format("{{\"$or\":[{0},{1}]}}", query1, query2);
		}

		// Token: 0x0600C288 RID: 49800 RVA: 0x003AF09E File Offset: 0x003AD29E
		public static string QueryConvertVal(object val)
		{
			if (val == null)
			{
				return "null";
			}
			if (val is string)
			{
				return "\"" + val + "\"";
			}
			if (val is bool)
			{
				return val.ToString().ToLower();
			}
			return val.ToString();
		}

		// Token: 0x0600C289 RID: 49801 RVA: 0x003AF0DC File Offset: 0x003AD2DC
		public static string QueryEqual(InGameMessageAttributes attr, object val)
		{
			return string.Format("{{\"{0}\":{1}}}", InGameMessageUtils.GetUIDOfAttr(attr), InGameMessageUtils.QueryConvertVal(val));
		}

		// Token: 0x0600C28A RID: 49802 RVA: 0x003AF0F4 File Offset: 0x003AD2F4
		public static string QueryOp(InGameMessageAttributes attr, object val, string op)
		{
			return string.Format("{{\"{0}\":{{\"{1}\":{2}}}}}", InGameMessageUtils.GetUIDOfAttr(attr), op, InGameMessageUtils.QueryConvertVal(val));
		}

		// Token: 0x0600C28B RID: 49803 RVA: 0x003AF10D File Offset: 0x003AD30D
		public static string QueryOpIn(InGameMessageAttributes attr, object val, string op)
		{
			return string.Format("{{\"{0}\":{{\"{1}\":[{2}]}}}}", InGameMessageUtils.GetUIDOfAttr(attr), op, InGameMessageUtils.QueryConvertVal(val));
		}

		// Token: 0x0600C28C RID: 49804 RVA: 0x003AF126 File Offset: 0x003AD326
		public static string QueryLess(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryOp(attr, val, "$lt");
		}

		// Token: 0x0600C28D RID: 49805 RVA: 0x003AF134 File Offset: 0x003AD334
		public static string QueryGreater(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryOp(attr, val, "$gt");
		}

		// Token: 0x0600C28E RID: 49806 RVA: 0x003AF142 File Offset: 0x003AD342
		public static string QueryNullOr(InGameMessageAttributes attr, object val, string op)
		{
			return InGameMessageUtils.QueryOr(InGameMessageUtils.QueryEqual(attr, null), InGameMessageUtils.QueryOp(attr, val, op));
		}

		// Token: 0x0600C28F RID: 49807 RVA: 0x003AF158 File Offset: 0x003AD358
		public static string QueryNullOrEqual(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryOr(InGameMessageUtils.QueryEqual(attr, null), InGameMessageUtils.QueryEqual(attr, val));
		}

		// Token: 0x0600C290 RID: 49808 RVA: 0x003AF16D File Offset: 0x003AD36D
		public static string QueryNullOrLess(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryNullOr(attr, val, "$lt");
		}

		// Token: 0x0600C291 RID: 49809 RVA: 0x003AF17B File Offset: 0x003AD37B
		public static string QueryNullOrLessEqual(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryNullOr(attr, val, "$lte");
		}

		// Token: 0x0600C292 RID: 49810 RVA: 0x003AF189 File Offset: 0x003AD389
		public static string QueryNullOrGreater(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryNullOr(attr, val, "$gt");
		}

		// Token: 0x0600C293 RID: 49811 RVA: 0x003AF197 File Offset: 0x003AD397
		public static string QueryNullOrGreaterEqual(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryNullOr(attr, val, "$gte");
		}

		// Token: 0x0600C294 RID: 49812 RVA: 0x003AF1A5 File Offset: 0x003AD3A5
		public static string QueryEmptyOrIncludedIn(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryOr(InGameMessageUtils.QueryOpIn(attr, string.Empty, "$nin"), InGameMessageUtils.QueryOpIn(attr, val, "$in"));
		}

		// Token: 0x0600C295 RID: 49813 RVA: 0x003AF1C8 File Offset: 0x003AD3C8
		public static string QueryEmptyOrRegEx(InGameMessageAttributes attr, object val)
		{
			return InGameMessageUtils.QueryOr(InGameMessageUtils.QueryEqual(attr, string.Empty), InGameMessageUtils.QueryOp(attr, val, "$regex"));
		}

		// Token: 0x0600C296 RID: 49814 RVA: 0x003AF1E8 File Offset: 0x003AD3E8
		public static string MakeQueryString()
		{
			string text = InGameMessageUtils.QueryNullOrEqual(InGameMessageAttributes.GAME_VERSION, InGameMessageUtils.ClientVersion);
			string query = InGameMessageUtils.QueryNullOrGreaterEqual(InGameMessageAttributes.MAX_GAME_VERSION, InGameMessageUtils.ClientVersion);
			text = InGameMessageUtils.QueryAnd(text, query);
			string query2 = InGameMessageUtils.QueryNullOrLessEqual(InGameMessageAttributes.MIN_GAME_VERSION, InGameMessageUtils.ClientVersion);
			text = InGameMessageUtils.QueryAnd(text, query2);
			string query3 = InGameMessageUtils.QueryEmptyOrIncludedIn(InGameMessageAttributes.PLATFORM, PlatformSettings.OS.ToString());
			text = InGameMessageUtils.QueryAnd(text, query3);
			if (PlatformSettings.OS == OSCategory.Android)
			{
				string query4 = InGameMessageUtils.QueryEmptyOrIncludedIn(InGameMessageAttributes.ANDROID_STORE, AndroidDeviceSettings.Get().GetAndroidStore().ToString());
				text = InGameMessageUtils.QueryAnd(text, query4);
			}
			string text2 = "";
			foreach (string str in GameAttributes.Get().GetAttributions(true))
			{
				if (!string.IsNullOrEmpty(text2))
				{
					text2 += "|";
				}
				text2 = text2 + "\\\\b" + str + "\\\\b";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				string query5 = InGameMessageUtils.QueryEmptyOrRegEx(InGameMessageAttributes.GAME_ATTRS, "[" + text2 + "]");
				text = InGameMessageUtils.QueryAnd(text, query5);
			}
			return "&query=" + text;
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x0600C297 RID: 49815 RVA: 0x003AF340 File Offset: 0x003AD540
		// (set) Token: 0x0600C298 RID: 49816 RVA: 0x003AF347 File Offset: 0x003AD547
		private static int ClientVersion { get; set; } = Convert.ToInt32(string.Format("{0}{1}{2:D2}", 20, 4, 0));

		// Token: 0x0600C299 RID: 49817 RVA: 0x003AF350 File Offset: 0x003AD550
		private static JsonList GetRootListNode(JsonNode response)
		{
			if (response.ContainsKey("entries"))
			{
				return response["entries"] as JsonList;
			}
			if (response.ContainsKey("entry"))
			{
				return new JsonList
				{
					response["entry"]
				};
			}
			return null;
		}

		// Token: 0x0600C29A RID: 49818 RVA: 0x003AF3A0 File Offset: 0x003AD5A0
		private static T GetAttribute<T>(JsonNode aNode, InGameMessageUtils.InGameMessageDef attrDef)
		{
			T t = default(T);
			t = InGameMessageUtils.GetValueFromNode<T>(attrDef.m_key, aNode);
			if (EqualityComparer<T>.Default.Equals(t, default(T)))
			{
				return (T)((object)attrDef.m_defValue);
			}
			return t;
		}

		// Token: 0x0600C29B RID: 49819 RVA: 0x003AF3E8 File Offset: 0x003AD5E8
		private static T GetValueFromNode<T>(string keyName, JsonNode node)
		{
			node = InGameMessageUtils.GetNodeAt(ref keyName, node);
			if (node == null)
			{
				return default(T);
			}
			if (node.ContainsKey(keyName))
			{
				try
				{
					return (T)((object)Convert.ChangeType(node[keyName], typeof(T)));
				}
				catch
				{
				}
			}
			return default(T);
		}

		// Token: 0x0600C29C RID: 49820 RVA: 0x003AF454 File Offset: 0x003AD654
		private static List<T> GetAttributeList<T>(JsonNode aNode, InGameMessageUtils.InGameMessageDef attrDef)
		{
			List<T> valueFromNodeList = InGameMessageUtils.GetValueFromNodeList<T>(attrDef.m_key, aNode);
			if (EqualityComparer<List<T>>.Default.Equals(valueFromNodeList, null))
			{
				return null;
			}
			return valueFromNodeList;
		}

		// Token: 0x0600C29D RID: 49821 RVA: 0x003AF480 File Offset: 0x003AD680
		private static List<T> GetValueFromNodeList<T>(string keyName, JsonNode node)
		{
			node = InGameMessageUtils.GetNodeAt(ref keyName, node);
			if (node == null)
			{
				return null;
			}
			List<T> list = null;
			if (node.ContainsKey(keyName) && node[keyName] is JsonList)
			{
				list = new List<T>();
				foreach (object value in (node[keyName] as JsonList))
				{
					try
					{
						list.Add((T)((object)Convert.ChangeType(value, typeof(T))));
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

		// Token: 0x0600C29E RID: 49822 RVA: 0x003AF538 File Offset: 0x003AD738
		private static JsonNode GetNodeAt(ref string keyName, JsonNode node)
		{
			if (keyName.Contains('.'))
			{
				string[] array = keyName.Split(new char[]
				{
					'.'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (i == array.Length - 1)
					{
						keyName = text;
						break;
					}
					if (!node.ContainsKey(text) || !(node[text] is JsonNode))
					{
						return null;
					}
					node = (node[text] as JsonNode);
				}
			}
			return node;
		}

		// Token: 0x04009CAF RID: 40111
		private static Dictionary<InGameMessageAttributes, InGameMessageUtils.InGameMessageDef> s_attributes = new Dictionary<InGameMessageAttributes, InGameMessageUtils.InGameMessageDef>
		{
			{
				InGameMessageAttributes.UID,
				new InGameMessageUtils.InGameMessageDef("uid", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT,
				new InGameMessageUtils.InGameMessageDef("text", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT_OFFSET_X,
				new InGameMessageUtils.InGameMessageDef("textoffsetx", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT_OFFSET_Y,
				new InGameMessageUtils.InGameMessageDef("textoffsety", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT_WIDTH,
				new InGameMessageUtils.InGameMessageDef("textwidth", string.Empty)
			},
			{
				InGameMessageAttributes.TEXT_HEIGHT,
				new InGameMessageUtils.InGameMessageDef("textheight", string.Empty)
			},
			{
				InGameMessageAttributes.ENTRYTITLE,
				new InGameMessageUtils.InGameMessageDef("title", string.Empty)
			},
			{
				InGameMessageAttributes.TITLE,
				new InGameMessageUtils.InGameMessageDef("displaytitle", string.Empty)
			},
			{
				InGameMessageAttributes.TEXTURE_URL,
				new InGameMessageUtils.InGameMessageDef("texture_url", string.Empty)
			},
			{
				InGameMessageAttributes.LINK,
				new InGameMessageUtils.InGameMessageDef("link", string.Empty)
			},
			{
				InGameMessageAttributes.EFFECT,
				new InGameMessageUtils.InGameMessageDef("effect", string.Empty)
			},
			{
				InGameMessageAttributes.MAX_VIEW_COUNT,
				new InGameMessageUtils.InGameMessageDef("maxviewcount", 0)
			},
			{
				InGameMessageAttributes.PUBLISH_DATE,
				new InGameMessageUtils.InGameMessageDef("publish_details.time", default(DateTime))
			},
			{
				InGameMessageAttributes.BEGINNING_DATE,
				new InGameMessageUtils.InGameMessageDef("beginningdate", default(DateTime))
			},
			{
				InGameMessageAttributes.EXPIRY_DATE,
				new InGameMessageUtils.InGameMessageDef("expirydate", default(DateTime))
			},
			{
				InGameMessageAttributes.PRIORITY_LEVEL,
				new InGameMessageUtils.InGameMessageDef("prioritylevel", 0)
			},
			{
				InGameMessageAttributes.GAME_VERSION,
				new InGameMessageUtils.InGameMessageDef("gameversion", 0)
			},
			{
				InGameMessageAttributes.MIN_GAME_VERSION,
				new InGameMessageUtils.InGameMessageDef("mingameversion", 0)
			},
			{
				InGameMessageAttributes.MAX_GAME_VERSION,
				new InGameMessageUtils.InGameMessageDef("maxgameversion", 0)
			},
			{
				InGameMessageAttributes.PLATFORM,
				new InGameMessageUtils.InGameMessageDef("platform", null)
			},
			{
				InGameMessageAttributes.ANDROID_STORE,
				new InGameMessageUtils.InGameMessageDef("androidstore", null)
			},
			{
				InGameMessageAttributes.GAME_ATTRS,
				new InGameMessageUtils.InGameMessageDef("gameattrs", string.Empty)
			},
			{
				InGameMessageAttributes.DISMISS_COND,
				new InGameMessageUtils.InGameMessageDef("dismiss", string.Empty)
			},
			{
				InGameMessageAttributes.LAYOUT_TYPE,
				new InGameMessageUtils.InGameMessageDef("layouttype", string.Empty)
			},
			{
				InGameMessageAttributes.DISPLAY_IMAGE_TYPE,
				new InGameMessageUtils.InGameMessageDef("displayimage", string.Empty)
			},
			{
				InGameMessageAttributes.PRODUCT_ID,
				new InGameMessageUtils.InGameMessageDef("productid", 0L)
			},
			{
				InGameMessageAttributes.TEXT_BODY,
				new InGameMessageUtils.InGameMessageDef("textbody", string.Empty)
			},
			{
				InGameMessageAttributes.OPEN_FULL_SHOP,
				new InGameMessageUtils.InGameMessageDef("openfullshop", false)
			}
		};

		// Token: 0x02002921 RID: 10529
		private struct InGameMessageDef
		{
			// Token: 0x06013E13 RID: 81427 RVA: 0x0053EE40 File Offset: 0x0053D040
			public InGameMessageDef(string key, object defValue)
			{
				this.m_key = key;
				this.m_defValue = defValue;
			}

			// Token: 0x0400FBDA RID: 64474
			public string m_key;

			// Token: 0x0400FBDB RID: 64475
			public object m_defValue;
		}
	}
}
