using System.Collections.Generic;
using System.IO;
using System.Text;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class PublicChannelState : IProtoBuf
	{
		public bool HasIdentity;

		private string _Identity;

		public bool HasLocale;

		private uint _Locale;

		private List<bnet.protocol.v2.Attribute> _SearchAttribute = new List<bnet.protocol.v2.Attribute>();

		private List<AccountId> _Reservation = new List<AccountId>();

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
			PublicChannelState publicChannelState = obj as PublicChannelState;
			if (publicChannelState == null)
			{
				return false;
			}
			if (HasIdentity != publicChannelState.HasIdentity || (HasIdentity && !Identity.Equals(publicChannelState.Identity)))
			{
				return false;
			}
			if (HasLocale != publicChannelState.HasLocale || (HasLocale && !Locale.Equals(publicChannelState.Locale)))
			{
				return false;
			}
			if (SearchAttribute.Count != publicChannelState.SearchAttribute.Count)
			{
				return false;
			}
			for (int i = 0; i < SearchAttribute.Count; i++)
			{
				if (!SearchAttribute[i].Equals(publicChannelState.SearchAttribute[i]))
				{
					return false;
				}
			}
			if (Reservation.Count != publicChannelState.Reservation.Count)
			{
				return false;
			}
			for (int j = 0; j < Reservation.Count; j++)
			{
				if (!Reservation[j].Equals(publicChannelState.Reservation[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static PublicChannelState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PublicChannelState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PublicChannelState Deserialize(Stream stream, PublicChannelState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PublicChannelState DeserializeLengthDelimited(Stream stream)
		{
			PublicChannelState publicChannelState = new PublicChannelState();
			DeserializeLengthDelimited(stream, publicChannelState);
			return publicChannelState;
		}

		public static PublicChannelState DeserializeLengthDelimited(Stream stream, PublicChannelState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PublicChannelState Deserialize(Stream stream, PublicChannelState instance, long limit)
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
					instance.Identity = ProtocolParser.ReadString(stream);
					continue;
				case 21:
					instance.Locale = binaryReader.ReadUInt32();
					continue;
				case 26:
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

		public static void Serialize(Stream stream, PublicChannelState instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identity));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(21);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.SearchAttribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.SearchAttribute)
				{
					stream.WriteByte(26);
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
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (Reservation.Count > 0)
			{
				foreach (AccountId item2 in Reservation)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
