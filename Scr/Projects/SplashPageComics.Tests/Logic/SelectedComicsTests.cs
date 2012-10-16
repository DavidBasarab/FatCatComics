using FluentAssertions;
using Moq;
using NUnit.Framework;
using SplashPageComics.Business.Data;
using SplashPageComics.Business.Logic;

namespace SplashPageComics.Tests.Logic
{
    [TestFixture]
    [Category("Logic")]
    public class SelectedComicsTests
    {
        private Mock<ISelectedFolderDataAccess> selectedFolderDataAccess;
        private SelectedComics selectedComics;
        private bool result;

        [SetUp]
        public void SetUp()
        {
            selectedFolderDataAccess = new Mock<ISelectedFolderDataAccess>();
            selectedComics = new SelectedComics(selectedFolderDataAccess.Object);
        }

        private void RunIsAtLeastOneFolderSelected()
        {
            result = selectedComics.IsAtLeastOneFolderSelected();
        }

        private void SetUpNumberOfSelectedFolders(int numberOfFolders = 3)
        {
            selectedFolderDataAccess.Setup(v => v.NumberOfSelectedFolders()).Returns(numberOfFolders);
        }

        [Test]
        public void AnyFolderSelectedWillCallDataAccessForCountOfFolders()
        {
            SetUpNumberOfSelectedFolders();

            RunIsAtLeastOneFolderSelected();

            selectedFolderDataAccess.VerifyAll();
        }

        [Test]
        public void IfTheNumberOfSelectedFoldersIsGreaterThan0ThenOneHasBeenSelected()
        {
            SetUpNumberOfSelectedFolders();

            RunIsAtLeastOneFolderSelected();

            result.Should().BeTrue();
        }

        [Test]
        public void IfTheNumbersOfSelectedFoldersIs0ThenNoneHaveBeenSelected()
        {
            SetUpNumberOfSelectedFolders(0);

            RunIsAtLeastOneFolderSelected();

            result.Should().BeFalse();
        }
    }
}