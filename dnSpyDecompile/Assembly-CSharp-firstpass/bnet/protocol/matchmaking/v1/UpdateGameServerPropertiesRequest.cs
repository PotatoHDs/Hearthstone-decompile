using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003F1 RID: 1009
	public class UpdateGameServerPropertiesRequest : IProtoBuf
	{
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x000D3FE0 File Offset: 0x000D21E0
		// (set) Token: 0x060042DA RID: 17114 RVA: 0x000D3FE8 File Offset: 0x000D21E8
		public GameServerProperties ServerProperties
		{
			get
			{
				return this._ServerProperties;
			}
			set
			{
				this._ServerProperties = value;
				this.HasServerProperties = (value != null);
			}
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x000D3FFB File Offset: 0x000D21FB
		public void SetServerProperties(GameServerProperties val)
		{
			this.ServerProperties = val;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x000D4004 File Offset: 0x000D2204
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasServerProperties)
			{
				num ^= this.ServerProperties.GetHashCode();
			}
			return num;
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x000D4034 File Offset: 0x000D2234
		public override bool Equals(object obj)
		{
			UpdateGameServerPropertiesRequest updateGameServerPropertiesRequest = obj as UpdateGameServerPropertiesRequest;
			return updateGameServerPropertiesRequest != null && this.HasServerProperties == updateGameServerPropertiesRequest.HasServerProperties && (!this.HasServerProperties || this.ServerProperties.Equals(updateGameServerPropertiesRequest.ServerProperties));
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060042DE RID: 17118 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x000D4079 File Offset: 0x000D2279
		public static UpdateGameServerPropertiesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateGameServerPropertiesRequest>(bs, 0, -1);
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x000D4083 File Offset: 0x000D2283
		public void Deserialize(Stream stream)
		{
			UpdateGameServerPropertiesRequest.Deserialize(stream, this);
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x000D408D File Offset: 0x000D228D
		public static UpdateGameServerPropertiesRequest Deserialize(Stream stream, UpdateGameServerPropertiesRequest instance)
		{
			return UpdateGameServerPropertiesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x000D4098 File Offset: 0x000D2298
		public static UpdateGameServerPropertiesRequest DeserializeLengthDelimited(Stream stream)
		{
			UpdateGameServerPropertiesRequest updateGameServerPropertiesRequest = new UpdateGameServerPropertiesRequest();
			UpdateGameServerPropertiesRequest.DeserializeLengthDelimited(stream, updateGameServerPropertiesRequest);
			return updateGameServerPropertiesRequest;
		}

		// Token: 0x060042E3 RID: 17123 RVA: 0x000D40B4 File Offset: 0x000D22B4
		public static UpdateGameServerPropertiesRequest DeserializeLengthDelimited(Stream stream, UpdateGameServerPropertiesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateGameServerPropertiesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060042E4 RID: 17124 RVA: 0x000D40DC File Offset: 0x000D22DC
		public static UpdateGameServerPropertiesRequest Deserialize(Stream stream, UpdateGameServerPropertiesRequest instance, long limit)
		{
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
				else if (num == 10)
				{
					if (instance.ServerProperties == null)
					{
						instance.ServerProperties = GameServerProperties.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameServerProperties.DeserializeLengthDelimited(stream, instance.ServerProperties);
					}
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

		// Token: 0x060042E5 RID: 17125 RVA: 0x000D4176 File Offset: 0x000D2376
		public void Serialize(Stream stream)
		{
			UpdateGameServerPropertiesRequest.Serialize(stream, this);
		}

		// Token: 0x060042E6 RID: 17126 RVA: 0x000D417F File Offset: 0x000D237F
		public static void Serialize(Stream stream, UpdateGameServerPropertiesRequest instance)
		{
			if (instance.HasServerProperties)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.ServerProperties.GetSerializedSize());
				GameServerProperties.Serialize(stream, instance.ServerProperties);
			}
		}

		// Token: 0x060042E7 RID: 17127 RVA: 0x000D41B0 File Offset: 0x000D23B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasServerProperties)
			{
				num += 1U;
				uint serializedSize = this.ServerProperties.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x040016F1 RID: 5873
		public bool HasServerProperties;

		// Token: 0x040016F2 RID: 5874
		private GameServerProperties _ServerProperties;
	}
}
