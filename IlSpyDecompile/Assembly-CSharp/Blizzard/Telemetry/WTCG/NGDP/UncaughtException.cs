using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class UncaughtException : IProtoBuf
	{
		public bool HasStackTrace;

		private string _StackTrace;

		public bool HasAndroidModel;

		private string _AndroidModel;

		public bool HasAndroidSdkVersion;

		private uint _AndroidSdkVersion;

		public string StackTrace
		{
			get
			{
				return _StackTrace;
			}
			set
			{
				_StackTrace = value;
				HasStackTrace = value != null;
			}
		}

		public string AndroidModel
		{
			get
			{
				return _AndroidModel;
			}
			set
			{
				_AndroidModel = value;
				HasAndroidModel = value != null;
			}
		}

		public uint AndroidSdkVersion
		{
			get
			{
				return _AndroidSdkVersion;
			}
			set
			{
				_AndroidSdkVersion = value;
				HasAndroidSdkVersion = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasStackTrace)
			{
				num ^= StackTrace.GetHashCode();
			}
			if (HasAndroidModel)
			{
				num ^= AndroidModel.GetHashCode();
			}
			if (HasAndroidSdkVersion)
			{
				num ^= AndroidSdkVersion.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UncaughtException ex = obj as UncaughtException;
			if (ex == null)
			{
				return false;
			}
			if (HasStackTrace != ex.HasStackTrace || (HasStackTrace && !StackTrace.Equals(ex.StackTrace)))
			{
				return false;
			}
			if (HasAndroidModel != ex.HasAndroidModel || (HasAndroidModel && !AndroidModel.Equals(ex.AndroidModel)))
			{
				return false;
			}
			if (HasAndroidSdkVersion != ex.HasAndroidSdkVersion || (HasAndroidSdkVersion && !AndroidSdkVersion.Equals(ex.AndroidSdkVersion)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UncaughtException Deserialize(Stream stream, UncaughtException instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UncaughtException DeserializeLengthDelimited(Stream stream)
		{
			UncaughtException ex = new UncaughtException();
			DeserializeLengthDelimited(stream, ex);
			return ex;
		}

		public static UncaughtException DeserializeLengthDelimited(Stream stream, UncaughtException instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UncaughtException Deserialize(Stream stream, UncaughtException instance, long limit)
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
				case 10:
					instance.StackTrace = ProtocolParser.ReadString(stream);
					continue;
				case 18:
					instance.AndroidModel = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.AndroidSdkVersion = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, UncaughtException instance)
		{
			if (instance.HasStackTrace)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.StackTrace));
			}
			if (instance.HasAndroidModel)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AndroidModel));
			}
			if (instance.HasAndroidSdkVersion)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.AndroidSdkVersion);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasStackTrace)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(StackTrace);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAndroidModel)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(AndroidModel);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasAndroidSdkVersion)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(AndroidSdkVersion);
			}
			return num;
		}
	}
}
