using System;
using System.Linq;

namespace Model.Conditions
{
    /// <summary>
    /// Class representing a single elemental condition 
    /// </summary>
    public class SimpleCondition: ConditionBase, ICondition
    {
        private SimpleCondition() : base() { }

        /// <summary>
        /// Consturctor using the condition as string and setting properties by splitting the string
        /// </summary>
        /// <param name="conditionString">The complete condition as string</param>
        private SimpleCondition (string conditionString) :base()
        {  
            // <Resource>|<State> <EqualityOperator> <StateValue>
            Value = conditionString;
            var a = Globals.Conditions;
            var splits= conditionString.Split('|');
            Resource = splits[0].Trim();
            var equop = CompareOperators.Where(op => splits[1].Contains(op)).FirstOrDefault();
            CompareOperator = equop;
            State = splits[1].Split(CompareOperator, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault().Trim();
            StateValue = splits[1].Split(CompareOperator, StringSplitOptions.RemoveEmptyEntries).LastOrDefault().Trim();
        }

        /// <summary>
        /// Encapsulates the constructor returning a new condition if the condition has not already been created
        /// </summary>
        /// <param name="conditionString">The complete condition as a string</param>
        /// <returns>A new condition if the condition has not already been created,
        /// otherwise the condtion that has the same value and is present in the <see cref="Globals.Conditions"/></returns>
        public static ICondition Create(string  conditionString)
        {
            conditionString = conditionString.Trim();
            if (Globals.Conditions.Any(c => c.Value.Value == conditionString))
            {             
                return Globals.Conditions.Where(c=>c.Value.Value==conditionString).First().Value ;
            }
            else
            {
                return new SimpleCondition(conditionString);
            }            
        }

        /// <summary>
        /// The equality operator of the condition if it is a <see cref="SimpleCondition"/>
        /// </summary>
        public override string CompareOperator { get; set; } = string.Empty;
        /// <summary>
        /// The resource (actor of a machine) that has the condition if it is a <see cref="SimpleCondition"/>
        /// </summary>
        public override string Resource { get; set; } = string.Empty;
        /// <summary>
        /// A state the resource can have within a <see cref="SimpleCondition"/>
        /// </summary>
        public override string State { get; set; }  = string.Empty;
        /// <summary>
        /// The value of the state of a <see cref="SimpleCondition"/>
        /// </summary>
        public override object StateValue { get; set; } = string.Empty;

    }
}
