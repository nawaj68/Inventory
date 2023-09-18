using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Service.Services.Base;
using InventoryManagement.Service.Services.Configurations;
using InventoryManagement.Sql.Entities.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers.Configurations
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : GenericBaseController<State>
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService) : base(stateService)
        {
            this._stateService = stateService;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(long? countryId = null, string searchText = null)
        {
            var res = await _stateService.GetDropdownAsync(countryId ,searchText);

            return new ApiOkActionResult(res);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _stateService.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _stateService.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{stateId}")]
        public async Task<IActionResult> GetStateDetailsAsync(long stateId)
        {
            var res = await _stateService.GetStateDetailsAsync(stateId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddStateDetailsAsync([FromForm] StateModel model)
        {
            var res = await _stateService.AddStateDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{stateId}")]
        public async Task<IActionResult> UpdateStateDetailsAsync(long stateId, [FromForm] StateModel model)
        {

            var res = await _stateService.UpdateStateDetailsAsync(stateId, model);

            return new ApiOkActionResult(res);
        }
    }
}
