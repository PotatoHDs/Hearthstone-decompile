using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003B7 RID: 951
	public class AddPlayersRequest : IProtoBuf
	{
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003DF3 RID: 15859 RVA: 0x000C74B3 File Offset: 0x000C56B3
		// (set) Token: 0x06003DF4 RID: 15860 RVA: 0x000C74BB File Offset: 0x000C56BB
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

		// Token: 0x06003DF5 RID: 15861 RVA: 0x000C74CE File Offset: 0x000C56CE
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x000C74D7 File Offset: 0x000C56D7
		// (set) Token: 0x06003DF7 RID: 15863 RVA: 0x000C74DF File Offset: 0x000C56DF
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

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x000C74D7 File Offset: 0x000C56D7
		public List<Player> PlayerList
		{
			get
			{
				return this._Player;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x000C74E8 File Offset: 0x000C56E8
		public int PlayerCount
		{
			get
			{
				return this._Player.Count;
			}
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000C74F5 File Offset: 0x000C56F5
		public void AddPlayer(Player val)
		{
			this._Player.Add(val);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000C7503 File Offset: 0x000C5703
		public void ClearPlayer()
		{
			this._Player.Clear();
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x000C7510 File Offset: 0x000C5710
		public void SetPlayer(List<Player> val)
		{
			this.Player = val;
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x000C751C File Offset: 0x000C571C
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
			return num;
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x000C7594 File Offset: 0x000C5794
		public override bool Equals(object obj)
		{
			AddPlayersRequest addPlayersRequest = obj as AddPlayersRequest;
			if (addPlayersRequest == null)
			{
				return false;
			}
			if (this.HasGameHandle != addPlayersRequest.HasGameHandle || (this.HasGameHandle && !this.GameHandle.Equals(addPlayersRequest.GameHandle)))
			{
				return false;
			}
			if (this.Player.Count != addPlayersRequest.Player.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Player.Count; i++)
			{
				if (!this.Player[i].Equals(addPlayersRequest.Player[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06003DFF RID: 15871 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x000C762A File Offset: 0x000C582A
		public static AddPlayersRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersRequest>(bs, 0, -1);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x000C7634 File Offset: 0x000C5834
		public void Deserialize(Stream stream)
		{
			AddPlayersRequest.Deserialize(stream, this);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x000C763E File Offset: 0x000C583E
		public static AddPlayersRequest Deserialize(Stream stream, AddPlayersRequest instance)
		{
			return AddPlayersRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x000C764C File Offset: 0x000C584C
		public static AddPlayersRequest DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersRequest addPlayersRequest = new AddPlayersRequest();
			AddPlayersRequest.DeserializeLengthDelimited(stream, addPlayersRequest);
			return addPlayersRequest;
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x000C7668 File Offset: 0x000C5868
		public static AddPlayersRequest DeserializeLengthDelimited(Stream stream, AddPlayersRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddPlayersRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000C7690 File Offset: 0x000C5890
		public static AddPlayersRequest Deserialize(Stream stream, AddPlayersRequest instance, long limit)
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

		// Token: 0x06003E06 RID: 15878 RVA: 0x000C775A File Offset: 0x000C595A
		public void Serialize(Stream stream)
		{
			AddPlayersRequest.Serialize(stream, this);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x000C7764 File Offset: 0x000C5964
		public static void Serialize(Stream stream, AddPlayersRequest instance)
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
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000C7808 File Offset: 0x000C5A08
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
			return num;
		}

		// Token: 0x040015F8 RID: 5624
		public bool HasGameHandle;

		// Token: 0x040015F9 RID: 5625
		private GameHandle _GameHandle;

		// Token: 0x040015FA RID: 5626
		private List<Player> _Player = new List<Player>();
	}
}
