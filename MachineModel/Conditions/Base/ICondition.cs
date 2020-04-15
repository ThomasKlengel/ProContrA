using System.Collections.Generic;

namespace Model.Conditions
{
    /// <summary>
    /// An interface representing a condtion to check
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// The resource (actor of a machine) that has the condition if it is a <see cref="SimpleCondition"/>
        /// </summary>
        public string Resource { get; set; }
        /// <summary>
        /// The compare operator of the condition if it is a <see cref="SimpleCondition"/>
        /// </summary>
        public string CompareOperator { get; set; }
        /// <summary>
        /// The logic operators within a <see cref="ComplexCondition"/>
        /// </summary>
        public List<string> LogicOperators { get; set; }
        /// <summary>
        /// A state the resource can have within a <see cref="SimpleCondition"/>
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// The child conditions of a <see cref="ComplexCondition"/>
        /// </summary>
        public List<ICondition> Children { get; set; }
        /// <summary>
        /// The value of the state of a <see cref="SimpleCondition"/>
        /// </summary>
        public object StateValue { get; set; }
        /// <summary>
        /// The ID of the condition
        /// </summary>
        public ulong ID { get; }
        /// <summary>
        /// The value of the condition as string
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Whether a condition is valid or not
        /// </summary>
        public bool IsValid { get; }
        /// <summary>
        /// Whether condtion has children or not
        /// </summary>
        public bool HasChildren { get; }
    }
}
