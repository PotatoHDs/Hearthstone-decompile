using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F1 RID: 1265
	public class LogonQueueUpdateRequest : IProtoBuf
	{
		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060059CC RID: 22988 RVA: 0x00112AAE File Offset: 0x00110CAE
		// (set) Token: 0x060059CD RID: 22989 RVA: 0x00112AB6 File Offset: 0x00110CB6
		public uint Position { get; set; }

		// Token: 0x060059CE RID: 22990 RVA: 0x00112ABF File Offset: 0x00110CBF
		public void SetPosition(uint val)
		{
			this.Position = val;
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060059CF RID: 22991 RVA: 0x00112AC8 File Offset: 0x00110CC8
		// (set) Token: 0x060059D0 RID: 22992 RVA: 0x00112AD0 File Offset: 0x00110CD0
		public ulong EstimatedTime { get; set; }

		// Token: 0x060059D1 RID: 22993 RVA: 0x00112AD9 File Offset: 0x00110CD9
		public void SetEstimatedTime(ulong val)
		{
			this.EstimatedTime = val;
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060059D2 RID: 22994 RVA: 0x00112AE2 File Offset: 0x00110CE2
		// (set) Token: 0x060059D3 RID: 22995 RVA: 0x00112AEA File Offset: 0x00110CEA
		public ulong EtaDeviationInSec { get; set; }

		// Token: 0x060059D4 RID: 22996 RVA: 0x00112AF3 File Offset: 0x00110CF3
		public void SetEtaDeviationInSec(ulong val)
		{
			this.EtaDeviationInSec = val;
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x00112AFC File Offset: 0x00110CFC
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Position.GetHashCode() ^ this.EstimatedTime.GetHashCode() ^ this.EtaDeviationInSec.GetHashCode();
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x00112B44 File Offset: 0x00110D44
		public override bool Equals(object obj)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = obj as LogonQueueUpdateRequest;
			return logonQueueUpdateRequest != null && this.Position.Equals(logonQueueUpdateRequest.Position) && this.EstimatedTime.Equals(logonQueueUpdateRequest.EstimatedTime) && this.EtaDeviationInSec.Equals(logonQueueUpdateRequest.EtaDeviationInSec);
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060059D7 RID: 22999 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x00112BA6 File Offset: 0x00110DA6
		public static LogonQueueUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonQueueUpdateRequest>(bs, 0, -1);
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x00112BB0 File Offset: 0x00110DB0
		public void Deserialize(Stream stream)
		{
			LogonQueueUpdateRequest.Deserialize(stream, this);
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x00112BBA File Offset: 0x00110DBA
		public static LogonQueueUpdateRequest Deserialize(Stream stream, LogonQueueUpdateRequest instance)
		{
			return LogonQueueUpdateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x00112BC8 File Offset: 0x00110DC8
		public static LogonQueueUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonQueueUpdateRequest logonQueueUpdateRequest = new LogonQueueUpdateRequest();
			LogonQueueUpdateRequest.DeserializeLengthDelimited(stream, logonQueueUpdateRequest);
			return logonQueueUpdateRequest;
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x00112BE4 File Offset: 0x00110DE4
		public static LogonQueueUpdateRequest DeserializeLengthDelimited(Stream stream, LogonQueueUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogonQueueUpdateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x00112C0C File Offset: 0x00110E0C
		public static LogonQueueUpdateRequest Deserialize(Stream stream, LogonQueueUpdateRequest instance, long limit)
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
				else if (num != 8)
				{
					if (num != 16)
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
							instance.EtaDeviationInSec = ProtocolParser.ReadUInt64(stream);
						}
					}
					else
					{
						instance.EstimatedTime = ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Position = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x00112CB9 File Offset: 0x00110EB9
		public void Serialize(Stream stream)
		{
			LogonQueueUpdateRequest.Serialize(stream, this);
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x00112CC2 File Offset: 0x00110EC2
		public static void Serialize(Stream stream, LogonQueueUpdateRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.Position);
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.EstimatedTime);
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.EtaDeviationInSec);
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x00112CFF File Offset: 0x00110EFF
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt32(this.Position) + ProtocolParser.SizeOfUInt64(this.EstimatedTime) + ProtocolParser.SizeOfUInt64(this.EtaDeviationInSec) + 3U;
		}
	}
}
