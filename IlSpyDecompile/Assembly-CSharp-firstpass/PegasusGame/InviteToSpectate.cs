using System;
using System.IO;
using PegasusShared;

namespace PegasusGame
{
	public class InviteToSpectate : IProtoBuf
	{
		public enum PacketID
		{
			ID = 25
		}

		public bool HasTargetBnetAccountId;

		private BnetId _TargetBnetAccountId;

		public BnetId TargetBnetAccountId
		{
			get
			{
				return _TargetBnetAccountId;
			}
			set
			{
				_TargetBnetAccountId = value;
				HasTargetBnetAccountId = value != null;
			}
		}

		public BnetId TargetGameAccountId { get; set; }

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasTargetBnetAccountId)
			{
				num ^= TargetBnetAccountId.GetHashCode();
			}
			return num ^ TargetGameAccountId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			InviteToSpectate inviteToSpectate = obj as InviteToSpectate;
			if (inviteToSpectate == null)
			{
				return false;
			}
			if (HasTargetBnetAccountId != inviteToSpectate.HasTargetBnetAccountId || (HasTargetBnetAccountId && !TargetBnetAccountId.Equals(inviteToSpectate.TargetBnetAccountId)))
			{
				return false;
			}
			if (!TargetGameAccountId.Equals(inviteToSpectate.TargetGameAccountId))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static InviteToSpectate Deserialize(Stream stream, InviteToSpectate instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static InviteToSpectate DeserializeLengthDelimited(Stream stream)
		{
			InviteToSpectate inviteToSpectate = new InviteToSpectate();
			DeserializeLengthDelimited(stream, inviteToSpectate);
			return inviteToSpectate;
		}

		public static InviteToSpectate DeserializeLengthDelimited(Stream stream, InviteToSpectate instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static InviteToSpectate Deserialize(Stream stream, InviteToSpectate instance, long limit)
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
					if (instance.TargetBnetAccountId == null)
					{
						instance.TargetBnetAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.TargetBnetAccountId);
					}
					continue;
				case 18:
					if (instance.TargetGameAccountId == null)
					{
						instance.TargetGameAccountId = BnetId.DeserializeLengthDelimited(stream);
					}
					else
					{
						BnetId.DeserializeLengthDelimited(stream, instance.TargetGameAccountId);
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

		public static void Serialize(Stream stream, InviteToSpectate instance)
		{
			if (instance.HasTargetBnetAccountId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.TargetBnetAccountId.GetSerializedSize());
				BnetId.Serialize(stream, instance.TargetBnetAccountId);
			}
			if (instance.TargetGameAccountId == null)
			{
				throw new ArgumentNullException("TargetGameAccountId", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.TargetGameAccountId.GetSerializedSize());
			BnetId.Serialize(stream, instance.TargetGameAccountId);
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasTargetBnetAccountId)
			{
				num++;
				uint serializedSize = TargetBnetAccountId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			uint serializedSize2 = TargetGameAccountId.GetSerializedSize();
			num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			return num + 1;
		}
	}
}
