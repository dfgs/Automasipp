using Automasipp.backend.Controllers;
using Automasipp.backend.DataSources;
using Automasipp.Backend.Controllers;
using Automasipp.Backend.DataSources;
using Automasipp.Models;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using ResultTypeLib;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Automasipp.Backend.UnitTests.DataSources
{
    [TestClass]
    public class ReportDataSourceUnitTest
    {

        #region constructor
        [TestMethod]
        public void ConstructorShouldFailAndLogErrorIfLoggerIsNull()
        {
            ILogger logger;

            logger = Mock.Of<ILogger>();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => new ReportDataSource(null, Mock.Of<IReportDeserializer>(), "invalidfolder"));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
        }
        [TestMethod]
        public void ConstructorShouldFailAndLogErrorIfDeserializeIsNull()
        {
            ILogger logger;

            logger = Mock.Of<ILogger>();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => new ReportDataSource(logger, null, "invalidfolder"));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
        }
        [TestMethod]
        public void ConstructorShouldFailAndLogErrorIfReportsFolderIsNull()
        {
            ILogger logger;

            logger = Mock.Of<ILogger>();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => new ReportDataSource(logger, Mock.Of<IReportDeserializer>(), null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.


        }
        
        #endregion


        #region CreateReportFromFileName
        
        [TestMethod]
        public void CreateReportsFromFileNameShouldThrowExceptionIfScenarioNameIsNull()
        {
            ILogger logger;
            ReportDataSource dataSource;
            IReportDeserializer reportDeserializer;

            logger = Mock.Of<ILogger>();
            reportDeserializer = Mock.Of<IReportDeserializer>();

            dataSource = new ReportDataSource(logger, reportDeserializer, "invalidfolder");

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => dataSource.CreateReportsFromFileName(null,1234));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
        }

        [TestMethod]
        public void CreateReportsFromFileNameShouldSuccess()
        {
            ILogger logger;
            ReportDataSource dataSource;
            IReportDeserializer reportDeserializer;
            Report[] reports;


            logger = Mock.Of<ILogger>();
            reportDeserializer = new ReportDeserializer();

            dataSource = new ReportDataSource(logger, reportDeserializer, @".\Reports");

            reports = dataSource.CreateReportsFromFileName("Demo1",1234).ToArray();
            Assert.AreEqual(3,reports.Length);
            Assert.IsNotNull(reports[0]);
            Assert.IsNotNull(reports[1]);
            Assert.IsNotNull(reports[2]);

        }
        #endregion


        #region GetReports
        [TestMethod]
        public void GetReportsShouldFailAndLogErrorIfSourceFolderIsInvalid()
        {
            ILogger logger;
            IReportDataSource dataSource;
            IResult<Report[]> result;
            IReportDeserializer reportDeserializer;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());
           
            reportDeserializer = Mock.Of<IReportDeserializer>();

            dataSource = new ReportDataSource(logger,reportDeserializer,"invalidfolder");

            result=dataSource.GetReports("Demo1",1234);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<DirectoryNotFoundException>(ex));
            
            Mock.VerifyAll();

        }
        [TestMethod]
        public void GetReportsShouldFailAndLogErrorIfScenarioNameIsInvalid()
        {
            ILogger logger;
            IReportDataSource dataSource;
            IResult<Report[]> result;
            IReportDeserializer reportDeserializer;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            reportDeserializer = Mock.Of<IReportDeserializer>();

            dataSource = new ReportDataSource(logger, reportDeserializer, @".\Reports");

            result = dataSource.GetReports("Invalid", 1234);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<FileNotFoundException>(ex));

            Mock.VerifyAll();

        }
        [TestMethod]
        public void GetReportsShouldFailAndLogErrorIfPIDIsInvalid()
        {
            ILogger logger;
            IReportDataSource dataSource;
            IResult<Report[]> result;
            IReportDeserializer reportDeserializer;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            reportDeserializer = Mock.Of<IReportDeserializer>();

            dataSource = new ReportDataSource(logger, reportDeserializer, @".\Reports");

            result = dataSource.GetReports("Demo1", 9999);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<FileNotFoundException>(ex));

            Mock.VerifyAll();

        }

        [TestMethod]
        public void GetReportsShouldSuccess()
        {
            ILogger logger;
            IReportDataSource dataSource;
            IResult<Report[]> result;
            IReportDeserializer reportDeserializer;


            logger = Mock.Of<ILogger>();
            reportDeserializer = new ReportDeserializer();;

            dataSource = new ReportDataSource(logger, reportDeserializer, @".\Reports");

            result = dataSource.GetReports("Demo1",1234);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded());
            result.Match((val) =>
            {
                Assert.AreEqual(3, val.Length);
            }
            , (ex) => Assert.Fail());

        }

 
        [TestMethod]
        public void GetReportsWithScenarioNameShouldFailAndLogErrorIfScenariosFolderIsNull()
        {
            ILogger logger;
            IReportDataSource dataSource;
            IResult<Report[]> result;
            IReportDeserializer reportDeserializer;


            logger = Mock.Of<ILogger>();
            Mock.Get(logger).Setup(m => m.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>())).Verifiable(Times.Once());

            reportDeserializer = Mock.Of<IReportDeserializer>();

            dataSource = new ReportDataSource(logger,reportDeserializer,  @".\Reports");

#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            result = dataSource.GetReports(null,1234);
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded());
            result.Match((val) => Assert.Fail(),
            (ex) =>
            {
                Assert.IsNotNull(result);
                Assert.IsFalse(result.Succeeded());
                result.Match((val) => Assert.Fail(), (ex) => Assert.IsInstanceOfType<ArgumentNullException>(ex));
            }

            );

            Mock.VerifyAll();


        }
        #endregion



    }
}