using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class ListChannelCountResponse : IProtoBuf
	{
		private List<ChannelCount> _Channel = new List<ChannelCount>();

		public List<ChannelCount> Channel
		{
			get
			{
				return _Channel;
			}
			set
			{
				_Channel = value;
			}
		}

		public List<ChannelCount> ChannelList => _Channel;

		public int ChannelCount => _Channel.Count;

		public bool IsInitialized => true;

		public void AddChannel(ChannelCount val)
		{
			_Channel.Add(val);
		}

		public void ClearChannel()
		{
			_Channel.Clear();
		}

		public void SetChannel(List<ChannelCount> val)
		{
			Channel = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ChannelCount item in Channel)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ListChannelCountResponse listChannelCountResponse = obj as ListChannelCountResponse;
			if (listChannelCountResponse == null)
			{
				return false;
			}
			if (Channel.Count != listChannelCountResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < Channel.Count; i++)
			{
				if (!Channel[i].Equals(listChannelCountResponse.Channel[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ListChannelCountResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelCountResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ListChannelCountResponse Deserialize(Stream stream, ListChannelCountResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ListChannelCountResponse DeserializeLengthDelimited(Stream stream)
		{
			ListChannelCountResponse listChannelCountResponse = new ListChannelCountResponse();
			DeserializeLengthDelimited(stream, listChannelCountResponse);
			return listChannelCountResponse;
		}

		public static ListChannelCountResponse DeserializeLengthDelimited(Stream stream, ListChannelCountResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ListChannelCountResponse Deserialize(Stream stream, ListChannelCountResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<ChannelCount>();
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
					instance.Channel.Add(bnet.protocol.channel.v1.ChannelCount.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ListChannelCountResponse instance)
		{
			if (instance.Channel.Count <= 0)
			{
				return;
			}
			foreach (ChannelCount item in instance.Channel)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.channel.v1.ChannelCount.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Channel.Count > 0)
			{
				foreach (ChannelCount item in Channel)
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
