﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OCart.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String AvatarPath { get; set; }

        //public ICollection<Dialog> Dialogs { get; set; }
        public ICollection<ArtistComment> ArtistComments { get; set; }
    }
}
