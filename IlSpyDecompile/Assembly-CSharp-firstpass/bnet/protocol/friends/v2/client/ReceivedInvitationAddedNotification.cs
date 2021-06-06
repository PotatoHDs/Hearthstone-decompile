using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class ReceivedInvitationAddedNotification : IProtoBuf
	{
		public bool HasAgentAccountId;

		private ulong _AgentAccountId;

		private List<ReceivedInvitation> _Invitation = new List<ReceivedInvitation>();

		public ulong AgentAccountId
		{
			get
			{
				return _AgentAccountId;
			}
			set
			{
				_AgentAccountId = value;
				HasAgentAccountId = true;
			}
		}

		public List<ReceivedInvitation> Invitation
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

		public List<ReceivedInvitation> InvitationList => _Invitation;

		public int InvitationCount => _Invitation.Count;

		public bool IsInitialized => true;

		public void SetAgentAccountId(ulong val)
		{
			AgentAccountId = val;
		}

		public void AddInvitation(ReceivedInvitation val)
		{
			_Invitation.Add(val);
		}

		public void ClearInvitation()
		{
			_Invitation.Clear();
		}

		public void SetInvitation(List<ReceivedInvitation> val)
		{
			Invitation = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasAgentAccountId)
			{
				num ^= AgentAccountId.GetHashCode();
			}
			foreach (ReceivedInvitation item in Invitation)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = obj as ReceivedInvitationAddedNotification;
			if (receivedInvitationAddedNotification == null)
			{
				return false;
			}
			if (HasAgentAccountId != receivedInvitationAddedNotification.HasAgentAccountId || (HasAgentAccountId && !AgentAccountId.Equals(receivedInvitationAddedNotification.AgentAccountId)))
			{
				return false;
			}
			if (Invitation.Count != receivedInvitationAddedNotification.Invitation.Count)
			{
				return false;
			}
			for (int i = 0; i < Invitation.Count; i++)
			{
				if (!Invitation[i].Equals(receivedInvitationAddedNotification.Invitation[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static ReceivedInvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitationAddedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static ReceivedInvitationAddedNotification Deserialize(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static ReceivedInvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = new ReceivedInvitationAddedNotification();
			DeserializeLengthDelimited(stream, receivedInvitationAddedNotification);
			return receivedInvitationAddedNotification;
		}

		public static ReceivedInvitationAddedNotification DeserializeLengthDelimited(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static ReceivedInvitationAddedNotification Deserialize(Stream stream, ReceivedInvitationAddedNotification instance, long limit)
		{
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<ReceivedInvitation>();
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
				case 8:
					instance.AgentAccountId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 18:
					instance.Invitation.Add(ReceivedInvitation.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			if (instance.HasAgentAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AgentAccountId);
			}
			if (instance.Invitation.Count <= 0)
			{
				return;
			}
			foreach (ReceivedInvitation item in instance.Invitation)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				ReceivedInvitation.Serialize(stream, item);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasAgentAccountId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(AgentAccountId);
			}
			if (Invitation.Count > 0)
			{
				foreach (ReceivedInvitation item in Invitation)
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
