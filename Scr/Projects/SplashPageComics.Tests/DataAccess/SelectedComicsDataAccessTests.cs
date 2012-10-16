using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SplashPageComics.Business.Data;
using SplashPageComics.Business.DataTypes;

namespace SplashPageComics.Tests.DataAccess
{
    [TestFixture]
    [Category("DataAccess")]
    public class SelectedComicsDataAccessTests
    {
        private Mock<DataStore> dataStore;
        private SelectedFolderDataAccess selectedFolderDataAccess;

        private void SetUpSelectedFoldersProperty()
        {
            var selectedFolders = new List<SelectedFolder>
            {
                new SelectedFolder(),
                new SelectedFolder()
            };

            dataStore.Setup(v => v.SelectedFolders).Returns(selectedFolders);
        }

        [Test]
        public void ToGetTheNumberOfFoldersWillUseSelectedFoldersFromDataStore()
        {
            dataStore = new Mock<DataStore>();

            SetUpSelectedFoldersProperty();

            selectedFolderDataAccess = new SelectedFolderDataAccess(dataStore.Object);

            var results = selectedFolderDataAccess.NumberOfSelectedFolders();

            results.Should().Be(2);
        }
    }
}