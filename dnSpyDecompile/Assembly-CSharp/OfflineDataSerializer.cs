using System;
using System.Collections.Generic;
using System.IO;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x020008F4 RID: 2292
public static class OfflineDataSerializer
{
	// Token: 0x06007F64 RID: 32612 RVA: 0x0029532E File Offset: 0x0029352E
	public static IOfflineDataSerializer GetSerializer(int serializerVersion)
	{
		if (serializerVersion == 0)
		{
			return new OfflineDataSerializer.OfflineDataSerializer_V0Deserializer();
		}
		if (serializerVersion != 1)
		{
			return null;
		}
		return new OfflineDataSerializer.OfflineDataSerializer_V1Deserializer();
	}

	// Token: 0x06007F65 RID: 32613 RVA: 0x00295348 File Offset: 0x00293548
	private static T ReadProtoFromFile<T>(BinaryReader reader) where T : IProtoBuf, new()
	{
		int num = reader.ReadInt32();
		if (num == 0)
		{
			return default(T);
		}
		return ProtobufUtil.ParseFrom<T>(reader.ReadBytes(num), 0, num);
	}

	// Token: 0x06007F66 RID: 32614 RVA: 0x00295378 File Offset: 0x00293578
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

	// Token: 0x020025B0 RID: 9648
	private abstract class OfflineDataSerializerBase : IOfflineDataSerializer
	{
		// Token: 0x0601342A RID: 78890 RVA: 0x0052F180 File Offset: 0x0052D380
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
			foreach (long value in fakeDeckIds)
			{
				writer.Write(value);
			}
			List<DeckInfo> list = (data.OriginalDeckList == null) ? new List<DeckInfo>() : data.OriginalDeckList;
			List<DeckInfo> list2 = (data.LocalDeckList == null) ? new List<DeckInfo>() : data.LocalDeckList;
			writer.Write(list.Count);
			foreach (DeckInfo packet in list)
			{
				OfflineDataSerializer.AppendProtoToFile(writer, packet);
			}
			writer.Write(list2.Count);
			foreach (DeckInfo packet2 in list2)
			{
				OfflineDataSerializer.AppendProtoToFile(writer, packet2);
			}
			List<DeckContents> list3 = (data.OriginalDeckContents == null) ? new List<DeckContents>() : data.OriginalDeckContents;
			List<DeckContents> list4 = (data.LocalDeckContents == null) ? new List<DeckContents>() : data.LocalDeckContents;
			writer.Write(list3.Count);
			foreach (DeckContents packet3 in list3)
			{
				OfflineDataSerializer.AppendProtoToFile(writer, packet3);
			}
			writer.Write(list4.Count);
			foreach (DeckContents packet4 in list4)
			{
				OfflineDataSerializer.AppendProtoToFile(writer, packet4);
			}
			List<FavoriteHero> list5 = (data.FavoriteHeroes == null) ? new List<FavoriteHero>() : data.FavoriteHeroes;
			writer.Write(list5.Count);
			foreach (FavoriteHero packet5 in list5)
			{
				OfflineDataSerializer.AppendProtoToFile(writer, packet5);
			}
			writer.Write(data.m_hasChangedFavoriteHeroesOffline);
			OfflineDataSerializer.AppendProtoToFile(writer, data.CardBacks);
			writer.Write(data.m_hasChangedCardBacksOffline);
			OfflineDataSerializer.AppendProtoToFile(writer, data.Collection);
		}

