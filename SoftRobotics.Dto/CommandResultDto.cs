using System;
namespace SoftRobotics.Dto
{
	public class CommandResultDto
	{
        public bool? IsSuccess { get; set; }
        public string? Message { get; set; } = "Başarılı Giriş";
        public string? ErrorMessage { get; set; } = "Hatalı Giriş";
    }
}

