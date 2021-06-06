using System;
using System.IO;

namespace bnet.protocol
{
	// Token: 0x020002A5 RID: 677
	public class Identity : IProtoBuf
	{
		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x0008A1C0 File Offset: 0x000883C0
		// (set) Token: 0x060026D7 RID: 9943 RVA: 0x0008A1C8 File Offset: 0x000883C8
		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0008A1DB File Offset: 0x000883DB
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x0008A1E4 File Offset: 0x000883E4
		// (set) Token: 0x060026DA RID: 9946 RVA: 0x0008A1EC File Offset: 0x000883EC
		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x0008A1FF File Offset: 0x000883FF
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x0008A208 File Offset: 0x00088408
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			return num;
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0008A250 File Offset: 0x00088450
		public override bool Equals(object obj)
		{
			Identity identity = obj as Identity;
			return identity != null && this.HasAccountId == identity.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(identity.AccountId)) && this.HasGameAccountId == identity.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(identity.GameAccountId));
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0008A2C0 File Offset: 0x000884C0
		public static Identity ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Identity>(bs, 0, -1);
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0008A2CA File Offset: 0x000884CA
		public void Deserialize(Stream stream)
		{
			Identity.Deserialize(stream, this);
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x0008A2D4 File Offset: 0x000884D4
		public static Identity Deserialize(Stream stream, Identity instance)
		{
			return Identity.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0008A2E0 File Offset: 0x000884E0
		public static Identity DeserializeLengthDelimited(Stream stream)
		{
			Identity identity = new Identity();
			Identity.DeserializeLengthDelimited(stream, identity);
			return identity;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0008A2FC File Offset: 0x000884FC
		public static Identity DeserializeLengthDelimited(Stream stream, Identity instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Identity.Deserialize(stream, instance, num);
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x0008A324 File Offset: 0x00088524
		public static Identity Deserialize(Stream stream, Identity instance, long limit)
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
				else if (num != 10)
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
					else if (instance.GameAccountId == null)
					{
						instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
				}
				else if (instance.AccountId == null)
				{
					instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
				}
				else
				{
					EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x0008A3F6 File Offset: 0x000885F6
		public void Serialize(Stream stream)
		{
			Identity.Serialize(stream, this);
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x0008A400 File Offset: 0x00088600
		public static void Serialize(Stream stream, Identity instance)
		{
			if (instance.HasAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x0008A468 File Offset: 0x00088668
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize = this.AccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasGameAccountId)
			{
				num += 1U;
				uint serializedSize2 = this.GameAccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x0400110A RID: 4362
		public bool HasAccountId;

		// Token: 0x0400110B RID: 4363
		private EntityId _AccountId;

		// Token: 0x0400110C RID: 4364
		public bool HasGameAccountId;

		// Token: 0x0400110D RID: 4365
		private EntityId _GameAccountId;
	}
}
