using System;
using System.IO;

namespace bnet.protocol.account.v1
{
	// Token: 0x0200051E RID: 1310
	public class GameAccountStateNotification : IProtoBuf
	{
		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x0011B9D0 File Offset: 0x00119BD0
		// (set) Token: 0x06005D75 RID: 23925 RVA: 0x0011B9D8 File Offset: 0x00119BD8
		public GameAccountState GameAccountState
		{
			get
			{
				return this._GameAccountState;
			}
			set
			{
				this._GameAccountState = value;
				this.HasGameAccountState = (value != null);
			}
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x0011B9EB File Offset: 0x00119BEB
		public void SetGameAccountState(GameAccountState val)
		{
			this.GameAccountState = val;
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x0011B9F4 File Offset: 0x00119BF4
		// (set) Token: 0x06005D78 RID: 23928 RVA: 0x0011B9FC File Offset: 0x00119BFC
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

		// Token: 0x06005D79 RID: 23929 RVA: 0x0011BA0C File Offset: 0x00119C0C
		public void SetSubscriberId(ulong val)
		{
			this.SubscriberId = val;
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x0011BA15 File Offset: 0x00119C15
		// (set) Token: 0x06005D7B RID: 23931 RVA: 0x0011BA1D File Offset: 0x00119C1D
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

		// Token: 0x06005D7C RID: 23932 RVA: 0x0011BA30 File Offset: 0x00119C30
		public void SetGameAccountTags(GameAccountFieldTags val)
		{
			this.GameAccountTags = val;
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x0011BA39 File Offset: 0x00119C39
		// (set) Token: 0x06005D7E RID: 23934 RVA: 0x0011BA41 File Offset: 0x00119C41
		public bool SubscriptionCompleted
		{
			get
			{
				return this._SubscriptionCompleted;
			}
			set
			{
				this._SubscriptionCompleted = value;
				this.HasSubscriptionCompleted = true;
			}
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x0011BA51 File Offset: 0x00119C51
		public void SetSubscriptionCompleted(bool val)
		{
			this.SubscriptionCompleted = val;
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x0011BA5C File Offset: 0x00119C5C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameAccountState)
			{
				num ^= this.GameAccountState.GetHashCode();
			}
			if (this.HasSubscriberId)
			{
				num ^= this.SubscriberId.GetHashCode();
			}
			if (this.HasGameAccountTags)
			{
				num ^= this.GameAccountTags.GetHashCode();
			}
			if (this.HasSubscriptionCompleted)
			{
				num ^= this.SubscriptionCompleted.GetHashCode();
			}
			return num;
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x0011BAD4 File Offset: 0x00119CD4
		public override bool Equals(object obj)
		{
			GameAccountStateNotification gameAccountStateNotification = obj as GameAccountStateNotification;
			return gameAccountStateNotification != null && this.HasGameAccountState == gameAccountStateNotification.HasGameAccountState && (!this.HasGameAccountState || this.GameAccountState.Equals(gameAccountStateNotification.GameAccountState)) && this.HasSubscriberId == gameAccountStateNotification.HasSubscriberId && (!this.HasSubscriberId || this.SubscriberId.Equals(gameAccountStateNotification.SubscriberId)) && this.HasGameAccountTags == gameAccountStateNotification.HasGameAccountTags && (!this.HasGameAccountTags || this.GameAccountTags.Equals(gameAccountStateNotification.GameAccountTags)) && this.HasSubscriptionCompleted == gameAccountStateNotification.HasSubscriptionCompleted && (!this.HasSubscriptionCompleted || this.SubscriptionCompleted.Equals(gameAccountStateNotification.SubscriptionCompleted));
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06005D82 RID: 23938 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x0011BBA0 File Offset: 0x00119DA0
		public static GameAccountStateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameAccountStateNotification>(bs, 0, -1);
		}

		// Token: 0x06005D84 RID: 23940 RVA: 0x0011BBAA File Offset: 0x00119DAA
		public void Deserialize(Stream stream)
		{
			GameAccountStateNotification.Deserialize(stream, this);
		}

		// Token: 0x06005D85 RID: 23941 RVA: 0x0011BBB4 File Offset: 0x00119DB4
		public static GameAccountStateNotification Deserialize(Stream stream, GameAccountStateNotification instance)
		{
			return GameAccountStateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x0011BBC0 File Offset: 0x00119DC0
		public static GameAccountStateNotification DeserializeLengthDelimited(Stream stream)
		{
			GameAccountStateNotification gameAccountStateNotification = new GameAccountStateNotification();
			GameAccountStateNotification.DeserializeLengthDelimited(stream, gameAccountStateNotification);
			return gameAccountStateNotification;
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x0011BBDC File Offset: 0x00119DDC
		public static GameAccountStateNotification DeserializeLengthDelimited(Stream stream, GameAccountStateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameAccountStateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x0011BC04 File Offset: 0x00119E04
		public static GameAccountStateNotification Deserialize(Stream stream, GameAccountStateNotification instance, long limit)
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
								instance.SubscriberId = ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.GameAccountState == null)
							{
								instance.GameAccountState = GameAccountState.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountState.DeserializeLengthDelimited(stream, instance.GameAccountState);
							continue;
						}
					}
					else if (num != 26)
					{
						if (num == 32)
						{
							instance.SubscriptionCompleted = ProtocolParser.ReadBool(stream);
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

		// Token: 0x06005D89 RID: 23945 RVA: 0x0011BD0F File Offset: 0x00119F0F
		public void Serialize(Stream stream)
		{
			GameAccountStateNotification.Serialize(stream, this);
		}

		// Token: 0x06005D8A RID: 23946 RVA: 0x0011BD18 File Offset: 0x00119F18
		public static void Serialize(Stream stream, GameAccountStateNotification instance)
		{
			if (instance.HasGameAccountState)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountState.GetSerializedSize());
				GameAccountState.Serialize(stream, instance.GameAccountState);
			}
			if (instance.HasSubscriberId)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.SubscriberId);
			}
			if (instance.HasGameAccountTags)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountTags.GetSerializedSize());
				GameAccountFieldTags.Serialize(stream, instance.GameAccountTags);
			}
			if (instance.HasSubscriptionCompleted)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.SubscriptionCompleted);
			}
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x0011BDB8 File Offset: 0x00119FB8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameAccountState)
			{
				num += 1U;
				uint serializedSize = this.GameAccountState.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasSubscriberId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.SubscriberId);
			}
			if (this.HasGameAccountTags)
			{
				num += 1U;
				uint serializedSize2 = this.GameAccountTags.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasSubscriptionCompleted)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001CD0 RID: 7376
		public bool HasGameAccountState;

		// Token: 0x04001CD1 RID: 7377
		private GameAccountState _GameAccountState;

		// Token: 0x04001CD2 RID: 7378
		public bool HasSubscriberId;

		// Token: 0x04001CD3 RID: 7379
		private ulong _SubscriberId;

		// Token: 0x04001CD4 RID: 7380
		public bool HasGameAccountTags;

		// Token: 0x04001CD5 RID: 7381
		private GameAccountFieldTags _GameAccountTags;

		// Token: 0x04001CD6 RID: 7382
		public bool HasSubscriptionCompleted;

		// Token: 0x04001CD7 RID: 7383
		private bool _SubscriptionCompleted;
	}
}
