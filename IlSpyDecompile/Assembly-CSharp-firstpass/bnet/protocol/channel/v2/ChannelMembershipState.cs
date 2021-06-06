using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v2
{
	public class ChannelMembershipState : IProtoBuf
	{
		private List<ChannelDescription> _Description = new List<ChannelDescription>();

		private List<ChannelInvitation> _Invitation = new List<ChannelInvitation>();

		public List<ChannelDescription> Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
			}
		}

		public List<ChannelDescription> DescriptionList => _Description;

		public int DescriptionCount => _Description.Count;

		public List<ChannelInvitation> Invitation
		{
			get
			{
				return _Invitation;
			}
			set
			{
				_Invitation = value;
			}
		}

		public List<ChannelInvitation> InvitationList => _Invitation;

		public int InvitationCount => _Invitation.Count;

		public bool IsInitialized => true;

		public void AddDescription(ChannelDescription val)
		{
			_Description.Add(val);
		}

		public void ClearDescription()
		{
			_Description.Clear();
		}

		public void SetDescription(List<ChannelDescription> val)
		{
			Description = val;
		}

		public void AddInvitation(ChannelInvitation val)
		{
			_Invitation.Add(val);
		}

		public void ClearInvitation()
		{
			_Invitation.Clear();
		}

		public void SetInvitation(List<ChannelInvitation> val)
		{
			Invitation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (ChannelDescription item in Description)
			{
				num ^= item.GetHashCode();
			}
			foreach (ChannelInvitation item2 in Invitation)
			{
				num ^= item2.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ChannelMembershipState channelMembershipState = obj as ChannelMembershipState;
			if (channelMembershipState == null)
			{
				return false;
			}
			if (Description.Count != channelMembershipState.Description.Count)
			{
				return false;
			}
			for (int i = 0; i < Description.Count; i++)
			{
				if (!Description[i].Equals(channelMembershipState.Description[i]))
				{
					return false;
				}
			}
			if (Invitation.Count != channelMembershipState.Invitation.Count)
			{
				return false;
			}
			for (int j = 0; j < Invitation.Count; j++)
			{
				if (!Invitation[j].Equals(channelMembershipState.Invitation[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static ChannelMembershipState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelMembershipState>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ChannelMembershipState Deserialize(Stream stream, ChannelMembershipState instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ChannelMembershipState DeserializeLengthDelimited(Stream stream)
		{
			ChannelMembershipState channelMembershipState = new ChannelMembershipState();
			DeserializeLengthDelimited(stream, channelMembershipState);
			return channelMembershipState;
		}

		public static ChannelMembershipState DeserializeLengthDelimited(Stream stream, ChannelMembershipState instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ChannelMembershipState Deserialize(Stream stream, ChannelMembershipState instance, long limit)
		{
			if (instance.Description == null)
			{
				instance.Description = new List<ChannelDescription>();
			}
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<ChannelInvitation>();
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
					instance.Description.Add(ChannelDescription.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					instance.Invitation.Add(ChannelInvitation.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ChannelMembershipState instance)
		{
			if (instance.Description.Count > 0)
			{
				foreach (ChannelDescription item in instance.Description)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					ChannelDescription.Serialize(stream, item);
				}
			}
			if (instance.Invitation.Count <= 0)
			{
				return;
			}
			foreach (ChannelInvitation item2 in instance.Invitation)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item2.GetSerializedSize());
				ChannelInvitation.Serialize(stream, item2);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (Description.Count > 0)
			{
				foreach (ChannelDescription item in Description)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (Invitation.Count > 0)
			{
				foreach (ChannelInvitation item2 in Invitation)
				{
					num++;
					uint serializedSize2 = item2.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
				return num;
			}
			return num;
		}
	}
}
