using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;

namespace bnet.protocol.broadcast.v1
{
	public class BroadcastFilter : IProtoBuf
	{
		private List<AccountLicense> _TargetLicense = new List<AccountLicense>();

		public bool HasEmployeeOnly;

		private bool _EmployeeOnly;

		private List<string> _TargetProgram = new List<string>();

		private List<string> _TargetLocale = new List<string>();

		private List<string> _TargetCountry = new List<string>();

		private List<string> _TargetIpMask = new List<string>();

		public List<AccountLicense> TargetLicense
		{
			get
			{
				return _TargetLicense;
			}
			set
			{
				_TargetLicense = value;
			}
		}

		public List<AccountLicense> TargetLicenseList => _TargetLicense;

		public int TargetLicenseCount => _TargetLicense.Count;

		public bool EmployeeOnly
		{
			get
			{
				return _EmployeeOnly;
			}
			set
			{
				_EmployeeOnly = value;
				HasEmployeeOnly = true;
			}
		}

		public List<string> TargetProgram
		{
			get
			{
				return _TargetProgram;
			}
			set
			{
				_TargetProgram = value;
			}
		}

		public List<string> TargetProgramList => _TargetProgram;

		public int TargetProgramCount => _TargetProgram.Count;

		public List<string> TargetLocale
		{
			get
			{
				return _TargetLocale;
			}
			set
			{
				_TargetLocale = value;
			}
		}

		public List<string> TargetLocaleList => _TargetLocale;

		public int TargetLocaleCount => _TargetLocale.Count;

		public List<string> TargetCountry
		{
			get
			{
				return _TargetCountry;
			}
			set
			{
				_TargetCountry = value;
			}
		}

		public List<string> TargetCountryList => _TargetCountry;

		public int TargetCountryCount => _TargetCountry.Count;

		public List<string> TargetIpMask
		{
			get
			{
				return _TargetIpMask;
			}
			set
			{
				_TargetIpMask = value;
			}
		}

		public List<string> TargetIpMaskList => _TargetIpMask;

		public int TargetIpMaskCount => _TargetIpMask.Count;

		public bool IsInitialized => true;

		public void AddTargetLicense(AccountLicense val)
		{
			_TargetLicense.Add(val);
		}

		public void ClearTargetLicense()
		{
			_TargetLicense.Clear();
		}

		public void SetTargetLicense(List<AccountLicense> val)
		{
			TargetLicense = val;
		}

		public void SetEmployeeOnly(bool val)
		{
			EmployeeOnly = val;
		}

		public void AddTargetProgram(string val)
		{
			_TargetProgram.Add(val);
		}

		public void ClearTargetProgram()
		{
			_TargetProgram.Clear();
		}

		public void SetTargetProgram(List<string> val)
		{
			TargetProgram = val;
		}

		public void AddTargetLocale(string val)
		{
			_TargetLocale.Add(val);
		}

		public void ClearTargetLocale()
		{
			_TargetLocale.Clear();
		}

		public void SetTargetLocale(List<string> val)
		{
			TargetLocale = val;
		}

		public void AddTargetCountry(string val)
		{
			_TargetCountry.Add(val);
		}

		public void ClearTargetCountry()
		{
			_TargetCountry.Clear();
		}

		public void SetTargetCountry(List<string> val)
		{
			TargetCountry = val;
		}

		public void AddTargetIpMask(string val)
		{
			_TargetIpMask.Add(val);
		}

		public void ClearTargetIpMask()
		{
			_TargetIpMask.Clear();
		}

