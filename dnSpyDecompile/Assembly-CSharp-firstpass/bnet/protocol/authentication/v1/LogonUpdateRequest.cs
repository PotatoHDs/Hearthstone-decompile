using System;
using System.IO;

namespace bnet.protocol.authentication.v1
{
	// Token: 0x020004F0 RID: 1264
	public class LogonUpdateRequest : IProtoBuf
	{
		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x060059BC RID: 22972 RVA: 0x00112926 File Offset: 0x00110B26
		// (set) Token: 0x060059BD RID: 22973 RVA: 0x0011292E File Offset: 0x00110B2E
		public uint ErrorCode { get; set; }

		// Token: 0x060059BE RID: 22974 RVA: 0x00112937 File Offset: 0x00110B37
		public void SetErrorCode(uint val)
		{
			this.ErrorCode = val;
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x00112940 File Offset: 0x00110B40
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.ErrorCode.GetHashCode();
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x00112968 File Offset: 0x00110B68
		public override bool Equals(object obj)
		{
			LogonUpdateRequest logonUpdateRequest = obj as LogonUpdateRequest;
			return logonUpdateRequest != null && this.ErrorCode.Equals(logonUpdateRequest.ErrorCode);
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060059C1 RID: 22977 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x0011299A File Offset: 0x00110B9A
		public static LogonUpdateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<LogonUpdateRequest>(bs, 0, -1);
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x001129A4 File Offset: 0x00110BA4
		public void Deserialize(Stream stream)
		{
			LogonUpdateRequest.Deserialize(stream, this);
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x001129AE File Offset: 0x00110BAE
		public static LogonUpdateRequest Deserialize(Stream stream, LogonUpdateRequest instance)
		{
			return LogonUpdateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060059C5 RID: 22981 RVA: 0x001129BC File Offset: 0x00110BBC
		public static LogonUpdateRequest DeserializeLengthDelimited(Stream stream)
		{
			LogonUpdateRequest logonUpdateRequest = new LogonUpdateRequest();
			LogonUpdateRequest.DeserializeLengthDelimited(stream, logonUpdateRequest);
			return logonUpdateRequest;
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x001129D8 File Offset: 0x00110BD8
		public static LogonUpdateRequest DeserializeLengthDelimited(Stream stream, LogonUpdateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LogonUpdateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x00112A00 File Offset: 0x00110C00
		public static LogonUpdateRequest Deserialize(Stream stream, LogonUpdateRequest instance, long limit)
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
				else if (num == 8)
				{
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
				}
				else
				{
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

		// Token: 0x060059C8 RID: 22984 RVA: 0x00112A7F File Offset: 0x00110C7F
		public void Serialize(Stream stream)
		{
			LogonUpdateRequest.Serialize(stream, this);
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x00112A88 File Offset: 0x00110C88
		public static void Serialize(Stream stream, LogonUpdateRequest instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x00112A9D File Offset: 0x00110C9D
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt32(this.ErrorCode) + 1U;
		}
	}
}
