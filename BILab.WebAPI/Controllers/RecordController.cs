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
    [Authorize]
    public class RecordController : BaseCrudController<IRecordService, RecordDTO, GetShortedRecordDTO, Guid> {
        public RecordController(IRecordService service) : base(service) {
        }

        [Authorize]
        [HttpPost]
        public override async Task<IActionResult> CreateAsync([FromBody] RecordDTO createDto) {
            var result = await _service.CreateAsync(createDto);
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [Authorize]
        [HttpPut]
        public override async Task<IActionResult> UpdateAsync([FromBody] RecordDTO updateDto)
        {
            var result = await _service.UpdateAsync(updateDto);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [Authorize]
        [HttpGet("search")]
        public IActionResult GetFilteringRecords([FromQuery] PageableRecordRequestDto filters) {
            var result = _service.SearchFor<GetFullRecordDTO>(filters);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet("user-records/{userId}")]
        public async Task<IActionResult> GetUserRecords(Guid userId) {
            var result = await _service.GetRecordsByUserId(userId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpGet("employee-records/{userId}")]
        public async Task<IActionResult> GetEmployeeRecords(Guid userId) {
            var result = await _service.GetRecordsByEmployeeId(userId);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize]
        [HttpPut("closeOrCancel")] 
        public async Task<IActionResult> CloseRecord(CloseRecordDto dto) {
            var result = await _service.CloseRecord(dto);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [Authorize]
        [HttpGet("{id}/full-data")]
        public async Task<IActionResult> GetFullDataByIdAsync(Guid id)
        {
            var result = await _service.GetFullRecordDataByIdAsync(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
