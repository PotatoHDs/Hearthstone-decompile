using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	// Token: 0x020001A0 RID: 416
	public class SharedPlayerInfo : IProtoBuf
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0005CA1C File Offset: 0x0005AC1C
		// (set) Token: 0x06001A28 RID: 6696 RVA: 0x0005CA24 File Offset: 0x0005AC24
		public int Id { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0005CA2D File Offset: 0x0005AC2D
		// (set) Token: 0x06001A2A RID: 6698 RVA: 0x0005CA35 File Offset: 0x0005AC35
		public BnetId GameAccountId { get; set; }

		// Token: 0x06001A2B RID: 6699 RVA: 0x0005CA40 File Offset: 0x0005AC40
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Id.GetHashCode() ^ this.GameAccountId.GetHashCode();
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0005CA74 File Offset: 0x0005AC74
		public override bool Equals(object obj)
		{
			SharedPlayerInfo sharedPlayerInfo = obj as SharedPlayerInfo;
			return sharedPlayerInfo != null && this.Id.Equals(sharedPlayerInfo.Id) && this.GameAccountId.Equals(sharedPlayerInfo.GameAccountId);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0005CABB File Offset: 0x0005ACBB
		public void Deserialize(Stream stream)
		{
			SharedPlayerInfo.Deserialize(stream, this);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0005CAC5 File Offset: 0x0005ACC5
		public static SharedPlayerInfo Deserialize(Stream stream, SharedPlayerInfo instance)
		{
			return SharedPlayerInfo.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0005CAD0 File Offset: 0x0005ACD0
		public static SharedPlayerInfo DeserializeLengthDelimited(Stream stream)
		{
			SharedPlayerInfo sharedPlayerInfo = new SharedPlayerInfo();
			SharedPlayerInfo.DeserializeLengthDelimited(stream, sharedPlayerInfo);
			return sharedPlayerInfo;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0005CAEC File Offset: 0x0005ACEC
		public static SharedPlayerInfo DeserializeLengthDelimited(Stream stream, SharedPlayerInfo instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SharedPlayerInfo.Deserialize(stream, instance, num);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0005CB14 File Offset: 0x0005AD14
		public static SharedPlayerInfo Deserialize(Stream stream, SharedPlayerInfo instance, long limit)
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
					else if (instance.GameAccountId == null)
					{
						instance.GameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.GameAccountId);
					}
				}
				else
				{
					instance.Id = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0005CBC6 File Offset: 0x0005ADC6
		public void Serialize(Stream stream)
		{
			SharedPlayerInfo.Serialize(stream, this);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0005CBD0 File Offset: 0x0005ADD0
		public static void Serialize(Stream stream, SharedPlayerInfo instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Id));
			if (instance.GameAccountId == null)
			{
				throw new ArgumentNullException("GameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.GameAccountId);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0005CC30 File Offset: 0x0005AE30
		public uint GetSerializedSize()
		{
			uint num = 0U + ProtocolParser.SizeOfUInt64((ulong)((long)this.Id));
			uint serializedSize = this.GameAccountId.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 2U;
		}
	}
}
