using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BobNetProto
{
	public class DebugConsoleZones : IProtoBuf
	{
		public enum PacketID
		{
			ID = 148
		}

		public class DebugConsoleZone : IProtoBuf
		{
			public string Name { get; set; }

			public uint Id { get; set; }

			public override int GetHashCode()
			{
				return GetType().GetHashCode() ^ Name.GetHashCode() ^ Id.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				DebugConsoleZone debugConsoleZone = obj as DebugConsoleZone;
				if (debugConsoleZone == null)
				{
					return false;
				}
				if (!Name.Equals(debugConsoleZone.Name))
				{
					return false;
				}
				if (!Id.Equals(debugConsoleZone.Id))
				{
					return false;
				}
				return true;
			}

			public void Deserialize(Stream stream)
			{
				Deserialize(stream, this);
			}

			public static DebugConsoleZone Deserialize(Stream stream, DebugConsoleZone instance)
			{
				return Deserialize(stream, instance, -1L);
			}

			public static DebugConsoleZone DeserializeLengthDelimited(Stream stream)
			{
				DebugConsoleZone debugConsoleZone = new DebugConsoleZone();
				DeserializeLengthDelimited(stream, debugConsoleZone);
				return debugConsoleZone;
			}

			public static DebugConsoleZone DeserializeLengthDelimited(Stream stream, DebugConsoleZone instance)
			{
				long num = ProtocolParser.ReadUInt32(stream);
				num += stream.Position;
				return Deserialize(stream, instance, num);
			}

			public static DebugConsoleZone Deserialize(Stream stream, DebugConsoleZone instance, long limit)
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
					case 10:
						instance.Name = ProtocolParser.ReadString(stream);
						continue;
					case 16:
						instance.Id = ProtocolParser.ReadUInt32(stream);
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

			public static void Serialize(Stream stream, DebugConsoleZone instance)
			{
				if (instance.Name == null)
				{
					throw new ArgumentNullException("Name", "Required by proto specification.");
				}
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
				stream.WriteByte(16);
				ProtocolParser.WriteUInt32(stream, instance.Id);
			}

			public uint GetSerializedSize()
			{
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + ProtocolParser.SizeOfUInt32(Id) + 2;
			}
		}

		private List<DebugConsoleZone> _Zones = new List<DebugConsoleZone>();

		public List<DebugConsoleZone> Zones
		{
			get
			{
				return _Zones;
			}
			set
			{
				_Zones = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (DebugConsoleZone zone in Zones)
			{
				num ^= zone.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DebugConsoleZones debugConsoleZones = obj as DebugConsoleZones;
			if (debugConsoleZones == null)
			{
				return false;
			}
			if (Zones.Count != debugConsoleZones.Zones.Count)
			{
				return false;
			}
			for (int i = 0; i < Zones.Count; i++)
			{
				if (!Zones[i].Equals(debugConsoleZones.Zones[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static DebugConsoleZones Deserialize(Stream stream, DebugConsoleZones instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugConsoleZones DeserializeLengthDelimited(Stream stream)
		{
			DebugConsoleZones debugConsoleZones = new DebugConsoleZones();
			DeserializeLengthDelimited(stream, debugConsoleZones);
			return debugConsoleZones;
		}

		public static DebugConsoleZones DeserializeLengthDelimited(Stream stream, DebugConsoleZones instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugConsoleZones Deserialize(Stream stream, DebugConsoleZones instance, long limit)
		{
			if (instance.Zones == null)
			{
				instance.Zones = new List<DebugConsoleZone>();
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
					instance.Zones.Add(DebugConsoleZone.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DebugConsoleZones instance)
		{
			if (instance.Zones.Count <= 0)
			{
				return;
			}
			foreach (DebugConsoleZone zone in instance.Zones)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, zone.GetSerializedSize());
				DebugConsoleZone.Serialize(stream, zone);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Zones.Count > 0)
			{
				foreach (DebugConsoleZone zone in Zones)
				{
					num++;
					uint serializedSize = zone.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
