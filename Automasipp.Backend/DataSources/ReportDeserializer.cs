using Automasipp.Models;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Automasipp.Backend.DataSources
{
    public class ReportDeserializer : IReportDeserializer
    {

       
        public Report Deserialize(string Line)
        {
            Report report;
            string[] parts;
            int i = 0;

            if (Line == null) throw new ArgumentNullException(nameof(Line));

            parts = Line.Split(';');
            if (parts.Length < 95) throw new InvalidDataException("Incomplete data line provided");

            report= new Report();
            report.StartTime=ToDateTime(parts[i++]);
            report.LastResetTime = ToDateTime(parts[i++]);
            report.CurrentTime = ToDateTime(parts[i++]);
            report.ElapsedTime_Periodic = ToTimeSpan(parts[i++]);
            report.ElapsedTime_Cumutated = ToTimeSpan(parts[i++]);
            report.TargetRate = ToInt(parts[i++]);
            report.CallRate_Periodic = ToFloat(parts[i++]);
            report.CallRate_Cumutated = ToFloat(parts[i++]);
            report.IncomingCall_Periodic = ToInt(parts[i++]);
            report.IncomingCall_Cumutated = ToInt(parts[i++]);
            report.OutgoingCall_Periodic = ToInt(parts[i++]);
            report.OutgoingCall_Cumutated = ToInt(parts[i++]);
            report.TotalCallCreated = ToInt(parts[i++]);
            report.CurrentCall = ToInt(parts[i++]);
            report.SuccessfulCall_Periodic = ToInt(parts[i++]);
            report.SuccessfulCall_Cumutated = ToInt(parts[i++]);
            report.FailedCall_Periodic = ToInt(parts[i++]);
            report.FailedCall_Cumutated = ToInt(parts[i++]);
            report.FailedCannotSendMessage_Periodic = ToInt(parts[i++]);
            report.FailedCannotSendMessage_Cumutated = ToInt(parts[i++]);
            report.FailedMaxUDPRetrans_Periodic = ToInt(parts[i++]);
            report.FailedMaxUDPRetrans_Cumutated = ToInt(parts[i++]);
            report.FailedTcpConnect_Periodic = ToInt(parts[i++]);
            report.FailedTcpConnect_Cumutated = ToInt(parts[i++]);
            report.FailedTcpClosed_Periodic = ToInt(parts[i++]);
            report.FailedTcpClosed_Cumutated = ToInt(parts[i++]);
            report.FailedUnexpectedMessage_Periodic = ToInt(parts[i++]);
            report.FailedUnexpectedMessage_Cumutated = ToInt(parts[i++]);
            report.FailedCallRejected_Periodic = ToInt(parts[i++]);
            report.FailedCallRejected_Cumutated = ToInt(parts[i++]);
            report.FailedCmdNotSent_Periodic = ToInt(parts[i++]);
            report.FailedCmdNotSent_Cumutated = ToInt(parts[i++]);
            report.FailedRegexpDoesntMatch_Periodic = ToInt(parts[i++]);
            report.FailedRegexpDoesntMatch_Cumutated = ToInt(parts[i++]);
            report.FailedRegexpShouldntMatch_Periodic = ToInt(parts[i++]);
            report.FailedRegexpShouldntMatch_Cumutated = ToInt(parts[i++]);
            report.FailedRegexpHdrNotFound_Periodic = ToInt(parts[i++]);
            report.FailedRegexpHdrNotFound_Cumutated = ToInt(parts[i++]);
            report.FailedOutboundCongestion_Periodic = ToInt(parts[i++]);
            report.FailedOutboundCongestion_Cumutated = ToInt(parts[i++]);
            report.FailedTimeoutOnRecv_Periodic = ToInt(parts[i++]);
            report.FailedTimeoutOnRecv_Cumutated = ToInt(parts[i++]);
            report.FailedTimeoutOnSend_Periodic = ToInt(parts[i++]);
            report.FailedTimeoutOnSend_Cumutated = ToInt(parts[i++]);
            report.FailedTestDoesntMatch_Periodic = ToInt(parts[i++]);
            report.FailedTestDoesntMatch_Cumutated = ToInt(parts[i++]);
            report.FailedTestShouldntMatch_Periodic = ToInt(parts[i++]);
            report.FailedTestShouldntMatch_Cumutated = ToInt(parts[i++]);
            report.FailedStrcmpDoesntMatch_Periodic = ToInt(parts[i++]);
            report.FailedStrcmpDoesntMatch_Cumutated = ToInt(parts[i++]);
            report.FailedStrcmpShouldntMatch_Periodic = ToInt(parts[i++]);
            report.FailedStrcmpShouldntMatch_Cumutated = ToInt(parts[i++]);
            report.OutOfCallMsgs_Periodic = ToInt(parts[i++]);
            report.OutOfCallMsgs_Cumutated = ToInt(parts[i++]);
            report.DeadCallMsgs_Periodic = ToInt(parts[i++]);
            report.DeadCallMsgs_Cumutated = ToInt(parts[i++]);
            report.Retransmissions_Periodic = ToInt(parts[i++]);
            report.Retransmissions_Cumutated = ToInt(parts[i++]);
            report.AutoAnswered_Periodic = ToInt(parts[i++]);
            report.AutoAnswered_Cumutated = ToInt(parts[i++]);
            report.Warnings_Periodic = ToInt(parts[i++]);
            report.Warnings_Cumutated = ToInt(parts[i++]);
            report.FatalErrors_Periodic = ToInt(parts[i++]);
            report.FatalErrors_Cumutated = ToInt(parts[i++]);
            report.WatchdogMajor_Periodic = ToInt(parts[i++]);
            report.WatchdogMajor_Cumutated = ToInt(parts[i++]);
            report.WatchdogMinor_Periodic = ToInt(parts[i++]);
            report.WatchdogMinor_Cumutated = ToInt(parts[i++]);
            report.ResponseTime1_Periodic = ToTimeSpan(parts[i++]);
            report.ResponseTime1_Cumutated = ToTimeSpan(parts[i++]);
            report.ResponseTime1StDev_Periodic = ToTimeSpan(parts[i++]);
            report.ResponseTime1StDev_Cumutated = ToTimeSpan(parts[i++]);
            report.CallLength_Periodic = ToTimeSpan(parts[i++]);
            report.CallLength_Cumutated = ToTimeSpan(parts[i++]);
            report.CallLengthStDev_Periodic = ToTimeSpan(parts[i++]);
            report.CallLengthStDev_Cumutated = ToTimeSpan(parts[i++]);
            report.ResponseTimeRepartition1 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_lt10 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_lt20 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_lt30 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_lt40 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_lt50 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_lt100 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_lt150 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_lt200 = ToInt(parts[i++]);
            report.ResponseTimeRepartition1_ge200 = ToInt(parts[i++]);
            report.CallLengthRepartition = ToInt(parts[i++]);
            report.CallLengthRepartition_lt10 = ToInt(parts[i++]);
            report.CallLengthRepartition_lt50 = ToInt(parts[i++]);
            report.CallLengthRepartition_lt100 = ToInt(parts[i++]);
            report.CallLengthRepartition_lt500 = ToInt(parts[i++]);
            report.CallLengthRepartition_lt1000 = ToInt(parts[i++]);
            report.CallLengthRepartition_lt5000 = ToInt(parts[i++]);
            report.CallLengthRepartition_lt10000 = ToInt(parts[i++]);
            report.CallLengthRepartition_ge10000 = ToInt(parts[i++]);
            return report;
        }

        public DateTime ToDateTime(string Value)
        {
            string formattedValue;
            string[] parts;

            if (Value == null) throw new ArgumentNullException(nameof(Value));

            parts= Value.Split('\t',' ');
            if (parts.Length <3) throw new InvalidDataException("Incomplete value provided");
            formattedValue = $"{parts[0]} {parts[1]}";
            
            return DateTime.ParseExact(formattedValue, "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture);
        }
        public int ToInt(string Value)
        {
            if (Value == null) throw new ArgumentNullException(nameof(Value));
            if (Value == "") return 0;
            return int.Parse(Value);
        }
        public float ToFloat(string Value)
        {
            if (Value == null) throw new ArgumentNullException(nameof(Value));
            if (Value == "") return 0;
            return float.Parse(Value);
        }
        public TimeSpan ToTimeSpan(string Value)
        {
            string formattedValue;
            Match match;
            if (Value == null) throw new ArgumentNullException(nameof(Value));

            match = Regex.Match(Value, @"(?<validPart>\d\d:\d\d:\d\d)(:\d+)?");
            if (!match.Success) throw new InvalidDataException("Incomplete value provided");
            formattedValue = match.Groups["validPart"].Value;
            return TimeSpan.Parse(formattedValue);
        }

    }
}
