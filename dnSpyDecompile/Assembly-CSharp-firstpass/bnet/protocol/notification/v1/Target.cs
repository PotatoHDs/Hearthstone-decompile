using System;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.notification.v1
{
	// Token: 0x02000349 RID: 841
	public class Target : IProtoBuf
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003455 RID: 13397 RVA: 0x000ADC3A File Offset: 0x000ABE3A
		// (set) Token: 0x06003456 RID: 13398 RVA: 0x000ADC42 File Offset: 0x000ABE42
		public Identity Identity
		{
			get
			{
				return this._Identity;
			}
			set
			{
				this._Identity = value;
				this.HasIdentity = (value != null);
			}
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000ADC55 File Offset: 0x000ABE55
		public void SetIdentity(Identity val)
		{
			this.Identity = val;
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x000ADC5E File Offset: 0x000ABE5E
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x000ADC66 File Offset: 0x000ABE66
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = (value != null);
			}
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000ADC79 File Offset: 0x000ABE79
		public void SetType(string val)
		{
			this.Type = val;
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000ADC84 File Offset: 0x000ABE84
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasIdentity)
			{
				num ^= this.Identity.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000ADCCC File Offset: 0x000ABECC
		public override bool Equals(object obj)
		{
			Target target = obj as Target;
			return target != null && this.HasIdentity == target.HasIdentity && (!this.HasIdentity || this.Identity.Equals(target.Identity)) && this.HasType == target.HasType && (!this.HasType || this.Type.Equals(target.Type));
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600345D RID: 13405 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000ADD3C File Offset: 0x000ABF3C
		public static Target ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Target>(bs, 0, -1);
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000ADD46 File Offset: 0x000ABF46
		public void Deserialize(Stream stream)
		{
			Target.Deserialize(stream, this);
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000ADD50 File Offset: 0x000ABF50
		public static Target Deserialize(Stream stream, Target instance)
		{
			return Target.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000ADD5C File Offset: 0x000ABF5C
		public static Target DeserializeLengthDelimited(Stream stream)
		{
			Target target = new Target();
			Target.DeserializeLengthDelimited(stream, target);
			return target;
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000ADD78 File Offset: 0x000ABF78
		public static Target DeserializeLengthDelimited(Stream stream, Target instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return Target.Deserialize(stream, instance, num);
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x000ADDA0 File Offset: 0x000ABFA0
		public static Target Deserialize(Stream stream, Target instance, long limit)
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
					else
					{
						instance.Type = ProtocolParser.ReadString(stream);
					}
				}
				else if (instance.Identity == null)
				{
					instance.Identity = Identity.DeserializeLengthDelimited(stream);
				}
				else
				{
					Identity.DeserializeLengthDelimited(stream, instance.Identity);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x000ADE52 File Offset: 0x000AC052
		public void Serialize(Stream stream)
		{
			Target.Serialize(stream, this);
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x000ADE5C File Offset: 0x000AC05C
		public static void Serialize(Stream stream, Target instance)
		{
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.HasType)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Type));
			}
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x000ADEBC File Offset: 0x000AC0BC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasIdentity)
			{
				num += 1U;
				uint serializedSize = this.Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Type);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num;
		}

		// Token: 0x04001424 RID: 5156
		public bool HasIdentity;

		// Token: 0x04001425 RID: 5157
		private Identity _Identity;

		// Token: 0x04001426 RID: 5158
		public bool HasType;

		// Token: 0x04001427 RID: 5159
		private string _Type;
	}
}
