using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AppInitialized : IProtoBuf
	{
		public bool HasTestType;

		private string _TestType;

		public bool HasDuration;

		private float _Duration;

		public bool HasClientChangelist;

		private string _ClientChangelist;

		public string TestType
		{
			get
			{
				return _TestType;
			}
			set
			{
				_TestType = value;
				HasTestType = value != null;
			}
		}

		public float Duration
		{
			get
			{
				return _Duration;
			}
			set
			{
				_Duration = value;
				HasDuration = true;
			}
		}

		public string ClientChangelist
		{
			get
			{
				return _ClientChangelist;
			}
			set
			{
				_ClientChangelist = value;
				HasClientChangelist = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTestType)
			{
				num ^= TestType.GetHashCode();
			}
			if (HasDuration)
			{
				num ^= Duration.GetHashCode();
			}
			if (HasClientChangelist)
			{
				num ^= ClientChangelist.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AppInitialized appInitialized = obj as AppInitialized;
			if (appInitialized == null)
			{
				return false;
			}
			if (HasTestType != appInitialized.HasTestType || (HasTestType && !TestType.Equals(appInitialized.TestType)))
			{
				return false;
			}
			if (HasDuration != appInitialized.HasDuration || (HasDuration && !Duration.Equals(appInitialized.Duration)))
			{
				return false;
			}
			if (HasClientChangelist != appInitialized.HasClientChangelist || (HasClientChangelist && !ClientChangelist.Equals(appInitialized.ClientChangelist)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AppInitialized Deserialize(Stream stream, AppInitialized instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AppInitialized DeserializeLengthDelimited(Stream stream)
		{
			AppInitialized appInitialized = new AppInitialized();
			DeserializeLengthDelimited(stream, appInitialized);
			return appInitialized;
		}

		public static AppInitialized DeserializeLengthDelimited(Stream stream, AppInitialized instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AppInitialized Deserialize(Stream stream, AppInitialized instance, long limit)
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
				case 10:
					instance.TestType = ProtocolParser.ReadString(stream);
					continue;
				case 21:
					instance.Duration = binaryReader.ReadSingle();
					continue;
				case 26:
					instance.ClientChangelist = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, AppInitialized instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasTestType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.TestType));
			}
			if (instance.HasDuration)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Duration);
			}
			if (instance.HasClientChangelist)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.ClientChangelist));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTestType)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(TestType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasDuration)
			{
				num++;
				num += 4;
			}
			if (HasClientChangelist)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(ClientChangelist);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
