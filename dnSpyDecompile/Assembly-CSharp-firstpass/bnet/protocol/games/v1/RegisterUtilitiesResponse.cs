using System;
using System.IO;
using System.Text;

namespace bnet.protocol.games.v1
{
	// Token: 0x0200037D RID: 893
	public class RegisterUtilitiesResponse : IProtoBuf
	{
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000B9E2F File Offset: 0x000B802F
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x000B9E37 File Offset: 0x000B8037
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

		// Token: 0x060038E9 RID: 14569 RVA: 0x000B9E4A File Offset: 0x000B804A
		public void SetClientId(string val)
		{
			this.ClientId = val;
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000B9E54 File Offset: 0x000B8054
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasClientId)
			{
				num ^= this.ClientId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000B9E84 File Offset: 0x000B8084
		public override bool Equals(object obj)
		{
			RegisterUtilitiesResponse registerUtilitiesResponse = obj as RegisterUtilitiesResponse;
			return registerUtilitiesResponse != null && this.HasClientId == registerUtilitiesResponse.HasClientId && (!this.HasClientId || this.ClientId.Equals(registerUtilitiesResponse.ClientId));
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000B9EC9 File Offset: 0x000B80C9
		public static RegisterUtilitiesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RegisterUtilitiesResponse>(bs, 0, -1);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000B9ED3 File Offset: 0x000B80D3
		public void Deserialize(Stream stream)
		{
			RegisterUtilitiesResponse.Deserialize(stream, this);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000B9EDD File Offset: 0x000B80DD
		public static RegisterUtilitiesResponse Deserialize(Stream stream, RegisterUtilitiesResponse instance)
		{
			return RegisterUtilitiesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000B9EE8 File Offset: 0x000B80E8
		public static RegisterUtilitiesResponse DeserializeLengthDelimited(Stream stream)
		{
			RegisterUtilitiesResponse registerUtilitiesResponse = new RegisterUtilitiesResponse();
			RegisterUtilitiesResponse.DeserializeLengthDelimited(stream, registerUtilitiesResponse);
			return registerUtilitiesResponse;
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000B9F04 File Offset: 0x000B8104
		public static RegisterUtilitiesResponse DeserializeLengthDelimited(Stream stream, RegisterUtilitiesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return RegisterUtilitiesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000B9F2C File Offset: 0x000B812C
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

		// Token: 0x060038F3 RID: 14579 RVA: 0x000B9FAC File Offset: 0x000B81AC
		public void Serialize(Stream stream)
		{
			RegisterUtilitiesResponse.Serialize(stream, this);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000B9FB5 File Offset: 0x000B81B5
		public static void Serialize(Stream stream, RegisterUtilitiesResponse instance)
		{
			if (instance.HasClientId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientId));
			}
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000B9FE0 File Offset: 0x000B81E0
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

		// Token: 0x04001509 RID: 5385
		public bool HasClientId;

		// Token: 0x0400150A RID: 5386
		private string _ClientId;
	}
}
