﻿using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Pageable;
using BILab.Domain.DTOs.Procedure;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BILab.WebAPI.Controllers
{
    [Authorize(Roles = Constants.NameRoleAdmin)]
    public class ProcedureController : BaseController {
        private readonly IProcedureService _service;

        public ProcedureController(IProcedureService service) {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() {
            var result = await _service.GetAsync<ProcedureDTO>();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id) {
            var result = await _service.GetByIdAsync<ProcedureDTO>(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("/sales")]
        public async Task<IActionResult> GetAllSalesAsync() {
            var result = await _service.GetSalesAsync();
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [AllowAnonymous]
        [HttpGet("sales/{id}")]
        public async Task<IActionResult> GetSaleByIdAsync(Guid id) {
            var result = await _service.GetSaleByIdAsync(id);
            return GetResult(result, (int)HttpStatusCode.OK);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProcedureDTO createDto) {
            var result = await _service.CreateAsync(createDto);
            return GetResult(result, (int)HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id) {
            var result = await _service.DeleteAsync(id);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [Authorize(Roles = Constants.NameRoleAdmin)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProcedureDTO updateDto) {
            var result = await _service.UpdateAsync(updateDto);
            return GetResult(result, (int)HttpStatusCode.NoContent);
        }

        [AllowAnonymous]
        [HttpGet("search")]
        public IActionResult GetFilteringProcedures([FromQuery] PageableProcedureRequestDto filters) {
            var result = _service.SearchFor<GetProcedureDTO>(filters);
            return GetResult(result, (int)HttpStatusCode.OK);
        }
    }
}
