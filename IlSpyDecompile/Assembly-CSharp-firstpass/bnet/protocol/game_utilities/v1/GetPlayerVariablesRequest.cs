using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	public class GetPlayerVariablesRequest : IProtoBuf
	{
		private List<PlayerVariables> _PlayerVariables = new List<PlayerVariables>();

		public bool HasHost;

		private ProcessId _Host;

		public List<PlayerVariables> PlayerVariables
		{
			get
			{
				return _PlayerVariables;
			}
			set
			{
				_PlayerVariables = value;
			}
		}

		public List<PlayerVariables> PlayerVariablesList => _PlayerVariables;

		public int PlayerVariablesCount => _PlayerVariables.Count;

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

		public bool IsInitialized => true;

		public void AddPlayerVariables(PlayerVariables val)
		{
			_PlayerVariables.Add(val);
		}

		public void ClearPlayerVariables()
		{
			_PlayerVariables.Clear();
		}

		public void SetPlayerVariables(List<PlayerVariables> val)
		{
			PlayerVariables = val;
		}

		public void SetHost(ProcessId val)
		{
			Host = val;
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (PlayerVariables playerVariable in PlayerVariables)
			{
				num ^= playerVariable.GetHashCode();
			}
			if (HasHost)
			{
				num ^= Host.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetPlayerVariablesRequest getPlayerVariablesRequest = obj as GetPlayerVariablesRequest;
			if (getPlayerVariablesRequest == null)
			{
				return false;
			}
			if (PlayerVariables.Count != getPlayerVariablesRequest.PlayerVariables.Count)
			{
				return false;
			}
			for (int i = 0; i < PlayerVariables.Count; i++)
			{
				if (!PlayerVariables[i].Equals(getPlayerVariablesRequest.PlayerVariables[i]))
				{
					return false;
				}
			}
			if (HasHost != getPlayerVariablesRequest.HasHost || (HasHost && !Host.Equals(getPlayerVariablesRequest.Host)))
			{
				return false;
			}
			return true;
		}

		public static GetPlayerVariablesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPlayerVariablesRequest>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetPlayerVariablesRequest Deserialize(Stream stream, GetPlayerVariablesRequest instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetPlayerVariablesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetPlayerVariablesRequest getPlayerVariablesRequest = new GetPlayerVariablesRequest();
			DeserializeLengthDelimited(stream, getPlayerVariablesRequest);
			return getPlayerVariablesRequest;
		}

		public static GetPlayerVariablesRequest DeserializeLengthDelimited(Stream stream, GetPlayerVariablesRequest instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetPlayerVariablesRequest Deserialize(Stream stream, GetPlayerVariablesRequest instance, long limit)
		{
			if (instance.PlayerVariables == null)
			{
				instance.PlayerVariables = new List<PlayerVariables>();
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
					instance.PlayerVariables.Add(bnet.protocol.game_utilities.v1.PlayerVariables.DeserializeLengthDelimited(stream));
					continue;
				case 18:
					if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
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

		public static void Serialize(Stream stream, GetPlayerVariablesRequest instance)
		{
			if (instance.PlayerVariables.Count > 0)
			{
				foreach (PlayerVariables playerVariable in instance.PlayerVariables)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, playerVariable.GetSerializedSize());
					bnet.protocol.game_utilities.v1.PlayerVariables.Serialize(stream, playerVariable);
				}
			}
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (PlayerVariables.Count > 0)
			{
				foreach (PlayerVariables playerVariable in PlayerVariables)
				{
					num++;
					uint serializedSize = playerVariable.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (HasHost)
			{
				num++;
				uint serializedSize2 = Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}
	}
}
