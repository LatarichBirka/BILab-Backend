using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Shedule;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers
{
    public class SheduleController : BaseCrudController<ISheduleService, SheduleDTO, SheduleDTO, Guid> {
        public SheduleController(ISheduleService service) : base(service) {
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpPost]
        public override async Task<IActionResult> CreateAsync([FromBody] SheduleDTO createDto)
        {
            var result = await _service.CreateAsync(createDto);
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpPut]
        public override async Task<IActionResult> UpdateAsync([FromBody] SheduleDTO updateDto)
        {
            var result = await _service.UpdateAsync(updateDto);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [AllowAnonymous]
        [HttpGet]
        public override async Task<IActionResult> GetAllAsync() {
            var result = await _service.GetAsync<SheduleDTO>();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetByIdAsync(Guid id) {
            var result = await _service.GetByIdAsync<SheduleDTO>(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public IActionResult GetFilteringShedules([FromQuery] PageableSheduleRequestDto filters) {
            var result = _service.SearchFor<SheduleDTO>(filters);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [HttpGet("/{employeeId}")]
        public IActionResult GetFreeSchedule(Guid employeeId, [FromQuery] DateTime checkData) {
            var result = _service.GetFreeShedule(employeeId, checkData);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("/{employeeId}/schedules")]
        public async Task<IActionResult> GetSchedulesByEmployee(Guid employeeId)
        {
            var result = await _service.GetScheduleByEmployee(employeeId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}