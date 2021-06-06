using System;
using System.IO;

namespace SpectatorProto
{
	// Token: 0x02000030 RID: 48
	public class SecretJoinInfo : IProtoBuf
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000AE17 File Offset: 0x00009017
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000AE1F File Offset: 0x0000901F
		public SecretSource Source { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000AE28 File Offset: 0x00009028
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000AE30 File Offset: 0x00009030
		public long SpecificSourceIdentity
		{
			get
			{
				return this._SpecificSourceIdentity;
			}
			set
			{
				this._SpecificSourceIdentity = value;
				this.HasSpecificSourceIdentity = true;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000AE40 File Offset: 0x00009040
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000AE48 File Offset: 0x00009048
		public byte[] EncryptedMessage { get; set; }

		// Token: 0x06000288 RID: 648 RVA: 0x0000AE54 File Offset: 0x00009054
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Source.GetHashCode();
			if (this.HasSpecificSourceIdentity)
			{
				num ^= this.SpecificSourceIdentity.GetHashCode();
			}
			return num ^ this.EncryptedMessage.GetHashCode();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000AEAC File Offset: 0x000090AC
		public override bool Equals(object obj)
		{
			SecretJoinInfo secretJoinInfo = obj as SecretJoinInfo;
			return secretJoinInfo != null && this.Source.Equals(secretJoinInfo.Source) && this.HasSpecificSourceIdentity == secretJoinInfo.HasSpecificSourceIdentity && (!this.HasSpecificSourceIdentity || this.SpecificSourceIdentity.Equals(secretJoinInfo.SpecificSourceIdentity)) && this.EncryptedMessage.Equals(secretJoinInfo.EncryptedMessage);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000AF2C File Offset: 0x0000912C
		public void Deserialize(Stream stream)
		{
			SecretJoinInfo.Deserialize(stream, this);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000AF36 File Offset: 0x00009136
		public static SecretJoinInfo Deserialize(Stream stream, SecretJoinInfo instance)
		{
			return SecretJoinInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000AF44 File Offset: 0x00009144
		public static SecretJoinInfo DeserializeLengthDelimited(Stream stream)
		{
			SecretJoinInfo secretJoinInfo = new SecretJoinInfo();
			SecretJoinInfo.DeserializeLengthDelimited(stream, secretJoinInfo);
			return secretJoinInfo;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000AF60 File Offset: 0x00009160
		public static SecretJoinInfo DeserializeLengthDelimited(Stream stream, SecretJoinInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SecretJoinInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000AF88 File Offset: 0x00009188
		public static SecretJoinInfo Deserialize(Stream stream, SecretJoinInfo instance, long limit)
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
						if (num != 26)
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
							instance.EncryptedMessage = ProtocolParser.ReadBytes(stream);
						}
					}
					else
					{
						instance.SpecificSourceIdentity = (long)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Source = (SecretSource)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000B036 File Offset: 0x00009236
		public void Serialize(Stream stream)
		{
			SecretJoinInfo.Serialize(stream, this);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000B040 File Offset: 0x00009240
		public static void Serialize(Stream stream, SecretJoinInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Source));
			if (instance.HasSpecificSourceIdentity)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.SpecificSourceIdentity);
			}
			if (instance.EncryptedMessage == null)
			{
				throw new ArgumentNullException("EncryptedMessage", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteBytes(stream, instance.EncryptedMessage);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B0AC File Offset: 0x000092AC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Source));
			if (this.HasSpecificSourceIdentity)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)this.SpecificSourceIdentity);
			}
			num += ProtocolParser.SizeOfUInt32(this.EncryptedMessage.Length) + (uint)this.EncryptedMessage.Length;
			return num + 2U;
		}

		// Token: 0x040000C3 RID: 195
		public bool HasSpecificSourceIdentity;

		// Token: 0x040000C4 RID: 196
		private long _SpecificSourceIdentity;
	}
}
