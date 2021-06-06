using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class AppPaused : IProtoBuf
	{
		public bool HasPauseStatus;

		private bool _PauseStatus;

		public bool HasPauseTime;

		private float _PauseTime;

		public bool PauseStatus
		{
			get
			{
				return _PauseStatus;
			}
			set
			{
				_PauseStatus = value;
				HasPauseStatus = true;
			}
		}

		public float PauseTime
		{
			get
			{
				return _PauseTime;
			}
			set
			{
				_PauseTime = value;
				HasPauseTime = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPauseStatus)
			{
				num ^= PauseStatus.GetHashCode();
			}
			if (HasPauseTime)
			{
				num ^= PauseTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AppPaused appPaused = obj as AppPaused;
			if (appPaused == null)
			{
				return false;
			}
			if (HasPauseStatus != appPaused.HasPauseStatus || (HasPauseStatus && !PauseStatus.Equals(appPaused.PauseStatus)))
			{
				return false;
			}
			if (HasPauseTime != appPaused.HasPauseTime || (HasPauseTime && !PauseTime.Equals(appPaused.PauseTime)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AppPaused Deserialize(Stream stream, AppPaused instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AppPaused DeserializeLengthDelimited(Stream stream)
		{
			AppPaused appPaused = new AppPaused();
			DeserializeLengthDelimited(stream, appPaused);
			return appPaused;
		}

		public static AppPaused DeserializeLengthDelimited(Stream stream, AppPaused instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AppPaused Deserialize(Stream stream, AppPaused instance, long limit)
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
				case 8:
					instance.PauseStatus = ProtocolParser.ReadBool(stream);
					continue;
				case 21:
					instance.PauseTime = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, AppPaused instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPauseStatus)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.PauseStatus);
			}
			if (instance.HasPauseTime)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.PauseTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPauseStatus)
			{
				num++;
				num++;
			}
			if (HasPauseTime)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
