using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BobNetProto
{
	public class DebugPaneDelItems : IProtoBuf
	{
		public enum PacketID
		{
			ID = 143
		}

		public class DebugPaneDelItem : IProtoBuf
		{
			public string Name { get; set; }

			public override int GetHashCode()
			{
				return GetType().GetHashCode() ^ Name.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				DebugPaneDelItem debugPaneDelItem = obj as DebugPaneDelItem;
				if (debugPaneDelItem == null)
				{
					return false;
				}
				if (!Name.Equals(debugPaneDelItem.Name))
				{
					return false;
				}
				return true;
			}

			public void Deserialize(Stream stream)
			{
				Deserialize(stream, this);
			}

			public static DebugPaneDelItem Deserialize(Stream stream, DebugPaneDelItem instance)
			{
				return Deserialize(stream, instance, -1L);
			}

			public static DebugPaneDelItem DeserializeLengthDelimited(Stream stream)
			{
				DebugPaneDelItem debugPaneDelItem = new DebugPaneDelItem();
				DeserializeLengthDelimited(stream, debugPaneDelItem);
				return debugPaneDelItem;
			}

			public static DebugPaneDelItem DeserializeLengthDelimited(Stream stream, DebugPaneDelItem instance)
			{
				long num = ProtocolParser.ReadUInt32(stream);
				num += stream.Position;
				return Deserialize(stream, instance, num);
			}

			public static DebugPaneDelItem Deserialize(Stream stream, DebugPaneDelItem instance, long limit)
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

			public static void Serialize(Stream stream, DebugPaneDelItem instance)
			{
				if (instance.Name == null)
				{
					throw new ArgumentNullException("Name", "Required by proto specification.");
				}
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
			}

			public uint GetSerializedSize()
			{
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Name);
				return 0 + (ProtocolParser.SizeOfUInt32(byteCount) + byteCount) + 1;
			}
		}

		private List<DebugPaneDelItem> _Items = new List<DebugPaneDelItem>();

		public List<DebugPaneDelItem> Items
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
			foreach (DebugPaneDelItem item in Items)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			DebugPaneDelItems debugPaneDelItems = obj as DebugPaneDelItems;
			if (debugPaneDelItems == null)
			{
				return false;
			}
			if (Items.Count != debugPaneDelItems.Items.Count)
			{
				return false;
			}
			for (int i = 0; i < Items.Count; i++)
			{
				if (!Items[i].Equals(debugPaneDelItems.Items[i]))
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

		public static DebugPaneDelItems Deserialize(Stream stream, DebugPaneDelItems instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static DebugPaneDelItems DeserializeLengthDelimited(Stream stream)
		{
			DebugPaneDelItems debugPaneDelItems = new DebugPaneDelItems();
			DeserializeLengthDelimited(stream, debugPaneDelItems);
			return debugPaneDelItems;
		}

		public static DebugPaneDelItems DeserializeLengthDelimited(Stream stream, DebugPaneDelItems instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static DebugPaneDelItems Deserialize(Stream stream, DebugPaneDelItems instance, long limit)
		{
			if (instance.Items == null)
			{
				instance.Items = new List<DebugPaneDelItem>();
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
					instance.Items.Add(DebugPaneDelItem.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, DebugPaneDelItems instance)
		{
			if (instance.Items.Count <= 0)
			{
				return;
			}
			foreach (DebugPaneDelItem item in instance.Items)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				DebugPaneDelItem.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Items.Count > 0)
			{
				foreach (DebugPaneDelItem item in Items)
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
