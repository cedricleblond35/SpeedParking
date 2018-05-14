//using BO;
using BO;
using DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RepositoryFactory
    {

        public static IRepository<T> GetRepository<T>() where T : IIdentifiable
        {
            return new SerializerRepository<T>();
        }

        public static IRepository<Participant> GetCompetitorRepository(Context context) 
        {
            return new CompetitorRepository(context);
        }
        public static IRepository<Race> GetRaceRepository(Context context)
        {
            return new RaceRepository(context);
        }

        public static IRepository<Organizer> GetOrganizerRepository(Context context)
        {
            return new OrganizerRepository(context);
        }
    }
}
