using System.Collections;
using System.Collections.Generic;

public class ULDA_Tavern : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_900h_Male_Human_Death_01 = new AssetReference("VO_ULDA_900h_Male_Human_Death_01.prefab:a618512d0ae93c6419d1b9f1720f0c7d");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_01.prefab:65498b246600ab84b9eb4e96184e45e1");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_02 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_02.prefab:48566fa9d5068774ca67f1ec23f06d29");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle_03 = new AssetReference("VO_ULDA_900h_Male_Human_Idle_03.prefab:a6ce884acf40eb74180e9e752fb41657");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle1_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle1_01.prefab:7f4788f127fa1684e982d622e4424fd1");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Idle2_01 = new AssetReference("VO_ULDA_900h_Male_Human_Idle2_01.prefab:ff21e137e4df36747a30929d6ab7d92b");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Intro_01 = new AssetReference("VO_ULDA_900h_Male_Human_Intro_01.prefab:9bf19e53c470da045bfe20d61dc0e585");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Intro_02 = new AssetReference("VO_ULDA_900h_Male_Human_Intro_02.prefab:bdcfc890b9ad2be4396e1648c0397bcb");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroBrann_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroBrann_01.prefab:3041ed0482e59554b8940d2a934fe9d0");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroDesert_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroDesert_01.prefab:75caa9e4ea688d34188356b2499c12a3");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroElise_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroElise_01.prefab:53ce4c280b8f5c444999fdcc49027d03");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroFinley_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroFinley_01.prefab:31b5df6d6a9eb8b488fc0a0c5feb7188");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01.prefab:a45e068e096abd6409a0c197b3792ea8");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroLostCity_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroLostCity_01.prefab:50fcf1a49c9555649a4c8db3e363cdf6");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroReno_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroReno_01.prefab:a576c5b44d4278f4bb56938af4edd0d9");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_IntroTomb_01 = new AssetReference("VO_ULDA_900h_Male_Human_IntroTomb_01.prefab:568a63b73074b0346afc8bb2aa240acf");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_01 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_01.prefab:f90597594b5e1fa4fb4c398c9d371ad8");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_02 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_02.prefab:9c881a8dd90f185408b4e31ca9bc2e2c");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_03 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_03.prefab:88e4f84362decef4989053c6088f3fed");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_Outro_04 = new AssetReference("VO_ULDA_900h_Male_Human_Outro_04.prefab:c3866464c40079d4b9d8763b0d5a5b7a");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_OutroTomb_01 = new AssetReference("VO_ULDA_900h_Male_Human_OutroTomb_01.prefab:5a7668fdb3254f4479a086e60bcf8651");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_01.prefab:16b2ac46ef0a3dd4c8fc196a097e9121");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_02.prefab:52986adc218b63549946a774a79e6c87");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerBrood_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerBrood_03.prefab:12641e68851fb6a45921086eb72c5259");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_01.prefab:e3c6117dc85931b488820418b5ebca46");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_02.prefab:007a20f573821fe46abad2320f42588d");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerDismiss_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerDismiss_03.prefab:47ec8429b2702de499444fef95972af2");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_01.prefab:e469dcf6a9bf2ae4aa04a0315e8780b9");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_02.prefab:ebe23f92c5ac3814f81c2cf3b7d97cf5");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerGoodFood_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerGoodFood_03.prefab:a77a0845a6119174688bbad816c37dab");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_01.prefab:1bad65c353a782249aebc03b0fdfd6a4");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_02.prefab:a17c1f358216e274bb40d32a3080c298");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerKindle_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerKindle_03.prefab:7f1ac96d6edebf14699b348f4ec447df");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_01.prefab:667dec4793e3fbb4ca6dee70ee293081");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_02.prefab:703cb4d6eabc74b478d5f322d5b07fc0");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruit_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruit_03.prefab:bd69258416bf3bd40a67de1b07fac6a1");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01.prefab:ca3d6ea5ea7bcc645875af6997c3c99c");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02.prefab:ec1c81a1e32aad3498d8b261cfc74e8f");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03.prefab:eea6ed6b4c5c7304c81b89c6a7565304");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_01.prefab:2313c06d0d5e09e44b0f7e60a08c9435");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_02.prefab:39f1dcbe34147af4b9e2597a632ccb09");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRightHandMan_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRightHandMan_03.prefab:07ee37e9bdd88a542a66570c6d1c8835");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01.prefab:cbc0b3b5edf633e479922267130ae75b");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02.prefab:7b080fb528b74b543b22482754490e6f");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03.prefab:50fa4cf8fd9e03e43a72b5786b392323");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_01.prefab:45e8ed29067f702449397cd16d377b3c");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_02.prefab:5002f55604bdd4849987f659ed624f49");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTakeAChance_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTakeAChance_03.prefab:75d8f482869d1c94d97730e882d555c4");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_01.prefab:91e8eeec3f5cba64d8d83560e237fd74");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_02.prefab:715791bca57fa894c85a5348cc09c037");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTallTales_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTallTales_03.prefab:f97e00a29a5ef4b43931c49725c0ab81");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_01.prefab:2cbce5768dcedda44bab0bbdc8af3b43");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_02.prefab:fc516e58206fe3241a8e22131f43c5af");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTellAStory_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTellAStory_03.prefab:da2016f741eca1647a9d2baf4223521d");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_01.prefab:5b4c8f675f9096d49a1ad23239339d33");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_02.prefab:4518bcd42ea25f1488bfbc6711ee1379");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheChallenge_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheChallenge_03.prefab:03a6ca9852b70ca4e82d99254776fd42");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01.prefab:44ec582a2eaa8984f84a78bd77921a56");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02.prefab:cc1d5dc0d55b8c24aab751c7857cb31d");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03.prefab:3f5016af315c8a844945d63d7a011677");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01.prefab:1571c80b735c54441a4ecf2df09f33ca");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02.prefab:248ad8c32e6b98244a31eaa6fdf448bc");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03 = new AssetReference("VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03.prefab:4b810d42e0221574f9747a9a3eb24ffc");

	private static readonly AssetReference VO_ULDA_900h_Male_Human_RareTreasure_01 = new AssetReference("VO_ULDA_900h_Male_Human_RareTreasure_01.prefab:d9c549f0640fd344db572338ccfcdbd8");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01.prefab:540ac575d524e4745bfdc00458b1d088");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02.prefab:99f3e65a7220c664b880ce0c876e3bcd");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03.prefab:799e5530f557a3c4795b6dd013413459");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01.prefab:1942472c07315c447b94ebcb589e8a26");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02.prefab:81aefc8eef05603408d8197329d25693");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03.prefab:a4c5d93c7ad167945b947544d56be8e9");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01.prefab:6e3a6ea1c8e0e714aae1a97ce3eb164e");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02.prefab:d1ca72a295335a14db0f86a3aa2db056");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03.prefab:93b587275021b9341b704497105dea84");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_01.prefab:881fafab42858244f8ff9c3c361247b2");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_02 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_02.prefab:d9148ac445aa53b48ab7714f8263008d");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Entering_The_Inn_03 = new AssetReference("VO_ULDA_Reno_Male_Human_Entering_The_Inn_03.prefab:bfd07b9ad56d7c744a35d9c2cf0127df");

	private List<string> m_IdleLines = new List<string> { VO_ULDA_900h_Male_Human_Idle_01, VO_ULDA_900h_Male_Human_Idle_02, VO_ULDA_900h_Male_Human_Idle_03, VO_ULDA_900h_Male_Human_Idle1_01 };

	private List<string> m_IntroLines = new List<string> { VO_ULDA_900h_Male_Human_Intro_01, VO_ULDA_900h_Male_Human_Intro_02 };

	private List<string> m_OutroLines = new List<string> { VO_ULDA_900h_Male_Human_Outro_01, VO_ULDA_900h_Male_Human_Outro_02, VO_ULDA_900h_Male_Human_Outro_03, VO_ULDA_900h_Male_Human_Outro_04 };

	private List<string> m_PlayerBroodLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerBrood_01, VO_ULDA_900h_Male_Human_PlayerBrood_02, VO_ULDA_900h_Male_Human_PlayerBrood_03 };

	private List<string> m_PlayerDismissLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerDismiss_01, VO_ULDA_900h_Male_Human_PlayerDismiss_02, VO_ULDA_900h_Male_Human_PlayerDismiss_03 };

	private List<string> m_PlayerGoodFoodLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerGoodFood_01, VO_ULDA_900h_Male_Human_PlayerGoodFood_02, VO_ULDA_900h_Male_Human_PlayerGoodFood_03 };

	private List<string> m_PlayerKindleLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerKindle_01, VO_ULDA_900h_Male_Human_PlayerKindle_02, VO_ULDA_900h_Male_Human_PlayerKindle_03 };

	private List<string> m_PlayerRecruitLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerRecruit_01, VO_ULDA_900h_Male_Human_PlayerRecruit_02, VO_ULDA_900h_Male_Human_PlayerRecruit_03 };

	private List<string> m_PlayerRecruitAVeteranLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01, VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02, VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03 };

	private List<string> m_PlayerRightHandManLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerRightHandMan_01, VO_ULDA_900h_Male_Human_PlayerRightHandMan_02, VO_ULDA_900h_Male_Human_PlayerRightHandMan_03 };

	private List<string> m_PlayerRoundofDrinksLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01, VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02, VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03 };

	private List<string> m_PlayerTakeAChanceLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerTakeAChance_01, VO_ULDA_900h_Male_Human_PlayerTakeAChance_02, VO_ULDA_900h_Male_Human_PlayerTakeAChance_03 };

	private List<string> m_PlayerTallTalesLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerTallTales_01, VO_ULDA_900h_Male_Human_PlayerTallTales_02, VO_ULDA_900h_Male_Human_PlayerTallTales_03 };

	private List<string> m_PlayerTellAStoryLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerTellAStory_01, VO_ULDA_900h_Male_Human_PlayerTellAStory_02, VO_ULDA_900h_Male_Human_PlayerTellAStory_03 };

	private List<string> m_PlayerTheChallengeLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerTheChallenge_01, VO_ULDA_900h_Male_Human_PlayerTheChallenge_02 };

	private List<string> m_PlayerTheGangsAllHereLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01, VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02, VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03 };

	private List<string> m_PlayerYoureAllFiredLines = new List<string> { VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01, VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02, VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03 };

	private List<string> m_IntroRenoLines = new List<string> { VO_ULDA_Reno_Male_Human_Entering_The_Inn_01, VO_ULDA_Reno_Male_Human_Entering_The_Inn_02, VO_ULDA_Reno_Male_Human_Entering_The_Inn_03 };

	private List<string> m_IntroBrannLines = new List<string> { VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01, VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02, VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03 };

	private List<string> m_IntroEliseLines = new List<string> { VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01, VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02, VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03 };

	private List<string> m_IntroFinleyLines = new List<string> { VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01, VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02, VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	protected override void HandleMainReadyStep()
	{
		if (GameState.Get() == null)
		{
			Log.Gameplay.PrintError("ULDA_Tavern.HandleMainReadyStep(): GameState is null.");
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			Log.Gameplay.PrintError("ULDA_Tavern.HandleMainReadyStep(): GameEntity is null.");
		}
		else if (gameEntity.GetTag(GAME_TAG.TURN) == 1)
		{
			GameState.Get().SetMulliganBusy(busy: true);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public ULDA_Tavern()
	{
		SceneMgr.Get().RegisterSceneLoadedEvent(OnGameplaySceneLoaded);
	}

	~ULDA_Tavern()
	{
		if (SceneMgr.Get() != null)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnGameplaySceneLoaded);
		}
	}

	private void OnGameplaySceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnGameplaySceneLoaded);
			ManaCrystalMgr.Get().SetEnemyManaCounterActive(active: false);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_ULDA_900h_Male_Human_Death_01;
		m_standardEmoteResponseLine = VO_ULDA_900h_Male_Human_PlayerTheChallenge_03;
	}

	public override bool ShouldDoAlternateMulliganIntro()
	{
		return true;
	}

	public override bool DoAlternateMulliganIntro()
	{
		new TavernMulliganIntro().Show(GameEntity.Coroutines);
		return true;
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		Network.Get().DisconnectFromGameServer();
		SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
		GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
		SceneMgr.Get().SetNextMode(postGameSceneMode);
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override string[] GetOverrideBoardClickSounds()
	{
		return new string[4] { "TavernBoard_WoodPoke_Creak_1.prefab:ab4c166deba73ef4c80d46dca53aaf14", "TavernBoard_WoodPoke_Creak_2.prefab:272821729af020e46acb68d4e3f29e8e", "TavernBoard_WoodPoke_Creak_3.prefab:80220d29df0ae4849be9a9029b33088a", "TavernBoard_WoodPoke_Creak_4.prefab:0a478645bd054864c9ac52804f3be118" };
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_900h_Male_Human_Death_01, VO_ULDA_900h_Male_Human_Idle_01, VO_ULDA_900h_Male_Human_Idle_02, VO_ULDA_900h_Male_Human_Idle_03, VO_ULDA_900h_Male_Human_Idle1_01, VO_ULDA_900h_Male_Human_Idle2_01, VO_ULDA_900h_Male_Human_Intro_01, VO_ULDA_900h_Male_Human_Intro_02, VO_ULDA_900h_Male_Human_IntroBrann_01, VO_ULDA_900h_Male_Human_IntroDesert_01,
			VO_ULDA_900h_Male_Human_IntroElise_01, VO_ULDA_900h_Male_Human_IntroFinley_01, VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01, VO_ULDA_900h_Male_Human_IntroLostCity_01, VO_ULDA_900h_Male_Human_IntroReno_01, VO_ULDA_900h_Male_Human_IntroTomb_01, VO_ULDA_900h_Male_Human_Outro_01, VO_ULDA_900h_Male_Human_Outro_02, VO_ULDA_900h_Male_Human_Outro_03, VO_ULDA_900h_Male_Human_Outro_04,
			VO_ULDA_900h_Male_Human_OutroTomb_01, VO_ULDA_900h_Male_Human_PlayerBrood_01, VO_ULDA_900h_Male_Human_PlayerBrood_02, VO_ULDA_900h_Male_Human_PlayerBrood_03, VO_ULDA_900h_Male_Human_PlayerDismiss_01, VO_ULDA_900h_Male_Human_PlayerDismiss_02, VO_ULDA_900h_Male_Human_PlayerDismiss_03, VO_ULDA_900h_Male_Human_PlayerGoodFood_01, VO_ULDA_900h_Male_Human_PlayerGoodFood_02, VO_ULDA_900h_Male_Human_PlayerGoodFood_03,
			VO_ULDA_900h_Male_Human_PlayerKindle_01, VO_ULDA_900h_Male_Human_PlayerKindle_02, VO_ULDA_900h_Male_Human_PlayerKindle_03, VO_ULDA_900h_Male_Human_PlayerRecruit_01, VO_ULDA_900h_Male_Human_PlayerRecruit_02, VO_ULDA_900h_Male_Human_PlayerRecruit_03, VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_01, VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_02, VO_ULDA_900h_Male_Human_PlayerRecruitAVeteran_03, VO_ULDA_900h_Male_Human_PlayerRightHandMan_01,
			VO_ULDA_900h_Male_Human_PlayerRightHandMan_02, VO_ULDA_900h_Male_Human_PlayerRightHandMan_03, VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_01, VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_02, VO_ULDA_900h_Male_Human_PlayerRoundofDrinks_03, VO_ULDA_900h_Male_Human_PlayerTakeAChance_01, VO_ULDA_900h_Male_Human_PlayerTakeAChance_02, VO_ULDA_900h_Male_Human_PlayerTakeAChance_03, VO_ULDA_900h_Male_Human_PlayerTallTales_01, VO_ULDA_900h_Male_Human_PlayerTallTales_02,
			VO_ULDA_900h_Male_Human_PlayerTallTales_03, VO_ULDA_900h_Male_Human_PlayerTellAStory_01, VO_ULDA_900h_Male_Human_PlayerTellAStory_02, VO_ULDA_900h_Male_Human_PlayerTellAStory_03, VO_ULDA_900h_Male_Human_PlayerTheChallenge_01, VO_ULDA_900h_Male_Human_PlayerTheChallenge_02, VO_ULDA_900h_Male_Human_PlayerTheChallenge_03, VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_01, VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_02, VO_ULDA_900h_Male_Human_PlayerTheGangsAllHere_03,
			VO_ULDA_900h_Male_Human_PlayerYoureAllFired_01, VO_ULDA_900h_Male_Human_PlayerYoureAllFired_02, VO_ULDA_900h_Male_Human_PlayerYoureAllFired_03, VO_ULDA_900h_Male_Human_RareTreasure_01, VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_01, VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_02, VO_ULDA_Brann_Male_Dwarf_Entering_The_Inn_03, VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_01, VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_02, VO_ULDA_Elise_Female_NightElf_Entering_The_Inn_03,
			VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_01, VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_02, VO_ULDA_Finley_Male_Murloc_Entering_The_Inn_03, VO_ULDA_Reno_Male_Human_Entering_The_Inn_01, VO_ULDA_Reno_Male_Human_Entering_The_Inn_02, VO_ULDA_Reno_Male_Human_Entering_The_Inn_03
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			switch (cardId)
			{
			case "ULDA_Reno":
				yield return PlayRandomLineAlways(actor, m_IntroRenoLines);
				m_IntroLines.Add(VO_ULDA_900h_Male_Human_IntroReno_01);
				break;
			case "ULDA_Brann":
				yield return PlayRandomLineAlways(actor, m_IntroBrannLines);
				m_IntroLines.Add(VO_ULDA_900h_Male_Human_IntroBrann_01);
				break;
			case "ULDA_Finley":
				yield return PlayRandomLineAlways(actor, m_IntroFinleyLines);
				m_IntroLines.Add(VO_ULDA_900h_Male_Human_IntroFinley_01);
				break;
			case "ULDA_Elise":
				yield return PlayRandomLineAlways(actor, m_IntroEliseLines);
				m_IntroLines.Add(VO_ULDA_900h_Male_Human_IntroElise_01);
				break;
			}
			yield return PlayRandomLineAlways(enemyActor, m_IntroLines);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_OutroLines);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(enemyActor, VO_ULDA_900h_Male_Human_OutroTomb_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 110:
			m_IntroLines.Add(VO_ULDA_900h_Male_Human_IntroLostCity_01);
			break;
		case 111:
			m_IntroLines.Add(VO_ULDA_900h_Male_Human_IntroDesert_01);
			break;
		case 112:
			m_IntroLines.Add(VO_ULDA_900h_Male_Human_IntroTomb_01);
			break;
		case 113:
			m_IntroLines.Add(VO_ULDA_900h_Male_Human_IntroHallsofOrigination_01);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "DALA_912":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerBroodLines);
				break;
			case "DALA_902":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerDismissLines);
				break;
			case "DALA_909":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerYoureAllFiredLines);
				break;
			case "DALA_904":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerGoodFoodLines);
				break;
			case "DALA_911":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerKindleLines);
				break;
			case "DALA_901":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerRecruitLines);
				break;
			case "DALA_907":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerRecruitAVeteranLines);
				break;
			case "DALA_905":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerRightHandManLines);
				break;
			case "DALA_906":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerRoundofDrinksLines);
				break;
			case "DALA_903":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerTakeAChanceLines);
				break;
			case "DALA_913":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerTallTalesLines);
				break;
			case "ULDA_602":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerTellAStoryLines);
				break;
			case "ULDA_608":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerTheChallengeLines);
				break;
			case "DALA_910":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerTheGangsAllHereLines);
				break;
			case "ULDA_607":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_900h_Male_Human_RareTreasure_01);
				break;
			}
		}
	}
}
