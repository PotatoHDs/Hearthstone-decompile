using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x02000429 RID: 1065
	public class FriendNotification : IProtoBuf
	{
		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06004757 RID: 18263 RVA: 0x000DF24F File Offset: 0x000DD44F
		// (set) Token: 0x06004758 RID: 18264 RVA: 0x000DF257 File Offset: 0x000DD457
		public Friend Target { get; set; }

		// Token: 0x06004759 RID: 18265 RVA: 0x000DF260 File Offset: 0x000DD460
		public void SetTarget(Friend val)
		{
			this.Target = val;
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x000DF269 File Offset: 0x000DD469
		// (set) Token: 0x0600475B RID: 18267 RVA: 0x000DF271 File Offset: 0x000DD471
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

		// Token: 0x0600475C RID: 18268 RVA: 0x000DF284 File Offset: 0x000DD484
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x000DF28D File Offset: 0x000DD48D
		// (set) Token: 0x0600475E RID: 18270 RVA: 0x000DF295 File Offset: 0x000DD495
		public ObjectAddress Forward
		{
			get
			{
				return this._Forward;
			}
			set
			{
				this._Forward = value;
				this.HasForward = (value != null);
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x000DF2A8 File Offset: 0x000DD4A8
		public void SetForward(ObjectAddress val)
		{
			this.Forward = val;
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x000DF2B4 File Offset: 0x000DD4B4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Target.GetHashCode();
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasForward)
			{
				num ^= this.Forward.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x000DF308 File Offset: 0x000DD508
		public override bool Equals(object obj)
		{
			FriendNotification friendNotification = obj as FriendNotification;
			return friendNotification != null && this.Target.Equals(friendNotification.Target) && this.HasAccountId == friendNotification.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(friendNotification.AccountId)) && this.HasForward == friendNotification.HasForward && (!this.HasForward || this.Forward.Equals(friendNotification.Forward));
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000DF38D File Offset: 0x000DD58D
		public static FriendNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FriendNotification>(bs, 0, -1);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x000DF397 File Offset: 0x000DD597
		public void Deserialize(Stream stream)
		{
			FriendNotification.Deserialize(stream, this);
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x000DF3A1 File Offset: 0x000DD5A1
		public static FriendNotification Deserialize(Stream stream, FriendNotification instance)
		{
			return FriendNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x000DF3AC File Offset: 0x000DD5AC
		public static FriendNotification DeserializeLengthDelimited(Stream stream)
		{
			FriendNotification friendNotification = new FriendNotification();
			FriendNotification.DeserializeLengthDelimited(stream, friendNotification);
			return friendNotification;
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x000DF3C8 File Offset: 0x000DD5C8
		public static FriendNotification DeserializeLengthDelimited(Stream stream, FriendNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FriendNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000DF3F0 File Offset: 0x000DD5F0
		public static FriendNotification Deserialize(Stream stream, FriendNotification instance, long limit)
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
					if (num != 42)
					{
						if (num != 50)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Forward == null)
						{
							instance.Forward = ObjectAddress.DeserializeLengthDelimited(stream);
						}
						else
						{
							ObjectAddress.DeserializeLengthDelimited(stream, instance.Forward);
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
				else if (instance.Target == null)
				{
					instance.Target = Friend.DeserializeLengthDelimited(stream);
				}
				else
				{
					Friend.DeserializeLengthDelimited(stream, instance.Target);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000DF4F2 File Offset: 0x000DD6F2
		public void Serialize(Stream stream)
		{
			FriendNotification.Serialize(stream, this);
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x000DF4FC File Offset: 0x000DD6FC
		public static void Serialize(Stream stream, FriendNotification instance)
		{
			if (instance.Target == null)
			{
				throw new ArgumentNullException("Target", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Target.GetSerializedSize());
			Friend.Serialize(stream, instance.Target);
			if (instance.HasAccountId)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasForward)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.Forward.GetSerializedSize());
				ObjectAddress.Serialize(stream, instance.Forward);
			}
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000DF5A0 File Offset: 0x000DD7A0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Target.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize2 = this.AccountId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasForward)
			{
				num += 1U;
				uint serializedSize3 = this.Forward.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num + 1U;
		}

		// Token: 0x040017C1 RID: 6081
		public bool HasAccountId;

		// Token: 0x040017C2 RID: 6082
		private EntityId _AccountId;

		// Token: 0x040017C3 RID: 6083
		public bool HasForward;

		// Token: 0x040017C4 RID: 6084
		private ObjectAddress _Forward;
	}
}
