using System;
using System.Collections.Generic;
using System.IO;

namespace bnet.protocol.games.v1
{
	// Token: 0x020003AE RID: 942
	public class AddPlayersResponse : IProtoBuf
	{
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000C578A File Offset: 0x000C398A
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x000C5792 File Offset: 0x000C3992
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

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000C578A File Offset: 0x000C398A
		public List<ConnectInfo> ConnectInfoList
		{
			get
			{
				return this._ConnectInfo;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003D51 RID: 15697 RVA: 0x000C579B File Offset: 0x000C399B
		public int ConnectInfoCount
		{
			get
			{
				return this._ConnectInfo.Count;
			}
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000C57A8 File Offset: 0x000C39A8
		public void AddConnectInfo(ConnectInfo val)
		{
			this._ConnectInfo.Add(val);
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000C57B6 File Offset: 0x000C39B6
		public void ClearConnectInfo()
		{
			this._ConnectInfo.Clear();
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000C57C3 File Offset: 0x000C39C3
		public void SetConnectInfo(List<ConnectInfo> val)
		{
			this.ConnectInfo = val;
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000C57CC File Offset: 0x000C39CC
		public override int GetHashCode()
		{
			int num = base.GetType().GetHashCode();
			foreach (ConnectInfo connectInfo in this.ConnectInfo)
			{
				num ^= connectInfo.GetHashCode();
			}
			return num;
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000C5830 File Offset: 0x000C3A30
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

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x00003D6E File Offset: 0x00001F6E
		public bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000C589B File Offset: 0x000C3A9B
		public static AddPlayersResponse ParseFrom(byte[] bs)
		{
			return ProtobufUtil.ParseFrom<AddPlayersResponse>(bs, 0, -1);
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000C58A5 File Offset: 0x000C3AA5
		public void Deserialize(Stream stream)
		{
			AddPlayersResponse.Deserialize(stream, this);
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000C58AF File Offset: 0x000C3AAF
		public static AddPlayersResponse Deserialize(Stream stream, AddPlayersResponse instance)
		{
			return AddPlayersResponse.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000C58BC File Offset: 0x000C3ABC
		public static AddPlayersResponse DeserializeLengthDelimited(Stream stream)
		{
			AddPlayersResponse addPlayersResponse = new AddPlayersResponse();
			AddPlayersResponse.DeserializeLengthDelimited(stream, addPlayersResponse);
			return addPlayersResponse;
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000C58D8 File Offset: 0x000C3AD8
		public static AddPlayersResponse DeserializeLengthDelimited(Stream stream, AddPlayersResponse instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return AddPlayersResponse.Deserialize(stream, instance, num);
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000C5900 File Offset: 0x000C3B00
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

		// Token: 0x06003D5E RID: 15710 RVA: 0x000C5998 File Offset: 0x000C3B98
		public void Serialize(Stream stream)
		{
			AddPlayersResponse.Serialize(stream, this);
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000C59A4 File Offset: 0x000C3BA4
		public static void Serialize(Stream stream, AddPlayersResponse instance)
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

		// Token: 0x06003D60 RID: 15712 RVA: 0x000C5A1C File Offset: 0x000C3C1C
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

		// Token: 0x040015D5 RID: 5589
		private List<ConnectInfo> _ConnectInfo = new List<ConnectInfo>();
	}
}
