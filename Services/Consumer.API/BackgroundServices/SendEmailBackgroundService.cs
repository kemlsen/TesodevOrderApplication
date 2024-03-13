using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System;
using Consumer.API.Helper;
using Consumer.API.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Consumer.API.Excel;

namespace Consumer.API.BackgroundServices
{
    public class SendEmailBackgroundService : BackgroundService
    {
        private readonly IEmailHelper _emailHelper;
        private readonly ConsumerDbContext _consumerDbContext;
        private readonly ExcelUtilities _excelUtilities;
        public SendEmailBackgroundService(IEmailHelper emailHelper, ConsumerDbContext consumerDbContext, ExcelUtilities excelUtilities)
        {
            _emailHelper = emailHelper;
            _consumerDbContext = consumerDbContext;
            _excelUtilities = excelUtilities;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var subject = "Günlük Sipariş Logları";

                var body = "Merhabalar , \r\n\r\nGünlük sipariş logları ektedir. \r\n\r\nBilgilerinize.";

                var dailyLogs = await _consumerDbContext.Audits.AsQueryable().Where(x => x.CreatedAt >= DateTime.Now.AddDays(-1)).ToListAsync();

                var stream = _excelUtilities.DailyOrderLogsToExcel(dailyLogs);

                await _emailHelper.SendMailAsync(subject: subject, body: body, recipients: null, stream: stream); ;
            
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
