using System;
using System.Net.Http;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace IServices
{
    public interface ICountryService
    {
        Task<Country> GetCountry(string country);
    }
}
