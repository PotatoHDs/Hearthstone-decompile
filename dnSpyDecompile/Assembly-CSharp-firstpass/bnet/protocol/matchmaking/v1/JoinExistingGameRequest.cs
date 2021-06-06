using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003BD RID: 957
	public class JoinExistingGameRequest : IProtoBuf
	{
		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06003E5E RID: 15966 RVA: 0x000C83B7 File Offset: 0x000C65B7
		// (set) Token: 0x06003E5F RID: 15967 RVA: 0x000C83BF File Offset: 0x000C65BF
		public GameHandle GameHandle
		{
			get
			{
				return this._GameHandle;
			}
			set
			{
				this._GameHandle = value;
				this.HasGameHandle = (value != null);
			}
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x000C83D2 File Offset: 0x000C65D2
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06003E61 RID: 15969 RVA: 0x000C83DB File Offset: 0x000C65DB
		// (set) Token: 0x06003E62 RID: 15970 RVA: 0x000C83E3 File Offset: 0x000C65E3
		public RequestId RequestId
		{
			get
			{
				return this._RequestId;
			}
			set
			{
				this._RequestId = value;
				this.HasRequestId = (value != null);
			}
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x000C83F6 File Offset: 0x000C65F6
		public void SetRequestId(RequestId val)
		{
			this.RequestId = val;
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06003E64 RID: 15972 RVA: 0x000C83FF File Offset: 0x000C65FF
		// (set) Token: 0x06003E65 RID: 15973 RVA: 0x000C8407 File Offset: 0x000C6607
		public List<Player> Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000C83FF File Offset: 0x000C65FF
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000C8410 File Offset: 0x000C6610
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000C841D File Offset: 0x000C661D
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000C842B File Offset: 0x000C662B
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000C8438 File Offset: 0x000C6638
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000C8444 File Offset: 0x000C6644
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasRequestId)
			{
				num ^= this.RequestId.GetHashCode();
			}
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000C84D4 File Offset: 0x000C66D4
		public override bool Equals(object obj)
		{
			JoinExistingGameRequest joinExistingGameRequest = obj as JoinExistingGameRequest;
			if (joinExistingGameRequest == null)
			{
				return false;
			}
			if (this.HasGameHandle != joinExistingGameRequest.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(joinExistingGameRequest.GameHandle)))
			{
				return false;
			}
			if (this.HasRequestId != joinExistingGameRequest.HasRequestId || (this.HasRequestId && !this.RequestId.Equals(joinExistingGameRequest.RequestId)))
			{
				return false;
			}
			if (this.Player.Count != joinExistingGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(joinExistingGameRequest.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003E6D RID: 15981 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003E6E RID: 15982 RVA: 0x000C8595 File Offset: 0x000C6795
		public static JoinExistingGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<JoinExistingGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003E6F RID: 15983 RVA: 0x000C859F File Offset: 0x000C679F
		public void Deserialize(Stream stream)
		{
			JoinExistingGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x000C85A9 File Offset: 0x000C67A9
		public static JoinExistingGameRequest Deserialize(Stream stream, JoinExistingGameRequest instance)
		{
			return JoinExistingGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x000C85B4 File Offset: 0x000C67B4
		public static JoinExistingGameRequest DeserializeLengthDelimited(Stream stream)
		{
			JoinExistingGameRequest joinExistingGameRequest = new JoinExistingGameRequest();
			JoinExistingGameRequest.DeserializeLengthDelimited(stream, joinExistingGameRequest);
			return joinExistingGameRequest;
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x000C85D0 File Offset: 0x000C67D0
		public static JoinExistingGameRequest DeserializeLengthDelimited(Stream stream, JoinExistingGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return JoinExistingGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x000C85F8 File Offset: 0x000C67F8
		public static JoinExistingGameRequest Deserialize(Stream stream, JoinExistingGameRequest instance, long limit)
		{
			if (instance.Player == null)
			{
				instance.Player = new List<Player>();
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
				else if (num != 10)
				{
					if (num != 18)
					{
						if (num != 26)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else
						{
							instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
						}
					}
					else if (instance.RequestId == null)
					{
						instance.RequestId = RequestId.DeserializeLengthDelimited(stream);
					}
					else
					{
						RequestId.DeserializeLengthDelimited(stream, instance.RequestId);
					}
				}
				else if (instance.GameHandle == null)
				{
					instance.GameHandle = GameHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameHandle.DeserializeLengthDelimited(stream, instance.GameHandle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000C86F8 File Offset: 0x000C68F8
		public void Serialize(Stream stream)
		{
			JoinExistingGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000C8704 File Offset: 0x000C6904
		public static void Serialize(Stream stream, JoinExistingGameRequest instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasRequestId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.RequestId.GetSerializedSize());
				RequestId.Serialize(stream, instance.RequestId);
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(26);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.matchmaking.v1.Player.Serialize(stream, player);
				}
			}
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000C87D4 File Offset: 0x000C69D4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasRequestId)
			{
				num += 1U;
				uint serializedSize2 = this.RequestId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize3 = player.GetSerializedSize();
					num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
				}
			}
			return num;
		}

		// Token: 0x04001604 RID: 5636
		public bool HasGameHandle;

		// Token: 0x04001605 RID: 5637
		private GameHandle _GameHandle;

		// Token: 0x04001606 RID: 5638
		public bool HasRequestId;

		// Token: 0x04001607 RID: 5639
		private RequestId _RequestId;

		// Token: 0x04001608 RID: 5640
		private List<Player> _Player = new List<Player>();
	}
}
