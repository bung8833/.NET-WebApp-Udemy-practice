using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{
    public class Character : IFigure
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HP { get; set; } = 200;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public User? User { get; set; }
        public Weapon? Weapon { get; set; }
        public List<Skill>? Skills { get; set; }
        public int Fights { get; set; } = 0;
        public int Victories { get; set; } = 0;
        public int Defeats { get; set; } = 0;
    }
}