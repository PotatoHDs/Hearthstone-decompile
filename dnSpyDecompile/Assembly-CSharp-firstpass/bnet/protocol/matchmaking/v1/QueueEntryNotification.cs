using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003E9 RID: 1001
	public class QueueEntryNotification : IProtoBuf
	{
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x000D1E54 File Offset: 0x000D0054
		// (set) Token: 0x0600421B RID: 16923 RVA: 0x000D1E5C File Offset: 0x000D005C
		public RequestId RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = (value != null);
			}
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x000D1E6F File Offset: 0x000D006F
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x000D1E78 File Offset: 0x000D0078
		// (set) Token: 0x0600421E RID: 16926 RVA: 0x000D1E80 File Offset: 0x000D0080
		public QueueWaitTimes WaitTimes
		{
			get
			{
				return this._WaitTimes;
			}
			set
			{
				this._WaitTimes = value;
				this.HasWaitTimes = (value != null);
			}
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x000D1E93 File Offset: 0x000D0093
		public void SetWaitTimes(QueueWaitTimes val)
		{
			this.WaitTimes = val;
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x000D1E9C File Offset: 0x000D009C
		// (set) Token: 0x06004221 RID: 16929 RVA: 0x000D1EA4 File Offset: 0x000D00A4
		public List<GameAccountHandle> Member
		{
			get
			{
				return this._Member;
			}
			set
			{
				this._Member = value;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004222 RID: 16930 RVA: 0x000D1E9C File Offset: 0x000D009C
		public List<GameAccountHandle> MemberList
		{
			get
			{
				return this._Member;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004223 RID: 16931 RVA: 0x000D1EAD File Offset: 0x000D00AD
		public int MemberCount
		{
			get
			{
				return this._Member.Count;
			}
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x000D1EBA File Offset: 0x000D00BA
		public void AddMember(GameAccountHandle val)
		{
			this._Member.Add(val);
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x000D1EC8 File Offset: 0x000D00C8
		public void ClearMember()
		{
			this._Member.Clear();
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x000D1ED5 File Offset: 0x000D00D5
		public void SetMember(List<GameAccountHandle> val)
		{
			this.Member = val;
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004227 RID: 16935 RVA: 0x000D1EDE File Offset: 0x000D00DE
		// (set) Token: 0x06004228 RID: 16936 RVA: 0x000D1EE6 File Offset: 0x000D00E6
		public GameAccountHandle RequestInitiator
		{
			get
			{
				return this._RequestInitiator;
			}
			set
			{
				this._RequestInitiator = value;
				this.HasRequestInitiator = (value != null);
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000D1EF9 File Offset: 0x000D00F9
		public void SetRequestInitiator(GameAccountHandle val)
		{
			this.RequestInitiator = val;
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000D1F04 File Offset: 0x000D0104
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasWaitTimes)
			{
				num ^= this.WaitTimes.GetHashCode();
			}
			foreach (GameAccountHandle gameAccountHandle in this.Member)
			{
				num ^= gameAccountHandle.GetHashCode();
			}
			if (this.HasRequestInitiator)
			{
				num ^= this.RequestInitiator.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000D1FA8 File Offset: 0x000D01A8
		public override bool Equals(object obj)
		{
			QueueEntryNotification queueEntryNotification = obj as QueueEntryNotification;
			if (queueEntryNotification == null)
			{
				return false;
			}
			if (this.HasRequestId != queueEntryNotification.HasRequestId || (this.HasRequestId && !this.RequestId.Equals(queueEntryNotification.RequestId)))
			{
				return false;
			}
			if (this.HasWaitTimes != queueEntryNotification.HasWaitTimes || (this.HasWaitTimes && !this.WaitTimes.Equals(queueEntryNotification.WaitTimes)))
			{
				return false;
			}
			if (this.Member.Count != queueEntryNotification.Member.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Member.Count; i++)
			{
				if (!this.Member[i].Equals(queueEntryNotification.Member[i]))
				{
					return false;
				}
			}
			return this.HasRequestInitiator == queueEntryNotification.HasRequestInitiator && (!this.HasRequestInitiator || this.RequestInitiator.Equals(queueEntryNotification.RequestInitiator));
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000D2094 File Offset: 0x000D0294
		public static QueueEntryNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueEntryNotification>(bs, 0, -1);
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000D209E File Offset: 0x000D029E
		public void Deserialize(Stream stream)
		{
			QueueEntryNotification.Deserialize(stream, this);
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x000D20A8 File Offset: 0x000D02A8
		public static QueueEntryNotification Deserialize(Stream stream, QueueEntryNotification instance)
		{
			return QueueEntryNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000D20B4 File Offset: 0x000D02B4
		public static QueueEntryNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueEntryNotification queueEntryNotification = new QueueEntryNotification();
			QueueEntryNotification.DeserializeLengthDelimited(stream, queueEntryNotification);
			return queueEntryNotification;
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000D20D0 File Offset: 0x000D02D0
		public static QueueEntryNotification DeserializeLengthDelimited(Stream stream, QueueEntryNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueEntryNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000D20F8 File Offset: 0x000D02F8
		public static QueueEntryNotification Deserialize(Stream stream, QueueEntryNotification instance, long limit)
		{
			if (instance.Member == null)
			{
				instance.Member = new List<GameAccountHandle>();
			}
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
								if (instance.WaitTimes == null)
								{
									instance.WaitTimes = QueueWaitTimes.DeserializeLengthDelimited(stream);
									continue;
								}
								QueueWaitTimes.DeserializeLengthDelimited(stream, instance.WaitTimes);
								continue;
							}
						}
						else
						{
							if (instance.RequestId == null)
							{
								instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
								continue;
							}
							RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
							continue;
						}
					}
					else
					{
						if (num == 26)
						{
							instance.Member.Add(GameAccountHandle.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num == 34)
						{
							if (instance.RequestInitiator == null)
							{
								instance.RequestInitiator = GameAccountHandle.DeserializeLengthDelimited(stream);
								continue;
							}
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.RequestInitiator);
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

		// Token: 0x06004233 RID: 16947 RVA: 0x000D2238 File Offset: 0x000D0438
		public void Serialize(Stream stream)
		{
			QueueEntryNotification.Serialize(stream, this);
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000D2244 File Offset: 0x000D0444
		public static void Serialize(Stream stream, QueueEntryNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasWaitTimes)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.WaitTimes.GetSerializedSize());
				QueueWaitTimes.Serialize(stream, instance.WaitTimes);
			}
			if (instance.Member.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in instance.Member)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, gameAccountHandle.GetSerializedSize());
					GameAccountHandle.Serialize(stream, gameAccountHandle);
				}
			}
			if (instance.HasRequestInitiator)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.RequestInitiator.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.RequestInitiator);
			}
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x000D2340 File Offset: 0x000D0540
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasWaitTimes)
			{
				num += 1U;
				uint serializedSize2 = this.WaitTimes.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.Member.Count > 0)
			{
				foreach (GameAccountHandle gameAccountHandle in this.Member)
				{
					num += 1U;
					uint serializedSize3 = gameAccountHandle.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			if (this.HasRequestInitiator)
			{
				num += 1U;
				uint serializedSize4 = this.RequestInitiator.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			return num;
		}

		// Token: 0x040016C2 RID: 5826
		public bool HasRequestId;

		// Token: 0x040016C3 RID: 5827
		private RequestId _RequestId;

		// Token: 0x040016C4 RID: 5828
		public bool HasWaitTimes;

		// Token: 0x040016C5 RID: 5829
		private QueueWaitTimes _WaitTimes;

		// Token: 0x040016C6 RID: 5830
		private List<GameAccountHandle> _Member = new List<GameAccountHandle>();

		// Token: 0x040016C7 RID: 5831
		public bool HasRequestInitiator;

		// Token: 0x040016C8 RID: 5832
		private GameAccountHandle _RequestInitiator;
	}
}
