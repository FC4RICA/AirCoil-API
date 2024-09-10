using AirCoil_API.Dto;
using Microsoft.Data.SqlClient.DataClassification;
using System.ComponentModel.DataAnnotations;

namespace AirCoil_API.Helpers
{
    public class JobQueryObject
    {
        [DataType(DataType.Date)]
        public DateTime? StartDate {  get; set; }
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
