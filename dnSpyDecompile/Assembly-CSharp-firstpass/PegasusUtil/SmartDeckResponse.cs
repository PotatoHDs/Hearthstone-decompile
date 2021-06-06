using System;
using System.IO;
using HSCachedDeckCompletion;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000F6 RID: 246
	public class SmartDeckResponse : IProtoBuf
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0003A0E3 File Offset: 0x000382E3
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x0003A0EB File Offset: 0x000382EB
		public ErrorCode ErrorCode
		{
			get
			{
				return this._ErrorCode;
			}
			set
			{
				this._ErrorCode = value;
				this.HasErrorCode = true;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0003A0FB File Offset: 0x000382FB
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x0003A103 File Offset: 0x00038303
		public HSCachedDeckCompletionResponse ResponseMessage
		{
			get
			{
				return this._ResponseMessage;
			}
			set
			{
				this._ResponseMessage = value;
				this.HasResponseMessage = (value != null);
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0003A118 File Offset: 0x00038318
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasErrorCode)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			if (this.HasResponseMessage)
			{
				num ^= this.ResponseMessage.GetHashCode();
			}
			return num;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0003A168 File Offset: 0x00038368
		public override bool Equals(object obj)
		{
			SmartDeckResponse smartDeckResponse = obj as SmartDeckResponse;
			return smartDeckResponse != null && this.HasErrorCode == smartDeckResponse.HasErrorCode && (!this.HasErrorCode || this.ErrorCode.Equals(smartDeckResponse.ErrorCode)) && this.HasResponseMessage == smartDeckResponse.HasResponseMessage && (!this.HasResponseMessage || this.ResponseMessage.Equals(smartDeckResponse.ResponseMessage));
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003A1E6 File Offset: 0x000383E6
		public void Deserialize(Stream stream)
		{
			SmartDeckResponse.Deserialize(stream, this);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0003A1F0 File Offset: 0x000383F0
		public static SmartDeckResponse Deserialize(Stream stream, SmartDeckResponse instance)
		{
			return SmartDeckResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0003A1FC File Offset: 0x000383FC
		public static SmartDeckResponse DeserializeLengthDelimited(Stream stream)
		{
			SmartDeckResponse smartDeckResponse = new SmartDeckResponse();
			SmartDeckResponse.DeserializeLengthDelimited(stream, smartDeckResponse);
			return smartDeckResponse;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003A218 File Offset: 0x00038418
		public static SmartDeckResponse DeserializeLengthDelimited(Stream stream, SmartDeckResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SmartDeckResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003A240 File Offset: 0x00038440
		public static SmartDeckResponse Deserialize(Stream stream, SmartDeckResponse instance, long limit)
		{
			instance.ErrorCode = ErrorCode.ERROR_OK;
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
					else if (instance.ResponseMessage == null)
					{
						instance.ResponseMessage = HSCachedDeckCompletionResponse.DeserializeLengthDelimited(stream);
					}
					else
					{
						HSCachedDeckCompletionResponse.DeserializeLengthDelimited(stream, instance.ResponseMessage);
					}
				}
				else
				{
					instance.ErrorCode = (ErrorCode)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0003A2F9 File Offset: 0x000384F9
		public void Serialize(Stream stream)
		{
			SmartDeckResponse.Serialize(stream, this);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0003A304 File Offset: 0x00038504
		public static void Serialize(Stream stream, SmartDeckResponse instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ErrorCode));
			}
			if (instance.HasResponseMessage)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ResponseMessage.GetSerializedSize());
				HSCachedDeckCompletionResponse.Serialize(stream, instance.ResponseMessage);
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0003A35C File Offset: 0x0003855C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasErrorCode)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ErrorCode));
			}
			if (this.HasResponseMessage)
			{
				num += 1U;
				uint serializedSize = this.ResponseMessage.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04000525 RID: 1317
		public bool HasErrorCode;

		// Token: 0x04000526 RID: 1318
		private ErrorCode _ErrorCode;

		// Token: 0x04000527 RID: 1319
		public bool HasResponseMessage;

		// Token: 0x04000528 RID: 1320
		private HSCachedDeckCompletionResponse _ResponseMessage;

		// Token: 0x020005FA RID: 1530
		public enum PacketID
		{
			// Token: 0x0400202C RID: 8236
			ID = 370
		}
	}
}
