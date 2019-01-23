﻿using System;
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
                var report = new ViewModelOverTimeWork();

                report.Name = reportsUser.Key;

                foreach (var reportUser in reportsUser
                    .Where(r => r.LoadDtm.Month == DateTime.Now.Month))
                {
                    if (reportUser.OvertimeHour >= 0)
                        report.OverTime += reportUser.OvertimeHour;
                    else if (reportUser.OvertimeHour < 0)
                        report.TimeOff += reportUser.OvertimeHour;
                }

                foreach (var reportUser in reportsUser
                    .Where(r => r.LoadDtm.Month == DateTime.Now.AddMonths(-1).Month))
                {
                    if (reportUser.OvertimeHour >= 0)
                        report.LastOverTime += reportUser.OvertimeHour;
                    else if (reportUser.OvertimeHour < 0)
                        report.LastTimeOff += reportUser.OvertimeHour;
                }

                reports.Add(report);
            }

            return reports;
        }
    }
}
