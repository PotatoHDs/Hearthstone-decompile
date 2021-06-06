using System;
using System.IO;
using PegasusShared;

namespace PegasusUtil
{
	// Token: 0x020000EF RID: 239
	public class GameToConnectNotification : IProtoBuf
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x000392EF File Offset: 0x000374EF
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x000392F7 File Offset: 0x000374F7
		public GameConnectionInfo Info { get; set; }

		// Token: 0x06001005 RID: 4101 RVA: 0x00039300 File Offset: 0x00037500
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.Info.GetHashCode();
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0003931C File Offset: 0x0003751C
		public override bool Equals(object obj)
		{
			GameToConnectNotification gameToConnectNotification = obj as GameToConnectNotification;
			return gameToConnectNotification != null && this.Info.Equals(gameToConnectNotification.Info);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003934B File Offset: 0x0003754B
		public void Deserialize(Stream stream)
		{
			GameToConnectNotification.Deserialize(stream, this);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00039355 File Offset: 0x00037555
		public static GameToConnectNotification Deserialize(Stream stream, GameToConnectNotification instance)
		{
			return GameToConnectNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00039360 File Offset: 0x00037560
		public static GameToConnectNotification DeserializeLengthDelimited(Stream stream)
		{
			GameToConnectNotification gameToConnectNotification = new GameToConnectNotification();
			GameToConnectNotification.DeserializeLengthDelimited(stream, gameToConnectNotification);
			return gameToConnectNotification;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003937C File Offset: 0x0003757C
		public static GameToConnectNotification DeserializeLengthDelimited(Stream stream, GameToConnectNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GameToConnectNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x000393A4 File Offset: 0x000375A4
		public static GameToConnectNotification Deserialize(Stream stream, GameToConnectNotification instance, long limit)
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
					if (instance.Info == null)
					{
						instance.Info = GameConnectionInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameConnectionInfo.DeserializeLengthDelimited(stream, instance.Info);
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

		// Token: 0x0600100C RID: 4108 RVA: 0x0003943E File Offset: 0x0003763E
		public void Serialize(Stream stream)
		{
			GameToConnectNotification.Serialize(stream, this);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x00039447 File Offset: 0x00037647
		public static void Serialize(Stream stream, GameToConnectNotification instance)
		{
			if (instance.Info == null)
			{
				throw new ArgumentNullException("Info", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Info.GetSerializedSize());
			GameConnectionInfo.Serialize(stream, instance.Info);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x00039488 File Offset: 0x00037688
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.Info.GetSerializedSize();
			return num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize)) + 1U;
		}

		// Token: 0x020005F3 RID: 1523
		public enum PacketID
		{
			// Token: 0x0400201A RID: 8218
			ID = 363
		}
	}
}
