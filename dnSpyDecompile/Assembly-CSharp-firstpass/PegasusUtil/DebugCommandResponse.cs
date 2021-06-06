using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x020000C3 RID: 195
	public class DebugCommandResponse : IProtoBuf
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00031B8B File Offset: 0x0002FD8B
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x00031B93 File Offset: 0x0002FD93
		public bool Success { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00031B9C File Offset: 0x0002FD9C
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x00031BA4 File Offset: 0x0002FDA4
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

		// Token: 0x06000D6B RID: 3435 RVA: 0x00031BB8 File Offset: 0x0002FDB8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.Success.GetHashCode();
			if (this.HasResponse)
			{
				num ^= this.Response.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00031BFC File Offset: 0x0002FDFC
		public override bool Equals(object obj)
		{
			DebugCommandResponse debugCommandResponse = obj as DebugCommandResponse;
			return debugCommandResponse != null && this.Success.Equals(debugCommandResponse.Success) && this.HasResponse == debugCommandResponse.HasResponse && (!this.HasResponse || this.Response.Equals(debugCommandResponse.Response));
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00031C59 File Offset: 0x0002FE59
		public void Deserialize(Stream stream)
		{
			DebugCommandResponse.Deserialize(stream, this);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00031C63 File Offset: 0x0002FE63
		public static DebugCommandResponse Deserialize(Stream stream, DebugCommandResponse instance)
		{
			return DebugCommandResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00031C70 File Offset: 0x0002FE70
		public static DebugCommandResponse DeserializeLengthDelimited(Stream stream)
		{
			DebugCommandResponse debugCommandResponse = new DebugCommandResponse();
			DebugCommandResponse.DeserializeLengthDelimited(stream, debugCommandResponse);
			return debugCommandResponse;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00031C8C File Offset: 0x0002FE8C
		public static DebugCommandResponse DeserializeLengthDelimited(Stream stream, DebugCommandResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return DebugCommandResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00031CB4 File Offset: 0x0002FEB4
		public static DebugCommandResponse Deserialize(Stream stream, DebugCommandResponse instance, long limit)
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
					if (num != 18)
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
						instance.Response = ProtocolParser.ReadString(stream);
					}
				}
				else
				{
					instance.Success = ProtocolParser.ReadBool(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00031D4B File Offset: 0x0002FF4B
		public void Serialize(Stream stream)
		{
			DebugCommandResponse.Serialize(stream, this);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00031D54 File Offset: 0x0002FF54
		public static void Serialize(Stream stream, DebugCommandResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.Success);
			if (instance.HasResponse)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Response));
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00031D90 File Offset: 0x0002FF90
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += 1U;
			if (this.HasResponse)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Response);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			return num + 1U;
		}

		// Token: 0x04000498 RID: 1176
		public bool HasResponse;

		// Token: 0x04000499 RID: 1177
		private string _Response;

		// Token: 0x020005D2 RID: 1490
		public enum PacketID
		{
			// Token: 0x04001FBD RID: 8125
			ID = 324
		}
	}
}
