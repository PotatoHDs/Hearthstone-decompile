using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003B8 RID: 952
	public class AddPlayersResponse : IProtoBuf
	{
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x000C78AF File Offset: 0x000C5AAF
		// (set) Token: 0x06003E0B RID: 15883 RVA: 0x000C78B7 File Offset: 0x000C5AB7
		public List<ConnectInfo> ConnectInfo
		{
			get
			{
				return this._ConnectInfo;
			}
			set
			{
				this._ConnectInfo = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06003E0C RID: 15884 RVA: 0x000C78AF File Offset: 0x000C5AAF
		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06003E0D RID: 15885 RVA: 0x000C78C0 File Offset: 0x000C5AC0
		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000C78CD File Offset: 0x000C5ACD
		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000C78DB File Offset: 0x000C5ADB
		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000C78E8 File Offset: 0x000C5AE8
		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000C78F4 File Offset: 0x000C5AF4
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000C7958 File Offset: 0x000C5B58
		public override bool Equals(object obj)
		{
			AddPlayersResponse addPlayersResponse = obj as AddPlayersResponse;
			if (addPlayersResponse == null)
			{
				return false;
			}
			if (this.ConnectInfo.Count != addPlayersResponse.ConnectInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ConnectInfo.Count; i++)
			{
				if (!this.ConnectInfo[i].Equals(addPlayersResponse.ConnectInfo[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003E13 RID: 15891 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x000C79C3 File Offset: 0x000C5BC3
		public static AddPlayersResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersResponse>(bs, 0, -1);
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x000C79CD File Offset: 0x000C5BCD
		public void Deserialize(Stream stream)
		{
			AddPlayersResponse.Deserialize(stream, this);
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000C79D7 File Offset: 0x000C5BD7
		public static AddPlayersResponse Deserialize(Stream stream, AddPlayersResponse instance)
		{
			return AddPlayersResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x000C79E4 File Offset: 0x000C5BE4
		public static AddPlayersResponse DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersResponse addPlayersResponse = new AddPlayersResponse();
			AddPlayersResponse.DeserializeLengthDelimited(stream, addPlayersResponse);
			return addPlayersResponse;
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000C7A00 File Offset: 0x000C5C00
		public static AddPlayersResponse DeserializeLengthDelimited(Stream stream, AddPlayersResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddPlayersResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000C7A28 File Offset: 0x000C5C28
		public static AddPlayersResponse Deserialize(Stream stream, AddPlayersResponse instance, long limit)
		{
			if (instance.ConnectInfo == null)
			{
				instance.ConnectInfo = new List<ConnectInfo>();
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
				else if (num == 10)
				{
					instance.ConnectInfo.Add(bnet.protocol.matchmaking.v1.ConnectInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003E1A RID: 15898 RVA: 0x000C7AC0 File Offset: 0x000C5CC0
		public void Serialize(Stream stream)
		{
			AddPlayersResponse.Serialize(stream, this);
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x000C7ACC File Offset: 0x000C5CCC
		public static void Serialize(Stream stream, AddPlayersResponse instance)
		{
			if (instance.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in instance.ConnectInfo)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, connectInfo.GetSerializedSize());
					bnet.protocol.matchmaking.v1.ConnectInfo.Serialize(stream, connectInfo);
				}
			}
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x000C7B44 File Offset: 0x000C5D44
		public uint GetSerializedSize()
		{
			uint num = 0U;
			if (this.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in this.ConnectInfo)
				{
					num += 1U;
					uint serializedSize = connectInfo.GetSerializedSize();
					num += serializedSize + ProtocolParser.SizeOfUInt32(serializedSize);
				}
			}
			return num;
		}

		// Token: 0x040015FB RID: 5627
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
