﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareTechnologiesTeamProject.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }


        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Display(Name = "Age:")]
        public int Age { get; set; }


        [Display(Name = "City:")]
        [StringLength(50)]
        public string City { get; set; }



        [Display(Name = "Full name:")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Display(Name = "Interests:")]
        [StringLength(200)]
        public string Interests { get; set; }

        [StringLength(500)]
        [Display(Name = "More Info:")]
        public string MoreInfo { get; set; }

        [StringLength(200)]
        public string ProfilePic { get; set; }

        //public void Update(ApplicationUser Profile, EditProfileViewModel viewModel)
        //{
        //    this.User = Profile.Id;

        //    this.FullName = viewModel.FullName;

        //   //TODO
        //}
    }
}