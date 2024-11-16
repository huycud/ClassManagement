using ClassManagement.Api.Data.EF;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Utilities.Common;

namespace ClassManagement.Api.Configurations
{
    static class QuartzConfig
    {
        public static void ConfigureQuartz(IServiceCollection services)
        {
            services.AddQuartz(opt =>
            {
                var deleteUnconfirmedEmailJobKey = new JobKey(JobConstants.DELETEUNCONFIRMEDEMAIL);

                opt.AddJob<DeleteUnconfirmedUsersJob>(deleteUnconfirmedEmailJobKey)

                    .AddTrigger(trigger => trigger.ForJob(deleteUnconfirmedEmailJobKey)

                                            .WithIdentity(JobConstants.DELETEUNCONFIRMEDEMAIL_TRIGGER)
                                            .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever()));
                //.WithCronSchedule("0 0 0 * * ?"));

                var deleteExpiredResetTokenJobKey = new JobKey(JobConstants.DELETEEXPIREDRESETTOKEN);

                opt.AddJob<DeleteExpiredPasswordResetTokensJob>(deleteExpiredResetTokenJobKey)

                    .AddTrigger(trigger => trigger.ForJob(deleteExpiredResetTokenJobKey)

                                            .WithIdentity(JobConstants.DELETEEXPIREDRESETTOKEN_TRIGGER)
                                            .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever()));
                //.WithCronSchedule("0 0 0 * * ?"));
            });

            services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
        }
    }

    public class DeleteUnconfirmedUsersJob(ILogger<DeleteUnconfirmedUsersJob> logger, AppDbContext appDbContext) : IJob
    {
        private readonly ILogger<DeleteUnconfirmedUsersJob> _logger = logger;

        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{JobConstants.DELETEUNCONFIRMEDEMAIL} Job excuted at {DateTime.UtcNow}");

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    var cutoffDate = DateTime.UtcNow.AddMinutes(-5);

                    var usersToDelete = _appDbContext.Users.Where(u => !u.EmailConfirmed && u.CreatedAt < cutoffDate);

                    if (!usersToDelete.Any())
                    {
                        _logger.LogInformation($"No unconfirmed email found at {DateTime.UtcNow}");
                    }
                    else
                    {
                        _appDbContext.Users.RemoveRange(usersToDelete);

                        await _appDbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        _logger.LogInformation($"{JobConstants.DELETEUNCONFIRMEDEMAIL} Job completed at {DateTime.UtcNow}");
                    }
                });
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                _logger.LogInformation($"{JobConstants.DELETEUNCONFIRMEDEMAIL} Job fail with errors {e.Message}");

                throw e;
            }
        }
    }

    public class DeleteExpiredPasswordResetTokensJob(ILogger<DeleteExpiredPasswordResetTokensJob> logger, AppDbContext appDbContext) : IJob
    {
        private readonly ILogger<DeleteExpiredPasswordResetTokensJob> _logger = logger;

        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{JobConstants.DELETEEXPIREDRESETTOKEN} Job excuted at {DateTime.UtcNow}");

            using var transaction = await _appDbContext.Database.BeginTransactionAsync();

            try
            {
                var init = _appDbContext.Database.CreateExecutionStrategy();

                await init.ExecuteAsync(async () =>
                {
                    var expiredTokensToDelete = _appDbContext.PasswordResetTokens.Where(x => x.IsUsed && x.Expiration < DateTime.UtcNow);

                    if (!expiredTokensToDelete.Any())
                    {
                        _logger.LogInformation($"No expired reset tokens found at {DateTime.UtcNow}");
                    }
                    else
                    {
                        _appDbContext.PasswordResetTokens.RemoveRange(expiredTokensToDelete);

                        await _appDbContext.SaveChangesAsync();

                        await transaction.CommitAsync();

                        _logger.LogInformation($"{JobConstants.DELETEEXPIREDRESETTOKEN} Job completed at {DateTime.UtcNow}");
                    }
                });
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();

                _logger.LogInformation($"{JobConstants.DELETEEXPIREDRESETTOKEN} Job fail with errors {e.Message}");

                throw e;
            }
        }
    }
}
