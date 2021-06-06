using System.IO;

namespace PegasusFSG
{
	public class FSGFeatureConfig : IProtoBuf
	{
		public enum PacketID
		{
			ID = 0x1FF
		}

		public bool HasGps;

		private bool _Gps;

		public bool HasWifi;

		private bool _Wifi;

		public bool HasAutoCheckin;

		private bool _AutoCheckin;

		public bool HasMaxAccuracy;

		private long _MaxAccuracy;

		public bool Gps
		{
			get
			{
				return _Gps;
			}
			set
			{
				_Gps = value;
				HasGps = true;
			}
		}

		public bool Wifi
		{
			get
			{
				return _Wifi;
			}
			set
			{
				_Wifi = value;
				HasWifi = true;
			}
		}

		public bool AutoCheckin
		{
			get
			{
				return _AutoCheckin;
			}
			set
			{
				_AutoCheckin = value;
				HasAutoCheckin = true;
			}
		}

		public long MaxAccuracy
		{
			get
			{
				return _MaxAccuracy;
			}
			set
			{
				_MaxAccuracy = value;
				HasMaxAccuracy = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasGps)
			{
				num ^= Gps.GetHashCode();
			}
			if (HasWifi)
			{
				num ^= Wifi.GetHashCode();
			}
			if (HasAutoCheckin)
			{
				num ^= AutoCheckin.GetHashCode();
			}
			if (HasMaxAccuracy)
			{
				num ^= MaxAccuracy.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FSGFeatureConfig fSGFeatureConfig = obj as FSGFeatureConfig;
			if (fSGFeatureConfig == null)
			{
				return false;
			}
			if (HasGps != fSGFeatureConfig.HasGps || (HasGps && !Gps.Equals(fSGFeatureConfig.Gps)))
			{
				return false;
			}
			if (HasWifi != fSGFeatureConfig.HasWifi || (HasWifi && !Wifi.Equals(fSGFeatureConfig.Wifi)))
			{
				return false;
			}
			if (HasAutoCheckin != fSGFeatureConfig.HasAutoCheckin || (HasAutoCheckin && !AutoCheckin.Equals(fSGFeatureConfig.AutoCheckin)))
			{
				return false;
			}
			if (HasMaxAccuracy != fSGFeatureConfig.HasMaxAccuracy || (HasMaxAccuracy && !MaxAccuracy.Equals(fSGFeatureConfig.MaxAccuracy)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FSGFeatureConfig Deserialize(Stream stream, FSGFeatureConfig instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FSGFeatureConfig DeserializeLengthDelimited(Stream stream)
		{
			FSGFeatureConfig fSGFeatureConfig = new FSGFeatureConfig();
			DeserializeLengthDelimited(stream, fSGFeatureConfig);
			return fSGFeatureConfig;
		}

		public static FSGFeatureConfig DeserializeLengthDelimited(Stream stream, FSGFeatureConfig instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FSGFeatureConfig Deserialize(Stream stream, FSGFeatureConfig instance, long limit)
		{
			instance.Gps = true;
			instance.Wifi = true;
			instance.AutoCheckin = true;
			instance.MaxAccuracy = 200L;
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
				case 8:
					instance.Gps = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.Wifi = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.AutoCheckin = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.MaxAccuracy = (long)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, FSGFeatureConfig instance)
		{
			if (instance.HasGps)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.Gps);
			}
			if (instance.HasWifi)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.Wifi);
			}
			if (instance.HasAutoCheckin)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.AutoCheckin);
			}
			if (instance.HasMaxAccuracy)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MaxAccuracy);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasGps)
			{
				num++;
				num++;
			}
			if (HasWifi)
			{
				num++;
				num++;
			}
			if (HasAutoCheckin)
			{
				num++;
				num++;
			}
			if (HasMaxAccuracy)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MaxAccuracy);
			}
			return num;
		}
	}
}
