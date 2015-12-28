using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvvmLight1.Model.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelloToDo.Model.Tasks;
using PixelloTools.Logging;

namespace MvvmLight1.Model.Tasks.Tests
{
    [TestClass()]
    public class TaskServiceTests
    {
        private Ilogger logger;

        public TaskServiceTests(Ilogger ilogger)
        {
            logger = ilogger;
        }

        private TaskService GetTestedTaskService()
        {
            return new TaskService(logger);
        }

        [TestMethod()]
        public void SaveTaskToFileTest()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void GetTaskItemsTest()
        {
            var list = GetTestedTaskService().GetTasksCollection();
            foreach (var taskItem in list)
            {
                taskItem.IsCompleted.ToString();
            }
        }
    }
}