﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class CalculatorVM: BaseVM
    {
        Operation _Op = new BinaryOperation();
        public Operation Op
        {
            get { return _Op; }
            set { _Op = value; OnPropertyChanged(); }
        }
        ObservableCollection<Operation> _Operations = new ObservableCollection<Operation>();
        public ObservableCollection<Operation> Operations
        {
            get { return _Operations; }
            set { _Operations = value; OnPropertyChanged(); }
        }



    }
}
