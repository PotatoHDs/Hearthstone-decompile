using System.Collections.Generic;
using System.IO;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public static class OfflineDataSerializer
{
	private abstract class OfflineDataSerializerBase : IOfflineDataSerializer
	{
		public void Serialize(OfflineDataCache.OfflineData data, BinaryWriter writer)
		{
			if (writer == null)
			{
				Debug.LogError("Could not Serialize OfflineData, writer was null");
				return;
			}
			writer.Write(data.UniqueFakeDeckId);
			List<long> fakeDeckIds = OfflineDataCache.GetFakeDeckIds(data);
			writer.Write(fakeDeckIds.Count);
			foreach (long item in fakeDeckIds)
			{
				writer.Write(item);
			}
			List<DeckInfo> list = ((data.OriginalDeckList == null) ? new List<DeckInfo>() : data.OriginalDeckList);
			List<DeckInfo> list2 = ((data.LocalDeckList == null) ? new List<DeckInfo>() : data.LocalDeckList);
			writer.Write(list.Count);
			foreach (DeckInfo item2 in list)
			{
				AppendProtoToFile(writer, item2);
			}
			writer.Write(list2.Count);
			foreach (DeckInfo item3 in list2)
			{
				AppendProtoToFile(writer, item3);
			}
			List<DeckContents> list3 = ((data.OriginalDeckContents == null) ? new List<DeckContents>() : data.OriginalDeckContents);
			List<DeckContents> list4 = ((data.LocalDeckContents == null) ? new List<DeckContents>() : data.LocalDeckContents);
			writer.Write(list3.Count);
			foreach (DeckContents item4 in list3)
			{
				AppendProtoToFile(writer, item4);
			}
			writer.Write(list4.Count);
			foreach (DeckContents item5 in list4)
			{
				AppendProtoToFile(writer, item5);
			}
			List<FavoriteHero> list5 = ((data.FavoriteHeroes == null) ? new List<FavoriteHero>() : data.FavoriteHeroes);
			writer.Write(list5.Count);
			foreach (FavoriteHero item6 in list5)
			{
				AppendProtoToFile(writer, item6);
			}
			writer.Write(data.m_hasChangedFavoriteHeroesOffline);
			AppendProtoToFile(writer, data.CardBacks);
			writer.Write(data.m_hasChangedCardBacksOffline);
			AppendProtoToFile(writer, data.Collection);
		}

		public abstract OfflineDataCache.OfflineData Deserialize(BinaryReader reader);
	}

	private class OfflineDataSerializer_V0Deserializer : OfflineDataSerializerBase
	{
		public override OfflineDataCache.OfflineData Deserialize(BinaryReader reader)
		{
			if (reader == null)
			{
				Debug.LogError("Could not Deserialize v0 OfflineData, reader was null");
				return null;
			}
			OfflineDataCache.OfflineData offlineData = new OfflineDataCache.OfflineData();
			offlineData.UniqueFakeDeckId = reader.ReadInt32();
			int num = reader.ReadInt32();
			offlineData.FakeDeckIds = new List<long>();
			for (int i = 0; i < num; i++)
			{
				offlineData.FakeDeckIds.Add(reader.ReadInt64());
			}
			int num2 = reader.ReadInt32();
			offlineData.OriginalDeckList = new List<DeckInfo>();
			for (int j = 0; j < num2; j++)
			{
				DeckInfo item = ReadProtoFromFile<DeckInfo>(reader);
				offlineData.OriginalDeckList.Add(item);
			}
			int num3 = reader.ReadInt32();
			offlineData.LocalDeckList = new List<DeckInfo>();
			for (int k = 0; k < num3; k++)
			{
				DeckInfo item2 = ReadProtoFromFile<DeckInfo>(reader);
				offlineData.LocalDeckList.Add(item2);
			}
			int num4 = reader.ReadInt32();
			offlineData.OriginalDeckContents = new List<DeckContents>();
			for (int l = 0; l < num4; l++)
			{
				DeckContents item3 = ReadProtoFromFile<DeckContents>(reader);
				offlineData.OriginalDeckContents.Add(item3);
			}
			int num5 = reader.ReadInt32();
			offlineData.LocalDeckContents = new List<DeckContents>();
			for (int m = 0; m < num5; m++)
			{
				DeckContents item4 = ReadProtoFromFile<DeckContents>(reader);
				offlineData.LocalDeckContents.Add(item4);
			}
			int num6 = reader.ReadInt32();
			offlineData.FavoriteHeroes = new List<FavoriteHero>();
			for (int n = 0; n < num6; n++)
			{
				FavoriteHero item5 = ReadProtoFromFile<FavoriteHero>(reader);
				offlineData.FavoriteHeroes.Add(item5);
			}
			offlineData.m_hasChangedFavoriteHeroesOffline = reader.ReadBoolean();
			offlineData.CardBacks = ReadProtoFromFile<CardBacks>(reader);
			offlineData.m_hasChangedCardBacksOffline = reader.ReadBoolean();
			return offlineData;
		}
	}

	private class OfflineDataSerializer_V1Deserializer : OfflineDataSerializerBase
	{
		public override OfflineDataCache.OfflineData Deserialize(BinaryReader reader)
		{
			if (reader == null)
			{
				Debug.LogError("Could not Deserialize v10 OfflineData, reader was null");
				return null;
			}
			OfflineDataCache.OfflineData offlineData = new OfflineDataCache.OfflineData();
			offlineData.UniqueFakeDeckId = reader.ReadInt32();
			int num = reader.ReadInt32();
			offlineData.FakeDeckIds = new List<long>();
			for (int i = 0; i < num; i++)
			{
				offlineData.FakeDeckIds.Add(reader.ReadInt64());
			}
			int num2 = reader.ReadInt32();
			offlineData.OriginalDeckList = new List<DeckInfo>();
			for (int j = 0; j < num2; j++)
			{
				DeckInfo item = ReadProtoFromFile<DeckInfo>(reader);
				offlineData.OriginalDeckList.Add(item);
			}
			int num3 = reader.ReadInt32();
			offlineData.LocalDeckList = new List<DeckInfo>();
			for (int k = 0; k < num3; k++)
			{
				DeckInfo item2 = ReadProtoFromFile<DeckInfo>(reader);
				offlineData.LocalDeckList.Add(item2);
			}
			int num4 = reader.ReadInt32();
			offlineData.OriginalDeckContents = new List<DeckContents>();
			for (int l = 0; l < num4; l++)
			{
				DeckContents item3 = ReadProtoFromFile<DeckContents>(reader);
				offlineData.OriginalDeckContents.Add(item3);
			}
			int num5 = reader.ReadInt32();
			offlineData.LocalDeckContents = new List<DeckContents>();
			for (int m = 0; m < num5; m++)
			{
				DeckContents item4 = ReadProtoFromFile<DeckContents>(reader);
				offlineData.LocalDeckContents.Add(item4);
			}
			int num6 = reader.ReadInt32();
			offlineData.FavoriteHeroes = new List<FavoriteHero>();
			for (int n = 0; n < num6; n++)
			{
				FavoriteHero item5 = ReadProtoFromFile<FavoriteHero>(reader);
				offlineData.FavoriteHeroes.Add(item5);
			}
			offlineData.m_hasChangedFavoriteHeroesOffline = reader.ReadBoolean();
			offlineData.CardBacks = ReadProtoFromFile<CardBacks>(reader);
			offlineData.m_hasChangedCardBacksOffline = reader.ReadBoolean();
			offlineData.Collection = ReadProtoFromFile<Collection>(reader);
			return offlineData;
		}
	}

	public static IOfflineDataSerializer GetSerializer(int serializerVersion)
	{
		return serializerVersion switch
		{
			0 => new OfflineDataSerializer_V0Deserializer(), 
			1 => new OfflineDataSerializer_V1Deserializer(), 
			_ => null, 
		};
	}

	private static T ReadProtoFromFile<T>(BinaryReader reader) where T : IProtoBuf, new()
	{
		int num = reader.ReadInt32();
		if (num == 0)
		{
			return default(T);
		}
		return ProtobufUtil.ParseFrom<T>(reader.ReadBytes(num), 0, num);
	}

	private static void AppendProtoToFile(BinaryWriter writer, IProtoBuf packet)
	{
		if (packet == null)
		{
			writer.Write(0);
			return;
		}
		byte[] array = ProtobufUtil.ToByteArray(packet);
		writer.Write(array.Length);
		writer.Write(array);
	}
}
