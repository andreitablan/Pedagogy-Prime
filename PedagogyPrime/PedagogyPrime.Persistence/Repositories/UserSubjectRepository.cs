namespace PedagogyPrime.Persistence.Repositories
{
    using Context;
    using Core.Entities;
    using Core.IRepositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserSubjectRepository : IUserSubjectRepository
    {
        private readonly PedagogyPrimeDbContext context;

        public UserSubjectRepository(PedagogyPrimeDbContext context)
        {
            this.context = context;
        }

        public async Task Add(UserSubject entity)
        {
            await context.UsersSubjects.AddAsync(entity);
        }

        public async Task<int> Delete(Guid id)
        {
            return await context.UsersSubjects.DeleteByKeyAsync(id);
        }

        public async Task<List<UserSubject>> GetAll()
        {
            return await context.UsersSubjects.AsNoTracking().ToListAsync();
        }

        public Task<UserSubject?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }


        public void Update(UserSubject entity)
        {
            context.UsersSubjects.Update(entity);
        }

        public async Task<List<User>> GetAllUsersBySubjectId(Guid subjectId)
        {
            return await context.UsersSubjects
                .AsNoTracking()
                .Where(x => x.SubjectId == subjectId)
                .Include(x => x.User)
                .Select(x => x.User)
                .ToListAsync();
        }

        public async Task<List<Subject>> GetAllSubjectsForUser(
            Guid userId
        )
        {
            return await context.UsersSubjects.Where(x => x.UserId == userId).Select(x => x.Subject).ToListAsync();
        }

        public async Task AddRange(List<UserSubject> userSubjects)
        {
            context.AddRangeAsync(userSubjects);
        }
    }
}