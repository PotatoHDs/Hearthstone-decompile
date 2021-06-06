using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000AF RID: 175
	public class MassDisenchantResponse : IProtoBuf
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0002E08C File Offset: 0x0002C28C
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0002E094 File Offset: 0x0002C294
		public int Amount { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0002E09D File Offset: 0x0002C29D
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0002E0A5 File Offset: 0x0002C2A5
		public long CollectionVersion
		{
			get
			{
				return this._CollectionVersion;
			}
			set
			{
				this._CollectionVersion = value;
				this.HasCollectionVersion = true;
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002E0B8 File Offset: 0x0002C2B8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Amount.GetHashCode();
			if (this.HasCollectionVersion)
			{
				num ^= this.CollectionVersion.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0002E0FC File Offset: 0x0002C2FC
		public override bool Equals(object obj)
		{
			MassDisenchantResponse massDisenchantResponse = obj as MassDisenchantResponse;
			return massDisenchantResponse != null && this.Amount.Equals(massDisenchantResponse.Amount) && this.HasCollectionVersion == massDisenchantResponse.HasCollectionVersion && (!this.HasCollectionVersion || this.CollectionVersion.Equals(massDisenchantResponse.CollectionVersion));
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002E15C File Offset: 0x0002C35C
		public void Deserialize(Stream stream)
		{
			MassDisenchantResponse.Deserialize(stream, this);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002E166 File Offset: 0x0002C366
		public static MassDisenchantResponse Deserialize(Stream stream, MassDisenchantResponse instance)
		{
			return MassDisenchantResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002E174 File Offset: 0x0002C374
		public static MassDisenchantResponse DeserializeLengthDelimited(Stream stream)
		{
			MassDisenchantResponse massDisenchantResponse = new MassDisenchantResponse();
			MassDisenchantResponse.DeserializeLengthDelimited(stream, massDisenchantResponse);
			return massDisenchantResponse;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002E190 File Offset: 0x0002C390
		public static MassDisenchantResponse DeserializeLengthDelimited(Stream stream, MassDisenchantResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return MassDisenchantResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0002E1B8 File Offset: 0x0002C3B8
		public static MassDisenchantResponse Deserialize(Stream stream, MassDisenchantResponse instance, long limit)
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
						instance.CollectionVersion = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Amount = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002E250 File Offset: 0x0002C450
		public void Serialize(Stream stream)
		{
			MassDisenchantResponse.Serialize(stream, this);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002E259 File Offset: 0x0002C459
		public static void Serialize(Stream stream, MassDisenchantResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Amount));
			if (instance.HasCollectionVersion)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CollectionVersion);
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002E28C File Offset: 0x0002C48C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Amount));
			if (this.HasCollectionVersion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.CollectionVersion);
			}
			return num + 1U;
		}

		// Token: 0x0400044F RID: 1103
		public bool HasCollectionVersion;

		// Token: 0x04000450 RID: 1104
		private long _CollectionVersion;

		// Token: 0x020005B8 RID: 1464
		public enum PacketID
		{
			// Token: 0x04001F78 RID: 8056
			ID = 269
		}
	}
}
