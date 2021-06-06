using System.IO;
using bnet.protocol.Types;

namespace bnet.protocol.friends.v2.client
{
	public class RemovedInvitationAssignment : IProtoBuf
	{
		public bool HasInvitationId;

		private ulong _InvitationId;

		public bool HasReason;

		private InvitationRemovedReason _Reason;

		public ulong InvitationId
		{
			get
			{
				return _InvitationId;
			}
			set
			{
				_InvitationId = value;
				HasInvitationId = true;
			}
		}

		public InvitationRemovedReason Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
				HasReason = true;
			}
		}

		public bool IsInitialized => true;

		public void SetInvitationId(ulong val)
		{
			InvitationId = val;
		}

		public void SetReason(InvitationRemovedReason val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasInvitationId)
			{
				num ^= InvitationId.GetHashCode();
			}
			if (HasReason)
			{
				num ^= Reason.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			RemovedInvitationAssignment removedInvitationAssignment = obj as RemovedInvitationAssignment;
			if (removedInvitationAssignment == null)
			{
				return false;
			}
			if (HasInvitationId != removedInvitationAssignment.HasInvitationId || (HasInvitationId && !InvitationId.Equals(removedInvitationAssignment.InvitationId)))
			{
				return false;
			}
			if (HasReason != removedInvitationAssignment.HasReason || (HasReason && !Reason.Equals(removedInvitationAssignment.Reason)))
			{
				return false;
			}
			return true;
		}

		public static RemovedInvitationAssignment ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemovedInvitationAssignment>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemovedInvitationAssignment Deserialize(Stream stream, RemovedInvitationAssignment instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemovedInvitationAssignment DeserializeLengthDelimited(Stream stream)
		{
			RemovedInvitationAssignment removedInvitationAssignment = new RemovedInvitationAssignment();
			DeserializeLengthDelimited(stream, removedInvitationAssignment);
			return removedInvitationAssignment;
		}

		public static RemovedInvitationAssignment DeserializeLengthDelimited(Stream stream, RemovedInvitationAssignment instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemovedInvitationAssignment Deserialize(Stream stream, RemovedInvitationAssignment instance, long limit)
		{
			instance.Reason = InvitationRemovedReason.INVITATION_REMOVED_REASON_ACCEPTED;
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
					instance.InvitationId = ProtocolParser.ReadUInt64(stream);
					continue;
				case 16:
					instance.Reason = (InvitationRemovedReason)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, RemovedInvitationAssignment instance)
		{
			if (instance.HasInvitationId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.InvitationId);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Reason);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasInvitationId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(InvitationId);
			}
			if (HasReason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Reason);
			}
			return num;
		}
	}
}
