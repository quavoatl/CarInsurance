using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV2.Repositories.Cover
{
    public interface ICoverRepository
    {
        List<DbModels.Cover> GetCovers();
        DbModels.Cover GetCover(string guid);
        void UpdateCover(DbModels.Cover cover);

        void Insert(DbModels.Cover cover);
    }
}
