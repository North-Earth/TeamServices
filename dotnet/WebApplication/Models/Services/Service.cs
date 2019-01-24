using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApplication.Models.DataBase;
using WebApplication.Models.Views;

namespace WebApplication.Models.Services
{
    public class Service : IService
    {
        public Service() { }

        public string GetMachineName(string ip)
        {
            var hostEntry = Dns.GetHostEntry(ip);
            var hostName = hostEntry.HostName;

            return hostName;
        }

        public List<ViewModelOverTimeWork> ParseToOvertimeReport
            (List<OvertimeWorkReport> overTimeWorkReports)
        {
            var reports = new List<ViewModelOverTimeWork>();

            foreach (var reportsUser in overTimeWorkReports.GroupBy(r => r.Name))
            {
                var overtime = reportsUser
                    .Where(r => r.OvertimeHour > 0);

                var unworkedTime = reportsUser
                    .Where(r => r.OvertimeHour < 0);

                var overtimeReason = overtime
                    .Where(r => r.LoadDtm == overtime
                    .Max(time => time.LoadDtm))
                    .FirstOrDefault().Description ?? "-";

                var unworkedTimeReason = unworkedTime
                    .Where(r => r.LoadDtm == unworkedTime
                    .Max(time => time.LoadDtm))
                    .FirstOrDefault().Description ?? "-";

                var report = new ViewModelOverTimeWork
                {
                    Name = reportsUser.Key,
                    OvertimeReason = overtimeReason,
                    UnworkedTimeReason = unworkedTimeReason
                };

                foreach (var reportUser in reportsUser
                    .Where(r => r.LoadDtm.Year == DateTime.Now.Year && r.LoadDtm.Month == DateTime.Now.Month))
                {
                    if (reportUser.OvertimeHour >= 0)
                        report.Overtime += reportUser.OvertimeHour;
                    else if (reportUser.OvertimeHour < 0)
                        report.UnworkedTime += reportUser.OvertimeHour;
                }

                foreach (var reportUser in reportsUser
                    .Where(r => r.LoadDtm.Year == DateTime.Now.Year && r.LoadDtm.Month == DateTime.Now.AddMonths(-1).Month))
                {
                    if (reportUser.OvertimeHour >= 0)
                        report.OvertimeLast += reportUser.OvertimeHour;
                    else if (reportUser.OvertimeHour < 0)
                        report.UnworkedTimeLast += reportUser.OvertimeHour;
                }

                reports.Add(report);
            }

            return reports;
        }
    }
}
