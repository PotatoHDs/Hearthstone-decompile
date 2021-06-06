using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class FactoryUpdateNotification : IProtoBuf
	{
		public static class Types
		{
			public enum Operation
			{
				ADD = 1,
				REMOVE,
				CHANGE
			}
		}

		public bool HasProgram;

		private uint _Program;

		private List<HostRoute> _Hosts = new List<HostRoute>();

		public Types.Operation Op { get; set; }

		public GameFactoryDescription Description { get; set; }

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

		public List<HostRoute> Hosts
		{
			get
			{
				return _Hosts;
			}
			set
			{
				_Hosts = value;
			}
		}

		public List<HostRoute> HostsList => _Hosts;

		public int HostsCount => _Hosts.Count;

		public bool IsInitialized => true;

		public void SetOp(Types.Operation val)
		{
			Op = val;
		}

		public void SetDescription(GameFactoryDescription val)
		{
			Description = val;
		}

		public void SetProgram(uint val)
		{
			Program = val;
		}

		public void AddHosts(HostRoute val)
		{
			_Hosts.Add(val);
		}

		public void ClearHosts()
		{
			_Hosts.Clear();
		}

		public void SetHosts(List<HostRoute> val)
		{
			Hosts = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Op.GetHashCode();
			hashCode ^= Description.GetHashCode();
			if (HasProgram)
			{
				hashCode ^= Program.GetHashCode();
			}
			foreach (HostRoute host in Hosts)
			{
				hashCode ^= host.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			FactoryUpdateNotification factoryUpdateNotification = obj as FactoryUpdateNotification;
			if (factoryUpdateNotification == null)
			{
				return false;
			}
			if (!Op.Equals(factoryUpdateNotification.Op))
			{
				return false;
			}
			if (!Description.Equals(factoryUpdateNotification.Description))
			{
				return false;
			}
			if (HasProgram != factoryUpdateNotification.HasProgram || (HasProgram && !Program.Equals(factoryUpdateNotification.Program)))
			{
				return false;
			}
			if (Hosts.Count != factoryUpdateNotification.Hosts.Count)
			{
				return false;
			}
			for (int i = 0; i < Hosts.Count; i++)
			{
				if (!Hosts[i].Equals(factoryUpdateNotification.Hosts[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static FactoryUpdateNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<FactoryUpdateNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static FactoryUpdateNotification Deserialize(Stream stream, FactoryUpdateNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static FactoryUpdateNotification DeserializeLengthDelimited(Stream stream)
		{
			FactoryUpdateNotification factoryUpdateNotification = new FactoryUpdateNotification();
			DeserializeLengthDelimited(stream, factoryUpdateNotification);
			return factoryUpdateNotification;
		}

		public static FactoryUpdateNotification DeserializeLengthDelimited(Stream stream, FactoryUpdateNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static FactoryUpdateNotification Deserialize(Stream stream, FactoryUpdateNotification instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Hosts == null)
			{
				instance.Hosts = new List<HostRoute>();
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
					instance.Op = (Types.Operation)ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					if (instance.Description == null)
					{
						instance.Description = GameFactoryDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameFactoryDescription.DeserializeLengthDelimited(stream, instance.Description);
					}
					continue;
				case 29:
					instance.Program = binaryReader.ReadUInt32();
					continue;
				case 34:
					instance.Hosts.Add(HostRoute.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, FactoryUpdateNotification instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.Op);
			if (instance.Description == null)
			{
				throw new ArgumentNullException("Description", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Description.GetSerializedSize());
			GameFactoryDescription.Serialize(stream, instance.Description);
			if (instance.HasProgram)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Program);
			}
			if (instance.Hosts.Count <= 0)
			{
				return;
			}
			foreach (HostRoute host in instance.Hosts)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, host.GetSerializedSize());
				HostRoute.Serialize(stream, host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += ProtocolParser.SizeOfUInt64((ulong)Op);
			uint serializedSize = Description.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (HasProgram)
			{
				num++;
				num += 4;
			}
			if (Hosts.Count > 0)
			{
				foreach (HostRoute host in Hosts)
				{
					num++;
					uint serializedSize2 = host.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num + 2;
		}
	}
}
