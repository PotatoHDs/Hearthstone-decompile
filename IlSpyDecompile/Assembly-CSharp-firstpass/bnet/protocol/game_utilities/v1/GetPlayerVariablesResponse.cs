using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	public class GetPlayerVariablesResponse : IProtoBuf
	{
		private List<PlayerVariables> _PlayerVariables = new List<PlayerVariables>();

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

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			foreach (PlayerVariables playerVariable in PlayerVariables)
			{
				num ^= playerVariable.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			GetPlayerVariablesResponse getPlayerVariablesResponse = obj as GetPlayerVariablesResponse;
			if (getPlayerVariablesResponse == null)
			{
				return false;
			}
			if (PlayerVariables.Count != getPlayerVariablesResponse.PlayerVariables.Count)
			{
				return false;
			}
			for (int i = 0; i < PlayerVariables.Count; i++)
			{
				if (!PlayerVariables[i].Equals(getPlayerVariablesResponse.PlayerVariables[i]))
				{
					return false;
				}
			}
			return true;
		}

		public static GetPlayerVariablesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPlayerVariablesResponse>(bs);
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static GetPlayerVariablesResponse Deserialize(Stream stream, GetPlayerVariablesResponse instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static GetPlayerVariablesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetPlayerVariablesResponse getPlayerVariablesResponse = new GetPlayerVariablesResponse();
			DeserializeLengthDelimited(stream, getPlayerVariablesResponse);
			return getPlayerVariablesResponse;
		}

		public static GetPlayerVariablesResponse DeserializeLengthDelimited(Stream stream, GetPlayerVariablesResponse instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static GetPlayerVariablesResponse Deserialize(Stream stream, GetPlayerVariablesResponse instance, long limit)
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

		public static void Serialize(Stream stream, GetPlayerVariablesResponse instance)
		{
			if (instance.PlayerVariables.Count <= 0)
			{
				return;
			}
			foreach (PlayerVariables playerVariable in instance.PlayerVariables)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, playerVariable.GetSerializedSize());
				bnet.protocol.game_utilities.v1.PlayerVariables.Serialize(stream, playerVariable);
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
				return num;
			}
			return num;
		}
	}
}
