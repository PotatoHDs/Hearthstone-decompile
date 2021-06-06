using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003EA RID: 1002
	public class QueueExitNotification : IProtoBuf
	{
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x000D2433 File Offset: 0x000D0633
		// (set) Token: 0x06004238 RID: 16952 RVA: 0x000D243B File Offset: 0x000D063B
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

		// Token: 0x06004239 RID: 16953 RVA: 0x000D244E File Offset: 0x000D064E
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x000D2457 File Offset: 0x000D0657
		// (set) Token: 0x0600423B RID: 16955 RVA: 0x000D245F File Offset: 0x000D065F
		public GameAccountHandle CancelInitiator
		{
			get
			{
				return this._CancelInitiator;
			}
			set
			{
				this._CancelInitiator = value;
				this.HasCancelInitiator = (value != null);
			}
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x000D2472 File Offset: 0x000D0672
		public void SetCancelInitiator(GameAccountHandle val)
		{
			this.CancelInitiator = val;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x000D247C File Offset: 0x000D067C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasCancelInitiator)
			{
				num ^= this.CancelInitiator.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000D24C4 File Offset: 0x000D06C4
		public override bool Equals(object obj)
		{
			QueueExitNotification queueExitNotification = obj as QueueExitNotification;
			return queueExitNotification != null && this.HasRequestId == queueExitNotification.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(queueExitNotification.RequestId)) && this.HasCancelInitiator == queueExitNotification.HasCancelInitiator && (!this.HasCancelInitiator || this.CancelInitiator.Equals(queueExitNotification.CancelInitiator));
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000D2534 File Offset: 0x000D0734
		public static QueueExitNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueExitNotification>(bs, 0, -1);
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x000D253E File Offset: 0x000D073E
		public void Deserialize(Stream stream)
		{
			QueueExitNotification.Deserialize(stream, this);
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000D2548 File Offset: 0x000D0748
		public static QueueExitNotification Deserialize(Stream stream, QueueExitNotification instance)
		{
			return QueueExitNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004243 RID: 16963 RVA: 0x000D2554 File Offset: 0x000D0754
		public static QueueExitNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueExitNotification queueExitNotification = new QueueExitNotification();
			QueueExitNotification.DeserializeLengthDelimited(stream, queueExitNotification);
			return queueExitNotification;
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000D2570 File Offset: 0x000D0770
		public static QueueExitNotification DeserializeLengthDelimited(Stream stream, QueueExitNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueExitNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000D2598 File Offset: 0x000D0798
		public static QueueExitNotification Deserialize(Stream stream, QueueExitNotification instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else if (instance.CancelInitiator == null)
					{
						instance.CancelInitiator = GameAccountHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameAccountHandle.DeserializeLengthDelimited(stream, instance.CancelInitiator);
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

		// Token: 0x06004246 RID: 16966 RVA: 0x000D266A File Offset: 0x000D086A
		public void Serialize(Stream stream)
		{
			QueueExitNotification.Serialize(stream, this);
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000D2674 File Offset: 0x000D0874
		public static void Serialize(Stream stream, QueueExitNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasCancelInitiator)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.CancelInitiator.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.CancelInitiator);
			}
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x000D26DC File Offset: 0x000D08DC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasCancelInitiator)
			{
				num += 1U;
				uint serializedSize2 = this.CancelInitiator.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x040016C9 RID: 5833
		public bool HasRequestId;

		// Token: 0x040016CA RID: 5834
		private RequestId _RequestId;

		// Token: 0x040016CB RID: 5835
		public bool HasCancelInitiator;

		// Token: 0x040016CC RID: 5836
		private GameAccountHandle _CancelInitiator;
	}
}
