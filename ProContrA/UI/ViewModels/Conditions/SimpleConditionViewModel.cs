using Model.Conditions;
using Model.Conditions.Evaluation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProContrA.UI.ViewModels
{
    /// <summary>
    /// An class representing a visual representation of a <see cref="SimpleCondition"/>
    /// </summary>
    public class SimpleConditionViewModel : Base.BaseViewModel, IConditionViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="condition">The <see cref="ICondition"/> that should be represented visually</param>
        public SimpleConditionViewModel(ICondition condition)
        {
            Condition = condition;
        }

        #region Properties
        /// <summary>
        /// The <see cref="ICondition"/> that is represented by this ViewModel
        /// </summary>
        public ICondition Condition { get; set; }

        /// <summary>
        /// The <see cref="Condition"/>s value
        /// </summary>
        public string Value
        {
            get { return Condition.Value; }
            set { OnPropertyChanged(new PropertyChangedEventArgs(nameof(Value))); }
        }

        /// <summary>
        /// The Background 
        /// </summary>
        public System.Windows.Media.SolidColorBrush BackgroundBrush
        {
            get
            {
                var red = new System.Windows.Media.Color() { R = 255, G = 0, B = 0, A = 255 };
                var green = new System.Windows.Media.Color() { R = 0, G = 255, B = 0, A = 255 };

                return Evaluator.Evaluate(Condition) == true ? new System.Windows.Media.SolidColorBrush(green) : new System.Windows.Media.SolidColorBrush(red);
            }
        }

        /// <summary>
        /// The <see cref="Condition"/>s children (<see cref="SimpleCondition"/>s and <see cref="ComplexCondition"/>s) as ViewModels.
        /// For a <see cref="SimpleConditionViewModel"/> the collection should be always empty.
        /// </summary>
        public ObservableCollection<IConditionViewModel> Children { get; set; } = new ObservableCollection<IConditionViewModel>();

        /// <summary>
        /// The <see cref="Condition"/>s <see cref="ICondition.LogicOperators"/>.
        /// For a <see cref="SimpleConditionViewModel"/> the collection should be always empty.
        /// </summary>
        public ObservableCollection<string> LogicOperators { get; set; } = new ObservableCollection<string>(); 
        #endregion

    }
}
