using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hearthstone.UI.Scripting
{
	public class ScriptContext
	{
		public enum ErrorCodes
		{
			Success,
			ParseError,
			EvaluationError,
			InternalError
		}

		public class SuggestionInfo
		{
			public enum Types
			{
				Unknown,
				Keyword,
				Model,
				Property
			}

			public string Identifier;

			public Types CandidateType;

			public Type ValueType;

			public int Weight;

			public string Description;

			public bool IsHinted;
		}

		public class FailedNodeInfo
		{
			private ScriptSyntaxTreeNode m_evaluatedNode;

			private ScriptSyntaxTreeNode m_invalidNode;

			public ScriptSyntaxTreeNode EvaluatedNode => m_evaluatedNode;

			public ScriptSyntaxTreeNode InvalidNode => m_invalidNode;

			public FailedNodeInfo()
			{
			}

			public FailedNodeInfo(ScriptSyntaxTreeNode evaluatedNode, ScriptSyntaxTreeNode invalidNode)
			{
				m_evaluatedNode = evaluatedNode;
				m_invalidNode = invalidNode;
			}
		}

		public struct EvaluationResults
		{
			private FailedNodeInfo m_failedNodeInfo;

			public object Value;

			public bool EventRaised;

			public ErrorCodes ErrorCode;

			public string ErrorMessage;

			public ScriptSyntaxTreeNode LastNode;

			public ScriptSyntaxTree.ParseResults ParseResults;

			public List<SuggestionInfo> Suggestions;

			public FailedNodeInfo FailedNodeInfo => m_failedNodeInfo;

			public bool SetFailedNodeIfNoneExists(ScriptSyntaxTreeNode evaluatedNode, ScriptSyntaxTreeNode invalidNode)
			{
				if (m_failedNodeInfo == null)
				{
					m_failedNodeInfo = new FailedNodeInfo(evaluatedNode, invalidNode);
					return true;
				}
				return false;
			}
		}

		public enum EncodingPolicy
		{
			None,
			Numerical,
			HumanReadable
		}

		public class EvaluationContext
		{
			public IDataModelProvider DataModelProvider;

			public string Script;

			public EvaluationResults Results;

			public ScriptSyntaxTree SyntaxTree;

			public EncodingPolicy EncodingPolicy;

			public StringBuilder EncodedScript;

			public List<object> QueryObjects;

			public Func<Type, IDataModel> DataModelDefaultConstructor;

			public Func<IEnumerable<IDataModel>> EditModeDataModelProvider;

			public Map<ScriptSyntaxTreeNode, int> CachedNodeValues;

			public ScriptFeatureFlags SupportedFeatures = ScriptFeatureFlags.All;

			public bool SuggestionsEnabled;

			public bool EditMode;

			public IDataModel GetDataModelById(int id)
			{
				if (EditMode)
				{
					return CreateDataModelById(id);
				}
				if (DataModelProvider != null && DataModelProvider.GetDataModel(id, out var model))
				{
					return model;
				}
				return null;
			}

			public IDataModel GetDataModelByDisplayName(string displayName)
			{
				if (EditMode)
				{
					return CreateDataModelByName(displayName);
				}
				if (GetDataModelByDisplayName(DataModelProvider, displayName, out var outDataModel))
				{
					return outDataModel;
				}
				if (GetDataModelByDisplayName(GlobalDataContext.Get(), displayName, out outDataModel))
				{
					return outDataModel;
				}
				return null;
			}

			private IDataModel CreateDataModel(Func<IDataModel, bool> predicate)
			{
				IDataModel dataModel;
				if (EditModeDataModelProvider != null && DataModelDefaultConstructor != null && (dataModel = EditModeDataModelProvider().FirstOrDefault(predicate)) != null)
				{
					return DataModelDefaultConstructor(dataModel.GetType());
				}
				return null;
			}

			private IDataModel CreateDataModelByName(string name)
			{
				return CreateDataModel((IDataModel a) => a.DataModelDisplayName == name);
			}

			private IDataModel CreateDataModelById(int id)
			{
				return CreateDataModel((IDataModel a) => a.DataModelId == id);
			}

			private static bool GetDataModelByDisplayName(IDataModelProvider dataModelProvider, string displayName, out IDataModel outDataModel)
			{
				if (dataModelProvider == null)
				{
					outDataModel = null;
					return false;
				}
				foreach (IDataModel dataModel in dataModelProvider.GetDataModels())
				{
					if (dataModel.DataModelDisplayName == displayName)
					{
						outDataModel = dataModel;
						return true;
					}
				}
				outDataModel = null;
				return false;
			}

			public void EmitError(ErrorCodes errorCode, string errorMessage, params object[] args)
			{
				if (Results.ErrorMessage == null)
				{
					Results.ErrorCode = errorCode;
					Results.ErrorMessage = (EditMode ? string.Format(errorMessage, args) : null);
					Results.Value = null;
				}
			}

			public bool CheckFeatureIsSupported(ScriptFeatureFlags feature)
			{
				if ((feature & SupportedFeatures) == 0)
				{
					EmitError(ErrorCodes.EvaluationError, "Unsupported feature: {0}", feature);
					return false;
				}
				return true;
			}

			[Conditional("UNITY_EDITOR")]
			public void AppendToEncodedScript(ScriptSyntaxTreeNode node, string encodedNode, bool autoSpacing, int offset = 0)
			{
				if (EncodedScript == null || node == null)
				{
					return;
				}
				EncodedScript.Append(encodedNode);
				if (node.Tokens == null || node.Tokens.Length == 0)
				{
					return;
				}
				ScriptToken scriptToken = node.Tokens[node.Tokens.Length - 1];
				for (int i = scriptToken.EndIndex; i < Script.Length; i++)
				{
					char c = Script[i];
					if (c == '\n')
					{
						EncodedScript.Append("\n");
						break;
					}
					if (i > scriptToken.EndIndex && c != ' ')
					{
						break;
					}
				}
				int num = scriptToken.Index + 1 + offset;
				if (num < SyntaxTree.Root.Tokens.Length)
				{
					ScriptToken.TokenType type = SyntaxTree.Root.Tokens[num].Type;
					if (type != ScriptToken.TokenType.Period && (uint)(type - 20) > 6u && autoSpacing)
					{
						EncodedScript.Append(' ');
					}
				}
			}

			[Conditional("UNITY_EDITOR")]
			public void EmitSuggestion(SuggestionInfo suggestion)
			{
				if (SuggestionsEnabled)
				{
					if (Results.Suggestions == null)
					{
						Results.Suggestions = new List<SuggestionInfo>();
					}
					Results.Suggestions.Add(suggestion);
				}
			}
		}

		private EvaluationContext m_evaluationContext = new EvaluationContext();

		public EvaluationResults Results => m_evaluationContext.Results;

		public ScriptFeatureFlags SupportedFeatures
		{
			get
			{
				return m_evaluationContext.SupportedFeatures;
			}
			set
			{
				m_evaluationContext.SupportedFeatures = value;
			}
		}

		public bool SuggestionsEnabled
		{
			get
			{
				return m_evaluationContext.SuggestionsEnabled;
			}
			set
			{
				m_evaluationContext.SuggestionsEnabled = value;
			}
		}

		public EvaluationResults Encode(string script, IDataModelProvider dataModelProvider, EncodingPolicy encodingPolicy, out string encodedString)
		{
			m_evaluationContext.EncodingPolicy = encodingPolicy;
			EvaluationResults result = Evaluate(script, dataModelProvider);
			encodedString = ((m_evaluationContext.EncodedScript != null) ? m_evaluationContext.EncodedScript.ToString() : script);
			return result;
		}

		public EvaluationResults Evaluate(string script, IDataModelProvider dataModelProvider)
		{
			m_evaluationContext.Script = script;
			m_evaluationContext.Results = default(EvaluationResults);
			m_evaluationContext.Results.ErrorCode = ErrorCodes.Success;
			m_evaluationContext.DataModelProvider = dataModelProvider;
			if (m_evaluationContext.QueryObjects != null)
			{
				m_evaluationContext.QueryObjects.Clear();
			}
			try
			{
				m_evaluationContext.SyntaxTree = ScriptSyntaxTree.Get(m_evaluationContext.Script);
				m_evaluationContext.Results.ParseResults = m_evaluationContext.SyntaxTree.Results;
				if (m_evaluationContext.Results.ParseResults.ErrorCode != 0)
				{
					m_evaluationContext.EmitError(ErrorCodes.ParseError, m_evaluationContext.EditMode ? ("Parsing error: " + m_evaluationContext.Results.ParseResults.ErrorMessage) : null);
					if (m_evaluationContext.EncodingPolicy == EncodingPolicy.None && !SuggestionsEnabled)
					{
						return m_evaluationContext.Results;
					}
				}
			}
			catch (Exception ex)
			{
				m_evaluationContext.EmitError(ErrorCodes.InternalError, m_evaluationContext.EditMode ? ("Failed to parse script: " + ex) : null);
				return m_evaluationContext.Results;
			}
			try
			{
				m_evaluationContext.EncodedScript = ((m_evaluationContext.EncodingPolicy == EncodingPolicy.None) ? null : new StringBuilder());
				if (m_evaluationContext.SyntaxTree.Root != null)
				{
					m_evaluationContext.SyntaxTree.Root.Evaluate(m_evaluationContext, out m_evaluationContext.Results.Value);
				}
			}
			catch (Exception ex2)
			{
				m_evaluationContext.EmitError(ErrorCodes.InternalError, m_evaluationContext.EditMode ? ("Failed to evaluate script: " + ex2) : null);
			}
			return m_evaluationContext.Results;
		}

		protected void EnableEditMode(Func<IEnumerable<IDataModel>> dataModelProvider, Func<Type, IDataModel> dataModelDefaultConstructor)
		{
			m_evaluationContext.EditMode = true;
			m_evaluationContext.EditModeDataModelProvider = dataModelProvider;
			m_evaluationContext.DataModelDefaultConstructor = dataModelDefaultConstructor;
		}
	}
}
