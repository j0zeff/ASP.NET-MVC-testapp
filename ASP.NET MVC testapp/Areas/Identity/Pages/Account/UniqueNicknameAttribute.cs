using System.ComponentModel.DataAnnotations;

public class UniqueNicknameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string nickname = value.ToString();
            var dbContext = validationContext.GetService(typeof(MyDbContext)) as MyDbContext;

            // Check if the nickname is already in use
            var existingUser = dbContext.AplicationUsers.FirstOrDefault(u => u.Nickname == nickname);

            if (existingUser != null)
            {
                return new ValidationResult("This nickname is already in use.");
            }
        }

        return ValidationResult.Success;
    }
}
