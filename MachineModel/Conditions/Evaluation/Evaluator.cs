using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Conditions.Evaluation
{
    public static class Evaluator
    {
        /// <summary>
        /// Evaluates any <see cref="ICondition"/> dependent on its type
        /// </summary>
        /// <param name="condition">The <see cref="ICondition"/> to evaluate</param>
        /// <returns></returns>
        public static bool Evaluate(ICondition condition)
        {
            if (condition is SimpleCondition sc)
            {
                return EvaluateSimpleCondition(sc);
            }
            else if (condition is ComplexCondition cc)
            {
                return EvaluateComplexCondition(cc);
            }

            Logger.LogToList($"Condition {condition.Value} is neither of simple nor complex and could not be evaluated ");

            return false;
        }

        /// <summary>
        /// Evaluates a <see cref="SimpleCondition"/>
        /// </summary>
        /// <param name="condition">The <see cref="SimpleCondition"/> to evaluate</param>
        /// <returns></returns>
        private static bool EvaluateSimpleCondition(SimpleCondition condition)
        {
            // get mapped state of condition
            var mapID = $"{condition.Resource}|{condition.State}";
            var mapState = Globals.Mapping[mapID];
            var result = false;

            // evaluate if boolean
            if (mapState is bool bState)
            {
                try
                {   // evaluation depends on operator
                    switch (condition.CompareOperator)
                    {
                        case "==": result = bState == Convert.ToBoolean(condition.StateValue); break;
                        case "!=": result = bState != Convert.ToBoolean(condition.StateValue); break;
                        default:
                            {
                                Logger.LogToList($"The SimpleCondition ({mapID}) thats StateValue ({bState}) is to be set as boolean has" +
                                    $" an invalid compare operator ({condition.CompareOperator}) -> boolean only supports == and != ");
                                break;
                            }
                    }
                }
                catch (FormatException fex)
                {
                    Logger.LogToList($"The SimpleCondition ({mapID}) thats StateValue ({bState}) is o be set as boolean has" +
                                $" an invalid ComparingStateValue ({condition.StateValue}).");
                    Logger.LogToList(fex.Message);
                }
            }
            // evaluate if double
            else if (mapState is double dState)
            {
                try
                {   // evaluation depends on operator
                    switch (condition.CompareOperator)
                    {
                        case "==": result = dState == Convert.ToDouble(condition.StateValue); break;
                        case "!=": result = dState != Convert.ToDouble(condition.StateValue); break;
                        case "<=": result = dState <= Convert.ToDouble(condition.StateValue); break;
                        case ">=": result = dState >= Convert.ToDouble(condition.StateValue); break;
                        case "<": result = dState < Convert.ToDouble(condition.StateValue); break;
                        case ">": result = dState > Convert.ToDouble(condition.StateValue); break;
                    }
                }
                catch (FormatException fex)
                {
                    Logger.LogToList($"The SimpleCondition ({mapID}) thats StateValue ({dState}) is to be set as double has" +
                                $" an invalid ComparingStateValue ({condition.StateValue}).");
                    Logger.LogToList(fex.Message);
                }

            }

            return result;
        }

        /// <summary>
        /// Evaluates a <see cref="ComplexCondition"/>
        /// </summary>
        /// <param name="condition">The <see cref="ComplexCondition"/> to evaluate</param>
        /// <returns></returns>
        private static bool EvaluateComplexCondition(ComplexCondition condition)
        {
            // evalute children
            var evaluationResults = new List<bool>();
            foreach (var child in condition.Children)
            {
                evaluationResults.Add(Evaluate(child));
            }

            // initial result is first child evaluation
            var result = evaluationResults.First();

            // continue with other child evaluations according to logic operators between them
            for (int i = 0; i < condition.LogicOperators.Count; i++)
            {
                switch (condition.LogicOperators[i])
                {
                    case "AND": result &= evaluationResults[i + 1]; break;
                    case "OR": result |= evaluationResults[i + 1]; break;
                    case "XOR": result ^= evaluationResults[i + 1]; break;
                }
            }

            return result;
        }



    }
}
