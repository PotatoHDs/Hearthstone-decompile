using System;
using System.IO;
using System.Text;

namespace bnet.protocol.diag.v1
{
	// Token: 0x02000436 RID: 1078
	public class GetVarRequest : IProtoBuf
	{
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060048E7 RID: 18663 RVA: 0x000E416A File Offset: 0x000E236A
		// (set) Token: 0x060048E8 RID: 18664 RVA: 0x000E4172 File Offset: 0x000E2372
		public string Name { get; set; }

		// Token: 0x060048E9 RID: 18665 RVA: 0x000E417B File Offset: 0x000E237B
		public void SetName(string val)
		{
			this.Name = val;
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x000E4184 File Offset: 0x000E2384
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Name.GetHashCode();
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x000E41A0 File Offset: 0x000E23A0
		public override bool Equals(object obj)
		{
			GetVarRequest getVarRequest = obj as GetVarRequest;
			return getVarRequest != null && this.Name.Equals(getVarRequest.Name);
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x000E41CF File Offset: 0x000E23CF
		public static GetVarRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetVarRequest>(bs, 0, -1);
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x000E41D9 File Offset: 0x000E23D9
		public void Deserialize(Stream stream)
		{
			GetVarRequest.Deserialize(stream, this);
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x000E41E3 File Offset: 0x000E23E3
		public static GetVarRequest Deserialize(Stream stream, GetVarRequest instance)
		{
			return GetVarRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x000E41F0 File Offset: 0x000E23F0
		public static GetVarRequest DeserializeLengthDelimited(Stream stream)
		{
			GetVarRequest getVarRequest = new GetVarRequest();
			GetVarRequest.DeserializeLengthDelimited(stream, getVarRequest);
			return getVarRequest;
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x000E420C File Offset: 0x000E240C
		public static GetVarRequest DeserializeLengthDelimited(Stream stream, GetVarRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetVarRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x000E4234 File Offset: 0x000E2434
		public static GetVarRequest Deserialize(Stream stream, GetVarRequest instance, long limit)
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
					instance.Name = ProtocolParser.ReadString(stream);
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

		// Token: 0x060048F3 RID: 18675 RVA: 0x000E42B4 File Offset: 0x000E24B4
		public void Serialize(Stream stream)
		{
			GetVarRequest.Serialize(stream, this);
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x000E42BD File Offset: 0x000E24BD
		public static void Serialize(Stream stream, GetVarRequest instance)
		{
			if (instance.Name == null)
			{
				throw new ArgumentNullException("Name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x000E42F8 File Offset: 0x000E24F8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1U;
		}
	}
}
