using System.IO;
using System.Text;

namespace bnet.protocol.authentication.v1
{
	public class SSOData : IProtoBuf
	{
		public bool HasAccountName;

		private string _AccountName;

		public bool HasRegion;

		private uint _Region;

		public bool HasProgram;

		private uint _Program;

		public bool HasTimeCreated;

		private ulong _TimeCreated;

		public bool HasIpAddress;

		private string _IpAddress;

		public string AccountName
		{
			get
			{
				return _AccountName;
			}
			set
			{
				_AccountName = value;
				HasAccountName = value != null;
			}
		}

		public uint Region
		{
			get
			{
				return _Region;
			}
			set
			{
				_Region = value;
				HasRegion = true;
			}
		}

		public uint Program
		{
			get
			{
				return _Program;
			}
			set
			{
				_Program = value;
				HasProgram = true;
			}
		}

		public ulong TimeCreated
		{
			get
			{
				return _TimeCreated;
			}
			set
			{
				_TimeCreated = value;
				HasTimeCreated = true;
			}
		}

		public string IpAddress
		{
			get
			{
				return _IpAddress;
			}
			set
			{
				_IpAddress = value;
				HasIpAddress = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetAccountName(string val)
		{
			AccountName = val;
		}

		public void SetRegion(uint val)
		{
			Region = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void SetTimeCreated(ulong val)
		{
			TimeCreated = val;
		}

		public void SetIpAddress(string val)
		{
			IpAddress = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAccountName)
			{
				num ^= AccountName.GetHashCode();
			}
			if (HasRegion)
			{
				num ^= Region.GetHashCode();
			}
			if (HasProgram)
			{
				num ^= Program.GetHashCode();
			}
			if (HasTimeCreated)
			{
				num ^= TimeCreated.GetHashCode();
			}
			if (HasIpAddress)
			{
				num ^= IpAddress.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SSOData sSOData = obj as SSOData;
			if (sSOData == null)
			{
				return false;
			}
			if (HasAccountName != sSOData.HasAccountName || (HasAccountName && !AccountName.Equals(sSOData.AccountName)))
			{
				return false;
			}
			if (HasRegion != sSOData.HasRegion || (HasRegion && !Region.Equals(sSOData.Region)))
			{
				return false;
			}
			if (HasProgram != sSOData.HasProgram || (HasProgram && !Program.Equals(sSOData.Program)))
			{
				return false;
			}
			if (HasTimeCreated != sSOData.HasTimeCreated || (HasTimeCreated && !TimeCreated.Equals(sSOData.TimeCreated)))
			{
				return false;
			}
			if (HasIpAddress != sSOData.HasIpAddress || (HasIpAddress && !IpAddress.Equals(sSOData.IpAddress)))
			{
				return false;
			}
			return true;
		}

		public static SSOData ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SSOData>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SSOData Deserialize(Stream stream, SSOData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SSOData DeserializeLengthDelimited(Stream stream)
		{
			SSOData sSOData = new SSOData();
			DeserializeLengthDelimited(stream, sSOData);
			return sSOData;
		}

		public static SSOData DeserializeLengthDelimited(Stream stream, SSOData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SSOData Deserialize(Stream stream, SSOData instance, long limit)
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
					instance.AccountName = ProtocolParser.ReadString(stream);
					continue;
				case 21:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 24:
					instance.TimeCreated = ProtocolParser.ReadUInt64(stream);
					continue;
				case 34:
					instance.IpAddress = ProtocolParser.ReadString(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					switch (key.Field)
					{
					case 0u:
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					case 16u:
						if (key.WireType == Wire.Varint)
						{
							instance.Region = ProtocolParser.ReadUInt32(stream);
						}
						break;
					default:
						ProtocolParser.SkipKey(stream, key);
						break;
					}
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

		public static void Serialize(Stream stream, SSOData instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasAccountName)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.AccountName));
			}
			if (instance.HasRegion)
			{
				stream.WriteByte(128);
				stream.WriteByte(1);
				ProtocolParser.WriteUInt32(stream, instance.Region);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasTimeCreated)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, instance.TimeCreated);
			}
			if (instance.HasIpAddress)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.IpAddress));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAccountName)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(AccountName);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasRegion)
			{
				num += 2;
				num += ProtocolParser.SizeOfUInt32(Region);
			}
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (HasTimeCreated)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(TimeCreated);
			}
			if (HasIpAddress)
			{
				num++;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(IpAddress);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			return num;
		}
	}
}
