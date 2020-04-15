using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.Conditions
{
    /// <summary>
    /// Abstract class containing enhanced information of a <see cref="ICondition"/>
    /// </summary>
    public abstract class ConditionBase : ICondition
    {
        /// <summary>
        /// Logic operators the can be used in a <see cref="ICondition"/> 
        /// </summary>
        public static readonly string[] LogicOperator = new[] { "AND", "OR", "XOR" };
        /// <summary>
        /// Compare operators that can be used in a <see cref="ICondition"/> 
        /// </summary>
        public static readonly string[] CompareOperators = new[] { "==", "!=", "<=", ">=", "<", ">" };

        /// <summary>
        /// The rgular expressions for logic operators
        /// </summary>
        public static readonly Regex[] RegexLogOp = new Regex[3] { new Regex("AND"), new Regex("OR"), new Regex("XOR") };
        /// <summary>
        /// The rgular expressions for equality operators
        /// </summary>
        public static readonly Dictionary<string, Regex> RegexEquOp = new Dictionary<string, Regex>()
        {
            { "==", new Regex("==") },
            { "!=",new Regex("!=|<>") },
            { "<=",new Regex("<=") },
            { ">=",new Regex(">=") },
            { "<",new Regex("(?!<=)<") },
            { ">",new Regex("(?!>=)>") }
        };
        
        /// <summary>
        /// Basic constructor doing things that are to be done by each <see cref="SimpleCondition"/> and <see cref="ComplexCondition"/>
        /// </summary>
        public ConditionBase()
        {
            IDs++;
            ID = IDs;
            Globals.Conditions.Add(ID, this);
        }

        /// <summary>
        /// An increasing number containing the max ID of the conditions
        /// </summary>
        private static ulong IDs { get; set; } = 0;
        /// <summary>
        /// The resource (actor of a machine) that has the condition if it is a <see cref="SimpleCondition"/>
        /// </summary>
        public virtual string Resource { get; set; } = null;
        /// <summary>
        /// The equality operator of the condition if it is a <see cref="SimpleCondition"/>
        /// </summary>
        public virtual string CompareOperator { get; set; } = null;
        /// <summary>
        /// The logic operators within a <see cref="ComplexCondition"/>
        /// </summary>
        public virtual List<string> LogicOperators { get; set; } = null;
        /// <summary>
        /// A state the resource can have within a <see cref="SimpleCondition"/>
        /// </summary>
        public virtual string State { get; set; } = null;
        /// <summary>
        /// The child conditions of a <see cref="ComplexCondition"/>
        /// </summary>
        public virtual List<ICondition> Children { get; set; }
        /// <summary>
        /// The value of the condition as string
        /// </summary>
        public virtual string Value { get; set; } = string.Empty;

        /// <summary>
        /// The value of the state of a <see cref="SimpleCondition"/>
        /// </summary>
        public virtual object StateValue { get; set; }
        /// <summary>
        /// The ID of the condition
        /// </summary>
        public ulong ID { get; }

        /// <summary>
        /// Whether condtion has children or not
        /// </summary>
        public bool HasChildren
        {
            get
            {                
                if (this is ComplexCondition cc)
                {
                    return (cc.Children != null && cc.Children.Count > 0);
                }
                return false;
            }
        }

        /// <summary>
        /// Whether a condition is valid or not
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool retval = false;
                if (this is SimpleCondition sc)
                {
                    return !(
                        // is invalid when
                        sc.Value == string.Empty
                        || sc.State == string.Empty
                        || sc.Children != null
                        || string.IsNullOrEmpty(sc.CompareOperator)
                        || CompareOperators.TakeWhile(op => sc.Value.Contains(op)).Count() > 1 // or more then one
                        || sc.LogicOperators != null // has any logical operator
                        ); ;
                }
                else if (this is ComplexCondition cc)
                {
                    return !(
                         !string.IsNullOrEmpty(cc.State)
                        || cc.Children == null
                        || cc.Children?.Count < 2
                        || cc.LogicOperators?.Count<1
                        );
                }
                return retval;
            }
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
