using System;
using System.IO;

namespace PegasusGame
{
	// Token: 0x02000191 RID: 401
	public class Tag : IProtoBuf
	{
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00057BA5 File Offset: 0x00055DA5
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x00057BAD File Offset: 0x00055DAD
		public int Name { get; set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00057BB6 File Offset: 0x00055DB6
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x00057BBE File Offset: 0x00055DBE
		public int Value { get; set; }

		// Token: 0x060018F0 RID: 6384 RVA: 0x00057BC8 File Offset: 0x00055DC8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Name.GetHashCode() ^ this.Value.GetHashCode();
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00057C00 File Offset: 0x00055E00
		public override bool Equals(object obj)
		{
			Tag tag = obj as Tag;
			return tag != null && this.Name.Equals(tag.Name) && this.Value.Equals(tag.Value);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00057C4A File Offset: 0x00055E4A
		public void Deserialize(Stream stream)
		{
			Tag.Deserialize(stream, this);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00057C54 File Offset: 0x00055E54
		public static Tag Deserialize(Stream stream, Tag instance)
		{
			return Tag.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00057C60 File Offset: 0x00055E60
		public static Tag DeserializeLengthDelimited(Stream stream)
		{
			Tag tag = new Tag();
			Tag.DeserializeLengthDelimited(stream, tag);
			return tag;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x00057C7C File Offset: 0x00055E7C
		public static Tag DeserializeLengthDelimited(Stream stream, Tag instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Tag.Deserialize(stream, instance, num);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00057CA4 File Offset: 0x00055EA4
		public static Tag Deserialize(Stream stream, Tag instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
						instance.Value = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Name = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00057D3D File Offset: 0x00055F3D
		public void Serialize(Stream stream)
		{
			Tag.Serialize(stream, this);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00057D46 File Offset: 0x00055F46
		public static void Serialize(Stream stream, Tag instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Name));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Value));
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00057D71 File Offset: 0x00055F71
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Name)) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Value)) + 2U;
		}
	}
}
