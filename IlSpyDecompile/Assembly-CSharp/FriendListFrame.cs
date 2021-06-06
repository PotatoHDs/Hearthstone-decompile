using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Blizzard.T5.Core;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusFSG;
using PegasusShared;
using UnityEngine;

public class FriendListFrame : MonoBehaviour
{
	public enum FriendListEditMode
	{
		NONE,
		REMOVE_FRIENDS
	}

	private class NearbyPlayerUpdate
	{
		public enum ChangeType
		{
			Added,
			Removed
		}

		public ChangeType Change;

		public BnetPlayer Player;

		public NearbyPlayerUpdate(ChangeType c, BnetPlayer p)
		{
			Change = c;
			Player = p;
		}

		public override bool Equals(object obj)
		{
			NearbyPlayerUpdate nearbyPlayerUpdate = obj as NearbyPlayerUpdate;
			if (nearbyPlayerUpdate == null)
			{
				return false;
			}
			if (Change != nearbyPlayerUpdate.Change)
			{
				return false;
			}
			return Player.GetAccountId() == nearbyPlayerUpdate.Player.GetAccountId();
		}

		public override int GetHashCode()
		{
			return Player.GetHashCode();
		}
	}

	[Serializable]
	public class Me
	{
		public UberText nameText;

		public AsyncReference m_rankedMedalWidgetReference;
	}

	[Serializable]
	public class RecentOpponent
	{
		public PegUIElement button;

		public UberText nameText;
	}

	[Serializable]
	public class Prefabs
	{
		public FriendListItemHeader headerItem;

		public FriendListItemFooter footerItem;

		public FriendListNearbyPlayersHeader nearbyPlayersHeaderItem;

		public FriendListRequestFrame requestItem;

		public FriendListFriendFrame friendItem;

		public FriendListFSGFrame fsgItem;

		public AddFriendFrame addFriendFrame;
	}

	[Serializable]
	public class ListInfo
	{
		public Transform topLeft;

		public Transform bottomRight;

		public Transform bottomRightWithScrollbar;

		public HeaderBackgroundInfo requestBackgroundInfo;

		public HeaderBackgroundInfo currentGameBackgroundInfo;
	}

	[Serializable]
	public class HeaderBackgroundInfo
	{
		public Mesh mesh;

		public Material material;
	}

	public struct FriendListItem
	{
		private object m_item;

		public MobileFriendListItem.TypeFlags ItemFlags { get; private set; }

		public bool IsHeader => MobileFriendListItem.ItemIsHeader(ItemFlags);

		public MobileFriendListItem.TypeFlags ItemMainType
		{
			get
			{
				if (IsHeader)
				{
					return MobileFriendListItem.TypeFlags.Header;
				}
				return SubType;
			}
		}

		public MobileFriendListItem.TypeFlags SubType => ItemFlags & ~MobileFriendListItem.TypeFlags.Header;

		public BnetPlayer GetFriend()
		{
			if ((ItemFlags & MobileFriendListItem.TypeFlags.Friend) == 0)
			{
				return null;
			}
			return (BnetPlayer)m_item;
		}

		public BnetPlayer GetNearbyPlayer()
		{
			if ((ItemFlags & MobileFriendListItem.TypeFlags.NearbyPlayer) == 0)
			{
				return null;
			}
			return (BnetPlayer)m_item;
		}

		public BnetInvitation GetInvite()
		{
			if ((ItemFlags & MobileFriendListItem.TypeFlags.Request) == 0)
			{
				return null;
			}
			return (BnetInvitation)m_item;
		}

		public FSGConfig GetFSGConfig()
		{
			if ((ItemFlags & MobileFriendListItem.TypeFlags.FoundFiresideGathering) == 0 && (ItemFlags & MobileFriendListItem.TypeFlags.CurrentFiresideGathering) == 0)
			{
				return null;
			}
			return (FSGConfig)m_item;
		}

		public BnetPlayer GetFiresideGatheringPlayer()
		{
			if ((ItemFlags & MobileFriendListItem.TypeFlags.FiresideGatheringPlayer) == 0)
			{
				return null;
			}
			return (BnetPlayer)m_item;
		}

		public string GetText()
		{
			return (string)m_item;
		}

		public override string ToString()
		{
			if (IsHeader)
			{
				return $"[{SubType}]Header";
			}
			return $"[{ItemMainType}]{m_item}";
		}

		public Type GetFrameType()
		{
			switch (ItemMainType)
			{
			case MobileFriendListItem.TypeFlags.Header:
				return typeof(FriendListItemHeader);
			case MobileFriendListItem.TypeFlags.Request:
				return typeof(FriendListRequestFrame);
			case MobileFriendListItem.TypeFlags.NearbyPlayer:
				return typeof(FriendListFriendFrame);
			case MobileFriendListItem.TypeFlags.Friend:
			case MobileFriendListItem.TypeFlags.CurrentGame:
				return typeof(FriendListFriendFrame);
			case MobileFriendListItem.TypeFlags.CurrentFiresideGathering:
			case MobileFriendListItem.TypeFlags.FoundFiresideGathering:
				return typeof(FriendListFSGFrame);
			case MobileFriendListItem.TypeFlags.FiresideGatheringPlayer:
				return typeof(FriendListFriendFrame);
			case MobileFriendListItem.TypeFlags.FiresideGatheringFooter:
				return typeof(FriendListItemFooter);
			default:
				throw new Exception(string.Concat("Unknown ItemType: ", ItemFlags, " (", (int)ItemFlags, ")"));
			}
		}

		public FriendListItem(bool isHeader, MobileFriendListItem.TypeFlags itemType, object itemData)
		{
			this = default(FriendListItem);
			if (!isHeader && itemData == null)
			{
				Log.All.Print("FriendListItem: itemData is null! itemType=" + itemType);
			}
			m_item = itemData;
			ItemFlags = itemType;
			if (isHeader)
			{
				ItemFlags |= MobileFriendListItem.TypeFlags.Header;
			}
			else
			{
				ItemFlags &= ~MobileFriendListItem.TypeFlags.Header;
			}
		}
	}

	private class VirtualizedFriendsListBehavior : TouchList.ILongListBehavior
	{
		private FriendListFrame m_friendList;

		private int m_cachedMaxVisibleItems = -1;

		private const int MAX_FREELIST_ITEMS = 20;

		private List<MobileFriendListItem> m_freelist;

		private HashSet<MobileFriendListItem> m_acquiredItems = new HashSet<MobileFriendListItem>();

		private Bounds[] m_boundsByType;

		public List<MobileFriendListItem> FreeList => m_freelist;

		public int AllItemsCount => m_friendList.m_allItems.Count;

		public int MaxVisibleItems
		{
			get
			{
				if (m_cachedMaxVisibleItems >= 0)
				{
					return m_cachedMaxVisibleItems;
				}
				m_cachedMaxVisibleItems = 0;
				UnityEngine.Vector2 clipSize = m_friendList.items.ClipSize;
				Bounds prefabBounds = GetPrefabBounds(m_friendList.prefabs.requestItem.gameObject);
				Bounds prefabBounds2 = GetPrefabBounds(m_friendList.prefabs.friendItem.gameObject);
				float a = prefabBounds.max.y - prefabBounds.min.y;
				float b = prefabBounds2.max.y - prefabBounds2.min.y;
				float num = Mathf.Min(a, b);
				if (num > 0f)
				{
					int num2 = Mathf.CeilToInt(clipSize.y / num);
					m_cachedMaxVisibleItems = num2 + 3;
				}
				return m_cachedMaxVisibleItems;
			}
		}

		public int MinBuffer => 2;

		public int MaxAcquiredItems => MaxVisibleItems + 2 * MinBuffer;

		private bool HasCollapsedHeaders
		{
			get
			{
				foreach (KeyValuePair<MobileFriendListItem.TypeFlags, FriendListItemHeader> header in m_friendList.m_headers)
				{
					if (!header.Value.IsShowingContents)
					{
						return true;
					}
				}
				return false;
			}
		}

		public VirtualizedFriendsListBehavior(FriendListFrame friendList)
		{
			m_friendList = friendList;
		}

		private static Bounds GetPrefabBounds(GameObject prefabGameObject)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(prefabGameObject);
			gameObject.SetActive(value: true);
			Bounds result = TransformUtil.ComputeSetPointBounds(gameObject);
			UnityEngine.Object.DestroyImmediate(gameObject);
			return result;
		}

