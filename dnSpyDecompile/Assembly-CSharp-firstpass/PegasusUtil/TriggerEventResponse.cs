using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x020000B9 RID: 185
	public class TriggerEventResponse : IProtoBuf
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0002FBAD File Offset: 0x0002DDAD
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0002FBB5 File Offset: 0x0002DDB5
		public int EventId { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0002FBBE File Offset: 0x0002DDBE
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0002FBC6 File Offset: 0x0002DDC6
		public bool Success { get; set; }

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002FBD0 File Offset: 0x0002DDD0
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.EventId.GetHashCode() ^ this.Success.GetHashCode();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002FC08 File Offset: 0x0002DE08
		public override bool Equals(object obj)
		{
			TriggerEventResponse triggerEventResponse = obj as TriggerEventResponse;
			return triggerEventResponse != null && this.EventId.Equals(triggerEventResponse.EventId) && this.Success.Equals(triggerEventResponse.Success);
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002FC52 File Offset: 0x0002DE52
		public void Deserialize(Stream stream)
		{
			TriggerEventResponse.Deserialize(stream, this);
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002FC5C File Offset: 0x0002DE5C
		public static TriggerEventResponse Deserialize(Stream stream, TriggerEventResponse instance)
		{
			return TriggerEventResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x0002FC68 File Offset: 0x0002DE68
		public static TriggerEventResponse DeserializeLengthDelimited(Stream stream)
		{
			TriggerEventResponse triggerEventResponse = new TriggerEventResponse();
			TriggerEventResponse.DeserializeLengthDelimited(stream, triggerEventResponse);
			return triggerEventResponse;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002FC84 File Offset: 0x0002DE84
		public static TriggerEventResponse DeserializeLengthDelimited(Stream stream, TriggerEventResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TriggerEventResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0002FCAC File Offset: 0x0002DEAC
		public static TriggerEventResponse Deserialize(Stream stream, TriggerEventResponse instance, long limit)
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
						Key key = ProtocolParser.ReadKey((byte)num, stream);
						if (key.Field == 0U)
						{
							throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
						}
						ProtocolParser.SkipKey(stream, key);
					}
					else
					{
						instance.Success = ProtocolParser.ReadBool(stream);
					}
				}
				else
				{
					instance.EventId = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0002FD44 File Offset: 0x0002DF44
		public void Serialize(Stream stream)
		{
			TriggerEventResponse.Serialize(stream, this);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0002FD4D File Offset: 0x0002DF4D
		public static void Serialize(Stream stream, TriggerEventResponse instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.EventId));
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.Success);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002FD77 File Offset: 0x0002DF77
		public uint GetSerializedSize()
		{
			return 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.EventId)) + 1U + 2U;
		}

		// Token: 0x020005C6 RID: 1478
		public enum PacketID
		{
			// Token: 0x04001FA0 RID: 8096
			ID = 299
		}
	}
}
