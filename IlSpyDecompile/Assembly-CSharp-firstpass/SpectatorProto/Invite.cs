using System;
using System.IO;
using PegasusShared;

namespace SpectatorProto
{
	public class Invite : IProtoBuf
	{
		public BnetId InviterGameAccountId { get; set; }

		public JoinInfo JoinInfo { get; set; }

		public override int GetHashCode()
		{
			return GetType().GetHashCode() ^ InviterGameAccountId.GetHashCode() ^ JoinInfo.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			Invite invite = obj as Invite;
			if (invite == null)
			{
				return false;
			}
			if (!InviterGameAccountId.Equals(invite.InviterGameAccountId))
			{
				return false;
			}
			if (!JoinInfo.Equals(invite.JoinInfo))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static Invite Deserialize(Stream stream, Invite instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static Invite DeserializeLengthDelimited(Stream stream)
		{
			Invite invite = new Invite();
			DeserializeLengthDelimited(stream, invite);
			return invite;
		}

		public static Invite DeserializeLengthDelimited(Stream stream, Invite instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static Invite Deserialize(Stream stream, Invite instance, long limit)
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
					if (instance.InviterGameAccountId == null)
					{
						instance.InviterGameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.InviterGameAccountId);
					}
					continue;
				case 18:
					if (instance.JoinInfo == null)
					{
						instance.JoinInfo = JoinInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						JoinInfo.DeserializeLengthDelimited(stream, instance.JoinInfo);
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

		public static void Serialize(Stream stream, Invite instance)
		{
			if (instance.InviterGameAccountId == null)
			{
				throw new ArgumentNullException("InviterGameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.InviterGameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.InviterGameAccountId);
			if (instance.JoinInfo == null)
			{
				throw new ArgumentNullException("JoinInfo", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.JoinInfo.GetSerializedSize());
			JoinInfo.Serialize(stream, instance.JoinInfo);
		}

		public uint GetSerializedSize()
		{
			uint serializedSize = InviterGameAccountId.GetSerializedSize();
			uint num = 0 + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = JoinInfo.GetSerializedSize();
			return num + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2;
		}
	}
}
