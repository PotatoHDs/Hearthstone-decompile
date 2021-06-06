using System.Collections.Generic;
using System.IO;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bnet.protocol.channel.v2
{
	public class ChannelStateOptions : IProtoBuf
	{
		private List<bnet.protocol.v2.Attribute> _Attribute = new List<bnet.protocol.v2.Attribute>();

		public bool HasPrivacyLevel;

		private PrivacyLevel _PrivacyLevel;

		public List<bnet.protocol.v2.Attribute> Attribute
		{
			get
			{
				return _Attribute;
			}
			set
			{
				_Attribute = value;
			}
		}

		public List<bnet.protocol.v2.Attribute> AttributeList => _Attribute;

		public int AttributeCount => _Attribute.Count;

		public PrivacyLevel PrivacyLevel
		{
			get
			{
				return _PrivacyLevel;
			}
			set
			{
				_PrivacyLevel = value;
				HasPrivacyLevel = true;
			}
		}

		public bool IsInitialized => true;

		public void AddAttribute(bnet.protocol.v2.Attribute val)
		{
			_Attribute.Add(val);
		}

		public void ClearAttribute()
		{
			_Attribute.Clear();
		}

		public void SetAttribute(List<bnet.protocol.v2.Attribute> val)
		{
			Attribute = val;
		}

		public void SetPrivacyLevel(PrivacyLevel val)
		{
			PrivacyLevel = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (bnet.protocol.v2.Attribute item in Attribute)
			{
				num ^= item.GetHashCode();
			}
			if (HasPrivacyLevel)
			{
				num ^= PrivacyLevel.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelStateOptions channelStateOptions = obj as ChannelStateOptions;
			if (channelStateOptions == null)
			{
				return false;
			}
			if (Attribute.Count != channelStateOptions.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < Attribute.Count; i++)
			{
				if (!Attribute[i].Equals(channelStateOptions.Attribute[i]))
				{
					return false;
				}
			}
			if (HasPrivacyLevel != channelStateOptions.HasPrivacyLevel || (HasPrivacyLevel && !PrivacyLevel.Equals(channelStateOptions.PrivacyLevel)))
			{
				return false;
			}
			return true;
		}

		public static ChannelStateOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelStateOptions>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelStateOptions Deserialize(Stream stream, ChannelStateOptions instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelStateOptions DeserializeLengthDelimited(Stream stream)
		{
			ChannelStateOptions channelStateOptions = new ChannelStateOptions();
			DeserializeLengthDelimited(stream, channelStateOptions);
			return channelStateOptions;
		}

		public static ChannelStateOptions DeserializeLengthDelimited(Stream stream, ChannelStateOptions instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelStateOptions Deserialize(Stream stream, ChannelStateOptions instance, long limit)
		{
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<bnet.protocol.v2.Attribute>();
			}
			instance.PrivacyLevel = PrivacyLevel.PRIVACY_LEVEL_OPEN;
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
					instance.Attribute.Add(bnet.protocol.v2.Attribute.DeserializeLengthDelimited(stream));
					continue;
				case 16:
					instance.PrivacyLevel = (PrivacyLevel)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, ChannelStateOptions instance)
		{
			if (instance.Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.v2.Attribute.Serialize(stream, item);
				}
			}
			if (instance.HasPrivacyLevel)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PrivacyLevel);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Attribute.Count > 0)
			{
				foreach (bnet.protocol.v2.Attribute item in Attribute)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasPrivacyLevel)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PrivacyLevel);
			}
			return num;
		}
	}
}
