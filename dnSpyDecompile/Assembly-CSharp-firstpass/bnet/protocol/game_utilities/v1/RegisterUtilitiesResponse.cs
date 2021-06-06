using System;
using System.IO;
using System.Text;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000364 RID: 868
	public class RegisterUtilitiesResponse : IProtoBuf
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x000B4F73 File Offset: 0x000B3173
		// (set) Token: 0x060036EA RID: 14058 RVA: 0x000B4F7B File Offset: 0x000B317B
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

		// Token: 0x060036EB RID: 14059 RVA: 0x000B4F8E File Offset: 0x000B318E
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000B4F98 File Offset: 0x000B3198
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000B4FC8 File Offset: 0x000B31C8
		public override bool Equals(object obj)
		{
			RegisterUtilitiesResponse registerUtilitiesResponse = obj as RegisterUtilitiesResponse;
			return registerUtilitiesResponse != null && this.HasClientId == registerUtilitiesResponse.HasClientId && (!this.HasClientId || this.ClientId.Equals(registerUtilitiesResponse.ClientId));
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000B500D File Offset: 0x000B320D
		public static RegisterUtilitiesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterUtilitiesResponse>(bs, 0, -1);
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000B5017 File Offset: 0x000B3217
		public void Deserialize(Stream stream)
		{
			RegisterUtilitiesResponse.Deserialize(stream, this);
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000B5021 File Offset: 0x000B3221
		public static RegisterUtilitiesResponse Deserialize(Stream stream, RegisterUtilitiesResponse instance)
		{
			return RegisterUtilitiesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000B502C File Offset: 0x000B322C
		public static RegisterUtilitiesResponse DeserializeLengthDelimited(Stream stream)
		{
			RegisterUtilitiesResponse registerUtilitiesResponse = new RegisterUtilitiesResponse();
			RegisterUtilitiesResponse.DeserializeLengthDelimited(stream, registerUtilitiesResponse);
			return registerUtilitiesResponse;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000B5048 File Offset: 0x000B3248
		public static RegisterUtilitiesResponse DeserializeLengthDelimited(Stream stream, RegisterUtilitiesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterUtilitiesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060036F4 RID: 14068 RVA: 0x000B5070 File Offset: 0x000B3270
		public static RegisterUtilitiesResponse Deserialize(Stream stream, RegisterUtilitiesResponse instance, long limit)
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
					instance.ClientId = ProtocolParser.ReadString(stream);
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

		// Token: 0x060036F5 RID: 14069 RVA: 0x000B50F0 File Offset: 0x000B32F0
		public void Serialize(Stream stream)
		{
			RegisterUtilitiesResponse.Serialize(stream, this);
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x000B50F9 File Offset: 0x000B32F9
		public static void Serialize(Stream stream, RegisterUtilitiesResponse instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x000B5124 File Offset: 0x000B3324
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasClientId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.ClientId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x040014A6 RID: 5286
		public bool HasClientId;

		// Token: 0x040014A7 RID: 5287
		private string _ClientId;
	}
}
