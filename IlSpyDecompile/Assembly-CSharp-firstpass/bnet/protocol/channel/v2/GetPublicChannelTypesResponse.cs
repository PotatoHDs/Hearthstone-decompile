using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v2
{
	public class GetPublicChannelTypesResponse : IProtoBuf
	{
		private List<PublicChannelType> _Channel = new List<PublicChannelType>();

		public bool HasContinuation;

		private ulong _Continuation;

		public List<PublicChannelType> Channel
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

		public List<PublicChannelType> ChannelList => _Channel;

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

		public void AddChannel(PublicChannelType val)
		{
			_Channel.Add(val);
		}

		public void ClearChannel()
		{
			_Channel.Clear();
		}

		public void SetChannel(List<PublicChannelType> val)
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
			foreach (PublicChannelType item in Channel)
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
			GetPublicChannelTypesResponse getPublicChannelTypesResponse = obj as GetPublicChannelTypesResponse;
			if (getPublicChannelTypesResponse == null)
			{
				return false;
			}
			if (Channel.Count != getPublicChannelTypesResponse.Channel.Count)
			{
				return false;
			}
			for (int i = 0; i < Channel.Count; i++)
			{
				if (!Channel[i].Equals(getPublicChannelTypesResponse.Channel[i]))
				{
					return false;
				}
			}
			if (HasContinuation != getPublicChannelTypesResponse.HasContinuation || (HasContinuation && !Continuation.Equals(getPublicChannelTypesResponse.Continuation)))
			{
				return false;
			}
			return true;
		}

		public static GetPublicChannelTypesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPublicChannelTypesResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetPublicChannelTypesResponse Deserialize(Stream stream, GetPublicChannelTypesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetPublicChannelTypesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetPublicChannelTypesResponse getPublicChannelTypesResponse = new GetPublicChannelTypesResponse();
			DeserializeLengthDelimited(stream, getPublicChannelTypesResponse);
			return getPublicChannelTypesResponse;
		}

		public static GetPublicChannelTypesResponse DeserializeLengthDelimited(Stream stream, GetPublicChannelTypesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetPublicChannelTypesResponse Deserialize(Stream stream, GetPublicChannelTypesResponse instance, long limit)
		{
			if (instance.Channel == null)
			{
				instance.Channel = new List<PublicChannelType>();
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
					instance.Channel.Add(PublicChannelType.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, GetPublicChannelTypesResponse instance)
		{
			if (instance.Channel.Count > 0)
			{
				foreach (PublicChannelType item in instance.Channel)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					PublicChannelType.Serialize(stream, item);
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
				foreach (PublicChannelType item in Channel)
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
