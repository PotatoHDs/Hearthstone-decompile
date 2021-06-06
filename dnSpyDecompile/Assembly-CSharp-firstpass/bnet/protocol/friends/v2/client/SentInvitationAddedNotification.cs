using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x020003F6 RID: 1014
	public class SentInvitationAddedNotification : IProtoBuf
	{
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x000D50F3 File Offset: 0x000D32F3
		// (set) Token: 0x06004346 RID: 17222 RVA: 0x000D50FB File Offset: 0x000D32FB
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

		// Token: 0x06004347 RID: 17223 RVA: 0x000D510B File Offset: 0x000D330B
		public void SetAgentAccountId(ulong val)
		{
			this.AgentAccountId = val;
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x000D5114 File Offset: 0x000D3314
		// (set) Token: 0x06004349 RID: 17225 RVA: 0x000D511C File Offset: 0x000D331C
		public List<SentInvitation> Invitation
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

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600434A RID: 17226 RVA: 0x000D5114 File Offset: 0x000D3314
		public List<SentInvitation> InvitationList
		{
			get
			{
				return this._Invitation;
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x000D5125 File Offset: 0x000D3325
		public int InvitationCount
		{
			get
			{
				return this._Invitation.Count;
			}
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000D5132 File Offset: 0x000D3332
		public void AddInvitation(SentInvitation val)
		{
			this._Invitation.Add(val);
		}

		// Token: 0x0600434D RID: 17229 RVA: 0x000D5140 File Offset: 0x000D3340
		public void ClearInvitation()
		{
			this._Invitation.Clear();
		}

		// Token: 0x0600434E RID: 17230 RVA: 0x000D514D File Offset: 0x000D334D
		public void SetInvitation(List<SentInvitation> val)
		{
			this.Invitation = val;
		}

		// Token: 0x0600434F RID: 17231 RVA: 0x000D5158 File Offset: 0x000D3358
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasAgentAccountId)
			{
				num ^= this.AgentAccountId.GetHashCode();
			}
			foreach (SentInvitation sentInvitation in this.Invitation)
			{
				num ^= sentInvitation.GetHashCode();
			}
			return num;
		}

		// Token: 0x06004350 RID: 17232 RVA: 0x000D51D4 File Offset: 0x000D33D4
		public override bool Equals(object obj)
		{
			SentInvitationAddedNotification sentInvitationAddedNotification = obj as SentInvitationAddedNotification;
			if (sentInvitationAddedNotification == null)
			{
				return false;
			}
			if (this.HasAgentAccountId != sentInvitationAddedNotification.HasAgentAccountId || (this.HasAgentAccountId && !this.AgentAccountId.Equals(sentInvitationAddedNotification.AgentAccountId)))
			{
				return false;
			}
			if (this.Invitation.Count != sentInvitationAddedNotification.Invitation.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Invitation.Count; i++)
			{
				if (!this.Invitation[i].Equals(sentInvitationAddedNotification.Invitation[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x000D526D File Offset: 0x000D346D
		public static SentInvitationAddedNotification ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<SentInvitationAddedNotification>(bs, 0, -1);
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x000D5277 File Offset: 0x000D3477
		public void Deserialize(Stream stream)
		{
			SentInvitationAddedNotification.Deserialize(stream, this);
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x000D5281 File Offset: 0x000D3481
		public static SentInvitationAddedNotification Deserialize(Stream stream, SentInvitationAddedNotification instance)
		{
			return SentInvitationAddedNotification.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x000D528C File Offset: 0x000D348C
		public static SentInvitationAddedNotification DeserializeLengthDelimited(Stream stream)
		{
			SentInvitationAddedNotification sentInvitationAddedNotification = new SentInvitationAddedNotification();
			SentInvitationAddedNotification.DeserializeLengthDelimited(stream, sentInvitationAddedNotification);
			return sentInvitationAddedNotification;
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x000D52A8 File Offset: 0x000D34A8
		public static SentInvitationAddedNotification DeserializeLengthDelimited(Stream stream, SentInvitationAddedNotification instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return SentInvitationAddedNotification.Deserialize(stream, instance, num);
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x000D52D0 File Offset: 0x000D34D0
		public static SentInvitationAddedNotification Deserialize(Stream stream, SentInvitationAddedNotification instance, long limit)
		{
			if (instance.Invitation == null)
			{
				instance.Invitation = new List<SentInvitation>();
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
						instance.Invitation.Add(SentInvitation.DeserializeLengthDelimited(stream));
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

		// Token: 0x06004358 RID: 17240 RVA: 0x000D537F File Offset: 0x000D357F
		public void Serialize(Stream stream)
		{
			SentInvitationAddedNotification.Serialize(stream, this);
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x000D5388 File Offset: 0x000D3588
		public static void Serialize(Stream stream, SentInvitationAddedNotification instance)
		{
			if (instance.HasAgentAccountId)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, instance.AgentAccountId);
			}
			if (instance.Invitation.Count > 0)
			{
				foreach (SentInvitation sentInvitation in instance.Invitation)
				{
					stream.WriteByte(18);
					ProtocolParser.WriteUInt32(stream, sentInvitation.GetSerializedSize());
					SentInvitation.Serialize(stream, sentInvitation);
				}
			}
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x000D5418 File Offset: 0x000D3618
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
				foreach (SentInvitation sentInvitation in this.Invitation)
				{
					num += 1U;
					uint serializedSize = sentInvitation.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040016FF RID: 5887
		public bool HasAgentAccountId;

		// Token: 0x04001700 RID: 5888
		private ulong _AgentAccountId;

		// Token: 0x04001701 RID: 5889
		private List<SentInvitation> _Invitation = new List<SentInvitation>();
	}
}
