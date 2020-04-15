using System.Collections.Generic;


namespace Model.Conditions
{
    /// <summary>
    /// A class representing a condtion that contains multiple <see cref="SimpleCondition"/>s or other <see cref="ComplexCondition"/>s
    /// </summary>
    public class ComplexCondition: ConditionBase, ICondition
    {
        public ComplexCondition() : base() { }

        /// <summary>
        /// The child conditions of a <see cref="ComplexCondition"/>
        /// </summary>
        public new List<ICondition> Children { get; set; } = new List<ICondition>();
        /// <summary>
        /// The logic operators within a <see cref="ComplexCondition"/>
        /// </summary>
        public new List<string> LogicOperators { get; set; } = new List<string>();
    }
}
