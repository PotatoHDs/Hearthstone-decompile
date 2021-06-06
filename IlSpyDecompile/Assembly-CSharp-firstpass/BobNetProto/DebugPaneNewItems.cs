using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BobNetProto
{
	public class DebugPaneNewItems : IProtoBuf
	{
		public enum PacketID
		{
			ID = 142
		}

		public class DebugPaneNewItem : IProtoBuf
		{
			public string Name { get; set; }

			public string Value { get; set; }

			public override int GetHashCode()
			{
				return GetType().GetHashCode() ^ Name.GetHashCode() ^ Value.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				DebugPaneNewItem debugPaneNewItem = obj as DebugPaneNewItem;
				if (debugPaneNewItem == null)
				{
					return false;
				}
				if (!Name.Equals(debugPaneNewItem.Name))
				{
					return false;
				}
				if (!Value.Equals(debugPaneNewItem.Value))
				{
					return false;
				}
				return true;
			}

			public void Deserialize(Stream stream)
			{
				Deserialize(stream, this);
			}

			public static DebugPaneNewItem Deserialize(Stream stream, DebugPaneNewItem instance)
			{
				return Deserialize(stream, instance, -1L);
			}

			public static DebugPaneNewItem DeserializeLengthDelimited(Stream stream)
			{
				DebugPaneNewItem debugPaneNewItem = new DebugPaneNewItem();
				DeserializeLengthDelimited(stream, debugPaneNewItem);
				return debugPaneNewItem;
			}

			public static DebugPaneNewItem DeserializeLengthDelimited(Stream stream, DebugPaneNewItem instance)
			{
				long num = ProtocolParser.ReadUInt32(stream);
				num += stream.Position;
				return Deserialize(stream, instance, num);
			}

			public static DebugPaneNewItem Deserialize(Stream stream, DebugPaneNewItem instance, long limit)
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
					case 18:
						instance.Value = ProtocolParser.ReadString(stream);
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

			public static void Serialize(Stream stream, DebugPaneNewItem instance)
			{
				if (instance.Name == null)
				{
					throw new ArgumentNullException("Name", "Required by proto specification.");
				}
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
				if (instance.Value == null)
				{
					throw new ArgumentNullException("Value", "Required by proto specification.");
				}
				stream.WriteByte(18);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Value));
			}

			public uint GetSerializedSize()
			{
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				uint num = 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount);
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(Value);
				return num + (ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2) + 2;
			}
		}

		private List<DebugPaneNewItem> _Items = new List<DebugPaneNewItem>();

		public List<DebugPaneNewItem> Items
		{
			get
			{
				return _Items;
			}
			set
			{
				_Items = value;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (DebugPaneNewItem item in Items)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DebugPaneNewItems debugPaneNewItems = obj as DebugPaneNewItems;
			if (debugPaneNewItems == null)
			{
				return false;
			}
			if (Items.Count != debugPaneNewItems.Items.Count)
			{
				return false;
			}
			for (int i = 0; i < Items.Count; i++)
			{
				if (!Items[i].Equals(debugPaneNewItems.Items[i]))
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

		public static DebugPaneNewItems Deserialize(Stream stream, DebugPaneNewItems instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugPaneNewItems DeserializeLengthDelimited(Stream stream)
		{
			DebugPaneNewItems debugPaneNewItems = new DebugPaneNewItems();
			DeserializeLengthDelimited(stream, debugPaneNewItems);
			return debugPaneNewItems;
		}

		public static DebugPaneNewItems DeserializeLengthDelimited(Stream stream, DebugPaneNewItems instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugPaneNewItems Deserialize(Stream stream, DebugPaneNewItems instance, long limit)
		{
			if (instance.Items == null)
			{
				instance.Items = new List<DebugPaneNewItem>();
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
					instance.Items.Add(DebugPaneNewItem.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DebugPaneNewItems instance)
		{
			if (instance.Items.Count <= 0)
			{
				return;
			}
			foreach (DebugPaneNewItem item in instance.Items)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				DebugPaneNewItem.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Items.Count > 0)
			{
				foreach (DebugPaneNewItem item in Items)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
				return num;
			}
			return num;
		}
	}
}
