using System;
using System.Collections.Generic;
using System.Linq;
using MiniJSON;

// Token: 0x020006A4 RID: 1700
public static class CatalogDeserializer
{
	// Token: 0x06005EDE RID: 24286 RVA: 0x001ED77C File Offset: 0x001EB97C
	public static List<Network.ShopSale> DeserializeShopSaleList(string jsonString)
	{
		List<Network.ShopSale> list = new List<Network.ShopSale>();
		if (string.IsNullOrEmpty(jsonString))
		{
			Log.Store.PrintError("Received no catalog product sale data", Array.Empty<object>());
			return list;
		}
		try
		{
			JsonNode jsonNode = Json.Deserialize(jsonString) as JsonNode;
			if (jsonNode == null)
			{
				Log.Store.PrintError("Failed to load sale data. Invalid JSON format:\n{0}", new object[]
				{
					jsonString
				});
				return list;
			}
			foreach (JsonNode jsonNode2 in ((JsonList)jsonNode["saleList"]).Cast<JsonNode>())
			{
				int num = CatalogDeserializer.JsonObjectToValue<int>(jsonNode2["saleId"]);
				long? num2 = CatalogDeserializer.TryGetValueFromJsonNode<long>(jsonNode2, "saleStartDate");
				long? num3 = CatalogDeserializer.TryGetValueFromJsonNode<long>(jsonNode2, "saleSoftEndDate");
				long? num4 = CatalogDeserializer.TryGetValueFromJsonNode<long>(jsonNode2, "saleHardEndDate");
				Network.ShopSale shopSale = new Network.ShopSale
				{
					SaleId = num
				};
				if (num2 != null)
				{
					shopSale.StartUtc = new DateTime?(TimeUtils.UnixTimeStampMillisecondsToDateTimeUtc(num2.Value));
				}
				if (num3 != null)
				{
					shopSale.SoftEndUtc = new DateTime?(TimeUtils.UnixTimeStampMillisecondsToDateTimeUtc(num3.Value));
					if (shopSale.StartUtc != null && shopSale.StartUtc > shopSale.SoftEndUtc.Value)
					{
						Log.Store.PrintWarning("Sale {0} start date exceeds the soft end date. Setting soft end to start. StartUtc={1} SoftEndUtc={2}", new object[]
						{
							num,
							shopSale.StartUtc.Value.ToString("G"),
							shopSale.SoftEndUtc.Value.ToString("G")
						});
						shopSale.SoftEndUtc = shopSale.StartUtc;
					}
				}
				if (num4 != null)
				{
					shopSale.HardEndUtc = new DateTime?(TimeUtils.UnixTimeStampMillisecondsToDateTimeUtc(num4.Value));
					if (shopSale.StartUtc != null && shopSale.StartUtc > shopSale.HardEndUtc.Value)
					{
						Log.Store.PrintWarning("Sale {0} start date exceeds the hard end date. Setting hard end to start. StartUtc={1} HardEndUtc={2}", new object[]
						{
							num,
							shopSale.StartUtc.Value.ToString("G"),
							shopSale.HardEndUtc.Value.ToString("G")
						});
						shopSale.HardEndUtc = new DateTime?(shopSale.StartUtc.Value);
					}
					if (shopSale.SoftEndUtc == null)
					{
						Log.Store.PrintWarning("Sale {0} has a hard end date but no soft end date. Setting soft end to hard end {1}.", new object[]
						{
							num,
							shopSale.HardEndUtc.Value.ToString("G")
						});
						shopSale.SoftEndUtc = new DateTime?(shopSale.HardEndUtc.Value);
					}
					else if (shopSale.SoftEndUtc.Value > shopSale.HardEndUtc.Value)
					{
						Log.Store.PrintWarning("Sale {0} soft end date exceeds the hard end date. Setting soft end to hard end. SoftEndUtc={1} HardEndUtc={2}", new object[]
						{
							num,
							shopSale.SoftEndUtc.Value.ToString("G"),
							shopSale.HardEndUtc.Value.ToString("G")
						});
						shopSale.SoftEndUtc = new DateTime?(shopSale.HardEndUtc.Value);
					}
				}
				list.Add(shopSale);
			}
			Log.Store.Print("Finished deserialization of catalog sales", Array.Empty<object>());
		}
		catch (Exception arg)
		{
			Log.Store.PrintError(string.Format("Failed loading catalog product sale data: {0}", arg), Array.Empty<object>());
		}
		return list;
	}

	// Token: 0x06005EDF RID: 24287 RVA: 0x001EDBDC File Offset: 0x001EBDDC
	private static T? TryGetValueFromJsonNode<T>(JsonNode node, string fieldName) where T : struct
	{
		T? result = null;
		object obj;
		if (node.TryGetValue(fieldName, out obj))
		{
			result = new T?(CatalogDeserializer.JsonObjectToValue<T>(obj));
		}
		return result;
	}

	// Token: 0x06005EE0 RID: 24288 RVA: 0x001EDC0A File Offset: 0x001EBE0A
	private static T JsonObjectToValue<T>(object obj)
	{
		return (T)((object)((IConvertible)obj).ToType(typeof(T), null));
	}
}
