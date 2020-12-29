using IVSoftware.Data.Models;
using IVSoftware.Web.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IVSoftware.Web.BusinessLogic
{
    public static class PersonBS
    {
        public static async Task<Person> GetFullPerson(this IEntityService<Person, Guid> _personService,
            Guid? id, string email,
            IEntityService<AcademicLevel, int> _academicLevelService)
        {
            var person = id.HasValue ?
               await _personService.GetByIdAndIncludeAsync(id.Value, p => p.User,
                   p => p.BasicEducations, p => p.HigherEducations)
               : (await _personService.FindByConditionAndIncludeAsync(p => p.Email == email,
                   p => p.User, p => p.BasicEducations, p => p.HigherEducations)).FirstOrDefault();

            if (person != null && person.HigherEducations.Count > 0)
            {
                for (int i = 0; i < person.HigherEducations.Count; i++)
                {
                    person.HigherEducations[i].AcademicLevel =
                        await _academicLevelService.GetByIdAsync(person.HigherEducations[i].AcademicLevelId);
                }
            }

            return person;
        }
    }
}