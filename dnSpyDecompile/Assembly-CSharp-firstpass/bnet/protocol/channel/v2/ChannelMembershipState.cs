using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.channel.v2
{
	// Token: 0x0200047F RID: 1151
	public class ChannelMembershipState : IProtoBuf
	{
		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06004FD1 RID: 20433 RVA: 0x000F7D4B File Offset: 0x000F5F4B
		// (set) Token: 0x06004FD2 RID: 20434 RVA: 0x000F7D53 File Offset: 0x000F5F53
		public List<ChannelDescription> Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x000F7D4B File Offset: 0x000F5F4B
		public List<ChannelDescription> DescriptionList
		{
			get
			{
				return this._Description;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x000F7D5C File Offset: 0x000F5F5C
		public int DescriptionCount
		{
			get
			{
				return this._Description.Count;
			}
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x000F7D69 File Offset: 0x000F5F69
		public void AddDescription(ChannelDescription val)
		{
			this._Description.Add(val);
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x000F7D77 File Offset: 0x000F5F77
		public void ClearDescription()
		{
			this._Description.Clear();
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x000F7D84 File Offset: 0x000F5F84
		public void SetDescription(List<ChannelDescription> val)
		{
			this.Description = val;
		}

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06004FD8 RID: 20440 RVA: 0x000F7D8D File Offset: 0x000F5F8D
		// (set) Token: 0x06004FD9 RID: 20441 RVA: 0x000F7D95 File Offset: 0x000F5F95
		public List<ChannelInvitation> Invitation
		{
			get
			{
				return this._Invitation;
			}
			set
			{
				this._Invitation = value;
			}
		}

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06004FDA RID: 20442 RVA: 0x000F7D8D File Offset: 0x000F5F8D
		public List<ChannelInvitation> InvitationList
		{
			get
			{
				return this._Invitation;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06004FDB RID: 20443 RVA: 0x000F7D9E File Offset: 0x000F5F9E
		public int InvitationCount
		{
			get
			{
				return this._Invitation.Count;
			}
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x000F7DAB File Offset: 0x000F5FAB
		public void AddInvitation(ChannelInvitation val)
		{
			this._Invitation.Add(val);
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x000F7DB9 File Offset: 0x000F5FB9
		public void ClearInvitation()
		{
			this._Invitation.Clear();
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x000F7DC6 File Offset: 0x000F5FC6
		public void SetInvitation(List<ChannelInvitation> val)
		{
			this.Invitation = val;
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x000F7DD0 File Offset: 0x000F5FD0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ChannelDescription channelDescription in this.Description)
			{
				num ^= channelDescription.GetHashCode();
			}
			foreach (ChannelInvitation channelInvitation in this.Invitation)
			{
				num ^= channelInvitation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x000F7E78 File Offset: 0x000F6078
		public override bool Equals(object obj)
		{
			ChannelMembershipState channelMembershipState = obj as ChannelMembershipState;
			if (channelMembershipState == null)
			{
				return false;
			}
			if (this.Description.Count != channelMembershipState.Description.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Description.Count; i++)
			{
				if (!this.Description[i].Equals(channelMembershipState.Description[i]))
				{
					return false;
				}
			}
			if (this.Invitation.Count != channelMembershipState.Invitation.Count)
			{
				return false;
			}
			for (int j = 0; j < this.Invitation.Count; j++)
			{
				if (!this.Invitation[j].Equals(channelMembershipState.Invitation[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06004FE1 RID: 20449 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x000F7F34 File Offset: 0x000F6134
		public static ChannelMembershipState ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ChannelMembershipState>(bs, 0, -1);
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x000F7F3E File Offset: 0x000F613E
		public void Deserialize(Stream stream)
		{
			ChannelMembershipState.Deserialize(stream, this);
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x000F7F48 File Offset: 0x000F6148
		public static ChannelMembershipState Deserialize(Stream stream, ChannelMembershipState instance)
		{
			return ChannelMembershipState.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x000F7F54 File Offset: 0x000F6154
		public static ChannelMembershipState DeserializeLengthDelimited(Stream stream)
		{
			ChannelMembershipState channelMembershipState = new ChannelMembershipState();
			ChannelMembershipState.DeserializeLengthDelimited(stream, channelMembershipState);
			return channelMembershipState;
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x000F7F70 File Offset: 0x000F6170
		public static ChannelMembershipState DeserializeLengthDelimited(Stream stream, ChannelMembershipState instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ChannelMembershipState.Deserialize(stream, instance, num);
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x000F7F98 File Offset: 0x000F6198
		public static ChannelMembershipState Deserialize(Stream stream, ChannelMembershipState instance, long limit)
		{
			if (instance.Description == null)
			{
				instance.Description = new List<ChannelDescription>();
			}
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<ChannelInvitation>();
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
						instance.Invitation.Add(ChannelInvitation.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.Description.Add(ChannelDescription.DeserializeLengthDelimited(stream));
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x000F8060 File Offset: 0x000F6260
		public void Serialize(Stream stream)
		{
			ChannelMembershipState.Serialize(stream, this);
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x000F806C File Offset: 0x000F626C
		public static void Serialize(Stream stream, ChannelMembershipState instance)
		{
			if (instance.Description.Count > 0)
			{
				foreach (ChannelDescription channelDescription in instance.Description)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, channelDescription.GetSerializedSize());
					ChannelDescription.Serialize(stream, channelDescription);
				}
			}
			if (instance.Invitation.Count > 0)
			{
				foreach (ChannelInvitation channelInvitation in instance.Invitation)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, channelInvitation.GetSerializedSize());
					ChannelInvitation.Serialize(stream, channelInvitation);
				}
			}
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x000F8148 File Offset: 0x000F6348
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.Description.Count > 0)
			{
				foreach (ChannelDescription channelDescription in this.Description)
				{
					num += 1U;
					uint serializedSize = channelDescription.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			if (this.Invitation.Count > 0)
			{
				foreach (ChannelInvitation channelInvitation in this.Invitation)
				{
					num += 1U;
					uint serializedSize2 = channelInvitation.GetSerializedSize();
					num += serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2);
				}
			}
			return num;
		}

		// Token: 0x040019CA RID: 6602
		private List<ChannelDescription> _Description = new List<ChannelDescription>();

		// Token: 0x040019CB RID: 6603
		private List<ChannelInvitation> _Invitation = new List<ChannelInvitation>();
	}
}
