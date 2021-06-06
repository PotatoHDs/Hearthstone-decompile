using System;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000385 RID: 901
	public class GetGameStatsRequest : IProtoBuf
	{
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600397C RID: 14716 RVA: 0x000BB1A6 File Offset: 0x000B93A6
		// (set) Token: 0x0600397D RID: 14717 RVA: 0x000BB1AE File Offset: 0x000B93AE
		public ulong FactoryId { get; set; }

		// Token: 0x0600397E RID: 14718 RVA: 0x000BB1B7 File Offset: 0x000B93B7
		public void SetFactoryId(ulong val)
		{
			this.FactoryId = val;
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x000BB1C0 File Offset: 0x000B93C0
		// (set) Token: 0x06003980 RID: 14720 RVA: 0x000BB1C8 File Offset: 0x000B93C8
		public AttributeFilter Filter { get; set; }

		// Token: 0x06003981 RID: 14721 RVA: 0x000BB1D1 File Offset: 0x000B93D1
		public void SetFilter(AttributeFilter val)
		{
			this.Filter = val;
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000BB1DC File Offset: 0x000B93DC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.FactoryId.GetHashCode() ^ this.Filter.GetHashCode();
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000BB210 File Offset: 0x000B9410
		public override bool Equals(object obj)
		{
			GetGameStatsRequest getGameStatsRequest = obj as GetGameStatsRequest;
			return getGameStatsRequest != null && this.FactoryId.Equals(getGameStatsRequest.FactoryId) && this.Filter.Equals(getGameStatsRequest.Filter);
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000BB257 File Offset: 0x000B9457
		public static GetGameStatsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameStatsRequest>(bs, 0, -1);
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000BB261 File Offset: 0x000B9461
		public void Deserialize(Stream stream)
		{
			GetGameStatsRequest.Deserialize(stream, this);
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000BB26B File Offset: 0x000B946B
		public static GetGameStatsRequest Deserialize(Stream stream, GetGameStatsRequest instance)
		{
			return GetGameStatsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000BB278 File Offset: 0x000B9478
		public static GetGameStatsRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameStatsRequest getGameStatsRequest = new GetGameStatsRequest();
			GetGameStatsRequest.DeserializeLengthDelimited(stream, getGameStatsRequest);
			return getGameStatsRequest;
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000BB294 File Offset: 0x000B9494
		public static GetGameStatsRequest DeserializeLengthDelimited(Stream stream, GetGameStatsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameStatsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000BB2BC File Offset: 0x000B94BC
		public static GetGameStatsRequest Deserialize(Stream stream, GetGameStatsRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num != 9)
				{
					if (num != 18)
					{
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.Filter == null)
					{
						instance.Filter = AttributeFilter.DeserializeLengthDelimited(stream);
					}
					else
					{
						AttributeFilter.DeserializeLengthDelimited(stream, instance.Filter);
					}
				}
				else
				{
					instance.FactoryId = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000BB375 File Offset: 0x000B9575
		public void Serialize(Stream stream)
		{
			GetGameStatsRequest.Serialize(stream, this);
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000BB380 File Offset: 0x000B9580
		public static void Serialize(Stream stream, GetGameStatsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.FactoryId);
			if (instance.Filter == null)
			{
				throw new ArgumentNullException("Filter", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Filter.GetSerializedSize());
			AttributeFilter.Serialize(stream, instance.Filter);
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000BB3E4 File Offset: 0x000B95E4
		public uint GetSerializedSize()
		{
			uint num = 0U + 8U;
			uint serializedSize = this.Filter.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2U;
		}
	}
}
