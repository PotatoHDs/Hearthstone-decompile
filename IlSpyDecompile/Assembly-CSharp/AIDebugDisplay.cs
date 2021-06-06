using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using PegasusGame;
using UnityEngine;

public class AIDebugDisplay : MonoBehaviour
{
	private static AIDebugDisplay s_instance;

	private List<List<List<AIDebugInformation>>> m_debugInformation = new List<List<List<AIDebugInformation>>>();

	public bool m_isDisplayed;

	private float m_currentHistoryScrollBarValue = 1f;

	private float m_currentIterationScrollBarValue = 1f;

	private float m_currentDepthScrollBarValue;

	private bool m_showIterationScrollBar;

	private bool m_showDepthScrollBar;

	public static AIDebugDisplay Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<AIDebugDisplay>();
			obj.name = "AIDebugDisplay (Dynamically created)";
		}
		return s_instance;
	}

	private void Start()
	{
		if (!HearthstoneApplication.IsPublic() && GameState.Get() != null)
		{
			GameState.Get().RegisterCreateGameListener(GameState_CreateGameEvent, null);
		}
	}

	private void GameState_CreateGameEvent(GameState.CreateGamePhase createGamePhase, object userData)
	{
		m_debugInformation.Clear();
	}

	public bool ToggleDebugDisplay(string func, string[] args, string rawArgs)
	{
		m_isDisplayed = !m_isDisplayed;
		return true;
	}

	private void Update()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		GameState gameState = GameState.Get();
		if (gameState == null || !m_isDisplayed)
		{
			return;
		}
		AIDebugInformation aIDebugInformation = null;
		if (!gameState.IsFriendlySidePlayerTurn())
		{
			int currentTurn = gameState.GetGameEntity().GetTag(GAME_TAG.TURN);
			int moveID = gameState.GetOpposingSidePlayer().GetTag(GAME_TAG.NUM_OPTIONS_PLAYED_THIS_TURN) + 1;
			List<List<AIDebugInformation>> list = m_debugInformation.Find((List<List<AIDebugInformation>> x) => x.Count > 0 && x[0].Count > 0 && x[0][0].MoveID == moveID && x[0][0].TurnID == currentTurn);
			if (list != null)
			{
				List<AIDebugInformation> list2 = list[list.Count - 1];
				if (list2[0].DebugIteration == 0)
				{
					aIDebugInformation = list2[0];
				}
			}
			m_currentHistoryScrollBarValue = 1f;
			m_currentIterationScrollBarValue = 1f;
			m_currentDepthScrollBarValue = 0f;
			m_showIterationScrollBar = (m_showDepthScrollBar = list != null && list.Count > 1);
		}
		else if (m_debugInformation.Count > 0)
		{
			int num = (int)(m_currentHistoryScrollBarValue * (float)m_debugInformation.Count);
			if (num >= m_debugInformation.Count)
			{
				num = m_debugInformation.Count - 1;
			}
			List<List<AIDebugInformation>> list3 = m_debugInformation[num];
			m_showIterationScrollBar = (m_showDepthScrollBar = list3 != null && list3.Count > 1);
			int num2 = (int)(m_currentIterationScrollBarValue * (float)list3.Count);
			if (num2 >= list3.Count)
			{
				num2 = list3.Count - 1;
			}
			List<AIDebugInformation> list4 = list3[num2];
			int num3 = (int)(m_currentDepthScrollBarValue * (float)list4.Count);
			if (num3 >= list4.Count)
			{
				num3 = list4.Count - 1;
			}
			aIDebugInformation = list4[num3];
		}
		if (aIDebugInformation == null && m_debugInformation.Count > 0)
		{
			List<List<AIDebugInformation>> list5 = m_debugInformation.FindLast((List<List<AIDebugInformation>> x) => x.Count > 0 && x[x.Count - 1].Count > 0 && x[x.Count - 1][0].DebugIteration == 0);
			if (list5 != null)
			{
				aIDebugInformation = list5[list5.Count - 1][0];
				m_showIterationScrollBar = (m_showDepthScrollBar = list5 != null && list5.Count > 1);
			}
		}
		UpdateDisplay(aIDebugInformation);
	}

	private string AppendLine(string inputString, string stringToAppend)
	{
		return $"{inputString}\n{stringToAppend}";
	}

	private string FormatOptionName(AIEvaluation evaluation)
	{
		string text = "";
		text = ((!evaluation.OptionChosen) ? $"{evaluation.OptionName} (ID{evaluation.EntityID})" : $"AI CHOSE: {evaluation.OptionName} (ID{evaluation.EntityID})");
		if (evaluation.TargetScores.Count >= 1)
		{
			AITarget aITarget = evaluation.TargetScores.Find((AITarget x) => x.TargetChosen);
			if (aITarget != null)
			{
				text = $"{text} targeting {aITarget.EntityName} (ID{aITarget.EntityID})";
			}
		}
		return text;
	}

	private void OnGUI()
	{
		if (!HearthstoneApplication.IsPublic() && GameState.Get() != null && m_isDisplayed)
		{
			int num = 25;
			int num2 = 15;
			int num3 = 10;
			int num4 = Screen.height - 100;
			if (m_showIterationScrollBar)
			{
				num4 -= num + num2 + num3;
			}
			if (m_showDepthScrollBar)
			{
				num4 -= num + num2 + num3;
			}
			GUI.Label(new Rect(5f, num4, 200f, num), "AI Debug History");
			float num5 = GUI.HorizontalSlider(new Rect(5f, num4 + num, 400f, num2), m_currentHistoryScrollBarValue, 0f, 1f);
			num4 += num + num2 + num3;
			if (num5 != m_currentHistoryScrollBarValue)
			{
				m_currentHistoryScrollBarValue = num5;
				m_currentIterationScrollBarValue = 1f;
				m_currentDepthScrollBarValue = 0f;
			}
			if (m_showIterationScrollBar)
			{
				GUI.Label(new Rect(5f, num4, 200f, num), "Tree Search Iteration");
				m_currentIterationScrollBarValue = GUI.HorizontalSlider(new Rect(5f, num4 + num, 400f, num2), m_currentIterationScrollBarValue, 0f, 1f);
				num4 += num + num2 + num3;
			}
			if (m_showDepthScrollBar)
			{
				GUI.Label(new Rect(5f, num4, 200f, num), "Tree Search Depth");
				m_currentDepthScrollBarValue = GUI.HorizontalSlider(new Rect(5f, num4 + num, 400f, num2), m_currentDepthScrollBarValue, 0f, 1f);
			}
		}
	}

	private int GetOverallScore(AIEvaluation evaluation)
	{
		return evaluation.BaseScore + evaluation.BonusScore + evaluation.ContextualScore.Sum((AIContextualValue x) => x.ContextualScore) + evaluation.EdgeCount;
	}

	private void UpdateDisplay(AIDebugInformation debugInfo)
	{
		string text = "";
		Vector3 position = new Vector3(Screen.width, Screen.height, 0f);
		if (GameState.Get() != null && GameState.Get().GetGameEntity() != null)
		{
			text = $"Uuid: {GameState.Get().GetGameEntity().m_uuid}\n";
		}
		if (debugInfo == null)
		{
			DebugTextManager.Get().DrawDebugText(text, position, 0f, screenSpace: true);
			return;
		}
		if (debugInfo.ModelVersion != 0L)
		{
			text += $"Model Version: {debugInfo.ModelVersion}\n";
		}
		text += $"AI Debug Turn {debugInfo.TurnID}, Move {debugInfo.MoveID}";
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
			text = AppendLine(text, text2);
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
			text = AppendLine(text, text3);
		}
		if (debugInfo.TotalVisits > 0)
		{
			text = AppendLine(text, "Total Visits: " + debugInfo.TotalVisits);
		}
		if (debugInfo.UniqueNodes > 0)
		{
			text = AppendLine(text, "Unique Nodes: " + debugInfo.UniqueNodes);
		}
		if (debugInfo.SubtreeDepth > 0)
		{
			text = AppendLine(text, "SubTree Depth: " + debugInfo.SubtreeDepth);
		}
		List<AIEvaluation> list = new List<AIEvaluation>();
		list.AddRange(debugInfo.Evaluations);
		debugInfo.Evaluations = debugInfo.Evaluations.OrderByDescending((AIEvaluation x) => x.OptionChosen ? 9999999 : GetOverallScore(x)).ToList();
		foreach (AIEvaluation item in list)
		{
			text = AppendLine(text, "---");
			text = AppendLine(text, FormatOptionName(item));
			if (item.BaseScore > 0)
			{
				text = AppendLine(text, "Total Option Score: " + GetOverallScore(item));
			}
			int num = 0;
			foreach (AIContextualValue item2 in item.ContextualScore)
			{
				num += item2.ContextualScore;
			}
			if (item.BonusScore != 0 || num != 0)
			{
				text = AppendLine(text, "Base Score: " + item.BaseScore);
			}
			if (item.BonusScore != 0)
			{
				text = AppendLine(text, "Bonus Score: " + item.BonusScore);
			}
			if (item.ContextualScore.Count > 0)
			{
				text = AppendLine(text, "Contextual Score from: ");
				foreach (AIContextualValue item3 in item.ContextualScore)
				{
					text = AppendLine(text, $"{item3.EntityName} (ID{item3.EntityID}): {item3.ContextualScore}");
				}
			}
			if (item.PriorProbability != 0f)
			{
				text = AppendLine(text, "Prior Probability: " + item.PriorProbability.ToString(".000"));
			}
			if (item.PuctValue != 0f && debugInfo.TotalVisits > 1)
			{
				text = AppendLine(text, "Puct Value: " + item.PuctValue.ToString(".000"));
			}
			if (item.FinalVisitCount > 0)
			{
				text = AppendLine(text, "Visit Count: " + item.FinalVisitCount + " (" + item.EdgeCount + ")");
			}
			if (item.SubtreeDepth > 0)
			{
				text = AppendLine(text, "Subtree Depth: " + item.SubtreeDepth);
			}
			if (item.FinalQValue != 0f)
			{
				text = AppendLine(text, "Q Value: " + item.FinalQValue.ToString(".000"));
			}
			string text4 = "";
			if (item.InferenceValue != 0f)
			{
				text4 = text4 + "Inference: " + item.InferenceValue.ToString(".000");
			}
			if (item.HeuristicValue != 0f)
			{
				text4 = text4 + " Heuristic: " + item.HeuristicValue.ToString(".000");
			}
			if (item.SubtreeValue != 0f)
			{
				text4 = text4 + " Subtree: " + item.SubtreeValue.ToString(".000");
			}
			if (!string.IsNullOrEmpty(text4))
			{
				text = AppendLine(text, text4);
			}
			if (item.TargetScores.Count >= 1)
			{
				text = AppendLine(text, "Target scores: ");
				foreach (AITarget targetScore in item.TargetScores)
				{
					if (targetScore.TargetScore > 0)
					{
						text = AppendLine(text, $"{targetScore.EntityName} (ID{targetScore.EntityID}): {targetScore.TargetScore}");
					}
					else if (targetScore.PriorProbability > 0f)
					{
						string text5 = "";
						if (targetScore.InferenceValue != 0f)
						{
							text5 = text5 + ", Inf: " + targetScore.InferenceValue.ToString(".000");
						}
						if (targetScore.HeuristicValue != 0f)
						{
							text5 = text5 + ", Heur: " + targetScore.HeuristicValue.ToString(".000");
						}
						if (targetScore.SubtreeValue != 0f)
						{
							text5 = text5 + ", Sub: " + targetScore.SubtreeValue.ToString(".000");
						}
						string text6 = "";
						if (debugInfo.TotalVisits > 1)
						{
							text6 = $", PUCT {targetScore.PuctValue:.000}, Visit {targetScore.FinalVisitCount} ({targetScore.EdgeCount}), Value {targetScore.FinalQValue:.000}{text5}";
						}
						text = AppendLine(text, $"{targetScore.EntityName} (ID{targetScore.EntityID}): Prior {targetScore.PriorProbability:.000}{text6}");
					}
				}
			}
			if (item.PositionScores.Count < 2)
			{
				continue;
			}
			text = AppendLine(text, "Position scores: ");
			foreach (AIPosition positionScore in item.PositionScores)
			{
				if (positionScore.PriorProbability > 0f)
				{
					string text7 = "";
					if (positionScore.InferenceValue != 0f)
					{
						text7 = text7 + ", Inf: " + positionScore.InferenceValue.ToString(".000");
					}
					if (positionScore.HeuristicValue != 0f)
					{
						text7 = text7 + ", Heur: " + positionScore.HeuristicValue.ToString(".000");
					}
					if (positionScore.SubtreeValue != 0f)
					{
						text7 = text7 + ", Sub: " + positionScore.SubtreeValue.ToString(".000");
					}
					text = AppendLine(text, $"Pos {((positionScore.Position > 0) ? positionScore.Position : item.PositionScores.Count)}: Prior {positionScore.PriorProbability:.000}, PUCT {positionScore.PuctValue:.000}, Visit {positionScore.FinalVisitCount} ({positionScore.EdgeCount}), Value {positionScore.FinalQValue:.000}{text7}");
				}
			}
		}
		DebugTextManager.Get().DrawDebugText(text, position, 0f, screenSpace: true);
	}

	public void OnAIDebugInformation(AIDebugInformation debugInfo)
	{
		int num = m_debugInformation.FindIndex((List<List<AIDebugInformation>> x) => x.Count > 0 && x[0].Count > 0 && x[0][0].MoveID == debugInfo.MoveID && x[0][0].TurnID == debugInfo.TurnID);
		if (num == -1)
		{
			num = m_debugInformation.Count;
			m_debugInformation.Add(new List<List<AIDebugInformation>>());
		}
		int num2 = m_debugInformation[num].FindIndex((List<AIDebugInformation> x) => x.Count > 0 && x[0].DebugIteration == debugInfo.DebugIteration);
		if (num2 == -1)
		{
			num2 = m_debugInformation[num].Count;
			m_debugInformation[num].Add(new List<AIDebugInformation>());
		}
		int num3 = m_debugInformation[num][num2].FindIndex((AIDebugInformation x) => x.DebugDepth == debugInfo.DebugDepth);
		if (num3 == -1)
		{
			m_debugInformation[num][num2].Add(debugInfo);
		}
		else
		{
			m_debugInformation[num][num2][num3] = debugInfo;
		}
	}
}
