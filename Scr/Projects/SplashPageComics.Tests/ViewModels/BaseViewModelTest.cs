using Moq;
using NUnit.Framework;
using SplashPageComics.Business.ViewModels;

namespace SplashPageComics.Tests.ViewModels
{
    public abstract class BaseViewModelTest
    {
        protected Mock<MessengerService> messengerService;

        [SetUp]
        public void SetUp()
        {
            messengerService = new Mock<MessengerService>();
        }
    }
}