using System;
using System.IO;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x0200048E RID: 1166
	public class ChannelStateChangedNotification : IProtoBuf
	{
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x000FB6FA File Offset: 0x000F98FA
		// (set) Token: 0x06005113 RID: 20755 RVA: 0x000FB702 File Offset: 0x000F9902
		public GameAccountHandle AgentId
		{
			get
			{
				return this._AgentId;
			}
			set
			{
				this._AgentId = value;
				this.HasAgentId = (value != null);
			}
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x000FB715 File Offset: 0x000F9915
		public void SetAgentId(GameAccountHandle val)
		{
			this.AgentId = val;
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06005115 RID: 20757 RVA: 0x000FB71E File Offset: 0x000F991E
		// (set) Token: 0x06005116 RID: 20758 RVA: 0x000FB726 File Offset: 0x000F9926
		public ChannelId ChannelId
		{
			get
			{
				return this._ChannelId;
			}
			set
			{
				this._ChannelId = value;
				this.HasChannelId = (value != null);
			}
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x000FB739 File Offset: 0x000F9939
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005118 RID: 20760 RVA: 0x000FB742 File Offset: 0x000F9942
		// (set) Token: 0x06005119 RID: 20761 RVA: 0x000FB74A File Offset: 0x000F994A
		public ChannelStateAssignment Assignment
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

		// Token: 0x0600511A RID: 20762 RVA: 0x000FB75D File Offset: 0x000F995D
		public void SetAssignment(ChannelStateAssignment val)
		{
			this.Assignment = val;
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x000FB768 File Offset: 0x000F9968
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentId)
			{
				num ^= this.AgentId.GetHashCode();
			}
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasAssignment)
			{
				num ^= this.Assignment.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x000FB7C4 File Offset: 0x000F99C4
		public override bool Equals(object obj)
		{
			ChannelStateChangedNotification channelStateChangedNotification = obj as ChannelStateChangedNotification;
			return channelStateChangedNotification != null && this.HasAgentId == channelStateChangedNotification.HasAgentId && (!this.HasAgentId || this.AgentId.Equals(channelStateChangedNotification.AgentId)) && this.HasChannelId == channelStateChangedNotification.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(channelStateChangedNotification.ChannelId)) && this.HasAssignment == channelStateChangedNotification.HasAssignment && (!this.HasAssignment || this.Assignment.Equals(channelStateChangedNotification.Assignment));
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x0600511D RID: 20765 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x000FB85F File Offset: 0x000F9A5F
		public static ChannelStateChangedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelStateChangedNotification>(bs, 0, -1);
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x000FB869 File Offset: 0x000F9A69
		public void Deserialize(Stream stream)
		{
			ChannelStateChangedNotification.Deserialize(stream, this);
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x000FB873 File Offset: 0x000F9A73
		public static ChannelStateChangedNotification Deserialize(Stream stream, ChannelStateChangedNotification instance)
		{
			return ChannelStateChangedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x000FB880 File Offset: 0x000F9A80
		public static ChannelStateChangedNotification DeserializeLengthDelimited(Stream stream)
		{
			ChannelStateChangedNotification channelStateChangedNotification = new ChannelStateChangedNotification();
			ChannelStateChangedNotification.DeserializeLengthDelimited(stream, channelStateChangedNotification);
			return channelStateChangedNotification;
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x000FB89C File Offset: 0x000F9A9C
		public static ChannelStateChangedNotification DeserializeLengthDelimited(Stream stream, ChannelStateChangedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelStateChangedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x000FB8C4 File Offset: 0x000F9AC4
		public static ChannelStateChangedNotification Deserialize(Stream stream, ChannelStateChangedNotification instance, long limit)
		{
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
					if (num != 26)
					{
						if (num != 34)
						{
							Key key = ProtocolParser.ReadKey((byte)num, stream);
							if (key.Field == 0U)
							{
								throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
							}
							ProtocolParser.SkipKey(stream, key);
						}
						else if (instance.Assignment == null)
						{
							instance.Assignment = ChannelStateAssignment.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelStateAssignment.DeserializeLengthDelimited(stream, instance.Assignment);
						}
					}
					else if (instance.ChannelId == null)
					{
						instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
					}
					else
					{
						ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
					}
				}
				else if (instance.AgentId == null)
				{
					instance.AgentId = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.AgentId);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x000FB9C6 File Offset: 0x000F9BC6
		public void Serialize(Stream stream)
		{
			ChannelStateChangedNotification.Serialize(stream, this);
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x000FB9D0 File Offset: 0x000F9BD0
		public static void Serialize(Stream stream, ChannelStateChangedNotification instance)
		{
			if (instance.HasAgentId)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.AgentId.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.AgentId);
			}
			if (instance.HasChannelId)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasAssignment)
			{
				stream.WriteByte(34);
				ProtocolParser.WriteUInt32(stream, instance.Assignment.GetSerializedSize());
				ChannelStateAssignment.Serialize(stream, instance.Assignment);
			}
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x000FBA64 File Offset: 0x000F9C64
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentId)
			{
				num += 1U;
				uint serializedSize = this.AgentId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize2 = this.ChannelId.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			if (this.HasAssignment)
			{
				num += 1U;
				uint serializedSize3 = this.Assignment.GetSerializedSize();
				num += serializedSize3 + ProtocolParser.SizeOfUInt32(serializedSize3);
			}
			return num;
		}

		// Token: 0x04001A1F RID: 6687
		public bool HasAgentId;

		// Token: 0x04001A20 RID: 6688
		private GameAccountHandle _AgentId;

		// Token: 0x04001A21 RID: 6689
		public bool HasChannelId;

		// Token: 0x04001A22 RID: 6690
		private ChannelId _ChannelId;

		// Token: 0x04001A23 RID: 6691
		public bool HasAssignment;

		// Token: 0x04001A24 RID: 6692
		private ChannelStateAssignment _Assignment;
	}
}
