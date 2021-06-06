using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200050C RID: 1292
	public class GetAccountStateRequest : IProtoBuf
	{
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x00117DA6 File Offset: 0x00115FA6
		// (set) Token: 0x06005BFE RID: 23550 RVA: 0x00117DAE File Offset: 0x00115FAE
		public EntityId EntityId
		{
			get
			{
				return this._EntityId;
			}
			set
			{
				this._EntityId = value;
				this.HasEntityId = (value != null);
			}
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x00117DC1 File Offset: 0x00115FC1
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06005C00 RID: 23552 RVA: 0x00117DCA File Offset: 0x00115FCA
		// (set) Token: 0x06005C01 RID: 23553 RVA: 0x00117DD2 File Offset: 0x00115FD2
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x00117DE2 File Offset: 0x00115FE2
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06005C03 RID: 23555 RVA: 0x00117DEB File Offset: 0x00115FEB
		// (set) Token: 0x06005C04 RID: 23556 RVA: 0x00117DF3 File Offset: 0x00115FF3
		public uint Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
				this.HasRegion = true;
			}
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x00117E03 File Offset: 0x00116003
		public void SetRegion(uint val)
		{
			this.Region = val;
		}

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x06005C06 RID: 23558 RVA: 0x00117E0C File Offset: 0x0011600C
		// (set) Token: 0x06005C07 RID: 23559 RVA: 0x00117E14 File Offset: 0x00116014
		public AccountFieldOptions Options
		{
			get
			{
				return this._Options;
			}
			set
			{
				this._Options = value;
				this.HasOptions = (value != null);
			}
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x00117E27 File Offset: 0x00116027
		public void SetOptions(AccountFieldOptions val)
		{
			this.Options = val;
		}

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x06005C09 RID: 23561 RVA: 0x00117E30 File Offset: 0x00116030
		// (set) Token: 0x06005C0A RID: 23562 RVA: 0x00117E38 File Offset: 0x00116038
		public AccountFieldTags Tags
		{
			get
			{
				return this._Tags;
			}
			set
			{
				this._Tags = value;
				this.HasTags = (value != null);
			}
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x00117E4B File Offset: 0x0011604B
		public void SetTags(AccountFieldTags val)
		{
			this.Tags = val;
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x00117E54 File Offset: 0x00116054
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasRegion)
			{
				num ^= this.Region.GetHashCode();
			}
			if (this.HasOptions)
			{
				num ^= this.Options.GetHashCode();
			}
			if (this.HasTags)
			{
				num ^= this.Tags.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x00117EE4 File Offset: 0x001160E4
		public override bool Equals(object obj)
		{
			GetAccountStateRequest getAccountStateRequest = obj as GetAccountStateRequest;
			return getAccountStateRequest != null && this.HasEntityId == getAccountStateRequest.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(getAccountStateRequest.EntityId)) && this.HasProgram == getAccountStateRequest.HasProgram && (!this.HasProgram || this.Program.Equals(getAccountStateRequest.Program)) && this.HasRegion == getAccountStateRequest.HasRegion && (!this.HasRegion || this.Region.Equals(getAccountStateRequest.Region)) && this.HasOptions == getAccountStateRequest.HasOptions && (!this.HasOptions || this.Options.Equals(getAccountStateRequest.Options)) && this.HasTags == getAccountStateRequest.HasTags && (!this.HasTags || this.Tags.Equals(getAccountStateRequest.Tags));
		}

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06005C0E RID: 23566 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x00117FDB File Offset: 0x001161DB
		public static GetAccountStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetAccountStateRequest>(bs, 0, -1);
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x00117FE5 File Offset: 0x001161E5
		public void Deserialize(Stream stream)
		{
			GetAccountStateRequest.Deserialize(stream, this);
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x00117FEF File Offset: 0x001161EF
		public static GetAccountStateRequest Deserialize(Stream stream, GetAccountStateRequest instance)
		{
			return GetAccountStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x00117FFC File Offset: 0x001161FC
		public static GetAccountStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetAccountStateRequest getAccountStateRequest = new GetAccountStateRequest();
			GetAccountStateRequest.DeserializeLengthDelimited(stream, getAccountStateRequest);
			return getAccountStateRequest;
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x00118018 File Offset: 0x00116218
		public static GetAccountStateRequest DeserializeLengthDelimited(Stream stream, GetAccountStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetAccountStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x00118040 File Offset: 0x00116240
		public static GetAccountStateRequest Deserialize(Stream stream, GetAccountStateRequest instance, long limit)
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
				else
				{
					if (num <= 16)
					{
						if (num != 10)
						{
							if (num == 16)
							{
								instance.Program = ProtocolParser.ReadUInt32(stream);
								continue;
							}
						}
						else
						{
							if (instance.EntityId == null)
							{
								instance.EntityId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.EntityId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Region = ProtocolParser.ReadUInt32(stream);
							continue;
						}
						if (num != 82)
						{
							if (num == 90)
							{
								if (instance.Tags == null)
								{
									instance.Tags = AccountFieldTags.DeserializeLengthDelimited(stream);
									continue;
								}
								AccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
								continue;
							}
						}
						else
						{
							if (instance.Options == null)
							{
								instance.Options = AccountFieldOptions.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountFieldOptions.DeserializeLengthDelimited(stream, instance.Options);
							continue;
						}
					}
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

		// Token: 0x06005C15 RID: 23573 RVA: 0x0011817E File Offset: 0x0011637E
		public void Serialize(Stream stream)
		{
			GetAccountStateRequest.Serialize(stream, this);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x00118188 File Offset: 0x00116388
		public static void Serialize(Stream stream, GetAccountStateRequest instance)
		{
			if (instance.HasEntityId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Program);
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
			if (instance.HasOptions)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				AccountFieldOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x00118254 File Offset: 0x00116454
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasEntityId)
			{
				num += 1U;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Program);
			}
			if (this.HasRegion)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Region);
			}
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize2 = this.Options.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasTags)
			{
				num += 1U;
				uint serializedSize3 = this.Tags.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001C7D RID: 7293
		public bool HasEntityId;

		// Token: 0x04001C7E RID: 7294
		private EntityId _EntityId;

		// Token: 0x04001C7F RID: 7295
		public bool HasProgram;

		// Token: 0x04001C80 RID: 7296
		private uint _Program;

		// Token: 0x04001C81 RID: 7297
		public bool HasRegion;

		// Token: 0x04001C82 RID: 7298
		private uint _Region;

		// Token: 0x04001C83 RID: 7299
		public bool HasOptions;

		// Token: 0x04001C84 RID: 7300
		private AccountFieldOptions _Options;

		// Token: 0x04001C85 RID: 7301
		public bool HasTags;

		// Token: 0x04001C86 RID: 7302
		private AccountFieldTags _Tags;
	}
}
