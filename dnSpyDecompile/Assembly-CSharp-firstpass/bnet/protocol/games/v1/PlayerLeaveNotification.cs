using System;
using System.IO;
using bnet.protocol.games.v2;
using bnet.protocol.games.v2.Types;

namespace bnet.protocol.games.v1
{
	// Token: 0x02000394 RID: 916
	public class PlayerLeaveNotification : IProtoBuf
	{
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x000BE332 File Offset: 0x000BC532
		// (set) Token: 0x06003ABA RID: 15034 RVA: 0x000BE33A File Offset: 0x000BC53A
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

		// Token: 0x06003ABB RID: 15035 RVA: 0x000BE34D File Offset: 0x000BC54D
		public void SetGameHandle(GameHandle val)
		{
			this.GameHandle = val;
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000BE356 File Offset: 0x000BC556
		// (set) Token: 0x06003ABD RID: 15037 RVA: 0x000BE35E File Offset: 0x000BC55E
		public Assignment Assignment
		{
			get
			{
				return this._Assignment;
			}
			set
			{
				this._Assignment = value;
				this.HasAssignment = (value != null);
			}
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x000BE371 File Offset: 0x000BC571
		public void SetAssignment(Assignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x000BE37A File Offset: 0x000BC57A
		// (set) Token: 0x06003AC0 RID: 15040 RVA: 0x000BE382 File Offset: 0x000BC582
		public PlayerLeaveReason Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				this._Reason = value;
				this.HasReason = true;
			}
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000BE392 File Offset: 0x000BC592
		public void SetReason(PlayerLeaveReason val)
		{
			this.Reason = val;
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000BE39C File Offset: 0x000BC59C
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasGameHandle)
			{
				num ^= this.GameHandle.GetHashCode();
			}
			if (this.HasAssignment)
			{
				num ^= this.Assignment.GetHashCode();
			}
			if (this.HasReason)
			{
				num ^= this.Reason.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000BE404 File Offset: 0x000BC604
		public override bool Equals(object obj)
		{
			PlayerLeaveNotification playerLeaveNotification = obj as PlayerLeaveNotification;
			return playerLeaveNotification != null && this.HasGameHandle == playerLeaveNotification.HasGameHandle && (!this.HasGameHandle || this.GameHandle.Equals(playerLeaveNotification.GameHandle)) && this.HasAssignment == playerLeaveNotification.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(playerLeaveNotification.Assignment)) && this.HasReason == playerLeaveNotification.HasReason && (!this.HasReason || this.Reason.Equals(playerLeaveNotification.Reason));
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000BE4AD File Offset: 0x000BC6AD
		public static PlayerLeaveNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<PlayerLeaveNotification>(bs, 0, -1);
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000BE4B7 File Offset: 0x000BC6B7
		public void Deserialize(Stream stream)
		{
			PlayerLeaveNotification.Deserialize(stream, this);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000BE4C1 File Offset: 0x000BC6C1
		public static PlayerLeaveNotification Deserialize(Stream stream, PlayerLeaveNotification instance)
		{
			return PlayerLeaveNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000BE4CC File Offset: 0x000BC6CC
		public static PlayerLeaveNotification DeserializeLengthDelimited(Stream stream)
		{
			PlayerLeaveNotification playerLeaveNotification = new PlayerLeaveNotification();
			PlayerLeaveNotification.DeserializeLengthDelimited(stream, playerLeaveNotification);
			return playerLeaveNotification;
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000BE4E8 File Offset: 0x000BC6E8
		public static PlayerLeaveNotification DeserializeLengthDelimited(Stream stream, PlayerLeaveNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return PlayerLeaveNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000BE510 File Offset: 0x000BC710
		public static PlayerLeaveNotification Deserialize(Stream stream, PlayerLeaveNotification instance, long limit)
		{
			instance.Reason = PlayerLeaveReason.PLAYER_LEAVE_REASON_PLAYER_REMOVED_BY_GAME_SERVER;
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
						if (num != 24)
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
							instance.Reason = (PlayerLeaveReason)ProtocolParser.ReadUInt64(stream);
						}
					}
					else if (instance.Assignment == null)
					{
						instance.Assignment = Assignment.DeserializeLengthDelimited(stream);
					}
					else
					{
						Assignment.DeserializeLengthDelimited(stream, instance.Assignment);
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

		// Token: 0x06003ACB RID: 15051 RVA: 0x000BE600 File Offset: 0x000BC800
		public void Serialize(Stream stream)
		{
			PlayerLeaveNotification.Serialize(stream, this);
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000BE60C File Offset: 0x000BC80C
		public static void Serialize(Stream stream, PlayerLeaveNotification instance)
		{
			if (instance.HasGameHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.GameHandle.GetSerializedSize());
				GameHandle.Serialize(stream, instance.GameHandle);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				Assignment.Serialize(stream, instance.Assignment);
			}
			if (instance.HasReason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)((long)instance.Reason));
			}
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000BE690 File Offset: 0x000BC890
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasGameHandle)
			{
				num += 1U;
				uint serializedSize = this.GameHandle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasAssignment)
			{
				num += 1U;
				uint serializedSize2 = this.Assignment.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasReason)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64((ulong)((long)this.Reason));
			}
			return num;
		}

		// Token: 0x04001548 RID: 5448
		public bool HasGameHandle;

		// Token: 0x04001549 RID: 5449
		private GameHandle _GameHandle;

		// Token: 0x0400154A RID: 5450
		public bool HasAssignment;

		// Token: 0x0400154B RID: 5451
		private Assignment _Assignment;

		// Token: 0x0400154C RID: 5452
		public bool HasReason;

		// Token: 0x0400154D RID: 5453
		private PlayerLeaveReason _Reason;
	}
}
