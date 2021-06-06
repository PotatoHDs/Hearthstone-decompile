using System.IO;

namespace bnet.protocol
{
	public class ViewMarker : IProtoBuf
	{
		public bool HasLastReadTime;

		private ulong _LastReadTime;

		public bool HasLastMessageTime;

		private ulong _LastMessageTime;

		public ulong LastReadTime
		{
			get
			{
				return _LastReadTime;
			}
			set
			{
				_LastReadTime = value;
				HasLastReadTime = true;
			}
		}

		public ulong LastMessageTime
		{
			get
			{
				return _LastMessageTime;
			}
			set
			{
				_LastMessageTime = value;
				HasLastMessageTime = true;
			}
		}

		public bool IsInitialized => true;

		public void SetLastReadTime(ulong val)
		{
			LastReadTime = val;
		}

		public void SetLastMessageTime(ulong val)
		{
			LastMessageTime = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasLastReadTime)
			{
				num ^= LastReadTime.GetHashCode();
			}
			if (HasLastMessageTime)
			{
				num ^= LastMessageTime.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ViewMarker viewMarker = obj as ViewMarker;
			if (viewMarker == null)
			{
				return false;
			}
			if (HasLastReadTime != viewMarker.HasLastReadTime || (HasLastReadTime && !LastReadTime.Equals(viewMarker.LastReadTime)))
			{
				return false;
			}
			if (HasLastMessageTime != viewMarker.HasLastMessageTime || (HasLastMessageTime && !LastMessageTime.Equals(viewMarker.LastMessageTime)))
			{
				return false;
			}
			return true;
		}

		public static ViewMarker ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ViewMarker>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ViewMarker Deserialize(Stream stream, ViewMarker instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ViewMarker DeserializeLengthDelimited(Stream stream)
		{
			ViewMarker viewMarker = new ViewMarker();
			DeserializeLengthDelimited(stream, viewMarker);
			return viewMarker;
		}

		public static ViewMarker DeserializeLengthDelimited(Stream stream, ViewMarker instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ViewMarker Deserialize(Stream stream, ViewMarker instance, long limit)
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
				case 8:
					instance.LastReadTime = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.LastMessageTime = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ViewMarker instance)
		{
			if (instance.HasLastReadTime)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.LastReadTime);
			}
			if (instance.HasLastMessageTime)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.LastMessageTime);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasLastReadTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(LastReadTime);
			}
			if (HasLastMessageTime)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(LastMessageTime);
			}
			return num;
		}
	}
}
