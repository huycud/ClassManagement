using Microsoft.AspNetCore.Identity;

namespace ClassManagement.Api.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public bool IsDisabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Client Client { get; set; }
        public virtual List<Notify>? Notifies { get; set; } //teacher, admin
        public virtual Image? Images { get; set; } // student,teacher, admin
        public virtual List<Class>? Classes { get; set; } // teacher
        public virtual List<Score>? Scores { get; set; } // student
        public virtual Evalution? Evalution { get; set; } // student
        public virtual List<StudentClass>? StudentClasses { get; set; } // student
        public virtual List<PasswordResetToken> PasswordResetTokens { get; set; }
    }
}
