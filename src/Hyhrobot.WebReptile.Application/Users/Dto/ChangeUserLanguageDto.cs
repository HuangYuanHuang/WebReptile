using System.ComponentModel.DataAnnotations;

namespace Hyhrobot.WebReptile.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}