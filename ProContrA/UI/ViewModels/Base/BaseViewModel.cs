using System.ComponentModel;

namespace ProContrA.UI.ViewModels.Base
{
    /// <summary>
    /// The basic ViewModel for simplifying the creation of other ViewModels.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}
