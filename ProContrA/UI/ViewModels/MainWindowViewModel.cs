
using ProContrA.UI.ViewModels.Base;
using System.ComponentModel;
using System.Windows.Controls;

namespace ProContrA.UI.ViewModels
{
    public class MainWindowViewModel : Base.BaseViewModel
    {
        /// <summary>
        /// creates a new instance of a ViewModel for the MainWindow.
        /// </summary>
        public MainWindowViewModel()
        {
            // set the start page to an empty page
            DisplayPage = new UI.Views.Pages.BlankPage();

            // define the commands for the buttons
            DisplayOverviewCommand = new RelayCommand(DisplayOverview);
            DisplaySequenceCommand  = new RelayCommand(DisplaySequence, CanDisplaySequence);
            LoadSequenceCommand = new RelayCommand(LoadSequence);
            DisplayConditionsCommand = new RelayCommand(DisplayConditions, CanDisplayConditions);
            LoadMappingCommand = new RelayCommand(LoadMapping);
        }

        #region EventHandler

        #endregion

        #region Properties
        private Page _displayPage;

        /// <summary>
        /// Gets the <see cref="Page"/> to display in the main frame.
        /// </summary>
        public Page DisplayPage
        {
            get
            {
                return _displayPage;
            }

            private set
            {
                if (_displayPage != value)
                {
                    _displayPage = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(DisplayPage)));
                }
            }
        }

        private bool _sequenceLoaded;

        /// <summary>
        /// Gets the <see cref="Page"/> to display in the main frame.
        /// </summary>
        public bool SequenceLoaded
        {
            get
            {
                return _sequenceLoaded;
            }

            private set
            {
                if (_sequenceLoaded != value)
                {
                    this._sequenceLoaded = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(SequenceLoaded)));
                }
            }
        }

        private bool _mappingLoaded;

        /// <summary>
        /// Gets the <see cref="Page"/> to display in the main frame.
        /// </summary>
        public bool MappingLoaded
        {
            get
            {
                return _mappingLoaded;
            }

            private set
            {
                if (_mappingLoaded != value)
                {
                    this._mappingLoaded = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(MappingLoaded)));
                }
            }
        }

        #endregion

        #region Commands

        #region Display Overview Command
        public RelayCommand DisplayOverviewCommand { get; private set; }

        private void DisplayOverview(object o)
        {
            this.DisplayPage = new UI.Views.Pages.BlankPage();
        }

        #endregion

        #region Display Sequence Command
        public RelayCommand DisplaySequenceCommand { get; private set; }

        private void DisplaySequence(object o)
        {
            this.DisplayPage = new UI.Views.Pages.DisplaySequencePage();
        }

        private bool CanDisplaySequence(object o)
        {
            // can only siplay a sequence when one has already been loaded
            return SequenceLoaded;
        }
        #endregion

        #region Load Sequence Command
        public RelayCommand LoadSequenceCommand { get; private set; }

        private void LoadSequence(object o)
        {
            SequenceLoaded = ModelParser.Initializer.Initialize();
        }

        #endregion

        #region Display Conditions Command
        public RelayCommand DisplayConditionsCommand { get; private set; }

        private void DisplayConditions(object o)
        {
            this.DisplayPage = new UI.Views.Pages.DisplayConditionsPage();
        }

        private bool CanDisplayConditions(object o)
        {
            // can only siplay a sequence when one has already been loaded
            return SequenceLoaded || MappingLoaded;
        }
        #endregion

        #region Load Mapping Command
        public RelayCommand LoadMappingCommand { get; private set; }

        private void LoadMapping(object o)
        {
            MappingLoaded = Model.Conditions.Mapping.ReadMappingFromFile();
        }

        #endregion
        #endregion

    }
}
