using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using LINQtoCSV;
using System.IO;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using BobNetProto;
using PegasusGame;
using PegasusShared;
using PegasusUtil;
using hearthdecktracker.data;
using System.Threading;
using Networking;

namespace hearthdecktracker
{
    public partial class MainForm : Form
    {
        private bool dragging;
        private Point offset;

        private static PacketDecoderManager s_packetDecoders = new PacketDecoderManager(false);
        private static Dictionary<int, string> entity_dictionary;
        private static string cardid;
        private Thread thread;

        //Tinkered with the AllDeckCardLists class a bit. Updated for new list access
        public List<DeckCardList> deckCardLists = AllDeckCardLists.GetLists();


        /****************************
         * DECK LISTS ARE HERE
         ***************************/
        //Decklists now defined in JSON

        public MainForm()
        {
            setuplistener();
            InitializeComponent();
            cbDeckCardLists.DataSource = deckCardLists;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dragging = false;
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            offset = new Point(e.X, e.Y);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        /*******************
         * Displaying stuff
         *******************/

        private void cbDeckCardLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDeckCardLists.SelectedIndex != 0)
            {
                update_decklist();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (cbDeckCardLists.SelectedIndex != 0)
            {
                update_decklist();
            }
        }


        private void choose_row(string cardname)
        {
            if (gvDeckCardList.InvokeRequired)
            {
                Invoke(new Action<string>(choose_row), cardname);
            }
            else
            {
                int oldamt;
                int chg;

                foreach (DataGridViewRow row in gvDeckCardList.Rows)
                {
                    if (row.Cells["Name"].Value.ToString().Equals(cardname))
                    {
                        gvDeckCardList.CurrentCell = gvDeckCardList.Rows[row.Index].Cells[0];
                        //dataGridView1.Rows[row.Index + 1].Selected = true;

                        oldamt = int.Parse(gvDeckCardList.CurrentRow.Cells["Amt"].Value.ToString());
                        chg = -1;
                        if (oldamt != 0)
                        {
                            gvDeckCardList.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.Red;

                            int newamt = Math.Max(0, oldamt + chg);
                            gvDeckCardList.CurrentRow.Cells["Amt"].Value = newamt;

                            if (newamt == 0)
                            {
                                gvDeckCardList.CurrentRow.DefaultCellStyle.ForeColor = Color.DarkGray;
                                gvDeckCardList.CurrentRow.DefaultCellStyle.SelectionForeColor = Color.DarkGray;
                            }
                            else
                            {
                                //dataGridView1.CurrentRow.DefaultCellStyle.ForeColor = Color.DarkGray;
                            }
                        }
                    }
                }
            }
        }

        private void gvDeckCardList_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int oldamt;
            int chg;

