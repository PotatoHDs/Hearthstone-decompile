using System.IO;

namespace PegasusUtil
{
	public class PVPDRSessionInfo : IProtoBuf
	{
		public bool HasHasSession;

		private bool _HasSession;

		public bool HasIsActive;

		private bool _IsActive;

		public bool HasSeason;

		private uint _Season;

		public bool HasWins;

		private uint _Wins;

		public bool HasLosses;

		private uint _Losses;

		public bool HasDeckId;

		private long _DeckId;

		public bool HasIsPaidEntry;

		private bool _IsPaidEntry;

		public bool HasSession
		{
			get
			{
				return _HasSession;
			}
			set
			{
				_HasSession = value;
				HasHasSession = true;
			}
		}

		public bool IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				_IsActive = value;
				HasIsActive = true;
			}
		}

		public uint Season
		{
			get
			{
				return _Season;
			}
			set
			{
				_Season = value;
				HasSeason = true;
			}
		}

		public uint Wins
		{
			get
			{
				return _Wins;
			}
			set
			{
				_Wins = value;
				HasWins = true;
			}
		}

		public uint Losses
		{
			get
			{
				return _Losses;
			}
			set
			{
				_Losses = value;
				HasLosses = true;
			}
		}

		public long DeckId
		{
			get
			{
				return _DeckId;
			}
			set
			{
				_DeckId = value;
				HasDeckId = true;
			}
		}

		public bool IsPaidEntry
		{
			get
			{
				return _IsPaidEntry;
			}
			set
			{
				_IsPaidEntry = value;
				HasIsPaidEntry = true;
			}
		}

		public override int GetHashCode()
		{
			int num = GetType().GetHashCode();
			if (HasHasSession)
			{
				num ^= HasSession.GetHashCode();
			}
			if (HasIsActive)
			{
				num ^= IsActive.GetHashCode();
			}
			if (HasSeason)
			{
				num ^= Season.GetHashCode();
			}
			if (HasWins)
			{
				num ^= Wins.GetHashCode();
			}
			if (HasLosses)
			{
				num ^= Losses.GetHashCode();
			}
			if (HasDeckId)
			{
				num ^= DeckId.GetHashCode();
			}
			if (HasIsPaidEntry)
			{
				num ^= IsPaidEntry.GetHashCode();
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			PVPDRSessionInfo pVPDRSessionInfo = obj as PVPDRSessionInfo;
			if (pVPDRSessionInfo == null)
			{
				return false;
			}
			if (HasHasSession != pVPDRSessionInfo.HasHasSession || (HasHasSession && !HasSession.Equals(pVPDRSessionInfo.HasSession)))
			{
				return false;
			}
			if (HasIsActive != pVPDRSessionInfo.HasIsActive || (HasIsActive && !IsActive.Equals(pVPDRSessionInfo.IsActive)))
			{
				return false;
			}
			if (HasSeason != pVPDRSessionInfo.HasSeason || (HasSeason && !Season.Equals(pVPDRSessionInfo.Season)))
			{
				return false;
			}
			if (HasWins != pVPDRSessionInfo.HasWins || (HasWins && !Wins.Equals(pVPDRSessionInfo.Wins)))
			{
				return false;
			}
			if (HasLosses != pVPDRSessionInfo.HasLosses || (HasLosses && !Losses.Equals(pVPDRSessionInfo.Losses)))
			{
				return false;
			}
			if (HasDeckId != pVPDRSessionInfo.HasDeckId || (HasDeckId && !DeckId.Equals(pVPDRSessionInfo.DeckId)))
			{
				return false;
			}
			if (HasIsPaidEntry != pVPDRSessionInfo.HasIsPaidEntry || (HasIsPaidEntry && !IsPaidEntry.Equals(pVPDRSessionInfo.IsPaidEntry)))
			{
				return false;
			}
			return true;
		}

		public void Deserialize(Stream stream)
		{
			Deserialize(stream, this);
		}

		public static PVPDRSessionInfo Deserialize(Stream stream, PVPDRSessionInfo instance)
		{
			return Deserialize(stream, instance, -1L);
		}

		public static PVPDRSessionInfo DeserializeLengthDelimited(Stream stream)
		{
			PVPDRSessionInfo pVPDRSessionInfo = new PVPDRSessionInfo();
			DeserializeLengthDelimited(stream, pVPDRSessionInfo);
			return pVPDRSessionInfo;
		}

		public static PVPDRSessionInfo DeserializeLengthDelimited(Stream stream, PVPDRSessionInfo instance)
		{
			long num = ProtocolParser.ReadUInt32(stream);
			num += stream.Position;
			return Deserialize(stream, instance, num);
		}

		public static PVPDRSessionInfo Deserialize(Stream stream, PVPDRSessionInfo instance, long limit)
		{
			while (true)
			{
				if (limit >= 0 && stream.Position >= limit)
				{
					if (stream.Position == limit)
					{
						break;
					}
					throw new ProtocolBufferException("Read past max limit");
				}
				int num = stream.ReadByte();
				switch (num)
				{
				case -1:
					break;
				case 8:
					instance.HasSession = ProtocolParser.ReadBool(stream);
					continue;
				case 16:
					instance.IsActive = ProtocolParser.ReadBool(stream);
					continue;
				case 24:
					instance.Season = ProtocolParser.ReadUInt32(stream);
					continue;
				case 32:
					instance.Wins = ProtocolParser.ReadUInt32(stream);
					continue;
				case 40:
					instance.Losses = ProtocolParser.ReadUInt32(stream);
					continue;
				case 48:
					instance.DeckId = (long)ProtocolParser.ReadUInt64(stream);
					continue;
				case 56:
					instance.IsPaidEntry = ProtocolParser.ReadBool(stream);
					continue;
				default:
				{
					Key key = ProtocolParser.ReadKey((byte)num, stream);
					if (key.Field == 0)
					{
						throw new ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
					}
					ProtocolParser.SkipKey(stream, key);
					continue;
				}
				}
				if (limit < 0)
				{
					break;
				}
				throw new EndOfStreamException();
			}
			return instance;
		}

		public void Serialize(Stream stream)
		{
			Serialize(stream, this);
		}

		public static void Serialize(Stream stream, PVPDRSessionInfo instance)
		{
			if (instance.HasHasSession)
			{
				stream.WriteByte(8);
				ProtocolParser.WriteBool(stream, instance.HasSession);
			}
			if (instance.HasIsActive)
			{
				stream.WriteByte(16);
				ProtocolParser.WriteBool(stream, instance.IsActive);
			}
			if (instance.HasSeason)
			{
				stream.WriteByte(24);
				ProtocolParser.WriteUInt32(stream, instance.Season);
			}
			if (instance.HasWins)
			{
				stream.WriteByte(32);
				ProtocolParser.WriteUInt32(stream, instance.Wins);
			}
			if (instance.HasLosses)
			{
				stream.WriteByte(40);
				ProtocolParser.WriteUInt32(stream, instance.Losses);
			}
			if (instance.HasDeckId)
			{
				stream.WriteByte(48);
				ProtocolParser.WriteUInt64(stream, (ulong)instance.DeckId);
			}
			if (instance.HasIsPaidEntry)
			{
				stream.WriteByte(56);
				ProtocolParser.WriteBool(stream, instance.IsPaidEntry);
			}
		}

		public uint GetSerializedSize()
		{
			uint num = 0u;
			if (HasHasSession)
			{
				num++;
				num++;
			}
			if (HasIsActive)
			{
				num++;
				num++;
			}
			if (HasSeason)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Season);
			}
			if (HasWins)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Wins);
			}
			if (HasLosses)
			{
				num++;
				num += ProtocolParser.SizeOfUInt32(Losses);
			}
			if (HasDeckId)
			{
				num++;
				num += ProtocolParser.SizeOfUInt64((ulong)DeckId);
			}
			if (HasIsPaidEntry)
			{
				num++;
				num++;
			}
			return num;
		}
	}
}
