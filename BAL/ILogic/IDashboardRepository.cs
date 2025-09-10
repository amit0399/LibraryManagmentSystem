using LibraryManagementSystemMVC_Project.Models.ResponseModels;

namespace LibraryManagementSystemMVC_Project.BAL.ILogic
{
    public interface IDashboardRepository
    {
        Task<DashboardVModel> AdminDashboard();
        Task<List<UserDashboardVModel>> UserDashboard(string userId);

        Task<List<BookInventoryVModel>> GetBookInventory();

    }
}
