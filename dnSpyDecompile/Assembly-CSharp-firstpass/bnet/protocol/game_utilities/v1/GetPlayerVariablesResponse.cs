using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x0200035C RID: 860
	public class GetPlayerVariablesResponse : IProtoBuf
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x000B3677 File Offset: 0x000B1877
		// (set) Token: 0x06003649 RID: 13897 RVA: 0x000B367F File Offset: 0x000B187F
		public List<PlayerVariables> PlayerVariables
		{
			get
			{
				return this._PlayerVariables;
			}
			set
			{
				this._PlayerVariables = value;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x000B3677 File Offset: 0x000B1877
		public List<PlayerVariables> PlayerVariablesList
		{
			get
			{
				return this._PlayerVariables;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600364B RID: 13899 RVA: 0x000B3688 File Offset: 0x000B1888
		public int PlayerVariablesCount
		{
			get
			{
				return this._PlayerVariables.Count;
			}
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000B3695 File Offset: 0x000B1895
		public void AddPlayerVariables(PlayerVariables val)
		{
			this._PlayerVariables.Add(val);
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000B36A3 File Offset: 0x000B18A3
		public void ClearPlayerVariables()
		{
			this._PlayerVariables.Clear();
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000B36B0 File Offset: 0x000B18B0
		public void SetPlayerVariables(List<PlayerVariables> val)
		{
			this.PlayerVariables = val;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000B36BC File Offset: 0x000B18BC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (PlayerVariables playerVariables in this.PlayerVariables)
			{
				num ^= playerVariables.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000B3720 File Offset: 0x000B1920
		public override bool Equals(object obj)
		{
			GetPlayerVariablesResponse getPlayerVariablesResponse = obj as GetPlayerVariablesResponse;
			if (getPlayerVariablesResponse == null)
			{
				return false;
			}
			if (this.PlayerVariables.Count != getPlayerVariablesResponse.PlayerVariables.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PlayerVariables.Count; i++)
			{
				if (!this.PlayerVariables[i].Equals(getPlayerVariablesResponse.PlayerVariables[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000B378B File Offset: 0x000B198B
		public static GetPlayerVariablesResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPlayerVariablesResponse>(bs, 0, -1);
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000B3795 File Offset: 0x000B1995
		public void Deserialize(Stream stream)
		{
			GetPlayerVariablesResponse.Deserialize(stream, this);
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000B379F File Offset: 0x000B199F
		public static GetPlayerVariablesResponse Deserialize(Stream stream, GetPlayerVariablesResponse instance)
		{
			return GetPlayerVariablesResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000B37AC File Offset: 0x000B19AC
		public static GetPlayerVariablesResponse DeserializeLengthDelimited(Stream stream)
		{
			GetPlayerVariablesResponse getPlayerVariablesResponse = new GetPlayerVariablesResponse();
			GetPlayerVariablesResponse.DeserializeLengthDelimited(stream, getPlayerVariablesResponse);
			return getPlayerVariablesResponse;
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000B37C8 File Offset: 0x000B19C8
		public static GetPlayerVariablesResponse DeserializeLengthDelimited(Stream stream, GetPlayerVariablesResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetPlayerVariablesResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000B37F0 File Offset: 0x000B19F0
		public static GetPlayerVariablesResponse Deserialize(Stream stream, GetPlayerVariablesResponse instance, long limit)
		{
			if (instance.PlayerVariables == null)
			{
				instance.PlayerVariables = new List<PlayerVariables>();
			}
			while (limit < 0L || stream.Position < limit)
			{
				int num = stream.ReadByte();
				if (num == -1)
				{
					if (limit >= 0L)
					{
						throw new EndOfStreamException();
					}
					return instance;
				}
				else if (num == 10)
				{
					instance.PlayerVariables.Add(bnet.protocol.game_utilities.v1.PlayerVariables.DeserializeLengthDelimited(stream));
				}
				else
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0U)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000B3888 File Offset: 0x000B1A88
		public void Serialize(Stream stream)
		{
			GetPlayerVariablesResponse.Serialize(stream, this);
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000B3894 File Offset: 0x000B1A94
		public static void Serialize(Stream stream, GetPlayerVariablesResponse instance)
		{
			if (instance.PlayerVariables.Count > 0)
			{
				foreach (PlayerVariables playerVariables in instance.PlayerVariables)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, playerVariables.GetSerializedSize());
					bnet.protocol.game_utilities.v1.PlayerVariables.Serialize(stream, playerVariables);
				}
			}
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000B390C File Offset: 0x000B1B0C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.PlayerVariables.Count > 0)
			{
				foreach (PlayerVariables playerVariables in this.PlayerVariables)
				{
					num += 1U;
					uint serializedSize = playerVariables.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x0400148D RID: 5261
		private List<PlayerVariables> _PlayerVariables = new List<PlayerVariables>();
	}
}
