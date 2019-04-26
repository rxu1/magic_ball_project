using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MagicBall.Models
{
  public class MagicBall
  {
    [Key]
    public string Question { get; set; }
  }
}