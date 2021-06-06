using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class UpdateFinished : IProtoBuf
	{
		public bool HasUpdatedVersion;

		private string _UpdatedVersion;

		public bool HasAvailableSpaceMB;

		private float _AvailableSpaceMB;

		public bool HasElapsedSeconds;

		private float _ElapsedSeconds;

		public string UpdatedVersion
		{
			get
			{
				return _UpdatedVersion;
			}
			set
			{
				_UpdatedVersion = value;
				HasUpdatedVersion = value != null;
			}
		}

		public float AvailableSpaceMB
		{
			get
			{
				return _AvailableSpaceMB;
			}
			set
			{
				_AvailableSpaceMB = value;
				HasAvailableSpaceMB = true;
			}
		}

		public float ElapsedSeconds
		{
			get
			{
				return _ElapsedSeconds;
			}
			set
			{
				_ElapsedSeconds = value;
				HasElapsedSeconds = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasUpdatedVersion)
			{
				num ^= UpdatedVersion.GetHashCode();
			}
			if (HasAvailableSpaceMB)
			{
				num ^= AvailableSpaceMB.GetHashCode();
			}
			if (HasElapsedSeconds)
			{
				num ^= ElapsedSeconds.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UpdateFinished updateFinished = obj as UpdateFinished;
			if (updateFinished == null)
			{
				return false;
			}
			if (HasUpdatedVersion != updateFinished.HasUpdatedVersion || (HasUpdatedVersion && !UpdatedVersion.Equals(updateFinished.UpdatedVersion)))
			{
				return false;
			}
			if (HasAvailableSpaceMB != updateFinished.HasAvailableSpaceMB || (HasAvailableSpaceMB && !AvailableSpaceMB.Equals(updateFinished.AvailableSpaceMB)))
			{
				return false;
			}
			if (HasElapsedSeconds != updateFinished.HasElapsedSeconds || (HasElapsedSeconds && !ElapsedSeconds.Equals(updateFinished.ElapsedSeconds)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UpdateFinished Deserialize(Stream stream, UpdateFinished instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UpdateFinished DeserializeLengthDelimited(Stream stream)
		{
			UpdateFinished updateFinished = new UpdateFinished();
			DeserializeLengthDelimited(stream, updateFinished);
			return updateFinished;
		}

		public static UpdateFinished DeserializeLengthDelimited(Stream stream, UpdateFinished instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UpdateFinished Deserialize(Stream stream, UpdateFinished instance, long limit)
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
				case 18:
					instance.UpdatedVersion = ProtocolParser.ReadString(stream);
					continue;
				case 29:
					instance.AvailableSpaceMB = binaryReader.ReadSingle();
					continue;
				case 37:
					instance.ElapsedSeconds = binaryReader.ReadSingle();
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

		public static void Serialize(Stream stream, UpdateFinished instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasUpdatedVersion)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.UpdatedVersion));
			}
			if (instance.HasAvailableSpaceMB)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.AvailableSpaceMB);
			}
			if (instance.HasElapsedSeconds)
			{
				stream.WriteByte(37);
				binaryWriter.Write(instance.ElapsedSeconds);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasUpdatedVersion)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(UpdatedVersion);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasAvailableSpaceMB)
			{
				num++;
				num += 4;
			}
			if (HasElapsedSeconds)
			{
				num++;
				num += 4;
			}
			return num;
		}
	}
}