            if (e.Button == MouseButtons.Left)
            {
                oldamt = int.Parse(gvDeckCardList.CurrentRow.Cells["Amt"].Value.ToString());
                chg = -1;
                if (oldamt != 0)
                {
                    gvDeckCardList.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.Red;

                    int newamt = Math.Max(0, oldamt + chg);
                    gvDeckCardList.CurrentRow.Cells["Amt"].Value = newamt;


                    if (newamt == 0)
                    {
                        gvDeckCardList.CurrentRow.DefaultCellStyle.ForeColor = Color.DarkGray;
                        gvDeckCardList.CurrentRow.DefaultCellStyle.SelectionForeColor = Color.DarkGray;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                gvDeckCardList.ClearSelection();
                gvDeckCardList.CurrentCell = gvDeckCardList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                gvDeckCardList.Rows[e.RowIndex].Selected = true;
                gvDeckCardList.Focus();

                oldamt = int.Parse(gvDeckCardList.CurrentRow.Cells["Amt"].Value.ToString());
                chg = 1;

                if (oldamt != 2)
                {
                    gvDeckCardList.CurrentRow.DefaultCellStyle.SelectionBackColor = Color.Green;

                    int newamt = Math.Max(0, oldamt + chg);
                    gvDeckCardList.CurrentRow.Cells["Amt"].Value = newamt;

                    gvDeckCardList.CurrentRow.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                    gvDeckCardList.CurrentRow.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
                }
            }
            else
            {
                chg = 0;
            }
        }

        private void update_decklist()
        {
            gvDeckCardList.Rows.Clear();
            entity_dictionary = new Dictionary<int, string>();

            DeckCardList selectedDeckCardList = (DeckCardList) cbDeckCardLists.SelectedValue;

            foreach (DeckCard card in selectedDeckCardList.DeckCards)
            {
                string ad;
                if (card.Card.Atk == 0 && card.Card.Def == 0)
                {
                    ad = "---";
                }
                else
                {
                    ad = card.Card.Atk + "/" + card.Card.Def;
                }

                gvDeckCardList.Rows.Add(
                    Convert.ToInt32(card.Card.Mana),
                    card.Card.Name,
                    ad,
                    card.Card.Dmg + "|" + card.Card.Heal + "|" + card.Card.Catk,
                    card.Card.Targ,
                    Convert.ToInt32(card.Amount)
                );
            }

            gvDeckCardList.Visible = true;
            thread = new Thread(this.readtcpdata);
            thread.Start();
        }

        private void setuplistener()
        {
            //
            // s_packetDecoders.Add(1, new DefaultProtobufPacketDecoder<GetGameState>());
            // s_packetDecoders.Add(2, new DefaultProtobufPacketDecoder<ChooseOption>());
            // s_packetDecoders.Add(3, new DefaultProtobufPacketDecoder<ChooseEntities, ChooseEntities.Builder>());
            // // s_packetDecoders.Add(4, new DefaultProtobufPacketDecoder<Precast, Precast.Builder>());
            // // s_packetDecoders.Add(6, new DefaultProtobufPacketDecoder<ClientPacket, ClientPacket.Builder>());
            // // s_packetDecoders.Add(11, new DefaultProtobufPacketDecoder<GiveUp, GiveUp.Builder>());
            // // s_packetDecoders.Add(13, new DefaultProtobufPacketDecoder<ForcedEntityChoice, ForcedEntityChoice.Builder>());
            // // s_packetDecoders.Add(100, new DefaultProtobufPacketDecoder<AtlasPlayer, AtlasPlayer.Builder>());
            // // s_packetDecoders.Add(101, new DefaultProtobufPacketDecoder<AtlasError, AtlasError.Builder>());
            // // s_packetDecoders.Add(102, new DefaultProtobufPacketDecoder<AtlasCollection, AtlasCollection.Builder>());
            // // s_packetDecoders.Add(103, new DefaultProtobufPacketDecoder<AutoLogin, AutoLogin.Builder>());
            // // s_packetDecoders.Add(104, new DefaultProtobufPacketDecoder<AtlasDecks, AtlasDecks.Builder>());
            // // s_packetDecoders.Add(105, new DefaultProtobufPacketDecoder<AtlasSuccess, AtlasSuccess.Builder>());
            // // s_packetDecoders.Add(106, new DefaultProtobufPacketDecoder<AtlasOrders, AtlasOrders.Builder>());
            // // s_packetDecoders.Add(107, new DefaultProtobufPacketDecoder<AtlasAchieves, AtlasAchieves.Builder>());
            //
            // // s_packetDecoders.Add(108, new DefaultProtobufPacketDecoder<AtlasAchieveInfo, AtlasAchieveInfo.Builder>());
            // // s_packetDecoders.Add(109, new DefaultProtobufPacketDecoder<AtlasBoosters, AtlasBoosters.Builder>());
            // // s_packetDecoders.Add(110, new DefaultProtobufPacketDecoder<AtlasDrafts, AtlasDrafts.Builder>());
            // // s_packetDecoders.Add(111, new DefaultProtobufPacketDecoder<AtlasCurrencyDetails, AtlasCurrencyDetails.Builder>());
            // // s_packetDecoders.Add(112, new DefaultProtobufPacketDecoder<AtlasCardBacks, AtlasCardBacks.Builder>());
            // // s_packetDecoders.Add(113, new DefaultProtobufPacketDecoder<BeginPlaying, BeginPlaying.Builder>());
            //
            // s_packetDecoders.Add(123, new DefaultProtobufPacketDecoder<DebugConsoleCommand, DebugConsoleCommand.Builder>());
            // s_packetDecoders.Add(124, new DefaultProtobufPacketDecoder<DebugConsoleResponse, DebugConsoleResponse.Builder>());
            // s_packetDecoders.Add(125, new DefaultProtobufPacketDecoder<DebugConsoleGetCmdList, DebugConsoleGetCmdList.Builder>());
            // s_packetDecoders.Add(142, new DefaultProtobufPacketDecoder<DebugPaneNewItems, DebugPaneNewItems.Builder>());
            // s_packetDecoders.Add(143, new DefaultProtobufPacketDecoder<DebugPaneDelItems, DebugPaneDelItems.Builder>());
            // s_packetDecoders.Add(145, new DefaultProtobufPacketDecoder<DebugConsoleUpdateFromPane, DebugConsoleUpdateFromPane.Builder>());
            // s_packetDecoders.Add(146, new DefaultProtobufPacketDecoder<DebugConsoleCmdList, DebugConsoleCmdList.Builder>());
            // s_packetDecoders.Add(147, new DefaultProtobufPacketDecoder<DebugConsoleGetZones, DebugConsoleGetZones.Builder>());
            // s_packetDecoders.Add(148, new DefaultProtobufPacketDecoder<DebugConsoleZones, DebugConsoleZones.Builder>());
            // s_packetDecoders.Add(168, new DefaultProtobufPacketDecoder<AuroraHandshake, AuroraHandshake.Builder>());
            // s_packetDecoders.Add(201, new DefaultProtobufPacketDecoder<GetAccountInfo, GetAccountInfo.Builder>());
            // s_packetDecoders.Add(203, new DefaultProtobufPacketDecoder<UtilHandshake, UtilHandshake.Builder>());
            // s_packetDecoders.Add(204, new DefaultProtobufPacketDecoder<UtilAuth, UtilAuth.Builder>());
            // s_packetDecoders.Add(205, new DefaultProtobufPacketDecoder<UpdateLogin, UpdateLogin.Builder>());
            // s_packetDecoders.Add(206, new DefaultProtobufPacketDecoder<DebugAuth, DebugAuth.Builder>());
            // s_packetDecoders.Add(209, new DefaultProtobufPacketDecoder<CreateDeck, CreateDeck.Builder>());
            // s_packetDecoders.Add(210, new DefaultProtobufPacketDecoder<DeleteDeck, DeleteDeck.Builder>());
            // s_packetDecoders.Add(211, new DefaultProtobufPacketDecoder<RenameDeck, RenameDeck.Builder>());
            // s_packetDecoders.Add(213, new DefaultProtobufPacketDecoder<AckNotice, AckNotice.Builder>());
            // s_packetDecoders.Add(214, new DefaultProtobufPacketDecoder<GetDeck, GetDeck.Builder>());
            // s_packetDecoders.Add(220, new DefaultProtobufPacketDecoder<DeckGainedCard, DeckGainedCard.Builder>());
            // s_packetDecoders.Add(221, new DefaultProtobufPacketDecoder<DeckLostCard, DeckLostCard.Builder>());
            // s_packetDecoders.Add(222, new DefaultProtobufPacketDecoder<DeckSetData, DeckSetData.Builder>());
            // s_packetDecoders.Add(223, new DefaultProtobufPacketDecoder<AckCardSeen, AckCardSeen.Builder>());
            // s_packetDecoders.Add(225, new DefaultProtobufPacketDecoder<OpenBooster, OpenBooster.Builder>());
            // s_packetDecoders.Add(228, new DefaultProtobufPacketDecoder<ClientTracking, ClientTracking.Builder>());
            // s_packetDecoders.Add(229, new DefaultProtobufPacketDecoder<SubmitBug, SubmitBug.Builder>());
            // s_packetDecoders.Add(230, new DefaultProtobufPacketDecoder<SetProgress, SetProgress.Builder>());
            // s_packetDecoders.Add(235, new DefaultProtobufPacketDecoder<DraftBegin, DraftBegin.Builder>());
            // s_packetDecoders.Add(237, new DefaultProtobufPacketDecoder<GetBattlePayConfig, GetBattlePayConfig.Builder>());
            // s_packetDecoders.Add(239, new DefaultProtobufPacketDecoder<SetOptions, SetOptions.Builder>());
            // s_packetDecoders.Add(240, new DefaultProtobufPacketDecoder<GetOptions, GetOptions.Builder>());
            // s_packetDecoders.Add(242, new DefaultProtobufPacketDecoder<DraftRetire, DraftRetire.Builder>());
            // s_packetDecoders.Add(243, new DefaultProtobufPacketDecoder<AckAchieveProgress, AckAchieveProgress.Builder>());
            // s_packetDecoders.Add(244, new DefaultProtobufPacketDecoder<DraftGetPicksAndContents, DraftGetPicksAndContents.Builder>());
            // s_packetDecoders.Add(245, new DefaultProtobufPacketDecoder<DraftMakePick, DraftMakePick.Builder>());
            // s_packetDecoders.Add(250, new DefaultProtobufPacketDecoder<GetPurchaseMethod, GetPurchaseMethod.Builder>());
            // s_packetDecoders.Add(253, new DefaultProtobufPacketDecoder<GetAchieves, GetAchieves.Builder>());
            // s_packetDecoders.Add(255, new DefaultProtobufPacketDecoder<GetBattlePayStatus, GetBattlePayStatus.Builder>());
            // s_packetDecoders.Add(257, new DefaultProtobufPacketDecoder<BuySellCard, BuySellCard.Builder>());
            // s_packetDecoders.Add(259, new DefaultProtobufPacketDecoder<DevBnetIdentify, DevBnetIdentify.Builder>());
            // s_packetDecoders.Add(261, new DefaultProtobufPacketDecoder<GuardianTrack, GuardianTrack.Builder>());
            // s_packetDecoders.Add(263, new DefaultProtobufPacketDecoder<CloseCardMarket, CloseCardMarket.Builder>());
            // s_packetDecoders.Add(267, new DefaultProtobufPacketDecoder<CheckAccountLicenses, CheckAccountLicenses.Builder>());
            // s_packetDecoders.Add(268, new DefaultProtobufPacketDecoder<MassDisenchantRequest, MassDisenchantRequest.Builder>());
            // s_packetDecoders.Add(273, new DefaultProtobufPacketDecoder<DoPurchase, DoPurchase.Builder>());
            // s_packetDecoders.Add(274, new DefaultProtobufPacketDecoder<CancelPurchase, CancelPurchase.Builder>());
            // s_packetDecoders.Add(276, new DefaultProtobufPacketDecoder<CheckGameLicenses, CheckGameLicenses.Builder>());
            // s_packetDecoders.Add(279, new DefaultProtobufPacketDecoder<PurchaseWithGold, PurchaseWithGold.Builder>());
            // s_packetDecoders.Add(281, new DefaultProtobufPacketDecoder<CancelQuest, CancelQuest.Builder>());
            // s_packetDecoders.Add(284, new DefaultProtobufPacketDecoder<ValidateAchieve, ValidateAchieve.Builder>());
            // s_packetDecoders.Add(287, new DefaultProtobufPacketDecoder<DraftAckRewards, DraftAckRewards.Builder>());
            // s_packetDecoders.Add(291, new DefaultProtobufPacketDecoder<SetCardBack, SetCardBack.Builder>());
            // s_packetDecoders.Add(293, new DefaultProtobufPacketDecoder<DoThirdPartyPurchase, DoThirdPartyPurchase.Builder>());
            // s_packetDecoders.Add(294, new DefaultProtobufPacketDecoder<GetThirdPartyPurchaseStatus, GetThirdPartyPurchaseStatus.Builder>());
            // s_packetDecoders.Add(298, new DefaultProtobufPacketDecoder<TriggerLaunchDayEvent, TriggerLaunchDayEvent.Builder>());
            // s_packetDecoders.Add(401, new DefaultProtobufPacketDecoder<AtlasGetPlayerInfo, AtlasGetPlayerInfo.Builder>());
            // s_packetDecoders.Add(402, new DefaultProtobufPacketDecoder<AtlasGetCollection, AtlasGetCollection.Builder>());
            // s_packetDecoders.Add(403, new DefaultProtobufPacketDecoder<AtlasGetCardDetails, AtlasGetCardDetails.Builder>());
            // s_packetDecoders.Add(404, new DefaultProtobufPacketDecoder<AtlasGetDecks, AtlasGetDecks.Builder>());
            // s_packetDecoders.Add(405, new DefaultProtobufPacketDecoder<AtlasAddCard, AtlasAddCard.Builder>());
            // s_packetDecoders.Add(406, new DefaultProtobufPacketDecoder<AtlasRemoveCard, AtlasRemoveCard.Builder>());
            // s_packetDecoders.Add(407, new DefaultProtobufPacketDecoder<AtlasChangeArcaneDust, AtlasChangeArcaneDust.Builder>());
            // s_packetDecoders.Add(408, new DefaultProtobufPacketDecoder<AtlasRestoreCard, AtlasRestoreCard.Builder>());
            // s_packetDecoders.Add(409, new DefaultProtobufPacketDecoder<AtlasGetOrders, AtlasGetOrders.Builder>());
            // s_packetDecoders.Add(410, new DefaultProtobufPacketDecoder<AtlasGetAchieves, AtlasGetAchieves.Builder>());
            // s_packetDecoders.Add(411, new DefaultProtobufPacketDecoder<AtlasGetAchieveInfo, AtlasGetAchieveInfo.Builder>());
            // s_packetDecoders.Add(412, new DefaultProtobufPacketDecoder<AtlasGetBoosters, AtlasGetBoosters.Builder>());
            // s_packetDecoders.Add(413, new DefaultProtobufPacketDecoder<AtlasAddBooster, AtlasAddBooster.Builder>());
            // s_packetDecoders.Add(414, new DefaultProtobufPacketDecoder<AtlasRemoveBooster, AtlasRemoveBooster.Builder>());
            // s_packetDecoders.Add(415, new DefaultProtobufPacketDecoder<AtlasGetDrafts, AtlasGetDrafts.Builder>());
            // s_packetDecoders.Add(416, new DefaultProtobufPacketDecoder<AtlasAddDraft, AtlasAddDraft.Builder>());
            // s_packetDecoders.Add(417, new DefaultProtobufPacketDecoder<AtlasRemoveDraft, AtlasRemoveDraft.Builder>());
            // s_packetDecoders.Add(418, new DefaultProtobufPacketDecoder<AtlasChangeGold, AtlasChangeGold.Builder>());
            // s_packetDecoders.Add(419, new DefaultProtobufPacketDecoder<AtlasGetCurrencyDetails, AtlasGetCurrencyDetails.Builder>());
            // s_packetDecoders.Add(420, new DefaultProtobufPacketDecoder<AtlasChangeBonusGold, AtlasChangeBonusGold.Builder>());
            // s_packetDecoders.Add(421, new DefaultProtobufPacketDecoder<AtlasGetCardBacks, AtlasGetCardBacks.Builder>());
            // s_packetDecoders.Add(422, new DefaultProtobufPacketDecoder<AtlasAddCardBack, AtlasAddCardBack.Builder>());
            // s_packetDecoders.Add(423, new DefaultProtobufPacketDecoder<AtlasRemoveCardBack, AtlasRemoveCardBack.Builder>());
            //
            // s_packetDecoders.Add(5, new DefaultProtobufPacketDecoder<DebugMessage, DebugMessage.Builder>());
            // s_packetDecoders.Add(7, new DefaultProtobufPacketDecoder<StartGameState, StartGameState.Builder>());
            // s_packetDecoders.Add(8, new DefaultProtobufPacketDecoder<FinishGameState, FinishGameState.Builder>());
            // s_packetDecoders.Add(9, new DefaultProtobufPacketDecoder<PegasusGame.TurnTimer, PegasusGame.TurnTimer.Builder>());
            // s_packetDecoders.Add(10, new DefaultProtobufPacketDecoder<NAckOption, NAckOption.Builder>());
            // s_packetDecoders.Add(12, new DefaultProtobufPacketDecoder<GameCanceled, GameCanceled.Builder>());
            // s_packetDecoders.Add(14, new DefaultProtobufPacketDecoder<AllOptions, AllOptions.Builder>());
            // //s_packetDecoders.Add(15, new DefaultProtobufPacketDecoder<UserUI, UserUI.Builder>());
            // s_packetDecoders.Add(16, new DefaultProtobufPacketDecoder<GameSetup, GameSetup.Builder>());
            // s_packetDecoders.Add(17, new DefaultProtobufPacketDecoder<EntityChoice, EntityChoice.Builder>());
            // s_packetDecoders.Add(18, new DefaultProtobufPacketDecoder<PreLoad, PreLoad.Builder>());
            // s_packetDecoders.Add(19, new DefaultProtobufPacketDecoder<PowerHistory, PowerHistory.Builder>());
            // s_packetDecoders.Add(21, new DefaultProtobufPacketDecoder<PegasusGame.Notification, PegasusGame.Notification.Builder>());
            // s_packetDecoders.Add(114, new DefaultProtobufPacketDecoder<GameStarting, GameStarting.Builder>());
            // s_packetDecoders.Add(167, new DefaultProtobufPacketDecoder<DeadendUtil, DeadendUtil.Builder>());
            // s_packetDecoders.Add(169, new DefaultProtobufPacketDecoder<Deadend, Deadend.Builder>());
            // s_packetDecoders.Add(202, new DefaultProtobufPacketDecoder<DeckList, DeckList.Builder>());
            // s_packetDecoders.Add(207, new DefaultProtobufPacketDecoder<Collection, Collection.Builder>());
            // s_packetDecoders.Add(208, new DefaultProtobufPacketDecoder<GamesInfo, GamesInfo.Builder>());
            // s_packetDecoders.Add(212, new DefaultProtobufPacketDecoder<ProfileNotices, ProfileNotices.Builder>());
            // s_packetDecoders.Add(215, new DefaultProtobufPacketDecoder<DeckContents, DeckContents.Builder>());
            // s_packetDecoders.Add(216, new DefaultProtobufPacketDecoder<DBAction, DBAction.Builder>());
            // s_packetDecoders.Add(217, new DefaultProtobufPacketDecoder<DeckCreated, DeckCreated.Builder>());
            // s_packetDecoders.Add(218, new DefaultProtobufPacketDecoder<DeckDeleted, DeckDeleted.Builder>());
            // s_packetDecoders.Add(219, new DefaultProtobufPacketDecoder<DeckRenamed, DeckRenamed.Builder>());
            // s_packetDecoders.Add(224, new DefaultProtobufPacketDecoder<BoosterList, BoosterList.Builder>());
            // s_packetDecoders.Add(226, new DefaultProtobufPacketDecoder<BoosterContent, BoosterContent.Builder>());
            // s_packetDecoders.Add(227, new DefaultProtobufPacketDecoder<ProfileLastLogin, ProfileLastLogin.Builder>());
            // s_packetDecoders.Add(231, new DefaultProtobufPacketDecoder<ProfileDeckLimit, ProfileDeckLimit.Builder>());
            // s_packetDecoders.Add(232, new DefaultProtobufPacketDecoder<MedalInfo, MedalInfo.Builder>());
            // s_packetDecoders.Add(233, new DefaultProtobufPacketDecoder<ProfileProgress, ProfileProgress.Builder>());
            // s_packetDecoders.Add(234, new DefaultProtobufPacketDecoder<MedalHistory, MedalHistory.Builder>());
            // s_packetDecoders.Add(236, new DefaultProtobufPacketDecoder<CardBacks, CardBacks.Builder>());
            // s_packetDecoders.Add(238, new DefaultProtobufPacketDecoder<BattlePayConfigResponse, BattlePayConfigResponse.Builder>());
            // s_packetDecoders.Add(241, new DefaultProtobufPacketDecoder<ClientOptions, ClientOptions.Builder>());
            // s_packetDecoders.Add(246, new DefaultProtobufPacketDecoder<DraftBeginning, DraftBeginning.Builder>());
            // s_packetDecoders.Add(247, new DefaultProtobufPacketDecoder<DraftRetired, DraftRetired.Builder>());
            // s_packetDecoders.Add(248, new DefaultProtobufPacketDecoder<DraftChoicesAndContents, DraftChoicesAndContents.Builder>());
            // s_packetDecoders.Add(249, new DefaultProtobufPacketDecoder<DraftChosen, DraftChosen.Builder>());
            // s_packetDecoders.Add(251, new DefaultProtobufPacketDecoder<DraftError, DraftError.Builder>());
            // s_packetDecoders.Add(252, new DefaultProtobufPacketDecoder<Achieves, Achieves.Builder>());
            // s_packetDecoders.Add(254, new NoOpPacketDecoder());
            // s_packetDecoders.Add(256, new DefaultProtobufPacketDecoder<PurchaseResponse, PurchaseResponse.Builder>());
            // s_packetDecoders.Add(258, new DefaultProtobufPacketDecoder<BoughtSoldCard, BoughtSoldCard.Builder>());
            // s_packetDecoders.Add(260, new DefaultProtobufPacketDecoder<CardValues, CardValues.Builder>());
            // s_packetDecoders.Add(262, new DefaultProtobufPacketDecoder<ArcaneDustBalance, ArcaneDustBalance.Builder>());
            // s_packetDecoders.Add(264, new DefaultProtobufPacketDecoder<GuardianVars, GuardianVars.Builder>());
            // s_packetDecoders.Add(265, new DefaultProtobufPacketDecoder<BattlePayStatusResponse, BattlePayStatusResponse.Builder>());
            // s_packetDecoders.Add(266, new ThrottlePacketDecoder());
            // s_packetDecoders.Add(269, new DefaultProtobufPacketDecoder<MassDisenchantResponse, MassDisenchantResponse.Builder>());
            // s_packetDecoders.Add(270, new DefaultProtobufPacketDecoder<PlayerRecords, PlayerRecords.Builder>());
            // s_packetDecoders.Add(271, new DefaultProtobufPacketDecoder<RewardProgress, RewardProgress.Builder>());
            // s_packetDecoders.Add(272, new DefaultProtobufPacketDecoder<PurchaseMethod, PurchaseMethod.Builder>());
            // s_packetDecoders.Add(275, new DefaultProtobufPacketDecoder<CancelPurchaseResponse, CancelPurchaseResponse.Builder>());
            // s_packetDecoders.Add(277, new DefaultProtobufPacketDecoder<CheckLicensesResponse, CheckLicensesResponse.Builder>());
            // s_packetDecoders.Add(278, new DefaultProtobufPacketDecoder<GoldBalance, GoldBalance.Builder>());
            // s_packetDecoders.Add(280, new DefaultProtobufPacketDecoder<PurchaseWithGoldResponse, PurchaseWithGoldResponse.Builder>());
            // s_packetDecoders.Add(282, new DefaultProtobufPacketDecoder<CancelQuestResponse, CancelQuestResponse.Builder>());
            // s_packetDecoders.Add(283, new DefaultProtobufPacketDecoder<HeroXP, HeroXP.Builder>());
            // s_packetDecoders.Add(285, new DefaultProtobufPacketDecoder<ValidateAchieveResponse, ValidateAchieveResponse.Builder>());
            // s_packetDecoders.Add(286, new DefaultProtobufPacketDecoder<PlayQueue, PlayQueue.Builder>());
            // s_packetDecoders.Add(288, new DefaultProtobufPacketDecoder<DraftRewardsAcked, DraftRewardsAcked.Builder>());
            // s_packetDecoders.Add(289, new DefaultProtobufPacketDecoder<Disconnected, Disconnected.Builder>());
            // s_packetDecoders.Add(292, new DefaultProtobufPacketDecoder<SetCardBackResponse, SetCardBackResponse.Builder>());
            // s_packetDecoders.Add(295, new DefaultProtobufPacketDecoder<ThirdPartyPurchaseStatusResponse, ThirdPartyPurchaseStatusResponse.Builder>());
            // s_packetDecoders.Add(296, new DefaultProtobufPacketDecoder<SetProgressResponse, SetProgressResponse.Builder>());
            // s_packetDecoders.Add(299, new DefaultProtobufPacketDecoder<TriggerEventResponse, TriggerEventResponse.Builder>());
            // s_packetDecoders.Add(300, new DefaultProtobufPacketDecoder<MassiveLoginReply, MassiveLoginReply.Builder>());
        }

        private void readtcpdata()
        {
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
            PacketDevice selectedDevice = allDevices[2]; //3

            using (PacketCommunicator communicator =
                selectedDevice.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                communicator.SetFilter("port 1119 or port 3724");
                communicator.ReceivePackets(0, DispatcherHandler);
            }
        }

        // private void GetBattleNetPackets()
        // {
        //     GamesAPI.UtilResponse utilResponse;
        //     while ((utilResponse = BattleNet.NextUtilPacket()) != null)
        //     {
        //         bnet.protocol.Attribute attribute1 = utilResponse.m_response.AttributeList[0];
        //         bnet.protocol.Attribute attribute2 = utilResponse.m_response.AttributeList[1];
        //         int intValue = (int) attribute1.Value.IntValue;
        //         byte[] blobValue = attribute2.Value.BlobValue;
        //         int length = blobValue.Length;
        //         byte[] numArray = blobValue;
        //         this.m_connectApi.DecodeAndProcessPacket(new PegasusPacket(intValue, length, (object) numArray)
        //         {
        //             Context = utilResponse.m_context
        //         });
        //     }
        // }

        private void DispatcherHandler(Packet packet)
        {
            int le = packet.Ethernet.IpV4.Tcp.Http.Length;
            byte[] d = packet.Buffer.Reverse().Take(le).Reverse().ToArray();


            PegasusPacket x = Activator.CreateInstance<PegasusPacket>();

            // try
            // {
            //     x.Decode(d, 0, d.Length);
            // }
            // catch
            // {
            //     int a = 0;
            //     var b = a;
            // }
            bool decoded = false;
            try
            {
                // Debug.WriteLine(d.Length.ToString() + " " + le.ToString() + " " + x.Decode(d, 0, d.Length).ToString());
                x.Decode(d, 0, d.Length);
                // Debug.WriteLine(x.ToString() + " " + le.ToString() + " " + d.Length.ToString() + " " + x.Type.ToString());
                decoded = true;
            }
            catch
            {
            }

            if (decoded && x.Type == 19)
            {
                if (s_packetDecoders.CanDecodePacket(x.Type))
                {
                    PegasusPacket item = s_packetDecoders.DecodePacket(x);

                    var bod = item.Body;
                    var typ = item.Type;

                    if (typ == 19)
                    {
                        PowerHistory history = (PowerHistory) bod;
                        IList<PowerHistoryData> listy = history.List;
                        // Debug.WriteLine(listy.Count);
                        // Debug.WriteLine("\n\n");
                        
                        foreach (var phd in listy)
                        {
                            
                            if (phd.HasShowEntity)
                            {
                                try
                                {
                                    entity_dictionary.Add(phd.ShowEntity.Entity, phd.ShowEntity.Name);
                                    Debug.WriteLine(phd.ShowEntity.Entity.ToString() + " " + phd.ShowEntity.Name);
                                }
                                catch
                                {
                                }
                            }


                            // Debug.WriteLine("");
                            // Debug.WriteLine($"shwEntity = {phd.HasShowEntity}, powerStat={phd.HasPowerStart}" +
                            //                 $", {phd.}");
                            if (phd.HasPowerStart)
                            {
                                Debug.WriteLine(phd.PowerStart.Type.ToString());
                                // foreach (var prop in phd.GetType().GetProperties())
                                // {
                                //     Debug.Write($"{prop.Name}={prop.GetValue(phd, null)}, ");
                                // }


                                if (phd.PowerStart.Type.ToString() == "PLAY")
                                {
                                    if (entity_dictionary.TryGetValue(phd.PowerStart.Source, out cardid))
                                    {
                                        // string cardname = CardList.List.Find(r => r.ID == cardid).Name;
                                        // Debug.WriteLine(cardname);
                                        // choose_row(cardname);
                                    }
                                    // Debug.WriteLine($"ShowEntity = {phd.ShowEntity.Name}");
                                   
                                }
                            }
                        }
                    }

                    else
                    {
                        //Console.WriteLine(x.Body.ToString());
                    }
                }
            }
        }
    }
}