using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class InitialClientStateOutOfOrder : IProtoBuf
	{
		public bool HasCountNotificationsAchieve;

		private int _CountNotificationsAchieve;

		public bool HasCountNotificationsNotice;

		private int _CountNotificationsNotice;

		public bool HasCountNotificationsCollection;

		private int _CountNotificationsCollection;

		public bool HasCountNotificationsCurrency;

		private int _CountNotificationsCurrency;

		public bool HasCountNotificationsBooster;

		private int _CountNotificationsBooster;

		public bool HasCountNotificationsHeroxp;

		private int _CountNotificationsHeroxp;

		public bool HasCountNotificationsPlayerRecord;

		private int _CountNotificationsPlayerRecord;

		public bool HasCountNotificationsArenaSession;

		private int _CountNotificationsArenaSession;

		public bool HasCountNotificationsCardBack;

		private int _CountNotificationsCardBack;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public int CountNotificationsAchieve
		{
			get
			{
				return _CountNotificationsAchieve;
			}
			set
			{
				_CountNotificationsAchieve = value;
				HasCountNotificationsAchieve = true;
			}
		}

		public int CountNotificationsNotice
		{
			get
			{
				return _CountNotificationsNotice;
			}
			set
			{
				_CountNotificationsNotice = value;
				HasCountNotificationsNotice = true;
			}
		}

		public int CountNotificationsCollection
		{
			get
			{
				return _CountNotificationsCollection;
			}
			set
			{
				_CountNotificationsCollection = value;
				HasCountNotificationsCollection = true;
			}
		}

		public int CountNotificationsCurrency
		{
			get
			{
				return _CountNotificationsCurrency;
			}
			set
			{
				_CountNotificationsCurrency = value;
				HasCountNotificationsCurrency = true;
			}
		}

		public int CountNotificationsBooster
		{
			get
			{
				return _CountNotificationsBooster;
			}
			set
			{
				_CountNotificationsBooster = value;
				HasCountNotificationsBooster = true;
			}
		}

		public int CountNotificationsHeroxp
		{
			get
			{
				return _CountNotificationsHeroxp;
			}
			set
			{
				_CountNotificationsHeroxp = value;
				HasCountNotificationsHeroxp = true;
			}
		}

		public int CountNotificationsPlayerRecord
		{
			get
			{
				return _CountNotificationsPlayerRecord;
			}
			set
			{
				_CountNotificationsPlayerRecord = value;
				HasCountNotificationsPlayerRecord = true;
			}
		}

		public int CountNotificationsArenaSession
		{
			get
			{
				return _CountNotificationsArenaSession;
			}
			set
			{
				_CountNotificationsArenaSession = value;
				HasCountNotificationsArenaSession = true;
			}
		}

		public int CountNotificationsCardBack
		{
			get
			{
				return _CountNotificationsCardBack;
			}
			set
			{
				_CountNotificationsCardBack = value;
				HasCountNotificationsCardBack = true;
			}
		}

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasCountNotificationsAchieve)
			{
				num ^= CountNotificationsAchieve.GetHashCode();
			}
			if (HasCountNotificationsNotice)
			{
				num ^= CountNotificationsNotice.GetHashCode();
			}
			if (HasCountNotificationsCollection)
			{
				num ^= CountNotificationsCollection.GetHashCode();
			}
			if (HasCountNotificationsCurrency)
			{
				num ^= CountNotificationsCurrency.GetHashCode();
			}
			if (HasCountNotificationsBooster)
			{
				num ^= CountNotificationsBooster.GetHashCode();
			}
			if (HasCountNotificationsHeroxp)
			{
				num ^= CountNotificationsHeroxp.GetHashCode();
			}
			if (HasCountNotificationsPlayerRecord)
			{
				num ^= CountNotificationsPlayerRecord.GetHashCode();
			}
			if (HasCountNotificationsArenaSession)
			{
				num ^= CountNotificationsArenaSession.GetHashCode();
			}
			if (HasCountNotificationsCardBack)
			{
				num ^= CountNotificationsCardBack.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			InitialClientStateOutOfOrder initialClientStateOutOfOrder = obj as InitialClientStateOutOfOrder;
			if (initialClientStateOutOfOrder == null)
			{
				return false;
			}
			if (HasCountNotificationsAchieve != initialClientStateOutOfOrder.HasCountNotificationsAchieve || (HasCountNotificationsAchieve && !CountNotificationsAchieve.Equals(initialClientStateOutOfOrder.CountNotificationsAchieve)))
			{
				return false;
			}
			if (HasCountNotificationsNotice != initialClientStateOutOfOrder.HasCountNotificationsNotice || (HasCountNotificationsNotice && !CountNotificationsNotice.Equals(initialClientStateOutOfOrder.CountNotificationsNotice)))
			{
				return false;
			}
			if (HasCountNotificationsCollection != initialClientStateOutOfOrder.HasCountNotificationsCollection || (HasCountNotificationsCollection && !CountNotificationsCollection.Equals(initialClientStateOutOfOrder.CountNotificationsCollection)))
			{
				return false;
			}
			if (HasCountNotificationsCurrency != initialClientStateOutOfOrder.HasCountNotificationsCurrency || (HasCountNotificationsCurrency && !CountNotificationsCurrency.Equals(initialClientStateOutOfOrder.CountNotificationsCurrency)))
			{
				return false;
			}
			if (HasCountNotificationsBooster != initialClientStateOutOfOrder.HasCountNotificationsBooster || (HasCountNotificationsBooster && !CountNotificationsBooster.Equals(initialClientStateOutOfOrder.CountNotificationsBooster)))
			{
				return false;
			}
			if (HasCountNotificationsHeroxp != initialClientStateOutOfOrder.HasCountNotificationsHeroxp || (HasCountNotificationsHeroxp && !CountNotificationsHeroxp.Equals(initialClientStateOutOfOrder.CountNotificationsHeroxp)))
			{
				return false;
			}
			if (HasCountNotificationsPlayerRecord != initialClientStateOutOfOrder.HasCountNotificationsPlayerRecord || (HasCountNotificationsPlayerRecord && !CountNotificationsPlayerRecord.Equals(initialClientStateOutOfOrder.CountNotificationsPlayerRecord)))
			{
				return false;
			}
			if (HasCountNotificationsArenaSession != initialClientStateOutOfOrder.HasCountNotificationsArenaSession || (HasCountNotificationsArenaSession && !CountNotificationsArenaSession.Equals(initialClientStateOutOfOrder.CountNotificationsArenaSession)))
			{
				return false;
			}
			if (HasCountNotificationsCardBack != initialClientStateOutOfOrder.HasCountNotificationsCardBack || (HasCountNotificationsCardBack && !CountNotificationsCardBack.Equals(initialClientStateOutOfOrder.CountNotificationsCardBack)))
			{
				return false;
			}
			if (HasDeviceInfo != initialClientStateOutOfOrder.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(initialClientStateOutOfOrder.DeviceInfo)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InitialClientStateOutOfOrder Deserialize(Stream stream, InitialClientStateOutOfOrder instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InitialClientStateOutOfOrder DeserializeLengthDelimited(Stream stream)
		{
			InitialClientStateOutOfOrder initialClientStateOutOfOrder = new InitialClientStateOutOfOrder();
			DeserializeLengthDelimited(stream, initialClientStateOutOfOrder);
			return initialClientStateOutOfOrder;
		}

		public static InitialClientStateOutOfOrder DeserializeLengthDelimited(Stream stream, InitialClientStateOutOfOrder instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InitialClientStateOutOfOrder Deserialize(Stream stream, InitialClientStateOutOfOrder instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 16:
					instance.CountNotificationsAchieve = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.CountNotificationsNotice = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 32:
					instance.CountNotificationsCollection = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.CountNotificationsCurrency = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.CountNotificationsBooster = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.CountNotificationsHeroxp = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 64:
					instance.CountNotificationsPlayerRecord = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 72:
					instance.CountNotificationsArenaSession = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 80:
					instance.CountNotificationsCardBack = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 10:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, InitialClientStateOutOfOrder instance)
		{
			if (instance.HasCountNotificationsAchieve)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsAchieve);
			}
			if (instance.HasCountNotificationsNotice)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsNotice);
			}
			if (instance.HasCountNotificationsCollection)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsCollection);
			}
			if (instance.HasCountNotificationsCurrency)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsCurrency);
			}
			if (instance.HasCountNotificationsBooster)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsBooster);
			}
			if (instance.HasCountNotificationsHeroxp)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsHeroxp);
			}
			if (instance.HasCountNotificationsPlayerRecord)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsPlayerRecord);
			}
			if (instance.HasCountNotificationsArenaSession)
			{
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsArenaSession);
			}
			if (instance.HasCountNotificationsCardBack)
			{
				stream.WriteByte(80);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.CountNotificationsCardBack);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasCountNotificationsAchieve)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsAchieve);
			}
			if (HasCountNotificationsNotice)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsNotice);
			}
			if (HasCountNotificationsCollection)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsCollection);
			}
			if (HasCountNotificationsCurrency)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsCurrency);
			}
			if (HasCountNotificationsBooster)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsBooster);
			}
			if (HasCountNotificationsHeroxp)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsHeroxp);
			}
			if (HasCountNotificationsPlayerRecord)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsPlayerRecord);
			}
			if (HasCountNotificationsArenaSession)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsArenaSession);
			}
			if (HasCountNotificationsCardBack)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)CountNotificationsCardBack);
			}
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize = DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			return num;
		}
	}
}
