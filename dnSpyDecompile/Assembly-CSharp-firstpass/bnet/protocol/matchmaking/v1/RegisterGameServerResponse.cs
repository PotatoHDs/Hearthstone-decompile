using System;
using System.IO;
using System.Text;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003F0 RID: 1008
	public class RegisterGameServerResponse : IProtoBuf
	{
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x000D3D2F File Offset: 0x000D1F2F
		// (set) Token: 0x060042C7 RID: 17095 RVA: 0x000D3D37 File Offset: 0x000D1F37
		public ulong GameServerGuid
		{
			get
			{
				return this._GameServerGuid;
			}
			set
			{
				this._GameServerGuid = value;
				this.HasGameServerGuid = true;
			}
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x000D3D47 File Offset: 0x000D1F47
		public void SetGameServerGuid(ulong val)
		{
			this.GameServerGuid = val;
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x000D3D50 File Offset: 0x000D1F50
		// (set) Token: 0x060042CA RID: 17098 RVA: 0x000D3D58 File Offset: 0x000D1F58
		public string ClientId
		{
			get
			{
				return this._ClientId;
			}
			set
			{
				this._ClientId = value;
				this.HasClientId = (value != null);
			}
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x000D3D6B File Offset: 0x000D1F6B
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x000D3D74 File Offset: 0x000D1F74
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameServerGuid)
			{
				num ^= this.GameServerGuid.GetHashCode();
			}
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000D3DC0 File Offset: 0x000D1FC0
		public override bool Equals(object obj)
		{
			RegisterGameServerResponse registerGameServerResponse = obj as RegisterGameServerResponse;
			return registerGameServerResponse != null && this.HasGameServerGuid == registerGameServerResponse.HasGameServerGuid && (!this.HasGameServerGuid || this.GameServerGuid.Equals(registerGameServerResponse.GameServerGuid)) && this.HasClientId == registerGameServerResponse.HasClientId && (!this.HasClientId || this.ClientId.Equals(registerGameServerResponse.ClientId));
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060042CE RID: 17102 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x000D3E33 File Offset: 0x000D2033
		public static RegisterGameServerResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterGameServerResponse>(bs, 0, -1);
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x000D3E3D File Offset: 0x000D203D
		public void Deserialize(Stream stream)
		{
			RegisterGameServerResponse.Deserialize(stream, this);
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x000D3E47 File Offset: 0x000D2047
		public static RegisterGameServerResponse Deserialize(Stream stream, RegisterGameServerResponse instance)
		{
			return RegisterGameServerResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x000D3E54 File Offset: 0x000D2054
		public static RegisterGameServerResponse DeserializeLengthDelimited(Stream stream)
		{
			RegisterGameServerResponse registerGameServerResponse = new RegisterGameServerResponse();
			RegisterGameServerResponse.DeserializeLengthDelimited(stream, registerGameServerResponse);
			return registerGameServerResponse;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x000D3E70 File Offset: 0x000D2070
		public static RegisterGameServerResponse DeserializeLengthDelimited(Stream stream, RegisterGameServerResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterGameServerResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000D3E98 File Offset: 0x000D2098
		public static RegisterGameServerResponse Deserialize(Stream stream, RegisterGameServerResponse instance, long limit)
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
					else
					{
						instance.ClientId = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.GameServerGuid = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000D3F37 File Offset: 0x000D2137
		public void Serialize(Stream stream)
		{
			RegisterGameServerResponse.Serialize(stream, this);
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000D3F40 File Offset: 0x000D2140
		public static void Serialize(Stream stream, RegisterGameServerResponse instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasGameServerGuid)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.GameServerGuid);
			}
			if (instance.HasClientId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000D3F98 File Offset: 0x000D2198
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameServerGuid)
			{
				num += 1U;
				num += 8U;
			}
			if (this.HasClientId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040016ED RID: 5869
		public bool HasGameServerGuid;

		// Token: 0x040016EE RID: 5870
		private ulong _GameServerGuid;

		// Token: 0x040016EF RID: 5871
		public bool HasClientId;

		// Token: 0x040016F0 RID: 5872
		private string _ClientId;
	}
}
