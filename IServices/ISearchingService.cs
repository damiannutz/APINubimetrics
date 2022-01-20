using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ISearchingService
    {
        public Task<Searching> GetSearching(string query);
    }
}
