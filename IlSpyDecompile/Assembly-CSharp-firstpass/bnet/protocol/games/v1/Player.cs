using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class Player : IProtoBuf
	{
		public bool HasIdentity;

		private Identity _Identity;

		private List<Attribute> _Attribute = new List<Attribute>();

		public bool HasRating;

		private double _Rating;

		public bool HasHost;

		private HostRoute _Host;

		public Identity Identity
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

		public List<Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public double Rating
		{
			get
			{
				return _Rating;
			}
			set
			{
				_Rating = value;
				HasRating = true;
			}
		}

		public HostRoute Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetIdentity(Identity val)
		{
			Identity = val;
		}

		public void AddAttribute(Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<Attribute> val)
		{
			Attribute = val;
		}

		public void SetRating(double val)
		{
			Rating = val;
		}

		public void SetHost(HostRoute val)
		{
			Host = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasRating)
			{
				num ^= Rating.GetHashCode();
			}
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			Player player = obj as Player;
			if (player == null)
			{
				return false;
			}
			if (HasIdentity != player.HasIdentity || (HasIdentity && !Identity.Equals(player.Identity)))
			{
				return false;
			}
			if (Attribute.Count != player.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(player.Attribute[i]))
				{
					return false;
				}
			}
			if (HasRating != player.HasRating || (HasRating && !Rating.Equals(player.Rating)))
			{
				return false;
			}
			if (HasHost != player.HasHost || (HasHost && !Host.Equals(player.Host)))
			{
				return false;
			}
			return true;
		}

		public static Player ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Player>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Player Deserialize(Stream stream, Player instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Player DeserializeLengthDelimited(Stream stream)
		{
			Player player = new Player();
			DeserializeLengthDelimited(stream, player);
			return player;
		}

		public static Player DeserializeLengthDelimited(Stream stream, Player instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Player Deserialize(Stream stream, Player instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
					if (instance.Identity == null)
					{
						instance.Identity = Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						Identity.DeserializeLengthDelimited(stream, instance.Identity);
					}
					continue;
				case 18:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 25:
					instance.Rating = binaryReader.ReadDouble();
					continue;
				case 34:
					if (instance.Host == null)
					{
						instance.Host = HostRoute.DeserializeLengthDelimited(stream);
					}
					else
					{
						HostRoute.DeserializeLengthDelimited(stream, instance.Host);
					}
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

		public static void Serialize(Stream stream, Player instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
				Identity.Serialize(stream, instance.Identity);
			}
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute item in instance.Attribute)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasRating)
			{
				stream.WriteByte(25);
				binaryWriter.Write(instance.Rating);
			}
			if (instance.HasHost)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				HostRoute.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIdentity)
			{
				num++;
				uint serializedSize = Identity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasRating)
			{
				num++;
				num += 8;
			}
			if (HasHost)
			{
				num++;
				uint serializedSize3 = Host.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}
	}
}
