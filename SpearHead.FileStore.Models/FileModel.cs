using System.ComponentModel.DataAnnotations;
namespace SpearHead.FileStore.Models
{
    public sealed class FileModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "File byte cannot have blank values")]
        public byte[] FileBytes { get; set; }
    }
}