		public bool IsItemShowable(int allItemsIndex)
		{
			if (allItemsIndex < 0 || allItemsIndex >= AllItemsCount)
			{
				return false;
			}
			FriendListItem friendListItem = m_friendList.m_allItems[allItemsIndex];
			if (friendListItem.IsHeader && !m_friendList.IsInEditMode)
			{
				return true;
			}
			if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter && !m_friendList.IsInEditMode)
			{
				return true;
			}
			if (friendListItem.ItemMainType != MobileFriendListItem.TypeFlags.FoundFiresideGathering && friendListItem.ItemMainType != MobileFriendListItem.TypeFlags.CurrentFiresideGathering && friendListItem.ItemMainType != MobileFriendListItem.TypeFlags.FiresideGatheringPlayer)
			{
				FriendListItemHeader friendListItemHeader = m_friendList.FindHeader(friendListItem.SubType);
				if (friendListItemHeader == null || !friendListItemHeader.IsShowingContents)
				{
					return false;
				}
			}
			if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.FoundFiresideGathering)
			{
				if (FiresideGatheringManager.Get().IsCheckedIn)
				{
					return false;
				}
				if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB) && friendListItem.GetFSGConfig().IsInnkeeper && !friendListItem.GetFSGConfig().IsSetupComplete)
				{
					return false;
				}
			}
			if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
			{
				if (!FiresideGatheringManager.Get().IsCheckedIn)
				{
					return false;
				}
				if (!FiresideGatheringManager.Get().IsCheckedInToFSG(friendListItem.GetFSGConfig().FsgId))
				{
					return false;
				}
			}
			if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.Friend && BnetNearbyPlayerMgr.Get().IsNearbyPlayer(friendListItem.GetFriend()))
			{
				return false;
			}
			if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.NearbyPlayer && FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(friendListItem.GetNearbyPlayer()))
			{
				return false;
			}
			if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer && (!friendListItem.GetFiresideGatheringPlayer().IsOnline() || friendListItem.GetFiresideGatheringPlayer().GetBestProgramId() != BnetProgramId.HEARTHSTONE))
			{
				return false;
			}
			if (m_friendList.EditMode == FriendListEditMode.REMOVE_FRIENDS)
			{
				if (friendListItem.ItemMainType != MobileFriendListItem.TypeFlags.Header)
				{
					if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer && !BnetFriendMgr.Get().IsFriend(friendListItem.GetFiresideGatheringPlayer()))
					{
						return false;
					}
					if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.NearbyPlayer && !BnetFriendMgr.Get().IsFriend(friendListItem.GetNearbyPlayer()))
					{
						return false;
					}
					if (friendListItem.ItemFlags == MobileFriendListItem.TypeFlags.FiresideGatheringFooter)
					{
						return false;
					}
					if (friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.FoundFiresideGathering || friendListItem.ItemMainType == MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
					{
						return false;
					}
				}
				else if (friendListItem.ItemFlags == (MobileFriendListItem.TypeFlags.NearbyPlayer | MobileFriendListItem.TypeFlags.Header))
				{
					return false;
				}
			}
			return true;
		}

		public Vector3 GetItemSize(int allItemsIndex)
		{
			if (allItemsIndex < 0 || allItemsIndex >= AllItemsCount)
			{
				return Vector3.zero;
			}
			FriendListItem friendListItem = m_friendList.m_allItems[allItemsIndex];
			if (m_boundsByType == null)
			{
				InitializeBoundsByTypeArray();
			}
			int boundsByTypeIndex = GetBoundsByTypeIndex(friendListItem.ItemMainType);
			return m_boundsByType[boundsByTypeIndex].size;
		}

		public void ReleaseAllItems()
		{
			if (m_acquiredItems.Count == 0)
			{
				return;
			}
			if (m_freelist == null)
			{
				m_freelist = new List<MobileFriendListItem>();
			}
			foreach (MobileFriendListItem acquiredItem in m_acquiredItems)
			{
				if (acquiredItem.IsHeader)
				{
					acquiredItem.gameObject.SetActive(value: false);
				}
				else if (m_freelist.Count >= 20)
				{
					UnityEngine.Object.Destroy(acquiredItem.gameObject);
				}
				else
				{
					m_freelist.Add(acquiredItem);
					acquiredItem.gameObject.SetActive(value: false);
				}
				acquiredItem.Unselected();
			}
			m_acquiredItems.Clear();
		}

		public void ReleaseItem(ITouchListItem item)
		{
			MobileFriendListItem mobileFriendListItem = item as MobileFriendListItem;
			if (mobileFriendListItem == null)
			{
				throw new ArgumentException("given item is not MobileFriendListItem: " + item);
			}
			if (m_freelist == null)
			{
				m_freelist = new List<MobileFriendListItem>();
			}
			if (mobileFriendListItem.IsHeader)
			{
				mobileFriendListItem.gameObject.SetActive(value: false);
			}
			else if (m_freelist.Count >= 20)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
			else
			{
				m_freelist.Add(mobileFriendListItem);
				mobileFriendListItem.gameObject.SetActive(value: false);
			}
			if (!m_acquiredItems.Remove(mobileFriendListItem))
			{
				Log.All.Print("VirtualizedFriendsListBehavior.ReleaseItem item not found in m_acquiredItems: {0}", mobileFriendListItem);
			}
			mobileFriendListItem.Unselected();
		}

		public ITouchListItem AcquireItem(int index)
		{
			if (m_acquiredItems.Count >= MaxAcquiredItems)
			{
				throw new Exception("Bug in ILongListBehavior? there are too many acquired items! index=" + index + " max=" + MaxAcquiredItems + " maxVisible=" + MaxVisibleItems + " minBuffer=" + MinBuffer + " acquiredItems.Count=" + m_acquiredItems.Count + " hasCollapsedHeaders=" + HasCollapsedHeaders.ToString());
			}
			if (index < 0 || index >= m_friendList.m_allItems.Count)
			{
				throw new IndexOutOfRangeException($"Invalid index, {DebugUtils.GetHierarchyPathAndType(m_friendList)} has {m_friendList.m_allItems.Count} elements.");
			}
			FriendListItem item = m_friendList.m_allItems[index];
			MobileFriendListItem.TypeFlags itemMainType = item.ItemMainType;
			Type frameType = item.GetFrameType();
			if (m_freelist != null && !item.IsHeader)
			{
				int num = m_freelist.FindLastIndex((MobileFriendListItem m) => (!item.IsHeader) ? (m.GetComponent(frameType) != null) : m.IsHeader);
				if (num >= 0 && m_freelist[num] == null)
				{
					for (int i = 0; i < m_freelist.Count; i++)
					{
						if (m_freelist[i] == null)
						{
							m_freelist.RemoveAt(i);
							i--;
						}
					}
					num = m_freelist.FindLastIndex((MobileFriendListItem m) => (!item.IsHeader) ? (m.GetComponent(frameType) != null) : m.IsHeader);
				}
				if (num >= 0)
				{
					MobileFriendListItem mobileFriendListItem = m_freelist[num];
					m_freelist.RemoveAt(num);
					mobileFriendListItem.gameObject.SetActive(value: true);
					switch (itemMainType)
					{
					case MobileFriendListItem.TypeFlags.Friend:
					case MobileFriendListItem.TypeFlags.NearbyPlayer:
					case MobileFriendListItem.TypeFlags.FiresideGatheringPlayer:
					{
						FriendListFriendFrame component4 = mobileFriendListItem.GetComponent<FriendListFriendFrame>();
						component4.gameObject.SetActive(value: true);
						BnetPlayer player = null;
						bool flag = false;
						switch (itemMainType)
						{
						case MobileFriendListItem.TypeFlags.Friend:
							player = item.GetFriend();
							break;
						case MobileFriendListItem.TypeFlags.NearbyPlayer:
							player = item.GetNearbyPlayer();
							break;
						case MobileFriendListItem.TypeFlags.FiresideGatheringPlayer:
							player = item.GetFiresideGatheringPlayer();
							flag = true;
							break;
						}
						component4.Initialize(player, flag);
						FriendListItemHeader parent = (flag ? null : m_friendList.FindHeader(itemMainType));
						m_friendList.FinishCreateVisualItem(component4, itemMainType, parent, component4.gameObject);
						break;
					}
					case MobileFriendListItem.TypeFlags.Request:
					{
						FriendListRequestFrame component3 = mobileFriendListItem.GetComponent<FriendListRequestFrame>();
						component3.gameObject.SetActive(value: true);
						component3.SetInvite(item.GetInvite());
						m_friendList.FinishCreateVisualItem(component3, itemMainType, m_friendList.FindHeader(itemMainType), component3.gameObject);
						component3.UpdateInvite();
						break;
					}
					case MobileFriendListItem.TypeFlags.CurrentFiresideGathering:
					case MobileFriendListItem.TypeFlags.FoundFiresideGathering:
					{
						FriendListFSGFrame component2 = mobileFriendListItem.GetComponent<FriendListFSGFrame>();
						component2.InitFrame(item.GetFSGConfig());
						m_friendList.FinishCreateVisualItem(component2, itemMainType, null, component2.gameObject);
						break;
					}
					case MobileFriendListItem.TypeFlags.FiresideGatheringFooter:
					{
						FriendListItemFooter component = mobileFriendListItem.GetComponent<FriendListItemFooter>();
						component.Text = item.GetText();
						m_friendList.FinishCreateVisualItem(component, itemMainType, null, component.gameObject);
						break;
					}
					default:
						throw new NotImplementedException("VirtualizedFriendsListBehavior.AcquireItem[reuse] frameType=" + frameType.FullName + " itemType=" + itemMainType);
					}
					m_acquiredItems.Add(mobileFriendListItem);
					return mobileFriendListItem;
				}
			}
			MobileFriendListItem mobileFriendListItem2 = null;
			switch (itemMainType)
			{
			case MobileFriendListItem.TypeFlags.Header:
				mobileFriendListItem2 = m_friendList.FindHeader(item.SubType).GetComponent<MobileFriendListItem>();
				break;
			case MobileFriendListItem.TypeFlags.Friend:
			{
				BnetPlayer friend = item.GetFriend();
				mobileFriendListItem2 = m_friendList.CreateFriendFrame(friend);
				break;
			}
			case MobileFriendListItem.TypeFlags.Request:
				mobileFriendListItem2 = m_friendList.CreateRequestFrame(item.GetInvite());
				break;
			case MobileFriendListItem.TypeFlags.NearbyPlayer:
				mobileFriendListItem2 = m_friendList.CreateNearbyPlayerFrame(item.GetNearbyPlayer());
				break;
			case MobileFriendListItem.TypeFlags.FoundFiresideGathering:
				mobileFriendListItem2 = m_friendList.CreateFSGFrame(item.GetFSGConfig(), MobileFriendListItem.TypeFlags.FoundFiresideGathering);
				break;
			case MobileFriendListItem.TypeFlags.CurrentFiresideGathering:
				mobileFriendListItem2 = m_friendList.CreateFSGFrame(item.GetFSGConfig(), MobileFriendListItem.TypeFlags.CurrentFiresideGathering);
				break;
			case MobileFriendListItem.TypeFlags.FiresideGatheringPlayer:
				mobileFriendListItem2 = m_friendList.CreateFSGPlayerFrame(item.GetFiresideGatheringPlayer());
				break;
			case MobileFriendListItem.TypeFlags.FiresideGatheringFooter:
				mobileFriendListItem2 = m_friendList.CreateFSGFooter(item.GetText());
				break;
			default:
				throw new NotImplementedException("VirtualizedFriendsListBehavior.AcquireItem[new] type=" + frameType.FullName);
			}
			m_acquiredItems.Add(mobileFriendListItem2);
			return mobileFriendListItem2;
		}

		private void InitializeBoundsByTypeArray()
		{
			Array values = Enum.GetValues(typeof(MobileFriendListItem.TypeFlags));
			m_boundsByType = new Bounds[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				MobileFriendListItem.TypeFlags itemType = (MobileFriendListItem.TypeFlags)values.GetValue(i);
				Component prefab = GetPrefab(itemType);
				int boundsByTypeIndex = GetBoundsByTypeIndex(itemType);
				m_boundsByType[boundsByTypeIndex] = ((prefab == null) ? default(Bounds) : GetPrefabBounds(prefab.gameObject));
			}
		}

		private int GetBoundsByTypeIndex(MobileFriendListItem.TypeFlags itemType)
		{
			return itemType switch
			{
				MobileFriendListItem.TypeFlags.Header => 0, 
				MobileFriendListItem.TypeFlags.Request => 1, 
				MobileFriendListItem.TypeFlags.NearbyPlayer => 2, 
				MobileFriendListItem.TypeFlags.CurrentGame => 3, 
				MobileFriendListItem.TypeFlags.Friend => 4, 
				MobileFriendListItem.TypeFlags.FoundFiresideGathering => 5, 
				MobileFriendListItem.TypeFlags.FiresideGatheringPlayer => 6, 
				MobileFriendListItem.TypeFlags.CurrentFiresideGathering => 7, 
				MobileFriendListItem.TypeFlags.FiresideGatheringFooter => 8, 
				_ => throw new Exception(string.Concat("Unknown ItemType: ", itemType, " (", (int)itemType, ")")), 
			};
		}

		private Component GetPrefab(MobileFriendListItem.TypeFlags itemType)
		{
			switch (itemType)
			{
			case MobileFriendListItem.TypeFlags.Header:
				return m_friendList.prefabs.headerItem;
			case MobileFriendListItem.TypeFlags.Request:
				return m_friendList.prefabs.requestItem;
			case MobileFriendListItem.TypeFlags.NearbyPlayer:
				return m_friendList.prefabs.friendItem;
			case MobileFriendListItem.TypeFlags.Friend:
			case MobileFriendListItem.TypeFlags.CurrentGame:
			case MobileFriendListItem.TypeFlags.FiresideGatheringPlayer:
				return m_friendList.prefabs.friendItem;
			case MobileFriendListItem.TypeFlags.CurrentFiresideGathering:
			case MobileFriendListItem.TypeFlags.FoundFiresideGathering:
				return m_friendList.prefabs.fsgItem;
			case MobileFriendListItem.TypeFlags.FiresideGatheringFooter:
				return m_friendList.prefabs.footerItem;
			default:
				throw new Exception(string.Concat("Unknown ItemType: ", itemType, " (", (int)itemType, ")"));
			}
		}
	}

	public Me me;

	public Prefabs prefabs;

	public ListInfo listInfo;

	public TouchList items;

	public PlayerPortrait myPortrait;

	public PegUIElement addFriendButton;

	public PegUIElement removeFriendButton;

	public GameObject removeFriendButtonEnabledVisual;

	public GameObject removeFriendButtonDisabledVisual;

	public GameObject removeFriendButtonButtonGlow;

	public PegUIElement rafButton;

	public GameObject rafButtonEnabledVisual;

	public GameObject rafButtonDisabledVisual;

	public GameObject rafButtonButtonGlow;

	public GameObject rafButtonGlowBone;

	public TouchListScrollbar scrollbar;

	public NineSliceElement window;

	public PegUIElement fsgButton;

	public GameObject fsgButtonButtonGlow;

	public GameObject portraitBackground;

	public Material unrankedBackground;

	public Material rankedBackground;

	public GameObject innerShadow;

	public GameObject outerShadow;

	public GameObject temporaryAccountPaper;

	public GameObject temporaryAccountCover;

	public GameObject temporaryAccountDrawingBone;

	public GameObject temporaryAccountDrawing;

	public UIBButton temporaryAccountSignUpButton;

	public PegUIElement flyoutMenuButton;

	public GameObject flyoutMenu;

	public float flyoutMiddleFrameScaleOffsetForFSG;

	public float flyoutShadowScaleOffsetForFSG;

	public GameObject flyoutMiddleFrame;

	public GameObject flyoutMiddleShadow;

	public MultiSliceElement flyoutFrameContainer;

	public MultiSliceElement flyoutShadowContainer;

	public HighlightState flyoutButtonGlow;

	private AddFriendFrame m_addFriendFrame;

	private AlertPopup m_removeFriendPopup;

	private Camera m_itemsCamera;

	private FriendListEditMode m_editMode;

	private BnetPlayer m_friendToRemove;

	private bool m_flyoutOpen;

	private bool m_patronStrangersHidden;

	private const int PatronCountHardLimit = 99;

	private RankedMedal m_myRankedMedal;

	private Widget m_myRankedMedalWidget;

	private RankedPlayDataModel m_myRankedDataModel;

	private Coroutine m_updateFriendItemsWhenAvailableCoroutine;

	private List<NearbyPlayerUpdate> m_nearbyPlayerUpdates = new List<NearbyPlayerUpdate>();

	private BnetPlayerChangelist m_playersChangeList = new BnetPlayerChangelist();

	private float m_lastNearbyPlayersUpdate;

	private bool m_nearbyPlayersNeedUpdate;

	private const float NEARBY_PLAYERS_UPDATE_TIME = 10f;

	private bool m_isRAFButtonEnabled = true;

	private bool m_isFSGButtonEnabled = true;

	private List<FriendListItem> m_allItems = new List<FriendListItem>();

	private VirtualizedFriendsListBehavior m_longListBehavior;

	private Dictionary<MobileFriendListItem.TypeFlags, FriendListItemHeader> m_headers = new Dictionary<MobileFriendListItem.TypeFlags, FriendListItemHeader>();

	public bool IsStarted { get; private set; }

	public bool ShowingAddFriendFrame => m_addFriendFrame != null;

	public bool IsInEditMode => m_editMode != FriendListEditMode.NONE;

	public FriendListEditMode EditMode => m_editMode;

	public bool IsFlyoutOpen => m_flyoutOpen;

	public bool IsReady
	{
		get
		{
			foreach (FriendListFriendFrame renderedItem in GetRenderedItems<FriendListFriendFrame>())
			{
				if (renderedItem.gameObject.activeInHierarchy && renderedItem.ShouldShowRankedMedal && !renderedItem.IsRankedMedalReady)
				{
					return false;
				}
			}
			if (m_myRankedMedal == null || !m_myRankedMedal.IsReady)
			{
				return false;
			}
			return true;
		}
	}

	public event Action OnStarted;

	public event Action AddFriendFrameOpened;

	public event Action AddFriendFrameClosed;

	public event Action RemoveFriendPopupOpened;

	public event Action RemoveFriendPopupClosed;

	private void Awake()
	{
		InitButtons();
		RegisterFriendEvents();
		CreateItemsCamera();
		UpdateBackgroundCollider();
		bool active = !UniversalInputManager.Get().IsTouchMode() || PlatformSettings.OS == OSCategory.PC;
		if (scrollbar != null)
		{
			scrollbar.gameObject.SetActive(active);
		}
		if (BnetFriendMgr.Get().HasOnlineFriends() || BnetNearbyPlayerMgr.Get().HasNearbyStrangers())
		{
			CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded();
		}
		if (TemporaryAccountManager.IsTemporaryAccount())
		{
			items.GetComponent<BoxCollider>().enabled = false;
			temporaryAccountPaper.SetActive(value: true);
			temporaryAccountCover.SetActive(value: true);
			temporaryAccountDrawing.SetActive(value: true);
			temporaryAccountSignUpButton.AddEventListener(UIEventType.RELEASE, OnTemporaryAccountSignUpButtonPressed);
		}
	}

	private void Start()
	{
		UpdateMyself();
		InitItems();
		UpdateRAFState();
		UpdateFSGState();
		TelemetryManager.Client().SendFriendsListView(SceneMgr.Get().GetMode().ToString());
		me.m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(OnMyRankedMedalWidgetReady);
		IsStarted = true;
		if (this.OnStarted != null)
		{
			this.OnStarted();
		}
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
	}

	private void OnDestroy()
	{
		UnregisterFriendEvents();
		CloseAddFriendFrame();
		if (m_longListBehavior != null && m_longListBehavior.FreeList != null)
		{
			foreach (MobileFriendListItem free in m_longListBehavior.FreeList)
			{
				if (free != null)
				{
					UnityEngine.Object.Destroy(free.gameObject);
				}
			}
		}
		foreach (FriendListItemHeader value in m_headers.Values)
		{
			if (value != null)
			{
				UnityEngine.Object.Destroy(value.gameObject);
			}
		}
		if (FatalErrorMgr.Get() != null)
		{
			FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		}
	}

	private void Update()
	{
		HandleKeyboardInput();
		if (m_nearbyPlayersNeedUpdate && Time.realtimeSinceStartup >= m_lastNearbyPlayersUpdate + 10f)
		{
			HandleNearbyPlayersChanged();
		}
	}

	private void OnEnable()
	{
		if (m_nearbyPlayersNeedUpdate)
		{
			HandleNearbyPlayersChanged();
		}
		if (m_playersChangeList.GetChanges().Count > 0)
		{
			DoPlayersChanged(m_playersChangeList);
			m_playersChangeList.GetChanges().Clear();
		}
		if (items.IsInitialized)
		{
			ResumeItemsLayout();
		}
		UpdateMyself();
		items.ResetState();
		m_editMode = FriendListEditMode.NONE;
		m_friendToRemove = null;
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		m_longListBehavior.ReleaseAllItems();
		m_allItems.Clear();
	}

	public void SetItemsCameraEnabled(bool enable)
	{
		m_itemsCamera.gameObject.SetActive(enable);
	}

	public void SetWorldRect(float x, float y, float width, float height)
	{
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(value: true);
		window.SetEntireSize(width, height);
		Vector3 vector = TransformUtil.ComputeWorldPoint(TransformUtil.ComputeSetPointBounds(window), new Vector3(0f, 1f, 0f));
		Vector3 translation = new Vector3(x, y, vector.z) - vector;
		base.transform.Translate(translation);
		UpdateItemsList();
		UpdateItemsCamera();
		UpdateBackgroundCollider();
		UpdateDropShadow();
		base.gameObject.SetActive(activeSelf);
		if (temporaryAccountDrawingBone != null && TemporaryAccountManager.IsTemporaryAccount())
		{
			temporaryAccountDrawing.transform.position = temporaryAccountDrawingBone.transform.position;
		}
	}

	public void SetWorldPosition(float x, float y)
	{
		SetWorldPosition(new Vector3(x, y));
	}

	public void SetWorldPosition(Vector3 pos)
	{
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(value: true);
		base.transform.position = pos;
		UpdateItemsList();
		UpdateItemsCamera();
		UpdateBackgroundCollider();
		base.gameObject.SetActive(activeSelf);
		if (temporaryAccountDrawingBone != null && TemporaryAccountManager.IsTemporaryAccount())
		{
			temporaryAccountDrawing.transform.position = temporaryAccountDrawingBone.transform.position;
		}
	}

	public void SetWorldHeight(float height)
	{
		bool activeSelf = base.gameObject.activeSelf;
		base.gameObject.SetActive(value: true);
		window.SetEntireHeight(height);
		UpdateItemsList();
		UpdateItemsCamera();
		UpdateBackgroundCollider();
		UpdateDropShadow();
		base.gameObject.SetActive(activeSelf);
		if (temporaryAccountDrawingBone != null && TemporaryAccountManager.IsTemporaryAccount())
		{
			temporaryAccountDrawing.transform.position = temporaryAccountDrawingBone.transform.position;
		}
	}

	public void ShowAddFriendFrame(BnetPlayer player = null)
	{
		m_addFriendFrame = UnityEngine.Object.Instantiate(prefabs.addFriendFrame);
		m_addFriendFrame.Closed += CloseAddFriendFrame;
		if (player != null)
		{
			m_addFriendFrame.SetPlayer(player);
		}
	}

	public void CloseAddFriendFrame()
	{
		if (!(m_addFriendFrame == null))
		{
			m_addFriendFrame.Close();
			if (this.AddFriendFrameClosed != null)
			{
				this.AddFriendFrameClosed();
			}
			m_addFriendFrame = null;
		}
	}

	public void ShowRemoveFriendPopup(BnetPlayer friend)
	{
		m_friendToRemove = friend;
		if (m_friendToRemove != null)
		{
			string uniqueName = FriendUtils.GetUniqueName(m_friendToRemove);
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_text = GameStrings.Format("GLOBAL_FRIENDLIST_REMOVE_FRIEND_ALERT_MESSAGE", uniqueName);
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = OnRemoveFriendPopupResponse;
			AlertPopup.PopupInfo info = popupInfo;
			DialogManager.Get().ShowPopup(info, OnRemoveFriendDialogShown, m_friendToRemove);
			if (this.RemoveFriendPopupOpened != null)
			{
				this.RemoveFriendPopupOpened();
			}
		}
	}

	public void SelectFriend(BnetPlayer player)
	{
		foreach (FriendListFriendFrame renderedItem in GetRenderedItems<FriendListFriendFrame>())
		{
			Widget widget = renderedItem.GetWidget();
			if (widget != null)
			{
				if (renderedItem.GetFriend() == player)
				{
					widget.TriggerEvent("SHOW_HIGHLIGHT");
				}
				else
				{
					widget.TriggerEvent("HIDE_HIGHLIGHT");
				}
			}
		}
	}

	public void UpdateRAFButtonGlow()
	{
		bool @bool = Options.Get().GetBool(Option.HAS_SEEN_RAF);
		rafButtonButtonGlow.SetActive(!@bool && m_isRAFButtonEnabled);
		UpdateFlyoutButtonGlow();
	}

	public void UpdateFSGButtonGlow()
	{
		bool @bool = Options.Get().GetBool(Option.HAS_CLICKED_FIRESIDE_GATHERINGS_BUTTON);
		fsgButtonButtonGlow.SetActive(!@bool && m_isFSGButtonEnabled);
		UpdateFlyoutButtonGlow();
	}

	private void UpdateFlyoutButtonGlow()
	{
		flyoutButtonGlow.ChangeState((fsgButtonButtonGlow.activeSelf || rafButtonButtonGlow.activeSelf || IsFlyoutOpen) ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.NONE);
	}

	public OrientedBounds ComputeFrameWorldBounds()
	{
		return TransformUtil.ComputeOrientedWorldBounds(base.gameObject, new List<GameObject> { items.gameObject });
	}

	public void SetRAFButtonEnabled(bool enabled)
	{
		if (m_isRAFButtonEnabled != enabled)
		{
			m_isRAFButtonEnabled = enabled;
			rafButton.GetComponent<UIBHighlight>().EnableResponse = m_isRAFButtonEnabled;
			rafButtonEnabledVisual.SetActive(enabled);
			rafButtonDisabledVisual.SetActive(!enabled);
			if (m_isRAFButtonEnabled)
			{
				rafButton.AddEventListener(UIEventType.RELEASE, OnRAFButtonReleased);
			}
			else
			{
				rafButton.RemoveEventListener(UIEventType.RELEASE, OnRAFButtonReleased);
			}
			UpdateRAFButtonGlow();
		}
	}

	public void SetFSGButtonEnabled()
	{
		bool flag = !FiresideGatheringManager.Get().IsCheckedIn && FiresideGatheringManager.CanRequestNearbyFSG;
		if (m_isFSGButtonEnabled != flag)
		{
			m_isFSGButtonEnabled = flag;
			fsgButton.SetEnabled(m_isFSGButtonEnabled);
			SetupFSGButtonAndFixFrameLength(m_isFSGButtonEnabled, flyoutMiddleFrame.transform.localScale.y, flyoutMiddleShadow.transform.localScale.y);
			if (m_isFSGButtonEnabled)
			{
				fsgButton.AddEventListener(UIEventType.RELEASE, OnFSGButtonReleased);
			}
			else
			{
				fsgButton.RemoveEventListener(UIEventType.RELEASE, OnFSGButtonReleased);
			}
			UpdateFSGButtonGlow();
		}
	}

	private void SetupFSGButtonAndFixFrameLength(bool enabled, float middleFrameScaleY, float middleShadowScaleY)
	{
		if (enabled)
		{
			fsgButton.gameObject.SetActive(value: true);
			return;
		}
		fsgButton.gameObject.SetActive(value: false);
		middleFrameScaleY -= flyoutMiddleFrameScaleOffsetForFSG;
		middleShadowScaleY -= flyoutShadowScaleOffsetForFSG;
		flyoutMiddleFrame.transform.localScale = new Vector3(flyoutMiddleFrame.transform.localScale.x, middleFrameScaleY, flyoutMiddleFrame.transform.localScale.z);
		flyoutMiddleShadow.transform.localScale = new Vector3(flyoutMiddleShadow.transform.localScale.x, middleShadowScaleY, flyoutMiddleShadow.transform.localScale.z);
		flyoutFrameContainer.UpdateSlices();
		flyoutShadowContainer.UpdateSlices();
	}

	public void OpenFlyoutMenu()
	{
		if (!(flyoutMenu == null))
		{
			m_flyoutOpen = true;
			flyoutMenu.SetActive(value: true);
			UpdateFlyoutButtonGlow();
		}
	}

	public void CloseFlyoutMenu()
	{
		if (!(flyoutMenu == null))
		{
			m_flyoutOpen = false;
			flyoutMenu.SetActive(value: false);
			UpdateFlyoutButtonGlow();
		}
	}

	private void CreateItemsCamera()
	{
		GameObject gameObject = new GameObject("ItemsCamera");
		gameObject.transform.parent = items.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, -100f);
		m_itemsCamera = gameObject.AddComponent<Camera>();
		m_itemsCamera.orthographic = true;
		m_itemsCamera.depth = BnetBar.CameraDepth + 1;
		m_itemsCamera.clearFlags = CameraClearFlags.Depth;
		m_itemsCamera.cullingMask = GameLayer.BattleNetFriendList.LayerBit();
		m_itemsCamera.allowHDR = false;
		UpdateItemsCamera();
	}

	private void UpdateItemsList()
	{
		Transform bottomRightBone = GetBottomRightBone();
		items.transform.position = (listInfo.topLeft.position + bottomRightBone.position) / 2f;
		Vector3 vector = bottomRightBone.position - listInfo.topLeft.position;
		items.ClipSize = new UnityEngine.Vector2(vector.x, Math.Abs(vector.y));
		if (innerShadow != null)
		{
			innerShadow.transform.position = items.transform.position;
			Vector3 vector2 = GetBottomRightBone().position - listInfo.topLeft.position;
			TransformUtil.SetLocalScaleToWorldDimension(innerShadow, new WorldDimensionIndex(Mathf.Abs(vector2.x), 0), new WorldDimensionIndex(Mathf.Abs(vector2.y), 2));
		}
	}

	private void UpdateItemsCamera()
	{
		Camera bnetCamera = BaseUI.Get().GetBnetCamera();
		Transform bottomRightBone = GetBottomRightBone();
		Vector3 vector = bnetCamera.WorldToScreenPoint(listInfo.topLeft.position);
		Vector3 vector2 = bnetCamera.WorldToScreenPoint(bottomRightBone.position);
		GeneralUtils.Swap(ref vector.y, ref vector2.y);
		m_itemsCamera.pixelRect = new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
		m_itemsCamera.orthographicSize = m_itemsCamera.rect.height * bnetCamera.orthographicSize;
	}

	private void UpdateBackgroundCollider()
	{
		Renderer[] componentsInChildren = window.GetComponentsInChildren<Renderer>();
		Bounds bounds = new Bounds(base.transform.position, Vector3.zero);
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			if (renderer.bounds.size.x != 0f && renderer.bounds.size.y != 0f && renderer.bounds.size.z != 0f)
			{
				bounds.Encapsulate(renderer.bounds);
			}
		}
		Vector3 vector = base.transform.InverseTransformPoint(bounds.min);
		Vector3 vector2 = base.transform.InverseTransformPoint(bounds.max);
		BoxCollider boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
		{
			boxCollider = base.gameObject.AddComponent<BoxCollider>();
		}
		boxCollider.center = (vector + vector2) / 2f + Vector3.forward;
		boxCollider.size = vector2 - vector;
	}

	private void UpdateDropShadow()
	{
		if (!(outerShadow == null))
		{
			outerShadow.SetActive(!UniversalInputManager.Get().IsTouchMode());
		}
	}

	private void UpdateMyself()
	{
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		if (myPlayer != null && myPlayer.IsDisplayable())
		{
			BnetBattleTag battleTag = myPlayer.GetBattleTag();
			if (Options.Get().GetBool(Option.STREAMER_MODE))
			{
				me.nameText.Text = string.Empty;
			}
			else
			{
				me.nameText.Text = string.Format("<color=#{0}>{1}</color> <size=32><color=#{2}>#{3}</color></size>", "5ecaf0ff", battleTag.GetName(), "999999ff", battleTag.GetNumber().ToString());
			}
			if (RankMgr.Get().GetLocalPlayerMedalInfo().IsDisplayable())
			{
				myPortrait.gameObject.SetActive(value: false);
				if (portraitBackground != null)
				{
					portraitBackground.GetComponent<Renderer>().SetMaterial(rankedBackground);
				}
			}
			else
			{
				myPortrait.SetProgramId(BnetProgramId.HEARTHSTONE);
				myPortrait.gameObject.SetActive(value: true);
				if (portraitBackground != null)
				{
					portraitBackground.GetComponent<Renderer>().SetMaterial(unrankedBackground);
				}
			}
			UpdateMyRankedMedalWidget();
		}
		else
		{
			me.nameText.Text = string.Empty;
		}
	}

	private void InitItems()
	{
		BnetFriendMgr bnetFriendMgr = BnetFriendMgr.Get();
		BnetNearbyPlayerMgr bnetNearbyPlayerMgr = BnetNearbyPlayerMgr.Get();
		items.SelectionEnabled = true;
		items.SelectedIndexChanging += (int index) => index != -1;
		SuspendItemsLayout();
		UpdateCurrentFiresideGatherings();
		UpdateFoundFiresideGatherings();
		InitFiresideGatheringPlayers();
		UpdateRequests(bnetFriendMgr.GetReceivedInvites(), null);
		UpdateAllFriends(bnetFriendMgr.GetFriends(), null);
		UpdateAllNearbyPlayers(bnetNearbyPlayerMgr.GetNearbyStrangers(), null);
		UpdateAllNearbyPlayers(bnetNearbyPlayerMgr.GetNearbyFriends(), null);
		UpdateAllHeaders();
		ResumeItemsLayout();
		UpdateAllHeaderBackgrounds();
		UpdateSelectedItem();
		UpdateRAFButtonGlow();
		UpdateFSGButtonGlow();
	}

	public void UpdateItems()
	{
		foreach (FriendListRequestFrame renderedItem in GetRenderedItems<FriendListRequestFrame>())
		{
			renderedItem.UpdateInvite();
		}
		UpdateFriendItems();
	}

	public void UpdateFriendItems()
	{
		if (m_updateFriendItemsWhenAvailableCoroutine != null)
		{
			StopCoroutine(m_updateFriendItemsWhenAvailableCoroutine);
		}
		foreach (FriendListFriendFrame renderedItem in GetRenderedItems<FriendListFriendFrame>())
		{
			renderedItem.UpdateFriend();
		}
	}

	public void UpdateFriendItemsWhenAvailable()
	{
		if (m_updateFriendItemsWhenAvailableCoroutine != null)
		{
			StopCoroutine(m_updateFriendItemsWhenAvailableCoroutine);
		}
		m_updateFriendItemsWhenAvailableCoroutine = StartCoroutine(UpdateFriendItemsWhenAvailableCoroutine());
	}

	private IEnumerator UpdateFriendItemsWhenAvailableCoroutine()
	{
		while (!FriendChallengeMgr.Get().AmIAvailable())
		{
			yield return null;
		}
		m_updateFriendItemsWhenAvailableCoroutine = null;
		UpdateFriendItems();
	}

	private void UpdateCurrentFiresideGatherings()
	{
		for (int num = m_allItems.Count - 1; num >= 0; num--)
		{
			if (m_allItems[num].ItemMainType == MobileFriendListItem.TypeFlags.CurrentFiresideGathering)
			{
				m_allItems.RemoveAt(num);
			}
		}
		FSGConfig currentFSG = FiresideGatheringManager.Get().CurrentFSG;
		if (currentFSG != null)
		{
			FriendListItem itemToAdd = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.CurrentFiresideGathering, currentFSG);
			AddItem(itemToAdd);
		}
	}

	private void UpdateFoundFiresideGatherings()
	{
		for (int num = m_allItems.Count - 1; num >= 0; num--)
		{
			if (m_allItems[num].ItemMainType == MobileFriendListItem.TypeFlags.FoundFiresideGathering)
			{
				m_allItems.RemoveAt(num);
			}
		}
		foreach (FSGConfig fSG in FiresideGatheringManager.Get().GetFSGs())
		{
			FriendListItem itemToAdd = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.FoundFiresideGathering, fSG);
			AddItem(itemToAdd);
		}
	}

	private void InitFiresideGatheringPlayers()
	{
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		List<BnetPlayer> displayablePatronList = firesideGatheringManager.DisplayablePatronList;
		UpdateFiresideGatheringPlayers(displayablePatronList, null);
		if (firesideGatheringManager.CurrentFsgIsLargeScale)
		{
			string itemData = GameStrings.Format("GLOBAL_FIRESIDE_GATHERING_PATRON_LIST_FOOTER_TEXT_LARGE_SCALE", 99);
			FriendListItem itemToAdd = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.FiresideGatheringFooter, itemData);
			AddItem(itemToAdd);
		}
	}

	private void UpdateFiresideGatheringPlayers(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		if (FiresideGatheringManager.Get().DisplayablePatronCount >= FiresideGatheringManager.Get().FriendListPatronCountLimit)
		{
			m_patronStrangersHidden = true;
			if (removedList == null)
			{
				removedList = new List<BnetPlayer>();
			}
			List<BnetPlayer> list = new List<BnetPlayer>();
			foreach (BnetPlayer displayablePatron in FiresideGatheringManager.Get().DisplayablePatronList)
			{
				if (!BnetFriendMgr.Get().IsFriend(displayablePatron))
				{
					list.Add(displayablePatron);
				}
			}
			removedList.AddRange(list);
			addedList?.RemoveAll(list.Contains);
			if (!m_allItems.Exists((FriendListItem item) => item.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter))
			{
				int num = Mathf.Clamp(FiresideGatheringManager.Get().DisplayablePatronCount, FiresideGatheringManager.Get().FriendListPatronCountLimit, 99);
				string itemData = ((FiresideGatheringManager.Get().DisplayablePatronCount > 99) ? GameStrings.Format("GLOBAL_FIRESIDE_GATHERING_PATRON_LIST_FOOTER_TEXT_LARGE_SCALE", 99) : GameStrings.Format("GLOBAL_FIRESIDE_GATHERING_PATRON_LIST_FOOTER_TEXT_SOFT_LIMIT", num));
				FriendListItem itemToAdd = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.FiresideGatheringFooter, itemData);
				AddItem(itemToAdd);
			}
		}
		else if (m_patronStrangersHidden)
		{
			m_patronStrangersHidden = false;
			RemoveItem(isHeader: false, MobileFriendListItem.TypeFlags.FiresideGatheringFooter, string.Empty);
			if (addedList == null)
			{
				addedList = new List<BnetPlayer>();
			}
			List<BnetPlayer> list2 = new List<BnetPlayer>();
			foreach (BnetPlayer displayablePatron2 in FiresideGatheringManager.Get().DisplayablePatronList)
			{
				if (!BnetFriendMgr.Get().IsFriend(displayablePatron2) && !addedList.Contains(displayablePatron2))
				{
					list2.Add(displayablePatron2);
				}
			}
			addedList.AddRange(list2);
		}
		if (removedList != null)
		{
			foreach (BnetPlayer removed in removedList)
			{
				RemoveItem(isHeader: false, MobileFriendListItem.TypeFlags.FiresideGatheringPlayer, removed);
			}
		}
		if (addedList != null)
		{
			foreach (BnetPlayer added in addedList)
			{
				FriendListItem itemToAdd2 = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.FiresideGatheringPlayer, added);
				AddItem(itemToAdd2);
			}
		}
		UpdateFriendItems();
	}

	private void OnFiresideGatheringPresencePatronsUpdated(List<BnetPlayer> addedPatrons, List<BnetPlayer> removedPatrons)
	{
		UpdateFiresideGatheringPlayers(addedPatrons, removedPatrons);
		BnetFriendChangelist bnetFriendChangelist = null;
		bool flag = false;
		if (addedPatrons != null)
		{
			foreach (BnetPlayer addedPatron in addedPatrons)
			{
				flag = true;
				if (BnetFriendMgr.Get().IsFriend(addedPatron))
				{
					if (bnetFriendChangelist == null)
					{
						bnetFriendChangelist = new BnetFriendChangelist();
					}
					bnetFriendChangelist.AddRemovedFriend(addedPatron);
				}
			}
		}
		if (removedPatrons != null)
		{
			foreach (BnetPlayer removedPatron in removedPatrons)
			{
				flag = true;
				if (BnetFriendMgr.Get().IsFriend(removedPatron))
				{
					if (bnetFriendChangelist == null)
					{
						bnetFriendChangelist = new BnetFriendChangelist();
					}
					bnetFriendChangelist.AddAddedFriend(removedPatron);
				}
			}
		}
		if (bnetFriendChangelist != null)
		{
			OnFriendsChanged(bnetFriendChangelist, null);
		}
		else if (flag)
		{
			SortAndRefreshTouchList();
		}
	}

	private void RemoveAllFiresideGatheringPlayers()
	{
		m_allItems.RemoveAll((FriendListItem item) => item.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringPlayer || item.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter);
	}

	private void UpdateRequests(List<BnetInvitation> addedList, List<BnetInvitation> removedList)
	{
		if (removedList == null && addedList == null)
		{
			return;
		}
		if (removedList != null)
		{
			foreach (BnetInvitation removed in removedList)
			{
				RemoveItem(isHeader: false, MobileFriendListItem.TypeFlags.Request, removed);
			}
		}
		foreach (FriendListRequestFrame renderedItem in GetRenderedItems<FriendListRequestFrame>())
		{
			renderedItem.UpdateInvite();
		}
		if (addedList == null)
		{
			return;
		}
		foreach (BnetInvitation added in addedList)
		{
			FriendListItem itemToAdd = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.Request, added);
			AddItem(itemToAdd);
		}
	}

	private void UpdateAllFriends(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		if (removedList == null && addedList == null)
		{
			return;
		}
		if (removedList != null)
		{
			foreach (BnetPlayer removed in removedList)
			{
				RemoveItem(isHeader: false, MobileFriendListItem.TypeFlags.Friend, removed);
			}
		}
		UpdateFriendItems();
		if (addedList != null)
		{
			foreach (BnetPlayer added in addedList)
			{
				if (!FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(added) && !BnetNearbyPlayerMgr.Get().IsNearbyPlayer(added))
				{
					added.GetPersistentGameId();
					FriendListItem itemToAdd = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.Friend, added);
					AddItem(itemToAdd);
				}
			}
		}
		SortAndRefreshTouchList();
	}

	private void UpdateAllNearbyPlayers(List<BnetPlayer> addedList, List<BnetPlayer> removedList)
	{
		if (removedList != null)
		{
			foreach (BnetPlayer removed in removedList)
			{
				RemoveItem(isHeader: false, MobileFriendListItem.TypeFlags.NearbyPlayer, removed);
			}
		}
		UpdateFriendItems();
		if (addedList != null)
		{
			foreach (BnetPlayer added in addedList)
			{
				FriendListItem itemToAdd = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.NearbyPlayer, added);
				AddItem(itemToAdd);
			}
		}
		SortAndRefreshTouchList();
	}

	private FriendListFriendFrame FindRenderedBaseFriendFrame(BnetPlayer friend)
	{
		return FindFirstRenderedItem((FriendListFriendFrame frame) => frame.GetFriend() == friend);
	}

	private void UpdateFriendFrame(BnetPlayer friend)
	{
		FriendListFriendFrame friendListFriendFrame = FindRenderedBaseFriendFrame(friend);
		if (friendListFriendFrame != null)
		{
			friendListFriendFrame.UpdateFriend();
		}
	}

	private MobileFriendListItem CreateFriendFrame(BnetPlayer friend)
	{
		FriendListFriendFrame friendListFriendFrame = UnityEngine.Object.Instantiate(prefabs.friendItem);
		UberText[] objs = UberText.EnableAllTextInObject(friendListFriendFrame.gameObject, enable: false);
		friendListFriendFrame.Initialize(friend);
		MobileFriendListItem result = FinishCreateVisualItem(friendListFriendFrame, MobileFriendListItem.TypeFlags.Friend, FindHeader(MobileFriendListItem.TypeFlags.Friend), friendListFriendFrame.gameObject);
		UberText.EnableAllTextObjects(objs, enable: true);
		return result;
	}

	private MobileFriendListItem CreateNearbyPlayerFrame(BnetPlayer friend)
	{
		FriendListFriendFrame friendListFriendFrame = UnityEngine.Object.Instantiate(prefabs.friendItem);
		UberText[] objs = UberText.EnableAllTextInObject(friendListFriendFrame.gameObject, enable: false);
		friendListFriendFrame.Initialize(friend);
		MobileFriendListItem result = FinishCreateVisualItem(friendListFriendFrame, MobileFriendListItem.TypeFlags.NearbyPlayer, FindHeader(MobileFriendListItem.TypeFlags.NearbyPlayer), friendListFriendFrame.gameObject);
		UberText.EnableAllTextObjects(objs, enable: true);
		return result;
	}

	private MobileFriendListItem CreateRequestFrame(BnetInvitation invite)
	{
		FriendListRequestFrame friendListRequestFrame = UnityEngine.Object.Instantiate(prefabs.requestItem);
		UberText[] objs = UberText.EnableAllTextInObject(friendListRequestFrame.gameObject, enable: false);
		friendListRequestFrame.SetInvite(invite);
		MobileFriendListItem result = FinishCreateVisualItem(friendListRequestFrame, MobileFriendListItem.TypeFlags.Request, FindHeader(MobileFriendListItem.TypeFlags.Request), friendListRequestFrame.gameObject);
		UberText.EnableAllTextObjects(objs, enable: true);
		return result;
	}

	private MobileFriendListItem CreateFSGFrame(FSGConfig fsgConfig, MobileFriendListItem.TypeFlags flag)
	{
		FriendListFSGFrame friendListFSGFrame = UnityEngine.Object.Instantiate(prefabs.fsgItem);
		friendListFSGFrame.InitFrame(fsgConfig);
		return FinishCreateVisualItem(friendListFSGFrame, flag, null, friendListFSGFrame.gameObject);
	}

	private MobileFriendListItem CreateFSGPlayerFrame(BnetPlayer friend)
	{
		FriendListFriendFrame friendListFriendFrame = UnityEngine.Object.Instantiate(prefabs.friendItem);
		UberText[] objs = UberText.EnableAllTextInObject(friendListFriendFrame.gameObject, enable: false);
		friendListFriendFrame.Initialize(friend, isFSGPatron: true);
		MobileFriendListItem result = FinishCreateVisualItem(friendListFriendFrame, MobileFriendListItem.TypeFlags.FiresideGatheringPlayer, null, friendListFriendFrame.gameObject);
		UberText.EnableAllTextObjects(objs, enable: true);
		return result;
	}

	private MobileFriendListItem CreateFSGFooter(string text)
	{
		FriendListItemFooter friendListItemFooter = UnityEngine.Object.Instantiate(prefabs.footerItem);
		friendListItemFooter.Text = text;
		return FinishCreateVisualItem(friendListItemFooter, MobileFriendListItem.TypeFlags.FiresideGatheringFooter, null, friendListItemFooter.gameObject);
	}

	private void UpdateAllHeaders()
	{
		UpdateRequestsHeader();
		UpdateNearbyPlayersHeader();
		UpdateFriendsHeader();
	}

	private void UpdateAllHeaderBackgrounds()
	{
		UpdateHeaderBackground(FindHeader(MobileFriendListItem.TypeFlags.Request));
	}

	private void UpdateRequestsHeader(FriendListItemHeader header = null)
	{
		int num = 0;
		foreach (FriendListItem allItem in m_allItems)
		{
			if (allItem.ItemMainType == MobileFriendListItem.TypeFlags.Request)
			{
				num++;
			}
		}
		if (num > 0)
		{
			string text = GameStrings.Format("GLOBAL_FRIENDLIST_REQUESTS_HEADER", num);
			if (header == null)
			{
				header = FindOrAddHeader(MobileFriendListItem.TypeFlags.Request);
				if (!DoesHeaderExist(MobileFriendListItem.TypeFlags.Request))
				{
					FriendListItem itemToAdd = new FriendListItem(isHeader: true, MobileFriendListItem.TypeFlags.Request, null);
					AddItem(itemToAdd);
				}
			}
			header.SetText(text);
		}
		else if (header == null)
		{
			RemoveItem(isHeader: true, MobileFriendListItem.TypeFlags.Request, null);
		}
	}

	private void UpdateNearbyPlayersHeader(FriendListItemHeader header = null)
	{
		int num = 0;
		foreach (FriendListItem allItem in m_allItems)
		{
			if (allItem.ItemMainType == MobileFriendListItem.TypeFlags.NearbyPlayer && !FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(allItem.GetNearbyPlayer()))
			{
				num++;
			}
		}
		string text = GameStrings.Format("GLOBAL_FRIENDLIST_NEARBY_PLAYERS_HEADER", num);
		if (header == null)
		{
			header = FindOrAddHeader(MobileFriendListItem.TypeFlags.NearbyPlayer);
			if (!DoesHeaderExist(MobileFriendListItem.TypeFlags.NearbyPlayer))
			{
				FriendListItem itemToAdd = new FriendListItem(isHeader: true, MobileFriendListItem.TypeFlags.NearbyPlayer, null);
				AddItem(itemToAdd);
			}
		}
		FriendListNearbyPlayersHeader friendListNearbyPlayersHeader = header as FriendListNearbyPlayersHeader;
		if (friendListNearbyPlayersHeader != null)
		{
			friendListNearbyPlayersHeader.SetText(num);
		}
		else
		{
			header.SetText(text);
			Debug.LogError("FriendListFrame: Could not cast header to type FriendListNearbyPlayersHeader.");
		}
		if (header != null)
		{
			header.SetToggleEnabled(enabled: false);
		}
	}

	private List<FriendListItem> GetFriendItems()
	{
		List<FriendListItem> list = new List<FriendListItem>();
		foreach (FriendListItem allItem in m_allItems)
		{
			if (allItem.ItemMainType == MobileFriendListItem.TypeFlags.Friend)
			{
				list.Add(allItem);
			}
		}
		return list;
	}

	private void UpdateFriendsHeader(FriendListItemHeader header = null)
	{
		List<FriendListItem> friendItems = GetFriendItems();
		int num = 0;
		foreach (FriendListItem item in friendItems)
		{
			BnetPlayer friend = item.GetFriend();
			if (friend.IsOnline() && !BnetNearbyPlayerMgr.Get().IsNearbyPlayer(friend) && !FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(friend))
			{
				num++;
			}
		}
		int count = friendItems.Count;
		string text = null;
		text = ((num != count) ? GameStrings.Format("GLOBAL_FRIENDLIST_FRIENDS_HEADER", num, count) : GameStrings.Format("GLOBAL_FRIENDLIST_FRIENDS_HEADER_ALL_ONLINE", num));
		if (header == null)
		{
			header = FindOrAddHeader(MobileFriendListItem.TypeFlags.Friend);
			if (!DoesHeaderExist(MobileFriendListItem.TypeFlags.Friend))
			{
				FriendListItem itemToAdd = new FriendListItem(isHeader: true, MobileFriendListItem.TypeFlags.Friend, null);
				AddItem(itemToAdd);
			}
		}
		header.SetText(text);
		header.SetToggleEnabled(enabled: false);
	}

	private void UpdateHeaderBackground(FriendListItemHeader itemHeader)
	{
		if (itemHeader == null)
		{
			return;
		}
		MobileFriendListItem component = itemHeader.GetComponent<MobileFriendListItem>();
		if (component == null)
		{
			return;
		}
		TiledBackground tiledBackground = null;
		if (itemHeader.Background == null)
		{
			GameObject gameObject = new GameObject("ItemsBackground");
			gameObject.transform.parent = component.transform;
			TransformUtil.Identity(gameObject);
			gameObject.layer = 24;
			HeaderBackgroundInfo headerBackgroundInfo = (((component.Type & MobileFriendListItem.TypeFlags.Request) != 0) ? listInfo.requestBackgroundInfo : listInfo.currentGameBackgroundInfo);
			gameObject.AddComponent<MeshFilter>().mesh = headerBackgroundInfo.mesh;
			gameObject.AddComponent<MeshRenderer>().SetMaterial(headerBackgroundInfo.material);
			tiledBackground = gameObject.AddComponent<TiledBackground>();
			itemHeader.Background = gameObject;
		}
		else
		{
			tiledBackground = itemHeader.Background.GetComponent<TiledBackground>();
		}
		tiledBackground.transform.parent = null;
		MobileFriendListItem.TypeFlags typeFlags = component.Type ^ MobileFriendListItem.TypeFlags.Header;
		Bounds bounds = new Bounds(component.transform.position, Vector3.zero);
		foreach (ITouchListItem renderedItem in items.RenderedItems)
		{
			MobileFriendListItem mobileFriendListItem = renderedItem as MobileFriendListItem;
			if (mobileFriendListItem != null && (mobileFriendListItem.Type & typeFlags) != 0)
			{
				bounds.Encapsulate(mobileFriendListItem.ComputeWorldBounds());
			}
		}
		tiledBackground.transform.parent = component.transform.parent.transform;
		bounds.center = tiledBackground.transform.parent.transform.InverseTransformPoint(bounds.center);
		tiledBackground.SetBounds(bounds);
		TransformUtil.SetPosZ(tiledBackground.transform, 2f);
		tiledBackground.gameObject.SetActive(itemHeader.IsShowingContents);
	}

	private FriendListItemHeader FindHeader(MobileFriendListItem.TypeFlags type)
	{
		type |= MobileFriendListItem.TypeFlags.Header;
		m_headers.TryGetValue(type, out var value);
		return value;
	}

	private bool DoesHeaderExist(MobileFriendListItem.TypeFlags type)
	{
		foreach (FriendListItem allItem in m_allItems)
		{
			if (allItem.IsHeader && allItem.SubType == type)
			{
				return true;
			}
		}
		return false;
	}

	private FriendListItemHeader FindOrAddHeader(MobileFriendListItem.TypeFlags type)
	{
		type |= MobileFriendListItem.TypeFlags.Header;
		FriendListItemHeader friendListItemHeader = FindHeader(type);
		if (friendListItemHeader == null)
		{
			FriendListItem friendListItem = new FriendListItem(isHeader: true, type, null);
			if (type == (MobileFriendListItem.TypeFlags.NearbyPlayer | MobileFriendListItem.TypeFlags.Header))
			{
				friendListItemHeader = UnityEngine.Object.Instantiate(prefabs.nearbyPlayersHeaderItem);
				((FriendListNearbyPlayersHeader)friendListItemHeader).OnPanelOpened += CloseFlyoutMenu;
			}
			else
			{
				friendListItemHeader = UnityEngine.Object.Instantiate(prefabs.headerItem);
			}
			m_headers[type] = friendListItemHeader;
			Option option = Option.FRIENDS_LIST_FRIEND_SECTION_HIDE;
			switch (friendListItem.SubType)
			{
			case MobileFriendListItem.TypeFlags.Request:
				option = Option.FRIENDS_LIST_REQUEST_SECTION_HIDE;
				break;
			case MobileFriendListItem.TypeFlags.NearbyPlayer:
				option = Option.FRIENDS_LIST_NEARBYPLAYER_SECTION_HIDE;
				break;
			case MobileFriendListItem.TypeFlags.Friend:
			case MobileFriendListItem.TypeFlags.CurrentGame:
				option = Option.FRIENDS_LIST_FRIEND_SECTION_HIDE;
				break;
			}
			friendListItemHeader.SubType = friendListItem.SubType;
			friendListItemHeader.Option = option;
			bool showHeaderSection = GetShowHeaderSection(option);
			friendListItemHeader.SetInitialShowContents(showHeaderSection);
			friendListItemHeader.ClearToggleListeners();
			friendListItemHeader.AddToggleListener(OnHeaderSectionToggle, friendListItemHeader);
			UberText[] objs = UberText.EnableAllTextInObject(friendListItemHeader.gameObject, enable: false);
			FinishCreateVisualItem(friendListItemHeader, type, null, null);
			UberText.EnableAllTextObjects(objs, enable: true);
		}
		return friendListItemHeader;
	}

	private void OnHeaderSectionToggle(bool show, object userdata)
	{
		FriendListItemHeader header = (FriendListItemHeader)userdata;
		SetShowHeaderSection(header.Option, show);
		int startingLongListIndex = m_allItems.FindIndex((FriendListItem item) => item.IsHeader && item.SubType == header.SubType);
		items.RefreshList(startingLongListIndex, preserveScrolling: true);
		UpdateHeaderBackground(header);
	}

	public T FindFirstRenderedItem<T>(Predicate<T> predicate = null) where T : MonoBehaviour
	{
		foreach (ITouchListItem renderedItem in items.RenderedItems)
		{
			T component = renderedItem.GetComponent<T>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null && (predicate == null || predicate(component)))
			{
				return component;
			}
		}
		return null;
	}

	private List<T> GetRenderedItems<T>() where T : MonoBehaviour
	{
		List<T> list = new List<T>();
		foreach (ITouchListItem renderedItem in items.RenderedItems)
		{
			T component = renderedItem.GetComponent<T>();
			if ((UnityEngine.Object)component != (UnityEngine.Object)null)
			{
				list.Add(component);
			}
		}
		return list;
	}

	private MobileFriendListItem FinishCreateVisualItem<T>(T obj, MobileFriendListItem.TypeFlags type, ITouchListItem parent, GameObject showObj) where T : MonoBehaviour
	{
		MobileFriendListItem mobileFriendListItem = obj.gameObject.GetComponent<MobileFriendListItem>();
		if (mobileFriendListItem == null)
		{
			mobileFriendListItem = obj.gameObject.AddComponent<MobileFriendListItem>();
			if (obj is FriendListFriendFrame)
			{
				((FriendListFriendFrame)(object)obj).InitializeMobileFriendListItem(mobileFriendListItem);
			}
			BoxCollider component = mobileFriendListItem.GetComponent<BoxCollider>();
			if (component != null)
			{
				component.size = new Vector3(component.size.x, component.size.y + items.elementSpacing, component.size.z);
			}
		}
		mobileFriendListItem.Type = type;
		mobileFriendListItem.SetShowObject(showObj);
		mobileFriendListItem.SetParent(parent);
		if (mobileFriendListItem.Selectable)
		{
			BnetPlayer selectedFriend = FriendMgr.Get().GetSelectedFriend();
			if (selectedFriend != null)
			{
				BnetPlayer bnetPlayer = null;
				if (obj is FriendListFriendFrame)
				{
					bnetPlayer = ((FriendListFriendFrame)(object)obj).GetFriend();
				}
				if (bnetPlayer != null && selectedFriend == bnetPlayer)
				{
					mobileFriendListItem.Selected();
				}
			}
		}
		return mobileFriendListItem;
	}

	private bool RemoveItem(bool isHeader, MobileFriendListItem.TypeFlags type, object itemToRemove)
	{
		int num = m_allItems.FindIndex(delegate(FriendListItem item)
		{
			if (item.IsHeader != isHeader || item.SubType != type)
			{
				return false;
			}
			if (itemToRemove == null)
			{
				return true;
			}
			switch (type)
			{
			case MobileFriendListItem.TypeFlags.Request:
				return item.GetInvite() == (BnetInvitation)itemToRemove;
			case MobileFriendListItem.TypeFlags.NearbyPlayer:
				return item.GetNearbyPlayer() == (BnetPlayer)itemToRemove;
			case MobileFriendListItem.TypeFlags.Friend:
			case MobileFriendListItem.TypeFlags.CurrentGame:
				return item.GetFriend() == (BnetPlayer)itemToRemove;
			case MobileFriendListItem.TypeFlags.CurrentFiresideGathering:
			case MobileFriendListItem.TypeFlags.FoundFiresideGathering:
				return item.GetFSGConfig() == (FSGConfig)itemToRemove;
			case MobileFriendListItem.TypeFlags.FiresideGatheringPlayer:
				return item.GetFiresideGatheringPlayer() == (BnetPlayer)itemToRemove;
			case MobileFriendListItem.TypeFlags.FiresideGatheringFooter:
				return item.ItemMainType == MobileFriendListItem.TypeFlags.FiresideGatheringFooter;
			default:
				return false;
			}
		});
		if (num < 0)
		{
			return false;
		}
		m_allItems.RemoveAt(num);
		return true;
	}

	private void AddItem(FriendListItem itemToAdd)
	{
		m_allItems.Add(itemToAdd);
	}

	private void SuspendItemsLayout()
	{
		items.SuspendLayout();
	}

	private void ResumeItemsLayout()
	{
		items.ResumeLayout(repositionItems: false);
		SortAndRefreshTouchList();
	}

	public void ToggleRemoveFriendsMode()
	{
		FriendListEditMode editFriendsMode;
		switch (m_editMode)
		{
		case FriendListEditMode.NONE:
			editFriendsMode = FriendListEditMode.REMOVE_FRIENDS;
			break;
		case FriendListEditMode.REMOVE_FRIENDS:
			editFriendsMode = FriendListEditMode.NONE;
			break;
		default:
			Log.All.PrintError("FriendListFrame: Should not be toggling Remove Friends mode when in mode {0}!", m_editMode);
			return;
		}
		SetEditFriendsMode(editFriendsMode);
		removeFriendButtonDisabledVisual.SetActive(m_editMode == FriendListEditMode.REMOVE_FRIENDS);
		removeFriendButtonEnabledVisual.SetActive(m_editMode == FriendListEditMode.NONE);
		removeFriendButtonButtonGlow.SetActive(m_editMode == FriendListEditMode.REMOVE_FRIENDS);
	}

	private bool SetEditFriendsMode(FriendListEditMode mode)
	{
		m_editMode = mode;
		SortAndRefreshTouchList();
		UpdateFriendItems();
		return true;
	}

	public void ExitRemoveFriendsMode()
	{
		if (m_editMode == FriendListEditMode.REMOVE_FRIENDS)
		{
			ToggleRemoveFriendsMode();
		}
	}

	private void SortAndRefreshTouchList()
	{
		if (!items.IsLayoutSuspended)
		{
			m_allItems.Sort(ItemsSortCompare);
			if (m_longListBehavior == null)
			{
				m_longListBehavior = new VirtualizedFriendsListBehavior(this);
				items.LongListBehavior = m_longListBehavior;
			}
			else
			{
				items.RefreshList(0, preserveScrolling: true);
			}
		}
	}

	private int ItemsSortCompare(FriendListItem item1, FriendListItem item2)
	{
		int num = item2.ItemFlags.CompareTo(item1.ItemFlags);
		if (num != 0)
		{
			return num;
		}
		switch (item1.ItemFlags)
		{
		case MobileFriendListItem.TypeFlags.Friend:
		case MobileFriendListItem.TypeFlags.CurrentGame:
			return FriendUtils.FriendSortCompare(item1.GetFriend(), item2.GetFriend());
		case MobileFriendListItem.TypeFlags.NearbyPlayer:
			return FriendUtils.FriendSortCompare(item1.GetNearbyPlayer(), item2.GetNearbyPlayer());
		case MobileFriendListItem.TypeFlags.Request:
		{
			BnetInvitation invite = item1.GetInvite();
			BnetInvitation invite2 = item2.GetInvite();
			int num2 = string.Compare(invite.GetInviterName(), invite2.GetInviterName(), ignoreCase: true);
			if (num2 != 0)
			{
				return num2;
			}
			ulong lo = invite.GetInviterId().GetLo();
			long lo2 = (long)invite2.GetInviterId().GetLo();
			return (int)((long)lo - lo2);
		}
		case MobileFriendListItem.TypeFlags.FoundFiresideGathering:
		{
			FSGConfig fSGConfig = item1.GetFSGConfig();
			FSGConfig fSGConfig2 = item2.GetFSGConfig();
			return FiresideGatheringManager.Get().FiresideGatheringSort(fSGConfig, fSGConfig2);
		}
		case MobileFriendListItem.TypeFlags.FiresideGatheringPlayer:
			return FiresideGatheringManager.Get().FiresideGatheringPlayerSort(item1.GetFiresideGatheringPlayer(), item2.GetFiresideGatheringPlayer());
		default:
			return 0;
		}
	}

	private void RegisterFriendEvents()
	{
		BnetFriendMgr.Get().AddChangeListener(OnFriendsChanged);
		BnetPresenceMgr.Get().AddPlayersChangedListener(OnPlayersChanged);
		FriendChallengeMgr.Get().AddChangedListener(OnFriendChallengeChanged);
		BnetNearbyPlayerMgr.Get().AddChangeListener(OnNearbyPlayersChanged);
		SceneMgr.Get().RegisterScenePreUnloadEvent(OnScenePreUnload);
		SpectatorManager.Get().OnInviteReceived += SpectatorManager_OnInviteReceivedOrSent;
		SpectatorManager.Get().OnInviteSent += SpectatorManager_OnInviteReceivedOrSent;
		Network.Get().RegisterNetHandler(RequestNearbyFSGsResponse.PacketID.ID, OnRequestNearbyFSGsResponse);
		FiresideGatheringManager.Get().OnJoinFSG += OnJoinFSG;
		FiresideGatheringManager.Get().OnLeaveFSG += OnLeaveFSG;
		FiresideGatheringManager.OnPatronListUpdated += OnFiresideGatheringPresencePatronsUpdated;
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheFeatures), UpdateFSGState);
		NetCache.Get().RegisterUpdatedListener(typeof(FSGFeatureConfig), UpdateFSGState);
	}

	private void UnregisterFriendEvents()
	{
		BnetFriendMgr.RemoveChangeListenerFromInstance(OnFriendsChanged);
		BnetPresenceMgr.RemovePlayersChangedListenerFromInstance(OnPlayersChanged);
		FriendChallengeMgr.RemoveChangedListenerFromInstance(OnFriendChallengeChanged);
		BnetNearbyPlayerMgr.RemoveChangeListenerFromInstance(OnNearbyPlayersChanged);
		SceneMgr.Get()?.UnregisterScenePreUnloadEvent(OnScenePreUnload);
		if (SpectatorManager.InstanceExists())
		{
			SpectatorManager spectatorManager = SpectatorManager.Get();
			spectatorManager.OnInviteReceived -= SpectatorManager_OnInviteReceivedOrSent;
			spectatorManager.OnInviteSent -= SpectatorManager_OnInviteReceivedOrSent;
		}
		Network.Get()?.RemoveNetHandler(RequestNearbyFSGsResponse.PacketID.ID, OnRequestNearbyFSGsResponse);
		FiresideGatheringManager firesideGatheringManager = FiresideGatheringManager.Get();
		if (firesideGatheringManager != null)
		{
			firesideGatheringManager.OnJoinFSG -= OnJoinFSG;
			firesideGatheringManager.OnLeaveFSG -= OnLeaveFSG;
			FiresideGatheringManager.OnPatronListUpdated -= OnFiresideGatheringPresencePatronsUpdated;
		}
		NetCache.Get()?.RemoveUpdatedListener(typeof(NetCache.NetCacheFeatures), UpdateFSGState);
		NetCache.Get()?.RemoveUpdatedListener(typeof(FSGFeatureConfig), UpdateFSGState);
	}

	private void OnFriendsChanged(BnetFriendChangelist changelist, object userData)
	{
		SuspendItemsLayout();
		UpdateRequests(changelist.GetAddedReceivedInvites(), changelist.GetRemovedReceivedInvites());
		UpdateAllFriends(changelist.GetAddedFriends(), changelist.GetRemovedFriends());
		UpdateAllHeaders();
		ResumeItemsLayout();
		UpdateAllHeaderBackgrounds();
		UpdateSelectedItem();
	}

	private void OnNearbyPlayersChanged(BnetNearbyPlayerChangelist changelist, object userData)
	{
		m_nearbyPlayersNeedUpdate = true;
		if (changelist.GetAddedStrangers() != null)
		{
			foreach (BnetPlayer addedStranger in changelist.GetAddedStrangers())
			{
				NearbyPlayerUpdate item = new NearbyPlayerUpdate(NearbyPlayerUpdate.ChangeType.Added, addedStranger);
				m_nearbyPlayerUpdates.Remove(item);
				m_nearbyPlayerUpdates.Add(item);
			}
		}
		if (changelist.GetRemovedStrangers() != null)
		{
			foreach (BnetPlayer removedStranger in changelist.GetRemovedStrangers())
			{
				NearbyPlayerUpdate item2 = new NearbyPlayerUpdate(NearbyPlayerUpdate.ChangeType.Removed, removedStranger);
				m_nearbyPlayerUpdates.Remove(item2);
				m_nearbyPlayerUpdates.Add(item2);
			}
		}
		if (changelist.GetAddedFriends() != null)
		{
			foreach (BnetPlayer addedFriend in changelist.GetAddedFriends())
			{
				NearbyPlayerUpdate item3 = new NearbyPlayerUpdate(NearbyPlayerUpdate.ChangeType.Added, addedFriend);
				m_nearbyPlayerUpdates.Remove(item3);
				m_nearbyPlayerUpdates.Add(item3);
			}
		}
		if (changelist.GetRemovedFriends() != null)
		{
			foreach (BnetPlayer removedFriend in changelist.GetRemovedFriends())
			{
				NearbyPlayerUpdate item4 = new NearbyPlayerUpdate(NearbyPlayerUpdate.ChangeType.Removed, removedFriend);
				m_nearbyPlayerUpdates.Remove(item4);
				m_nearbyPlayerUpdates.Add(item4);
			}
		}
		if (changelist.GetAddedPlayers() != null)
		{
			foreach (BnetPlayer addedPlayer in changelist.GetAddedPlayers())
			{
				NearbyPlayerUpdate item5 = new NearbyPlayerUpdate(NearbyPlayerUpdate.ChangeType.Added, addedPlayer);
				m_nearbyPlayerUpdates.Remove(item5);
				m_nearbyPlayerUpdates.Add(item5);
			}
		}
		if (changelist.GetRemovedPlayers() != null)
		{
			foreach (BnetPlayer removedPlayer in changelist.GetRemovedPlayers())
			{
				NearbyPlayerUpdate item6 = new NearbyPlayerUpdate(NearbyPlayerUpdate.ChangeType.Removed, removedPlayer);
				m_nearbyPlayerUpdates.Remove(item6);
				m_nearbyPlayerUpdates.Add(item6);
			}
		}
		if (base.gameObject.activeInHierarchy && Time.realtimeSinceStartup >= m_lastNearbyPlayersUpdate + 10f)
		{
			HandleNearbyPlayersChanged();
		}
	}

	private void HandleNearbyPlayersChanged()
	{
		if (!m_nearbyPlayersNeedUpdate)
		{
			return;
		}
		UpdateFriendItems();
		BnetFriendChangelist bnetFriendChangelist = null;
		if (m_nearbyPlayerUpdates.Count > 0)
		{
			SuspendItemsLayout();
			foreach (NearbyPlayerUpdate nearbyPlayerUpdate in m_nearbyPlayerUpdates)
			{
				if (nearbyPlayerUpdate.Change == NearbyPlayerUpdate.ChangeType.Added)
				{
					if (FiresideGatheringManager.Get().IsPlayerInMyFSGAndDisplayable(nearbyPlayerUpdate.Player))
					{
						continue;
					}
					FriendListItem itemToAdd = new FriendListItem(isHeader: false, MobileFriendListItem.TypeFlags.NearbyPlayer, nearbyPlayerUpdate.Player);
					AddItem(itemToAdd);
					if (BnetFriendMgr.Get().IsFriend(nearbyPlayerUpdate.Player))
					{
						if (bnetFriendChangelist == null)
						{
							bnetFriendChangelist = new BnetFriendChangelist();
						}
						bnetFriendChangelist.AddRemovedFriend(nearbyPlayerUpdate.Player);
					}
					continue;
				}
				RemoveItem(isHeader: false, MobileFriendListItem.TypeFlags.NearbyPlayer, nearbyPlayerUpdate.Player);
				if (BnetFriendMgr.Get().IsFriend(nearbyPlayerUpdate.Player))
				{
					if (bnetFriendChangelist == null)
					{
						bnetFriendChangelist = new BnetFriendChangelist();
					}
					bnetFriendChangelist.AddAddedFriend(nearbyPlayerUpdate.Player);
				}
			}
			m_nearbyPlayerUpdates.Clear();
			UpdateAllHeaders();
			ResumeItemsLayout();
			UpdateAllHeaderBackgrounds();
			UpdateSelectedItem();
		}
		m_nearbyPlayersNeedUpdate = false;
		m_lastNearbyPlayersUpdate = Time.realtimeSinceStartup;
		if (bnetFriendChangelist != null)
		{
			OnFriendsChanged(bnetFriendChangelist, null);
		}
	}

	private void DoPlayersChanged(BnetPlayerChangelist changelist)
	{
		SuspendItemsLayout();
		BnetPlayer myPlayer = BnetPresenceMgr.Get().GetMyPlayer();
		bool flag = false;
		bool flag2 = false;
		foreach (BnetPlayerChange change in changelist.GetChanges())
		{
			BnetPlayer oldPlayer = change.GetOldPlayer();
			BnetPlayer newPlayer = change.GetNewPlayer();
			if (newPlayer == myPlayer)
			{
				UpdateMyself();
				BnetGameAccount hearthstoneGameAccount = newPlayer.GetHearthstoneGameAccount();
				flag = ((oldPlayer != null && !(oldPlayer.GetHearthstoneGameAccount() == null)) ? (oldPlayer.GetHearthstoneGameAccount().CanBeInvitedToGame() != hearthstoneGameAccount.CanBeInvitedToGame()) : hearthstoneGameAccount.CanBeInvitedToGame());
				continue;
			}
			if (oldPlayer == null || oldPlayer.GetBestName() != newPlayer.GetBestName())
			{
				flag2 = true;
			}
			UpdateFriendFrame(newPlayer);
		}
		if (flag)
		{
			UpdateItems();
		}
		else if (flag2)
		{
			UpdateFriendItems();
		}
		UpdateAllHeaders();
		UpdateAllHeaderBackgrounds();
		ResumeItemsLayout();
	}

	private void OnPlayersChanged(BnetPlayerChangelist changelist, object userData)
	{
		if (base.gameObject.activeInHierarchy)
		{
			DoPlayersChanged(changelist);
			return;
		}
		List<BnetPlayerChange> changes = changelist.GetChanges();
		m_playersChangeList.GetChanges().AddRange(changes);
	}

	private void OnRequestNearbyFSGsResponse()
	{
		Log.FiresideGatherings.Print("FriendListFrame.OnNearbyFSGsResponse");
		UpdateCurrentFiresideGatherings();
		UpdateFoundFiresideGatherings();
		SortAndRefreshTouchList();
	}

	private void OnJoinFSG(FSGConfig gathering)
	{
		Log.FiresideGatherings.Print("FriendListFrame.OnJoinFSG");
		UpdateCurrentFiresideGatherings();
		UpdateFiresideGatheringPlayers(FiresideGatheringManager.Get().DisplayablePatronList, null);
		SortAndRefreshTouchList();
		UpdateFSGState();
	}

	private void OnLeaveFSG(FSGConfig gathering)
	{
		Log.FiresideGatherings.Print("FriendListFrame.OnLeaveFSG");
		UpdateFoundFiresideGatherings();
		RemoveAllFiresideGatheringPlayers();
		SortAndRefreshTouchList();
		UpdateFSGState();
	}

	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if ((uint)(mode - 8) <= 1u)
		{
			if (ChatMgr.Get() != null)
			{
				ChatMgr.Get().CloseFriendsList();
			}
			else
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void SpectatorManager_OnInviteReceivedOrSent(OnlineEventType evt, BnetPlayer inviter)
	{
		UpdateFriendFrame(inviter);
	}

	private void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		if (player == BnetPresenceMgr.Get().GetMyPlayer())
		{
			UpdateFriendItems();
		}
		else
		{
			UpdateFriendFrame(player);
		}
	}

	private bool HandleKeyboardInput()
	{
		FatalErrorMgr.Get().HasError();
		return false;
	}

	private void OnAddFriendButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		CloseFlyoutMenu();
		if (m_addFriendFrame != null)
		{
			CloseAddFriendFrame();
			return;
		}
		if (this.AddFriendFrameOpened != null)
		{
			this.AddFriendFrameOpened();
		}
		BnetPlayer selectedFriend = FriendMgr.Get().GetSelectedFriend();
		ShowAddFriendFrame(selectedFriend);
	}

	private void OnEditFriendsButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		ToggleRemoveFriendsMode();
	}

	private void OnRAFButtonReleased(UIEvent e)
	{
		if (m_isRAFButtonEnabled)
		{
			SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
			RAFManager.Get().ShowRAFFrame();
			TelemetryManager.Client().SendClickRecruitAFriend();
		}
	}

	private void OnRAFButtonOver(UIEvent e)
	{
		TooltipZone component = rafButton.GetComponent<TooltipZone>();
		if (!(component == null))
		{
			string bodytext = ((GameUtils.GetNextTutorial() != 0) ? GameStrings.Get("GLUE_RAF_TOOLTIP_LOCKED_DESC") : GameStrings.Get("GLUE_RAF_TOOLTIP_DESC"));
			component.ShowSocialTooltip(rafButton, GameStrings.Get("GLUE_RAF_TOOLTIP_HEADLINE"), bodytext, 75f, GameLayer.BattleNetDialog);
		}
	}

	private void OnRAFButtonOut(UIEvent e)
	{
		TooltipZone component = rafButton.GetComponent<TooltipZone>();
		if (component != null)
		{
			component.HideTooltip();
		}
	}

	private void OnFSGButtonReleased(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		Options.Get().SetBool(Option.HAS_CLICKED_FIRESIDE_GATHERINGS_BUTTON, val: true);
		FiresideGatheringManager.Get().ShowFindFSGDialog();
		ChatMgr.Get().CloseFriendsList();
	}

	private bool OnRemoveFriendDialogShown(DialogBase dialog, object userData)
	{
		BnetPlayer player = (BnetPlayer)userData;
		if (!BnetFriendMgr.Get().IsFriend(player))
		{
			return false;
		}
		m_removeFriendPopup = (AlertPopup)dialog;
		return true;
	}

	private void OnRemoveFriendPopupResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CONFIRM && m_friendToRemove != null)
		{
			BnetFriendMgr.Get().RemoveFriend(m_friendToRemove);
		}
		m_friendToRemove = null;
		m_removeFriendPopup = null;
		if (this.RemoveFriendPopupClosed != null)
		{
			this.RemoveFriendPopupClosed();
		}
	}

	private void OnFlyoutButtonReleased(UIEvent e)
	{
		if (IsInEditMode)
		{
			ExitRemoveFriendsMode();
		}
		else if (m_flyoutOpen)
		{
			CloseFlyoutMenu();
		}
		else
		{
			OpenFlyoutMenu();
		}
		if (ChatMgr.Get().IsChatLogUIShowing())
		{
			ChatMgr.Get().CloseChatUI(closeFriendList: false);
		}
	}

	private void UpdateSelectedItem()
	{
		BnetPlayer selectedFriend = FriendMgr.Get().GetSelectedFriend();
		FriendListFriendFrame friendListFriendFrame = FindRenderedBaseFriendFrame(selectedFriend);
		if (friendListFriendFrame == null)
		{
			if (items.SelectedIndex == -1)
			{
				return;
			}
			items.SelectedIndex = -1;
			if (m_removeFriendPopup != null)
			{
				m_removeFriendPopup.Hide();
				m_removeFriendPopup = null;
				if (this.RemoveFriendPopupClosed != null)
				{
					this.RemoveFriendPopupClosed();
				}
			}
		}
		else
		{
			items.SelectedIndex = items.IndexOf(friendListFriendFrame.GetComponent<MobileFriendListItem>());
		}
	}

	private void UpdateRAFState()
	{
		if (SetRotationManager.ShouldShowSetRotationIntro() || SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN || WelcomeQuests.Get() != null || TemporaryAccountManager.IsTemporaryAccount() || GameUtils.GetNextTutorial() != 0)
		{
			SetRAFButtonEnabled(enabled: false);
		}
	}

	private void UpdateFSGState()
	{
		SetFSGButtonEnabled();
	}

	private void InitButtons()
	{
		addFriendButton.AddEventListener(UIEventType.RELEASE, OnAddFriendButtonReleased);
		removeFriendButton.AddEventListener(UIEventType.RELEASE, OnEditFriendsButtonReleased);
		rafButton.AddEventListener(UIEventType.RELEASE, OnRAFButtonReleased);
		rafButton.AddEventListener(UIEventType.ROLLOVER, OnRAFButtonOver);
		rafButton.AddEventListener(UIEventType.ROLLOUT, OnRAFButtonOut);
		fsgButton.AddEventListener(UIEventType.RELEASE, OnFSGButtonReleased);
		flyoutMenuButton.AddEventListener(UIEventType.RELEASE, OnFlyoutButtonReleased);
	}

	private bool GetShowHeaderSection(Option setoption)
	{
		return !(bool)Options.Get().GetOption(setoption, false);
	}

	private void SetShowHeaderSection(Option sectionoption, bool show)
	{
		if (GetShowHeaderSection(sectionoption) != show)
		{
			Options.Get().SetOption(sectionoption, !show);
		}
	}

	private Transform GetBottomRightBone()
	{
		if (!(scrollbar != null) || !scrollbar.gameObject.activeSelf)
		{
			return listInfo.bottomRight;
		}
		return listInfo.bottomRightWithScrollbar;
	}

	private void OnTemporaryAccountSignUpButtonPressed(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		ChatMgr.Get().CloseFriendsList();
		TemporaryAccountManager.Get().ShowHealUpPage(TemporaryAccountManager.HealUpReason.FRIENDS_LIST);
	}

	private void OnMyRankedMedalWidgetReady(Widget widget)
	{
		m_myRankedMedalWidget = widget;
		m_myRankedMedal = widget.GetComponentInChildren<RankedMedal>();
		UpdateMyRankedMedalWidget();
	}

	private void UpdateMyRankedMedalWidget()
	{
		if (m_myRankedMedal == null)
		{
			return;
		}
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		if (localPlayerMedalInfo == null || !localPlayerMedalInfo.IsDisplayable())
		{
			m_myRankedMedalWidget.Hide();
			return;
		}
		m_myRankedMedalWidget.Show();
		localPlayerMedalInfo.CreateOrUpdateDataModel(localPlayerMedalInfo.GetBestCurrentRankFormatType(), ref m_myRankedDataModel, RankedMedal.DisplayMode.Default, isTooltipEnabled: false, hasEarnedCardBack: false, delegate(RankedPlayDataModel dm)
		{
			m_myRankedMedal.BindRankedPlayDataModel(dm);
		});
	}
}
