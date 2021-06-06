using System;
using System.IO;

namespace bnet.protocol.friends.v2.client
{
	// Token: 0x0200040E RID: 1038
	public class GetFriendsOptions : IProtoBuf
	{
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x060044FB RID: 17659 RVA: 0x000D8CAB File Offset: 0x000D6EAB
		// (set) Token: 0x060044FC RID: 17660 RVA: 0x000D8CB3 File Offset: 0x000D6EB3
		public bool FetchNames
		{
			get
			{
				return this._FetchNames;
			}
			set
			{
				this._FetchNames = value;
				this.HasFetchNames = true;
			}
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x000D8CC3 File Offset: 0x000D6EC3
		public void SetFetchNames(bool val)
		{
			this.FetchNames = val;
		}

		// Token: 0x060044FE RID: 17662 RVA: 0x000D8CCC File Offset: 0x000D6ECC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			if (this.HasFetchNames)
			{
				num ^= this.FetchNames.GetHashCode();
			}
			return num;
		}

		// Token: 0x060044FF RID: 17663 RVA: 0x000D8D00 File Offset: 0x000D6F00
		public override bool Equals(object obj)
		{
			GetFriendsOptions getFriendsOptions = obj as GetFriendsOptions;
			return getFriendsOptions != null && this.HasFetchNames == getFriendsOptions.HasFetchNames && (!this.HasFetchNames || this.FetchNames.Equals(getFriendsOptions.FetchNames));
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x000D8D48 File Offset: 0x000D6F48
		public static GetFriendsOptions ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<GetFriendsOptions>(bs, 0, -1);
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x000D8D52 File Offset: 0x000D6F52
		public void Deserialize(Stream stream)
		{
			GetFriendsOptions.Deserialize(stream, this);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x000D8D5C File Offset: 0x000D6F5C
		public static GetFriendsOptions Deserialize(Stream stream, GetFriendsOptions instance)
		{
			return GetFriendsOptions.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000D8D68 File Offset: 0x000D6F68
		public static GetFriendsOptions DeserializeLengthDelimited(Stream stream)
		{
			GetFriendsOptions getFriendsOptions = new GetFriendsOptions();
			GetFriendsOptions.DeserializeLengthDelimited(stream, getFriendsOptions);
			return getFriendsOptions;
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x000D8D84 File Offset: 0x000D6F84
		public static GetFriendsOptions DeserializeLengthDelimited(Stream stream, GetFriendsOptions instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return GetFriendsOptions.Deserialize(stream, instance, num);
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x000D8DAC File Offset: 0x000D6FAC
		public static GetFriendsOptions Deserialize(Stream stream, GetFriendsOptions instance, long limit)
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
				else if (num == 8)
				{
					instance.FetchNames = ProtocolParser.ReadBool(stream);
				}
				else
				{
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

		// Token: 0x06004507 RID: 17671 RVA: 0x000D8E2B File Offset: 0x000D702B
		public void Serialize(Stream stream)
		{
			GetFriendsOptions.Serialize(stream, this);
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x000D8E34 File Offset: 0x000D7034
		public static void Serialize(Stream stream, GetFriendsOptions instance)
		{
			if (instance.HasFetchNames)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.FetchNames);
			}
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x000D8E54 File Offset: 0x000D7054
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.HasFetchNames)
			{
				num += 1U;
				num += 1U;
			}
			return num;
		}

		// Token: 0x0400173D RID: 5949
		public bool HasFetchNames;

		// Token: 0x0400173E RID: 5950
		private bool _FetchNames;
	}
}
