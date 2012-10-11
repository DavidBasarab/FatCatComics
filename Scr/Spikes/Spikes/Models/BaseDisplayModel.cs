using GalaSoft.MvvmLight;

namespace Spikes.Models
{
    public abstract class BaseDisplayModel : ViewModelBase
    {
        protected BaseDisplayModel(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; set; }
    }
}