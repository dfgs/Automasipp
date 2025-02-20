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
    public class ReportDeserializerUnitTest
    {

        #region constructor
        /*[TestMethod]
        public void ConstructorShouldFailAndLogErrorIfLoggerIsNull()
        {
           
        }*/


        #endregion

        #region ToDateTime
        [TestMethod]
        public void ToDateTimeShouldThrowExceptionIfValueParameterIsNull()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => deserializer.ToDateTime(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

        }
        [TestMethod]
        public void ToDateTimeShouldThrowExceptionIfValueParameterIsInvalid()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
            Assert.ThrowsException<InvalidDataException>(() => deserializer.ToDateTime("2025-02-20\t12:19:24.147986"));

        }
        [TestMethod]
        public void ToDateTimeShouldConvertValueWithTab()
        {
            ReportDeserializer deserializer;
            DateTime result;

            deserializer = new ReportDeserializer();
            result = deserializer.ToDateTime("2025-02-20\t12:19:24.147986\t1740053964.147986");
            Assert.AreEqual(2025, result.Year);
            Assert.AreEqual(2, result.Month);
            Assert.AreEqual(20, result.Day);
        }
        [TestMethod]
        public void ToDateTimeShouldConvertValueWithSpace()
        {
            ReportDeserializer deserializer;
            DateTime result;

            deserializer = new ReportDeserializer();
            result = deserializer.ToDateTime("2025-02-20 12:19:24.147986 1740053964.147986");
            Assert.AreEqual(2025, result.Year);
            Assert.AreEqual(2, result.Month);
            Assert.AreEqual(20, result.Day);
        }
        #endregion

        #region ToInt
        [TestMethod]
        public void ToIntShouldThrowExceptionIfValueParameterIsNull()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => deserializer.ToInt(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

        }
        [TestMethod]
        public void ToIntShouldThrowExceptionIfValueParameterIsInvalid()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
            Assert.ThrowsException<FormatException>(() => deserializer.ToInt("abc"));

        }
       
        [TestMethod]
        public void ToIntShouldConvertValue()
        {
            ReportDeserializer deserializer;
            int result;

            deserializer = new ReportDeserializer();
            result = deserializer.ToInt("12345");
            Assert.AreEqual(12345, result);
        }

        [TestMethod]
        public void ToIntShouldConvertEmptyValueToZero()
        {
            ReportDeserializer deserializer;
            int result;

            deserializer = new ReportDeserializer();
            result = deserializer.ToInt("");
            Assert.AreEqual(0, result);
        }
        #endregion

        #region ToFloat
        [TestMethod]
        public void ToFloatShouldThrowExceptionIfValueParameterIsNull()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => deserializer.ToFloat(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

        }
        [TestMethod]
        public void ToFloatShouldThrowExceptionIfValueParameterIsInvalid()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
            Assert.ThrowsException<FormatException>(() => deserializer.ToFloat("abc"));

        }

        [TestMethod]
        public void ToFloatShouldConvertValue()
        {
            ReportDeserializer deserializer;
            float result;

            deserializer = new ReportDeserializer();
            result = deserializer.ToFloat("123.45");
            Assert.AreEqual(123.45f, result);
        }

        [TestMethod]
        public void ToFloatShouldConvertEmptyValueToZero()
        {
            ReportDeserializer deserializer;
            float result;

            deserializer = new ReportDeserializer();
            result = deserializer.ToFloat("");
            Assert.AreEqual(0f, result);
        }
        #endregion

        #region ToTimeSpan
        [TestMethod]
        public void ToTimeSpanShouldThrowExceptionIfValueParameterIsNull()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(() => deserializer.ToTimeSpan(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

        }
        [TestMethod]
        public void ToTimeSpanShouldThrowExceptionIfValueParameterIsInvalid()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
            Assert.ThrowsException<InvalidDataException>(() => deserializer.ToTimeSpan("abc"));

        }

        [TestMethod]
        public void ToTimeSpanShouldConvertValue()
        {
            ReportDeserializer deserializer;
            TimeSpan result;

            deserializer = new ReportDeserializer();
            result = deserializer.ToTimeSpan("12:00:05");
            Assert.AreEqual(12, result.Hours);
            Assert.AreEqual(00, result.Minutes);
            Assert.AreEqual(05, result.Seconds);
        }
        [TestMethod]
        public void ToTimeSpanShouldConvertPreciseValue()
        {
            ReportDeserializer deserializer;
            TimeSpan result;

            deserializer = new ReportDeserializer();
            result = deserializer.ToTimeSpan("00:00:02:697000");
            Assert.AreEqual(00, result.Hours);
            Assert.AreEqual(00, result.Minutes);
            Assert.AreEqual(02, result.Seconds);
        }

        #endregion


        #region Deserialize
        [TestMethod]
        public void DeserializeShouldThrowExceptionIfLineParameterIsNull()
        {
            ReportDeserializer deserializer;

 
            deserializer = new ReportDeserializer();
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
            Assert.ThrowsException<ArgumentNullException>(()=>deserializer.Deserialize(null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.

        }


        [TestMethod]
        public void DeserializeShouldThrowExceptionIfLineIsIncomplete()
        {
            ReportDeserializer deserializer;


            deserializer = new ReportDeserializer();
            Assert.ThrowsException<InvalidDataException>(() => deserializer.Deserialize("col1;col2;col3"));

        }

        [TestMethod]
        public void DeserializeShouldReturnValidReport()
        {
            ReportDeserializer deserializer;
            string line;
            Report report;
            line = "2025-02-20\t12:19:24.147986\t1740053964.147986;2025-02-20\t12:19:24.149846\t1740053964.149846;2025-02-20\t12:19:43.337682\t1740053983.337682;00:00:19;00:00:19;10;0.0521186;0.0521132;0;0;1;1;1;0;0;0;1;1;0;0;0;0;0;0;0;0;1;1;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;2;2;0;0;0;0;0;0;00:00:02:697000;00:00:02:697000;00:00:00:000000;00:00:00:000000;00:00:19:089000;00:00:19:089000;2562047788015:12:55:808000;2562047788015:12:55:808000;;0;0;0;0;0;0;0;0;1;;0;0;0;0;0;0;0;1;";

            deserializer = new ReportDeserializer();
            report=deserializer.Deserialize(line);
            Assert.IsNotNull(report);
            Assert.AreEqual(2025, report.StartTime.Year);
            Assert.AreEqual(0.0521186f, report.CallRate_Periodic);
            Assert.AreEqual(0.0521132f, report.CallRate_Cumutated);
            Assert.AreEqual(19,report.ElapsedTime_Periodic.Seconds);
        }

        [DataRow("2025-02-20\t12:19:24.147986\t1740053964.147986;2025-02-20\t12:19:24.147986\t1740053964.147986;2025-02-20\t12:19:24.149483\t1740053964.149483;00:00:00;00:00:00;10;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;00:00:00:000000;00:00:00:000000;00:00:00:000000;00:00:00:000000;00:00:00:000000;00:00:00:000000;00:00:00:000000;00:00:00:000000;;0;0;0;0;0;0;0;0;0;;0;0;0;0;0;0;0;0;")]
        [DataRow("2025-02-20\t12:19:24.147986\t1740053964.147986;2025-02-20\t12:19:24.149846\t1740053964.149846;2025-02-20\t12:19:43.337682\t1740053983.337682;00:00:19;00:00:19;10;0.0521186;0.0521132;0;0;1;1;1;0;0;0;1;1;0;0;0;0;0;0;0;0;1;1;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;2;2;0;0;0;0;0;0;00:00:02:697000;00:00:02:697000;00:00:00:000000;00:00:00:000000;00:00:19:089000;00:00:19:089000;2562047788015:12:55:808000;2562047788015:12:55:808000;;0;0;0;0;0;0;0;0;1;;0;0;0;0;0;0;0;1;")]
        [DataRow("2025-02-20\t12:19:24.147986\t1740053964.147986;2025-02-20\t12:19:43.337837\t1740053983.337837;2025-02-20\t12:19:43.338034\t1740053983.338034;00:00:00;00:00:19;10;0;0.0521105;0;0;0;1;1;0;0;0;0;1;0;0;0;0;0;0;0;0;0;1;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;2;0;0;0;0;0;0;00:00:00:000000;00:00:02:697000;00:00:00:000000;00:00:00:000000;00:00:00:000000;00:00:19:089000;00:00:00:000000;2562047788015:12:55:808000;;0;0;0;0;0;0;0;0;1;;0;0;0;0;0;0;0;1;")]
        [TestMethod]
        public void DeserializeShouldReturnValidReport2(string line)
        {
            ReportDeserializer deserializer;
            Report report;

            deserializer = new ReportDeserializer();
            report = deserializer.Deserialize(line);
            Assert.IsNotNull(report);
           
        }

        #endregion



    }
}