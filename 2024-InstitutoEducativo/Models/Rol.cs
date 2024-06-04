using _2024_InstitutoEducativo.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _2024_InstitutoEducativo.Models
{
    public class Rol : IdentityRole<int>
    {



        public Rol() : base() { }

        public Rol(string rolName) : base(rolName) { }

        [Display(Name = "Nombre")]
        public override string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        public override string NormalizedName
        {
            get => base.NormalizedName;
            set => base.NormalizedName = value;
        }

    }
}

