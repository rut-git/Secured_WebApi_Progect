using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService:IRatingService
    {
        private IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
        public async Task<int> addRating(Rating rating)
        {
            return await _ratingRepository.addRating(rating);
        }
    }
}
