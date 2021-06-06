using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003F4 RID: 1012
	public class ReceivedInvitationAddedNotification : IProtoBuf
	{
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x000D496B File Offset: 0x000D2B6B
		// (set) Token: 0x06004318 RID: 17176 RVA: 0x000D4973 File Offset: 0x000D2B73
		public ulong AgentAccountId
		{
			get
			{
				return this._AgentAccountId;
			}
			set
			{
				this._AgentAccountId = value;
				this.HasAgentAccountId = true;
			}
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000D4983 File Offset: 0x000D2B83
		public void SetAgentAccountId(ulong val)
		{
			this.AgentAccountId = val;
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x000D498C File Offset: 0x000D2B8C
		// (set) Token: 0x0600431B RID: 17179 RVA: 0x000D4994 File Offset: 0x000D2B94
		public List<ReceivedInvitation> Invitation
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

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x000D498C File Offset: 0x000D2B8C
		public List<ReceivedInvitation> InvitationList
		{
			get
			{
				return this._Invitation;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x000D499D File Offset: 0x000D2B9D
		public int InvitationCount
		{
			get
			{
				return this._Invitation.Count;
			}
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x000D49AA File Offset: 0x000D2BAA
		public void AddInvitation(ReceivedInvitation val)
		{
			this._Invitation.Add(val);
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000D49B8 File Offset: 0x000D2BB8
		public void ClearInvitation()
		{
			this._Invitation.Clear();
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000D49C5 File Offset: 0x000D2BC5
		public void SetInvitation(List<ReceivedInvitation> val)
		{
			this.Invitation = val;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000D49D0 File Offset: 0x000D2BD0
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentAccountId)
			{
				num ^= this.AgentAccountId.GetHashCode();
			}
			foreach (ReceivedInvitation receivedInvitation in this.Invitation)
			{
				num ^= receivedInvitation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000D4A4C File Offset: 0x000D2C4C
		public override bool Equals(object obj)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = obj as ReceivedInvitationAddedNotification;
			if (receivedInvitationAddedNotification == null)
			{
				return false;
			}
			if (this.HasAgentAccountId != receivedInvitationAddedNotification.HasAgentAccountId || (this.HasAgentAccountId && !this.AgentAccountId.Equals(receivedInvitationAddedNotification.AgentAccountId)))
			{
				return false;
			}
			if (this.Invitation.Count != receivedInvitationAddedNotification.Invitation.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Invitation.Count; i++)
			{
				if (!this.Invitation[i].Equals(receivedInvitationAddedNotification.Invitation[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x000D4AE5 File Offset: 0x000D2CE5
		public static ReceivedInvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<ReceivedInvitationAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x000D4AEF File Offset: 0x000D2CEF
		public void Deserialize(Stream stream)
		{
			ReceivedInvitationAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x000D4AF9 File Offset: 0x000D2CF9
		public static ReceivedInvitationAddedNotification Deserialize(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			return ReceivedInvitationAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x000D4B04 File Offset: 0x000D2D04
		public static ReceivedInvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			ReceivedInvitationAddedNotification receivedInvitationAddedNotification = new ReceivedInvitationAddedNotification();
			ReceivedInvitationAddedNotification.DeserializeLengthDelimited(stream, receivedInvitationAddedNotification);
			return receivedInvitationAddedNotification;
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x000D4B20 File Offset: 0x000D2D20
		public static ReceivedInvitationAddedNotification DeserializeLengthDelimited(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return ReceivedInvitationAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x000D4B48 File Offset: 0x000D2D48
		public static ReceivedInvitationAddedNotification Deserialize(Stream stream, ReceivedInvitationAddedNotification instance, long limit)
		{
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<ReceivedInvitation>();
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
				else if (num != 8)
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
						instance.Invitation.Add(ReceivedInvitation.DeserializeLengthDelimited(stream));
					}
				}
				else
				{
					instance.AgentAccountId = ProtocolParser.ReadUInt64(stream);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x000D4BF7 File Offset: 0x000D2DF7
		public void Serialize(Stream stream)
		{
			ReceivedInvitationAddedNotification.Serialize(stream, this);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x000D4C00 File Offset: 0x000D2E00
		public static void Serialize(Stream stream, ReceivedInvitationAddedNotification instance)
		{
			if (instance.HasAgentAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AgentAccountId);
			}
			if (instance.Invitation.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in instance.Invitation)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, receivedInvitation.GetSerializedSize());
					ReceivedInvitation.Serialize(stream, receivedInvitation);
				}
			}
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000D4C90 File Offset: 0x000D2E90
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasAgentAccountId)
			{
				num += 1U;
				num += ProtocolParser.SizeOfUInt64(this.AgentAccountId);
			}
			if (this.Invitation.Count > 0)
			{
				foreach (ReceivedInvitation receivedInvitation in this.Invitation)
				{
					num += 1U;
					uint serializedSize = receivedInvitation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040016F9 RID: 5881
		public bool HasAgentAccountId;

		// Token: 0x040016FA RID: 5882
		private ulong _AgentAccountId;

		// Token: 0x040016FB RID: 5883
		private List<ReceivedInvitation> _Invitation = new List<ReceivedInvitation>();
	}
}
