using System;
using System.IO;
using System.Text;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200037A RID: 890
	public class RegisterServerResponse : IProtoBuf
	{
		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000B97C7 File Offset: 0x000B79C7
		// (set) Token: 0x060038B4 RID: 14516 RVA: 0x000B97CF File Offset: 0x000B79CF
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

		// Token: 0x060038B5 RID: 14517 RVA: 0x000B97E2 File Offset: 0x000B79E2
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000B97EC File Offset: 0x000B79EC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000B981C File Offset: 0x000B7A1C
		public override bool Equals(object obj)
		{
			RegisterServerResponse registerServerResponse = obj as RegisterServerResponse;
			return registerServerResponse != null && this.HasClientId == registerServerResponse.HasClientId && (!this.HasClientId || this.ClientId.Equals(registerServerResponse.ClientId));
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060038B8 RID: 14520 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000B9861 File Offset: 0x000B7A61
		public static RegisterServerResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterServerResponse>(bs, 0, -1);
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000B986B File Offset: 0x000B7A6B
		public void Deserialize(Stream stream)
		{
			RegisterServerResponse.Deserialize(stream, this);
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000B9875 File Offset: 0x000B7A75
		public static RegisterServerResponse Deserialize(Stream stream, RegisterServerResponse instance)
		{
			return RegisterServerResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000B9880 File Offset: 0x000B7A80
		public static RegisterServerResponse DeserializeLengthDelimited(Stream stream)
		{
			RegisterServerResponse registerServerResponse = new RegisterServerResponse();
			RegisterServerResponse.DeserializeLengthDelimited(stream, registerServerResponse);
			return registerServerResponse;
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000B989C File Offset: 0x000B7A9C
		public static RegisterServerResponse DeserializeLengthDelimited(Stream stream, RegisterServerResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterServerResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x000B98C4 File Offset: 0x000B7AC4
		public static RegisterServerResponse Deserialize(Stream stream, RegisterServerResponse instance, long limit)
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

		// Token: 0x060038BF RID: 14527 RVA: 0x000B9944 File Offset: 0x000B7B44
		public void Serialize(Stream stream)
		{
			RegisterServerResponse.Serialize(stream, this);
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000B994D File Offset: 0x000B7B4D
		public static void Serialize(Stream stream, RegisterServerResponse instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000B9978 File Offset: 0x000B7B78
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

		// Token: 0x04001505 RID: 5381
		public bool HasClientId;

		// Token: 0x04001506 RID: 5382
		private string _ClientId;
	}
}
