using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OxuAz.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [Display(Name = "Sekil")]

        public IFormFile  Photo { get; set; }
        [Required(ErrorMessage ="Basliq Yazin")]
        [StringLength(50, ErrorMessage = "Basliq 50 simvoldan artiq ola bilmez")]
        [Display(Name = "Başlıq")]

        public string Title { get; set; }
        [Required(ErrorMessage = "Metn Yazin")]
        [StringLength(100,ErrorMessage ="Metn 100 simvoldan artiq ola bilmez")]
        [Display(Name = "Mətn")]

        public string Content { get; set; }
        [Display(Name = "Bəyənilib")]

        public int Like { get; set; }
        [Display(Name = "Bəyənilməyib")]

        public int DisLike { get; set; }
        [Display(Name = "Tarix")]

        public DateTime CreateAt { get; set; }
    }
}
