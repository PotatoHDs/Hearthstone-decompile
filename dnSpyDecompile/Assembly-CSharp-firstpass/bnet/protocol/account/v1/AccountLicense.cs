using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000522 RID: 1314
	public class AccountLicense : IProtoBuf
	{
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06005DCA RID: 24010 RVA: 0x0011C77E File Offset: 0x0011A97E
		// (set) Token: 0x06005DCB RID: 24011 RVA: 0x0011C786 File Offset: 0x0011A986
		public uint Id { get; set; }

		// Token: 0x06005DCC RID: 24012 RVA: 0x0011C78F File Offset: 0x0011A98F
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06005DCD RID: 24013 RVA: 0x0011C798 File Offset: 0x0011A998
		// (set) Token: 0x06005DCE RID: 24014 RVA: 0x0011C7A0 File Offset: 0x0011A9A0
		public ulong Expires
		{
			get
			{
				return this._Expires;
			}
			set
			{
				this._Expires = value;
				this.HasExpires = true;
			}
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x0011C7B0 File Offset: 0x0011A9B0
		public void SetExpires(ulong val)
		{
			this.Expires = val;
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x0011C7BC File Offset: 0x0011A9BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Id.GetHashCode();
			if (this.HasExpires)
			{
				num ^= this.Expires.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x0011C800 File Offset: 0x0011AA00
		public override bool Equals(object obj)
		{
			AccountLicense accountLicense = obj as AccountLicense;
			return accountLicense != null && this.Id.Equals(accountLicense.Id) && this.HasExpires == accountLicense.HasExpires && (!this.HasExpires || this.Expires.Equals(accountLicense.Expires));
		}

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x0011C860 File Offset: 0x0011AA60
		public static AccountLicense ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AccountLicense>(bs, 0, -1);
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x0011C86A File Offset: 0x0011AA6A
		public void Deserialize(Stream stream)
		{
			AccountLicense.Deserialize(stream, this);
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x0011C874 File Offset: 0x0011AA74
		public static AccountLicense Deserialize(Stream stream, AccountLicense instance)
		{
			return AccountLicense.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x0011C880 File Offset: 0x0011AA80
		public static AccountLicense DeserializeLengthDelimited(Stream stream)
		{
			AccountLicense accountLicense = new AccountLicense();
			AccountLicense.DeserializeLengthDelimited(stream, accountLicense);
			return accountLicense;
		}

		// Token: 0x06005DD7 RID: 24023 RVA: 0x0011C89C File Offset: 0x0011AA9C
		public static AccountLicense DeserializeLengthDelimited(Stream stream, AccountLicense instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AccountLicense.Deserialize(stream, instance, num);
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x0011C8C4 File Offset: 0x0011AAC4
		public static AccountLicense Deserialize(Stream stream, AccountLicense instance, long limit)
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
						instance.Expires = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Id = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x0011C95B File Offset: 0x0011AB5B
		public void Serialize(Stream stream)
		{
			AccountLicense.Serialize(stream, this);
		}

		// Token: 0x06005DDA RID: 24026 RVA: 0x0011C964 File Offset: 0x0011AB64
		public static void Serialize(Stream stream, AccountLicense instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Id);
			if (instance.HasExpires)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Expires);
			}
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x0011C998 File Offset: 0x0011AB98
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt32(this.Id);
			if (this.HasExpires)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.Expires);
			}
			return num + 1U;
		}

		// Token: 0x04001CE3 RID: 7395
		public bool HasExpires;

		// Token: 0x04001CE4 RID: 7396
		private ulong _Expires;
	}
}
