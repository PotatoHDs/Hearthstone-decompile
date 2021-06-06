using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.matchmaking.v1
{
	// Token: 0x020003B6 RID: 950
	public class CreateGameResponse : IProtoBuf
	{
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x000C7197 File Offset: 0x000C5397
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x000C719F File Offset: 0x000C539F
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

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06003DE1 RID: 15841 RVA: 0x000C7197 File Offset: 0x000C5397
		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x000C71A8 File Offset: 0x000C53A8
		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000C71B5 File Offset: 0x000C53B5
		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x000C71C3 File Offset: 0x000C53C3
		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x000C71D0 File Offset: 0x000C53D0
		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x000C71DC File Offset: 0x000C53DC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x000C7240 File Offset: 0x000C5440
		public override bool Equals(object obj)
		{
			CreateGameResponse createGameResponse = obj as CreateGameResponse;
			if (createGameResponse == null)
			{
				return false;
			}
			if (this.ConnectInfo.Count != createGameResponse.ConnectInfo.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ConnectInfo.Count; i++)
			{
				if (!this.ConnectInfo[i].Equals(createGameResponse.ConnectInfo[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000C72AB File Offset: 0x000C54AB
		public static CreateGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameResponse>(bs, 0, -1);
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000C72B5 File Offset: 0x000C54B5
		public void Deserialize(Stream stream)
		{
			CreateGameResponse.Deserialize(stream, this);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000C72BF File Offset: 0x000C54BF
		public static CreateGameResponse Deserialize(Stream stream, CreateGameResponse instance)
		{
			return CreateGameResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000C72CC File Offset: 0x000C54CC
		public static CreateGameResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateGameResponse createGameResponse = new CreateGameResponse();
			CreateGameResponse.DeserializeLengthDelimited(stream, createGameResponse);
			return createGameResponse;
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x000C72E8 File Offset: 0x000C54E8
		public static CreateGameResponse DeserializeLengthDelimited(Stream stream, CreateGameResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000C7310 File Offset: 0x000C5510
		public static CreateGameResponse Deserialize(Stream stream, CreateGameResponse instance, long limit)
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

		// Token: 0x06003DEF RID: 15855 RVA: 0x000C73A8 File Offset: 0x000C55A8
		public void Serialize(Stream stream)
		{
			CreateGameResponse.Serialize(stream, this);
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000C73B4 File Offset: 0x000C55B4
		public static void Serialize(Stream stream, CreateGameResponse instance)
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

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000C742C File Offset: 0x000C562C
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

		// Token: 0x040015F7 RID: 5623
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
