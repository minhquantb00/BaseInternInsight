﻿using BaseInsightDotNet.Commons.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Core.Entities
{
    [Table("ConfirmEmail_tbl")]
    public class ConfirmEmail : BaseEntity
    {
        public string ConfirmCode { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsConfirm {  get; set; }
    }
}
