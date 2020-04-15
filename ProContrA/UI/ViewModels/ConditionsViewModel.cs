using Model.Conditions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ProContrA.UI.ViewModels
{
    /// <summary>
    /// Class representing a collection of <see cref="IConditionViewModel"/>
    /// </summary>
    public class ConditionsViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConditionsViewModel ()
        {
            var conds = Model.Conditions.Globals.Conditions.Values.ToList();
            foreach (var cond in  conds)
            {
                if (cond is SimpleCondition)
                {
                    Conditions.Add(new SimpleConditionViewModel(cond));
                }
                else if (cond is ComplexCondition)
                {
                    Conditions.Add(new ComplexConditionViewModel(cond));
                }
            }
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="conditions">The <see cref="ICondition"/>s to be represented by this collection </param>
        public ConditionsViewModel (List<Model.Conditions.ICondition> conditions)
        {
            foreach (var cond in conditions)
            {
                if (cond is SimpleCondition)
                {
                    Conditions.Add(new SimpleConditionViewModel(cond));
                }
                else if (cond is ComplexCondition)
                {
                    Conditions.Add(new ComplexConditionViewModel(cond));
                }
            }
        }

        /// <summary>
        /// The <see cref="IConditionViewModel"/>s of this collection
        /// </summary>
        public ObservableCollection<IConditionViewModel> Conditions { get; set; } = new ObservableCollection<IConditionViewModel>();

    }
}
