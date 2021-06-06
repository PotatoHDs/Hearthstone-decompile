using System.IO;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class VersionStarted : IProtoBuf
	{
		public bool HasDummy;

		private int _Dummy;

		public int Dummy
		{
			get
			{
				return _Dummy;
			}
			set
			{
				_Dummy = value;
				HasDummy = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasDummy)
			{
				num ^= Dummy.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VersionStarted versionStarted = obj as VersionStarted;
			if (versionStarted == null)
			{
				return false;
			}
			if (HasDummy != versionStarted.HasDummy || (HasDummy && !Dummy.Equals(versionStarted.Dummy)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static VersionStarted Deserialize(Stream stream, VersionStarted instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static VersionStarted DeserializeLengthDelimited(Stream stream)
		{
			VersionStarted versionStarted = new VersionStarted();
			DeserializeLengthDelimited(stream, versionStarted);
			return versionStarted;
		}

		public static VersionStarted DeserializeLengthDelimited(Stream stream, VersionStarted instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static VersionStarted Deserialize(Stream stream, VersionStarted instance, long limit)
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
				case 8:
					instance.Dummy = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, VersionStarted instance)
		{
			if (instance.HasDummy)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Dummy);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasDummy)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Dummy);
			}
			return num;
		}
	}
}
