using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x0200035B RID: 859
	public class GetPlayerVariablesRequest : IProtoBuf
	{
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003631 RID: 13873 RVA: 0x000B3273 File Offset: 0x000B1473
		// (set) Token: 0x06003632 RID: 13874 RVA: 0x000B327B File Offset: 0x000B147B
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

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003633 RID: 13875 RVA: 0x000B3273 File Offset: 0x000B1473
		public List<PlayerVariables> PlayerVariablesList
		{
			get
			{
				return this._PlayerVariables;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x000B3284 File Offset: 0x000B1484
		public int PlayerVariablesCount
		{
			get
			{
				return this._PlayerVariables.Count;
			}
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000B3291 File Offset: 0x000B1491
		public void AddPlayerVariables(PlayerVariables val)
		{
			this._PlayerVariables.Add(val);
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000B329F File Offset: 0x000B149F
		public void ClearPlayerVariables()
		{
			this._PlayerVariables.Clear();
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000B32AC File Offset: 0x000B14AC
		public void SetPlayerVariables(List<PlayerVariables> val)
		{
			this.PlayerVariables = val;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06003638 RID: 13880 RVA: 0x000B32B5 File Offset: 0x000B14B5
		// (set) Token: 0x06003639 RID: 13881 RVA: 0x000B32BD File Offset: 0x000B14BD
		public ProcessId Host
		{
			get
			{
				return this._Host;
			}
			set
			{
				this._Host = value;
				this.HasHost = (value != null);
			}
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000B32D0 File Offset: 0x000B14D0
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x000B32DC File Offset: 0x000B14DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (PlayerVariables playerVariables in this.PlayerVariables)
			{
				num ^= playerVariables.GetHashCode();
			}
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000B3354 File Offset: 0x000B1554
		public override bool Equals(object obj)
		{
			GetPlayerVariablesRequest getPlayerVariablesRequest = obj as GetPlayerVariablesRequest;
			if (getPlayerVariablesRequest == null)
			{
				return false;
			}
			if (this.PlayerVariables.Count != getPlayerVariablesRequest.PlayerVariables.Count)
			{
				return false;
			}
			for (int i = 0; i < this.PlayerVariables.Count; i++)
			{
				if (!this.PlayerVariables[i].Equals(getPlayerVariablesRequest.PlayerVariables[i]))
				{
					return false;
				}
			}
			return this.HasHost == getPlayerVariablesRequest.HasHost && (!this.HasHost || this.Host.Equals(getPlayerVariablesRequest.Host));
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600363D RID: 13885 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000B33EA File Offset: 0x000B15EA
		public static GetPlayerVariablesRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetPlayerVariablesRequest>(bs, 0, -1);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000B33F4 File Offset: 0x000B15F4
		public void Deserialize(Stream stream)
		{
			GetPlayerVariablesRequest.Deserialize(stream, this);
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000B33FE File Offset: 0x000B15FE
		public static GetPlayerVariablesRequest Deserialize(Stream stream, GetPlayerVariablesRequest instance)
		{
			return GetPlayerVariablesRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000B340C File Offset: 0x000B160C
		public static GetPlayerVariablesRequest DeserializeLengthDelimited(Stream stream)
		{
			GetPlayerVariablesRequest getPlayerVariablesRequest = new GetPlayerVariablesRequest();
			GetPlayerVariablesRequest.DeserializeLengthDelimited(stream, getPlayerVariablesRequest);
			return getPlayerVariablesRequest;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000B3428 File Offset: 0x000B1628
		public static GetPlayerVariablesRequest DeserializeLengthDelimited(Stream stream, GetPlayerVariablesRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetPlayerVariablesRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000B3450 File Offset: 0x000B1650
		public static GetPlayerVariablesRequest Deserialize(Stream stream, GetPlayerVariablesRequest instance, long limit)
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
					else if (instance.Host == null)
					{
						instance.Host = ProcessId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ProcessId.DeserializeLengthDelimited(stream, instance.Host);
					}
				}
				else
				{
					instance.PlayerVariables.Add(bnet.protocol.game_utilities.v1.PlayerVariables.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000B351A File Offset: 0x000B171A
		public void Serialize(Stream stream)
		{
			GetPlayerVariablesRequest.Serialize(stream, this);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000B3524 File Offset: 0x000B1724
		public static void Serialize(Stream stream, GetPlayerVariablesRequest instance)
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
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000B35C8 File Offset: 0x000B17C8
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
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize2 = this.Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x0400148A RID: 5258
		private List<PlayerVariables> _PlayerVariables = new List<PlayerVariables>();

		// Token: 0x0400148B RID: 5259
		public bool HasHost;

		// Token: 0x0400148C RID: 5260
		private ProcessId _Host;
	}
}
