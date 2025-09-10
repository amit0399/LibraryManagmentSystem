using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.DatabaseConnection;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemMVC_Project.BAL.Logic
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ILogger<ApplicationUserRepository> _logger;
        private readonly DBConnectionFactory _dbConnectionFactory;

        public ApplicationUserRepository(ILogger<ApplicationUserRepository> logger, DBConnectionFactory dbConnectionFactory)
        {
            _logger = logger;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> DeleteApplicationUser(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var result = await _dbConnectionFactory.tblApplicationUsers.Where(option => option.UserId == Guid.Parse(userId)).FirstOrDefaultAsync();
                    if (result != null)
                    {
                        result.DeletedOn = DateTime.Now;
                        result.DeletedBy = "Admin";
                        result.IsDeleted = true;

                        await _dbConnectionFactory.SaveChangesAsync();

                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Repository : " + ex.Message);
                return 0;

            }
            return 1;
        }

        public async Task<IEnumerable<ApplicationUserVModel>> GetAllUserMembers()
        {
            IList<ApplicationUserVModel> applicationUserVModels = new List<ApplicationUserVModel>();
            try
            {
                applicationUserVModels = await _dbConnectionFactory.tblApplicationUsers.Where(option => option.IsDeleted == false).Select(option => new ApplicationUserVModel()
                {
                    UserId = option.UserId,
                    UserName = option.UserName
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Repository getAllUsermember Method : " + ex.Message);
            }

            return applicationUserVModels;
        }

        public async Task<ApplicationUserVModel> GetApplicationUserDeatils(string userId)
        {
            ApplicationUserVModel applicationUserVModel = new ApplicationUserVModel();
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    applicationUserVModel = await _dbConnectionFactory.tblApplicationUsers.Where(option => option.UserId == Guid.Parse(userId) && option.IsDeleted == false).Select(option => new ApplicationUserVModel()
                    {
                        UserId = option.UserId,
                        UserName = option.UserName,
                        Email = option.Email
                        
                    }).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "eeror" + ex.Message);
            }
            return applicationUserVModel;
        }

        public async Task<IEnumerable<ApplicationUserVModel>> GetApplicationUserList()
        {

            IList<ApplicationUserVModel> applicationUserVModels = new List<ApplicationUserVModel>();
            try
            {
                applicationUserVModels = await _dbConnectionFactory.tblApplicationUsers.Where(option => option.IsDeleted == false).Select(option => new ApplicationUserVModel()
                {
                    UserId = option.UserId,
                    UserName = option.UserName,
                    Email = option.Email

                }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on UserMenmberRepository in GetUserMemberList : " + ex.Message);

            }
            return applicationUserVModels;

        }
    }
}
