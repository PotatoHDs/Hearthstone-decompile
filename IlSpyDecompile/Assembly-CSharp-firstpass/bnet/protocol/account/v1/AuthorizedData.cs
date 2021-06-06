using System.Collections.Generic;
using System.IO;
using System.Text;

namespace bnet.protocol.account.v1
{
	public class AuthorizedData : IProtoBuf
	{
		public bool HasData;

		private string _Data;

		private List<uint> _License = new List<uint>();

		public string Data
		{
			get
			{
				return _Data;
			}
			set
			{
				_Data = value;
				HasData = value != null;
			}
		}

		public List<uint> License
		{
			get
			{
				return _License;
			}
			set
			{
				_License = value;
			}
		}

		public List<uint> LicenseList => _License;

		public int LicenseCount => _License.Count;

		public bool IsInitialized => true;

		public void SetData(string val)
		{
			Data = val;
		}

		public void AddLicense(uint val)
		{
			_License.Add(val);
		}

		public void ClearLicense()
		{
			_License.Clear();
		}

		public void SetLicense(List<uint> val)
		{
			License = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasData)
			{
				num ^= Data.GetHashCode();
			}
			foreach (uint item in License)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			AuthorizedData authorizedData = obj as AuthorizedData;
			if (authorizedData == null)
			{
				return false;
			}
			if (HasData != authorizedData.HasData || (HasData && !Data.Equals(authorizedData.Data)))
			{
				return false;
			}
			if (License.Count != authorizedData.License.Count)
			{
				return false;
			}
			for (int i = 0; i < License.Count; i++)
			{
				if (!License[i].Equals(authorizedData.License[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static AuthorizedData ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AuthorizedData>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static AuthorizedData Deserialize(Stream stream, AuthorizedData instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static AuthorizedData DeserializeLengthDelimited(Stream stream)
		{
			AuthorizedData authorizedData = new AuthorizedData();
			DeserializeLengthDelimited(stream, authorizedData);
			return authorizedData;
		}

		public static AuthorizedData DeserializeLengthDelimited(Stream stream, AuthorizedData instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static AuthorizedData Deserialize(Stream stream, AuthorizedData instance, long limit)
		{
			if (instance.License == null)
			{
				instance.License = new List<uint>();
			}
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
					instance.Data = ProtocolParser.ReadString(stream);
					continue;
				case 16:
					instance.License.Add(ProtocolParser.ReadUInt32(stream));
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

		public static void Serialize(Stream stream, AuthorizedData instance)
		{
			if (instance.HasData)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Data));
			}
			if (instance.License.Count <= 0)
			{
				return;
			}
			foreach (uint item in instance.License)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasData)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Data);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (License.Count > 0)
			{
				foreach (uint item in License)
				{
					num++;
					num += ProtocolParser.SizeOfUInt32(item);
				}
				return num;
			}
			return num;
		}
	}
}
