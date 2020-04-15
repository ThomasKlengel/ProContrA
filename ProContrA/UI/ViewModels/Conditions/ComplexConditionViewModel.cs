using Model.Conditions;
using Model.Conditions.Evaluation;
using ProContrA.UI.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ProContrA.UI.ViewModels
{
    /// <summary>
    /// An class representing a visual representation of a <see cref="ComplexCondition"/>
    /// </summary>
    public class ComplexConditionViewModel : ViewModels.Base.BaseViewModel,IConditionViewModel
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="condition">The <see cref="ICondition"/> that should be represented visually</param>
        public ComplexConditionViewModel(ICondition condition)
        {
            Condition = condition;

            Condition.Children?.ForEach(cond =>
            {
                if (cond is SimpleCondition)
                {
                    Children.Add(new SimpleConditionViewModel(cond));
                }
                else if (cond is ComplexCondition)
                {
                    Children.Add(new ComplexConditionViewModel(cond));
                }
            });
            Condition.LogicOperators?.ForEach(logOp =>
            {
                LogicOperators.Add(logOp);
            });

            ExpandCommand = new RelayCommand(Expand, CanExpand);
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
        /// The <see cref="Condition"/>s children (<see cref="SimpleCondition"/>s and <see cref="ComplexCondition"/>s) as ViewModels
        /// </summary>
        public ObservableCollection<IConditionViewModel> Children { get; set; } = new ObservableCollection<IConditionViewModel>();

        /// <summary>
        /// The <see cref="Condition"/>s <see cref="ICondition.LogicOperators"/>
        /// </summary>
        public ObservableCollection<string> LogicOperators { get; set; } = new ObservableCollection<string>();

        private bool isExpanded;
        /// <summary>
        /// If the <see cref="ComplexConditionViewModel"/> is expanded
        /// </summary>
        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsExpanded)));
                }
            }
        } 
        #endregion

        #region Expand Command
        /// <summary>
        /// The command for expanding the <see cref="ComplexCondition"/>
        /// </summary>
        public RelayCommand ExpandCommand { get; set; }

        public bool CanExpand(object o)
        {
            if (Condition is ComplexCondition) return true;
            return false;
        }

        private void Expand(object o)
        {
            // only expand when its a complex condition. simple conditions cant be expanded since they dont have any children
            if (Condition is ComplexCondition)
            {
                var childs = Condition.Children;
                this.Children = new ObservableCollection<IConditionViewModel>();

                foreach (var cond in childs)
                {
                    if (cond is SimpleCondition)
                    {
                        Children.Add(new SimpleConditionViewModel(cond));
                    }
                    else if (cond is ComplexCondition)
                    {
                        Children.Add(new ComplexConditionViewModel(cond));
                    }
                }
            }
        } 
        #endregion

    }
}
