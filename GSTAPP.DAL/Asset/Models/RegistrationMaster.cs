namespace GSTAPP.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegistrationMaster")]
    public partial class RegistrationMaster
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(15)]
        public string PhoneNo { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        public bool? EmailVerification { get; set; }

        public bool? PhoneNoVerification { get; set; }

        [StringLength(10)]
        public string EmailVerificationCode { get; set; }

        [StringLength(10)]
        public string PhoneNoVerificationCode { get; set; }

        public DateTime? ValidAt { get; set; }
    }
}
