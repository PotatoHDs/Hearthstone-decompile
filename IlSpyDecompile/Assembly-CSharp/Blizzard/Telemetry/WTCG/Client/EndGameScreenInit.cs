using System.IO;

namespace Blizzard.Telemetry.WTCG.Client
{
	public class EndGameScreenInit : IProtoBuf
	{
		public bool HasPlayer;

		private Player _Player;

		public bool HasDeviceInfo;

		private DeviceInfo _DeviceInfo;

		public bool HasElapsedTime;

		private float _ElapsedTime;

		public bool HasMedalInfoRetryCount;

		private int _MedalInfoRetryCount;

		public bool HasMedalInfoRetriesTimedOut;

		private bool _MedalInfoRetriesTimedOut;

		public bool HasShowRankedReward;

		private bool _ShowRankedReward;

		public bool HasShowCardBackProgress;

		private bool _ShowCardBackProgress;

		public bool HasOtherRewardCount;

		private int _OtherRewardCount;

		public Player Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
				HasPlayer = value != null;
			}
		}

		public DeviceInfo DeviceInfo
		{
			get
			{
				return _DeviceInfo;
			}
			set
			{
				_DeviceInfo = value;
				HasDeviceInfo = value != null;
			}
		}

		public float ElapsedTime
		{
			get
			{
				return _ElapsedTime;
			}
			set
			{
				_ElapsedTime = value;
				HasElapsedTime = true;
			}
		}

		public int MedalInfoRetryCount
		{
			get
			{
				return _MedalInfoRetryCount;
			}
			set
			{
				_MedalInfoRetryCount = value;
				HasMedalInfoRetryCount = true;
			}
		}

		public bool MedalInfoRetriesTimedOut
		{
			get
			{
				return _MedalInfoRetriesTimedOut;
			}
			set
			{
				_MedalInfoRetriesTimedOut = value;
				HasMedalInfoRetriesTimedOut = true;
			}
		}

		public bool ShowRankedReward
		{
			get
			{
				return _ShowRankedReward;
			}
			set
			{
				_ShowRankedReward = value;
				HasShowRankedReward = true;
			}
		}

		public bool ShowCardBackProgress
		{
			get
			{
				return _ShowCardBackProgress;
			}
			set
			{
				_ShowCardBackProgress = value;
				HasShowCardBackProgress = true;
			}
		}

		public int OtherRewardCount
		{
			get
			{
				return _OtherRewardCount;
			}
			set
			{
				_OtherRewardCount = value;
				HasOtherRewardCount = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasPlayer)
			{
				num ^= Player.GetHashCode();
			}
			if (HasDeviceInfo)
			{
				num ^= DeviceInfo.GetHashCode();
			}
			if (HasElapsedTime)
			{
				num ^= ElapsedTime.GetHashCode();
			}
			if (HasMedalInfoRetryCount)
			{
				num ^= MedalInfoRetryCount.GetHashCode();
			}
			if (HasMedalInfoRetriesTimedOut)
			{
				num ^= MedalInfoRetriesTimedOut.GetHashCode();
			}
			if (HasShowRankedReward)
			{
				num ^= ShowRankedReward.GetHashCode();
			}
			if (HasShowCardBackProgress)
			{
				num ^= ShowCardBackProgress.GetHashCode();
			}
			if (HasOtherRewardCount)
			{
				num ^= OtherRewardCount.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			EndGameScreenInit endGameScreenInit = obj as EndGameScreenInit;
			if (endGameScreenInit == null)
			{
				return false;
			}
			if (HasPlayer != endGameScreenInit.HasPlayer || (HasPlayer && !Player.Equals(endGameScreenInit.Player)))
			{
				return false;
			}
			if (HasDeviceInfo != endGameScreenInit.HasDeviceInfo || (HasDeviceInfo && !DeviceInfo.Equals(endGameScreenInit.DeviceInfo)))
			{
				return false;
			}
			if (HasElapsedTime != endGameScreenInit.HasElapsedTime || (HasElapsedTime && !ElapsedTime.Equals(endGameScreenInit.ElapsedTime)))
			{
				return false;
			}
			if (HasMedalInfoRetryCount != endGameScreenInit.HasMedalInfoRetryCount || (HasMedalInfoRetryCount && !MedalInfoRetryCount.Equals(endGameScreenInit.MedalInfoRetryCount)))
			{
				return false;
			}
			if (HasMedalInfoRetriesTimedOut != endGameScreenInit.HasMedalInfoRetriesTimedOut || (HasMedalInfoRetriesTimedOut && !MedalInfoRetriesTimedOut.Equals(endGameScreenInit.MedalInfoRetriesTimedOut)))
			{
				return false;
			}
			if (HasShowRankedReward != endGameScreenInit.HasShowRankedReward || (HasShowRankedReward && !ShowRankedReward.Equals(endGameScreenInit.ShowRankedReward)))
			{
				return false;
			}
			if (HasShowCardBackProgress != endGameScreenInit.HasShowCardBackProgress || (HasShowCardBackProgress && !ShowCardBackProgress.Equals(endGameScreenInit.ShowCardBackProgress)))
			{
				return false;
			}
			if (HasOtherRewardCount != endGameScreenInit.HasOtherRewardCount || (HasOtherRewardCount && !OtherRewardCount.Equals(endGameScreenInit.OtherRewardCount)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static EndGameScreenInit Deserialize(Stream stream, EndGameScreenInit instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static EndGameScreenInit DeserializeLengthDelimited(Stream stream)
		{
			EndGameScreenInit endGameScreenInit = new EndGameScreenInit();
			DeserializeLengthDelimited(stream, endGameScreenInit);
			return endGameScreenInit;
		}

		public static EndGameScreenInit DeserializeLengthDelimited(Stream stream, EndGameScreenInit instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static EndGameScreenInit Deserialize(Stream stream, EndGameScreenInit instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
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
					if (instance.Player == null)
					{
						instance.Player = Player.DeserializeLengthDelimited(stream);
					}
					else
					{
						Player.DeserializeLengthDelimited(stream, instance.Player);
					}
					continue;
				case 18:
					if (instance.DeviceInfo == null)
					{
						instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
					}
					else
					{
						DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
					}
					continue;
				case 29:
					instance.ElapsedTime = binaryReader.ReadSingle();
					continue;
				case 32:
					instance.MedalInfoRetryCount = (int)ProtocolParser.ReadUInt64(stream);
					continue;
				case 40:
					instance.MedalInfoRetriesTimedOut = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.ShowRankedReward = ProtocolParser.ReadBool(stream);
					continue;
				case 56:
					instance.ShowCardBackProgress = ProtocolParser.ReadBool(stream);
					continue;
				case 64:
					instance.OtherRewardCount = (int)ProtocolParser.ReadUInt64(stream);
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

		public static void Serialize(Stream stream, EndGameScreenInit instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasElapsedTime)
			{
				stream.WriteByte(29);
				binaryWriter.Write(instance.ElapsedTime);
			}
			if (instance.HasMedalInfoRetryCount)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.MedalInfoRetryCount);
			}
			if (instance.HasMedalInfoRetriesTimedOut)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.MedalInfoRetriesTimedOut);
			}
			if (instance.HasShowRankedReward)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.ShowRankedReward);
			}
			if (instance.HasShowCardBackProgress)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.ShowCardBackProgress);
			}
			if (instance.HasOtherRewardCount)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.OtherRewardCount);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasPlayer)
			{
				num++;
				uint serializedSize = Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (HasDeviceInfo)
			{
				num++;
				uint serializedSize2 = DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (HasElapsedTime)
			{
				num++;
				num += 4;
			}
			if (HasMedalInfoRetryCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)MedalInfoRetryCount);
			}
			if (HasMedalInfoRetriesTimedOut)
			{
				num++;
				num++;
			}
			if (HasShowRankedReward)
			{
				num++;
				num++;
			}
			if (HasShowCardBackProgress)
			{
				num++;
				num++;
			}
			if (HasOtherRewardCount)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)OtherRewardCount);
			}
			return num;
		}
	}
}
