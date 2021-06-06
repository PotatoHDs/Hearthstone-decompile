using System;
using System.IO;
using System.Text;

namespace Blizzard.Telemetry.WTCG.Client
{
	// Token: 0x020011CA RID: 4554
	public class InGameMessageAction : IProtoBuf
	{
		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x0600CA80 RID: 51840 RVA: 0x003CA161 File Offset: 0x003C8361
		// (set) Token: 0x0600CA81 RID: 51841 RVA: 0x003CA169 File Offset: 0x003C8369
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

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x0600CA82 RID: 51842 RVA: 0x003CA17C File Offset: 0x003C837C
		// (set) Token: 0x0600CA83 RID: 51843 RVA: 0x003CA184 File Offset: 0x003C8384
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

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x0600CA84 RID: 51844 RVA: 0x003CA197 File Offset: 0x003C8397
		// (set) Token: 0x0600CA85 RID: 51845 RVA: 0x003CA19F File Offset: 0x003C839F
		public string MessageType
		{
			get
			{
				return this._MessageType;
			}
			set
			{
				this._MessageType = value;
				this.HasMessageType = (value != null);
			}
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x0600CA86 RID: 51846 RVA: 0x003CA1B2 File Offset: 0x003C83B2
		// (set) Token: 0x0600CA87 RID: 51847 RVA: 0x003CA1BA File Offset: 0x003C83BA
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
				this.HasTitle = (value != null);
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x0600CA88 RID: 51848 RVA: 0x003CA1CD File Offset: 0x003C83CD
		// (set) Token: 0x0600CA89 RID: 51849 RVA: 0x003CA1D5 File Offset: 0x003C83D5
		public InGameMessageAction.ActionType Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				this._Action = value;
				this.HasAction = true;
			}
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x0600CA8A RID: 51850 RVA: 0x003CA1E5 File Offset: 0x003C83E5
		// (set) Token: 0x0600CA8B RID: 51851 RVA: 0x003CA1ED File Offset: 0x003C83ED
		public int ViewCounts
		{
			get
			{
				return this._ViewCounts;
			}
			set
			{
				this._ViewCounts = value;
				this.HasViewCounts = true;
			}
		}

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x0600CA8C RID: 51852 RVA: 0x003CA1FD File Offset: 0x003C83FD
		// (set) Token: 0x0600CA8D RID: 51853 RVA: 0x003CA205 File Offset: 0x003C8405
		public string Uid
		{
			get
			{
				return this._Uid;
			}
			set
			{
				this._Uid = value;
				this.HasUid = (value != null);
			}
		}

