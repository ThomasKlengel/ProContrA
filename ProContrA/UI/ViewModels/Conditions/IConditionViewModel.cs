using Model.Conditions;
using System.Collections.ObjectModel;

namespace ProContrA.UI.ViewModels
{
    /// <summary>
    /// An interface representing a visual representation of a condtion
    /// </summary>
    public interface IConditionViewModel
    {       
        public ICondition Condition { get; set; }

        /// <summary>
        /// The logic operators within a <see cref="ComplexCondition"/>
        /// </summary>
        public ObservableCollection<string> LogicOperators { get; set; }

        /// <summary>
        /// The child conditions of a <see cref="ComplexCondition"/>
        /// </summary>
        public ObservableCollection<IConditionViewModel> Children { get; set; }

        /// <summary>
        /// The value of the condition as string
        /// </summary>
        public string Value { get; set; }
    }
}
