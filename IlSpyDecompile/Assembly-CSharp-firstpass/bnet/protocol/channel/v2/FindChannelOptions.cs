using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class FindChannelOptions : IProtoBuf
	{
		public bool HasType;

		private UniqueChannelType _Type;

		public bool HasIdentity;

		private string _Identity;

		public bool HasLocale;

		private uint _Locale;

		private List<bnet.protocol.v2.Attribute> _SearchAttribute = new List<bnet.protocol.v2.Attribute>();

		private List<AccountId> _Reservation = new List<AccountId>();

		public UniqueChannelType Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				HasType = value != null;
			}
		}

		public string Identity
		{
			get
			{
				return _Identity;
			}
			set
			{
				_Identity = value;
				HasIdentity = value != null;
			}
		}

		public uint Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
				HasLocale = true;
			}
		}

		public List<bnet.protocol.v2.Attribute> SearchAttribute
		{
			get
			{
				return _SearchAttribute;
			}
			set
			{
				_SearchAttribute = value;
			}
		}

		public List<bnet.protocol.v2.Attribute> SearchAttributeList => _SearchAttribute;

		public int SearchAttributeCount => _SearchAttribute.Count;

		public List<AccountId> Reservation
		{
			get
			{
				return _Reservation;
			}
			set
			{
				_Reservation = value;
			}
		}

		public List<AccountId> ReservationList => _Reservation;

		public int ReservationCount => _Reservation.Count;

		public bool IsInitialized => true;

		public void SetType(UniqueChannelType val)
		{
			Type = val;
		}

		public void SetIdentity(string val)
		{
			Identity = val;
		}

		public void SetLocale(uint val)
		{
			Locale = val;
		}

		public void AddSearchAttribute(bnet.protocol.v2.Attribute val)
		{
			_SearchAttribute.Add(val);
		}

		public void ClearSearchAttribute()
		{
			_SearchAttribute.Clear();
		}

		public void SetSearchAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			SearchAttribute = val;
		}

		public void AddReservation(AccountId val)
		{
			_Reservation.Add(val);
		}

		public void ClearReservation()
		{
			_Reservation.Clear();
		}

		public void SetReservation(List<AccountId> val)
		{
			Reservation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasType)
			{
				num ^= Type.GetHashCode();
			}
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			foreach (bnet.protocol.v2.Attribute item in SearchAttribute)
			{
				num ^= item.GetHashCode();
			}
			foreach (AccountId item2 in Reservation)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			FindChannelOptions findChannelOptions = obj as FindChannelOptions;
			if (findChannelOptions == null)
			{
				return false;
			}
			if (HasType != findChannelOptions.HasType || (HasType && !Type.Equals(findChannelOptions.Type)))
			{
				return false;
			}
			if (HasIdentity != findChannelOptions.HasIdentity || (HasIdentity && !Identity.Equals(findChannelOptions.Identity)))
			{
				return false;
			}
			if (HasLocale != findChannelOptions.HasLocale || (HasLocale && !Locale.Equals(findChannelOptions.Locale)))
			{
				return false;
			}
			if (SearchAttribute.Count != findChannelOptions.SearchAttribute.Count)
			{
				return false;
			}
			for (int i = 0; i < SearchAttribute.Count; i++)
			{
				if (!SearchAttribute[i].Equals(findChannelOptions.SearchAttribute[i]))
				{
					return false;
				}
			}
			if (Reservation.Count != findChannelOptions.Reservation.Count)
			{
				return false;
			}
			for (int j = 0; j < Reservation.Count; j++)
			{
				if (!Reservation[j].Equals(findChannelOptions.Reservation[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static FindChannelOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FindChannelOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FindChannelOptions Deserialize(Stream stream, FindChannelOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FindChannelOptions DeserializeLengthDelimited(Stream stream)
		{
			FindChannelOptions findChannelOptions = new FindChannelOptions();
			DeserializeLengthDelimited(stream, findChannelOptions);
			return findChannelOptions;
		}

		public static FindChannelOptions DeserializeLengthDelimited(Stream stream, FindChannelOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FindChannelOptions Deserialize(Stream stream, FindChannelOptions instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.SearchAttribute == null)
			{
				instance.SearchAttribute = new List<bnet.protocol.v2.Attribute>();
			}
			if (instance.Reservation == null)
			{
				instance.Reservation = new List<AccountId>();
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
					if (instance.Type == null)
					{
						instance.Type = UniqueChannelType.DeserializeLengthDelimited(stream);
					}
					else
					{
						UniqueChannelType.DeserializeLengthDelimited(stream, instance.Type);
					}
					continue;
				case 18:
					instance.Identity = ProtocolParser.ReadString(stream);
					continue;
				case 29:
					instance.Locale = binaryReader.ReadUInt32();
					continue;
				case 34:
					instance.SearchAttribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 50:
					instance.Reservation.Add(AccountId.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, FindChannelOptions instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasType)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Type.GetSerializedSize());
				UniqueChannelType.Serialize(stream, instance.Type);
			}
			if (instance.HasIdentity)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identity));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.SearchAttribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.SearchAttribute)
				{
					stream.WriteByte(34);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.Reservation.Count <= 0)
			{
				return;
			}
			foreach (AccountId item2 in instance.Reservation)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
				AccountId.Serialize(stream, item2);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasType)
			{
				num++;
				uint serializedSize = Type.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasIdentity)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Identity);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasLocale)
			{
				num++;
				num += 4;
			}
			if (SearchAttribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in SearchAttribute)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (Reservation.Count > 0)
			{
				foreach (AccountId item2 in Reservation)
				{
					num++;
					uint serializedSize3 = item2.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
				return num;
			}
			return num;
		}
	}
}
