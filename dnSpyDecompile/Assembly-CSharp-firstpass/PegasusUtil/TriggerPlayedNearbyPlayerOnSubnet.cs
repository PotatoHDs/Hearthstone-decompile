using System;
using System.IO;

namespace PegasusUtil
{
	// Token: 0x02000071 RID: 113
	public class TriggerPlayedNearbyPlayerOnSubnet : IProtoBuf
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001A6D5 File Offset: 0x000188D5
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001A6DD File Offset: 0x000188DD
		public NearbyPlayer LastPlayed { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001A6E6 File Offset: 0x000188E6
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001A6EE File Offset: 0x000188EE
		public NearbyPlayer OtherPlayer { get; set; }

		// Token: 0x06000723 RID: 1827 RVA: 0x0001A6F7 File Offset: 0x000188F7
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.LastPlayed.GetHashCode() ^ this.OtherPlayer.GetHashCode();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001A71C File Offset: 0x0001891C
		public override bool Equals(object obj)
		{
			TriggerPlayedNearbyPlayerOnSubnet triggerPlayedNearbyPlayerOnSubnet = obj as TriggerPlayedNearbyPlayerOnSubnet;
			return triggerPlayedNearbyPlayerOnSubnet != null && this.LastPlayed.Equals(triggerPlayedNearbyPlayerOnSubnet.LastPlayed) && this.OtherPlayer.Equals(triggerPlayedNearbyPlayerOnSubnet.OtherPlayer);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001A760 File Offset: 0x00018960
		public void Deserialize(Stream stream)
		{
			TriggerPlayedNearbyPlayerOnSubnet.Deserialize(stream, this);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001A76A File Offset: 0x0001896A
		public static TriggerPlayedNearbyPlayerOnSubnet Deserialize(Stream stream, TriggerPlayedNearbyPlayerOnSubnet instance)
		{
			return TriggerPlayedNearbyPlayerOnSubnet.Deserialize(stream, instance, -1L);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001A778 File Offset: 0x00018978
		public static TriggerPlayedNearbyPlayerOnSubnet DeserializeLengthDelimited(Stream stream)
		{
			TriggerPlayedNearbyPlayerOnSubnet triggerPlayedNearbyPlayerOnSubnet = new TriggerPlayedNearbyPlayerOnSubnet();
			TriggerPlayedNearbyPlayerOnSubnet.DeserializeLengthDelimited(stream, triggerPlayedNearbyPlayerOnSubnet);
			return triggerPlayedNearbyPlayerOnSubnet;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001A794 File Offset: 0x00018994
		public static TriggerPlayedNearbyPlayerOnSubnet DeserializeLengthDelimited(Stream stream, TriggerPlayedNearbyPlayerOnSubnet instance)
		{
			long num = (long)((ulong)ProtocolParser.ReadUInt32(stream));
			num += stream.Position;
			return TriggerPlayedNearbyPlayerOnSubnet.Deserialize(stream, instance, num);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001A7BC File Offset: 0x000189BC
		public static TriggerPlayedNearbyPlayerOnSubnet Deserialize(Stream stream, TriggerPlayedNearbyPlayerOnSubnet instance, long limit)
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
					else if (instance.OtherPlayer == null)
					{
						instance.OtherPlayer = NearbyPlayer.DeserializeLengthDelimited(stream);
					}
					else
					{
						NearbyPlayer.DeserializeLengthDelimited(stream, instance.OtherPlayer);
					}
				}
				else if (instance.LastPlayed == null)
				{
					instance.LastPlayed = NearbyPlayer.DeserializeLengthDelimited(stream);
				}
				else
				{
					NearbyPlayer.DeserializeLengthDelimited(stream, instance.LastPlayed);
				}
			}
			if (stream.Position != limit)
			{
				throw new ProtocolBufferException("Read past max limit");
			}
			return instance;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001A88E File Offset: 0x00018A8E
		public void Serialize(Stream stream)
		{
			TriggerPlayedNearbyPlayerOnSubnet.Serialize(stream, this);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001A898 File Offset: 0x00018A98
		public static void Serialize(Stream stream, TriggerPlayedNearbyPlayerOnSubnet instance)
		{
			if (instance.LastPlayed == null)
			{
				throw new ArgumentNullException("LastPlayed", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteUInt32(stream, instance.LastPlayed.GetSerializedSize());
			NearbyPlayer.Serialize(stream, instance.LastPlayed);
			if (instance.OtherPlayer == null)
			{
				throw new ArgumentNullException("OtherPlayer", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteUInt32(stream, instance.OtherPlayer.GetSerializedSize());
			NearbyPlayer.Serialize(stream, instance.OtherPlayer);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001A920 File Offset: 0x00018B20
		public uint GetSerializedSize()
		{
			uint num = 0U;
			uint serializedSize = this.LastPlayed.GetSerializedSize();
			uint num2 = num + (serializedSize + ProtocolParser.SizeOfUInt32(serializedSize));
			uint serializedSize2 = this.OtherPlayer.GetSerializedSize();
			return num2 + (serializedSize2 + ProtocolParser.SizeOfUInt32(serializedSize2)) + 2U;
		}

		// Token: 0x02000583 RID: 1411
		public enum PacketID
		{
			// Token: 0x04001EE5 RID: 7909
			ID = 298,
			// Token: 0x04001EE6 RID: 7910
			System = 0
		}
	}
}
