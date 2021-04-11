using System;
using System.Collections.Generic;
using jetbrains_test_linqviz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinqvizTaskTest
{
    [TestClass]
    public class UnitTest1
    {
        Controller controller;
        [TestInitialize]
        public void SetUp()
        {
            controller = new Controller(3,3);

        }

        [TestMethod]
        public void TestNonConditionalDepartmentLogic()
        {
            controller.SetSimpleDepartment(1,1,0,3);
            controller.SetSimpleDepartment(3, 0, 3, 3);

            HashSet<string> set = controller.GetCheckList(3);
            Assert.IsTrue(set.Contains("100"));
        }

        [TestMethod]
        public void TestConditionalDepartmentLogicConditionTrue()
        {
            controller.SetSimpleDepartment(1, 1, 0, 2);
            controller.SetConditionalDepartment(2, 1, 0, 3, 3, 1, 1, 1);
            controller.SetSimpleDepartment(3, 0, 3, 3);

            HashSet<string> set = controller.GetCheckList(3);
            Assert.IsTrue(set.Contains("100"));
        }

        [TestMethod]
        public void TestConditionalDepartmentLogicConditionFalse()
        {
            controller.SetSimpleDepartment(1, 0, 0, 2);
            controller.SetConditionalDepartment(2, 1, 0, 1, 1, 0, 3, 3);
            controller.SetSimpleDepartment(3, 0, 3, 3);

            HashSet<string> set = controller.GetCheckList(3);
            Assert.IsTrue(set.Contains("100"));
        }

        [TestMethod]
        public void TestLoop()
        {
            controller.SetSimpleDepartment(1, 1, 0, 2);
            controller.SetConditionalDepartment(2, 1, 0, 1, 1, 0, 3, 3);

            HashSet<string> set = controller.GetCheckList(3);
            Assert.IsTrue(set.Contains("loop"));
        }

        [TestCleanup]
        public void TearDown()
        {

        }
    }
}