		// Token: 0x0600CA8E RID: 51854 RVA: 0x003CA218 File Offset: 0x003C8418
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasPlayer)
			{
				num ^= this.Player.GetHashCode();
			}
			if (this.HasDeviceInfo)
			{
				num ^= this.DeviceInfo.GetHashCode();
			}
			if (this.HasMessageType)
			{
				num ^= this.MessageType.GetHashCode();
			}
			if (this.HasTitle)
			{
				num ^= this.Title.GetHashCode();
			}
			if (this.HasAction)
			{
				num ^= this.Action.GetHashCode();
			}
			if (this.HasViewCounts)
			{
				num ^= this.ViewCounts.GetHashCode();
			}
			if (this.HasUid)
			{
				num ^= this.Uid.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600CA8F RID: 51855 RVA: 0x003CA2D8 File Offset: 0x003C84D8
		public override bool Equals(object obj)
		{
			InGameMessageAction inGameMessageAction = obj as InGameMessageAction;
			return inGameMessageAction != null && this.HasPlayer == inGameMessageAction.HasPlayer && (!this.HasPlayer || this.Player.Equals(inGameMessageAction.Player)) && this.HasDeviceInfo == inGameMessageAction.HasDeviceInfo && (!this.HasDeviceInfo || this.DeviceInfo.Equals(inGameMessageAction.DeviceInfo)) && this.HasMessageType == inGameMessageAction.HasMessageType && (!this.HasMessageType || this.MessageType.Equals(inGameMessageAction.MessageType)) && this.HasTitle == inGameMessageAction.HasTitle && (!this.HasTitle || this.Title.Equals(inGameMessageAction.Title)) && this.HasAction == inGameMessageAction.HasAction && (!this.HasAction || this.Action.Equals(inGameMessageAction.Action)) && this.HasViewCounts == inGameMessageAction.HasViewCounts && (!this.HasViewCounts || this.ViewCounts.Equals(inGameMessageAction.ViewCounts)) && this.HasUid == inGameMessageAction.HasUid && (!this.HasUid || this.Uid.Equals(inGameMessageAction.Uid));
		}

		// Token: 0x0600CA90 RID: 51856 RVA: 0x003CA430 File Offset: 0x003C8630
		public void Deserialize(Stream stream)
		{
			InGameMessageAction.Deserialize(stream, this);
		}

		// Token: 0x0600CA91 RID: 51857 RVA: 0x003CA43A File Offset: 0x003C863A
		public static InGameMessageAction Deserialize(Stream stream, InGameMessageAction instance)
		{
			return InGameMessageAction.Deserialize(stream, instance, -1L);
		}

		// Token: 0x0600CA92 RID: 51858 RVA: 0x003CA448 File Offset: 0x003C8648
		public static InGameMessageAction DeserializeLengthDelimited(Stream stream)
		{
			InGameMessageAction inGameMessageAction = new InGameMessageAction();
			InGameMessageAction.DeserializeLengthDelimited(stream, inGameMessageAction);
			return inGameMessageAction;
		}

		// Token: 0x0600CA93 RID: 51859 RVA: 0x003CA464 File Offset: 0x003C8664
		public static InGameMessageAction DeserializeLengthDelimited(Stream stream, InGameMessageAction instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return InGameMessageAction.Deserialize(stream, instance, num);
		}

		// Token: 0x0600CA94 RID: 51860 RVA: 0x003CA48C File Offset: 0x003C868C
		public static InGameMessageAction Deserialize(Stream stream, InGameMessageAction instance, long limit)
		{
			instance.Action = InGameMessageAction.ActionType.CLOSE;
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
						if (num != 10)
						{
							if (num != 18)
							{
								if (num == 26)
								{
									instance.MessageType = ProtocolParser.ReadString(stream);
									continue;
								}
							}
							else
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
					else if (num <= 40)
					{
						if (num == 34)
						{
							instance.Title = ProtocolParser.ReadString(stream);
							continue;
						}
						if (num == 40)
						{
							instance.Action = (InGameMessageAction.ActionType)ProtocolParser.ReadUInt64(stream);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.ViewCounts = (int)ProtocolParser.ReadUInt64(stream);
							continue;
						}
						if (num == 82)
						{
							instance.Uid = ProtocolParser.ReadString(stream);
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

		// Token: 0x0600CA95 RID: 51861 RVA: 0x003CA601 File Offset: 0x003C8801
		public void Serialize(Stream stream)
		{
			InGameMessageAction.Serialize(stream, this);
		}

		// Token: 0x0600CA96 RID: 51862 RVA: 0x003CA60C File Offset: 0x003C880C
		public static void Serialize(Stream stream, InGameMessageAction instance)
		{
			if (instance.HasPlayer)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Player.GetSerializedSize());
				Player.Serialize(stream, instance.Player);
			}
			if (instance.HasDeviceInfo)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.DeviceInfo.GetSerializedSize());
				DeviceInfo.Serialize(stream, instance.DeviceInfo);
			}
			if (instance.HasMessageType)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.MessageType));
			}
			if (instance.HasTitle)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Title));
			}
			if (instance.HasAction)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Action));
			}
			if (instance.HasViewCounts)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.ViewCounts));
			}
			if (instance.HasUid)
			{
				stream.WriteByte(82);
				ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Uid));
			}
		}

		// Token: 0x0600CA97 RID: 51863 RVA: 0x003CA720 File Offset: 0x003C8920
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasPlayer)
			{
				num += 1U;
				uint serializedSize = this.Player.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasDeviceInfo)
			{
				num += 1U;
				uint serializedSize2 = this.DeviceInfo.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasMessageType)
			{
				num += 1U;
				uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.MessageType);
				num += ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
			}
			if (this.HasTitle)
			{
				num += 1U;
				uint byteCount2 = (uint)Encoding.UTF8.GetByteCount(this.Title);
				num += ProtocolParser.SizeOfUInt32(byteCount2) + byteCount2;
			}
			if (this.HasAction)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Action));
			}
			if (this.HasViewCounts)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.ViewCounts));
			}
			if (this.HasUid)
			{
				num += 1U;
				uint byteCount3 = (uint)Encoding.UTF8.GetByteCount(this.Uid);
				num += ProtocolParser.SizeOfUInt32(byteCount3) + byteCount3;
			}
			return num;
		}

		// Token: 0x04009FCB RID: 40907
		public bool HasPlayer;

		// Token: 0x04009FCC RID: 40908
		private Player _Player;

		// Token: 0x04009FCD RID: 40909
		public bool HasDeviceInfo;

		// Token: 0x04009FCE RID: 40910
		private DeviceInfo _DeviceInfo;

		// Token: 0x04009FCF RID: 40911
		public bool HasMessageType;

		// Token: 0x04009FD0 RID: 40912
		private string _MessageType;

		// Token: 0x04009FD1 RID: 40913
		public bool HasTitle;

		// Token: 0x04009FD2 RID: 40914
		private string _Title;

		// Token: 0x04009FD3 RID: 40915
		public bool HasAction;

		// Token: 0x04009FD4 RID: 40916
		private InGameMessageAction.ActionType _Action;

		// Token: 0x04009FD5 RID: 40917
		public bool HasViewCounts;

		// Token: 0x04009FD6 RID: 40918
		private int _ViewCounts;

		// Token: 0x04009FD7 RID: 40919
		public bool HasUid;

		// Token: 0x04009FD8 RID: 40920
		private string _Uid;

		// Token: 0x02002944 RID: 10564
		public enum ActionType
		{
			// Token: 0x0400FC69 RID: 64617
			CLOSE = 1,
			// Token: 0x0400FC6A RID: 64618
			MORE_LINK_CLICK,
			// Token: 0x0400FC6B RID: 64619
			SCROLL_TO_NEXT,
			// Token: 0x0400FC6C RID: 64620
			OPENED_SHOP
		}
	}
}
