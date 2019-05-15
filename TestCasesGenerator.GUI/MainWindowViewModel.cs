using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using TestCasesGenerator.Core;
using TestCasesGenerator.Core.Structures;

namespace TestCasesGenerator.GUI
{
    class MainWindowViewModel : BaseViewModel
    {
        private TestsGenerator _testsGenerator;

        public ObservableCollection<TestCase> TestCases { get; set; }
        public bool IsLoading { get; set; }

        private DataTable _testCasesTable;
        public DataTable TestCasesTable
        {
            get
            {
                return this._testCasesTable;
            }
            set
            {
                this._testCasesTable = value;
            }
        }

        public MainWindowViewModel()
        {
            this.TestCases = new ObservableCollection<TestCase>();
            this._testCasesTable = new DataTable();
            this.IsLoading = false;
            this._testsGenerator = new TestsGenerator();
        }

        public async void GenerateTests(string filePath)
        {
            this.IsLoading = true;
            TestCase[] testCases = await Task.Run(() => this._testsGenerator.GenerateTests(filePath));
            this.TestCases = new ObservableCollection<TestCase>(testCases);
            this.BuildTable();
            this.IsLoading = false;
        }

        private void BuildTable()
        {
            DataTable table = new DataTable();

            DataColumn testNameCol = new DataColumn();
            testNameCol.ColumnName = "Test Name";
            table.Columns.Add(testNameCol);

            Dictionary<string, DataColumn> varCols = new Dictionary<string, DataColumn>();
            foreach(string key in this.TestCases[0].Inputs.Keys)
            {
                DataColumn varCol = new DataColumn();
                varCol.ColumnName = $"Input: {key}";
                table.Columns.Add(varCol);
                varCols.Add(key, varCol);
            }

            DataColumn valueCol = new DataColumn();
            valueCol.ColumnName = "Result";
            table.Columns.Add(valueCol);

            int i = 0;
            foreach(TestCase testCase in this.TestCases)
            {
                i++;
                DataRow testRow = table.NewRow();
                testRow[testNameCol] = $"Test {i} {testCase.Name}";
                foreach(KeyValuePair<string, object> input in testCase.Inputs)
                {
                    testRow[varCols[input.Key]] = input.Value;
                }
                testRow[valueCol] = testCase.ExpectedOutput;
                table.Rows.Add(testRow);
            }

            this.TestCasesTable = table;
        }
    }
}
