using LibraryManagementSystemMVC_Project.BAL.ILogic;
using LibraryManagementSystemMVC_Project.DatabaseConnection;
using LibraryManagementSystemMVC_Project.Models.EntityModels;
using LibraryManagementSystemMVC_Project.Models.RequestModels;
using LibraryManagementSystemMVC_Project.Models.ResponseModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemMVC_Project.BAL.Logic
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ILogger<AccountRepository> _logger;
        private readonly DBConnectionFactory _dbConnectionFactory;
        private readonly HttpContextAccessor _httpContextAccessor;

        public AccountRepository(ILogger<AccountRepository> logger,DBConnectionFactory dbConnectionFactory)
        {
                _logger= logger;
            _dbConnectionFactory= dbConnectionFactory;
        }
        public async Task<ApplicationUserVModel?> Authentication(SignInModel signInModel)
        {           
            try
            {
                if (signInModel != null)
                {
                   
                    var userResult = await _dbConnectionFactory.tblApplicationUsers.Where(option => option.Email.Equals(signInModel.Email)).FirstOrDefaultAsync();
                    if (userResult != null)
                    {
                        if (BCrypt.Net.BCrypt.Verify(signInModel.Password, userResult.PasswordHash))
                        {
                            return new ApplicationUserVModel
                            {
                                UserId = userResult.UserId,
                                UserName = userResult.UserName,
                                Email = userResult.Email,
                                Role = userResult.Role


                            };
                            
                        }
                        else
                        {
                            //password is incorrect
                            return null;
                        }
                    }
                    else
                    {
                        //username is not match
                        return null;
                    }
                }
                else
                {
                    // model data is null or empty
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Account Repository in Authentication Method : " + ex.Message);
            }
            return null;
        }

        public async Task<bool> Registration(SignUpModel signUpModel)
        {
            try
            {
                if (await _dbConnectionFactory.tblApplicationUsers.AnyAsync(option => option.UserName.Equals(signUpModel.UserName)))
                {
                    return false;
                }
                else
                {
                    ApplicationUser user = new ApplicationUser()
                    {
                        UserId = Guid.NewGuid(),
                        UserName=signUpModel.UserName,
                        Email=signUpModel.Email,
                        PasswordHash=BCrypt.Net.BCrypt.HashPassword(signUpModel.Password),
                        Role="User",
                        CreatedBy="User",
                        CreatedOn=DateTime.Now,
                        IsDeleted=false,                        
                    };

                    _dbConnectionFactory.Add(user);
                    await _dbConnectionFactory.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical(DateTime.Now.ToString() + "Error on Account Repository in Registration method : " + ex.Message);
            }
            return false;
        }

        public async Task SignOut()
        {
            _httpContextAccessor.HttpContext.Session.Clear();

        }
    }
}
