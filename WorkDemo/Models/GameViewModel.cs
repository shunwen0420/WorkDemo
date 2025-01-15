using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkDemo.Models
{
    [Table("Game")]
    public class GameViewModel()
    {
        [Key]
        [Display(Name = "遊戲流水號")]
        public int GameID { get; set; }

        [Display(Name = "遊戲名稱")]
        [Required]
        [MaxLength(20)]
        public string GameName { get; set; } = string.Empty;

        [Display(Name = "圖片")]
        [Required]
        public string Image { get; set; } = string.Empty;

        [Display(Name = "價錢")]
        [Required]
        public double Price { get; set; }
    }
}
