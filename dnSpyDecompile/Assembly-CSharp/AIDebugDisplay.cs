using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using PegasusGame;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class AIDebugDisplay : MonoBehaviour
{
	// Token: 0x0600219D RID: 8605 RVA: 0x000A4877 File Offset: 0x000A2A77
	public static AIDebugDisplay Get()
	{
		if (AIDebugDisplay.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			AIDebugDisplay.s_instance = gameObject.AddComponent<AIDebugDisplay>();
			gameObject.name = "AIDebugDisplay (Dynamically created)";
		}
		return AIDebugDisplay.s_instance;
	}

	// Token: 0x0600219E RID: 8606 RVA: 0x000A48A5 File Offset: 0x000A2AA5
	private void Start()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterCreateGameListener(new GameState.CreateGameCallback(this.GameState_CreateGameEvent), null);
		}
	}

	// Token: 0x0600219F RID: 8607 RVA: 0x000A48CE File Offset: 0x000A2ACE
	private void GameState_CreateGameEvent(GameState.CreateGamePhase createGamePhase, object userData)
	{
		this.m_debugInformation.Clear();
	}

	// Token: 0x060021A0 RID: 8608 RVA: 0x000A48DB File Offset: 0x000A2ADB
	public bool ToggleDebugDisplay(string func, string[] args, string rawArgs)
	{
		this.m_isDisplayed = !this.m_isDisplayed;
		return true;
	}

	// Token: 0x060021A1 RID: 8609 RVA: 0x000A48F0 File Offset: 0x000A2AF0
	private void Update()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return;
		}
		if (!this.m_isDisplayed)
		{
			return;
		}
		AIDebugInformation aidebugInformation = null;
		if (!gameState.IsFriendlySidePlayerTurn())
		{
			int currentTurn = gameState.GetGameEntity().GetTag(GAME_TAG.TURN);
			int moveID = gameState.GetOpposingSidePlayer().GetTag(GAME_TAG.NUM_OPTIONS_PLAYED_THIS_TURN) + 1;
			List<List<AIDebugInformation>> list = this.m_debugInformation.Find((List<List<AIDebugInformation>> x) => x.Count > 0 && x[0].Count > 0 && x[0][0].MoveID == moveID && x[0][0].TurnID == currentTurn);
			if (list != null)
			{
				List<AIDebugInformation> list2 = list[list.Count - 1];
				if (list2[0].DebugIteration == 0)
				{
					aidebugInformation = list2[0];
				}
			}
			this.m_currentHistoryScrollBarValue = 1f;
			this.m_currentIterationScrollBarValue = 1f;
			this.m_currentDepthScrollBarValue = 0f;
			this.m_showIterationScrollBar = (this.m_showDepthScrollBar = (list != null && list.Count > 1));
		}
		else if (this.m_debugInformation.Count > 0)
		{
			int num = (int)(this.m_currentHistoryScrollBarValue * (float)this.m_debugInformation.Count);
			if (num >= this.m_debugInformation.Count)
			{
				num = this.m_debugInformation.Count - 1;
			}
			List<List<AIDebugInformation>> list3 = this.m_debugInformation[num];
			this.m_showIterationScrollBar = (this.m_showDepthScrollBar = (list3 != null && list3.Count > 1));
			int num2 = (int)(this.m_currentIterationScrollBarValue * (float)list3.Count);
			if (num2 >= list3.Count)
			{
				num2 = list3.Count - 1;
			}
			List<AIDebugInformation> list4 = list3[num2];
			int num3 = (int)(this.m_currentDepthScrollBarValue * (float)list4.Count);
			if (num3 >= list4.Count)
			{
				num3 = list4.Count - 1;
			}
			aidebugInformation = list4[num3];
		}
		if (aidebugInformation == null && this.m_debugInformation.Count > 0)
		{
			List<List<AIDebugInformation>> list5 = this.m_debugInformation.FindLast((List<List<AIDebugInformation>> x) => x.Count > 0 && x[x.Count - 1].Count > 0 && x[x.Count - 1][0].DebugIteration == 0);
			if (list5 != null)
			{
				aidebugInformation = list5[list5.Count - 1][0];
				this.m_showIterationScrollBar = (this.m_showDepthScrollBar = (list5 != null && list5.Count > 1));
			}
		}
		this.UpdateDisplay(aidebugInformation);
	}

	// Token: 0x060021A2 RID: 8610 RVA: 0x000A4B3B File Offset: 0x000A2D3B
	private string AppendLine(string inputString, string stringToAppend)
	{
		return string.Format("{0}\n{1}", inputString, stringToAppend);
	}

	// Token: 0x060021A3 RID: 8611 RVA: 0x000A4B4C File Offset: 0x000A2D4C
	private string FormatOptionName(AIEvaluation evaluation)
	{
		string text;
		if (evaluation.OptionChosen)
		{
			text = string.Format("AI CHOSE: {0} (ID{1})", evaluation.OptionName, evaluation.EntityID);
		}
		else
		{
			text = string.Format("{0} (ID{1})", evaluation.OptionName, evaluation.EntityID);
		}
		if (evaluation.TargetScores.Count >= 1)
		{
			AITarget aitarget = evaluation.TargetScores.Find((AITarget x) => x.TargetChosen);
			if (aitarget != null)
			{
				text = string.Format("{0} targeting {1} (ID{2})", text, aitarget.EntityName, aitarget.EntityID);
			}
		}
		return text;
	}

	// Token: 0x060021A4 RID: 8612 RVA: 0x000A4BFC File Offset: 0x000A2DFC
	private void OnGUI()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		if (GameState.Get() == null)
		{
			return;
		}
		if (!this.m_isDisplayed)
		{
			return;
		}
		int num = 25;
		int num2 = 15;
		int num3 = 10;
		int num4 = Screen.height - 100;
		if (this.m_showIterationScrollBar)
		{
			num4 -= num + num2 + num3;
		}
		if (this.m_showDepthScrollBar)
		{
			num4 -= num + num2 + num3;
		}
		GUI.Label(new Rect(5f, (float)num4, 200f, (float)num), "AI Debug History");
		float num5 = GUI.HorizontalSlider(new Rect(5f, (float)(num4 + num), 400f, (float)num2), this.m_currentHistoryScrollBarValue, 0f, 1f);
		num4 += num + num2 + num3;
		if (num5 != this.m_currentHistoryScrollBarValue)
		{
			this.m_currentHistoryScrollBarValue = num5;
			this.m_currentIterationScrollBarValue = 1f;
			this.m_currentDepthScrollBarValue = 0f;
		}
		if (this.m_showIterationScrollBar)
		{
			GUI.Label(new Rect(5f, (float)num4, 200f, (float)num), "Tree Search Iteration");
			this.m_currentIterationScrollBarValue = GUI.HorizontalSlider(new Rect(5f, (float)(num4 + num), 400f, (float)num2), this.m_currentIterationScrollBarValue, 0f, 1f);
			num4 += num + num2 + num3;
		}
		if (this.m_showDepthScrollBar)
		{
			GUI.Label(new Rect(5f, (float)num4, 200f, (float)num), "Tree Search Depth");
			this.m_currentDepthScrollBarValue = GUI.HorizontalSlider(new Rect(5f, (float)(num4 + num), 400f, (float)num2), this.m_currentDepthScrollBarValue, 0f, 1f);
		}
	}

	// Token: 0x060021A5 RID: 8613 RVA: 0x000A4D80 File Offset: 0x000A2F80
	private int GetOverallScore(AIEvaluation evaluation)
	{
		return evaluation.BaseScore + evaluation.BonusScore + evaluation.ContextualScore.Sum((AIContextualValue x) => x.ContextualScore) + evaluation.EdgeCount;
	}

	// Token: 0x060021A6 RID: 8614 RVA: 0x000A4DCC File Offset: 0x000A2FCC
	private void UpdateDisplay(AIDebugInformation debugInfo)
	{
		string text = "";
		Vector3 position = new Vector3((float)Screen.width, (float)Screen.height, 0f);
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null)
		{
			text = string.Format("Uuid: {0}\n", GameState.Get().GetGameEntity().m_uuid);
		}
		if (debugInfo == null)
		{
			DebugTextManager.Get().DrawDebugText(text, position, 0f, true, "", null);
			return;
		}
		if (debugInfo.ModelVersion != 0L)
		{
			text += string.Format("Model Version: {0}\n", debugInfo.ModelVersion);
		}
		text += string.Format("AI Debug Turn {0}, Move {1}", debugInfo.TurnID, debugInfo.MoveID);
		string text2 = "";
		if (debugInfo.DebugIteration != 0)
		{
			text2 = text2 + "Iteration: " + debugInfo.DebugIteration;
		}
		if (debugInfo.DebugDepth != 0)
		{
			text2 = text2 + " Depth: " + debugInfo.DebugDepth;
		}
		if (!string.IsNullOrEmpty(text2))
		{
			text = this.AppendLine(text, text2);
		}
		string text3 = "";
		if (debugInfo.InferenceValue != 0f)
		{
			text3 = text3 + "Inference: " + debugInfo.InferenceValue.ToString(".000");
		}
		if (debugInfo.HeuristicValue != 0f)
		{
			text3 = text3 + " Heuristic: " + debugInfo.HeuristicValue.ToString(".000");
		}
		if (debugInfo.SubtreeValue != 0f)
		{
			text3 = text3 + " Subtree: " + debugInfo.SubtreeValue.ToString(".000");
		}
		if (!string.IsNullOrEmpty(text3))
		{
			text = this.AppendLine(text, text3);
		}
		if (debugInfo.TotalVisits > 0)
		{
			text = this.AppendLine(text, "Total Visits: " + debugInfo.TotalVisits);
		}
		if (debugInfo.UniqueNodes > 0)
		{
			text = this.AppendLine(text, "Unique Nodes: " + debugInfo.UniqueNodes);
		}
		if (debugInfo.SubtreeDepth > 0)
		{
			text = this.AppendLine(text, "SubTree Depth: " + debugInfo.SubtreeDepth);
		}
		List<AIEvaluation> list = new List<AIEvaluation>();
		list.AddRange(debugInfo.Evaluations);
		debugInfo.Evaluations = debugInfo.Evaluations.OrderByDescending(delegate(AIEvaluation x)
		{
			if (x.OptionChosen)
			{
				return 9999999;
			}
			return this.GetOverallScore(x);
		}).ToList<AIEvaluation>();
		foreach (AIEvaluation aievaluation in list)
		{
			text = this.AppendLine(text, "---");
			text = this.AppendLine(text, this.FormatOptionName(aievaluation));
			if (aievaluation.BaseScore > 0)
			{
				text = this.AppendLine(text, "Total Option Score: " + this.GetOverallScore(aievaluation));
			}
			int num = 0;
			foreach (AIContextualValue aicontextualValue in aievaluation.ContextualScore)
			{
				num += aicontextualValue.ContextualScore;
			}
			if (aievaluation.BonusScore != 0 || num != 0)
			{
				text = this.AppendLine(text, "Base Score: " + aievaluation.BaseScore);
			}
			if (aievaluation.BonusScore != 0)
			{
				text = this.AppendLine(text, "Bonus Score: " + aievaluation.BonusScore);
			}
			if (aievaluation.ContextualScore.Count > 0)
			{
				text = this.AppendLine(text, "Contextual Score from: ");
				foreach (AIContextualValue aicontextualValue2 in aievaluation.ContextualScore)
				{
					text = this.AppendLine(text, string.Format("{0} (ID{1}): {2}", aicontextualValue2.EntityName, aicontextualValue2.EntityID, aicontextualValue2.ContextualScore));
				}
			}
			if (aievaluation.PriorProbability != 0f)
			{
				text = this.AppendLine(text, "Prior Probability: " + aievaluation.PriorProbability.ToString(".000"));
			}
			if (aievaluation.PuctValue != 0f && debugInfo.TotalVisits > 1)
			{
				text = this.AppendLine(text, "Puct Value: " + aievaluation.PuctValue.ToString(".000"));
			}
			if (aievaluation.FinalVisitCount > 0)
			{
				text = this.AppendLine(text, string.Concat(new object[]
				{
					"Visit Count: ",
					aievaluation.FinalVisitCount,
					" (",
					aievaluation.EdgeCount,
					")"
				}));
			}
			if (aievaluation.SubtreeDepth > 0)
			{
				text = this.AppendLine(text, "Subtree Depth: " + aievaluation.SubtreeDepth);
			}
			if (aievaluation.FinalQValue != 0f)
			{
				text = this.AppendLine(text, "Q Value: " + aievaluation.FinalQValue.ToString(".000"));
			}
			string text4 = "";
			if (aievaluation.InferenceValue != 0f)
			{
				text4 = text4 + "Inference: " + aievaluation.InferenceValue.ToString(".000");
			}
			if (aievaluation.HeuristicValue != 0f)
			{
				text4 = text4 + " Heuristic: " + aievaluation.HeuristicValue.ToString(".000");
			}
			if (aievaluation.SubtreeValue != 0f)
			{
				text4 = text4 + " Subtree: " + aievaluation.SubtreeValue.ToString(".000");
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text = this.AppendLine(text, text4);
			}
			if (aievaluation.TargetScores.Count >= 1)
			{
				text = this.AppendLine(text, "Target scores: ");
				foreach (AITarget aitarget in aievaluation.TargetScores)
				{
					if (aitarget.TargetScore > 0)
					{
						text = this.AppendLine(text, string.Format("{0} (ID{1}): {2}", aitarget.EntityName, aitarget.EntityID, aitarget.TargetScore));
					}
					else if (aitarget.PriorProbability > 0f)
					{
						string text5 = "";
						if (aitarget.InferenceValue != 0f)
						{
							text5 = text5 + ", Inf: " + aitarget.InferenceValue.ToString(".000");
						}
						if (aitarget.HeuristicValue != 0f)
						{
							text5 = text5 + ", Heur: " + aitarget.HeuristicValue.ToString(".000");
						}
						if (aitarget.SubtreeValue != 0f)
						{
							text5 = text5 + ", Sub: " + aitarget.SubtreeValue.ToString(".000");
						}
						string text6 = "";
						if (debugInfo.TotalVisits > 1)
						{
							text6 = string.Format(", PUCT {0:.000}, Visit {1} ({2}), Value {3:.000}{4}", new object[]
							{
								aitarget.PuctValue,
								aitarget.FinalVisitCount,
								aitarget.EdgeCount,
								aitarget.FinalQValue,
								text5
							});
						}
						text = this.AppendLine(text, string.Format("{0} (ID{1}): Prior {2:.000}{3}", new object[]
						{
							aitarget.EntityName,
							aitarget.EntityID,
							aitarget.PriorProbability,
							text6
						}));
					}
				}
			}
			if (aievaluation.PositionScores.Count >= 2)
			{
				text = this.AppendLine(text, "Position scores: ");
				foreach (AIPosition aiposition in aievaluation.PositionScores)
				{
					if (aiposition.PriorProbability > 0f)
					{
						string text7 = "";
						if (aiposition.InferenceValue != 0f)
						{
							text7 = text7 + ", Inf: " + aiposition.InferenceValue.ToString(".000");
						}
						if (aiposition.HeuristicValue != 0f)
						{
							text7 = text7 + ", Heur: " + aiposition.HeuristicValue.ToString(".000");
						}
						if (aiposition.SubtreeValue != 0f)
						{
							text7 = text7 + ", Sub: " + aiposition.SubtreeValue.ToString(".000");
						}
						text = this.AppendLine(text, string.Format("Pos {0}: Prior {1:.000}, PUCT {2:.000}, Visit {3} ({4}), Value {5:.000}{6}", new object[]
						{
							(aiposition.Position > 0) ? aiposition.Position : aievaluation.PositionScores.Count,
							aiposition.PriorProbability,
							aiposition.PuctValue,
							aiposition.FinalVisitCount,
							aiposition.EdgeCount,
							aiposition.FinalQValue,
							text7
						}));
					}
				}
			}
		}
		DebugTextManager.Get().DrawDebugText(text, position, 0f, true, "", null);
	}

	// Token: 0x060021A7 RID: 8615 RVA: 0x000A578C File Offset: 0x000A398C
	public void OnAIDebugInformation(AIDebugInformation debugInfo)
	{
		int num = this.m_debugInformation.FindIndex((List<List<AIDebugInformation>> x) => x.Count > 0 && x[0].Count > 0 && x[0][0].MoveID == debugInfo.MoveID && x[0][0].TurnID == debugInfo.TurnID);
		if (num == -1)
		{
			num = this.m_debugInformation.Count;
			this.m_debugInformation.Add(new List<List<AIDebugInformation>>());
		}
		int num2 = this.m_debugInformation[num].FindIndex((List<AIDebugInformation> x) => x.Count > 0 && x[0].DebugIteration == debugInfo.DebugIteration);
		if (num2 == -1)
		{
			num2 = this.m_debugInformation[num].Count;
			this.m_debugInformation[num].Add(new List<AIDebugInformation>());
		}
		int num3 = this.m_debugInformation[num][num2].FindIndex((AIDebugInformation x) => x.DebugDepth == debugInfo.DebugDepth);
		if (num3 == -1)
		{
			this.m_debugInformation[num][num2].Add(debugInfo);
			return;
		}
		this.m_debugInformation[num][num2][num3] = debugInfo;
	}

	// Token: 0x04001297 RID: 4759
	private static AIDebugDisplay s_instance;

	// Token: 0x04001298 RID: 4760
	private List<List<List<AIDebugInformation>>> m_debugInformation = new List<List<List<AIDebugInformation>>>();

	// Token: 0x04001299 RID: 4761
	public bool m_isDisplayed;

	// Token: 0x0400129A RID: 4762
	private float m_currentHistoryScrollBarValue = 1f;

	// Token: 0x0400129B RID: 4763
	private float m_currentIterationScrollBarValue = 1f;

	// Token: 0x0400129C RID: 4764
	private float m_currentDepthScrollBarValue;

	// Token: 0x0400129D RID: 4765
	private bool m_showIterationScrollBar;

	// Token: 0x0400129E RID: 4766
	private bool m_showDepthScrollBar;
}
