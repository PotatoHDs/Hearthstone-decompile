using System;
using System.IO;
using System.Text;

namespace PegasusUtil
{
	// Token: 0x020000EE RID: 238
	public class LocateCheatServerResponse : IProtoBuf
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x000390C6 File Offset: 0x000372C6
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x000390CE File Offset: 0x000372CE
		public string Address { get; set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x000390D7 File Offset: 0x000372D7
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x000390DF File Offset: 0x000372DF
		public int Port { get; set; }

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000390E8 File Offset: 0x000372E8
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Address.GetHashCode() ^ this.Port.GetHashCode();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003911C File Offset: 0x0003731C
		public override bool Equals(object obj)
		{
			LocateCheatServerResponse locateCheatServerResponse = obj as LocateCheatServerResponse;
			return locateCheatServerResponse != null && this.Address.Equals(locateCheatServerResponse.Address) && this.Port.Equals(locateCheatServerResponse.Port);
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00039163 File Offset: 0x00037363
		public void Deserialize(Stream stream)
		{
			LocateCheatServerResponse.Deserialize(stream, this);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0003916D File Offset: 0x0003736D
		public static LocateCheatServerResponse Deserialize(Stream stream, LocateCheatServerResponse instance)
		{
			return LocateCheatServerResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x00039178 File Offset: 0x00037378
		public static LocateCheatServerResponse DeserializeLengthDelimited(Stream stream)
		{
			LocateCheatServerResponse locateCheatServerResponse = new LocateCheatServerResponse();
			LocateCheatServerResponse.DeserializeLengthDelimited(stream, locateCheatServerResponse);
			return locateCheatServerResponse;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x00039194 File Offset: 0x00037394
		public static LocateCheatServerResponse DeserializeLengthDelimited(Stream stream, LocateCheatServerResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return LocateCheatServerResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000391BC File Offset: 0x000373BC
		public static LocateCheatServerResponse Deserialize(Stream stream, LocateCheatServerResponse instance, long limit)
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
						instance.Port = (int)ProtocolParser.ReadUInt64(stream);
					}
				}
				else
				{
					instance.Address = ProtocolParser.ReadString(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00039255 File Offset: 0x00037455
		public void Serialize(Stream stream)
		{
			LocateCheatServerResponse.Serialize(stream, this);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00039260 File Offset: 0x00037460
		public static void Serialize(Stream stream, LocateCheatServerResponse instance)
		{
			if (instance.Address == null)
			{
				throw new ArgumentNullException("Address", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Address));
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Port));
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000392B8 File Offset: 0x000374B8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Address);
			return num + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt64((ulong)((long)this.Port)) + 2U;
		}

		// Token: 0x020005F2 RID: 1522
		public enum PacketID
		{
			// Token: 0x04002018 RID: 8216
			ID = 362
		}
	}
}
