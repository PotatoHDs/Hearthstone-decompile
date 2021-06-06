using System.IO;

namespace PegasusGame
{
	public class GameGuardianVars : IProtoBuf
	{
		public enum PacketID
		{
			ID = 35
		}

		public bool HasClientLostFrameTimeCatchUpThreshold;

		private float _ClientLostFrameTimeCatchUpThreshold;

		public bool HasClientLostFrameTimeCatchUpUseSlush;

		private bool _ClientLostFrameTimeCatchUpUseSlush;

		public bool HasClientLostFrameTimeCatchUpLowEndOnly;

		private bool _ClientLostFrameTimeCatchUpLowEndOnly;

		public bool HasGameAllowDeferredPowers;

		private bool _GameAllowDeferredPowers;

		public bool HasGameAllowBatchedPowers;

		private bool _GameAllowBatchedPowers;

		public bool HasGameAllowDiamondCards;

		private bool _GameAllowDiamondCards;

		public float ClientLostFrameTimeCatchUpThreshold
		{
			get
			{
				return _ClientLostFrameTimeCatchUpThreshold;
			}
			set
			{
				_ClientLostFrameTimeCatchUpThreshold = value;
				HasClientLostFrameTimeCatchUpThreshold = true;
			}
		}

		public bool ClientLostFrameTimeCatchUpUseSlush
		{
			get
			{
				return _ClientLostFrameTimeCatchUpUseSlush;
			}
			set
			{
				_ClientLostFrameTimeCatchUpUseSlush = value;
				HasClientLostFrameTimeCatchUpUseSlush = true;
			}
		}

		public bool ClientLostFrameTimeCatchUpLowEndOnly
		{
			get
			{
				return _ClientLostFrameTimeCatchUpLowEndOnly;
			}
			set
			{
				_ClientLostFrameTimeCatchUpLowEndOnly = value;
				HasClientLostFrameTimeCatchUpLowEndOnly = true;
			}
		}

		public bool GameAllowDeferredPowers
		{
			get
			{
				return _GameAllowDeferredPowers;
			}
			set
			{
				_GameAllowDeferredPowers = value;
				HasGameAllowDeferredPowers = true;
			}
		}

		public bool GameAllowBatchedPowers
		{
			get
			{
				return _GameAllowBatchedPowers;
			}
			set
			{
				_GameAllowBatchedPowers = value;
				HasGameAllowBatchedPowers = true;
			}
		}

