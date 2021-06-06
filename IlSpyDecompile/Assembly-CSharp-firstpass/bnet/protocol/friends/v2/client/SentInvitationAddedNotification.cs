using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	public class SentInvitationAddedNotification : IProtoBuf
	{
		public bool HasAgentAccountId;

		private ulong _AgentAccountId;

		private List<SentInvitation> _Invitation = new List<SentInvitation>();

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

		public List<SentInvitation> Invitation
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

		public List<SentInvitation> InvitationList => _Invitation;

		public int InvitationCount => _Invitation.Count;

		public bool IsInitialized => true;

		public void SetAgentAccountId(ulong val)
		{
			AgentAccountId = val;
		}

		public void AddInvitation(SentInvitation val)
		{
			_Invitation.Add(val);
		}

		public void ClearInvitation()
		{
			_Invitation.Clear();
		}

		public void SetInvitation(List<SentInvitation> val)
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
			foreach (SentInvitation item in Invitation)
			{
				num ^= item.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			SentInvitationAddedNotification sentInvitationAddedNotification = obj as SentInvitationAddedNotification;
			if (sentInvitationAddedNotification == null)
			{
				return false;
			}
			if (HasAgentAccountId != sentInvitationAddedNotification.HasAgentAccountId || (HasAgentAccountId && !AgentAccountId.Equals(sentInvitationAddedNotification.AgentAccountId)))
			{
				return false;
			}
			if (Invitation.Count != sentInvitationAddedNotification.Invitation.Count)
			{
				return false;
			}
			for (int i = 0; i < Invitation.Count; i++)
			{
				if (!Invitation[i].Equals(sentInvitationAddedNotification.Invitation[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static SentInvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitationAddedNotification>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static SentInvitationAddedNotification Deserialize(Stream stream, SentInvitationAddedNotification instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static SentInvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			SentInvitationAddedNotification sentInvitationAddedNotification = new SentInvitationAddedNotification();
			DeserializeLengthDelimited(stream, sentInvitationAddedNotification);
			return sentInvitationAddedNotification;
		}

		public static SentInvitationAddedNotification DeserializeLengthDelimited(Stream stream, SentInvitationAddedNotification instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static SentInvitationAddedNotification Deserialize(Stream stream, SentInvitationAddedNotification instance, long limit)
		{
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<SentInvitation>();
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
					instance.Invitation.Add(SentInvitation.DeserializeLengthDelimited(stream));
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

		public static void Serialize(Stream stream, SentInvitationAddedNotification instance)
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
			foreach (SentInvitation item in instance.Invitation)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
				SentInvitation.Serialize(stream, item);
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
				foreach (SentInvitation item in Invitation)
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
