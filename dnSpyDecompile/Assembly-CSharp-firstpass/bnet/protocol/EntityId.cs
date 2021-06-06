using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002A4 RID: 676
	public class EntityId : IProtoBuf
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x00089FC3 File Offset: 0x000881C3
		// (set) Token: 0x060026C4 RID: 9924 RVA: 0x00089FCB File Offset: 0x000881CB
		public ulong High { get; set; }

		// Token: 0x060026C5 RID: 9925 RVA: 0x00089FD4 File Offset: 0x000881D4
		public void SetHigh(ulong val)
		{
			this.High = val;
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x00089FDD File Offset: 0x000881DD
		// (set) Token: 0x060026C7 RID: 9927 RVA: 0x00089FE5 File Offset: 0x000881E5
		public ulong Low { get; set; }

		// Token: 0x060026C8 RID: 9928 RVA: 0x00089FEE File Offset: 0x000881EE
		public void SetLow(ulong val)
		{
			this.Low = val;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x00089FF8 File Offset: 0x000881F8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.High.GetHashCode() ^ this.Low.GetHashCode();
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0008A030 File Offset: 0x00088230
		public override bool Equals(object obj)
		{
			EntityId entityId = obj as EntityId;
			return entityId != null && this.High.Equals(entityId.High) && this.Low.Equals(entityId.Low);
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0008A07A File Offset: 0x0008827A
		public static EntityId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<EntityId>(bs, 0, -1);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0008A084 File Offset: 0x00088284
		public void Deserialize(Stream stream)
		{
			EntityId.Deserialize(stream, this);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0008A08E File Offset: 0x0008828E
		public static EntityId Deserialize(Stream stream, EntityId instance)
		{
			return EntityId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0008A09C File Offset: 0x0008829C
		public static EntityId DeserializeLengthDelimited(Stream stream)
		{
			EntityId entityId = new EntityId();
			EntityId.DeserializeLengthDelimited(stream, entityId);
			return entityId;
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0008A0B8 File Offset: 0x000882B8
		public static EntityId DeserializeLengthDelimited(Stream stream, EntityId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return EntityId.Deserialize(stream, instance, num);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0008A0E0 File Offset: 0x000882E0
		public static EntityId Deserialize(Stream stream, EntityId instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				else if (num != 9)
				{
					if (num != 17)
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
						instance.Low = binaryReader.ReadUInt64();
					}
				}
				else
				{
					instance.High = binaryReader.ReadUInt64();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0008A17F File Offset: 0x0008837F
		public void Serialize(Stream stream)
		{
			EntityId.Serialize(stream, this);
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0008A188 File Offset: 0x00088388
		public static void Serialize(Stream stream, EntityId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.High);
			stream.WriteByte(17);
			binaryWriter.Write(instance.Low);
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0008A1B7 File Offset: 0x000883B7
		public uint GetSerializedSize()
		{
			return 0U + 8U + 8U + 2U;
		}
	}
}
