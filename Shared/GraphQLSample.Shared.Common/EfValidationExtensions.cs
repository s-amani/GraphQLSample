using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GraphQLSample.Shared.Common
{
    public static class EfValidationExtensions
    {
        public static string GetValidationErrors(this DbContext context)
        {
            var errors = new StringBuilder();
            var entities = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity);

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, validationContext, validationResults, validateAllProperties: true)) 
                    continue;
                
                foreach (var validationResult in validationResults)
                {
                    var names = validationResult.MemberNames.Aggregate((s1, s2) => $"{s1}, {s2}");
                    errors.AppendFormat("{0}: {1}", names, validationResult.ErrorMessage);
                }
            }

            return errors.ToString();
        }
    }
}