﻿using BILab.Domain;
using BILab.Domain.Contracts.Services.EntityServices;
using BILab.Domain.DTOs.Shedule;
using BILab.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

namespace BILab.WebAPI.Controllers {
    [Authorize(Roles = $"{Constants.NameRoleAdmin}, {Constants.NameRoleEmployee}")]
    public class SheduleController : BaseCrudController<ISheduleService, SheduleDTO, SheduleDTO, Guid> {
        public SheduleController(ISheduleService service) : base(service) {
        }
    }
}
