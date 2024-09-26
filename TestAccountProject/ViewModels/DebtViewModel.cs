using System.ComponentModel.DataAnnotations;

namespace TestAccountProject.ViewModel
{
    public class DebtViewModel
    {
        [Required]
        public string Comment;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date;

        [Required]
        public decimal Amount;
    }
}
