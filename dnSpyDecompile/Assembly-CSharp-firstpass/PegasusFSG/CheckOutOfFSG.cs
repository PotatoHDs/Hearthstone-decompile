using System;
using System.IO;
using PegasusShared;

namespace PegasusFSG
{
	// Token: 0x0200001A RID: 26
	public class CheckOutOfFSG : IProtoBuf
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000056B7 File Offset: 0x000038B7
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000056BF File Offset: 0x000038BF
		public long FsgId { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000056C8 File Offset: 0x000038C8
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000056D0 File Offset: 0x000038D0
		public Platform Platform
		{
			get
			{
				return this._Platform;
			}
			set
			{
				this._Platform = value;
				this.HasPlatform = (value != null);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000056E4 File Offset: 0x000038E4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			num ^= this.FsgId.GetHashCode();
			if (this.HasPlatform)
			{
				num ^= this.Platform.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005728 File Offset: 0x00003928
		public override bool Equals(object obj)
		{
			CheckOutOfFSG checkOutOfFSG = obj as CheckOutOfFSG;
			return checkOutOfFSG != null && this.FsgId.Equals(checkOutOfFSG.FsgId) && this.HasPlatform == checkOutOfFSG.HasPlatform && (!this.HasPlatform || this.Platform.Equals(checkOutOfFSG.Platform));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005785 File Offset: 0x00003985
		public void Deserialize(Stream stream)
		{
			CheckOutOfFSG.Deserialize(stream, this);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000578F File Offset: 0x0000398F
		public static CheckOutOfFSG Deserialize(Stream stream, CheckOutOfFSG instance)
		{
			return CheckOutOfFSG.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000579C File Offset: 0x0000399C
		public static CheckOutOfFSG DeserializeLengthDelimited(Stream stream)
		{
			CheckOutOfFSG checkOutOfFSG = new CheckOutOfFSG();
			CheckOutOfFSG.DeserializeLengthDelimited(stream, checkOutOfFSG);
			return checkOutOfFSG;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000057B8 File Offset: 0x000039B8
		public static CheckOutOfFSG DeserializeLengthDelimited(Stream stream, CheckOutOfFSG instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CheckOutOfFSG.Deserialize(stream, instance, num);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000057E0 File Offset: 0x000039E0
		public static CheckOutOfFSG Deserialize(Stream stream, CheckOutOfFSG instance, long limit)
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
					else if (instance.Platform == null)
					{
						instance.Platform = Platform.DeserializeLengthDelimited(stream);
					}
					else
					{
						Platform.DeserializeLengthDelimited(stream, instance.Platform);
					}
				}
				else
				{
					instance.FsgId = (long)ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005891 File Offset: 0x00003A91
		public void Serialize(Stream stream)
		{
			CheckOutOfFSG.Serialize(stream, this);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000589C File Offset: 0x00003A9C
		public static void Serialize(Stream stream, CheckOutOfFSG instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.FsgId);
			if (instance.HasPlatform)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Platform.GetSerializedSize());
				Platform.Serialize(stream, instance.Platform);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000058EC File Offset: 0x00003AEC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			num += ProtocolParser.SizeOfUInt64((ulong)this.FsgId);
			if (this.HasPlatform)
			{
				num += 1U;
				uint serializedSize = this.Platform.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num + 1U;
		}

		// Token: 0x0400004D RID: 77
		public bool HasPlatform;

		// Token: 0x0400004E RID: 78
		private Platform _Platform;

		// Token: 0x02000550 RID: 1360
		public enum PacketID
		{
			// Token: 0x04001E11 RID: 7697
			ID = 503,
			// Token: 0x04001E12 RID: 7698
			System = 3
		}
	}
}