		public bool GameAllowDiamondCards
		{
			get
			{
				return _GameAllowDiamondCards;
			}
			set
			{
				_GameAllowDiamondCards = value;
				HasGameAllowDiamondCards = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasClientLostFrameTimeCatchUpThreshold)
			{
				num ^= ClientLostFrameTimeCatchUpThreshold.GetHashCode();
			}
			if (HasClientLostFrameTimeCatchUpUseSlush)
			{
				num ^= ClientLostFrameTimeCatchUpUseSlush.GetHashCode();
			}
			if (HasClientLostFrameTimeCatchUpLowEndOnly)
			{
				num ^= ClientLostFrameTimeCatchUpLowEndOnly.GetHashCode();
			}
			if (HasGameAllowDeferredPowers)
			{
				num ^= GameAllowDeferredPowers.GetHashCode();
			}
			if (HasGameAllowBatchedPowers)
			{
				num ^= GameAllowBatchedPowers.GetHashCode();
			}
			if (HasGameAllowDiamondCards)
			{
				num ^= GameAllowDiamondCards.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GameGuardianVars gameGuardianVars = obj as GameGuardianVars;
			if (gameGuardianVars == null)
			{
				return false;
			}
			if (HasClientLostFrameTimeCatchUpThreshold != gameGuardianVars.HasClientLostFrameTimeCatchUpThreshold || (HasClientLostFrameTimeCatchUpThreshold && !ClientLostFrameTimeCatchUpThreshold.Equals(gameGuardianVars.ClientLostFrameTimeCatchUpThreshold)))
			{
				return false;
			}
			if (HasClientLostFrameTimeCatchUpUseSlush != gameGuardianVars.HasClientLostFrameTimeCatchUpUseSlush || (HasClientLostFrameTimeCatchUpUseSlush && !ClientLostFrameTimeCatchUpUseSlush.Equals(gameGuardianVars.ClientLostFrameTimeCatchUpUseSlush)))
			{
				return false;
			}
			if (HasClientLostFrameTimeCatchUpLowEndOnly != gameGuardianVars.HasClientLostFrameTimeCatchUpLowEndOnly || (HasClientLostFrameTimeCatchUpLowEndOnly && !ClientLostFrameTimeCatchUpLowEndOnly.Equals(gameGuardianVars.ClientLostFrameTimeCatchUpLowEndOnly)))
			{
				return false;
			}
			if (HasGameAllowDeferredPowers != gameGuardianVars.HasGameAllowDeferredPowers || (HasGameAllowDeferredPowers && !GameAllowDeferredPowers.Equals(gameGuardianVars.GameAllowDeferredPowers)))
			{
				return false;
			}
			if (HasGameAllowBatchedPowers != gameGuardianVars.HasGameAllowBatchedPowers || (HasGameAllowBatchedPowers && !GameAllowBatchedPowers.Equals(gameGuardianVars.GameAllowBatchedPowers)))
			{
				return false;
			}
			if (HasGameAllowDiamondCards != gameGuardianVars.HasGameAllowDiamondCards || (HasGameAllowDiamondCards && !GameAllowDiamondCards.Equals(gameGuardianVars.GameAllowDiamondCards)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GameGuardianVars Deserialize(Stream stream, GameGuardianVars instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GameGuardianVars DeserializeLengthDelimited(Stream stream)
		{
			GameGuardianVars gameGuardianVars = new GameGuardianVars();
			DeserializeLengthDelimited(stream, gameGuardianVars);
			return gameGuardianVars;
		}

		public static GameGuardianVars DeserializeLengthDelimited(Stream stream, GameGuardianVars instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GameGuardianVars Deserialize(Stream stream, GameGuardianVars instance, long limit)
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
				case 13:
					instance.ClientLostFrameTimeCatchUpThreshold = binaryReader.ReadSingle();
					continue;
				case 16:
					instance.ClientLostFrameTimeCatchUpUseSlush = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.ClientLostFrameTimeCatchUpLowEndOnly = ProtocolParser.ReadBool(stream);
					continue;
				case 32:
					instance.GameAllowDeferredPowers = ProtocolParser.ReadBool(stream);
					continue;
				case 40:
					instance.GameAllowBatchedPowers = ProtocolParser.ReadBool(stream);
					continue;
				case 48:
					instance.GameAllowDiamondCards = ProtocolParser.ReadBool(stream);
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

		public static void Serialize(Stream stream, GameGuardianVars instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.HasClientLostFrameTimeCatchUpThreshold)
			{
				stream.WriteByte(13);
				binaryWriter.Write(instance.ClientLostFrameTimeCatchUpThreshold);
			}
			if (instance.HasClientLostFrameTimeCatchUpUseSlush)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.ClientLostFrameTimeCatchUpUseSlush);
			}
			if (instance.HasClientLostFrameTimeCatchUpLowEndOnly)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteBool(stream, instance.ClientLostFrameTimeCatchUpLowEndOnly);
			}
			if (instance.HasGameAllowDeferredPowers)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteBool(stream, instance.GameAllowDeferredPowers);
			}
			if (instance.HasGameAllowBatchedPowers)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.GameAllowBatchedPowers);
			}
			if (instance.HasGameAllowDiamondCards)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.GameAllowDiamondCards);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasClientLostFrameTimeCatchUpThreshold)
			{
				num++;
				num += 4;
			}
			if (HasClientLostFrameTimeCatchUpUseSlush)
			{
				num++;
				num++;
			}
			if (HasClientLostFrameTimeCatchUpLowEndOnly)
			{
				num++;
				num++;
			}
			if (HasGameAllowDeferredPowers)
			{
				num++;
				num++;
			}
			if (HasGameAllowBatchedPowers)
			{
				num++;
				num++;
			}
			if (HasGameAllowDiamondCards)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
