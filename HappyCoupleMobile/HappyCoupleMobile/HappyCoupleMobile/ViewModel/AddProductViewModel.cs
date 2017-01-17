using HappyCoupleMobile.Services;
using HappyCoupleMobile.ViewModel.Abstract;

namespace HappyCoupleMobile.ViewModel
{
    public class AddProductViewModel : BaseHappyViewModel
    {
        public AddProductViewModel(ISimpleAuthService simpleAuthService) : base(simpleAuthService)
        {
        }
    }
}