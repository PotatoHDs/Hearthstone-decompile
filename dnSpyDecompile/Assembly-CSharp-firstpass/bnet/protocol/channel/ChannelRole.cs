using System;
using System.IO;

namespace bnet.protocol.channel
{
	// Token: 0x02000448 RID: 1096
	public class ChannelRole : IProtoBuf
	{
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06004A9B RID: 19099 RVA: 0x000E8960 File Offset: 0x000E6B60
		// (set) Token: 0x06004A9C RID: 19100 RVA: 0x000E8968 File Offset: 0x000E6B68
		public uint Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
				this.HasId = true;
			}
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x000E8978 File Offset: 0x000E6B78
		public void SetId(uint val)
		{
			this.Id = val;
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x000E8981 File Offset: 0x000E6B81
		// (set) Token: 0x06004A9F RID: 19103 RVA: 0x000E8989 File Offset: 0x000E6B89
		public RoleState State
		{
			get
			{
				return this._State;
			}
			set
			{
				this._State = value;
				this.HasState = (value != null);
			}
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x000E899C File Offset: 0x000E6B9C
		public void SetState(RoleState val)
		{
			this.State = val;
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06004AA1 RID: 19105 RVA: 0x000E89A5 File Offset: 0x000E6BA5
		// (set) Token: 0x06004AA2 RID: 19106 RVA: 0x000E89AD File Offset: 0x000E6BAD
		public ChannelPrivilegeSet Privilege
		{
			get
			{
				return this._Privilege;
			}
			set
			{
				this._Privilege = value;
				this.HasPrivilege = (value != null);
			}
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x000E89C0 File Offset: 0x000E6BC0
		public void SetPrivilege(ChannelPrivilegeSet val)
		{
			this.Privilege = val;
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x000E89CC File Offset: 0x000E6BCC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasId)
			{
				num ^= this.Id.GetHashCode();
			}
			if (this.HasState)
			{
				num ^= this.State.GetHashCode();
			}
			if (this.HasPrivilege)
			{
				num ^= this.Privilege.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x000E8A2C File Offset: 0x000E6C2C
		public override bool Equals(object obj)
		{
			ChannelRole channelRole = obj as ChannelRole;
			return channelRole != null && this.HasId == channelRole.HasId && (!this.HasId || this.Id.Equals(channelRole.Id)) && this.HasState == channelRole.HasState && (!this.HasState || this.State.Equals(channelRole.State)) && this.HasPrivilege == channelRole.HasPrivilege && (!this.HasPrivilege || this.Privilege.Equals(channelRole.Privilege));
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x000E8ACA File Offset: 0x000E6CCA
		public static ChannelRole ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelRole>(bs, 0, -1);
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x000E8AD4 File Offset: 0x000E6CD4
		public void Deserialize(Stream stream)
		{
			ChannelRole.Deserialize(stream, this);
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x000E8ADE File Offset: 0x000E6CDE
		public static ChannelRole Deserialize(Stream stream, ChannelRole instance)
		{
			return ChannelRole.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x000E8AEC File Offset: 0x000E6CEC
		public static ChannelRole DeserializeLengthDelimited(Stream stream)
		{
			ChannelRole channelRole = new ChannelRole();
			ChannelRole.DeserializeLengthDelimited(stream, channelRole);
			return channelRole;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x000E8B08 File Offset: 0x000E6D08
		public static ChannelRole DeserializeLengthDelimited(Stream stream, ChannelRole instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelRole.Deserialize(stream, instance, num);
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x000E8B30 File Offset: 0x000E6D30
		public static ChannelRole Deserialize(Stream stream, ChannelRole instance, long limit)
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
				else if (num != 8)
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
						else if (instance.Privilege == null)
						{
							instance.Privilege = ChannelPrivilegeSet.DeserializeLengthDelimited(stream);
						}
						else
						{
							ChannelPrivilegeSet.DeserializeLengthDelimited(stream, instance.Privilege);
						}
					}
					else if (instance.State == null)
					{
						instance.State = RoleState.DeserializeLengthDelimited(stream);
					}
					else
					{
						RoleState.DeserializeLengthDelimited(stream, instance.State);
					}
				}
				else
				{
					instance.Id = ProtocolParser.ReadUInt32(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x000E8C17 File Offset: 0x000E6E17
		public void Serialize(Stream stream)
		{
			ChannelRole.Serialize(stream, this);
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x000E8C20 File Offset: 0x000E6E20
		public static void Serialize(Stream stream, ChannelRole instance)
		{
			if (instance.HasId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt32(stream, instance.Id);
			}
			if (instance.HasState)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.State.GetSerializedSize());
				RoleState.Serialize(stream, instance.State);
			}
			if (instance.HasPrivilege)
			{
				stream.WriteByte(26);
				ProtocolParser.WriteUInt32(stream, instance.Privilege.GetSerializedSize());
				ChannelPrivilegeSet.Serialize(stream, instance.Privilege);
			}
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x000E8CA4 File Offset: 0x000E6EA4
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt32(this.Id);
			}
			if (this.HasState)
			{
				num += 1U;
				uint serializedSize = this.State.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasPrivilege)
			{
				num += 1U;
				uint serializedSize2 = this.Privilege.GetSerializedSize();
				num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
			}
			return num;
		}

		// Token: 0x0400187E RID: 6270
		public bool HasId;

		// Token: 0x0400187F RID: 6271
		private uint _Id;

		// Token: 0x04001880 RID: 6272
		public bool HasState;

		// Token: 0x04001881 RID: 6273
		private RoleState _State;

		// Token: 0x04001882 RID: 6274
		public bool HasPrivilege;

		// Token: 0x04001883 RID: 6275
		private ChannelPrivilegeSet _Privilege;
	}
}
