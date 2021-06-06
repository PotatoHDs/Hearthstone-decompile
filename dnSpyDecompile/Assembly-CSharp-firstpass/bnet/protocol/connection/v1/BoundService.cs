using System;
using System.IO;

namespace bnet.protocol.connection.v1
{
	// Token: 0x0200043F RID: 1087
	public class BoundService : IProtoBuf
	{
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x000E6275 File Offset: 0x000E4475
		// (set) Token: 0x060049B3 RID: 18867 RVA: 0x000E627D File Offset: 0x000E447D
		public uint Hash { get; set; }

		// Token: 0x060049B4 RID: 18868 RVA: 0x000E6286 File Offset: 0x000E4486
		public void SetHash(uint val)
		{
			this.Hash = val;
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060049B5 RID: 18869 RVA: 0x000E628F File Offset: 0x000E448F
		// (set) Token: 0x060049B6 RID: 18870 RVA: 0x000E6297 File Offset: 0x000E4497
		public uint Id { get; set; }

		// Token: 0x060049B7 RID: 18871 RVA: 0x000E62A0 File Offset: 0x000E44A0
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x000E62AC File Offset: 0x000E44AC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Hash.GetHashCode() ^ this.Id.GetHashCode();
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x000E62E4 File Offset: 0x000E44E4
		public override bool Equals(object obj)
		{
			BoundService boundService = obj as BoundService;
			return boundService != null && this.Hash.Equals(boundService.Hash) && this.Id.Equals(boundService.Id);
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x000E632E File Offset: 0x000E452E
		public static BoundService ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BoundService>(bs, 0, -1);
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x000E6338 File Offset: 0x000E4538
		public void Deserialize(Stream stream)
		{
			BoundService.Deserialize(stream, this);
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x000E6342 File Offset: 0x000E4542
		public static BoundService Deserialize(Stream stream, BoundService instance)
		{
			return BoundService.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x000E6350 File Offset: 0x000E4550
		public static BoundService DeserializeLengthDelimited(Stream stream)
		{
			BoundService boundService = new BoundService();
			BoundService.DeserializeLengthDelimited(stream, boundService);
			return boundService;
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x000E636C File Offset: 0x000E456C
		public static BoundService DeserializeLengthDelimited(Stream stream, BoundService instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return BoundService.Deserialize(stream, instance, num);
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x000E6394 File Offset: 0x000E4594
		public static BoundService Deserialize(Stream stream, BoundService instance, long limit)
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
				else if (num != 13)
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
						instance.Id = ProtocolParser.ReadUInt32(stream);
					}
				}
				else
				{
					instance.Hash = binaryReader.ReadUInt32();
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x000E6433 File Offset: 0x000E4633
		public void Serialize(Stream stream)
		{
			BoundService.Serialize(stream, this);
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x000E643C File Offset: 0x000E463C
		public static void Serialize(Stream stream, BoundService instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(13);
			binaryWriter.Write(instance.Hash);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.Id);
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x000E646B File Offset: 0x000E466B
		public uint GetSerializedSize()
		{
			return 0U + 4U + ProtocolParser.SizeOfUInt32(this.Id) + 2U;
		}
	}
}
