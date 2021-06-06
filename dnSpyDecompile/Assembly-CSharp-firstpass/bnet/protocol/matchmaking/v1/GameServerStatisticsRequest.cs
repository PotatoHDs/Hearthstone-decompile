using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003D0 RID: 976
	public class GameServerStatisticsRequest : IProtoBuf
	{
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003FF6 RID: 16374 RVA: 0x000CC421 File Offset: 0x000CA621
		// (set) Token: 0x06003FF7 RID: 16375 RVA: 0x000CC429 File Offset: 0x000CA629
		public ulong MatchmakerGuid
		{
			get
			{
				return this._MatchmakerGuid;
			}
			set
			{
				this._MatchmakerGuid = value;
				this.HasMatchmakerGuid = true;
			}
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000CC439 File Offset: 0x000CA639
		public void SetMatchmakerGuid(ulong val)
		{
			this.MatchmakerGuid = val;
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000CC444 File Offset: 0x000CA644
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasMatchmakerGuid)
			{
				num ^= this.MatchmakerGuid.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000CC478 File Offset: 0x000CA678
		public override bool Equals(object obj)
		{
			GameServerStatisticsRequest gameServerStatisticsRequest = obj as GameServerStatisticsRequest;
			return gameServerStatisticsRequest != null && this.HasMatchmakerGuid == gameServerStatisticsRequest.HasMatchmakerGuid && (!this.HasMatchmakerGuid || this.MatchmakerGuid.Equals(gameServerStatisticsRequest.MatchmakerGuid));
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003FFB RID: 16379 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000CC4C0 File Offset: 0x000CA6C0
		public static GameServerStatisticsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameServerStatisticsRequest>(bs, 0, -1);
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000CC4CA File Offset: 0x000CA6CA
		public void Deserialize(Stream stream)
		{
			GameServerStatisticsRequest.Deserialize(stream, this);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000CC4D4 File Offset: 0x000CA6D4
		public static GameServerStatisticsRequest Deserialize(Stream stream, GameServerStatisticsRequest instance)
		{
			return GameServerStatisticsRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000CC4E0 File Offset: 0x000CA6E0
		public static GameServerStatisticsRequest DeserializeLengthDelimited(Stream stream)
		{
			GameServerStatisticsRequest gameServerStatisticsRequest = new GameServerStatisticsRequest();
			GameServerStatisticsRequest.DeserializeLengthDelimited(stream, gameServerStatisticsRequest);
			return gameServerStatisticsRequest;
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000CC4FC File Offset: 0x000CA6FC
		public static GameServerStatisticsRequest DeserializeLengthDelimited(Stream stream, GameServerStatisticsRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameServerStatisticsRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000CC524 File Offset: 0x000CA724
		public static GameServerStatisticsRequest Deserialize(Stream stream, GameServerStatisticsRequest instance, long limit)
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
				else if (num == 9)
				{
					instance.MatchmakerGuid = binaryReader.ReadUInt64();
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000CC5AB File Offset: 0x000CA7AB
		public void Serialize(Stream stream)
		{
			GameServerStatisticsRequest.Serialize(stream, this);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x000CC5B4 File Offset: 0x000CA7B4
		public static void Serialize(Stream stream, GameServerStatisticsRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasMatchmakerGuid)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.MatchmakerGuid);
			}
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x000CC5E4 File Offset: 0x000CA7E4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasMatchmakerGuid)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x0400165D RID: 5725
		public bool HasMatchmakerGuid;

		// Token: 0x0400165E RID: 5726
		private ulong _MatchmakerGuid;
	}
}
