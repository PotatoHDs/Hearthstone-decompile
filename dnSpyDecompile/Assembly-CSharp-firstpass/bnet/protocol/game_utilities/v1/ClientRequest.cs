using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.game_utilities.v1
{
	// Token: 0x02000356 RID: 854
	public class ClientRequest : IProtoBuf
	{
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060035B3 RID: 13747 RVA: 0x000B1B5F File Offset: 0x000AFD5F
		// (set) Token: 0x060035B4 RID: 13748 RVA: 0x000B1B67 File Offset: 0x000AFD67
		public List<Attribute> Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				this._Attribute = value;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000B1B5F File Offset: 0x000AFD5F
		public List<Attribute> AttributeList
		{
			get
			{
				return this._Attribute;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000B1B70 File Offset: 0x000AFD70
		public int AttributeCount
		{
			get
			{
				return this._Attribute.Count;
			}
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000B1B7D File Offset: 0x000AFD7D
		public void AddAttribute(Attribute val)
		{
			this._Attribute.Add(val);
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000B1B8B File Offset: 0x000AFD8B
		public void ClearAttribute()
		{
			this._Attribute.Clear();
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000B1B98 File Offset: 0x000AFD98
		public void SetAttribute(List<Attribute> val)
		{
			this.Attribute = val;
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000B1BA1 File Offset: 0x000AFDA1
		// (set) Token: 0x060035BB RID: 13755 RVA: 0x000B1BA9 File Offset: 0x000AFDA9
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

		// Token: 0x060035BC RID: 13756 RVA: 0x000B1BBC File Offset: 0x000AFDBC
		public void SetHost(ProcessId val)
		{
			this.Host = val;
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x000B1BC5 File Offset: 0x000AFDC5
		// (set) Token: 0x060035BE RID: 13758 RVA: 0x000B1BCD File Offset: 0x000AFDCD
		public EntityId AccountId
		{
			get
			{
				return this._AccountId;
			}
			set
			{
				this._AccountId = value;
				this.HasAccountId = (value != null);
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000B1BE0 File Offset: 0x000AFDE0
		public void SetAccountId(EntityId val)
		{
			this.AccountId = val;
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x000B1BE9 File Offset: 0x000AFDE9
		// (set) Token: 0x060035C1 RID: 13761 RVA: 0x000B1BF1 File Offset: 0x000AFDF1
		public EntityId GameAccountId
		{
			get
			{
				return this._GameAccountId;
			}
			set
			{
				this._GameAccountId = value;
				this.HasGameAccountId = (value != null);
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000B1C04 File Offset: 0x000AFE04
		public void SetGameAccountId(EntityId val)
		{
			this.GameAccountId = val;
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000B1C0D File Offset: 0x000AFE0D
		// (set) Token: 0x060035C4 RID: 13764 RVA: 0x000B1C15 File Offset: 0x000AFE15
		public uint Program
		{
			get
			{
				return this._Program;
			}
			set
			{
				this._Program = value;
				this.HasProgram = true;
			}
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000B1C25 File Offset: 0x000AFE25
		public void SetProgram(uint val)
		{
			this.Program = val;
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x000B1C2E File Offset: 0x000AFE2E
		// (set) Token: 0x060035C7 RID: 13767 RVA: 0x000B1C36 File Offset: 0x000AFE36
		public ClientInfo ClientInfo
		{
			get
			{
				return this._ClientInfo;
			}
			set
			{
				this._ClientInfo = value;
				this.HasClientInfo = (value != null);
			}
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000B1C49 File Offset: 0x000AFE49
		public void SetClientInfo(ClientInfo val)
		{
			this.ClientInfo = val;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000B1C54 File Offset: 0x000AFE54
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (Attribute attribute in this.Attribute)
			{
				num ^= attribute.GetHashCode();
			}
			if (this.HasHost)
			{
				num ^= this.Host.GetHashCode();
			}
			if (this.HasAccountId)
			{
				num ^= this.AccountId.GetHashCode();
			}
			if (this.HasGameAccountId)
			{
				num ^= this.GameAccountId.GetHashCode();
			}
			if (this.HasProgram)
			{
				num ^= this.Program.GetHashCode();
			}
			if (this.HasClientInfo)
			{
				num ^= this.ClientInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000B1D28 File Offset: 0x000AFF28
		public override bool Equals(object obj)
		{
			ClientRequest clientRequest = obj as ClientRequest;
			if (clientRequest == null)
			{
				return false;
			}
			if (this.Attribute.Count != clientRequest.Attribute.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Attribute.Count; i++)
			{
				if (!this.Attribute[i].Equals(clientRequest.Attribute[i]))
				{
					return false;
				}
			}
			return this.HasHost == clientRequest.HasHost && (!this.HasHost || this.Host.Equals(clientRequest.Host)) && this.HasAccountId == clientRequest.HasAccountId && (!this.HasAccountId || this.AccountId.Equals(clientRequest.AccountId)) && this.HasGameAccountId == clientRequest.HasGameAccountId && (!this.HasGameAccountId || this.GameAccountId.Equals(clientRequest.GameAccountId)) && this.HasProgram == clientRequest.HasProgram && (!this.HasProgram || this.Program.Equals(clientRequest.Program)) && this.HasClientInfo == clientRequest.HasClientInfo && (!this.HasClientInfo || this.ClientInfo.Equals(clientRequest.ClientInfo));
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060035CB RID: 13771 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000B1E6D File Offset: 0x000B006D
		public static ClientRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ClientRequest>(bs, 0, -1);
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000B1E77 File Offset: 0x000B0077
		public void Deserialize(Stream stream)
		{
			ClientRequest.Deserialize(stream, this);
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000B1E81 File Offset: 0x000B0081
		public static ClientRequest Deserialize(Stream stream, ClientRequest instance)
		{
			return ClientRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000B1E8C File Offset: 0x000B008C
		public static ClientRequest DeserializeLengthDelimited(Stream stream)
		{
			ClientRequest clientRequest = new ClientRequest();
			ClientRequest.DeserializeLengthDelimited(stream, clientRequest);
			return clientRequest;
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000B1EA8 File Offset: 0x000B00A8
		public static ClientRequest DeserializeLengthDelimited(Stream stream, ClientRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ClientRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000B1ED0 File Offset: 0x000B00D0
		public static ClientRequest Deserialize(Stream stream, ClientRequest instance, long limit)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			if (instance.Attribute == null)
			{
				instance.Attribute = new List<Attribute>();
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
				else
				{
					if (num <= 26)
					{
						if (num == 10)
						{
							instance.Attribute.Add(bnet.protocol.Attribute.DeserializeLengthDelimited(stream));
							continue;
						}
						if (num != 18)
						{
							if (num == 26)
							{
								if (instance.AccountId == null)
								{
									instance.AccountId = EntityId.DeserializeLengthDelimited(stream);
									continue;
								}
								EntityId.DeserializeLengthDelimited(stream, instance.AccountId);
								continue;
							}
						}
						else
						{
							if (instance.Host == null)
							{
								instance.Host = ProcessId.DeserializeLengthDelimited(stream);
								continue;
							}
							ProcessId.DeserializeLengthDelimited(stream, instance.Host);
							continue;
						}
					}
					else if (num != 34)
					{
						if (num == 45)
						{
							instance.Program = binaryReader.ReadUInt32();
							continue;
						}
						if (num == 50)
						{
							if (instance.ClientInfo == null)
							{
								instance.ClientInfo = ClientInfo.DeserializeLengthDelimited(stream);
								continue;
							}
							ClientInfo.DeserializeLengthDelimited(stream, instance.ClientInfo);
							continue;
						}
					}
					else
					{
						if (instance.GameAccountId == null)
						{
							instance.GameAccountId = EntityId.DeserializeLengthDelimited(stream);
							continue;
						}
						EntityId.DeserializeLengthDelimited(stream, instance.GameAccountId);
						continue;
					}
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

		// Token: 0x060035D2 RID: 13778 RVA: 0x000B2069 File Offset: 0x000B0269
		public void Serialize(Stream stream)
		{
			ClientRequest.Serialize(stream, this);
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000B2074 File Offset: 0x000B0274
		public static void Serialize(Stream stream, ClientRequest instance)
		{
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			if (instance.Attribute.Count > 0)
			{
				foreach (Attribute attribute in instance.Attribute)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, attribute.GetSerializedSize());
					bnet.protocol.Attribute.Serialize(stream, attribute);
				}
			}
			if (instance.HasHost)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Host.GetSerializedSize());
				ProcessId.Serialize(stream, instance.Host);
			}
			if (instance.HasAccountId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.AccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.AccountId);
			}
			if (instance.HasGameAccountId)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.GameAccountId.GetSerializedSize());
				EntityId.Serialize(stream, instance.GameAccountId);
			}
			if (instance.HasProgram)
			{
				stream.WriteByte(45);
				binaryWriter.Write(instance.Program);
			}
			if (instance.HasClientInfo)
			{
				stream.WriteByte(50);
				ProtocolParser.WriteUInt32(stream, instance.ClientInfo.GetSerializedSize());
				ClientInfo.Serialize(stream, instance.ClientInfo);
			}
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000B21C0 File Offset: 0x000B03C0
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Attribute.Count > 0)
			{
				foreach (Attribute attribute in this.Attribute)
				{
					num += 1U;
					uint serializedSize = attribute.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.HasHost)
			{
				num += 1U;
				uint serializedSize2 = this.Host.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAccountId)
			{
				num += 1U;
				uint serializedSize3 = this.AccountId.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			if (this.HasGameAccountId)
			{
				num += 1U;
				uint serializedSize4 = this.GameAccountId.GetSerializedSize();
				num += serializedSize4 + ProtocolParser.SizeOfUInt32(serializedSize4);
			}
			if (this.HasProgram)
			{
				num += 1U;
				num += 4U;
			}
			if (this.HasClientInfo)
			{
				num += 1U;
				uint serializedSize5 = this.ClientInfo.GetSerializedSize();
				num += serializedSize5 + ProtocolParser.SizeOfUInt32(serializedSize5);
			}
			return num;
		}

		// Token: 0x04001472 RID: 5234
		private List<Attribute> _Attribute = new List<Attribute>();

		// Token: 0x04001473 RID: 5235
		public bool HasHost;

		// Token: 0x04001474 RID: 5236
		private ProcessId _Host;

		// Token: 0x04001475 RID: 5237
		public bool HasAccountId;

		// Token: 0x04001476 RID: 5238
		private EntityId _AccountId;

		// Token: 0x04001477 RID: 5239
		public bool HasGameAccountId;

		// Token: 0x04001478 RID: 5240
		private EntityId _GameAccountId;

		// Token: 0x04001479 RID: 5241
		public bool HasProgram;

		// Token: 0x0400147A RID: 5242
		private uint _Program;

		// Token: 0x0400147B RID: 5243
		public bool HasClientInfo;

		// Token: 0x0400147C RID: 5244
		private ClientInfo _ClientInfo;
	}
}
