using WEB.DAL;
using WEB.Repositories.Response;

namespace WEB.BLL
{
    public class AdminService
    {
        UserRep uRep = new UserRep();
        PostRep pRep = new PostRep();
        ReactReq rRep = new ReactReq();
        CommentRep cRep = new CommentRep();
        
        public StatsResponse CountStats(int month, int year)
        {
            StatsResponse statsResponse = new StatsResponse();
            statsResponse.CountUser = uRep.CountUser(month, year);
            statsResponse.CountPost = pRep.CountPost(month, year);
            statsResponse.CountComment = cRep.CountComment(month, year);
            statsResponse.CountReact = rRep.CountReact(month, year);

            return statsResponse;
        }
    }
}
