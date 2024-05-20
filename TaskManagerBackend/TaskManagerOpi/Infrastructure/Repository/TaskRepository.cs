using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class TaskRepository<T> : ITaskRepository<T> where T : class
    {
        private readonly TaskDbContext.TaskDbContext _context;

        public TaskRepository(TaskDbContext.TaskDbContext context)
        {
            _context = context;
        }

        public async Task Delete(Guid id)
        {
            string stringId = id.ToString();
            T? entity = await _context.Set<T>().FindAsync(stringId);
            _context.Set<T>().Remove(entity!);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {   
            string stringId = id.ToString();
            T? entity = await _context.Set<T>().FindAsync(stringId);            

            return entity!;
        }

        public async Task SaveEntityAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
