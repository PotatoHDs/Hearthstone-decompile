using System;
using System.IO;
using System.Text;

namespace BobNetProto
{
	// Token: 0x020001E0 RID: 480
	public class DebugConsoleResponse : IProtoBuf
	{
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x0006AA0B File Offset: 0x00068C0B
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x0006AA13 File Offset: 0x00068C13
		public string Response
		{
			get
			{
				return this._Response;
			}
			set
			{
				this._Response = value;
				this.HasResponse = (value != null);
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x0006AA26 File Offset: 0x00068C26
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x0006AA2E File Offset: 0x00068C2E
		public DebugConsoleResponse.ResponseType ResponseType_
		{
			get
			{
				return this._ResponseType_;
			}
			set
			{
				this._ResponseType_ = value;
				this.HasResponseType_ = true;
			}
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0006AA40 File Offset: 0x00068C40
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasResponse)
			{
				num ^= this.Response.GetHashCode();
			}
			if (this.HasResponseType_)
			{
				num ^= this.ResponseType_.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0006AA90 File Offset: 0x00068C90
		public override bool Equals(object obj)
		{
			DebugConsoleResponse debugConsoleResponse = obj as DebugConsoleResponse;
			return debugConsoleResponse != null && this.HasResponse == debugConsoleResponse.HasResponse && (!this.HasResponse || this.Response.Equals(debugConsoleResponse.Response)) && this.HasResponseType_ == debugConsoleResponse.HasResponseType_ && (!this.HasResponseType_ || this.ResponseType_.Equals(debugConsoleResponse.ResponseType_));
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0006AB0E File Offset: 0x00068D0E
		public void Deserialize(Stream stream)
		{
			DebugConsoleResponse.Deserialize(stream, this);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x0006AB18 File Offset: 0x00068D18
		public static DebugConsoleResponse Deserialize(Stream stream, DebugConsoleResponse instance)
		{
			return DebugConsoleResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0006AB24 File Offset: 0x00068D24
		public static DebugConsoleResponse DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleResponse debugConsoleResponse = new DebugConsoleResponse();
			DebugConsoleResponse.DeserializeLengthDelimited(stream, debugConsoleResponse);
			return debugConsoleResponse;
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0006AB40 File Offset: 0x00068D40
		public static DebugConsoleResponse DeserializeLengthDelimited(Stream stream, DebugConsoleResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugConsoleResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x0006AB68 File Offset: 0x00068D68
		public static DebugConsoleResponse Deserialize(Stream stream, DebugConsoleResponse instance, long limit)
		{
			instance.ResponseType_ = DebugConsoleResponse.ResponseType.CONSOLE_OUTPUT;
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
					if (num != 16)
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
						instance.ResponseType_ = (DebugConsoleResponse.ResponseType)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Response = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0006AC08 File Offset: 0x00068E08
		public void Serialize(Stream stream)
		{
			DebugConsoleResponse.Serialize(stream, this);
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0006AC14 File Offset: 0x00068E14
		public static void Serialize(Stream stream, DebugConsoleResponse instance)
		{
			if (instance.HasResponse)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Response));
			}
			if (instance.HasResponseType_)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ResponseType_));
			}
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0006AC64 File Offset: 0x00068E64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasResponse)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Response);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasResponseType_)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ResponseType_));
			}
			return num;
		}

		// Token: 0x04000AEB RID: 2795
		public bool HasResponse;

		// Token: 0x04000AEC RID: 2796
		private string _Response;

		// Token: 0x04000AED RID: 2797
		public bool HasResponseType_;

		// Token: 0x04000AEE RID: 2798
		private DebugConsoleResponse.ResponseType _ResponseType_;

		// Token: 0x0200066B RID: 1643
		public enum PacketID
		{
			// Token: 0x04002170 RID: 8560
			ID = 124
		}

		// Token: 0x0200066C RID: 1644
		public enum ResponseType
		{
			// Token: 0x04002172 RID: 8562
			CONSOLE_OUTPUT,
			// Token: 0x04002173 RID: 8563
			LOG_MESSAGE
		}
	}
}
