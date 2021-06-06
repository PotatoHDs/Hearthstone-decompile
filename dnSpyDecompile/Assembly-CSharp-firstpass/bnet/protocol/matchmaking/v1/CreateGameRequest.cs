using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003B5 RID: 949
	public class CreateGameRequest : IProtoBuf
	{
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x000C6CAD File Offset: 0x000C4EAD
		// (set) Token: 0x06003DC6 RID: 15814 RVA: 0x000C6CB5 File Offset: 0x000C4EB5
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

		// Token: 0x06003DC7 RID: 15815 RVA: 0x000C6CC8 File Offset: 0x000C4EC8
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x000C6CD1 File Offset: 0x000C4ED1
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x000C6CD9 File Offset: 0x000C4ED9
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

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000C6CD1 File Offset: 0x000C4ED1
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06003DCB RID: 15819 RVA: 0x000C6CE2 File Offset: 0x000C4EE2
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000C6CEF File Offset: 0x000C4EEF
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000C6CFD File Offset: 0x000C4EFD
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000C6D0A File Offset: 0x000C4F0A
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06003DCF RID: 15823 RVA: 0x000C6D13 File Offset: 0x000C4F13
		// (set) Token: 0x06003DD0 RID: 15824 RVA: 0x000C6D1B File Offset: 0x000C4F1B
		public GameCreationProperties CreationProperties
		{
			get
			{
				return this._CreationProperties;
			}
			set
			{
				this._CreationProperties = value;
				this.HasCreationProperties = (value != null);
			}
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000C6D2E File Offset: 0x000C4F2E
		public void SetCreationProperties(GameCreationProperties val)
		{
			this.CreationProperties = val;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000C6D38 File Offset: 0x000C4F38
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			foreach (Player player in this.Player)
			{
				num ^= player.GetHashCode();
			}
			if (this.HasCreationProperties)
			{
				num ^= this.CreationProperties.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000C6DC8 File Offset: 0x000C4FC8
		public override bool Equals(object obj)
		{
			CreateGameRequest createGameRequest = obj as CreateGameRequest;
			if (createGameRequest == null)
			{
				return false;
			}
			if (this.HasGameHandle != createGameRequest.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(createGameRequest.GameHandle)))
			{
				return false;
			}
			if (this.Player.Count != createGameRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(createGameRequest.Player[i]))
				{
					return false;
				}
			}
			return this.HasCreationProperties == createGameRequest.HasCreationProperties && (!this.HasCreationProperties || this.CreationProperties.Equals(createGameRequest.CreationProperties));
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06003DD4 RID: 15828 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000C6E89 File Offset: 0x000C5089
		public static CreateGameRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameRequest>(bs, 0, -1);
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x000C6E93 File Offset: 0x000C5093
		public void Deserialize(Stream stream)
		{
			CreateGameRequest.Deserialize(stream, this);
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000C6E9D File Offset: 0x000C509D
		public static CreateGameRequest Deserialize(Stream stream, CreateGameRequest instance)
		{
			return CreateGameRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x000C6EA8 File Offset: 0x000C50A8
		public static CreateGameRequest DeserializeLengthDelimited(Stream stream)
		{
			CreateGameRequest createGameRequest = new CreateGameRequest();
			CreateGameRequest.DeserializeLengthDelimited(stream, createGameRequest);
			return createGameRequest;
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000C6EC4 File Offset: 0x000C50C4
		public static CreateGameRequest DeserializeLengthDelimited(Stream stream, CreateGameRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000C6EEC File Offset: 0x000C50EC
		public static CreateGameRequest Deserialize(Stream stream, CreateGameRequest instance, long limit)
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
						else if (instance.CreationProperties == null)
						{
							instance.CreationProperties = GameCreationProperties.DeserializeLengthDelimited(stream);
						}
						else
						{
							GameCreationProperties.DeserializeLengthDelimited(stream, instance.CreationProperties);
						}
					}
					else
					{
						instance.Player.Add(bnet.protocol.matchmaking.v1.Player.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003DDB RID: 15835 RVA: 0x000C6FEC File Offset: 0x000C51EC
		public void Serialize(Stream stream)
		{
			CreateGameRequest.Serialize(stream, this);
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000C6FF8 File Offset: 0x000C51F8
		public static void Serialize(Stream stream, CreateGameRequest instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.Player.Count > 0)
			{
				foreach (Player player in instance.Player)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, player.GetSerializedSize());
					bnet.protocol.matchmaking.v1.Player.Serialize(stream, player);
				}
			}
			if (instance.HasCreationProperties)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.CreationProperties.GetSerializedSize());
				GameCreationProperties.Serialize(stream, instance.CreationProperties);
			}
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000C70C8 File Offset: 0x000C52C8
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.Player.Count > 0)
			{
				foreach (Player player in this.Player)
				{
					num += 1U;
					uint serializedSize2 = player.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			if (this.HasCreationProperties)
			{
				num += 1U;
				uint serializedSize3 = this.CreationProperties.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x040015F2 RID: 5618
		public bool HasGameHandle;

		// Token: 0x040015F3 RID: 5619
		private GameHandle _GameHandle;

		// Token: 0x040015F4 RID: 5620
		private List<Player> _Player = new List<Player>();

		// Token: 0x040015F5 RID: 5621
		public bool HasCreationProperties;

		// Token: 0x040015F6 RID: 5622
		private GameCreationProperties _CreationProperties;
	}
}
