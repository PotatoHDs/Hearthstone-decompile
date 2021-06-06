using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x020001B8 RID: 440
	public class NAckOption : IProtoBuf
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x00062A17 File Offset: 0x00060C17
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x00062A1F File Offset: 0x00060C1F
		public int Id { get; set; }

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00062A28 File Offset: 0x00060C28
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Id.GetHashCode();
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00062A50 File Offset: 0x00060C50
		public override bool Equals(object obj)
		{
			NAckOption nackOption = obj as NAckOption;
			return nackOption != null && this.Id.Equals(nackOption.Id);
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00062A82 File Offset: 0x00060C82
		public void Deserialize(Stream stream)
		{
			NAckOption.Deserialize(stream, this);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00062A8C File Offset: 0x00060C8C
		public static NAckOption Deserialize(Stream stream, NAckOption instance)
		{
			return NAckOption.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00062A98 File Offset: 0x00060C98
		public static NAckOption DeserializeLengthDelimited(Stream stream)
		{
			NAckOption nackOption = new NAckOption();
			NAckOption.DeserializeLengthDelimited(stream, nackOption);
			return nackOption;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00062AB4 File Offset: 0x00060CB4
		public static NAckOption DeserializeLengthDelimited(Stream stream, NAckOption instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return NAckOption.Deserialize(stream, instance, num);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00062ADC File Offset: 0x00060CDC
		public static NAckOption Deserialize(Stream stream, NAckOption instance, long limit)
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
				else if (num == 8)
				{
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
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

		// Token: 0x06001BF8 RID: 7160 RVA: 0x00062B5C File Offset: 0x00060D5C
		public void Serialize(Stream stream)
		{
			NAckOption.Serialize(stream, this);
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00062B65 File Offset: 0x00060D65
		public static void Serialize(Stream stream, NAckOption instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00062B7B File Offset: 0x00060D7B
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Id)) + 1U;
		}

		// Token: 0x0200064F RID: 1615
		public enum PacketID
		{
			// Token: 0x04002112 RID: 8466
			ID = 10
		}
	}
}
