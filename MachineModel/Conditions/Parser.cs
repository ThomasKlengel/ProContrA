using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.Conditions
{
    /// <summary>
    /// A class that can parse a string into a <see cref="ICondition"/>.
    /// Also contains static methods for condition strings.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Creates a new <see cref="ComplexCondition"/> or <see cref="SimpleCondition"/>
        /// </summary>
        /// <param name="conditionString">The input string to create the condition from</param>
        /// <returns>A <see cref="ComplexCondition"/> or <see cref="SimpleCondition"/> </returns>
        public static ICondition CreateCondidion(string conditionString)
        {
            // replace multiple whitespaces with a single whitespace
            conditionString = Regex.Replace(conditionString, @"\s+", " ");
            conditionString = StandardizeCondition(conditionString);            

            // get child strings and logic operators
            var childTextList = ExtractChildConditions(conditionString);
            var withoutChildren = conditionString;
            childTextList.OrderByDescending(child => child.Length).ToList().ForEach(m => withoutChildren = withoutChildren.Replace(m, ""));
            var logOps = ExtractLogOps(withoutChildren);

            ICondition condition;

            // create the correct derived type
            if (logOps.Count==0)
            {
                condition = SimpleCondition.Create(conditionString);
            }
            else
            {
                List<ICondition> children = new List<ICondition>();
                //create child conditions first
                foreach (var childText in childTextList)
                {
                    children.Add(CreateCondidion(childText));
                }
                // The set the values of the condition
                condition = new ComplexCondition()
                {
                    Children = children,
                    LogicOperators = logOps,
                    Value = conditionString
                };
                
            }

            return condition;
        }

        /// <summary>
        /// Extracts child conditions from a given condition
        /// </summary>
        /// <param name="condition">The condition as string </param>
        /// <returns>A list containing the extracted child conditions</returns>
        public static List<string> ExtractChildConditions(this string condition)
        {
            // Condtion \w+\s*\|\s*\w+\s*[<>!=]{1,2}\s*\w+ <EXECUTER>|<State> <CompareOperator> <CompareValue> 
            Regex Conditions = new Regex(@"(?:\(\s*\w+\s*\|\s*\w+\s*[<>!=]{1,2}\s*[\w\d.,]+\s*[^()]*(?>[^()]+|(?<o>)\(|(?<-o>)\))*(?(o)(?!)|)\)|\s*\w+\s*\|\s*\w+\s*[<>!=]{1,2}\s*[\w\d.,]+\s*)");
            var matches = Conditions.Matches(condition);
            var result = matches.Select(m => m.Value).ToList();

            
            if (result.Count==1 && result[0] == condition && !IsValidSimpleCondition(result[0]))
            {
                result = ExtractChildConditionsNoParenthesis(condition);
            }

            // remove possible empty space
            for (int i = result.Count-1; i >= 0;i--)
            {
                result[i] = result[i].Trim();
            }
            return result;
        }

        /// <summary>
        /// Extracts child conditions from a given condition without parenthesis
        /// </summary>
        /// <param name="condition">The condition as string </param>
        /// <returns>A list containing the extracted child conditions</returns>
        public static List<string> ExtractChildConditionsNoParenthesis(this string condition)
        {
            // Condtion \w+\s*\|\s*\w+\s*[<>!=]{1,2}\s*\w+ <EXECUTER>|<Item> <CompareOperator> <CompareValue> 
            Regex Conditions = new Regex(@"(?:(\s*\w+\s*\|\s*\w+\s*[<>!=]{ 1,2}\s*\w+\s* (?>|(?<o>)(|(?<-o>)))*(?(o) (?!)|))|\s*\w+\s*\|\s*\w+\s*[<>!=]{1,2}\s*\w+\s*)");
            var matches = Conditions.Matches(condition);
            return matches.Select(m => m.Value).ToList();
        }

        /// <summary>
        /// Extracts the logic operators from a given condition
        /// </summary>
        /// <param name="condition">The condition as string </param>
        /// <returns>A list containing the extracted logic operators</returns>
        public static List<string> ExtractLogOps(this string condition)
        {
            var regString = @"\s*("; ConditionBase.LogicOperator.ToList().ForEach(op => regString += $"{op}|");
            regString = regString.Remove(regString.Length - 1); regString += @")\s*";
            Regex LogOps = new Regex(regString);
            var matches = LogOps.Matches(condition);
            return matches.Select(m => m.Value.Trim()).ToList();
        }

        /// <summary>
        /// Extracts all <see cref="SimpleCondition"/>s from a condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns>A list of simple conditions</returns>
        public static List<string> ExtractSimpleConditions (this string condition)
        {
            Regex simples = new Regex(@"\w+\s*\|\s*\w+\s*[<>!=]{1,2}\s*[\w\d.,]+");
            return simples.Matches(condition).Select(m=>m.Value).ToList();
        }

        /// <summary>
        /// Standardizes a <see cref="SimpleCondition"/> string
        /// by removing empty spaces and placing a space befor and after the <see cref="ConditionBase.CompareOperators"/>
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static string StandardizeSimpleCondition (this string condition)
        {
            var temp = condition;
            temp = temp.Replace(" ", "");
            foreach (var equop in ConditionBase.CompareOperators)
            {
                if (temp.Contains(equop))
                {
                    temp = temp.Replace(equop, $" {equop} ");
                    break;
                }
            }

            return temp;
        }

        /// <summary>
        /// Standardizes a <see cref="ICondition"/> by replacing the <see cref="SimpleCondition"/>strings
        /// with Stadardized <see cref="SimpleCondition"/>strings
        /// </summary>
        /// <param name="condition">The condition string to standardize</param>
        /// <returns></returns>
        public static string StandardizeCondition (this string condition)
        {
            // extract simple conditions
            var simples = condition.ExtractSimpleConditions();     
            // replace simples with their standardized version
            simples.ForEach(r => condition= condition.Replace(r, r.StandardizeSimpleCondition()));
            return condition;
        }

        /// <summary>
        /// Checks if a given condition string is a valid <see cref="SimpleCondition"/>
        /// </summary>
        /// <param name="conditionstring">The condition to check</param>
        /// <returns>True if its a valid <see cref="SimpleCondition"/></returns>
        public static bool IsValidSimpleCondition (this string condition)
        {
            Regex simple = new Regex(@"\w+\s*\|\s*\w+\s*[<>!=]{1,2}\s*[\w\d.,]+");            
            return simple.Matches(condition).Count==1;
        }
    }
}
