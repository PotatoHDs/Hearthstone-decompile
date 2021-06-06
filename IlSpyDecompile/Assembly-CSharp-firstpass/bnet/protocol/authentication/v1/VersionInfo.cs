using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	public class VersionInfo : IProtoBuf
	{
		public bool HasNumber;

		private uint _Number;

		public bool HasPatch;

		private string _Patch;

		public bool HasIsOptional;

		private bool _IsOptional;

		public bool HasKickTime;

		private ulong _KickTime;

		public uint Number
		{
			get
			{
				return _Number;
			}
			set
			{
				_Number = value;
				HasNumber = true;
			}
		}

		public string Patch
		{
			get
			{
				return _Patch;
			}
			set
			{
				_Patch = value;
				HasPatch = value != null;
			}
		}

		public bool IsOptional
		{
			get
			{
				return _IsOptional;
			}
			set
			{
				_IsOptional = value;
				HasIsOptional = true;
			}
		}

		public ulong KickTime
		{
			get
			{
				return _KickTime;
			}
			set
			{
				_KickTime = value;
				HasKickTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetNumber(uint val)
		{
			Number = val;
		}

		public void SetPatch(string val)
		{
			Patch = val;
		}

		public void SetIsOptional(bool val)
		{
			IsOptional = val;
		}

		public void SetKickTime(ulong val)
		{
			KickTime = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasNumber)
			{
				num ^= Number.GetHashCode();
			}
			if (HasPatch)
			{
				num ^= Patch.GetHashCode();
			}
			if (HasIsOptional)
			{
				num ^= IsOptional.GetHashCode();
			}
			if (HasKickTime)
			{
				num ^= KickTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VersionInfo versionInfo = obj as VersionInfo;
			if (versionInfo == null)
			{
				return false;
			}
			if (HasNumber != versionInfo.HasNumber || (HasNumber && !Number.Equals(versionInfo.Number)))
			{
				return false;
			}
			if (HasPatch != versionInfo.HasPatch || (HasPatch && !Patch.Equals(versionInfo.Patch)))
			{
				return false;
			}
			if (HasIsOptional != versionInfo.HasIsOptional || (HasIsOptional && !IsOptional.Equals(versionInfo.IsOptional)))
			{
				return false;
			}
			if (HasKickTime != versionInfo.HasKickTime || (HasKickTime && !KickTime.Equals(versionInfo.KickTime)))
			{
				return false;
			}
			return true;
		}

		public static VersionInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<VersionInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static VersionInfo Deserialize(Stream stream, VersionInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static VersionInfo DeserializeLengthDelimited(Stream stream)
		{
			VersionInfo versionInfo = new VersionInfo();
			DeserializeLengthDelimited(stream, versionInfo);
			return versionInfo;
		}

		public static VersionInfo DeserializeLengthDelimited(Stream stream, VersionInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static VersionInfo Deserialize(Stream stream, VersionInfo instance, long limit)
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
					instance.Number = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					instance.Patch = ProtocolParser.ReadString(stream);
					continue;
				case 24:
					instance.IsOptional = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.KickTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, VersionInfo instance)
		{
			if (instance.HasNumber)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Number);
			}
			if (instance.HasPatch)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Patch));
			}
			if (instance.HasIsOptional)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsOptional);
			}
			if (instance.HasKickTime)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, instance.KickTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasNumber)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Number);
			}
			if (HasPatch)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Patch);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasIsOptional)
			{
				num++;
				num++;
			}
			if (HasKickTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(KickTime);
			}
			return num;
		}
	}
}
