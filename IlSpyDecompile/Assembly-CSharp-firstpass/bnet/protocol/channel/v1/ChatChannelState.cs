using System.IO;
using System.Text;

namespace bnet.protocol.channel.v1
{
	public class ChatChannelState : IProtoBuf
	{
		public bool HasIdentity;

		private string _Identity;

		public bool HasLocale;

		private uint _Locale;

		public bool HasPublic;

		private bool _Public;

		public bool HasBucketIndex;

		private uint _BucketIndex;

		public string Identity
		{
			get
			{
				return _Identity;
			}
			set
			{
				_Identity = value;
				HasIdentity = value != null;
			}
		}

		public uint Locale
		{
			get
			{
				return _Locale;
			}
			set
			{
				_Locale = value;
				HasLocale = true;
			}
		}

		public bool Public
		{
			get
			{
				return _Public;
			}
			set
			{
				_Public = value;
				HasPublic = true;
			}
		}

		public uint BucketIndex
		{
			get
			{
				return _BucketIndex;
			}
			set
			{
				_BucketIndex = value;
				HasBucketIndex = true;
			}
		}

		public bool IsInitialized => true;

		public void SetIdentity(string val)
		{
			Identity = val;
		}

		public void SetLocale(uint val)
		{
			Locale = val;
		}

		public void SetPublic(bool val)
		{
			Public = val;
		}

		public void SetBucketIndex(uint val)
		{
			BucketIndex = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIdentity)
			{
				num ^= Identity.GetHashCode();
			}
			if (HasLocale)
			{
				num ^= Locale.GetHashCode();
			}
			if (HasPublic)
			{
				num ^= Public.GetHashCode();
			}
			if (HasBucketIndex)
			{
				num ^= BucketIndex.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChatChannelState chatChannelState = obj as ChatChannelState;
			if (chatChannelState == null)
			{
				return false;
			}
			if (HasIdentity != chatChannelState.HasIdentity || (HasIdentity && !Identity.Equals(chatChannelState.Identity)))
			{
				return false;
			}
			if (HasLocale != chatChannelState.HasLocale || (HasLocale && !Locale.Equals(chatChannelState.Locale)))
			{
				return false;
			}
			if (HasPublic != chatChannelState.HasPublic || (HasPublic && !Public.Equals(chatChannelState.Public)))
			{
				return false;
			}
			if (HasBucketIndex != chatChannelState.HasBucketIndex || (HasBucketIndex && !BucketIndex.Equals(chatChannelState.BucketIndex)))
			{
				return false;
			}
			return true;
		}

		public static ChatChannelState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChatChannelState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChatChannelState Deserialize(Stream stream, ChatChannelState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChatChannelState DeserializeLengthDelimited(Stream stream)
		{
			ChatChannelState chatChannelState = new ChatChannelState();
			DeserializeLengthDelimited(stream, chatChannelState);
			return chatChannelState;
		}

		public static ChatChannelState DeserializeLengthDelimited(Stream stream, ChatChannelState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChatChannelState Deserialize(Stream stream, ChatChannelState instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			instance.Public = false;
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
					instance.Identity = ProtocolParser.ReadString(stream);
					continue;
				case 29:
					instance.Locale = binaryReader.ReadUInt32();
					continue;
				case 32:
					instance.Public = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.BucketIndex = ProtocolParser.ReadUInt32(stream);
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

		public static void Serialize(Stream stream, ChatChannelState instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasIdentity)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Identity));
			}
			if (instance.HasLocale)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.Locale);
			}
			if (instance.HasPublic)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.Public);
			}
			if (instance.HasBucketIndex)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.BucketIndex);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIdentity)
			{
				num++;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(Identity);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (HasLocale)
			{
				num++;
				num += 4;
			}
			if (HasPublic)
			{
				num++;
				num++;
			}
			if (HasBucketIndex)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(BucketIndex);
			}
			return num;
		}
	}
}
