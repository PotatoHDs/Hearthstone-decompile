using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.channel.v1
{
	public class ListChannelsRequest : IProtoBuf
	{
		public bool HasAgentIdentity;

		private bnet.protocol.account.v1.Identity _AgentIdentity;

		public bnet.protocol.account.v1.Identity AgentIdentity
		{
			get
			{
				return _AgentIdentity;
			}
			set
			{
				_AgentIdentity = value;
				HasAgentIdentity = value != null;
			}
		}

		public ListChannelsOptions Options { get; set; }

		public bool IsInitialized => true;

		public void SetAgentIdentity(bnet.protocol.account.v1.Identity val)
		{
			AgentIdentity = val;
		}

		public void SetOptions(ListChannelsOptions val)
		{
			Options = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentIdentity)
			{
				num ^= AgentIdentity.GetHashCode();
			}
			return num ^ Options.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ListChannelsRequest listChannelsRequest = obj as ListChannelsRequest;
			if (listChannelsRequest == null)
			{
				return false;
			}
			if (HasAgentIdentity != listChannelsRequest.HasAgentIdentity || (HasAgentIdentity && !AgentIdentity.Equals(listChannelsRequest.AgentIdentity)))
			{
				return false;
			}
			if (!Options.Equals(listChannelsRequest.Options))
			{
				return false;
			}
			return true;
		}

		public static ListChannelsRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ListChannelsRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ListChannelsRequest Deserialize(Stream stream, ListChannelsRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ListChannelsRequest DeserializeLengthDelimited(Stream stream)
		{
			ListChannelsRequest listChannelsRequest = new ListChannelsRequest();
			DeserializeLengthDelimited(stream, listChannelsRequest);
			return listChannelsRequest;
		}

		public static ListChannelsRequest DeserializeLengthDelimited(Stream stream, ListChannelsRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ListChannelsRequest Deserialize(Stream stream, ListChannelsRequest instance, long limit)
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
					if (instance.AgentIdentity == null)
					{
						instance.AgentIdentity = bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream);
					}
					else
					{
						bnet.protocol.account.v1.Identity.DeserializeLengthDelimited(stream, instance.AgentIdentity);
					}
					continue;
				case 18:
					if (instance.Options == null)
					{
						instance.Options = ListChannelsOptions.DeserializeLengthDelimited(stream);
					}
					else
					{
						ListChannelsOptions.DeserializeLengthDelimited(stream, instance.Options);
					}
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

		public static void Serialize(Stream stream, ListChannelsRequest instance)
		{
			if (instance.HasAgentIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentIdentity.GetSerializedSize());
				bnet.protocol.account.v1.Identity.Serialize(stream, instance.AgentIdentity);
			}
			if (instance.Options == null)
			{
				throw new ArgumentNullException("Options", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.Options.GetSerializedSize());
			ListChannelsOptions.Serialize(stream, instance.Options);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentIdentity)
			{
				num++;
				uint serializedSize = AgentIdentity.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = Options.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1;
		}
	}
}
