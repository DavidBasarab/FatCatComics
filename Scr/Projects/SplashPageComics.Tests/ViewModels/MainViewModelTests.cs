//using FluentAssertions;
//using Moq;
//using NUnit.Framework;
//using SplashPageComics.Business.Logic;
//using SplashPageComics.Business.ViewModels;

//namespace SplashPageComics.Tests.ViewModels
//{
//    [TestFixture]
//    [Category("ViewModels")]
//    public class MainViewModelTests : BaseViewModelTest
//    {
//        private Mock<SelectedComicsBusiness> selectedComicsBusiness;
//        private MainViewModel mainModel;

//        private void CreateMainViewModel()
//        {
//            mainModel = new MainViewModel(messengerService.Object, selectedComicsBusiness.Object);
//        }

//        private void CreateSelectedComics()
//        {
//            selectedComicsBusiness = new Mock<SelectedComicsBusiness>();
//        }

//        private void SetUpForIsAtLeastOneFolderSelected(bool aFolderSelected = true)
//        {
//            CreateSelectedComics();

//            selectedComicsBusiness.Setup(v => v.IsAtLeastOneFolderSelected()).Returns(aFolderSelected);
//        }

//        [Test]
//        public void OnStartIfAComicIsNotSelectedThenShowSelectedFolderIsFalse()
//        {
//            SetUpForIsAtLeastOneFolderSelected(false);

//            CreateMainViewModel();

//            mainModel.Start();

//            mainModel.ShowSelectFolder.Should().BeFalse();
//        }

//        [Test]
//        public void OnStartIfAComicIsSelectedThenShowSelectedFolderIsTrue()
//        {
//            SetUpForIsAtLeastOneFolderSelected();

//            CreateMainViewModel();

//            mainModel.Start();

//            mainModel.ShowSelectFolder.Should().BeTrue();
//        }

//        [Test]
//        public void StartWillCallSelectedComicsBusiness()
//        {
//            SetUpForIsAtLeastOneFolderSelected();

//            CreateMainViewModel();

//            mainModel.Start();

//            selectedComicsBusiness.VerifyAll();
//        }
//    }
//}