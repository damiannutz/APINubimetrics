using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetSave();
    }
}
