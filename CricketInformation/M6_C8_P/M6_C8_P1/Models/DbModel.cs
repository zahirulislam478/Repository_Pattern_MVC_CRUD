using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace M6_C8_P1.Models
{
    public enum Country { Australia = 1, England, India, Pakistan, Newzealand, SouthAfrica, Bangladesh, Afghanistan, WestIndies, SriLanka, Ireland, Zimbabwe }
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }

    public class Team : EntityBase
    {
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

        [Required, StringLength(50)]
        public string TeamLogo { get; set; }
        [Required, StringLength(100)]
        public string HomeGround { get; set; }
        public bool IsWCWinner { get; set; }
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }

    public class Player : EntityBase
    {
        [Required, StringLength(50), Display(Name = "Player Name")]
        public string PlayerName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required, StringLength(50), Display(Name = "Playing Role")]
        public string PlayingRole { get; set; }
        [Required]
        public int Matches { get; set; }
        [Required]
        public int Runs { get; set; }
        [Required]
        public int Hundred { get; set; }
        [Required]
        public int Fifty { get; set; }
        [Required]
        public int Wicket { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Salary { get; set; }

        [Required, ForeignKey("Team")]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }

    public class CricketDbContext : DbContext
    {
        public CricketDbContext()
        {
           Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }

    public class DbInitializer : DropCreateDatabaseIfModelChanges<CricketDbContext>
    {
        protected override void Seed(CricketDbContext context)
        {
            //Team t1 = new Team
            //{
            //    TeamName = "Australia",
            //    Established = DateTime.Parse("1887,1, 1"),
            //    Country = Country.Australia,
            //    Coach = "Justin Langer",
            //    Captain = "Aaron Finch",
            //    BoardPresident = "Earl Eddings",
            //    Ranking = 3,
            //    TeamLogo = "1.png",
            //    HomeGround = "Melbourne Cricket Ground",
            //    IsWCWinner = true
            //};

            //Team t2 = new Team
            //{
            //    TeamName = "India",
            //    Established = DateTime.Parse("1932,1, 1"),
            //    Country = Country.India,
            //    Coach = "Ravi Shastri",
            //    Captain = "Virat Kohli",
            //    BoardPresident = "Sourav Ganguly",
            //    Ranking = 2,
            //    TeamLogo = "6.png",
            //    HomeGround = "Eden Gardens",
            //    IsWCWinner = true
            //};

            //context.Teams.Add(t1);
            //context.Teams.Add(t2);
            //context.SaveChanges();

            //t1.Players.Add(new Player
            //{
            //    PlayerName = "David Warner",
            //    Age = 34,
            //    PlayingRole = "Batsman",
            //    Matches = 123,
            //    Runs = 5643,
            //    Hundred = 18,
            //    Fifty = 25,
            //    Wicket = 8,
            //    Salary = 1200000,
            //    Id = 1
            //});

            //t2.Players.Add(new Player
            //{
            //    PlayerName = "Jasprit Bumrah",
            //    Age = 30,
            //    PlayingRole = "Bowler",
            //    Matches = 65,
            //    Runs = 214,
            //    Hundred = 0,
            //    Fifty = 0,
            //    Wicket = 108,
            //    Salary = 800000,
            //    Id = 2
            //});

            //context.SaveChanges();
        }
    }
}