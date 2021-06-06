using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class CancelGameEntryRequest : IProtoBuf
	{
		public bool HasFactoryId;

		private ulong _FactoryId;

		private List<Player> _Player = new List<Player>();

		public bool HasCancelRequestInitiator;

		private EntityId _CancelRequestInitiator;

		public ulong RequestId { get; set; }

		public ulong FactoryId
		{
			get
			{
				return _FactoryId;
			}
			set
			{
				_FactoryId = value;
				HasFactoryId = true;
			}
		}

		public List<Player> Player
		{
			get
			{
				return _Player;
			}
			set
			{
				_Player = value;
			}
		}

		public List<Player> PlayerList => _Player;

		public int PlayerCount => _Player.Count;

		public EntityId CancelRequestInitiator
		{
			get
			{
				return _CancelRequestInitiator;
			}
			set
			{
				_CancelRequestInitiator = value;
				HasCancelRequestInitiator = value != null;
			}
		}

		public bool IsInitialized => true;

		public void SetRequestId(ulong val)
		{
			RequestId = val;
		}

		public void SetFactoryId(ulong val)
		{
			FactoryId = val;
		}

		public void AddPlayer(Player val)
		{
			_Player.Add(val);
		}

		public void ClearPlayer()
		{
			_Player.Clear();
		}

		public void SetPlayer(List<Player> val)
		{
			Player = val;
		}

		public void SetCancelRequestInitiator(EntityId val)
		{
			CancelRequestInitiator = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= RequestId.GetHashCode();
			if (HasFactoryId)
			{
				hashCode ^= FactoryId.GetHashCode();
			}
			foreach (Player item in Player)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasCancelRequestInitiator)
			{
				hashCode ^= CancelRequestInitiator.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			CancelGameEntryRequest cancelGameEntryRequest = obj as CancelGameEntryRequest;
			if (cancelGameEntryRequest == null)
			{
				return false;
			}
			if (!RequestId.Equals(cancelGameEntryRequest.RequestId))
			{
				return false;
			}
			if (HasFactoryId != cancelGameEntryRequest.HasFactoryId || (HasFactoryId && !FactoryId.Equals(cancelGameEntryRequest.FactoryId)))
			{
				return false;
			}
			if (Player.Count != cancelGameEntryRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(cancelGameEntryRequest.Player[i]))
				{
					return false;
				}
			}
			if (HasCancelRequestInitiator != cancelGameEntryRequest.HasCancelRequestInitiator || (HasCancelRequestInitiator && !CancelRequestInitiator.Equals(cancelGameEntryRequest.CancelRequestInitiator)))
			{
				return false;
			}
			return true;
		}

		public static CancelGameEntryRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CancelGameEntryRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static CancelGameEntryRequest Deserialize(Stream stream, CancelGameEntryRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static CancelGameEntryRequest DeserializeLengthDelimited(Stream stream)
		{
			CancelGameEntryRequest cancelGameEntryRequest = new CancelGameEntryRequest();
			DeserializeLengthDelimited(stream, cancelGameEntryRequest);
			return cancelGameEntryRequest;
		}

		public static CancelGameEntryRequest DeserializeLengthDelimited(Stream stream, CancelGameEntryRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static CancelGameEntryRequest Deserialize(Stream stream, CancelGameEntryRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
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
				case 9:
					instance.RequestId = binaryReader.ReadUInt64();
					continue;
				case 17:
					instance.FactoryId = binaryReader.ReadUInt64();
					continue;
				case 26:
					instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
					continue;
				case 34:
					if (instance.CancelRequestInitiator == null)
					{
						instance.CancelRequestInitiator = EntityId.DeserializeLengthDelimited(stream);
					}
					else
					{
						EntityId.DeserializeLengthDelimited(stream, instance.CancelRequestInitiator);
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

		public static void Serialize(Stream stream, CancelGameEntryRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			stream.WriteByte(9);
			binaryWriter.Write(instance.RequestId);
			if (instance.HasFactoryId)
			{
				stream.WriteByte(17);
				binaryWriter.Write(instance.FactoryId);
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player item in instance.Player)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, item);
				}
			}
			if (instance.HasCancelRequestInitiator)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.CancelRequestInitiator.GetSerializedSize());
				EntityId.Serialize(stream, instance.CancelRequestInitiator);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			num += 8;
			if (HasFactoryId)
			{
				num++;
				num += 8;
			}
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize = item.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasCancelRequestInitiator)
			{
				num++;
				uint serializedSize2 = CancelRequestInitiator.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num + 1;
		}
	}
}
