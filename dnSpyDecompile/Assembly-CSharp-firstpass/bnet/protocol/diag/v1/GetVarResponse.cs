using System;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	// Token: 0x02000437 RID: 1079
	public class GetVarResponse : IProtoBuf
	{
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060048F7 RID: 18679 RVA: 0x000E4322 File Offset: 0x000E2522
		// (set) Token: 0x060048F8 RID: 18680 RVA: 0x000E432A File Offset: 0x000E252A
		public string Value { get; set; }

		// Token: 0x060048F9 RID: 18681 RVA: 0x000E4333 File Offset: 0x000E2533
		public void SetValue(string val)
		{
			this.Value = val;
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x000E433C File Offset: 0x000E253C
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x000E4358 File Offset: 0x000E2558
		public override bool Equals(object obj)
		{
			GetVarResponse getVarResponse = obj as GetVarResponse;
			return getVarResponse != null && this.Value.Equals(getVarResponse.Value);
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060048FC RID: 18684 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x000E4387 File Offset: 0x000E2587
		public static GetVarResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetVarResponse>(bs, 0, -1);
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x000E4391 File Offset: 0x000E2591
		public void Deserialize(Stream stream)
		{
			GetVarResponse.Deserialize(stream, this);
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x000E439B File Offset: 0x000E259B
		public static GetVarResponse Deserialize(Stream stream, GetVarResponse instance)
		{
			return GetVarResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x000E43A8 File Offset: 0x000E25A8
		public static GetVarResponse DeserializeLengthDelimited(Stream stream)
		{
			GetVarResponse getVarResponse = new GetVarResponse();
			GetVarResponse.DeserializeLengthDelimited(stream, getVarResponse);
			return getVarResponse;
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x000E43C4 File Offset: 0x000E25C4
		public static GetVarResponse DeserializeLengthDelimited(Stream stream, GetVarResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetVarResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x000E43EC File Offset: 0x000E25EC
		public static GetVarResponse Deserialize(Stream stream, GetVarResponse instance, long limit)
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
					instance.Value = ProtocolParser.ReadString(stream);
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

		// Token: 0x06004903 RID: 18691 RVA: 0x000E446C File Offset: 0x000E266C
		public void Serialize(Stream stream)
		{
			GetVarResponse.Serialize(stream, this);
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x000E4475 File Offset: 0x000E2675
		public static void Serialize(Stream stream, GetVarResponse instance)
		{
			if (instance.Value == null)
			{
				throw new ArgumentNullException("Value", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x000E44B0 File Offset: 0x000E26B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Value);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1U;
		}
	}
}
