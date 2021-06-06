using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F7 RID: 1271
	public class MemModuleLoadResponse : IProtoBuf
	{
		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06005A54 RID: 23124 RVA: 0x00113E27 File Offset: 0x00112027
		// (set) Token: 0x06005A55 RID: 23125 RVA: 0x00113E2F File Offset: 0x0011202F
		public byte[] Data { get; set; }

		// Token: 0x06005A56 RID: 23126 RVA: 0x00113E38 File Offset: 0x00112038
		public void SetData(byte[] val)
		{
			this.Data = val;
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x00113E41 File Offset: 0x00112041
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Data.GetHashCode();
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x00113E5C File Offset: 0x0011205C
		public override bool Equals(object obj)
		{
			MemModuleLoadResponse memModuleLoadResponse = obj as MemModuleLoadResponse;
			return memModuleLoadResponse != null && this.Data.Equals(memModuleLoadResponse.Data);
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06005A59 RID: 23129 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x00113E8B File Offset: 0x0011208B
		public static MemModuleLoadResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<MemModuleLoadResponse>(bs, 0, -1);
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x00113E95 File Offset: 0x00112095
		public void Deserialize(Stream stream)
		{
			MemModuleLoadResponse.Deserialize(stream, this);
		}

		// Token: 0x06005A5C RID: 23132 RVA: 0x00113E9F File Offset: 0x0011209F
		public static MemModuleLoadResponse Deserialize(Stream stream, MemModuleLoadResponse instance)
		{
			return MemModuleLoadResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005A5D RID: 23133 RVA: 0x00113EAC File Offset: 0x001120AC
		public static MemModuleLoadResponse DeserializeLengthDelimited(Stream stream)
		{
			MemModuleLoadResponse memModuleLoadResponse = new MemModuleLoadResponse();
			MemModuleLoadResponse.DeserializeLengthDelimited(stream, memModuleLoadResponse);
			return memModuleLoadResponse;
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x00113EC8 File Offset: 0x001120C8
		public static MemModuleLoadResponse DeserializeLengthDelimited(Stream stream, MemModuleLoadResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MemModuleLoadResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x00113EF0 File Offset: 0x001120F0
		public static MemModuleLoadResponse Deserialize(Stream stream, MemModuleLoadResponse instance, long limit)
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
					instance.Data = ProtocolParser.ReadBytes(stream);
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

		// Token: 0x06005A60 RID: 23136 RVA: 0x00113F70 File Offset: 0x00112170
		public void Serialize(Stream stream)
		{
			MemModuleLoadResponse.Serialize(stream, this);
		}

		// Token: 0x06005A61 RID: 23137 RVA: 0x00113F79 File Offset: 0x00112179
		public static void Serialize(Stream stream, MemModuleLoadResponse instance)
		{
			if (instance.Data == null)
			{
				throw new ArgumentNullException("Data", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, instance.Data);
		}

		// Token: 0x06005A62 RID: 23138 RVA: 0x00113FA7 File Offset: 0x001121A7
		public uint GetSerializedSize()
		{
			return 0U + (ProtocolParser.SizeOfUInt32(this.Data.Length) + (uint)this.Data.Length) + 1U;
		}
	}
}
