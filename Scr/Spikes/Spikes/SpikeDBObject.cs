using System;
using System.ComponentModel;
using WinRTDatabase;

namespace Spikes
{
    public class SpikeDBObject : PropertyChangedCore
    {
        private string collectionName;
        private string fileLocation;
        private bool hasBeenRead;
        private DateTime lastTimeRead;
        private int numberOfPages;
        private string title;

        public string FileLocation
        {
            get { return fileLocation; }
            set
            {
                fileLocation = value;
                OnPropertyChanged("FileLocation");
            }
        }

        public int NumberOfPages
        {
            get { return numberOfPages; }
            set
            {
                numberOfPages = value;
                OnPropertyChanged("NumberOfPages");
            }
        }

        public DateTime LastTimeRead
        {
            get { return lastTimeRead; }
            set
            {
                lastTimeRead = value;
                OnPropertyChanged("LastTimeRead");
            }
        }

        public bool HasBeenRead
        {
            get { return hasBeenRead; }
            set
            {
                hasBeenRead = value;
                OnPropertyChanged("HasBeenRead");
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string CollectionName
        {
            get { return collectionName; }
            set
            {
                collectionName = value;
                OnPropertyChanged("CollectionName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Person : PropertyChangedCore
    {
        private Guid personID;

        public Guid PersonID
        {
            get { return personID; }
            set { SetProperty(value, ref personID); }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(value, ref firstName); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { SetProperty(value, ref lastName); }
        }

        private int age;

        public int Age
        {
            get { return age; }
            set { SetProperty(value, ref age); }
        }

    }
}