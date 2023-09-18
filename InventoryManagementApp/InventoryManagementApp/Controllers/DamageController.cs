using InventoryManagement.Core;
using InventoryManagement.Extensions;
using InventoryManagement.Helpers.Base;
using InventoryManagement.Service.Models.Enrols;
using InventoryManagement.Service.Services.EnrolConfigurations;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DamageController : GenericBaseController<Damage>
    {
        private readonly IDamageService _damage;

        public DamageController(IDamageService damage) : base(damage)
        {
            _damage= damage;
        }
        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdownAsync(string searchText = null)
        {
            var res = await _damage.GetDropdownAsync(searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearchAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string searchText = null)
        {
            var res = await _damage.GetSearchAsync(pageIndex, pageSize, searchText);

            return new ApiOkActionResult(res);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterAsync(int pageIndex = CommonVariables.pageIndex, int pageSize = CommonVariables.pageSize, string filterText1 = null /*string filterText2 = null*/)
        {
            var res = await _damage.GetFilterAsync(pageIndex, pageSize, filterText1 /*filterText2*/);

            return new ApiOkActionResult(res);
        }
        [HttpGet("{damageId}")]
        public async Task<IActionResult> GetDamageDetailsAsync(long damageId)
        {
            var res = await _damage.GetDamageDetailsAsync(damageId);

            return new ApiOkActionResult(res);
        }
        [HttpPost()]
        public async Task<IActionResult> AddDamageDetailsAsync([FromForm] DamageModel model)
        {
            var res = await _damage.AddDamageDetailsAsync(model);

            return new ApiOkActionResult(res);
        }
        [HttpPut("{damageId}")]
        public async Task<IActionResult> UpdateDamageDetailsAsync(long damageId, [FromForm] DamageModel model)
        {

            var res = await _damage.UpdateDamageDetailsAsync(damageId, model);

            return new ApiOkActionResult(res);
        }
    }
}
