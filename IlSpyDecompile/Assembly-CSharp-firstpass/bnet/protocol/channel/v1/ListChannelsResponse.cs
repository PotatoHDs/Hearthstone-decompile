using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class ListChannelsResponse : IProtoBuf
	{
		private List<ChannelDescription> _Channel = new List<ChannelDescription>();

		public List<ChannelDescription> Channel
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

		public List<ChannelDescription> ChannelList => _Channel;

		public int ChannelCount => _Channel.Count;

		public bool IsInitialized => true;

		public void AddChannel(ChannelDescription val)
		{
			_Channel.Add(val);
		}

		public void ClearChannel()
		{
			_Channel.Clear();
		}

		public void SetChannel(List<ChannelDescription> val)
		{
			Channel = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ChannelDescription item in Channel)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ListChannelsResponse listChannelsResponse = obj as ListChannelsResponse;
			if (listChannelsResponse == null)
			{
				return false;
			}
			if (Channel.Count != listChannelsResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < Channel.Count; i++)
			{
				if (!Channel[i].Equals(listChannelsResponse.Channel[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ListChannelsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ListChannelsResponse Deserialize(Stream stream, ListChannelsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ListChannelsResponse DeserializeLengthDelimited(Stream stream)
		{
			ListChannelsResponse listChannelsResponse = new ListChannelsResponse();
			DeserializeLengthDelimited(stream, listChannelsResponse);
			return listChannelsResponse;
		}

		public static ListChannelsResponse DeserializeLengthDelimited(Stream stream, ListChannelsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ListChannelsResponse Deserialize(Stream stream, ListChannelsResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<ChannelDescription>();
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
					instance.Channel.Add(ChannelDescription.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ListChannelsResponse instance)
		{
			if (instance.Channel.Count <= 0)
			{
				return;
			}
			foreach (ChannelDescription item in instance.Channel)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				ChannelDescription.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Channel.Count > 0)
			{
				foreach (ChannelDescription item in Channel)
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
