﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Zero.Editions.Dto
{
    public class CreateEditionDto
    {
        [Required]
        public EditionCreateDto Edition { get; set; }

        [Required]
        public List<NameValueDto> FeatureValues { get; set; }
        
        public List<string> GrantedPermissionNames { get; set; }
        
        public List<int> GrantedDashboardWidgets { get; set; }
    }
}