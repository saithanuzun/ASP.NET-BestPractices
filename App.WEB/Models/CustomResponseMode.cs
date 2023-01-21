using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Web.Models
{
    public class CustomResponseModel<T> 
    {
        public T Data { get; set; }
        public List<String> Errors { get; set; }
    }
}