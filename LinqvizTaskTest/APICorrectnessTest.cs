using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jetbrains_test_linqviz;

namespace LinqvizTaskTest
{
    /// <summary>
    /// Summary description for APICorrectnessTest
    /// </summary>
    [TestClass]
    public class APICorrectnessTest
    {
        Controller controller;
        [TestInitialize]
        public void SetUp()
        {
            controller = new Controller(10, 10);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(1 ,11, 2, 2)]
        [DataRow(1, 1, 22, 2)]
        [DataRow(1, 1, 2, 22)]
        [DataRow(1, -1, 2, 2)]
        [DataRow(1, 1, -1, 2)]
        [DataRow(1, 1, 2, -1)]
        [DataRow(11, 1, 2, 3)]
        [DataRow(0, 1, 2, 3)]
        public void TestSettingNonConditionalDepartmentWrongArgument(int dep, int i, int j, int k)
        {
            controller.SetSimpleDepartment(dep, i, j, k);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(1, 1, 11, 2, 2, 1, 1, 1)]//i,j,k
        [DataRow(1, 1, 1, 22, 2, 1, 1, 1)]
        [DataRow(1, 1, 1, 2, 22, 1, 1, 1)]
        [DataRow(1, 1, -1, 2, 2, 1, 1, 1)]
        [DataRow(1, 1, 1, -1, 2, 1, 1, 1)]
        [DataRow(1, 1, 1, 2, -1, 1, 1, 1)]
        [DataRow(1, 1, 1, 1, 1, 11, 2, 2)]//t,r,p
        [DataRow(1, 1, 1, 1, 1, 1, 22, 2)]
        [DataRow(1, 1, 1, 1, 1, 1, 2, 22)]
        [DataRow(1, 1, 1, 1, 1, -1, 2, 2)]
        [DataRow(1, 1, 1, 1, 1, 1, -1, 2)]
        [DataRow(1, 1, 1, 1, 1, 1, 2, -1)]
        [DataRow(11, 1, 1, 2, 3, 1, 1, 1)]//dep
        [DataRow(0, 1, 1, 2, 3, 1, 1, 1)]
        [DataRow(1, 11, 1, 2, 3, 1, 1, 1)]//s
        [DataRow(1, 0, 1, 2, 3, 1, 1, 1)]
        public void TestSettingConditionalDepartmentWrongArgument(int dep, int s, int i, int j, int k, int t, int r, int p)
        {
            controller.SetConditionalDepartment(dep, s, i, j, k, t, r, p);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(0)]
        [DataRow(11)]
        public void TestExecutingRulesWrongArgument(int dep)
        {
            controller.GetCheckList(dep);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestExecutingRulesNotConfigured()
        {
            controller.GetCheckList(1);
        }


    }
}
