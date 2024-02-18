using M6_C8_P1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M6_C8_P1.ViewModels
{
    public class TeamInputModel
    {
        public int Id { get; set; }
        [Required, StringLength(50), Display(Name = " Team Name")]
        public string TeamName { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Established Date")]
        public DateTime Established { get; set; }
        [EnumDataType(typeof(Country))]
        public Country Country { get; set; }
        [Required, StringLength(50)]
        public string Coach { get; set; }
        [Required, StringLength(50)]
        public string Captain { get; set; }
        [Required, StringLength(50), Display(Name = " Board President")]
        public string BoardPresident { get; set; }
        [Required]
        public int Ranking { get; set; }

        [Required]
        public HttpPostedFileBase TeamLogo { get; set; }

        [Required, StringLength(100)]
        public string HomeGround { get; set; }
        public bool IsWCWinner { get; set; }
        public virtual List<Player> Players { get; set; } = new List<Player>();
    }
}