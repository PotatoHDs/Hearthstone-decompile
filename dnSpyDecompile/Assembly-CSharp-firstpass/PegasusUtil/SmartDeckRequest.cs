using System;
using System.IO;
using HSCachedDeckCompletion;

namespace PegasusUtil
{
	// Token: 0x020000F5 RID: 245
	public class SmartDeckRequest : IProtoBuf
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00039EF1 File Offset: 0x000380F1
		// (set) Token: 0x06001058 RID: 4184 RVA: 0x00039EF9 File Offset: 0x000380F9
		public HSCachedDeckCompletionRequest RequestMessage
		{
			get
			{
				return this._RequestMessage;
			}
			set
			{
				this._RequestMessage = value;
				this.HasRequestMessage = (value != null);
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00039F0C File Offset: 0x0003810C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasRequestMessage)
			{
				num ^= this.RequestMessage.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00039F3C File Offset: 0x0003813C
		public override bool Equals(object obj)
		{
			SmartDeckRequest smartDeckRequest = obj as SmartDeckRequest;
			return smartDeckRequest != null && this.HasRequestMessage == smartDeckRequest.HasRequestMessage && (!this.HasRequestMessage || this.RequestMessage.Equals(smartDeckRequest.RequestMessage));
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00039F81 File Offset: 0x00038181
		public void Deserialize(Stream stream)
		{
			SmartDeckRequest.Deserialize(stream, this);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00039F8B File Offset: 0x0003818B
		public static SmartDeckRequest Deserialize(Stream stream, SmartDeckRequest instance)
		{
			return SmartDeckRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00039F98 File Offset: 0x00038198
		public static SmartDeckRequest DeserializeLengthDelimited(Stream stream)
		{
			SmartDeckRequest smartDeckRequest = new SmartDeckRequest();
			SmartDeckRequest.DeserializeLengthDelimited(stream, smartDeckRequest);
			return smartDeckRequest;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00039FB4 File Offset: 0x000381B4
		public static SmartDeckRequest DeserializeLengthDelimited(Stream stream, SmartDeckRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SmartDeckRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00039FDC File Offset: 0x000381DC
		public static SmartDeckRequest Deserialize(Stream stream, SmartDeckRequest instance, long limit)
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
				else if (num == 10)
				{
					if (instance.RequestMessage == null)
					{
						instance.RequestMessage = HSCachedDeckCompletionRequest.DeserializeLengthDelimited(stream);
					}
					else
					{
						HSCachedDeckCompletionRequest.DeserializeLengthDelimited(stream, instance.RequestMessage);
					}
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

		// Token: 0x06001060 RID: 4192 RVA: 0x0003A076 File Offset: 0x00038276
		public void Serialize(Stream stream)
		{
			SmartDeckRequest.Serialize(stream, this);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0003A07F File Offset: 0x0003827F
		public static void Serialize(Stream stream, SmartDeckRequest instance)
		{
			if (instance.HasRequestMessage)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.RequestMessage.GetSerializedSize());
				HSCachedDeckCompletionRequest.Serialize(stream, instance.RequestMessage);
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0003A0B0 File Offset: 0x000382B0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasRequestMessage)
			{
				num += 1U;
				uint serializedSize = this.RequestMessage.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}

		// Token: 0x04000523 RID: 1315
		public bool HasRequestMessage;

		// Token: 0x04000524 RID: 1316
		private HSCachedDeckCompletionRequest _RequestMessage;

		// Token: 0x020005F9 RID: 1529
		public enum PacketID
		{
			// Token: 0x04002029 RID: 8233
			ID = 369,
			// Token: 0x0400202A RID: 8234
			System = 5
		}
	}
}
