using System;
using System.Collections.Generic;
using System.Text;

namespace WeddingApp.Infrastructure.SQLData
{
   public interface IDatabaseInitialise
    {
        void SeedDatabase(DBContext ctx);
    }
}
