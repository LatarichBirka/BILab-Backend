﻿namespace BILab.Domain.DTOs.User {
    public class ResetPasswordDTO {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
