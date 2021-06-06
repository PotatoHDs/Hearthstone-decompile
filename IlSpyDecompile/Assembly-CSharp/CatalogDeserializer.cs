using System;
using System.Collections.Generic;
using System.Linq;
using MiniJSON;

public static class CatalogDeserializer
{
	public static List<Network.ShopSale> DeserializeShopSaleList(string jsonString)
	{
		List<Network.ShopSale> list = new List<Network.ShopSale>();
		if (string.IsNullOrEmpty(jsonString))
		{
			Log.Store.PrintError("Received no catalog product sale data");
			return list;
		}
		try
		{
			JsonNode jsonNode = Json.Deserialize(jsonString) as JsonNode;
			if (jsonNode == null)
			{
				Log.Store.PrintError("Failed to load sale data. Invalid JSON format:\n{0}", jsonString);
				return list;
			}
			foreach (JsonNode item in ((JsonList)jsonNode["saleList"]).Cast<JsonNode>())
			{
				int num = JsonObjectToValue<int>(item["saleId"]);
				long? num2 = TryGetValueFromJsonNode<long>(item, "saleStartDate");
				long? num3 = TryGetValueFromJsonNode<long>(item, "saleSoftEndDate");
				long? num4 = TryGetValueFromJsonNode<long>(item, "saleHardEndDate");
				Network.ShopSale shopSale = new Network.ShopSale
				{
					SaleId = num
				};
				if (num2.HasValue)
				{
					shopSale.StartUtc = TimeUtils.UnixTimeStampMillisecondsToDateTimeUtc(num2.Value);
				}
				if (num3.HasValue)
				{
					shopSale.SoftEndUtc = TimeUtils.UnixTimeStampMillisecondsToDateTimeUtc(num3.Value);
					if (shopSale.StartUtc.HasValue && shopSale.StartUtc > shopSale.SoftEndUtc.Value)
					{
						Log.Store.PrintWarning("Sale {0} start date exceeds the soft end date. Setting soft end to start. StartUtc={1} SoftEndUtc={2}", num, shopSale.StartUtc.Value.ToString("G"), shopSale.SoftEndUtc.Value.ToString("G"));
						shopSale.SoftEndUtc = shopSale.StartUtc;
					}
				}
				if (num4.HasValue)
				{
					shopSale.HardEndUtc = TimeUtils.UnixTimeStampMillisecondsToDateTimeUtc(num4.Value);
					if (shopSale.StartUtc.HasValue && shopSale.StartUtc > shopSale.HardEndUtc.Value)
					{
						Log.Store.PrintWarning("Sale {0} start date exceeds the hard end date. Setting hard end to start. StartUtc={1} HardEndUtc={2}", num, shopSale.StartUtc.Value.ToString("G"), shopSale.HardEndUtc.Value.ToString("G"));
						shopSale.HardEndUtc = shopSale.StartUtc.Value;
					}
					if (!shopSale.SoftEndUtc.HasValue)
					{
						Log.Store.PrintWarning("Sale {0} has a hard end date but no soft end date. Setting soft end to hard end {1}.", num, shopSale.HardEndUtc.Value.ToString("G"));
						shopSale.SoftEndUtc = shopSale.HardEndUtc.Value;
					}
					else if (shopSale.SoftEndUtc.Value > shopSale.HardEndUtc.Value)
					{
						Log.Store.PrintWarning("Sale {0} soft end date exceeds the hard end date. Setting soft end to hard end. SoftEndUtc={1} HardEndUtc={2}", num, shopSale.SoftEndUtc.Value.ToString("G"), shopSale.HardEndUtc.Value.ToString("G"));
						shopSale.SoftEndUtc = shopSale.HardEndUtc.Value;
					}
				}
				list.Add(shopSale);
			}
			Log.Store.Print("Finished deserialization of catalog sales");
			return list;
		}
		catch (Exception arg)
		{
			Log.Store.PrintError($"Failed loading catalog product sale data: {arg}");
			return list;
		}
	}

	private static T? TryGetValueFromJsonNode<T>(JsonNode node, string fieldName) where T : struct
	{
		T? result = null;
		if (node.TryGetValue(fieldName, out var value))
		{
			result = JsonObjectToValue<T>(value);
		}
		return result;
	}

	private static T JsonObjectToValue<T>(object obj)
	{
		return (T)((IConvertible)obj).ToType(typeof(T), null);
	}
}
