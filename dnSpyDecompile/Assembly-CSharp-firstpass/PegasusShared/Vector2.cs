using System;
using System.IO;

namespace PegasusShared
{
	// Token: 0x0200014C RID: 332
	public class Vector2 : IProtoBuf
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0004B4A3 File Offset: 0x000496A3
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0004B4AB File Offset: 0x000496AB
		public float X { get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0004B4B4 File Offset: 0x000496B4
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0004B4BC File Offset: 0x000496BC
		public float Y { get; set; }

		// Token: 0x060015FA RID: 5626 RVA: 0x0004B4C8 File Offset: 0x000496C8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0004B500 File Offset: 0x00049700
		public override bool Equals(object obj)
		{
			Vector2 vector = obj as Vector2;
			return vector != null && this.X.Equals(vector.X) && this.Y.Equals(vector.Y);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0004B54A File Offset: 0x0004974A
		public void Deserialize(Stream stream)
		{
			Vector2.Deserialize(stream, this);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0004B554 File Offset: 0x00049754
		public static Vector2 Deserialize(Stream stream, Vector2 instance)
		{
			return Vector2.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0004B560 File Offset: 0x00049760
		public static Vector2 DeserializeLengthDelimited(Stream stream)
		{
			Vector2 vector = new Vector2();
			Vector2.DeserializeLengthDelimited(stream, vector);
			return vector;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0004B57C File Offset: 0x0004977C
		public static Vector2 DeserializeLengthDelimited(Stream stream, Vector2 instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Vector2.Deserialize(stream, instance, num);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0004B5A4 File Offset: 0x000497A4
		public static Vector2 Deserialize(Stream stream, Vector2 instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.X = 0f;
			instance.Y = 0f;
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
				else if (num != 13)
				{
					if (num != 21)
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
						instance.Y = binaryReader.ReadSingle();
					}
				}
				else
				{
					instance.X = binaryReader.ReadSingle();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0004B659 File Offset: 0x00049859
		public void Serialize(Stream stream)
		{
			Vector2.Serialize(stream, this);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0004B662 File Offset: 0x00049862
		public static void Serialize(Stream stream, Vector2 instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.X);
			stream.WriteByte(21);
			binaryWriter.Write(instance.Y);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004B691 File Offset: 0x00049891
		public uint GetSerializedSize()
		{
			return 0U + 4U + 4U + 2U;
		}
	}
}
