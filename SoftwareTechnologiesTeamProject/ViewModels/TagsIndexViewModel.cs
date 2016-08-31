﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftwareTechnologiesTeamProject.Models;

namespace SoftwareTechnologiesTeamProject.ViewModels
{
    public class TagsIndexViewModel
    {
        public List<Tag> PopularTags { get; set; }
        public List<Post> Posts { get; set; }
    }
}