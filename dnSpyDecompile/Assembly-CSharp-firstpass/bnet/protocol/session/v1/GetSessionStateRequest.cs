using System;
using System.IO;
using bnet.protocol.account.v1;

namespace bnet.protocol.session.v1
{
	// Token: 0x0200030F RID: 783
	public class GetSessionStateRequest : IProtoBuf
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x000A10D3 File Offset: 0x0009F2D3
		// (set) Token: 0x06002F81 RID: 12161 RVA: 0x000A10DB File Offset: 0x0009F2DB
		public GameAccountHandle Handle
		{
			get
			{
				return this._Handle;
			}
			set
			{
				this._Handle = value;
				this.HasHandle = (value != null);
			}
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000A10EE File Offset: 0x0009F2EE
		public void SetHandle(GameAccountHandle val)
		{
			this.Handle = val;
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x000A10F7 File Offset: 0x0009F2F7
		// (set) Token: 0x06002F84 RID: 12164 RVA: 0x000A10FF File Offset: 0x0009F2FF
		public bool IncludeBillingDisabled
		{
			get
			{
				return this._IncludeBillingDisabled;
			}
			set
			{
				this._IncludeBillingDisabled = value;
				this.HasIncludeBillingDisabled = true;
			}
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000A110F File Offset: 0x0009F30F
		public void SetIncludeBillingDisabled(bool val)
		{
			this.IncludeBillingDisabled = val;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000A1118 File Offset: 0x0009F318
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasHandle)
			{
				num ^= this.Handle.GetHashCode();
			}
			if (this.HasIncludeBillingDisabled)
			{
				num ^= this.IncludeBillingDisabled.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000A1164 File Offset: 0x0009F364
		public override bool Equals(object obj)
		{
			GetSessionStateRequest getSessionStateRequest = obj as GetSessionStateRequest;
			return getSessionStateRequest != null && this.HasHandle == getSessionStateRequest.HasHandle && (!this.HasHandle || this.Handle.Equals(getSessionStateRequest.Handle)) && this.HasIncludeBillingDisabled == getSessionStateRequest.HasIncludeBillingDisabled && (!this.HasIncludeBillingDisabled || this.IncludeBillingDisabled.Equals(getSessionStateRequest.IncludeBillingDisabled));
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000A11D7 File Offset: 0x0009F3D7
		public static GetSessionStateRequest ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetSessionStateRequest>(bs, 0, -1);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000A11E1 File Offset: 0x0009F3E1
		public void Deserialize(Stream stream)
		{
			GetSessionStateRequest.Deserialize(stream, this);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000A11EB File Offset: 0x0009F3EB
		public static GetSessionStateRequest Deserialize(Stream stream, GetSessionStateRequest instance)
		{
			return GetSessionStateRequest.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000A11F8 File Offset: 0x0009F3F8
		public static GetSessionStateRequest DeserializeLengthDelimited(Stream stream)
		{
			GetSessionStateRequest getSessionStateRequest = new GetSessionStateRequest();
			GetSessionStateRequest.DeserializeLengthDelimited(stream, getSessionStateRequest);
			return getSessionStateRequest;
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000A1214 File Offset: 0x0009F414
		public static GetSessionStateRequest DeserializeLengthDelimited(Stream stream, GetSessionStateRequest instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetSessionStateRequest.Deserialize(stream, instance, num);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000A123C File Offset: 0x0009F43C
		public static GetSessionStateRequest Deserialize(Stream stream, GetSessionStateRequest instance, long limit)
		{
			instance.IncludeBillingDisabled = false;
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
					if (num != 16)
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
						instance.IncludeBillingDisabled = ProtocolParser.ReadBool(stream);
					}
				}
				else if (instance.Handle == null)
				{
					instance.Handle = GameAccountHandle.DeserializeLengthDelimited(stream);
				}
				else
				{
					GameAccountHandle.DeserializeLengthDelimited(stream, instance.Handle);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000A12F5 File Offset: 0x0009F4F5
		public void Serialize(Stream stream)
		{
			GetSessionStateRequest.Serialize(stream, this);
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000A1300 File Offset: 0x0009F500
		public static void Serialize(Stream stream, GetSessionStateRequest instance)
		{
			if (instance.HasHandle)
			{
				stream.WriteByte(10);
				ProtocolParser.WriteUInt32(stream, instance.Handle.GetSerializedSize());
				GameAccountHandle.Serialize(stream, instance.Handle);
			}
			if (instance.HasIncludeBillingDisabled)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IncludeBillingDisabled);
			}
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000A1358 File Offset: 0x0009F558
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasHandle)
			{
				num += 1U;
				uint serializedSize = this.Handle.GetSerializedSize();
				num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
			}
			if (this.HasIncludeBillingDisabled)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400130D RID: 4877
		public bool HasHandle;

		// Token: 0x0400130E RID: 4878
		private GameAccountHandle _Handle;

		// Token: 0x0400130F RID: 4879
		public bool HasIncludeBillingDisabled;

		// Token: 0x04001310 RID: 4880
		private bool _IncludeBillingDisabled;
	}
}
