using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000C7 RID: 199
	public class GenericData : IProtoBuf
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x000328DF File Offset: 0x00030ADF
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x000328E7 File Offset: 0x00030AE7
		public uint TypeId { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000328F0 File Offset: 0x00030AF0
		// (set) Token: 0x06000DA8 RID: 3496 RVA: 0x000328F8 File Offset: 0x00030AF8
		public byte[] Data { get; set; }

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00032904 File Offset: 0x00030B04
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.TypeId.GetHashCode() ^ this.Data.GetHashCode();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00032938 File Offset: 0x00030B38
		public override bool Equals(object obj)
		{
			GenericData genericData = obj as GenericData;
			return genericData != null && this.TypeId.Equals(genericData.TypeId) && this.Data.Equals(genericData.Data);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0003297F File Offset: 0x00030B7F
		public void Deserialize(Stream stream)
		{
			GenericData.Deserialize(stream, this);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00032989 File Offset: 0x00030B89
		public static GenericData Deserialize(Stream stream, GenericData instance)
		{
			return GenericData.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00032994 File Offset: 0x00030B94
		public static GenericData DeserializeLengthDelimited(Stream stream)
		{
			GenericData genericData = new GenericData();
			GenericData.DeserializeLengthDelimited(stream, genericData);
			return genericData;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x000329B0 File Offset: 0x00030BB0
		public static GenericData DeserializeLengthDelimited(Stream stream, GenericData instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GenericData.Deserialize(stream, instance, num);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000329D8 File Offset: 0x00030BD8
		public static GenericData Deserialize(Stream stream, GenericData instance, long limit)
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
						instance.Data = ProtocolParser.ReadBytes(stream);
					}
				}
				else
				{
					instance.TypeId = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00032A6F File Offset: 0x00030C6F
		public void Serialize(Stream stream)
		{
			GenericData.Serialize(stream, this);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00032A78 File Offset: 0x00030C78
		public static void Serialize(Stream stream, GenericData instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.TypeId);
			if (instance.Data == null)
			{
				throw new ArgumentNullException("Data", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteBytes(stream, instance.Data);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00032AC4 File Offset: 0x00030CC4
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt32(this.TypeId) + (ProtocolParser.SizeOfUInt32(this.Data.Length) + (uint)this.Data.Length) + 2U;
		}
	}
}
