using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Service.Services.EnrolConfigurations;
using InventoryManagement.Sql.Entities.Configurations;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : GenericBaseController<Company>
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService) : base(companyService)
        {
            this._companyService = companyService;
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _companyService.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _companyService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _companyService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompanyDetailsAsync(long companyId)
        {
            var res = await _companyService.GetCompanyDetailsAsync(companyId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddCompanyDetailsAsync([FromForm] CompanyModel model)
        {
            var res = await _companyService.AddCompanyDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompanyDetailsAsync(long companyId, [FromForm] CompanyModel model)
        {

            var res = await _companyService.UpdateCompanyDetailsAsync(companyId, model);

            return new ApiOkActionResult(res);
        }
    }
}
