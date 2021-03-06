﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class AgreementFacilityViewModel
    {
        public AgreementFacilityViewModel()
        {
            agreementFacilityList = new List<AgreementFacilityViewModel>();
        }
        public int Id { get; set; }
        [DisplayName("Facility ID")]
        public string Facility_id { get; set; }
        [DisplayName("Facility Name")]
        public string Facility_Name { get; set; }
        [DisplayName("Numbers available")]
        public int Numbers_available { get; set; }
        public List<AgreementFacilityViewModel> agreementFacilityList { get; set; }

    }
}