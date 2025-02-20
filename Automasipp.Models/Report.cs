using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Automasipp.Models
{
    [Serializable]
    public class Report
    {
        [XmlAttribute]
        public DateTime StartTime
        {
            get;
            set;
        }
        [XmlAttribute]
        public DateTime LastResetTime
        {
            get;
            set;
        }
        [XmlAttribute]
        public DateTime CurrentTime
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan ElapsedTime_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan ElapsedTime_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int TargetRate
        {
            get;
            set;
        }
        [XmlAttribute]
        public float CallRate_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public float CallRate_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int IncomingCall_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int IncomingCall_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int OutgoingCall_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int OutgoingCall_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int TotalCallCreated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CurrentCall
        {
            get;
            set;
        }
        [XmlAttribute]
        public int SuccessfulCall_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int SuccessfulCall_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedCall_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedCall_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedCannotSendMessage_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedCannotSendMessage_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedMaxUDPRetrans_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedMaxUDPRetrans_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTcpConnect_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTcpConnect_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTcpClosed_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTcpClosed_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedUnexpectedMessage_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedUnexpectedMessage_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedCallRejected_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedCallRejected_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedCmdNotSent_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedCmdNotSent_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedRegexpDoesntMatch_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedRegexpDoesntMatch_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedRegexpShouldntMatch_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedRegexpShouldntMatch_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedRegexpHdrNotFound_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedRegexpHdrNotFound_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedOutboundCongestion_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedOutboundCongestion_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTimeoutOnRecv_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTimeoutOnRecv_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTimeoutOnSend_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTimeoutOnSend_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTestDoesntMatch_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTestDoesntMatch_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTestShouldntMatch_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedTestShouldntMatch_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedStrcmpDoesntMatch_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedStrcmpDoesntMatch_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedStrcmpShouldntMatch_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FailedStrcmpShouldntMatch_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int OutOfCallMsgs_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int OutOfCallMsgs_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int DeadCallMsgs_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int DeadCallMsgs_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int Retransmissions_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int Retransmissions_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int AutoAnswered_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int AutoAnswered_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int Warnings_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int Warnings_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FatalErrors_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int FatalErrors_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int WatchdogMajor_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int WatchdogMajor_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int WatchdogMinor_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public int WatchdogMinor_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan ResponseTime1_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan ResponseTime1_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan ResponseTime1StDev_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan ResponseTime1StDev_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan CallLength_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan CallLength_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan CallLengthStDev_Periodic
        {
            get;
            set;
        }
        [XmlAttribute]
        public TimeSpan CallLengthStDev_Cumutated
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_lt10
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_lt20
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_lt30
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_lt40
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_lt50
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_lt100
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_lt150
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_lt200
        {
            get;
            set;
        }
        [XmlAttribute]
        public int ResponseTimeRepartition1_ge200
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition_lt10
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition_lt50
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition_lt100
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition_lt500
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition_lt1000
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition_lt5000
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition_lt10000
        {
            get;
            set;
        }
        [XmlAttribute]
        public int CallLengthRepartition_ge10000
        {
            get;
            set;
        }
       

    }
}
