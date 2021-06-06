using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.NGDP
{
	public class VersionError : IProtoBuf
	{
		public bool HasErrorCode;

		private uint _ErrorCode;

		public bool HasAgentState;

		private uint _AgentState;

		public bool HasLanguages;

		private string _Languages;

		public bool HasRegion;

		private string _Region;

		public bool HasBranch;

		private string _Branch;

		public bool HasAdditionalTags;

		private string _AdditionalTags;

		public uint ErrorCode
		{
			get
			{
				return _ErrorCode;
			}
			set
			{
				_ErrorCode = value;
				HasErrorCode = true;
			}
		}

		public uint AgentState
		{
			get
			{
				return _AgentState;
			}
			set
			{
				_AgentState = value;
				HasAgentState = true;
			}
		}

		public string Languages
		{
			get
			{
				return _Languages;
			}
			set
			{
				_Languages = value;
				HasLanguages = value != null;
			}
		}

		public string Region
		{
			get
			{
				return _Region;
			}
			set
			{
				_Region = value;
				HasRegion = value != null;
			}
		}

		public string Branch
		{
			get
			{
				return _Branch;
			}
			set
			{
				_Branch = value;
				HasBranch = value != null;
			}
		}

		public string AdditionalTags
		{
			get
			{
				return _AdditionalTags;
			}
			set
			{
				_AdditionalTags = value;
				HasAdditionalTags = value != null;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasErrorCode)
			{
				num ^= ErrorCode.GetHashCode();
			}
			if (HasAgentState)
			{
				num ^= AgentState.GetHashCode();
			}
			if (HasLanguages)
			{
				num ^= Languages.GetHashCode();
			}
			if (HasRegion)
			{
				num ^= Region.GetHashCode();
			}
			if (HasBranch)
			{
				num ^= Branch.GetHashCode();
			}
			if (HasAdditionalTags)
			{
				num ^= AdditionalTags.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			VersionError versionError = obj as VersionError;
			if (versionError == null)
			{
				return false;
			}
			if (HasErrorCode != versionError.HasErrorCode || (HasErrorCode && !ErrorCode.Equals(versionError.ErrorCode)))
			{
				return false;
			}
			if (HasAgentState != versionError.HasAgentState || (HasAgentState && !AgentState.Equals(versionError.AgentState)))
			{
				return false;
			}
			if (HasLanguages != versionError.HasLanguages || (HasLanguages && !Languages.Equals(versionError.Languages)))
			{
				return false;
			}
			if (HasRegion != versionError.HasRegion || (HasRegion && !Region.Equals(versionError.Region)))
			{
				return false;
			}
			if (HasBranch != versionError.HasBranch || (HasBranch && !Branch.Equals(versionError.Branch)))
			{
				return false;
			}
			if (HasAdditionalTags != versionError.HasAdditionalTags || (HasAdditionalTags && !AdditionalTags.Equals(versionError.AdditionalTags)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static VersionError Deserialize(Stream stream, VersionError instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static VersionError DeserializeLengthDelimited(Stream stream)
		{
			VersionError versionError = new VersionError();
			DeserializeLengthDelimited(stream, versionError);
			return versionError;
		}

		public static VersionError DeserializeLengthDelimited(Stream stream, VersionError instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static VersionError Deserialize(Stream stream, VersionError instance, long limit)
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
					instance.ErrorCode = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.AgentState = ProtocolParser.ReadUInt32(stream);
					continue;
				case 42:
					instance.Languages = ProtocolParser.ReadString(stream);
					continue;
				case 50:
					instance.Region = ProtocolParser.ReadString(stream);
					continue;
				case 58:
					instance.Branch = ProtocolParser.ReadString(stream);
					continue;
				case 66:
					instance.AdditionalTags = ProtocolParser.ReadString(stream);
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

		public static void Serialize(Stream stream, VersionError instance)
		{
			if (instance.HasErrorCode)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.ErrorCode);
			}
			if (instance.HasAgentState)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.AgentState);
			}
			if (instance.HasLanguages)
			{
				stream.WriteByte(42);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Languages));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Region));
			}
			if (instance.HasBranch)
			{
				stream.WriteByte(58);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Branch));
			}
			if (instance.HasAdditionalTags)
			{
				stream.WriteByte(66);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AdditionalTags));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasErrorCode)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(ErrorCode);
			}
			if (HasAgentState)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(AgentState);
			}
			if (HasLanguages)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Languages);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasRegion)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Region);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (HasBranch)
			{
				num++;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(Branch);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			if (HasAdditionalTags)
			{
				num++;
				uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(AdditionalTags);
				num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
			}
			return num;
		}
	}
}
