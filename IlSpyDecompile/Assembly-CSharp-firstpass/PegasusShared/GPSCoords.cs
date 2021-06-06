using System.IO;

namespace PegasusShared
{
	public class GPSCoords : IProtoBuf
	{
		public bool HasLongitude;

		private double _Longitude;

		public bool HasAccuracy;

		private double _Accuracy;

		public double Latitude { get; set; }

		public double Longitude
		{
			get
			{
				return _Longitude;
			}
			set
			{
				_Longitude = value;
				HasLongitude = true;
			}
		}

		public double Accuracy
		{
			get
			{
				return _Accuracy;
			}
			set
			{
				_Accuracy = value;
				HasAccuracy = true;
			}
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Latitude.GetHashCode();
			if (HasLongitude)
			{
				hashCode ^= Longitude.GetHashCode();
			}
			if (HasAccuracy)
			{
				hashCode ^= Accuracy.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			GPSCoords gPSCoords = obj as GPSCoords;
			if (gPSCoords == null)
			{
				return false;
			}
			if (!Latitude.Equals(gPSCoords.Latitude))
			{
				return false;
			}
			if (HasLongitude != gPSCoords.HasLongitude || (HasLongitude && !Longitude.Equals(gPSCoords.Longitude)))
			{
				return false;
			}
			if (HasAccuracy != gPSCoords.HasAccuracy || (HasAccuracy && !Accuracy.Equals(gPSCoords.Accuracy)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GPSCoords Deserialize(Stream stream, GPSCoords instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GPSCoords DeserializeLengthDelimited(Stream stream)
		{
			GPSCoords gPSCoords = new GPSCoords();
			DeserializeLengthDelimited(stream, gPSCoords);
			return gPSCoords;
		}

		public static GPSCoords DeserializeLengthDelimited(Stream stream, GPSCoords instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GPSCoords Deserialize(Stream stream, GPSCoords instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
				case 9:
					instance.Latitude = binaryReader.ReadDouble();
					continue;
				case 17:
					instance.Longitude = binaryReader.ReadDouble();
					continue;
				case 25:
					instance.Accuracy = binaryReader.ReadDouble();
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

		public static void Serialize(Stream stream, GPSCoords instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.Latitude);
			if (instance.HasLongitude)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.Longitude);
			}
			if (instance.HasAccuracy)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.Accuracy);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8;
			if (HasLongitude)
			{
				num++;
				num += 8;
			}
			if (HasAccuracy)
			{
				num++;
				num += 8;
			}
			return num + 1;
		}
	}
}
