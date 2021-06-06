using System;
using System.IO;

namespace bnet.protocol.games.v2
{
	// Token: 0x0200036A RID: 874
	public class FactoryId : IProtoBuf
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000B6113 File Offset: 0x000B4313
		// (set) Token: 0x06003761 RID: 14177 RVA: 0x000B611B File Offset: 0x000B431B
		public ulong Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000B612B File Offset: 0x000B432B
		public void SetId(ulong val)
		{
			this.Id = val;
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000B6134 File Offset: 0x000B4334
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003764 RID: 14180 RVA: 0x000B6168 File Offset: 0x000B4368
		public override bool Equals(object obj)
		{
			FactoryId factoryId = obj as FactoryId;
			return factoryId != null && this.HasId == factoryId.HasId && (!this.HasId || this.Id.Equals(factoryId.Id));
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x000B61B0 File Offset: 0x000B43B0
		public static FactoryId ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FactoryId>(bs, 0, -1);
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000B61BA File Offset: 0x000B43BA
		public void Deserialize(Stream stream)
		{
			FactoryId.Deserialize(stream, this);
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000B61C4 File Offset: 0x000B43C4
		public static FactoryId Deserialize(Stream stream, FactoryId instance)
		{
			return FactoryId.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x000B61D0 File Offset: 0x000B43D0
		public static FactoryId DeserializeLengthDelimited(Stream stream)
		{
			FactoryId factoryId = new FactoryId();
			FactoryId.DeserializeLengthDelimited(stream, factoryId);
			return factoryId;
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000B61EC File Offset: 0x000B43EC
		public static FactoryId DeserializeLengthDelimited(Stream stream, FactoryId instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FactoryId.Deserialize(stream, instance, num);
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x000B6214 File Offset: 0x000B4414
		public static FactoryId Deserialize(Stream stream, FactoryId instance, long limit)
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
				else if (num == 9)
				{
					instance.Id = binaryReader.ReadUInt64();
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

		// Token: 0x0600376C RID: 14188 RVA: 0x000B629B File Offset: 0x000B449B
		public void Serialize(Stream stream)
		{
			FactoryId.Serialize(stream, this);
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x000B62A4 File Offset: 0x000B44A4
		public static void Serialize(Stream stream, FactoryId instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasId)
			{
				stream.WriteByte(9);
				binaryWriter.Write(instance.Id);
			}
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000B62D4 File Offset: 0x000B44D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += 8U;
			}
			return num;
		}

		// Token: 0x040014B9 RID: 5305
		public bool HasId;

		// Token: 0x040014BA RID: 5306
		private ulong _Id;
	}
}
