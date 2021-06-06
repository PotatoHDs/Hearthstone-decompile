using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000397 RID: 919
	public class QueueEntryNotification : IProtoBuf
	{
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000BECCA File Offset: 0x000BCECA
		// (set) Token: 0x06003AF6 RID: 15094 RVA: 0x000BECD2 File Offset: 0x000BCED2
		public FindGameRequestId RequestId
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

		// Token: 0x06003AF7 RID: 15095 RVA: 0x000BECE5 File Offset: 0x000BCEE5
		public void SetRequestId(FindGameRequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000BECEE File Offset: 0x000BCEEE
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x000BECF6 File Offset: 0x000BCEF6
		public QueueUpdate UpdateInfo
		{
			get
			{
				return this._UpdateInfo;
			}
			set
			{
				this._UpdateInfo = value;
				this.HasUpdateInfo = (value != null);
			}
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000BED09 File Offset: 0x000BCF09
		public void SetUpdateInfo(QueueUpdate val)
		{
			this.UpdateInfo = val;
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x000BED12 File Offset: 0x000BCF12
		// (set) Token: 0x06003AFC RID: 15100 RVA: 0x000BED1A File Offset: 0x000BCF1A
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

		// Token: 0x06003AFD RID: 15101 RVA: 0x000BED2D File Offset: 0x000BCF2D
		public void SetRequestInitiator(GameAccountHandle val)
		{
			this.RequestInitiator = val;
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x000BED38 File Offset: 0x000BCF38
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			if (this.HasUpdateInfo)
			{
				num ^= this.UpdateInfo.GetHashCode();
			}
			if (this.HasRequestInitiator)
			{
				num ^= this.RequestInitiator.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000BED94 File Offset: 0x000BCF94
		public override bool Equals(object obj)
		{
			QueueEntryNotification queueEntryNotification = obj as QueueEntryNotification;
			return queueEntryNotification != null && this.HasRequestId == queueEntryNotification.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(queueEntryNotification.RequestId)) && this.HasUpdateInfo == queueEntryNotification.HasUpdateInfo && (!this.HasUpdateInfo || this.UpdateInfo.Equals(queueEntryNotification.UpdateInfo)) && this.HasRequestInitiator == queueEntryNotification.HasRequestInitiator && (!this.HasRequestInitiator || this.RequestInitiator.Equals(queueEntryNotification.RequestInitiator));
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000BEE2F File Offset: 0x000BD02F
		public static QueueEntryNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueEntryNotification>(bs, 0, -1);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000BEE39 File Offset: 0x000BD039
		public void Deserialize(Stream stream)
		{
			QueueEntryNotification.Deserialize(stream, this);
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000BEE43 File Offset: 0x000BD043
		public static QueueEntryNotification Deserialize(Stream stream, QueueEntryNotification instance)
		{
			return QueueEntryNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x000BEE50 File Offset: 0x000BD050
		public static QueueEntryNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueEntryNotification queueEntryNotification = new QueueEntryNotification();
			QueueEntryNotification.DeserializeLengthDelimited(stream, queueEntryNotification);
			return queueEntryNotification;
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000BEE6C File Offset: 0x000BD06C
		public static QueueEntryNotification DeserializeLengthDelimited(Stream stream, QueueEntryNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueEntryNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x000BEE94 File Offset: 0x000BD094
		public static QueueEntryNotification Deserialize(Stream stream, QueueEntryNotification instance, long limit)
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
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.RequestInitiator == null)
						{
							instance.RequestInitiator = GameAccountHandle.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameAccountHandle.DeserializeLengthDelimited(stream, instance.RequestInitiator);
						}
					}
					else if (instance.UpdateInfo == null)
					{
						instance.UpdateInfo = QueueUpdate.DeserializeLengthDelimited(stream);
					}
					else
					{
						QueueUpdate.DeserializeLengthDelimited(stream, instance.UpdateInfo);
					}
				}
				else if (instance.RequestId == null)
				{
					instance.RequestId = FindGameRequestId.DeserializeLengthDelimited(stream);
				}
				else
				{
					FindGameRequestId.DeserializeLengthDelimited(stream, instance.RequestId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000BEF96 File Offset: 0x000BD196
		public void Serialize(Stream stream)
		{
			QueueEntryNotification.Serialize(stream, this);
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000BEFA0 File Offset: 0x000BD1A0
		public static void Serialize(Stream stream, QueueEntryNotification instance)
		{
			if (instance.HasRequestId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				FindGameRequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.HasUpdateInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.UpdateInfo.GetSerializedSize());
				QueueUpdate.Serialize(stream, instance.UpdateInfo);
			}
			if (instance.HasRequestInitiator)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.RequestInitiator.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.RequestInitiator);
			}
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000BF034 File Offset: 0x000BD234
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize = this.RequestId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasUpdateInfo)
			{
				num += 1U;
				uint serializedSize2 = this.UpdateInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasRequestInitiator)
			{
				num += 1U;
				uint serializedSize3 = this.RequestInitiator.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001556 RID: 5462
		public bool HasRequestId;

		// Token: 0x04001557 RID: 5463
		private FindGameRequestId _RequestId;

		// Token: 0x04001558 RID: 5464
		public bool HasUpdateInfo;

		// Token: 0x04001559 RID: 5465
		private QueueUpdate _UpdateInfo;

		// Token: 0x0400155A RID: 5466
		public bool HasRequestInitiator;

		// Token: 0x0400155B RID: 5467
		private GameAccountHandle _RequestInitiator;
	}
}
