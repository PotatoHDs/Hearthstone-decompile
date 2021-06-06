using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200052C RID: 1324
	public class SubscriberReference : IProtoBuf
	{
		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06005ECA RID: 24266 RVA: 0x0011F100 File Offset: 0x0011D300
		// (set) Token: 0x06005ECB RID: 24267 RVA: 0x0011F108 File Offset: 0x0011D308
		public ulong ObjectId
		{
			get
			{
				return this._ObjectId;
			}
			set
			{
				this._ObjectId = value;
				this.HasObjectId = true;
			}
		}

		// Token: 0x06005ECC RID: 24268 RVA: 0x0011F118 File Offset: 0x0011D318
		public void SetObjectId(ulong val)
		{
			this.ObjectId = val;
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x0011F121 File Offset: 0x0011D321
		// (set) Token: 0x06005ECE RID: 24270 RVA: 0x0011F129 File Offset: 0x0011D329
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

		// Token: 0x06005ECF RID: 24271 RVA: 0x0011F13C File Offset: 0x0011D33C
		public void SetEntityId(EntityId val)
		{
			this.EntityId = val;
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06005ED0 RID: 24272 RVA: 0x0011F145 File Offset: 0x0011D345
		// (set) Token: 0x06005ED1 RID: 24273 RVA: 0x0011F14D File Offset: 0x0011D34D
		public AccountFieldOptions AccountOptions
		{
			get
			{
				return this._AccountOptions;
			}
			set
			{
				this._AccountOptions = value;
				this.HasAccountOptions = (value != null);
			}
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x0011F160 File Offset: 0x0011D360
		public void SetAccountOptions(AccountFieldOptions val)
		{
			this.AccountOptions = val;
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x0011F169 File Offset: 0x0011D369
		// (set) Token: 0x06005ED4 RID: 24276 RVA: 0x0011F171 File Offset: 0x0011D371
		public AccountFieldTags AccountTags
		{
			get
			{
				return this._AccountTags;
			}
			set
			{
				this._AccountTags = value;
				this.HasAccountTags = (value != null);
			}
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x0011F184 File Offset: 0x0011D384
		public void SetAccountTags(AccountFieldTags val)
		{
			this.AccountTags = val;
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06005ED6 RID: 24278 RVA: 0x0011F18D File Offset: 0x0011D38D
		// (set) Token: 0x06005ED7 RID: 24279 RVA: 0x0011F195 File Offset: 0x0011D395
		public GameAccountFieldOptions GameAccountOptions
		{
			get
			{
				return this._GameAccountOptions;
			}
			set
			{
				this._GameAccountOptions = value;
				this.HasGameAccountOptions = (value != null);
			}
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x0011F1A8 File Offset: 0x0011D3A8
		public void SetGameAccountOptions(GameAccountFieldOptions val)
		{
			this.GameAccountOptions = val;
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x06005ED9 RID: 24281 RVA: 0x0011F1B1 File Offset: 0x0011D3B1
		// (set) Token: 0x06005EDA RID: 24282 RVA: 0x0011F1B9 File Offset: 0x0011D3B9
		public GameAccountFieldTags GameAccountTags
		{
			get
			{
				return this._GameAccountTags;
			}
			set
			{
				this._GameAccountTags = value;
				this.HasGameAccountTags = (value != null);
			}
		}

		// Token: 0x06005EDB RID: 24283 RVA: 0x0011F1CC File Offset: 0x0011D3CC
		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			this.GameAccountTags = val;
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x0011F1D5 File Offset: 0x0011D3D5
		// (set) Token: 0x06005EDD RID: 24285 RVA: 0x0011F1DD File Offset: 0x0011D3DD
		public ulong SubscriberId
		{
			get
			{
				return this._SubscriberId;
			}
			set
			{
				this._SubscriberId = value;
				this.HasSubscriberId = true;
			}
		}

		// Token: 0x06005EDE RID: 24286 RVA: 0x0011F1ED File Offset: 0x0011D3ED
		public void SetSubscriberId(ulong val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x0011F1F8 File Offset: 0x0011D3F8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasObjectId)
			{
				num ^= this.ObjectId.GetHashCode();
			}
			if (this.HasEntityId)
			{
				num ^= this.EntityId.GetHashCode();
			}
			if (this.HasAccountOptions)
			{
				num ^= this.AccountOptions.GetHashCode();
			}
			if (this.HasAccountTags)
			{
				num ^= this.AccountTags.GetHashCode();
			}
			if (this.HasGameAccountOptions)
			{
				num ^= this.GameAccountOptions.GetHashCode();
			}
			if (this.HasGameAccountTags)
			{
				num ^= this.GameAccountTags.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x0011F2B4 File Offset: 0x0011D4B4
		public override bool Equals(object obj)
		{
			SubscriberReference subscriberReference = obj as SubscriberReference;
			return subscriberReference != null && this.HasObjectId == subscriberReference.HasObjectId && (!this.HasObjectId || this.ObjectId.Equals(subscriberReference.ObjectId)) && this.HasEntityId == subscriberReference.HasEntityId && (!this.HasEntityId || this.EntityId.Equals(subscriberReference.EntityId)) && this.HasAccountOptions == subscriberReference.HasAccountOptions && (!this.HasAccountOptions || this.AccountOptions.Equals(subscriberReference.AccountOptions)) && this.HasAccountTags == subscriberReference.HasAccountTags && (!this.HasAccountTags || this.AccountTags.Equals(subscriberReference.AccountTags)) && this.HasGameAccountOptions == subscriberReference.HasGameAccountOptions && (!this.HasGameAccountOptions || this.GameAccountOptions.Equals(subscriberReference.GameAccountOptions)) && this.HasGameAccountTags == subscriberReference.HasGameAccountTags && (!this.HasGameAccountTags || this.GameAccountTags.Equals(subscriberReference.GameAccountTags)) && this.HasSubscriberId == subscriberReference.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(subscriberReference.SubscriberId));
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06005EE1 RID: 24289 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x0011F401 File Offset: 0x0011D601
		public static SubscriberReference ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SubscriberReference>(bs, 0, -1);
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x0011F40B File Offset: 0x0011D60B
		public void Deserialize(Stream stream)
		{
			SubscriberReference.Deserialize(stream, this);
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x0011F415 File Offset: 0x0011D615
		public static SubscriberReference Deserialize(Stream stream, SubscriberReference instance)
		{
			return SubscriberReference.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x0011F420 File Offset: 0x0011D620
		public static SubscriberReference DeserializeLengthDelimited(Stream stream)
		{
			SubscriberReference subscriberReference = new SubscriberReference();
			SubscriberReference.DeserializeLengthDelimited(stream, subscriberReference);
			return subscriberReference;
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x0011F43C File Offset: 0x0011D63C
		public static SubscriberReference DeserializeLengthDelimited(Stream stream, SubscriberReference instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SubscriberReference.Deserialize(stream, instance, num);
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x0011F464 File Offset: 0x0011D664
		public static SubscriberReference Deserialize(Stream stream, SubscriberReference instance, long limit)
		{
			instance.ObjectId = 0UL;
			instance.SubscriberId = 0UL;
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
					if (num <= 26)
					{
						if (num == 8)
						{
							instance.ObjectId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num != 18)
						{
							if (num == 26)
							{
								if (instance.AccountOptions == null)
								{
									instance.AccountOptions = AccountFieldOptions.DeserializeLengthDelimited(stream);
									continue;
								}
								AccountFieldOptions.DeserializeLengthDelimited(stream, instance.AccountOptions);
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
					else if (num <= 42)
					{
						if (num != 34)
						{
							if (num == 42)
							{
								if (instance.GameAccountOptions == null)
								{
									instance.GameAccountOptions = GameAccountFieldOptions.DeserializeLengthDelimited(stream);
									continue;
								}
								GameAccountFieldOptions.DeserializeLengthDelimited(stream, instance.GameAccountOptions);
								continue;
							}
						}
						else
						{
							if (instance.AccountTags == null)
							{
								instance.AccountTags = AccountFieldTags.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountFieldTags.DeserializeLengthDelimited(stream, instance.AccountTags);
							continue;
						}
					}
					else if (num != 50)
					{
						if (num == 56)
						{
							instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (instance.GameAccountTags == null)
						{
							instance.GameAccountTags = GameAccountFieldTags.DeserializeLengthDelimited(stream);
							continue;
						}
						GameAccountFieldTags.DeserializeLengthDelimited(stream, instance.GameAccountTags);
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

		// Token: 0x06005EE8 RID: 24296 RVA: 0x0011F62A File Offset: 0x0011D82A
		public void Serialize(Stream stream)
		{
			SubscriberReference.Serialize(stream, this);
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x0011F634 File Offset: 0x0011D834
		public static void Serialize(Stream stream, SubscriberReference instance)
		{
			if (instance.HasObjectId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.ObjectId);
			}
			if (instance.HasEntityId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.EntityId.GetSerializedSize());
				EntityId.Serialize(stream, instance.EntityId);
			}
			if (instance.HasAccountOptions)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountOptions.GetSerializedSize());
				AccountFieldOptions.Serialize(stream, instance.AccountOptions);
			}
			if (instance.HasAccountTags)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.AccountTags.GetSerializedSize());
				AccountFieldTags.Serialize(stream, instance.AccountTags);
			}
			if (instance.HasGameAccountOptions)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountOptions.GetSerializedSize());
				GameAccountFieldOptions.Serialize(stream, instance.GameAccountOptions);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x0011F75C File Offset: 0x0011D95C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasObjectId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.ObjectId);
			}
			if (this.HasEntityId)
			{
				num += 1U;
				uint serializedSize = this.EntityId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAccountOptions)
			{
				num += 1U;
				uint serializedSize2 = this.AccountOptions.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAccountTags)
			{
				num += 1U;
				uint serializedSize3 = this.AccountTags.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasGameAccountOptions)
			{
				num += 1U;
				uint serializedSize4 = this.GameAccountOptions.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasGameAccountTags)
			{
				num += 1U;
				uint serializedSize5 = this.GameAccountTags.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.SubscriberId);
			}
			return num;
		}

		// Token: 0x04001D27 RID: 7463
		public bool HasObjectId;

		// Token: 0x04001D28 RID: 7464
		private ulong _ObjectId;

		// Token: 0x04001D29 RID: 7465
		public bool HasEntityId;

		// Token: 0x04001D2A RID: 7466
		private EntityId _EntityId;

		// Token: 0x04001D2B RID: 7467
		public bool HasAccountOptions;

		// Token: 0x04001D2C RID: 7468
		private AccountFieldOptions _AccountOptions;

		// Token: 0x04001D2D RID: 7469
		public bool HasAccountTags;

		// Token: 0x04001D2E RID: 7470
		private AccountFieldTags _AccountTags;

		// Token: 0x04001D2F RID: 7471
		public bool HasGameAccountOptions;

		// Token: 0x04001D30 RID: 7472
		private GameAccountFieldOptions _GameAccountOptions;

		// Token: 0x04001D31 RID: 7473
		public bool HasGameAccountTags;

		// Token: 0x04001D32 RID: 7474
		private GameAccountFieldTags _GameAccountTags;

		// Token: 0x04001D33 RID: 7475
		public bool HasSubscriberId;

		// Token: 0x04001D34 RID: 7476
		private ulong _SubscriberId;
	}
}
