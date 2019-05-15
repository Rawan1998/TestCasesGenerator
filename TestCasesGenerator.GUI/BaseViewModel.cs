﻿using System.ComponentModel;

namespace TestCasesGenerator.GUI
{
    /// <summary>
    /// A Base View Model to fire Property Changed Events
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}