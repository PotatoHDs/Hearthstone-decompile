using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class Team : IProtoBuf
	{
		private List<Attribute> _Attribute = new List<Attribute>();

		public uint MaxPlayers { get; set; }

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

		public bool IsInitialized => true;

		public void SetMaxPlayers(uint val)
		{
			MaxPlayers = val;
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

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= MaxPlayers.GetHashCode();
			foreach (Attribute item in Attribute)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			Team team = obj as Team;
			if (team == null)
			{
				return false;
			}
			if (!MaxPlayers.Equals(team.MaxPlayers))
			{
				return false;
			}
			if (Attribute.Count != team.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(team.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static Team ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<Team>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Team Deserialize(Stream stream, Team instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Team DeserializeLengthDelimited(Stream stream)
		{
			Team team = new Team();
			DeserializeLengthDelimited(stream, team);
			return team;
		}

		public static Team DeserializeLengthDelimited(Stream stream, Team instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Team Deserialize(Stream stream, Team instance, long limit)
		{
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
				case 8:
					instance.MaxPlayers = ProtocolParser.ReadUInt32(stream);
					continue;
				case 18:
					instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, Team instance)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.MaxPlayers);
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (Attribute item in instance.Attribute)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt32(MaxPlayers);
			if (Attribute.Count > 0)
			{
				foreach (Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num + 1;
		}
	}
}
