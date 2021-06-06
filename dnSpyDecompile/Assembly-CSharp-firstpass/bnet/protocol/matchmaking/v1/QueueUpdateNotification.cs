using System;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003EC RID: 1004
	public class QueueUpdateNotification : IProtoBuf
	{
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x000D2C1B File Offset: 0x000D0E1B
		// (set) Token: 0x06004265 RID: 16997 RVA: 0x000D2C23 File Offset: 0x000D0E23
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

		// Token: 0x06004266 RID: 16998 RVA: 0x000D2C36 File Offset: 0x000D0E36
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x000D2C3F File Offset: 0x000D0E3F
		// (set) Token: 0x06004268 RID: 17000 RVA: 0x000D2C47 File Offset: 0x000D0E47
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

		// Token: 0x06004269 RID: 17001 RVA: 0x000D2C5A File Offset: 0x000D0E5A
		public void SetWaitTimes(QueueWaitTimes val)
		{
			this.WaitTimes = val;
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x0600426A RID: 17002 RVA: 0x000D2C63 File Offset: 0x000D0E63
		// (set) Token: 0x0600426B RID: 17003 RVA: 0x000D2C6B File Offset: 0x000D0E6B
		public bool IsMatchmaking
		{
			get
			{
				return this._IsMatchmaking;
			}
			set
			{
				this._IsMatchmaking = value;
				this.HasIsMatchmaking = true;
			}
		}

		// Token: 0x0600426C RID: 17004 RVA: 0x000D2C7B File Offset: 0x000D0E7B
		public void SetIsMatchmaking(bool val)
		{
			this.IsMatchmaking = val;
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x000D2C84 File Offset: 0x000D0E84
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
			if (this.HasIsMatchmaking)
			{
				num ^= this.IsMatchmaking.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x000D2CE4 File Offset: 0x000D0EE4
		public override bool Equals(object obj)
		{
			QueueUpdateNotification queueUpdateNotification = obj as QueueUpdateNotification;
			return queueUpdateNotification != null && this.HasRequestId == queueUpdateNotification.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(queueUpdateNotification.RequestId)) && this.HasWaitTimes == queueUpdateNotification.HasWaitTimes && (!this.HasWaitTimes || this.WaitTimes.Equals(queueUpdateNotification.WaitTimes)) && this.HasIsMatchmaking == queueUpdateNotification.HasIsMatchmaking && (!this.HasIsMatchmaking || this.IsMatchmaking.Equals(queueUpdateNotification.IsMatchmaking));
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x000D2D82 File Offset: 0x000D0F82
		public static QueueUpdateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueUpdateNotification>(bs, 0, -1);
		}

		// Token: 0x06004271 RID: 17009 RVA: 0x000D2D8C File Offset: 0x000D0F8C
		public void Deserialize(Stream stream)
		{
			QueueUpdateNotification.Deserialize(stream, this);
		}

		// Token: 0x06004272 RID: 17010 RVA: 0x000D2D96 File Offset: 0x000D0F96
		public static QueueUpdateNotification Deserialize(Stream stream, QueueUpdateNotification instance)
		{
			return QueueUpdateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004273 RID: 17011 RVA: 0x000D2DA4 File Offset: 0x000D0FA4
		public static QueueUpdateNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueUpdateNotification queueUpdateNotification = new QueueUpdateNotification();
			QueueUpdateNotification.DeserializeLengthDelimited(stream, queueUpdateNotification);
			return queueUpdateNotification;
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x000D2DC0 File Offset: 0x000D0FC0
		public static QueueUpdateNotification DeserializeLengthDelimited(Stream stream, QueueUpdateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueUpdateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x000D2DE8 File Offset: 0x000D0FE8
		public static QueueUpdateNotification Deserialize(Stream stream, QueueUpdateNotification instance, long limit)
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
					if (num != 18)
					{
						if (num != 24)
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
							instance.IsMatchmaking = ProtocolParser.ReadBool(stream);
						}
					}
					else if (instance.WaitTimes == null)
					{
						instance.WaitTimes = QueueWaitTimes.DeserializeLengthDelimited(stream);
					}
					else
					{
						QueueWaitTimes.DeserializeLengthDelimited(stream, instance.WaitTimes);
					}
				}
				else if (instance.RequestId == null)
				{
					instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
				}
				else
				{
					RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x000D2ED0 File Offset: 0x000D10D0
		public void Serialize(Stream stream)
		{
			QueueUpdateNotification.Serialize(stream, this);
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x000D2EDC File Offset: 0x000D10DC
		public static void Serialize(Stream stream, QueueUpdateNotification instance)
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
			if (instance.HasIsMatchmaking)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsMatchmaking);
			}
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000D2F60 File Offset: 0x000D1160
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
			if (this.HasIsMatchmaking)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x040016D2 RID: 5842
		public bool HasRequestId;

		// Token: 0x040016D3 RID: 5843
		private RequestId _RequestId;

		// Token: 0x040016D4 RID: 5844
		public bool HasWaitTimes;

		// Token: 0x040016D5 RID: 5845
		private QueueWaitTimes _WaitTimes;

		// Token: 0x040016D6 RID: 5846
		public bool HasIsMatchmaking;

		// Token: 0x040016D7 RID: 5847
		private bool _IsMatchmaking;
	}
}