		// Token: 0x0601342B RID: 78891
		public abstract OfflineDataCache.OfflineData Deserialize(BinaryReader reader);
	}

	// Token: 0x020025B1 RID: 9649
	private class OfflineDataSerializer_V0Deserializer : OfflineDataSerializer.OfflineDataSerializerBase
	{
		// Token: 0x0601342D RID: 78893 RVA: 0x0052F420 File Offset: 0x0052D620
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
				DeckInfo item = OfflineDataSerializer.ReadProtoFromFile<DeckInfo>(reader);
				offlineData.OriginalDeckList.Add(item);
			}
			int num3 = reader.ReadInt32();
			offlineData.LocalDeckList = new List<DeckInfo>();
			for (int k = 0; k < num3; k++)
			{
				DeckInfo item2 = OfflineDataSerializer.ReadProtoFromFile<DeckInfo>(reader);
				offlineData.LocalDeckList.Add(item2);
			}
			int num4 = reader.ReadInt32();
			offlineData.OriginalDeckContents = new List<DeckContents>();
			for (int l = 0; l < num4; l++)
			{
				DeckContents item3 = OfflineDataSerializer.ReadProtoFromFile<DeckContents>(reader);
				offlineData.OriginalDeckContents.Add(item3);
			}
			int num5 = reader.ReadInt32();
			offlineData.LocalDeckContents = new List<DeckContents>();
			for (int m = 0; m < num5; m++)
			{
				DeckContents item4 = OfflineDataSerializer.ReadProtoFromFile<DeckContents>(reader);
				offlineData.LocalDeckContents.Add(item4);
			}
			int num6 = reader.ReadInt32();
			offlineData.FavoriteHeroes = new List<FavoriteHero>();
			for (int n = 0; n < num6; n++)
			{
				FavoriteHero item5 = OfflineDataSerializer.ReadProtoFromFile<FavoriteHero>(reader);
				offlineData.FavoriteHeroes.Add(item5);
			}
			offlineData.m_hasChangedFavoriteHeroesOffline = reader.ReadBoolean();
			offlineData.CardBacks = OfflineDataSerializer.ReadProtoFromFile<CardBacks>(reader);
			offlineData.m_hasChangedCardBacksOffline = reader.ReadBoolean();
			return offlineData;
		}
	}

	// Token: 0x020025B2 RID: 9650
	private class OfflineDataSerializer_V1Deserializer : OfflineDataSerializer.OfflineDataSerializerBase
	{
		// Token: 0x0601342F RID: 78895 RVA: 0x0052F5C8 File Offset: 0x0052D7C8
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
				DeckInfo item = OfflineDataSerializer.ReadProtoFromFile<DeckInfo>(reader);
				offlineData.OriginalDeckList.Add(item);
			}
			int num3 = reader.ReadInt32();
			offlineData.LocalDeckList = new List<DeckInfo>();
			for (int k = 0; k < num3; k++)
			{
				DeckInfo item2 = OfflineDataSerializer.ReadProtoFromFile<DeckInfo>(reader);
				offlineData.LocalDeckList.Add(item2);
			}
			int num4 = reader.ReadInt32();
			offlineData.OriginalDeckContents = new List<DeckContents>();
			for (int l = 0; l < num4; l++)
			{
				DeckContents item3 = OfflineDataSerializer.ReadProtoFromFile<DeckContents>(reader);
				offlineData.OriginalDeckContents.Add(item3);
			}
			int num5 = reader.ReadInt32();
			offlineData.LocalDeckContents = new List<DeckContents>();
			for (int m = 0; m < num5; m++)
			{
				DeckContents item4 = OfflineDataSerializer.ReadProtoFromFile<DeckContents>(reader);
				offlineData.LocalDeckContents.Add(item4);
			}
			int num6 = reader.ReadInt32();
			offlineData.FavoriteHeroes = new List<FavoriteHero>();
			for (int n = 0; n < num6; n++)
			{
				FavoriteHero item5 = OfflineDataSerializer.ReadProtoFromFile<FavoriteHero>(reader);
				offlineData.FavoriteHeroes.Add(item5);
			}
			offlineData.m_hasChangedFavoriteHeroesOffline = reader.ReadBoolean();
			offlineData.CardBacks = OfflineDataSerializer.ReadProtoFromFile<CardBacks>(reader);
			offlineData.m_hasChangedCardBacksOffline = reader.ReadBoolean();
			offlineData.Collection = OfflineDataSerializer.ReadProtoFromFile<Collection>(reader);
			return offlineData;
		}
	}
}
