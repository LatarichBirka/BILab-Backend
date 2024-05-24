using BILab.BusinessLogic.Services.EntityServices;
using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Record;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers {
    public class RecordController : BaseCrudController<IRecordService, RecordDTO, GetShortedRecordDTO, Guid> {
        public RecordController(IRecordService service) : base(service) {
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [HttpGet("search")]
        public IActionResult GetFilteringRecords([FromQuery] PageableRecordRequestDto filters) {
            var result = _service.SearchFor<GetFullRecordDTO>(filters);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [HttpGet("user-records/{userId}")]
        public async Task<IActionResult> GetUserRecords(Guid userId) {
            var result = await _service.GetRecordsByUserId(userId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [HttpGet("employee-records/{userId}")]
        public async Task<IActionResult> GetEmployeeRecords(Guid userId) {
            var result = await _service.GetRecordsByEmployeeId(userId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [HttpPut("closeOrCancel")] 
        public async Task<IActionResult> CloseRecord(CloseRecordDto dto) {
            var result = await _service.CloseRecord(dto);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [HttpGet("{id}/full-data")]
        public async Task<IActionResult> GetFullDataByIdAsync(Guid id)
        {
            var result = await _service.GetFullRecordDataByIdAsync(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpGet("statistic-by-data")]
        public async Task<IActionResult> GetRecordsByTimePeriod(DateTime fromDate, DateTime toTime)
        {
            var result = await _service.GetRecordsByTimePeriodAsync(fromDate, toTime);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
