using System;

namespace bgs
{
	// Token: 0x02000211 RID: 529
	public enum BnetFeatureEvent
	{
		// Token: 0x04000BCD RID: 3021
		Auth_Logon,
		// Token: 0x04000BCE RID: 3022
		Auth_OnFinish,
		// Token: 0x04000BCF RID: 3023
		Auth_GameAccountSelected,
		// Token: 0x04000BD0 RID: 3024
		Auth_MemModuleLoad,
		// Token: 0x04000BD1 RID: 3025
		Friends_Created,
		// Token: 0x04000BD2 RID: 3026
		Friends_OnCreated,
		// Token: 0x04000BD3 RID: 3027
		Friends_OnAcceptInvitation,
		// Token: 0x04000BD4 RID: 3028
		Friends_OnRevokeInvitation,
		// Token: 0x04000BD5 RID: 3029
		Friends_OnDeclineInvitation,
		// Token: 0x04000BD6 RID: 3030
		Friends_OnIgnoreInvitation,
		// Token: 0x04000BD7 RID: 3031
		Friends_OnSendInvitation,
		// Token: 0x04000BD8 RID: 3032
		Friends_OnRemoveFriend,
		// Token: 0x04000BD9 RID: 3033
		Games_Created,
		// Token: 0x04000BDA RID: 3034
		Games_OnCreated,
		// Token: 0x04000BDB RID: 3035
		Games_OnQueueLeft,
		// Token: 0x04000BDC RID: 3036
		Games_OnMatchmakerEnd,
		// Token: 0x04000BDD RID: 3037
		Games_OnEnteredGame,
		// Token: 0x04000BDE RID: 3038
		Games_OnFindGame,
		// Token: 0x04000BDF RID: 3039
		Games_OnCancelGame,
		// Token: 0x04000BE0 RID: 3040
		Games_OnClientRequest,
		// Token: 0x04000BE1 RID: 3041
		LocalStore_Created,
		// Token: 0x04000BE2 RID: 3042
		LocalStore_OnCreated,
		// Token: 0x04000BE3 RID: 3043
		Party_API_Created,
		// Token: 0x04000BE4 RID: 3044
		Party_API_OnCreated,
		// Token: 0x04000BE5 RID: 3045
		Party_Create_Callback,
		// Token: 0x04000BE6 RID: 3046
		Party_Join_Callback,
		// Token: 0x04000BE7 RID: 3047
		Party_Leave_Callback,
		// Token: 0x04000BE8 RID: 3048
		Party_Dissolve_Callback,
		// Token: 0x04000BE9 RID: 3049
		Party_SetPrivacy_Callback,
		// Token: 0x04000BEA RID: 3050
		Party_SendInvite_Callback,
		// Token: 0x04000BEB RID: 3051
		Party_AcceptInvite_Callback,
		// Token: 0x04000BEC RID: 3052
		Party_DeclineInvite_Callback,
		// Token: 0x04000BED RID: 3053
		Party_RevokeInvite_Callback,
		// Token: 0x04000BEE RID: 3054
		Party_RequestPartyInvite_Callback,
		// Token: 0x04000BEF RID: 3055
		Party_IgnoreInviteRequest_Callback,
		// Token: 0x04000BF0 RID: 3056
		Party_KickMember_Callback,
		// Token: 0x04000BF1 RID: 3057
		Party_SendChatMessage_Callback,
		// Token: 0x04000BF2 RID: 3058
		Party_ClearAttribute_Callback,
		// Token: 0x04000BF3 RID: 3059
		Party_SetAttribute_Callback,
		// Token: 0x04000BF4 RID: 3060
		Party_AssignRole_Callback,
		// Token: 0x04000BF5 RID: 3061
		Presence_Created,
		// Token: 0x04000BF6 RID: 3062
		Presence_OnCreated,
		// Token: 0x04000BF7 RID: 3063
		Presence_OnRegisterConsumer,
		// Token: 0x04000BF8 RID: 3064
		Presence_OnPublishRichPresence,
		// Token: 0x04000BF9 RID: 3065
		Presence_OnRequestPresenceFields,
		// Token: 0x04000BFA RID: 3066
		Whisper_Created,
		// Token: 0x04000BFB RID: 3067
		Whisper_OnCreated,
		// Token: 0x04000BFC RID: 3068
		Whisper_OnSend,
		// Token: 0x04000BFD RID: 3069
		Bnet_OnConnected,
		// Token: 0x04000BFE RID: 3070
		Bnet_OnDisconnected,
		// Token: 0x04000BFF RID: 3071
		Challenge_Created,
		// Token: 0x04000C00 RID: 3072
		Challenge_OnCreated,
		// Token: 0x04000C01 RID: 3073
		Interface_GetLaunchOption,
		// Token: 0x04000C02 RID: 3074
		Resources_Created,
		// Token: 0x04000C03 RID: 3075
		ProfanityFilter_Created,
		// Token: 0x04000C04 RID: 3076
		Broadcast_Created,
		// Token: 0x04000C05 RID: 3077
		Broadcast_OnCreated,
		// Token: 0x04000C06 RID: 3078
		Notification_Created,
		// Token: 0x04000C07 RID: 3079
		Notification_OnCreated
	}
}
