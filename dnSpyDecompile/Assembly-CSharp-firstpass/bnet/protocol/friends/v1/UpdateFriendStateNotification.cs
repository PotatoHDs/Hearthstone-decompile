using System;
using System.IO;

namespace bnet.protocol.friends.v1
{
	// Token: 0x0200042A RID: 1066
	public class UpdateFriendStateNotification : IProtoBuf
	{
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x0600476D RID: 18285 RVA: 0x000DF611 File Offset: 0x000DD811
		// (set) Token: 0x0600476E RID: 18286 RVA: 0x000DF619 File Offset: 0x000DD819
		public Friend ChangedFriend { get; set; }

		// Token: 0x0600476F RID: 18287 RVA: 0x000DF622 File Offset: 0x000DD822
		public void SetChangedFriend(Friend val)
		{
			this.ChangedFriend = val;
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x000DF62B File Offset: 0x000DD82B
		// (set) Token: 0x06004771 RID: 18289 RVA: 0x000DF633 File Offset: 0x000DD833
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

		// Token: 0x06004772 RID: 18290 RVA: 0x000DF646 File Offset: 0x000DD846
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x000DF64F File Offset: 0x000DD84F
		// (set) Token: 0x06004774 RID: 18292 RVA: 0x000DF657 File Offset: 0x000DD857
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

		// Token: 0x06004775 RID: 18293 RVA: 0x000DF66A File Offset: 0x000DD86A
		public void SetForward(ObjectAddress val)
		{
			this.Forward = val;
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x000DF674 File Offset: 0x000DD874
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.ChangedFriend.GetHashCode();
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

		// Token: 0x06004777 RID: 18295 RVA: 0x000DF6C8 File Offset: 0x000DD8C8
		public override bool Equals(object obj)
		{
			UpdateFriendStateNotification updateFriendStateNotification = obj as UpdateFriendStateNotification;
			return updateFriendStateNotification != null && this.ChangedFriend.Equals(updateFriendStateNotification.ChangedFriend) && this.HasAccountId == updateFriendStateNotification.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(updateFriendStateNotification.AccountId)) && this.HasForward == updateFriendStateNotification.HasForward && (!this.HasForward || this.Forward.Equals(updateFriendStateNotification.Forward));
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x000DF74D File Offset: 0x000DD94D
		public static UpdateFriendStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UpdateFriendStateNotification>(bs, 0, -1);
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x000DF757 File Offset: 0x000DD957
		public void Deserialize(Stream stream)
		{
			UpdateFriendStateNotification.Deserialize(stream, this);
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x000DF761 File Offset: 0x000DD961
		public static UpdateFriendStateNotification Deserialize(Stream stream, UpdateFriendStateNotification instance)
		{
			return UpdateFriendStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x000DF76C File Offset: 0x000DD96C
		public static UpdateFriendStateNotification DeserializeLengthDelimited(Stream stream)
		{
			UpdateFriendStateNotification updateFriendStateNotification = new UpdateFriendStateNotification();
			UpdateFriendStateNotification.DeserializeLengthDelimited(stream, updateFriendStateNotification);
			return updateFriendStateNotification;
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x000DF788 File Offset: 0x000DD988
		public static UpdateFriendStateNotification DeserializeLengthDelimited(Stream stream, UpdateFriendStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UpdateFriendStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x000DF7B0 File Offset: 0x000DD9B0
		public static UpdateFriendStateNotification Deserialize(Stream stream, UpdateFriendStateNotification instance, long limit)
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
				else if (instance.ChangedFriend == null)
				{
					instance.ChangedFriend = Friend.DeserializeLengthDelimited(stream);
				}
				else
				{
					Friend.DeserializeLengthDelimited(stream, instance.ChangedFriend);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000DF8B2 File Offset: 0x000DDAB2
		public void Serialize(Stream stream)
		{
			UpdateFriendStateNotification.Serialize(stream, this);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x000DF8BC File Offset: 0x000DDABC
		public static void Serialize(Stream stream, UpdateFriendStateNotification instance)
		{
			if (instance.ChangedFriend == null)
			{
				throw new ArgumentNullException("ChangedFriend", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.ChangedFriend.GetSerializedSize());
			Friend.Serialize(stream, instance.ChangedFriend);
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

		// Token: 0x06004781 RID: 18305 RVA: 0x000DF960 File Offset: 0x000DDB60
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.ChangedFriend.GetSerializedSize();
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

		// Token: 0x040017C6 RID: 6086
		public bool HasAccountId;

		// Token: 0x040017C7 RID: 6087
		private EntityId _AccountId;

		// Token: 0x040017C8 RID: 6088
		public bool HasForward;

		// Token: 0x040017C9 RID: 6089
		private ObjectAddress _Forward;
	}
}
