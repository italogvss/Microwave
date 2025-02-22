﻿using Shared.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    internal interface RepositoryInterface<T>
    {
            Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
            Task AddAsync(T entity);
            Task UpdateAsync(T entity);
            Task DeleteAsync(int id);
    }
}
