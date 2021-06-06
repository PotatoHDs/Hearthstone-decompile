using System.IO;

namespace bnet.protocol.account.v1
{
	public class GameTimeInfo : IProtoBuf
	{
		public bool HasIsUnlimitedPlayTime;

		private bool _IsUnlimitedPlayTime;

		public bool HasPlayTimeExpires;

		private ulong _PlayTimeExpires;

		public bool HasIsSubscription;

		private bool _IsSubscription;

		public bool HasIsRecurringSubscription;

		private bool _IsRecurringSubscription;

		public bool IsUnlimitedPlayTime
		{
			get
			{
				return _IsUnlimitedPlayTime;
			}
			set
			{
				_IsUnlimitedPlayTime = value;
				HasIsUnlimitedPlayTime = true;
			}
		}

		public ulong PlayTimeExpires
		{
			get
			{
				return _PlayTimeExpires;
			}
			set
			{
				_PlayTimeExpires = value;
				HasPlayTimeExpires = true;
			}
		}

		public bool IsSubscription
		{
			get
			{
				return _IsSubscription;
			}
			set
			{
				_IsSubscription = value;
				HasIsSubscription = true;
			}
		}

		public bool IsRecurringSubscription
		{
			get
			{
				return _IsRecurringSubscription;
			}
			set
			{
				_IsRecurringSubscription = value;
				HasIsRecurringSubscription = true;
			}
		}

		public bool IsInitialized => true;

		public void SetIsUnlimitedPlayTime(bool val)
		{
			IsUnlimitedPlayTime = val;
		}

		public void SetPlayTimeExpires(ulong val)
		{
			PlayTimeExpires = val;
		}

		public void SetIsSubscription(bool val)
		{
			IsSubscription = val;
		}

		public void SetIsRecurringSubscription(bool val)
		{
			IsRecurringSubscription = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasIsUnlimitedPlayTime)
			{
				num ^= IsUnlimitedPlayTime.GetHashCode();
			}
			if (HasPlayTimeExpires)
			{
				num ^= PlayTimeExpires.GetHashCode();
			}
			if (HasIsSubscription)
			{
				num ^= IsSubscription.GetHashCode();
			}
			if (HasIsRecurringSubscription)
			{
				num ^= IsRecurringSubscription.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameTimeInfo gameTimeInfo = obj as GameTimeInfo;
			if (gameTimeInfo == null)
			{
				return false;
			}
			if (HasIsUnlimitedPlayTime != gameTimeInfo.HasIsUnlimitedPlayTime || (HasIsUnlimitedPlayTime && !IsUnlimitedPlayTime.Equals(gameTimeInfo.IsUnlimitedPlayTime)))
			{
				return false;
			}
			if (HasPlayTimeExpires != gameTimeInfo.HasPlayTimeExpires || (HasPlayTimeExpires && !PlayTimeExpires.Equals(gameTimeInfo.PlayTimeExpires)))
			{
				return false;
			}
			if (HasIsSubscription != gameTimeInfo.HasIsSubscription || (HasIsSubscription && !IsSubscription.Equals(gameTimeInfo.IsSubscription)))
			{
				return false;
			}
			if (HasIsRecurringSubscription != gameTimeInfo.HasIsRecurringSubscription || (HasIsRecurringSubscription && !IsRecurringSubscription.Equals(gameTimeInfo.IsRecurringSubscription)))
			{
				return false;
			}
			return true;
		}

		public static GameTimeInfo ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GameTimeInfo>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameTimeInfo Deserialize(Stream stream, GameTimeInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameTimeInfo DeserializeLengthDelimited(Stream stream)
		{
			GameTimeInfo gameTimeInfo = new GameTimeInfo();
			DeserializeLengthDelimited(stream, gameTimeInfo);
			return gameTimeInfo;
		}

		public static GameTimeInfo DeserializeLengthDelimited(Stream stream, GameTimeInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameTimeInfo Deserialize(Stream stream, GameTimeInfo instance, long limit)
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
				case 24:
					instance.IsUnlimitedPlayTime = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.PlayTimeExpires = ProtocolParser.ReadUInt64(stream);
					continue;
				case 48:
					instance.IsSubscription = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.IsRecurringSubscription = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameTimeInfo instance)
		{
			if (instance.HasIsUnlimitedPlayTime)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.IsUnlimitedPlayTime);
			}
			if (instance.HasPlayTimeExpires)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, instance.PlayTimeExpires);
			}
			if (instance.HasIsSubscription)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.IsSubscription);
			}
			if (instance.HasIsRecurringSubscription)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.IsRecurringSubscription);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasIsUnlimitedPlayTime)
			{
				num++;
				num++;
			}
			if (HasPlayTimeExpires)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64(PlayTimeExpires);
			}
			if (HasIsSubscription)
			{
				num++;
				num++;
			}
			if (HasIsRecurringSubscription)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
