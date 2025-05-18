using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Specifications
{
	public class RateSpecification:BaseSpecification<Rate>
	{
        private int TotalReviews { get; set; }
        private int WeightedAverage { get; set; }
        private int[] Stars { get; set; }
        private int Rating { get; set; }

        public RateSpecification(int productId = -1) : base()
        {
            Stars = new int[5];
            // Default Ordering
            AddOrderByDescending(r => r.Date);

            if (productId != -1)
            {
                AddCriteria(r => r.ProductID == productId);
            }
            AddInclude(r => r.Product);
            AddInclude(r => r.Customer);

        }

        public int[] getStars()
        {
            return Stars;
        }

        public void GetTotalReviews(IReadOnlyList<Rate> reviews)
        {
            foreach (var item in reviews)
            {
                switch (item.NumberOfStart)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        Stars[item.NumberOfStart - 1] += 1;
                        break;
                }

                TotalReviews += 1;
            }
        }

        public int CalculateRating()
        {
            var counter = 1;
            foreach (var item in Stars)
            {
                WeightedAverage += counter * item;
                counter++;
            }
            if (TotalReviews != 0)
            {
                Rating = WeightedAverage / TotalReviews;
            }

            return Rating;
        }


    }
}

