using System;
using System.IO;

namespace bnet.protocol.games.v2
{
	// Token: 0x02000369 RID: 873
	public class GameId : IProtoBuf
	{
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x0600374D RID: 14157 RVA: 0x000B5E43 File Offset: 0x000B4043
		// (set) Token: 0x0600374E RID: 14158 RVA: 0x000B5E4B File Offset: 0x000B404B
		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000B5E5B File Offset: 0x000B405B
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x000B5E64 File Offset: 0x000B4064
		// (set) Token: 0x06003751 RID: 14161 RVA: 0x000B5E6C File Offset: 0x000B406C
		public ProcessId ServerId
		{
			get
			{
				return this._ServerId;
			}
			set
			{
				this._ServerId = value;
				this.HasServerId = (value != null);
			}
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000B5E7F File Offset: 0x000B407F
		public void SetServerId(ProcessId val)
		{
			this.ServerId = val;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000B5E88 File Offset: 0x000B4088
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasServerId)
			{
				num ^= this.ServerId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000B5ED4 File Offset: 0x000B40D4
		public override bool Equals(object obj)
		{
			GameId gameId = obj as GameId;
			return gameId != null && this.HasId == gameId.HasId && (!this.HasId || this.Id.Equals(gameId.Id)) && this.HasServerId == gameId.HasServerId && (!this.HasServerId || this.ServerId.Equals(gameId.ServerId));
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000B5F47 File Offset: 0x000B4147
		public static GameId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameId>(bs, 0, -1);
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000B5F51 File Offset: 0x000B4151
		public void Deserialize(Stream stream)
		{
			GameId.Deserialize(stream, this);
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000B5F5B File Offset: 0x000B415B
		public static GameId Deserialize(Stream stream, GameId instance)
		{
			return GameId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000B5F68 File Offset: 0x000B4168
		public static GameId DeserializeLengthDelimited(Stream stream)
		{
			GameId gameId = new GameId();
			GameId.DeserializeLengthDelimited(stream, gameId);
			return gameId;
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000B5F84 File Offset: 0x000B4184
		public static GameId DeserializeLengthDelimited(Stream stream, GameId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameId.Deserialize(stream, instance, num);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000B5FAC File Offset: 0x000B41AC
		public static GameId Deserialize(Stream stream, GameId instance, long limit)
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
				else if (num != 13)
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
					else if (instance.ServerId == null)
					{
						instance.ServerId = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.ServerId);
					}
				}
				else
				{
					instance.Id = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000B6065 File Offset: 0x000B4265
		public void Serialize(Stream stream)
		{
			GameId.Serialize(stream, this);
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x000B6070 File Offset: 0x000B4270
		public static void Serialize(Stream stream, GameId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.Id);
			}
			if (instance.HasServerId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ServerId.GetSerializedSize());
				ProcessId.Serialize(stream, instance.ServerId);
			}
		}

		// Token: 0x0600375E RID: 14174 RVA: 0x000B60D0 File Offset: 0x000B42D0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasServerId)
			{
				num += 1U;
				uint serializedSize = this.ServerId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040014B5 RID: 5301
		public bool HasId;

		// Token: 0x040014B6 RID: 5302
		private uint _Id;

		// Token: 0x040014B7 RID: 5303
		public bool HasServerId;

		// Token: 0x040014B8 RID: 5304
		private ProcessId _ServerId;
	}
}
