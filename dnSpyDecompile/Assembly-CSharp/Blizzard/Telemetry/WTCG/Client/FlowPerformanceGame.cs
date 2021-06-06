using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011C2 RID: 4546
	public class FlowPerformanceGame : IProtoBuf
	{
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x0600C9E4 RID: 51684 RVA: 0x003C7CFB File Offset: 0x003C5EFB
		// (set) Token: 0x0600C9E5 RID: 51685 RVA: 0x003C7D03 File Offset: 0x003C5F03
		public string FlowId
		{
			get
			{
				return this._FlowId;
			}
			set
			{
				this._FlowId = value;
				this.HasFlowId = (value != null);
			}
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x0600C9E6 RID: 51686 RVA: 0x003C7D16 File Offset: 0x003C5F16
		// (set) Token: 0x0600C9E7 RID: 51687 RVA: 0x003C7D1E File Offset: 0x003C5F1E
		public DeviceInfo DeviceInfo
		{
			get
			{
				return this._DeviceInfo;
			}
			set
			{
				this._DeviceInfo = value;
				this.HasDeviceInfo = (value != null);
			}
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x0600C9E8 RID: 51688 RVA: 0x003C7D31 File Offset: 0x003C5F31
		// (set) Token: 0x0600C9E9 RID: 51689 RVA: 0x003C7D39 File Offset: 0x003C5F39
		public Player Player
		{
			get
			{
				return this._Player;
			}
			set
			{
				this._Player = value;
				this.HasPlayer = (value != null);
			}
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x0600C9EA RID: 51690 RVA: 0x003C7D4C File Offset: 0x003C5F4C
		// (set) Token: 0x0600C9EB RID: 51691 RVA: 0x003C7D54 File Offset: 0x003C5F54
		public string Uuid
		{
			get
			{
				return this._Uuid;
			}
			set
			{
				this._Uuid = value;
				this.HasUuid = (value != null);
			}
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x0600C9EC RID: 51692 RVA: 0x003C7D67 File Offset: 0x003C5F67
		// (set) Token: 0x0600C9ED RID: 51693 RVA: 0x003C7D6F File Offset: 0x003C5F6F
		public GameType GameType
		{
			get
			{
				return this._GameType;
			}
			set
			{
				this._GameType = value;
				this.HasGameType = true;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x0600C9EE RID: 51694 RVA: 0x003C7D7F File Offset: 0x003C5F7F
		// (set) Token: 0x0600C9EF RID: 51695 RVA: 0x003C7D87 File Offset: 0x003C5F87
		public FormatType FormatType
		{
			get
			{
				return this._FormatType;
			}
			set
			{
				this._FormatType = value;
				this.HasFormatType = true;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x0600C9F0 RID: 51696 RVA: 0x003C7D97 File Offset: 0x003C5F97
		// (set) Token: 0x0600C9F1 RID: 51697 RVA: 0x003C7D9F File Offset: 0x003C5F9F
		public int BoardId
		{
			get
			{
				return this._BoardId;
			}
			set
			{
				this._BoardId = value;
				this.HasBoardId = true;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x0600C9F2 RID: 51698 RVA: 0x003C7DAF File Offset: 0x003C5FAF
		// (set) Token: 0x0600C9F3 RID: 51699 RVA: 0x003C7DB7 File Offset: 0x003C5FB7
		public int ScenarioId
		{
			get
			{
				return this._ScenarioId;
			}
			set
			{
				this._ScenarioId = value;
				this.HasScenarioId = true;
			}
		}

		// Token: 0x0600C9F4 RID: 51700 RVA: 0x003C7DC8 File Offset: 0x003C5FC8
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFlowId)
			{
				num ^= this.FlowId.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasUuid)
			{
				num ^= this.Uuid.GetHashCode();
			}
			if (this.HasGameType)
			{
				num ^= this.GameType.GetHashCode();
			}
			if (this.HasFormatType)
			{
				num ^= this.FormatType.GetHashCode();
			}
			if (this.HasBoardId)
			{
				num ^= this.BoardId.GetHashCode();
			}
			if (this.HasScenarioId)
			{
				num ^= this.ScenarioId.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600C9F5 RID: 51701 RVA: 0x003C7EAC File Offset: 0x003C60AC
		public override bool Equals(object obj)
		{
			FlowPerformanceGame flowPerformanceGame = obj as FlowPerformanceGame;
			return flowPerformanceGame != null && this.HasFlowId == flowPerformanceGame.HasFlowId && (!this.HasFlowId || this.FlowId.Equals(flowPerformanceGame.FlowId)) && this.HasDeviceInfo == flowPerformanceGame.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(flowPerformanceGame.DeviceInfo)) && this.HasPlayer == flowPerformanceGame.HasPlayer && (!this.HasPlayer || this.Player.Equals(flowPerformanceGame.Player)) && this.HasUuid == flowPerformanceGame.HasUuid && (!this.HasUuid || this.Uuid.Equals(flowPerformanceGame.Uuid)) && this.HasGameType == flowPerformanceGame.HasGameType && (!this.HasGameType || this.GameType.Equals(flowPerformanceGame.GameType)) && this.HasFormatType == flowPerformanceGame.HasFormatType && (!this.HasFormatType || this.FormatType.Equals(flowPerformanceGame.FormatType)) && this.HasBoardId == flowPerformanceGame.HasBoardId && (!this.HasBoardId || this.BoardId.Equals(flowPerformanceGame.BoardId)) && this.HasScenarioId == flowPerformanceGame.HasScenarioId && (!this.HasScenarioId || this.ScenarioId.Equals(flowPerformanceGame.ScenarioId));
		}

		// Token: 0x0600C9F6 RID: 51702 RVA: 0x003C8040 File Offset: 0x003C6240
		public void Deserialize(Stream stream)
		{
			FlowPerformanceGame.Deserialize(stream, this);
		}

		// Token: 0x0600C9F7 RID: 51703 RVA: 0x003C804A File Offset: 0x003C624A
		public static FlowPerformanceGame Deserialize(Stream stream, FlowPerformanceGame instance)
		{
			return FlowPerformanceGame.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600C9F8 RID: 51704 RVA: 0x003C8058 File Offset: 0x003C6258
		public static FlowPerformanceGame DeserializeLengthDelimited(Stream stream)
		{
			FlowPerformanceGame flowPerformanceGame = new FlowPerformanceGame();
			FlowPerformanceGame.DeserializeLengthDelimited(stream, flowPerformanceGame);
			return flowPerformanceGame;
		}

		// Token: 0x0600C9F9 RID: 51705 RVA: 0x003C8074 File Offset: 0x003C6274
		public static FlowPerformanceGame DeserializeLengthDelimited(Stream stream, FlowPerformanceGame instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return FlowPerformanceGame.Deserialize(stream, instance, num);
		}

		// Token: 0x0600C9FA RID: 51706 RVA: 0x003C809C File Offset: 0x003C629C
		public static FlowPerformanceGame Deserialize(Stream stream, FlowPerformanceGame instance, long limit)
		{
			instance.GameType = GameType.GT_UNKNOWN;
			instance.FormatType = FormatType.FT_UNKNOWN;
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
					if (num <= 34)
					{
						if (num <= 18)
						{
							if (num == 10)
							{
								instance.FlowId = ProtocolParser.ReadString(stream);
								continue;
							}
							if (num == 18)
							{
								if (instance.DeviceInfo == null)
								{
									instance.DeviceInfo = DeviceInfo.DeserializeLengthDelimited(stream);
									continue;
								}
								DeviceInfo.DeserializeLengthDelimited(stream, instance.DeviceInfo);
								continue;
							}
						}
						else if (num != 26)
						{
							if (num == 34)
							{
								instance.Uuid = ProtocolParser.ReadString(stream);
								continue;
							}
						}
						else
						{
							if (instance.Player == null)
							{
								instance.Player = Player.DeserializeLengthDelimited(stream);
								continue;
							}
							Player.DeserializeLengthDelimited(stream, instance.Player);
							continue;
						}
					}
					else if (num <= 48)
					{
						if (num == 40)
						{
							instance.GameType = (GameType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 48)
						{
							instance.FormatType = (FormatType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 56)
						{
							instance.BoardId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 64)
						{
							instance.ScenarioId = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
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

		// Token: 0x0600C9FB RID: 51707 RVA: 0x003C823A File Offset: 0x003C643A
		public void Serialize(Stream stream)
		{
			FlowPerformanceGame.Serialize(stream, this);
		}

		// Token: 0x0600C9FC RID: 51708 RVA: 0x003C8244 File Offset: 0x003C6444
		public static void Serialize(Stream stream, FlowPerformanceGame instance)
		{
			if (instance.HasFlowId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.FlowId));
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasPlayer)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasUuid)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Uuid));
			}
			if (instance.HasGameType)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.GameType));
			}
			if (instance.HasFormatType)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.FormatType));
			}
			if (instance.HasBoardId)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.BoardId));
			}
			if (instance.HasScenarioId)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ScenarioId));
			}
		}

		// Token: 0x0600C9FD RID: 51709 RVA: 0x003C836C File Offset: 0x003C656C
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFlowId)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.FlowId);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize = this.DeviceInfo.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize2 = this.Player.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasUuid)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Uuid);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasGameType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.GameType));
			}
			if (this.HasFormatType)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.FormatType));
			}
			if (this.HasBoardId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.BoardId));
			}
			if (this.HasScenarioId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ScenarioId));
			}
			return num;
		}

		// Token: 0x04009F87 RID: 40839
		public bool HasFlowId;

		// Token: 0x04009F88 RID: 40840
		private string _FlowId;

		// Token: 0x04009F89 RID: 40841
		public bool HasDeviceInfo;

		// Token: 0x04009F8A RID: 40842
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009F8B RID: 40843
		public bool HasPlayer;

		// Token: 0x04009F8C RID: 40844
		private Player _Player;

		// Token: 0x04009F8D RID: 40845
		public bool HasUuid;

		// Token: 0x04009F8E RID: 40846
		private string _Uuid;

		// Token: 0x04009F8F RID: 40847
		public bool HasGameType;

		// Token: 0x04009F90 RID: 40848
		private GameType _GameType;

		// Token: 0x04009F91 RID: 40849
		public bool HasFormatType;

		// Token: 0x04009F92 RID: 40850
		private FormatType _FormatType;

		// Token: 0x04009F93 RID: 40851
		public bool HasBoardId;

		// Token: 0x04009F94 RID: 40852
		private int _BoardId;

		// Token: 0x04009F95 RID: 40853
		public bool HasScenarioId;

		// Token: 0x04009F96 RID: 40854
		private int _ScenarioId;
	}
}
