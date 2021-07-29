using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMF_WebSite.ViewModels
{
    public class ResultDetailsVM
    {
        BMFEntities1 db = new BMFEntities1();

        public List<M_EventRoundSessionUserResults> Results { get; set; }
        public List<M_EventSeason> Seasons { get; set; }
        public List<M_EventRound> Rounds { get; set; }
        public List<M_EventRoundSession> Sessions { get; set; }

        public void Get_SeasonsByCategory(long ID_EventCategory)
        {
            try
            {
                Seasons = (from results in db.M_EventRoundSessionUserResults
                           where results.IsActive == true
                           && results.DeletedOnUtc == null

                           && results.M_EventRoundSession.IsActive
                           && !results.M_EventRoundSession.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.M_EventRound.IsActive
                           && !results.M_EventRoundSession.M_EventRound.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.M_EventRound.M_EventCategory.IsActive
                           && !results.M_EventRoundSession.M_EventRound.M_EventCategory.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.M_EventRound.M_EventCategory.M_EventSeason.IsActive
                           && !results.M_EventRoundSession.M_EventRound.M_EventCategory.M_EventSeason.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.M_EventRound.M_EventCategory.ID == ID_EventCategory

                           select results.M_EventRoundSession.M_EventRound.M_EventCategory.M_EventSeason
                          ).Distinct().ToList();
            }
            catch (Exception ex)
            {

            }
        }

        public void Get_RoundsBySeason(long ID_Season)
        {
            try
            {
                Rounds = (from results in db.M_EventRoundSessionUserResults
                          where
                          results.IsActive
                          && !results.DeletedOnUtc.HasValue

                          && results.M_EventRoundSession.IsActive
                          && !results.M_EventRoundSession.DeletedOnUtc.HasValue

                          && results.M_EventRoundSession.M_EventRound.IsActive
                          && !results.M_EventRoundSession.M_EventRound.DeletedOnUtc.HasValue

                          && results.M_EventRoundSession.M_EventRound.M_EventCategory.IsActive
                          && !results.M_EventRoundSession.M_EventRound.M_EventCategory.DeletedOnUtc.HasValue

                          && results.M_EventRoundSession.M_EventRound.M_EventSeason.IsActive
                          && !results.M_EventRoundSession.M_EventRound.M_EventSeason.DeletedOnUtc.HasValue

                          && results.M_EventRoundSession.M_EventRound.M_EventSeason.ID == ID_Season

                          select results.M_EventRoundSession.M_EventRound
                          ).Distinct().ToList();
            }
            catch (Exception ex)
            {

            }
        }

        public void Get_SessionsByRound(long ID_Round)
        {
            try
            {
                Sessions = (from results in db.M_EventRoundSessionUserResults
                            where
                            results.IsActive
                            && !results.DeletedOnUtc.HasValue

                            && results.M_EventRoundSession.IsActive
                            && !results.M_EventRoundSession.DeletedOnUtc.HasValue

                            && results.M_EventRoundSession.M_EventRound.IsActive
                            && !results.M_EventRoundSession.M_EventRound.DeletedOnUtc.HasValue

                            && results.M_EventRoundSession.M_EventRound.M_EventCategory.IsActive
                            && !results.M_EventRoundSession.M_EventRound.M_EventCategory.DeletedOnUtc.HasValue
                            
                            && results.M_EventRoundSession.M_EventRound.M_EventSeason.IsActive
                            && !results.M_EventRoundSession.M_EventRound.M_EventSeason.DeletedOnUtc.HasValue

                            && results.M_EventRoundSession.M_EventRound.ID == ID_Round

                            select results.M_EventRoundSession
                          ).Distinct().ToList();
            }
            catch (Exception ex)
            {

            }
        }

        public void Get_Results(long ID_Season, long ID_Round, long ID_Session)
        {
            try
            {
                Results = (from results in db.M_EventRoundSessionUserResults
                           where
                           results.IsActive
                           && !results.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.IsActive
                           && !results.M_EventRoundSession.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.M_EventRound.IsActive
                           && !results.M_EventRoundSession.M_EventRound.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.M_EventRound.M_EventCategory.IsActive
                           && !results.M_EventRoundSession.M_EventRound.M_EventCategory.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.M_EventRound.M_EventSeason.IsActive
                           && !results.M_EventRoundSession.M_EventRound.M_EventSeason.DeletedOnUtc.HasValue

                           && results.M_EventRoundSession.M_EventRound.M_EventSeason.ID == ID_Season
                           && results.M_EventRoundSession.M_EventRound.ID == ID_Round
                           && results.M_EventRoundSession.ID == ID_Session

                           select results
                          ).ToList();
            }
            catch (Exception ex)
            {

            }
        }
    }
}