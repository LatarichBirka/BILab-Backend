﻿namespace BILab.Domain.DTOs.User {
    public class ChangePasswordDTO {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
