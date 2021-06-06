using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	public class RemovePlayersRequest : IProtoBuf
	{
		private List<Player> _Player = new List<Player>();

		public bool HasHost;

		private ProcessId _Host;

		private List<uint> _Reason = new List<uint>();

		public GameHandle GameHandle { get; set; }

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

		public ProcessId Host
		{
			get
			{
				return _Host;
			}
			set
			{
				_Host = value;
				HasHost = value != null;
			}
		}

		public List<uint> Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason = value;
			}
		}

		public List<uint> ReasonList => _Reason;

		public int ReasonCount => _Reason.Count;

		public bool IsInitialized => true;

		public void SetGameHandle(GameHandle val)
		{
			GameHandle = val;
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

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public void AddReason(uint val)
		{
			_Reason.Add(val);
		}

		public void ClearReason()
		{
			_Reason.Clear();
		}

		public void SetReason(List<uint> val)
		{
			Reason = val;
		}

		public override int GetHashCode()
		{
			int hashCode = GetType().GetHashCode();
			hashCode ^= GameHandle.GetHashCode();
			foreach (Player item in Player)
			{
				hashCode ^= item.GetHashCode();
			}
			if (HasHost)
			{
				hashCode ^= Host.GetHashCode();
			}
			foreach (uint item2 in Reason)
			{
				hashCode ^= item2.GetHashCode();
			}
			return hashCode;
		}

		public override bool Equals(object obj)
		{
			RemovePlayersRequest removePlayersRequest = obj as RemovePlayersRequest;
			if (removePlayersRequest == null)
			{
				return false;
			}
			if (!GameHandle.Equals(removePlayersRequest.GameHandle))
			{
				return false;
			}
			if (Player.Count != removePlayersRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < Player.Count; i++)
			{
				if (!Player[i].Equals(removePlayersRequest.Player[i]))
				{
					return false;
				}
			}
			if (HasHost != removePlayersRequest.HasHost || (HasHost && !Host.Equals(removePlayersRequest.Host)))
			{
				return false;
			}
			if (Reason.Count != removePlayersRequest.Reason.Count)
			{
				return false;
			}
			for (int j = 0; j < Reason.Count; j++)
			{
				if (!Reason[j].Equals(removePlayersRequest.Reason[j]))
				{
					return false;
				}
			}
			return true;
		}

		public static RemovePlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<RemovePlayersRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static RemovePlayersRequest Deserialize(Stream stream, RemovePlayersRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static RemovePlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			RemovePlayersRequest removePlayersRequest = new RemovePlayersRequest();
			DeserializeLengthDelimited(stream, removePlayersRequest);
			return removePlayersRequest;
		}

		public static RemovePlayersRequest DeserializeLengthDelimited(Stream stream, RemovePlayersRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static RemovePlayersRequest Deserialize(Stream stream, RemovePlayersRequest instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
			}
			if (instance.Reason == null)
			{
				instance.Reason = new List<uint>();
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
				case 10:
					if (instance.GameHandle == null)
					{
						instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
					}
					else
					{
						GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
					}
					continue;
				case 18:
					instance.Player.Add(bnet.protocol.games.v1.Player.DeserializeLengthDelimited(stream));
					continue;
				case 26:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
					continue;
				case 34:
				{
					long num2 = ProtocolParser.ReadUInt32(stream);
					num2 += stream.Position;
					while (stream.Position < num2)
					{
						instance.Reason.Add(ProtocolParser.ReadUInt32(stream));
					}
					if (stream.Position == num2)
					{
						continue;
					}
					throw new ProtocolBufferException("Read too many bytes in packed data");
				}
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

		public static void Serialize(Stream stream, RemovePlayersRequest instance)
		{
			if (instance.GameHandle == null)
			{
				throw new ArgumentNullException("GameHandle", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
			GameHandle.Serialize(stream, instance.GameHandle);
			if (instance.Player.Count > 0)
			{
				foreach (Player item in instance.Player)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, item.GetSerializedSize());
					bnet.protocol.games.v1.Player.Serialize(stream, item);
				}
			}
			if (instance.HasHost)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.Reason.Count <= 0)
			{
				return;
			}
			stream.WriteByte(34);
			uint num = 0u;
			foreach (uint item2 in instance.Reason)
			{
				num += ProtocolParser.SizeOfUInt32(item2);
			}
			ProtocolParser.WriteUInt32(stream, num);
			foreach (uint item3 in instance.Reason)
			{
				ProtocolParser.WriteUInt32(stream, item3);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			uint serializedSize = GameHandle.GetSerializedSize();
			num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			if (Player.Count > 0)
			{
				foreach (Player item in Player)
				{
					num++;
					uint serializedSize2 = item.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (HasHost)
			{
				num++;
				uint serializedSize3 = Host.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (Reason.Count > 0)
			{
				num++;
				uint num2 = num;
				foreach (uint item2 in Reason)
				{
					num += ProtocolParser.SizeOfUInt32(item2);
				}
				num += ProtocolParser.SizeOfUInt32(num - num2);
			}
			return num + 1;
		}
	}
}
