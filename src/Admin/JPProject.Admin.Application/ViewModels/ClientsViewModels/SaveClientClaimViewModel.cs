using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace JPProject.Admin.Application.ViewModels.ClientsViewModels
{
    public class SaveClientClaimViewModel
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public string Type { get; set; }
        [JsonIgnore]
        public string ClientId { get; set; }
    }
}