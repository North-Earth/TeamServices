using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication.Models.DataBase;
using WebApplication.Models.Repositories;
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
            (List<WorkReport> WorkReports)
        {
            var reports = new List<ViewModelOverTimeWork>();

            foreach (var reportsUser in WorkReports.GroupBy(r => r.Name))
            {
                var overtime = reportsUser
                    .Where(r => r.TimeHour > 0);

                var unworkedTime = reportsUser
                    .Where(r => r.TimeHour < 0);

                var overtimeReason = overtime?
                    .Where(r => r.LoadDtm == overtime
                    .Max(time => time.LoadDtm))
                    .FirstOrDefault()?.Description ?? "-";

                var unworkedTimeReason = unworkedTime?
                    .Where(r => r.LoadDtm == unworkedTime
                    .Max(time => time.LoadDtm))
                    .FirstOrDefault()?.Description ?? "-";

                var report = new ViewModelOverTimeWork
                {
                    Name = reportsUser.Key,
                    OvertimeReason = overtimeReason,
                    UnworkedTimeReason = unworkedTimeReason
                };

                foreach (var reportUser in reportsUser
                    .Where(r => r.LoadDtm.Year == DateTime.Now.Year && r.LoadDtm.Month == DateTime.Now.Month))
                {
                    if (reportUser.TimeHour >= 0)
                        report.Overtime += reportUser.TimeHour;
                    else if (reportUser.TimeHour < 0)
                        report.UnworkedTime += reportUser.TimeHour;
                }

                foreach (var reportUser in reportsUser
                    .Where(r => r.LoadDtm.Year == DateTime.Now.Year && r.LoadDtm.Month == DateTime.Now.AddMonths(-1).Month))
                {
                    if (reportUser.TimeHour
 >= 0)
                        report.OvertimeLast += reportUser.TimeHour;
                    else if (reportUser.TimeHour < 0)
                        report.UnworkedTimeLast += reportUser.TimeHour;
                }

                reports.Add(report);
            }

            return reports;
        }

        public async Task<Data> GetData(IDataLoader loader, IRepository repository)
        {
            var data = await loader.GetDataAsync(repository);
            return data;
        }
    }
}
