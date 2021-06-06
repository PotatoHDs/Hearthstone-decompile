using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003A9 RID: 937
	public class CreateGameResponse : IProtoBuf
	{
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06003CE1 RID: 15585 RVA: 0x000C45C0 File Offset: 0x000C27C0
		// (set) Token: 0x06003CE2 RID: 15586 RVA: 0x000C45C8 File Offset: 0x000C27C8
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

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x000C45C0 File Offset: 0x000C27C0
		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000C45D1 File Offset: 0x000C27D1
		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000C45DE File Offset: 0x000C27DE
		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000C45EC File Offset: 0x000C27EC
		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000C45F9 File Offset: 0x000C27F9
		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000C4604 File Offset: 0x000C2804
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000C4668 File Offset: 0x000C2868
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

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000C46D3 File Offset: 0x000C28D3
		public static CreateGameResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<CreateGameResponse>(bs, 0, -1);
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000C46DD File Offset: 0x000C28DD
		public void Deserialize(Stream stream)
		{
			CreateGameResponse.Deserialize(stream, this);
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000C46E7 File Offset: 0x000C28E7
		public static CreateGameResponse Deserialize(Stream stream, CreateGameResponse instance)
		{
			return CreateGameResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x000C46F4 File Offset: 0x000C28F4
		public static CreateGameResponse DeserializeLengthDelimited(Stream stream)
		{
			CreateGameResponse createGameResponse = new CreateGameResponse();
			CreateGameResponse.DeserializeLengthDelimited(stream, createGameResponse);
			return createGameResponse;
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000C4710 File Offset: 0x000C2910
		public static CreateGameResponse DeserializeLengthDelimited(Stream stream, CreateGameResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return CreateGameResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000C4738 File Offset: 0x000C2938
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
					instance.ConnectInfo.Add(bnet.protocol.games.v1.ConnectInfo.DeserializeLengthDelimited(stream));
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

		// Token: 0x06003CF1 RID: 15601 RVA: 0x000C47D0 File Offset: 0x000C29D0
		public void Serialize(Stream stream)
		{
			CreateGameResponse.Serialize(stream, this);
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000C47DC File Offset: 0x000C29DC
		public static void Serialize(Stream stream, CreateGameResponse instance)
		{
			if (instance.ConnectInfo.Count > 0)
			{
				foreach (ConnectInfo connectInfo in instance.ConnectInfo)
				{
					stream.WriteByte(10);
					ProtocolParser.WriteUInt32(stream, connectInfo.GetSerializedSize());
					bnet.protocol.games.v1.ConnectInfo.Serialize(stream, connectInfo);
				}
			}
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000C4854 File Offset: 0x000C2A54
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

		// Token: 0x040015CB RID: 5579
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
