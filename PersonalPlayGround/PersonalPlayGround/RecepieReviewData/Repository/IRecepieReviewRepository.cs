﻿using System.Collections.Generic;

namespace PersonalPlayGround.RecepieReviewData.Repository
{
    public interface IRecepieReviewRepository
    {
        List<RecepieReview> GetAllRecepieReviews();
        List<RecepieReview> GetRecepieReviewsByRecepieId(int recepieId);
        void AddRecepieReview(RecepieReview recepieReview);
    }
}