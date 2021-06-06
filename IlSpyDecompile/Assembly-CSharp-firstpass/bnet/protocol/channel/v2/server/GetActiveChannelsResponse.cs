using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v2.server
{
	public class GetActiveChannelsResponse : IProtoBuf
	{
		private List<ActiveChannelDescription> _Channel = new List<ActiveChannelDescription>();

		public bool HasContinuation;

		private ulong _Continuation;

		public List<ActiveChannelDescription> Channel
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

		public List<ActiveChannelDescription> ChannelList => _Channel;

		public int ChannelCount => _Channel.Count;

		public ulong Continuation
		{
			get
			{
				return _Continuation;
			}
			set
			{
				_Continuation = value;
				HasContinuation = true;
			}
		}

		public bool IsInitialized => true;

		public void AddChannel(ActiveChannelDescription val)
		{
			_Channel.Add(val);
		}

		public void ClearChannel()
		{
			_Channel.Clear();
		}

		public void SetChannel(List<ActiveChannelDescription> val)
		{
			Channel = val;
		}

		public void SetContinuation(ulong val)
		{
			Continuation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ActiveChannelDescription item in Channel)
			{
				num ^= item.GetHashCode();
			}
			if (HasContinuation)
			{
				num ^= Continuation.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetActiveChannelsResponse getActiveChannelsResponse = obj as GetActiveChannelsResponse;
			if (getActiveChannelsResponse == null)
			{
				return false;
			}
			if (Channel.Count != getActiveChannelsResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < Channel.Count; i++)
			{
				if (!Channel[i].Equals(getActiveChannelsResponse.Channel[i]))
				{
					return false;
				}
			}
			if (HasContinuation != getActiveChannelsResponse.HasContinuation || (HasContinuation && !Continuation.Equals(getActiveChannelsResponse.Continuation)))
			{
				return false;
			}
			return true;
		}

		public static GetActiveChannelsResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetActiveChannelsResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetActiveChannelsResponse Deserialize(Stream stream, GetActiveChannelsResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetActiveChannelsResponse DeserializeLengthDelimited(Stream stream)
		{
			GetActiveChannelsResponse getActiveChannelsResponse = new GetActiveChannelsResponse();
			DeserializeLengthDelimited(stream, getActiveChannelsResponse);
			return getActiveChannelsResponse;
		}

		public static GetActiveChannelsResponse DeserializeLengthDelimited(Stream stream, GetActiveChannelsResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetActiveChannelsResponse Deserialize(Stream stream, GetActiveChannelsResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<ActiveChannelDescription>();
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
					instance.Channel.Add(ActiveChannelDescription.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.Continuation = ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, GetActiveChannelsResponse instance)
		{
			if (instance.Channel.Count > 0)
			{
				foreach (ActiveChannelDescription item in instance.Channel)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					ActiveChannelDescription.Serialize(stream, item);
				}
			}
			if (instance.HasContinuation)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, instance.Continuation);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Channel.Count > 0)
			{
				foreach (ActiveChannelDescription item in Channel)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasContinuation)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(Continuation);
			}
			return num;
		}
	}
}
