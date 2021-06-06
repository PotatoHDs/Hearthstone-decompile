using System.IO;

namespace PegasusGame
{
	public class UserUI : IProtoBuf
	{
		public enum PacketID
		{
			ID = 0xF
		}

		public bool HasMouseInfo;

		private MouseInfo _MouseInfo;

		public bool HasEmote;

		private int _Emote;

		public bool HasPlayerId;

		private int _PlayerId;

		public MouseInfo MouseInfo
		{
			get
			{
				return _MouseInfo;
			}
			set
			{
				_MouseInfo = value;
				HasMouseInfo = value != null;
			}
		}

		public int Emote
		{
			get
			{
				return _Emote;
			}
			set
			{
				_Emote = value;
				HasEmote = true;
			}
		}

		public int PlayerId
		{
			get
			{
				return _PlayerId;
			}
			set
			{
				_PlayerId = value;
				HasPlayerId = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasMouseInfo)
			{
				num ^= MouseInfo.GetHashCode();
			}
			if (HasEmote)
			{
				num ^= Emote.GetHashCode();
			}
			if (HasPlayerId)
			{
				num ^= PlayerId.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			UserUI userUI = obj as UserUI;
			if (userUI == null)
			{
				return false;
			}
			if (HasMouseInfo != userUI.HasMouseInfo || (HasMouseInfo && !MouseInfo.Equals(userUI.MouseInfo)))
			{
				return false;
			}
			if (HasEmote != userUI.HasEmote || (HasEmote && !Emote.Equals(userUI.Emote)))
			{
				return false;
			}
			if (HasPlayerId != userUI.HasPlayerId || (HasPlayerId && !PlayerId.Equals(userUI.PlayerId)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static UserUI Deserialize(Stream stream, UserUI instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static UserUI DeserializeLengthDelimited(Stream stream)
		{
			UserUI userUI = new UserUI();
			DeserializeLengthDelimited(stream, userUI);
			return userUI;
		}

		public static UserUI DeserializeLengthDelimited(Stream stream, UserUI instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static UserUI Deserialize(Stream stream, UserUI instance, long limit)
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
					if (instance.MouseInfo == null)
					{
						instance.MouseInfo = MouseInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						MouseInfo.DeserializeLengthDelimited(stream, instance.MouseInfo);
					}
					continue;
				case 16:
					instance.Emote = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 24:
					instance.PlayerId = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, UserUI instance)
		{
			if (instance.HasMouseInfo)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.MouseInfo.GetSerializedSize());
				MouseInfo.Serialize(stream, instance.MouseInfo);
			}
			if (instance.HasEmote)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.Emote);
			}
			if (instance.HasPlayerId)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.PlayerId);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasMouseInfo)
			{
				num++;
				uint serializedSize = MouseInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasEmote)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)Emote);
			}
			if (HasPlayerId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)PlayerId);
			}
			return num;
		}
	}
}
