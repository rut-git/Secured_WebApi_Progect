using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRatingService
    {
        Task<int> addRating(Rating rating);
    }
}
