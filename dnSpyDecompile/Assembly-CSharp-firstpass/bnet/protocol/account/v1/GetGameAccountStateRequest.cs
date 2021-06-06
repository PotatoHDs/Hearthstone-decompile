using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x02000510 RID: 1296
	public class GetGameAccountStateRequest : IProtoBuf
	{
		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06005C4C RID: 23628 RVA: 0x001189F0 File Offset: 0x00116BF0
		// (set) Token: 0x06005C4D RID: 23629 RVA: 0x001189F8 File Offset: 0x00116BF8
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

		// Token: 0x06005C4E RID: 23630 RVA: 0x00118A0B File Offset: 0x00116C0B
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06005C4F RID: 23631 RVA: 0x00118A14 File Offset: 0x00116C14
		// (set) Token: 0x06005C50 RID: 23632 RVA: 0x00118A1C File Offset: 0x00116C1C
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

		// Token: 0x06005C51 RID: 23633 RVA: 0x00118A2F File Offset: 0x00116C2F
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06005C52 RID: 23634 RVA: 0x00118A38 File Offset: 0x00116C38
		// (set) Token: 0x06005C53 RID: 23635 RVA: 0x00118A40 File Offset: 0x00116C40
		public GameAccountFieldOptions Options
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

		// Token: 0x06005C54 RID: 23636 RVA: 0x00118A53 File Offset: 0x00116C53
		public void SetOptions(GameAccountFieldOptions val)
		{
			this.Options = val;
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06005C55 RID: 23637 RVA: 0x00118A5C File Offset: 0x00116C5C
		// (set) Token: 0x06005C56 RID: 23638 RVA: 0x00118A64 File Offset: 0x00116C64
		public GameAccountFieldTags Tags
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

		// Token: 0x06005C57 RID: 23639 RVA: 0x00118A77 File Offset: 0x00116C77
		public void SetTags(GameAccountFieldTags val)
		{
			this.Tags = val;
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00118A80 File Offset: 0x00116C80
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

		// Token: 0x06005C59 RID: 23641 RVA: 0x00118AF4 File Offset: 0x00116CF4
		public override bool Equals(object obj)
		{
			GetGameAccountStateRequest getGameAccountStateRequest = obj as GetGameAccountStateRequest;
			return getGameAccountStateRequest != null && this.HasAccountId == getGameAccountStateRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(getGameAccountStateRequest.AccountId)) && this.HasGameAccountId == getGameAccountStateRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(getGameAccountStateRequest.GameAccountId)) && this.HasOptions == getGameAccountStateRequest.HasOptions && (!this.HasOptions || this.Options.Equals(getGameAccountStateRequest.Options)) && this.HasTags == getGameAccountStateRequest.HasTags && (!this.HasTags || this.Tags.Equals(getGameAccountStateRequest.Tags));
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06005C5A RID: 23642 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x00118BBA File Offset: 0x00116DBA
		public static GetGameAccountStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetGameAccountStateRequest>(bs, 0, -1);
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x00118BC4 File Offset: 0x00116DC4
		public void Deserialize(Stream stream)
		{
			GetGameAccountStateRequest.Deserialize(stream, this);
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x00118BCE File Offset: 0x00116DCE
		public static GetGameAccountStateRequest Deserialize(Stream stream, GetGameAccountStateRequest instance)
		{
			return GetGameAccountStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005C5E RID: 23646 RVA: 0x00118BDC File Offset: 0x00116DDC
		public static GetGameAccountStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetGameAccountStateRequest getGameAccountStateRequest = new GetGameAccountStateRequest();
			GetGameAccountStateRequest.DeserializeLengthDelimited(stream, getGameAccountStateRequest);
			return getGameAccountStateRequest;
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x00118BF8 File Offset: 0x00116DF8
		public static GetGameAccountStateRequest DeserializeLengthDelimited(Stream stream, GetGameAccountStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetGameAccountStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x00118C20 File Offset: 0x00116E20
		public static GetGameAccountStateRequest Deserialize(Stream stream, GetGameAccountStateRequest instance, long limit)
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
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.GameAccountId == null)
								{
									instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
									continue;
								}
								EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
								continue;
							}
						}
						else
						{
							if (instance.AccountId == null)
							{
								instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
								continue;
							}
							EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
							continue;
						}
					}
					else if (num != 82)
					{
						if (num == 90)
						{
							if (instance.Tags == null)
							{
								instance.Tags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.Tags);
							continue;
						}
					}
					else
					{
						if (instance.Options == null)
						{
							instance.Options = GameAccountFieldOptions.DeserializeLengthDelimited(stream);
							continue;
						}
						GameAccountFieldOptions.DeserializeLengthDelimited(stream, instance.Options);
						continue;
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

		// Token: 0x06005C61 RID: 23649 RVA: 0x00118D65 File Offset: 0x00116F65
		public void Serialize(Stream stream)
		{
			GetGameAccountStateRequest.Serialize(stream, this);
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x00118D70 File Offset: 0x00116F70
		public static void Serialize(Stream stream, GetGameAccountStateRequest instance)
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
			if (instance.HasOptions)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
				GameAccountFieldOptions.Serialize(stream, instance.Options);
			}
			if (instance.HasTags)
			{
				stream.WriteByte(90);
				ProtocolParser.WriteUInt32(stream, instance.Tags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.Tags);
			}
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x00118E34 File Offset: 0x00117034
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
			if (this.HasOptions)
			{
				num += 1U;
				uint serializedSize3 = this.Options.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasTags)
			{
				num += 1U;
				uint serializedSize4 = this.Tags.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x04001C8F RID: 7311
		public bool HasAccountId;

		// Token: 0x04001C90 RID: 7312
		private EntityId _AccountId;

		// Token: 0x04001C91 RID: 7313
		public bool HasGameAccountId;

		// Token: 0x04001C92 RID: 7314
		private EntityId _GameAccountId;

		// Token: 0x04001C93 RID: 7315
		public bool HasOptions;

		// Token: 0x04001C94 RID: 7316
		private GameAccountFieldOptions _Options;

		// Token: 0x04001C95 RID: 7317
		public bool HasTags;

		// Token: 0x04001C96 RID: 7318
		private GameAccountFieldTags _Tags;
	}
}
