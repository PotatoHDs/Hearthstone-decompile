using System;
using System.IO;
using bnet.protocol.games.v2;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000398 RID: 920
	public class QueueUpdateNotification : IProtoBuf
	{
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000BF0AD File Offset: 0x000BD2AD
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x000BF0B5 File Offset: 0x000BD2B5
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

		// Token: 0x06003B0D RID: 15117 RVA: 0x000BF0C8 File Offset: 0x000BD2C8
		public void SetRequestId(FindGameRequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x000BF0D1 File Offset: 0x000BD2D1
		// (set) Token: 0x06003B0F RID: 15119 RVA: 0x000BF0D9 File Offset: 0x000BD2D9
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

		// Token: 0x06003B10 RID: 15120 RVA: 0x000BF0EC File Offset: 0x000BD2EC
		public void SetUpdateInfo(QueueUpdate val)
		{
			this.UpdateInfo = val;
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x000BF0F5 File Offset: 0x000BD2F5
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x000BF0FD File Offset: 0x000BD2FD
		public bool Matchmaking
		{
			get
			{
				return this._Matchmaking;
			}
			set
			{
				this._Matchmaking = value;
				this.HasMatchmaking = true;
			}
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x000BF10D File Offset: 0x000BD30D
		public void SetMatchmaking(bool val)
		{
			this.Matchmaking = val;
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x000BF116 File Offset: 0x000BD316
		// (set) Token: 0x06003B15 RID: 15125 RVA: 0x000BF11E File Offset: 0x000BD31E
		public uint MatchmakerResult
		{
			get
			{
				return this._MatchmakerResult;
			}
			set
			{
				this._MatchmakerResult = value;
				this.HasMatchmakerResult = true;
			}
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x000BF12E File Offset: 0x000BD32E
		public void SetMatchmakerResult(uint val)
		{
			this.MatchmakerResult = val;
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000BF138 File Offset: 0x000BD338
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
			if (this.HasMatchmaking)
			{
				num ^= this.Matchmaking.GetHashCode();
			}
			if (this.HasMatchmakerResult)
			{
				num ^= this.MatchmakerResult.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000BF1B0 File Offset: 0x000BD3B0
		public override bool Equals(object obj)
		{
			QueueUpdateNotification queueUpdateNotification = obj as QueueUpdateNotification;
			return queueUpdateNotification != null && this.HasRequestId == queueUpdateNotification.HasRequestId && (!this.HasRequestId || this.RequestId.Equals(queueUpdateNotification.RequestId)) && this.HasUpdateInfo == queueUpdateNotification.HasUpdateInfo && (!this.HasUpdateInfo || this.UpdateInfo.Equals(queueUpdateNotification.UpdateInfo)) && this.HasMatchmaking == queueUpdateNotification.HasMatchmaking && (!this.HasMatchmaking || this.Matchmaking.Equals(queueUpdateNotification.Matchmaking)) && this.HasMatchmakerResult == queueUpdateNotification.HasMatchmakerResult && (!this.HasMatchmakerResult || this.MatchmakerResult.Equals(queueUpdateNotification.MatchmakerResult));
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000BF27C File Offset: 0x000BD47C
		public static QueueUpdateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<QueueUpdateNotification>(bs, 0, -1);
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000BF286 File Offset: 0x000BD486
		public void Deserialize(Stream stream)
		{
			QueueUpdateNotification.Deserialize(stream, this);
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000BF290 File Offset: 0x000BD490
		public static QueueUpdateNotification Deserialize(Stream stream, QueueUpdateNotification instance)
		{
			return QueueUpdateNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x000BF29C File Offset: 0x000BD49C
		public static QueueUpdateNotification DeserializeLengthDelimited(Stream stream)
		{
			QueueUpdateNotification queueUpdateNotification = new QueueUpdateNotification();
			QueueUpdateNotification.DeserializeLengthDelimited(stream, queueUpdateNotification);
			return queueUpdateNotification;
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x000BF2B8 File Offset: 0x000BD4B8
		public static QueueUpdateNotification DeserializeLengthDelimited(Stream stream, QueueUpdateNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return QueueUpdateNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x000BF2E0 File Offset: 0x000BD4E0
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
				else
				{
					if (num <= 18)
					{
						if (num != 10)
						{
							if (num == 18)
							{
								if (instance.UpdateInfo == null)
								{
									instance.UpdateInfo = QueueUpdate.DeserializeLengthDelimited(stream);
									continue;
								}
								QueueUpdate.DeserializeLengthDelimited(stream, instance.UpdateInfo);
								continue;
							}
						}
						else
						{
							if (instance.RequestId == null)
							{
								instance.RequestId = FindGameRequestId.DeserializeLengthDelimited(stream);
								continue;
							}
							FindGameRequestId.DeserializeLengthDelimited(stream, instance.RequestId);
							continue;
						}
					}
					else
					{
						if (num == 24)
						{
							instance.Matchmaking = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 32)
						{
							instance.MatchmakerResult = ProtocolParser.ReadUInt32(stream);
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

		// Token: 0x06003B20 RID: 15136 RVA: 0x000BF3EB File Offset: 0x000BD5EB
		public void Serialize(Stream stream)
		{
			QueueUpdateNotification.Serialize(stream, this);
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000BF3F4 File Offset: 0x000BD5F4
		public static void Serialize(Stream stream, QueueUpdateNotification instance)
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
			if (instance.HasMatchmaking)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.Matchmaking);
			}
			if (instance.HasMatchmakerResult)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.MatchmakerResult);
			}
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000BF494 File Offset: 0x000BD694
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
			if (this.HasMatchmaking)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasMatchmakerResult)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.MatchmakerResult);
			}
			return num;
		}

		// Token: 0x0400155C RID: 5468
		public bool HasRequestId;

		// Token: 0x0400155D RID: 5469
		private FindGameRequestId _RequestId;

		// Token: 0x0400155E RID: 5470
		public bool HasUpdateInfo;

		// Token: 0x0400155F RID: 5471
		private QueueUpdate _UpdateInfo;

		// Token: 0x04001560 RID: 5472
		public bool HasMatchmaking;

		// Token: 0x04001561 RID: 5473
		private bool _Matchmaking;

		// Token: 0x04001562 RID: 5474
		public bool HasMatchmakerResult;

		// Token: 0x04001563 RID: 5475
		private uint _MatchmakerResult;
	}
}
