using System;
using System.IO;
using bnet.protocol.channel.v1;

namespace bnet.protocol.channel.v2.server
{
	// Token: 0x02000496 RID: 1174
	public class GetChannelRequest : IProtoBuf
	{
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x060051C3 RID: 20931 RVA: 0x000FD553 File Offset: 0x000FB753
		// (set) Token: 0x060051C4 RID: 20932 RVA: 0x000FD55B File Offset: 0x000FB75B
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

		// Token: 0x060051C5 RID: 20933 RVA: 0x000FD56E File Offset: 0x000FB76E
		public void SetChannelId(ChannelId val)
		{
			this.ChannelId = val;
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x060051C6 RID: 20934 RVA: 0x000FD577 File Offset: 0x000FB777
		// (set) Token: 0x060051C7 RID: 20935 RVA: 0x000FD57F File Offset: 0x000FB77F
		public bool FetchAttributes
		{
			get
			{
				return this._FetchAttributes;
			}
			set
			{
				this._FetchAttributes = value;
				this.HasFetchAttributes = true;
			}
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x000FD58F File Offset: 0x000FB78F
		public void SetFetchAttributes(bool val)
		{
			this.FetchAttributes = val;
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x060051C9 RID: 20937 RVA: 0x000FD598 File Offset: 0x000FB798
		// (set) Token: 0x060051CA RID: 20938 RVA: 0x000FD5A0 File Offset: 0x000FB7A0
		public bool FetchMembers
		{
			get
			{
				return this._FetchMembers;
			}
			set
			{
				this._FetchMembers = value;
				this.HasFetchMembers = true;
			}
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x000FD5B0 File Offset: 0x000FB7B0
		public void SetFetchMembers(bool val)
		{
			this.FetchMembers = val;
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x060051CC RID: 20940 RVA: 0x000FD5B9 File Offset: 0x000FB7B9
		// (set) Token: 0x060051CD RID: 20941 RVA: 0x000FD5C1 File Offset: 0x000FB7C1
		public bool FetchInvitations
		{
			get
			{
				return this._FetchInvitations;
			}
			set
			{
				this._FetchInvitations = value;
				this.HasFetchInvitations = true;
			}
		}

		// Token: 0x060051CE RID: 20942 RVA: 0x000FD5D1 File Offset: 0x000FB7D1
		public void SetFetchInvitations(bool val)
		{
			this.FetchInvitations = val;
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x060051CF RID: 20943 RVA: 0x000FD5DA File Offset: 0x000FB7DA
		// (set) Token: 0x060051D0 RID: 20944 RVA: 0x000FD5E2 File Offset: 0x000FB7E2
		public bool FetchRoles
		{
			get
			{
				return this._FetchRoles;
			}
			set
			{
				this._FetchRoles = value;
				this.HasFetchRoles = true;
			}
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x000FD5F2 File Offset: 0x000FB7F2
		public void SetFetchRoles(bool val)
		{
			this.FetchRoles = val;
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x000FD5FC File Offset: 0x000FB7FC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasChannelId)
			{
				num ^= this.ChannelId.GetHashCode();
			}
			if (this.HasFetchAttributes)
			{
				num ^= this.FetchAttributes.GetHashCode();
			}
			if (this.HasFetchMembers)
			{
				num ^= this.FetchMembers.GetHashCode();
			}
			if (this.HasFetchInvitations)
			{
				num ^= this.FetchInvitations.GetHashCode();
			}
			if (this.HasFetchRoles)
			{
				num ^= this.FetchRoles.GetHashCode();
			}
			return num;
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x000FD690 File Offset: 0x000FB890
		public override bool Equals(object obj)
		{
			GetChannelRequest getChannelRequest = obj as GetChannelRequest;
			return getChannelRequest != null && this.HasChannelId == getChannelRequest.HasChannelId && (!this.HasChannelId || this.ChannelId.Equals(getChannelRequest.ChannelId)) && this.HasFetchAttributes == getChannelRequest.HasFetchAttributes && (!this.HasFetchAttributes || this.FetchAttributes.Equals(getChannelRequest.FetchAttributes)) && this.HasFetchMembers == getChannelRequest.HasFetchMembers && (!this.HasFetchMembers || this.FetchMembers.Equals(getChannelRequest.FetchMembers)) && this.HasFetchInvitations == getChannelRequest.HasFetchInvitations && (!this.HasFetchInvitations || this.FetchInvitations.Equals(getChannelRequest.FetchInvitations)) && this.HasFetchRoles == getChannelRequest.HasFetchRoles && (!this.HasFetchRoles || this.FetchRoles.Equals(getChannelRequest.FetchRoles));
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x060051D4 RID: 20948 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x000FD78D File Offset: 0x000FB98D
		public static GetChannelRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetChannelRequest>(bs, 0, -1);
		}

		// Token: 0x060051D6 RID: 20950 RVA: 0x000FD797 File Offset: 0x000FB997
		public void Deserialize(Stream stream)
		{
			GetChannelRequest.Deserialize(stream, this);
		}

		// Token: 0x060051D7 RID: 20951 RVA: 0x000FD7A1 File Offset: 0x000FB9A1
		public static GetChannelRequest Deserialize(Stream stream, GetChannelRequest instance)
		{
			return GetChannelRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x060051D8 RID: 20952 RVA: 0x000FD7AC File Offset: 0x000FB9AC
		public static GetChannelRequest DeserializeLengthDelimited(Stream stream)
		{
			GetChannelRequest getChannelRequest = new GetChannelRequest();
			GetChannelRequest.DeserializeLengthDelimited(stream, getChannelRequest);
			return getChannelRequest;
		}

		// Token: 0x060051D9 RID: 20953 RVA: 0x000FD7C8 File Offset: 0x000FB9C8
		public static GetChannelRequest DeserializeLengthDelimited(Stream stream, GetChannelRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetChannelRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x060051DA RID: 20954 RVA: 0x000FD7F0 File Offset: 0x000FB9F0
		public static GetChannelRequest Deserialize(Stream stream, GetChannelRequest instance, long limit)
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
				else
				{
					if (num <= 40)
					{
						if (num != 18)
						{
							if (num == 40)
							{
								instance.FetchAttributes = ProtocolParser.ReadBool(stream);
								continue;
							}
						}
						else
						{
							if (instance.ChannelId == null)
							{
								instance.ChannelId = ChannelId.DeserializeLengthDelimited(stream);
								continue;
							}
							ChannelId.DeserializeLengthDelimited(stream, instance.ChannelId);
							continue;
						}
					}
					else
					{
						if (num == 48)
						{
							instance.FetchMembers = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 56)
						{
							instance.FetchInvitations = ProtocolParser.ReadBool(stream);
							continue;
						}
						if (num == 64)
						{
							instance.FetchRoles = ProtocolParser.ReadBool(stream);
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

		// Token: 0x060051DB RID: 20955 RVA: 0x000FD8F4 File Offset: 0x000FBAF4
		public void Serialize(Stream stream)
		{
			GetChannelRequest.Serialize(stream, this);
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x000FD900 File Offset: 0x000FBB00
		public static void Serialize(Stream stream, GetChannelRequest instance)
		{
			if (instance.HasChannelId)
			{
				stream.WriteByte(18);
				ProtocolParser.WriteUInt32(stream, instance.ChannelId.GetSerializedSize());
				ChannelId.Serialize(stream, instance.ChannelId);
			}
			if (instance.HasFetchAttributes)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteBool(stream, instance.FetchAttributes);
			}
			if (instance.HasFetchMembers)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteBool(stream, instance.FetchMembers);
			}
			if (instance.HasFetchInvitations)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.FetchInvitations);
			}
			if (instance.HasFetchRoles)
			{
				stream.WriteByte(64);
				ProtocolParser.WriteBool(stream, instance.FetchRoles);
			}
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x000FD9AC File Offset: 0x000FBBAC
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasChannelId)
			{
				num += 1U;
				uint serializedSize = this.ChannelId.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasFetchAttributes)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFetchMembers)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFetchInvitations)
			{
				num += 1U;
				num += 1U;
			}
			if (this.HasFetchRoles)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x04001A41 RID: 6721
		public bool HasChannelId;

		// Token: 0x04001A42 RID: 6722
		private ChannelId _ChannelId;

		// Token: 0x04001A43 RID: 6723
		public bool HasFetchAttributes;

		// Token: 0x04001A44 RID: 6724
		private bool _FetchAttributes;

		// Token: 0x04001A45 RID: 6725
		public bool HasFetchMembers;

		// Token: 0x04001A46 RID: 6726
		private bool _FetchMembers;

		// Token: 0x04001A47 RID: 6727
		public bool HasFetchInvitations;

		// Token: 0x04001A48 RID: 6728
		private bool _FetchInvitations;

		// Token: 0x04001A49 RID: 6729
		public bool HasFetchRoles;

		// Token: 0x04001A4A RID: 6730
		private bool _FetchRoles;
	}
}