		public void SetTargetIpMask(List<string> val)
		{
			TargetIpMask = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (AccountLicense item in TargetLicense)
			{
				num ^= item.GetHashCode();
			}
			if (HasEmployeeOnly)
			{
				num ^= EmployeeOnly.GetHashCode();
			}
			foreach (string item2 in TargetProgram)
			{
				num ^= item2.GetHashCode();
			}
			foreach (string item3 in TargetLocale)
			{
				num ^= item3.GetHashCode();
			}
			foreach (string item4 in TargetCountry)
			{
				num ^= item4.GetHashCode();
			}
			foreach (string item5 in TargetIpMask)
			{
				num ^= item5.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			BroadcastFilter broadcastFilter = obj as BroadcastFilter;
			if (broadcastFilter == null)
			{
				return false;
			}
			if (TargetLicense.Count != broadcastFilter.TargetLicense.Count)
			{
				return false;
			}
			for (int i = 0; i < TargetLicense.Count; i++)
			{
				if (!TargetLicense[i].Equals(broadcastFilter.TargetLicense[i]))
				{
					return false;
				}
			}
			if (HasEmployeeOnly != broadcastFilter.HasEmployeeOnly || (HasEmployeeOnly && !EmployeeOnly.Equals(broadcastFilter.EmployeeOnly)))
			{
				return false;
			}
			if (TargetProgram.Count != broadcastFilter.TargetProgram.Count)
			{
				return false;
			}
			for (int j = 0; j < TargetProgram.Count; j++)
			{
				if (!TargetProgram[j].Equals(broadcastFilter.TargetProgram[j]))
				{
					return false;
				}
			}
			if (TargetLocale.Count != broadcastFilter.TargetLocale.Count)
			{
				return false;
			}
			for (int k = 0; k < TargetLocale.Count; k++)
			{
				if (!TargetLocale[k].Equals(broadcastFilter.TargetLocale[k]))
				{
					return false;
				}
			}
			if (TargetCountry.Count != broadcastFilter.TargetCountry.Count)
			{
				return false;
			}
			for (int l = 0; l < TargetCountry.Count; l++)
			{
				if (!TargetCountry[l].Equals(broadcastFilter.TargetCountry[l]))
				{
					return false;
				}
			}
			if (TargetIpMask.Count != broadcastFilter.TargetIpMask.Count)
			{
				return false;
			}
			for (int m = 0; m < TargetIpMask.Count; m++)
			{
				if (!TargetIpMask[m].Equals(broadcastFilter.TargetIpMask[m]))
				{
					return false;
				}
			}
			return true;
		}

		public static BroadcastFilter ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<BroadcastFilter>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static BroadcastFilter Deserialize(Stream stream, BroadcastFilter instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static BroadcastFilter DeserializeLengthDelimited(Stream stream)
		{
			BroadcastFilter broadcastFilter = new BroadcastFilter();
			DeserializeLengthDelimited(stream, broadcastFilter);
			return broadcastFilter;
		}

		public static BroadcastFilter DeserializeLengthDelimited(Stream stream, BroadcastFilter instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static BroadcastFilter Deserialize(Stream stream, BroadcastFilter instance, long limit)
		{
			if (instance.TargetLicense == null)
			{
				instance.TargetLicense = new List<AccountLicense>();
			}
			if (instance.TargetProgram == null)
			{
				instance.TargetProgram = new List<string>();
			}
			if (instance.TargetLocale == null)
			{
				instance.TargetLocale = new List<string>();
			}
			if (instance.TargetCountry == null)
			{
				instance.TargetCountry = new List<string>();
			}
			if (instance.TargetIpMask == null)
			{
				instance.TargetIpMask = new List<string>();
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
					instance.TargetLicense.Add(AccountLicense.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.EmployeeOnly = ProtocolParser.ReadBool(stream);
					continue;
				case 26:
					instance.TargetProgram.Add(ProtocolParser.ReadString(stream));
					continue;
				case 34:
					instance.TargetLocale.Add(ProtocolParser.ReadString(stream));
					continue;
				case 42:
					instance.TargetCountry.Add(ProtocolParser.ReadString(stream));
					continue;
				case 50:
					instance.TargetIpMask.Add(ProtocolParser.ReadString(stream));
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

		public static void Serialize(Stream stream, BroadcastFilter instance)
		{
			if (instance.TargetLicense.Count > 0)
			{
				foreach (AccountLicense item in instance.TargetLicense)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					AccountLicense.Serialize(stream, item);
				}
			}
			if (instance.HasEmployeeOnly)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.EmployeeOnly);
			}
			if (instance.TargetProgram.Count > 0)
			{
				foreach (string item2 in instance.TargetProgram)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item2));
				}
			}
			if (instance.TargetLocale.Count > 0)
			{
				foreach (string item3 in instance.TargetLocale)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item3));
				}
			}
			if (instance.TargetCountry.Count > 0)
			{
				foreach (string item4 in instance.TargetCountry)
				{
					stream.WriteByte(42);
					ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item4));
				}
			}
			if (instance.TargetIpMask.Count <= 0)
			{
				return;
			}
			foreach (string item5 in instance.TargetIpMask)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(item5));
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (TargetLicense.Count > 0)
			{
				foreach (AccountLicense item in TargetLicense)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasEmployeeOnly)
			{
				num++;
				num++;
			}
			if (TargetProgram.Count > 0)
			{
				foreach (string item2 in TargetProgram)
				{
					num++;
					uint byteCount = (uint)Encoding.UTF8.GetByteCount(item2);
					num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
				}
			}
			if (TargetLocale.Count > 0)
			{
				foreach (string item3 in TargetLocale)
				{
					num++;
					uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(item3);
					num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
				}
			}
			if (TargetCountry.Count > 0)
			{
				foreach (string item4 in TargetCountry)
				{
					num++;
					uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(item4);
					num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
				}
			}
			if (TargetIpMask.Count > 0)
			{
				foreach (string item5 in TargetIpMask)
				{
					num++;
					uint byteCount4 = (uint)Encoding.UTF8.GetByteCount(item5);
					num += ProtocolParser.SizeOfUInt32(byteCount4) + byteCount4;
				}
				return num;
			}
			return num;
		}
	}
}
