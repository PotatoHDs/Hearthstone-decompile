using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.report.v2.Types;

namespace bnet.protocol.report.v2
{
	// Token: 0x02000321 RID: 801
	public class UserOptions : IProtoBuf
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000A4FEB File Offset: 0x000A31EB
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x000A4FF3 File Offset: 0x000A31F3
		public AccountId TargetId
		{
			get
			{
				return this._TargetId;
			}
			set
			{
				this._TargetId = value;
				this.HasTargetId = (value != null);
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000A5006 File Offset: 0x000A3206
		public void SetTargetId(AccountId val)
		{
			this.TargetId = val;
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000A500F File Offset: 0x000A320F
		// (set) Token: 0x0600310E RID: 12558 RVA: 0x000A5017 File Offset: 0x000A3217
		public IssueType Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
				this.HasType = true;
			}
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000A5027 File Offset: 0x000A3227
		public void SetType(IssueType val)
		{
			this.Type = val;
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000A5030 File Offset: 0x000A3230
		// (set) Token: 0x06003111 RID: 12561 RVA: 0x000A5038 File Offset: 0x000A3238
		public UserSource Source
		{
			get
			{
				return this._Source;
			}
			set
			{
				this._Source = value;
				this.HasSource = true;
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000A5048 File Offset: 0x000A3248
		public void SetSource(UserSource val)
		{
			this.Source = val;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x000A5051 File Offset: 0x000A3251
		// (set) Token: 0x06003114 RID: 12564 RVA: 0x000A5059 File Offset: 0x000A3259
		public ReportItem Item
		{
			get
			{
				return this._Item;
			}
			set
			{
				this._Item = value;
				this.HasItem = (value != null);
			}
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000A506C File Offset: 0x000A326C
		public void SetItem(ReportItem val)
		{
			this.Item = val;
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000A5078 File Offset: 0x000A3278
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasTargetId)
			{
				num ^= this.TargetId.GetHashCode();
			}
			if (this.HasType)
			{
				num ^= this.Type.GetHashCode();
			}
			if (this.HasSource)
			{
				num ^= this.Source.GetHashCode();
			}
			if (this.HasItem)
			{
				num ^= this.Item.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000A50FC File Offset: 0x000A32FC
		public override bool Equals(object obj)
		{
			UserOptions userOptions = obj as UserOptions;
			return userOptions != null && this.HasTargetId == userOptions.HasTargetId && (!this.HasTargetId || this.TargetId.Equals(userOptions.TargetId)) && this.HasType == userOptions.HasType && (!this.HasType || this.Type.Equals(userOptions.Type)) && this.HasSource == userOptions.HasSource && (!this.HasSource || this.Source.Equals(userOptions.Source)) && this.HasItem == userOptions.HasItem && (!this.HasItem || this.Item.Equals(userOptions.Item));
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000A51DE File Offset: 0x000A33DE
		public static UserOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<UserOptions>(bs, 0, -1);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000A51E8 File Offset: 0x000A33E8
		public void Deserialize(Stream stream)
		{
			UserOptions.Deserialize(stream, this);
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000A51F2 File Offset: 0x000A33F2
		public static UserOptions Deserialize(Stream stream, UserOptions instance)
		{
			return UserOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000A5200 File Offset: 0x000A3400
		public static UserOptions DeserializeLengthDelimited(Stream stream)
		{
			UserOptions userOptions = new UserOptions();
			UserOptions.DeserializeLengthDelimited(stream, userOptions);
			return userOptions;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000A521C File Offset: 0x000A341C
		public static UserOptions DeserializeLengthDelimited(Stream stream, UserOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return UserOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000A5244 File Offset: 0x000A3444
		public static UserOptions Deserialize(Stream stream, UserOptions instance, long limit)
		{
			instance.Type = IssueType.ISSUE_TYPE_SPAM;
			instance.Source = UserSource.USER_SOURCE_OTHER;
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
								instance.Type = (IssueType)ProtocolParser.ReadUInt64(stream);
								continue;
							}
						}
						else
						{
							if (instance.TargetId == null)
							{
								instance.TargetId = AccountId.DeserializeLengthDelimited(stream);
								continue;
							}
							AccountId.DeserializeLengthDelimited(stream, instance.TargetId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Source = (UserSource)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 34)
						{
							if (instance.Item == null)
							{
								instance.Item = ReportItem.DeserializeLengthDelimited(stream);
								continue;
							}
							ReportItem.DeserializeLengthDelimited(stream, instance.Item);
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

		// Token: 0x0600311F RID: 12575 RVA: 0x000A535F File Offset: 0x000A355F
		public void Serialize(Stream stream)
		{
			UserOptions.Serialize(stream, this);
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000A5368 File Offset: 0x000A3568
		public static void Serialize(Stream stream, UserOptions instance)
		{
			if (instance.HasTargetId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetId.GetSerializedSize());
				AccountId.Serialize(stream, instance.TargetId);
			}
			if (instance.HasType)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Type));
			}
			if (instance.HasSource)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Source));
			}
			if (instance.HasItem)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Item.GetSerializedSize());
				ReportItem.Serialize(stream, instance.Item);
			}
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000A540C File Offset: 0x000A360C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasTargetId)
			{
				num += 1U;
				uint serializedSize = this.TargetId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Type));
			}
			if (this.HasSource)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Source));
			}
			if (this.HasItem)
			{
				num += 1U;
				uint serializedSize2 = this.Item.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x04001372 RID: 4978
		public bool HasTargetId;

		// Token: 0x04001373 RID: 4979
		private AccountId _TargetId;

		// Token: 0x04001374 RID: 4980
		public bool HasType;

		// Token: 0x04001375 RID: 4981
		private IssueType _Type;

		// Token: 0x04001376 RID: 4982
		public bool HasSource;

		// Token: 0x04001377 RID: 4983
		private UserSource _Source;

		// Token: 0x04001378 RID: 4984
		public bool HasItem;

		// Token: 0x04001379 RID: 4985
		private ReportItem _Item;
	}
}
