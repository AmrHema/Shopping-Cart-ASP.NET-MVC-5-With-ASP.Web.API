﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chart_Leader.Models
{
    public class CategoriesViewModel
    {
        [Display(Name ="Categorie ID")]
        public int Cat_id { get; set; }

        [Display(Name = "Categorie Name")]
        public string Cat_Name { get; set; }

        [Display(Name = "Categorie Description")]
        public string Cat_Description { get; set; }

        [Display(Name = "Categorie Image")]
        public string Cat_Image { get; set; }
    }
}