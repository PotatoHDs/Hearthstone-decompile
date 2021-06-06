using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v1
{
	public class ChannelInfo : IProtoBuf
	{
		private List<Member> _Member = new List<Member>();

		public ChannelDescription Description { get; set; }

		public List<Member> Member
		{
			get
			{
				return _Member;
			}
			set
			{
				_Member = value;
			}
		}

		public List<Member> MemberList => _Member;

		public int MemberCount => _Member.Count;

		public bool IsInitialized => true;

		public void SetDescription(ChannelDescription val)
		{
			Description = val;
		}

		public void AddMember(Member val)
		{
			_Member.Add(val);
		}

		public void ClearMember()
		{
			_Member.Clear();
		}

		public void SetMember(List<Member> val)
		{
			Member = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= Description.GetHashCode();
			foreach (Member item in Member)
			{
				hashCode ^= item.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			ChannelInfo channelInfo = obj as ChannelInfo;
			if (channelInfo == null)
			{
				return false;
			}
			if (!Description.Equals(channelInfo.Description))
			{
				return false;
			}
			if (Member.Count != channelInfo.Member.Count)
			{
				return false;
			}
			for (int i = 0; i < Member.Count; i++)
			{
				if (!Member[i].Equals(channelInfo.Member[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ChannelInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelInfo Deserialize(Stream stream, ChannelInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelInfo DeserializeLengthDelimited(Stream stream)
		{
			ChannelInfo channelInfo = new ChannelInfo();
			DeserializeLengthDelimited(stream, channelInfo);
			return channelInfo;
		}

		public static ChannelInfo DeserializeLengthDelimited(Stream stream, ChannelInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelInfo Deserialize(Stream stream, ChannelInfo instance, long limit)
		{
			if (instance.Member == null)
			{
				instance.Member = new List<Member>();
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
					if (instance.Description == null)
					{
						instance.Description = ChannelDescription.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelDescription.DeserializeLengthDelimited(stream, instance.Description);
					}
					continue;
				case 18:
					instance.Member.Add(bnet.protocol.channel.v1.Member.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ChannelInfo instance)
		{
			if (instance.Description == null)
			{
				throw new ArgumentNullException("Description", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.Description.GetSerializedSize());
			ChannelDescription.Serialize(stream, instance.Description);
			if (instance.Member.Count <= 0)
			{
				return;
			}
			foreach (Member item in instance.Member)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				bnet.protocol.channel.v1.Member.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = Description.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (Member.Count > 0)
			{
				foreach (Member item in Member)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num + 1;
		}
	}
}
