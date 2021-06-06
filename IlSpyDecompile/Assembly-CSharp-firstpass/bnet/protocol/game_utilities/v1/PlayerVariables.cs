using System;
using System.Collections.Generic;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.game_utilities.v1
{
	public class PlayerVariables : IProtoBuf
	{
		public bool HasRating;

		private double _Rating;

		private List<Attribute> _Attribute = new List<Attribute>();

		public bnet.protocol.account.v1.Identity Identity { get; set; }

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

		public void SetIdentity(bnet.protocol.account.v1.Identity val)
		{
			Identity = val;
		}

		public void SetRating(double val)
		{
			Rating = val;
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
			hashCode ^= Identity.GetHashCode();
			if (HasRating)
			{
				hashCode ^= Rating.GetHashCode();
			}
			foreach (Attribute item in Attribute)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			PlayerVariables playerVariables = obj as PlayerVariables;
			if (playerVariables == null)
			{
				return false;
			}
			if (!Identity.Equals(playerVariables.Identity))
			{
				return false;
			}
			if (HasRating != playerVariables.HasRating || (HasRating && !Rating.Equals(playerVariables.Rating)))
			{
				return false;
			}
			if (Attribute.Count != playerVariables.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(playerVariables.Attribute[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static PlayerVariables ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerVariables>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PlayerVariables Deserialize(Stream stream, PlayerVariables instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PlayerVariables DeserializeLengthDelimited(Stream stream)
		{
			PlayerVariables playerVariables = new PlayerVariables();
			DeserializeLengthDelimited(stream, playerVariables);
			return playerVariables;
		}

		public static PlayerVariables DeserializeLengthDelimited(Stream stream, PlayerVariables instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PlayerVariables Deserialize(Stream stream, PlayerVariables instance, long limit)
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
						instance.Identity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.Identity);
					}
					continue;
				case 17:
					instance.Rating = binaryReader.ReadDouble();
					continue;
				case 26:
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

		public static void Serialize(Stream stream, PlayerVariables instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Identity == null)
			{
				throw new ArgumentNullException("Identity", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Identity.GetSerializedSize());
			bnet.protocol.account.v1.Identity.Serialize(stream, instance.Identity);
			if (instance.HasRating)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.Rating);
			}
			if (instance.Attribute.Count <= 0)
			{
				return;
			}
			foreach (Attribute item in instance.Attribute)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.Attribute.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Identity.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasRating)
			{
				num++;
				num += 8;
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
			return num + 1;
		}
	}
}
